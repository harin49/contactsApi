using contactsApi.Entities;

namespace contactsApi.Repository;

public interface IContactRepository
{
    Task<IReadOnlyList<Contact>> GetAll();
    Task<Contact> Get(Guid id);
    Task Add(Contact contactRequest);
    Task Save();
    void Delete(Contact contact);
}