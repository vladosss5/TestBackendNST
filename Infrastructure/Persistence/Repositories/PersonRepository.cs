using App.Core.Exceptions;
using App.Core.Interfaces;
using App.Core.Models.DTOs;
using AutoMapper;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using RecursiveDataAnnotationsValidation;
using System.ComponentModel.DataAnnotations;
using App.Core.Extensions;

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DBContext _dbContext;
    private readonly IMapper _mapper;

    
    public PersonRepository(DBContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);
    
    
    public async Task<ICollection<PersonResponse>> GetPersons()
    {
        return await _dbContext.Persons
            .Include(s => s.Skills)
            .Select(p => _mapper.Map<PersonResponse>(p))
            .ToListAsync();
    }

    public async Task<PersonResponse> GetPersonById(long idPerson)
    {
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        return _mapper.Map<PersonResponse>(await _dbContext.Persons.Include(s=>s.Skills)
            .SingleOrDefaultAsync(p => p.IdPerson == idPerson));
    }

    public async Task<bool> DeletePersonById(long idPerson)
    {
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        var person = await _dbContext.Persons.FindAsync(new object[] { idPerson });
        var skills = await _dbContext.Skills.Where(s => s.IdPerson == person.IdPerson).ToListAsync();
        
        _dbContext.Skills.RemoveRange(skills);
        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<PersonResponse> CreatePerson(PersonRequest personRequest)
    {
        var validator = new RecursiveDataAnnotationValidator();
        var results = new List<ValidationResult>();
        
        if (!validator.TryValidateObjectRecursive(personRequest, results))
            throw new ModelException(results);
        if (PersonExists(personRequest.Name).Result && 
            PersonExists(personRequest.DisplayName).Result)
            throw new AlreadyExistsException(nameof(Person), personRequest.Name);

        var person = _mapper.Map<Person>(personRequest);
        await _dbContext.AddAsync(person);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> UpdatePerson(long idPerson, PersonRequest personRequest)
    {
        var validator = new RecursiveDataAnnotationValidator();
        var results = new List<ValidationResult>();
        
        if (!validator.TryValidateObjectRecursive(personRequest, results))
            throw new ModelException(results);
        if (personRequest.Skills.Select(x => x.Name).HasDuplicates())
            throw new AlreadyExistsException(nameof(Skill), personRequest.Skills);
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        var person = await _dbContext.Persons.SingleOrDefaultAsync(p => p.IdPerson == idPerson);

        person.Name = personRequest.Name;
        person.DisplayName = personRequest.DisplayName;

        person.Skills = personRequest.Skills
            .Select(s => new Skill() { Name = s.Name, Level = s.Level })
            .ToList();
        
        _dbContext.UpdateRange(person);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<bool> PersonExists(long idPerson) =>
        await _dbContext.Persons.AnyAsync(p => p.IdPerson == idPerson);

    public async Task<bool> PersonExists(string namePerson) =>
        await _dbContext.Persons.AnyAsync(p => p.Name == namePerson);
}