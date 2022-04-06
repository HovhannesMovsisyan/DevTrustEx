using DevTrustTest.DevTrustTest.DAL.Models;
using DevTrustTest.DTO;
using System.Collections.Generic;

namespace DevTrustTest.DAL.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        Person GetById(long id);
        IEnumerable<Person> GetAll(IFilter<Person> filter);
        Person Add(Person entity);
        Person Update(Person entity);
    }
}
