using contactsApi.Entities;
using contactsApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace contactsApi.Services;

public interface IContactsService
{
    public Task<IReadOnlyList<Contact>> GetAllContacts();
    public Task<Contact> GetContactsById(Guid id);
    public Task<Contact> AddContact(AddContactRequest addContactRequest);
    public Task<Contact> UpdateContact(Guid id, UpdateContactRequest updateContactRequest);
    public Task<Contact> UpdateContactById(Guid id, JsonPatchDocument updateContactRequest);
    public Task<Guid?> DeleteContactById(Guid id);
}