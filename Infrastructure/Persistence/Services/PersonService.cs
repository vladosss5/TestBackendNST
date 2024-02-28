using App.Core.Exceptions;
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

    public async Task<ActionResult> GetPersonById(long idPerson)
    {
        try
        {
            var person = await _personRepository.GetPersonById(idPerson);
            return new OkObjectResult(person);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new NotFoundResult();
        }
    }

    public Task<ActionResult> DeletePersonById(long idPerson)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult> CreatePerson(PersonRequest personRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult> UpdatePerson(long idPerson, PersonRequest personRequest)
    {
        try
        {
            var person = await _personRepository.UpdatePerson(idPerson, personRequest);
            return new OkObjectResult(person);
        }
        catch (AlreadyExistsException e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e.Message);
            return new NotFoundResult();
        }
        catch (ModelException ex)
        {
            var messages = ex.Errors.Select(x => x.ErrorMessage).ToList();
            messages.ForEach(x => _logger.LogError(x));
            return new BadRequestObjectResult(messages);
        }
    }
}