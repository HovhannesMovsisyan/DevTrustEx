using Microsoft.AspNetCore.Mvc;
using DevTrustTest.BLL.Helpers;
using DevTrustTest.BLL.Models;
using DevTrustTest.BLL.ServiceInterfaces;
using System.Threading.Tasks;

namespace DevTrustTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        public PersonController(IPersonService personService) => _personService = personService;

        [HttpPost]
        [Route("GetAll")]
        public Task<string> GetAll(GetAllRequest request)
        {
            string response = null;
            var personModels = _personService.GetAll(request);

            foreach (var personModel in personModels)
                response += CustomJsonConverter.Serialize(personModel);

            return Task.Run(() => { return response; });
        }

        [HttpPost]
        [Route("Save")]
        public Task<int> Save(string value)
        {
            var personModel = CustomJsonConverter.Deserialize<PersonModel>(value);
            PersonModel person = _personService.Add(personModel);
            return Task.Run(() => { return (int)person.Id; });
        }
    }
}
