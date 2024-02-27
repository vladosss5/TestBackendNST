using App.Core.Interfaces;
using App.Core.Models.DTOs;
using AutoMapper;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DBContext _dbContext;
    private readonly IMapper _mapper;

    
    public PersonRepository(DBContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);
    
    
    public async Task<ICollection<PersonResponse>> GetPersons()
    {
        return await _dbContext.Persons.Select(p => _mapper.Map<PersonResponse>(p)).ToListAsync();
    }

    public Task<PersonResponse> GetPersonById(long idPerson)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePersonById(long idPerson)
    {
        throw new NotImplementedException();
    }

    public Task<PersonRequest> CreatePerson(PersonRequest personRequest)
    {
        throw new NotImplementedException();
    }

    public Task<PersonRequest> UpdatePerson(long idPerson, PersonRequest personRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PersonExists(long idPerson)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PersonExists(string namePerson)
    {
        throw new NotImplementedException();
    }
}