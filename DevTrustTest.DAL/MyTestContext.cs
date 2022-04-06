using Microsoft.EntityFrameworkCore;
using DevTrustTest.DevTrustTest.DAL.Models;

namespace DevTrustTest.DAL
{
    public class MyTestContext : DbContext
    {
        public MyTestContext(DbContextOptions options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
