using DevTrustTest.BLL.Helpers;
using DevTrustTest.BLL.Models;
using DevTrustTest.BLL.ServiceInterfaces;
using DevTrustTest.DAL.UnitOfWork;
using DevTrustTest.DevTrustTest.DAL.Models;
using System.Collections.Generic;

namespace DevTrustTest.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public PersonModel Add(PersonModel personModel)
        {
            var person = _unitOfWork.Person.GetById(personModel.Id);
            Person newPerson;
            if (person == null)
            {
                newPerson = _unitOfWork.Person.Add(personModel.Map());
                _unitOfWork.Save();
            }
            else
            {
                newPerson = _unitOfWork.Person.Update(personModel.Map());
            }
            return newPerson.Map();
        }

        public IEnumerable<PersonModel> GetAll(GetAllRequest request) => _unitOfWork.Person.GetAll(request.Map()).Map();

    }
}
