using EntityFrameworkSample.DataAccessLayer;
using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EntityFrameworkSample.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private readonly ISampleDatabase database;

        public CompanyController(ISampleDatabase database)
        {
            this.database = database;
        }

        [Route("create/{name}")]
        public async Task<IHttpActionResult> Create(string name)
        {
            var company = new Company(name);
            this.database.Add(company);
            return await this.SaveAndReturn(company);
        }

        [Route("get/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == id);
            if (company == null)
            {
                return BadRequest();
            }

            return Ok(company);
        }

        [Route("get")]
        public IHttpActionResult Get()
        {
            return Ok(this.database.Get<Company>());
        }

        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody]Company company)
        {
            var companyInDb = this.database.Get<Company>().SingleOrDefault(c => c.Id == company.Id);
            if (companyInDb == null)
            {
                return BadRequest();
            }

            companyInDb.Copy(company);
            return await this.SaveAndReturn(company);
        }

        [Route("delete/{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == id);
            if (company == null)
            {
                return BadRequest();
            }

            this.database.Remove(company);
            return await this.SaveAndReturn(company);
        }

        private async Task<IHttpActionResult> SaveAndReturn(IEntity entity)
        {
            var error = await this.database.CommitAsync() == 0;

            if (error)
            {
                return InternalServerError();
            }

            return Ok(entity);
        }
    }
}
