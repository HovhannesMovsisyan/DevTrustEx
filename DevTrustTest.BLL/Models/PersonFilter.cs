using DevTrustTest.DevTrustTest.DAL.Models;
using DevTrustTest.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevTrustTest.BLL.Models
{
    public class PersonFilter : IFilter<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public IQueryable<Person> Filter(IQueryable<Person> query)
        {
            if (!string.IsNullOrEmpty(FirstName))
            {
                query = query.Where(p => p.FirstName == this.FirstName);
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                query = query.Where(p => p.LastName == this.LastName);
            }
            if (!string.IsNullOrEmpty(City))
            {
                query = query.Include(p => p.Address).Where(a => a.Address.City == this.City);
            }
            return query;
        }
    }
}
