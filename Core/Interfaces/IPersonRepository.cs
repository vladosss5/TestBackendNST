using Core.Models;

namespace Core.Interfaces;

public interface IPersonRepository
{
    public Task<ICollection<Person>> GetAllPerson();

    public Task<Person> GetPersonById(long idPerson);

    public Task<Person> CreatePerson(Person person);

    public Task<Person> UpdatePerson(Person person);

    public Task<bool> DeletePerson(long idPerson);
    public Task<bool> PersonExists(long idPerson);
    public Task<bool> PersonExists(string namePerson);
}