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
using NuGet.Packaging;

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

    public async Task<Person> CreatePerson(Person personRequest)
    {
        var validator = new RecursiveDataAnnotationValidator();
        var results = new List<ValidationResult>();
        
        if (!validator.TryValidateObjectRecursive(personRequest, results))
            throw new ModelException(results);
        if (PersonExists(personRequest.Name).Result && 
            PersonExists(personRequest.DisplayName).Result)
            throw new AlreadyExistsException(nameof(Person), personRequest.Name);
        
        await _dbContext.AddAsync(personRequest);
        await _dbContext.SaveChangesAsync();

        return personRequest;
    }

    public async Task<Person> UpdatePerson(long idPerson, Person personRequest)
    {
        var validator = new RecursiveDataAnnotationValidator();
        var results = new List<ValidationResult>();
        
        if (!validator.TryValidateObjectRecursive(personRequest, results))
            throw new ModelException(results);
        if (personRequest.Skills.Select(x => x.Name).HasDuplicates())
            throw new AlreadyExistsException(nameof(Skill), personRequest.Skills);
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        var updatingPerson = await _dbContext.Persons
            .FirstOrDefaultAsync(p => p.Id == idPerson);

        var updatingSkills = _dbContext.Skills.Where(s => s.IdPerson == idPerson);

        updatingPerson.Name = personRequest.Name;
        updatingPerson.DisplayName = personRequest.DisplayName;
        updatingPerson.Skills.Clear();
        
        foreach (var oldSkill in updatingSkills)
        {
            foreach (var newSkill in personRequest.Skills)
            {
                if (oldSkill.Name == newSkill.Name)
                {
                    oldSkill.Level = newSkill.Level;
                    updatingPerson.Skills.Add(oldSkill);
                }
            }
        }
        
        _dbContext.Update(updatingPerson);
        await _dbContext.SaveChangesAsync();

        return updatingPerson;
    }

    public async Task<bool> PersonExists(long idPerson) =>
        await _dbContext.Persons.AnyAsync(p => p.Id == idPerson);

    public async Task<bool> PersonExists(string namePerson) =>
        await _dbContext.Persons.AnyAsync(p => p.Name == namePerson);
}