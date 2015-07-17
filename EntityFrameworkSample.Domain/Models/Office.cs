namespace EntityFrameworkSample.Domain.Models
{
    public class Office : IEntity
    {
        public Office(int suiteNumber)
        {
            this.SuiteNumber = suiteNumber;
        }

        public int Id { get; set; }

        public int SuiteNumber { get; set; }

        public Company Company { get; set; }
    }
}
