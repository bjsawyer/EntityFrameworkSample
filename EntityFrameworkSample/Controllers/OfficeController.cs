using System.Threading.Tasks;
using System.Web.Http;
using EntityFrameworkSample.DataAccessLayer;
using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;

namespace EntityFrameworkSample.Controllers
{
    [RoutePrefix("api/office")]
    public class OfficeController : ApiController
    {
        private readonly ISampleDatabase database;

        public OfficeController(ISampleDatabase database)
        {
            this.database = database;
        }

        [HttpGet]
        [Route("create/{suiteNumber:int}")]
        public async Task<IHttpActionResult> Create(int suiteNumber)
        {
            var office = new Office(suiteNumber);
            this.database.Add(office);
            return await this.SaveAndReturn(office);
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
