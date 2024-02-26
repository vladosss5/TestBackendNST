using AutoMapper;
using Core.Interfaces;
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
            var persons = await _personRepository.GetAllPerson();
            return _mapper.Map<List<PersonDto>>(persons);
        }
    }
}
