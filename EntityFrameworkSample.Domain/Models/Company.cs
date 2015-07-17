using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EntityFrameworkSample.Domain.Models
{
    public class Company : IEntity
    {
        public Company()
        {
        }

        public Company(string name)
        {
            Name = name;
            this.Contacts = new List<Contact>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public Office Office { get; set; }

        public ICollection<Contact> Contacts { get; set; }

        public Contact GetMostSeniorContact()
        {
            return Contacts.OrderBy(contact => contact.HireDate).FirstOrDefault();
        }

        public void Copy(Company companyToCopy)
        {
            this.Id = companyToCopy.Id;
            this.Name = companyToCopy.Name;
            this.Office = companyToCopy.Office;
            this.Contacts = companyToCopy.Contacts;
        }
    }
}
