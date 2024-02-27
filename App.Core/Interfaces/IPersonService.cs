using App.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Interfaces;

public interface IPersonService
{
    public Task<ActionResult> GetPersons();

    public Task<ActionResult> GetPersonById(long idPerson);
    
    public Task<ActionResult> DeletePersonById(long idPerson);
    
    public Task<ActionResult> CreatePerson(PersonRequest personRequest);
    
    public Task<ActionResult> UpdatePerson(long idPerson, PersonRequest personRequest);
}