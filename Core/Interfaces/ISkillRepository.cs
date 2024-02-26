using Core.Models;

namespace Core.Interfaces;

public interface ISkillRepository
{
    public Task<ICollection<Skill>> GetAllSkills();

    public Task<Skill> GetSkillById(int idSkill);

    public Task<Skill> CreateSkill(Skill skill);

    public Task<Skill> UpdateSkill(Skill skill);

    public Task<bool> DeleteSkill(int idSkill);
    
    public Task<bool> SkillExists(int idSkill);
    
    public Task<bool> SkillExists(string nameSkill);
}