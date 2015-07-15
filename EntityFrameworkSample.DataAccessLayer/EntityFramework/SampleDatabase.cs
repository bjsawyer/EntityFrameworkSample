using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkSample.DataAccessLayer.EntityFramework
{
    public class SampleDatabase : DbContext, ISampleDatabase
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Office> Offices { get; set; }

        // Optionally mask the EF DbSet with IQueryable - gains abstraction at the cost of functionality
        public IQueryable<T> Get<T>() where T: class, IEntity
        {
            return this.Set<T>();
        }

        public T Add<T>(T entity) where T : class, IEntity
        {
            return this.Set<T>().Add(entity);
        }

        public T Remove<T>(T entity) where T : class, IEntity
        {
            return this.Set<T>().Remove(entity);
        }

        public async Task<int> CommitAsync()
        {
            return await this.CommitAsync(CancellationToken.None);
        }

        public async Task<int> CommitAsync(CancellationToken token)
        {
            return await this.SaveChangesAsync(token);
        }
    }
}
