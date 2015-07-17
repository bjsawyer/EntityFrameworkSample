using System.Collections.Generic;
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

        [Route("addOffice/{id:int}")]
        public async Task<IHttpActionResult> AddOffice(int companyId, int officeId)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == companyId);
            if (company == null)
            {
                return BadRequest();
            }

            var office = this.database.Get<Office>().SingleOrDefault(o => o.Id == officeId);
            if (office == null)
            {
                return BadRequest();
            }

            company.Office = office;
            return await this.SaveAndReturn(company);
        }

        [Route("addContact/{id:int}")]
        public async Task<IHttpActionResult> AddContact(int companyId, int contactId)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == companyId);
            if (company == null)
            {
                return BadRequest();
            }

            var contact = this.database.Get<Contact>().SingleOrDefault(ct => ct.Id == contactId);
            if (contact == null)
            {
                return BadRequest();
            }

            company.Contacts.Add(contact);
            return await this.SaveAndReturn(company);
        }

        [Route("removeContact/{id:int}")]
        public async Task<IHttpActionResult> RemoveContact(int companyId, int contactId)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == companyId);
            if (company == null)
            {
                return BadRequest();
            }

            var contact = this.database.Get<Contact>().SingleOrDefault(ct => ct.Id == contactId);
            if (contact == null)
            {
                return BadRequest();
            }

            company.Contacts.Remove(contact);
            return await this.SaveAndReturn(company);
        }

        [Route("getContacts/{id:int}")]
        public IHttpActionResult GetContacts(int id)
        {
            var company = this.database.Get<Company>().SingleOrDefault(c => c.Id == id);
            if (company == null)
            {
                return BadRequest();
            }

            var contacts = company.Contacts;
            if (contacts == null)
            {
                return BadRequest();
            }

            return Ok(contacts);
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
