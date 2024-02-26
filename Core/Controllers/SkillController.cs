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
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;


        public SkillController(ISkillRepository skillRepository, IMapper mapper) =>
            (_skillRepository, _mapper) = (skillRepository, mapper);


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SkillDto>))]
        public async Task<ActionResult<ICollection<SkillDto>>> GetAllSkills()
        {
            var skills = await _skillRepository.GetAllSkills();
            return _mapper.Map<List<SkillDto>>(skills);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Skill>))]
        public async Task<ActionResult<Skill>> CreateSkill(SkillDto skillDto)
        {
            return await _skillRepository.CreateSkill(_mapper.Map<Skill>(skillDto));
        }
    }
}
