using App.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Interfaces;

public interface IPersonService
{
    public Task<ICollection<PersonResponse>> GetPersons();

    public Task<PersonResponse> GetPersonById(long idPerson);
    
    public Task<bool> DeletePersonById(long idPerson);
    
    public Task<PersonResponse> CreatePerson(PersonRequest personRequest);
    
    public Task<PersonResponse> UpdatePerson(long idPerson, PersonRequest personRequest);
}