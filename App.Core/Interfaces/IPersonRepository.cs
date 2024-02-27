using App.Core.Models.DTOs;
using Infrastructure;

namespace App.Core.Interfaces;

public interface IPersonRepository
{
    public Task<ICollection<PersonResponse>> GetPersons();

    public Task<PersonResponse> GetPersonById(long idPerson);

    public Task<bool> DeletePersonById(long idPerson);

    public Task<PersonRequest> CreatePerson(PersonRequest personRequest);

    public Task<PersonRequest> UpdatePerson(long idPerson, PersonRequest personRequest);
    
    public Task<bool> PersonExists(long idPerson);
    
    public Task<bool> PersonExists(string namePerson);
}