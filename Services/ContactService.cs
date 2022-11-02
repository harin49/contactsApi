using contactsApi.Entities;
using contactsApi.Models;
using contactsApi.Repository;
using Microsoft.AspNetCore.JsonPatch;

namespace contactsApi.Services;

public class ContactService: IContactsService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IReadOnlyList<Contact>> GetAllContacts()
    {
        return await _contactRepository.GetAll();
    }

    public async Task<Contact> GetContactsById(Guid id)
    {
        return await _contactRepository.Get(id);
    }

    public async Task<Contact> AddContact(AddContactRequest contactRequest)
    {
        var contactToBeAdded = new Contact
        {
            Id = new Guid(),
            FullName = contactRequest.FullName,
            PhoneNumber = contactRequest.PhoneNumber,
            EmailAddress = contactRequest.EmailAddress,
            Address = contactRequest.Address
        };

        await _contactRepository.Add(contactToBeAdded);
        await _contactRepository.Save();
        return contactToBeAdded;
    }
    
    public async Task<Contact> UpdateContact(Guid id, UpdateContactRequest updateContactRequest )
    {
        var contact = await _contactRepository.Get(id);
        if (contact == null)
        {
            return null;
        }
        
        contact.FullName = updateContactRequest.FullName;
        contact.EmailAddress = updateContactRequest.EmailAddress;
        contact.PhoneNumber = updateContactRequest.PhoneNumber;
        contact.Address = updateContactRequest.Address;
        
        await _contactRepository.Save();
        return contact;
    }

    public async Task<Contact> UpdateContactById(Guid id, JsonPatchDocument updateContactRequest)
    {
        var contact = await _contactRepository.Get(id);
        if (contact == null)
        {
            return null;
        }
        
        updateContactRequest.ApplyTo(contact);
        await _contactRepository.Save();
        return contact;
    }

    public async Task<Guid?> DeleteContactById(Guid id)
    {
        var contact = await _contactRepository.Get(id);

        if (contact == null)
        {
            return null;
        }

        _contactRepository.Delete(contact);
        await _contactRepository.Save();
        return id;
    }
}