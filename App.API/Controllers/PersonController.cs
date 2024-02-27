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
    }
}
