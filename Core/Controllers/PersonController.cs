using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Models.DtoEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        
        public PersonController(IPersonRepository personRepository, IMapper mapper) =>
            (_personRepository, _mapper) = (personRepository, mapper);
        

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonDto>))]
        public async Task<ActionResult<ICollection<PersonDto>>> GetAllPersons()
        {
            var persons = await _personRepository.GetAllPersons();
            return _mapper.Map<List<PersonDto>>(persons);
        }

        [HttpGet("IdPerson")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<ActionResult<PersonDto>> GetPersonById(int idPerson)
        {
            if (!_personRepository.PersonExists(idPerson).Result)
                return BadRequest(404);
            
            var person = await _personRepository.GetPersonById(idPerson);
            return _mapper.Map<PersonDto>(person);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<ActionResult<Person>> CreatePerson(PersonDto personDto)
        {
            return await _personRepository.CreatePerson(_mapper.Map<Person>(personDto));
        }

        [HttpPut("IdPerson")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        public async Task<ActionResult<PersonDto>> UpdatePerson(int idPerson, PersonDto personDto)
        {
            if (!_personRepository.PersonExists(idPerson).Result)
                return BadRequest(404);

            var updatingUser = await _personRepository.UpdatePerson(_mapper.Map<Person>(personDto));
            
            return _mapper.Map<PersonDto>(updatingUser);
        }

        [HttpDelete("IdPerson")]
        public async Task<IActionResult> DeletePerson(int idPerson)
        {
            if (!_personRepository.PersonExists(idPerson).Result)
                return BadRequest(404);

            return Ok(await _personRepository.DeletePerson(idPerson));
        }
    }
}
