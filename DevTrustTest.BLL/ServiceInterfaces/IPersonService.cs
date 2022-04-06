using DevTrustTest.BLL.Models;
using System.Collections.Generic;

namespace DevTrustTest.BLL.ServiceInterfaces
{
    public interface IPersonService
    {
        PersonModel Add(PersonModel personModel);
        IEnumerable<PersonModel> GetAll(GetAllRequest request);
    }
}
