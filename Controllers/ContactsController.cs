using contactsApi.Data;
using contactsApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contactsApi.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController : Controller
{
    private readonly ContactsApiDbContext _dbContext;

    public ContactsController(ContactsApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        return Ok(await _dbContext.Contacts.ToListAsync());
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetContact([FromRoute] Guid id)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact(AddContactRequest contactRequest)
    {
        var contactToBeAdded = new Contact
        {
            Id = new Guid(),
            FullName = contactRequest.FullName,
            PhoneNumber = contactRequest.PhoneNumber,
            EmailAddress = contactRequest.EmailAddress,
            Address = contactRequest.Address
        };

        await _dbContext.Contacts.AddAsync(contactToBeAdded);
        await _dbContext.SaveChangesAsync();
        return Ok(contactToBeAdded);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id,  UpdateContactRequest updateContactRequest)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        contact.FullName = updateContactRequest.FullName;
        contact.EmailAddress = updateContactRequest.EmailAddress;
        contact.PhoneNumber = updateContactRequest.PhoneNumber;
        contact.Address = updateContactRequest.Address;

        await _dbContext.SaveChangesAsync();
        return Ok(contact);
    }
    
    [HttpPatch]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateContactPatch([FromRoute] Guid id, [FromBody] JsonPatchDocument updateContactRequest)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }
    
        updateContactRequest.ApplyTo(contact);
        await _dbContext.SaveChangesAsync();
        return Ok(contact);
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
    {
        var contact = await _dbContext.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        _dbContext.Remove(contact);
        await _dbContext.SaveChangesAsync();
        return Ok(contact);
    }
}