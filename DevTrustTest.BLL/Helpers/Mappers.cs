using AutoMapper;
using DevTrustTest.BLL.Models;
using DevTrustTest.DevTrustTest.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DevTrustTest.BLL.Helpers
{
    public static class Mappers
    {
        public static Person Map(this PersonModel personModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<PersonModel, Person>(); cfg.CreateMap<AddressModel, Address>(); });
            var mapper = config.CreateMapper();
            var person = mapper.Map<Person>(personModel);
            return person;
        }
        public static PersonModel Map(this Person person)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonModel>(); cfg.CreateMap<Address, AddressModel>(); });
            var mapper = config.CreateMapper();
            var personModel = mapper.Map<PersonModel>(person);
            return personModel;
        }
        public static PersonFilter Map(this GetAllRequest request)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<GetAllRequest, PersonFilter>(); });
            var mapper = config.CreateMapper();
            var personfilter = mapper.Map<PersonFilter>(request);
            return personfilter;
        }

        public static IEnumerable<PersonModel> Map(this IEnumerable<Person> persons)
        {
            return new List<PersonModel>(persons.Select(s => new PersonModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                AddressId = s.AddressId,
                Address = new AddressModel
                {
                    Id = s.Address.Id,
                    AddressLine = s.Address.AddressLine,
                    City = s.Address.City
                }
            }));
        }
    }

}
