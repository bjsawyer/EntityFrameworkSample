using System;

namespace EntityFrameworkSample.Domain.Models
{
    public class Contact : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime HireDate { get; set; }
    }
}
