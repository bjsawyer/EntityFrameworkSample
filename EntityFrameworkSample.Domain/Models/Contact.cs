using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkSample.Domain.Models
{
    public class Contact : IEntity
    {
        public Contact(string name)
        {
            Name = name;
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime HireDate { get; set; }

        public void Copy(Contact contactToCopy)
        {
            this.Id = contactToCopy.Id;
            this.Name = contactToCopy.Name;
            this.HireDate = contactToCopy.HireDate;
        }
    }
}
