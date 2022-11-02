using contactsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace contactsApi.Data;

public class ContactsApiDbContext: DbContext
{
    public ContactsApiDbContext(DbContextOptions options
    ) : base(options)
    {
        
    }

    public DbSet<Contact> Contacts { get; set; }
}