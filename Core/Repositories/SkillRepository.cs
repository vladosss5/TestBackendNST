using Core.Data;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class SkillRepository: ISkillRepository
{
    protected readonly DBContext _dbContext;

    
    public SkillRepository(DBContext dbContext) =>
        _dbContext = dbContext;
    
    
    public async Task<ICollection<Skill>> GetAllSkills()
    {
        return await _dbContext.Skills.ToListAsync();
    }

    public async Task<Skill> GetSkillById(int idSkill)
    {
        var skill = await _dbContext.Skills.FindAsync(new object[] {idSkill});

        if (skill == null)
            throw new NotFoundException(nameof(Skill), idSkill);

        return skill;
    }

    public async Task<Skill> CreateSkill(Skill skill)
    {
        if (SkillExists(skill.Name).Result)
            throw new AlreadyExistsException(nameof(Skill), skill.Name);

        var newSkill = new Skill()
        {
            Name = skill.Name
        };

        await _dbContext.Skills.AddAsync(newSkill);
        await _dbContext.SaveChangesAsync();

        return newSkill;
    }

    public async Task<Skill> UpdateSkill(Skill skill)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteSkill(int idSkill)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SkillExists(int idSkill)
    {
        return await _dbContext.Skills.AnyAsync(s => s.IdSkill == idSkill);
    }

    public async Task<bool> SkillExists(string nameSkill)
    {
        return await _dbContext.Skills.AnyAsync(s => s.Name == nameSkill);
    }
}