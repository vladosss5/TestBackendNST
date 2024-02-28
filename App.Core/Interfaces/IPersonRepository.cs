using App.Core.Models.DTOs;
using Infrastructure;

namespace App.Core.Interfaces;

public interface IPersonRepository
{
    public Task<ICollection<PersonResponse>> GetPersons();

    public Task<PersonResponse> GetPersonById(long idPerson);

    public Task<bool> DeletePersonById(long idPerson);

    public Task<PersonResponse> CreatePerson(PersonRequest personRequest);

    public Task<PersonResponse> UpdatePerson(long idPerson, PersonRequest personRequest);
    
    public Task<bool> PersonExists(long idPerson);
    
    public Task<bool> PersonExists(string namePerson);
}