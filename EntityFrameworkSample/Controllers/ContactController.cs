using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EntityFrameworkSample.DataAccessLayer;
using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;

namespace EntityFrameworkSample.Controllers
{
    [RoutePrefix("api/contact")]
    public class ContactController : ApiController
    {
        private readonly ISampleDatabase database;

        public ContactController(ISampleDatabase database)
        {
            this.database = database;
        }

        [Route("create/{name}")]
        public async Task<IHttpActionResult> Create(string name)
        {
            var contact = new Contact(name);
            this.database.Add(contact);
            return await this.SaveAndReturn(contact);
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
