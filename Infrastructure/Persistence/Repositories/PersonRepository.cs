using App.Core.Exceptions;
using App.Core.Interfaces;
using App.Core.Models.DTOs;
using AutoMapper;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using RecursiveDataAnnotationsValidation;
using System.ComponentModel.DataAnnotations;
using App.Core.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DBContext _dbContext;
    
    
    public PersonRepository(DBContext dbContext, IMapper mapper) =>
        _dbContext = dbContext;
    
    
    public async Task<ICollection<Person>> GetPersons() => 
         await _dbContext.Persons.Include(s => s.Skills).ToListAsync();


    public async Task<Person> GetPersonById(long idPerson)
    {
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        return await _dbContext.Persons.Include(s=>s.Skills)
            .SingleOrDefaultAsync(p => p.Id == idPerson);
    }

    public async Task<bool> DeletePersonById(long idPerson)
    {
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        var person = await _dbContext.Persons.FindAsync(new object[] { idPerson });
        var skills = await _dbContext.Skills.Where(s => s.IdPerson == person.Id).ToListAsync();
        
        _dbContext.Skills.RemoveRange(skills);
        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Person> CreatePerson(Person person)
    {
        var validator = new RecursiveDataAnnotationValidator();
        var results = new List<ValidationResult>();
        
        if (!validator.TryValidateObjectRecursive(person, results))
            throw new ModelException(results);
        if (PersonExists(person.Name).Result && 
            PersonExists(person.DisplayName).Result)
            throw new AlreadyExistsException(nameof(Person), person.Name);
        
        await _dbContext.AddAsync(person);
        await _dbContext.SaveChangesAsync();

        return person;
    }

    public async Task<Person> UpdatePerson(long idPerson, Person person)
    {
        var validator = new RecursiveDataAnnotationValidator();
        var results = new List<ValidationResult>();
        
        if (!validator.TryValidateObjectRecursive(person, results))
            throw new ModelException(results);
        if (person.Skills.Select(x => x.Name).HasDuplicates())
            throw new AlreadyExistsException(nameof(Skill), person.Skills);
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        // var person = await _dbContext.Persons.SingleOrDefaultAsync(p => p.Id == idPerson);
        var skills = person.Skills.Select(s => new Skill()
        {
            Name = s.Name,
            Level = s.Level,
            IdPerson = person.Id
        }).ToList();

        person.Name = person.Name;
        person.DisplayName = person.DisplayName;
        person.Skills = skills;
        
        _dbContext.UpdateRange(skills);
        _dbContext.UpdateRange(person);
        await _dbContext.SaveChangesAsync();

        return person;
    }

    public async Task<bool> PersonExists(long idPerson) =>
        await _dbContext.Persons.AnyAsync(p => p.Id == idPerson);

    public async Task<bool> PersonExists(string namePerson) =>
        await _dbContext.Persons.AnyAsync(p => p.Name == namePerson);
}