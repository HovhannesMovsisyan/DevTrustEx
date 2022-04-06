namespace DevTrustTest.BLL.Models
{
    public class PersonModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long? AddressId { get; set; }
        public virtual AddressModel Address { get; set; }
    }
}
