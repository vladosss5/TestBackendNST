using App.Core.Interfaces;
using App.Core.Models.DTOs;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        
        public PersonController(IPersonService personService) => 
            _personService = personService;
        
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonResponse>))]
        public async Task<ActionResult<ICollection<PersonResponse>>> GetAllPersons()
        {
            return await _personService.GetPersons();
        }

        [HttpGet("IdPerson")]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<ActionResult<PersonResponse>> GetPersonById(int idPerson)
        {
            return await _personService.GetPersonById(idPerson);
        }

        [HttpPut("IdPerson")]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<ActionResult<PersonRequest>> UpdatePerson(int idPerson, PersonRequest personRequest)
        {
            return await _personService.UpdatePerson(idPerson, personRequest);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<ActionResult<PersonResponse>> CreatePerson(PersonRequest personRequest)
        {
            return await _personService.CreatePerson(personRequest);
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<ActionResult<bool>> DeletePerson(long idPerson)
        {
            return await _personService.DeletePersonById(idPerson);
        }
    }
}
