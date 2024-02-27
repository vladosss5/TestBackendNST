using App.Core.Interfaces;
using App.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<IPersonService> _logger;

    
    public PersonService(IPersonRepository personRepository, ILogger<IPersonService> logger) =>
        (_personRepository, _logger) = (personRepository, logger);


    public async Task<ActionResult> GetPersons() =>
        new OkObjectResult(await _personRepository.GetPersons());

    public Task<ActionResult> GetPersonById(long idPerson)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult> DeletePersonById(long idPerson)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult> CreatePerson(PersonRequest personRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult> UpdatePerson(long idPerson, PersonRequest personRequest)
    {
        throw new NotImplementedException();
    }
}