using App.Core.Exceptions;
using App.Core.Interfaces;
using App.Core.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<IPersonService> _logger;
    private readonly IMapper _mapper;

    
    public PersonService(IPersonRepository personRepository, 
        ILogger<IPersonService> logger, IMapper mapper) =>
        (_personRepository, _logger, _mapper) = 
        (personRepository, logger, mapper);


    public async Task<ICollection<PersonResponse>> GetPersons() =>  
         _personRepository.GetPersons().Result.Select(p => _mapper.Map<PersonResponse>(p)).ToList();

    public async Task<PersonResponse> GetPersonById(long idPerson)
    {
        try
        {
            return _mapper.Map<PersonResponse>(await _personRepository.GetPersonById(idPerson));
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e.Message);
            throw new NotFoundException(nameof(Person), idPerson);
        }
    }

    public async Task<bool> DeletePersonById(long idPerson)
    {
        try
        {
            return await _personRepository.DeletePersonById(idPerson);;
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e.Message);
            throw new NotFoundException(nameof(Person), idPerson);
        }
    }

    public async Task<PersonResponse> CreatePerson(PersonRequest personRequest)
    {
        try
        {
            return _mapper.Map<PersonResponse>(
                await _personRepository.CreatePerson(_mapper.Map<Person>(personRequest)));
        }
        catch (ModelException e)
        {
            var messages = e.Errors.Select(x => x.ErrorMessage).ToList();
            messages.ForEach(x => _logger.LogError(x));
            throw new AlreadyExistsException(nameof(Person), personRequest); //Исправить
        }
        catch (AlreadyExistsException e)
        {
            _logger.LogError(e.Message);
            throw new AlreadyExistsException(nameof(Person), personRequest.Name);
        }
    }

    public async Task<PersonResponse> UpdatePerson(long idPerson, PersonRequest personRequest)
    {
        try
        {
            return _mapper.Map<PersonResponse>(
                await _personRepository.UpdatePerson(idPerson, _mapper.Map<Person>(personRequest)));
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e.Message);
            throw new NotFoundException(nameof(Person), idPerson);
        }
        catch (ModelException ex)
        {
            var messages = ex.Errors.Select(x => x.ErrorMessage).ToList();
            messages.ForEach(x => _logger.LogError(x));
            throw new NotFoundException(nameof(Person), idPerson); //Исправить
        }
    }
}