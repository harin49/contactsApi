using contactsApi.Data;
using contactsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace contactsApi.Repository;

public class ContactRepository: IContactRepository
{
    private readonly ContactsApiDbContext _dbContext;

    public ContactRepository(ContactsApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Contact>> GetAll()
    {
        return await _dbContext.Contacts.ToListAsync();
    }

    public async Task<Contact> Get(Guid id)
    {
        return await _dbContext.Contacts.FindAsync(id);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task Add(Contact contactRequest)
    {
        await _dbContext.Contacts.AddAsync(contactRequest);
    }

    public void Delete(Contact contactToBeDeleted)
    {
         _dbContext.Remove(contactToBeDeleted);
    }
}