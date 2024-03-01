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
        public async Task<IActionResult> GetAllPersons()
        {
            var personResponse = await _personService.GetPersons();
            
            if (personResponse == null)
                return new EmptyResult();
                
            return Ok(personResponse);
        }

        [HttpGet("Id")]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<IActionResult> GetPersonById(int idPerson)
        {
            var personResponse = await _personService.GetPersonById(idPerson);
            if (personResponse == null)
                return BadRequest();

            return Ok(personResponse);
        }

        [HttpPut("Id")]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<IActionResult> UpdatePerson(int idPerson, PersonRequest personRequest)
        {
            var personResponse = await _personService.UpdatePerson(idPerson, personRequest);
            return Ok(personResponse);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<IActionResult> CreatePerson(PersonRequest personRequest)
        {
            var personResponse = await _personService.CreatePerson(personRequest);
            return Ok(personResponse);
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> DeletePerson(long idPerson)
        {
            var deleting = await _personService.DeletePersonById(idPerson);
            if (deleting == null)
                return BadRequest();

            return Ok();
        }
    }
}
