using Core.Data;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class PersonRepository : IPersonRepository
{
    protected readonly DBContext _dbContext;


    public PersonRepository(DBContext dbContext) =>
        _dbContext = dbContext;
    
    
    public async Task<ICollection<Person>> GetAllPersons()
    {
        return await _dbContext.Persons.ToListAsync();
    }

    public async Task<Person> GetPersonById(long idPerson)
    {
        return await _dbContext.Persons.FindAsync(new object[] {idPerson});
    }

    public async Task<Person> CreatePerson(Person person)
    {
        if (PersonExists(person.Name).Result &&
            PersonExists(person.DisplayName).Result)
            throw new AlreadyExistsException(nameof(Person), person.Name);

        Person newPerson = new Person()
        {
            Name = person.Name,
            DisplayName = person.DisplayName
        };

        await _dbContext.AddAsync(newPerson);
        await _dbContext.SaveChangesAsync();

        return newPerson;
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        _dbContext.Persons.UpdateRange(person);
        await _dbContext.SaveChangesAsync();
        
        return person;
    }

    public async Task<bool> DeletePerson(long idPerson)
    {
        if (!PersonExists(idPerson).Result)
            throw new NotFoundException(nameof(Person), idPerson);

        var deletingPerson = await _dbContext.Persons.FindAsync(new object[] { idPerson });
        
        _dbContext.Persons.RemoveRange(deletingPerson);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> PersonExists(string name)
    {
        return await _dbContext.Persons.AnyAsync(p => p.Name == name);
    }
    
    public async Task<bool> PersonExists(long idPerson)
    {
        return await _dbContext.Persons.AnyAsync(p => p.IdPerson == idPerson);
    }
}