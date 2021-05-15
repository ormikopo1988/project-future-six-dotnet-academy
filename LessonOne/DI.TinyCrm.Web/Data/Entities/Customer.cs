namespace DI.TinyCrm.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string VatNumber { get; set; }
        
        public string Address { get; set; }
    }
}
