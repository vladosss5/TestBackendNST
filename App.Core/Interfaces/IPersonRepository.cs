using App.Core.Models.DTOs;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Interfaces;

public interface IPersonRepository
{
    public Task<ICollection<Person>> GetPersons();

    public Task<Person> GetPersonById(long idPerson);

    public Task<bool> DeletePersonById(long idPerson);

    public Task<Person> CreatePerson(Person person);

    public Task<Person> UpdatePerson(long idPerson, Person person);
    
    public Task<bool> PersonExists(long idPerson);
    
    public Task<bool> PersonExists(string namePerson);
}