using App.Core.Exceptions;
using App.Core.Interfaces;
using App.Core.Models.DTOs;
using Infrastructure;
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
            try
            {
                var personResponse = await _personService.GetPersons();
                return Ok(personResponse);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpGet("Id")]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<IActionResult> GetPersonById(int idPerson)
        {
            try
            {
                var personResponse = await _personService.GetPersonById(idPerson);
                return Ok(personResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("Id")]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<IActionResult> UpdatePerson(int idPerson, PersonRequest personRequest)
        {
            try
            {
                var personResponse = await _personService.UpdatePerson(idPerson, personRequest);
                return Ok(personResponse);
            }
            catch (Exception e)
            {
                if (e is NotFoundException)
                {
                    return NotFound(e.Message);
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonResponse))]
        public async Task<IActionResult> CreatePerson(PersonRequest personRequest)
        {
            try
            {
                var personResponse = await _personService.CreatePerson(personRequest);
                return Ok(personResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> DeletePerson(long idPerson)
        {
            try
            {
                var deleting = await _personService.DeletePersonById(idPerson);
                return Ok(deleting);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
