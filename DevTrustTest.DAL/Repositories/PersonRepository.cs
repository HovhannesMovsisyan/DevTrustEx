using Microsoft.EntityFrameworkCore;
using DevTrustTest.DAL.RepositoryInterfaces;
using DevTrustTest.DevTrustTest.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using DevTrustTest.DTO;

namespace DevTrustTest.DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        protected readonly MyTestContext _context;
        public PersonRepository(MyTestContext context) => _context = context;
        public Person Add(Person entity)
        {
            _context.Persons.Add(entity);
            return entity;
        }

        public IEnumerable<Person> GetAll(IFilter<Person> filter) => filter.Filter(_context.Persons.Include(p => p.Address));
        public Person GetById(long id) => _context.Persons.Find(id);
        public Person Update(Person entity)
        {
            var newPerson = _context.Persons.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (newPerson != null)
            {
                newPerson.LastName = entity.LastName;
                newPerson.FirstName = entity.FirstName;
                newPerson.Address = new Address
                {
                    Id = entity.Address.Id,
                    AddressLine = entity.Address.AddressLine,
                    City = entity.Address.City
                };
            }

            _context.SaveChanges();
            return newPerson;
        }
    }
}
