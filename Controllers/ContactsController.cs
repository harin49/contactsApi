using contactsApi.Models;
using contactsApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace contactsApi.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController : Controller
{
    private readonly IContactsService _contactsService;

    public ContactsController(IContactsService contactsService)
    {
        _contactsService = contactsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contact = await _contactsService.GetAllContacts();
        return Ok(contact);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetContact([FromRoute] Guid id)
    {
        var contact = await _contactsService.GetContactsById(id);

        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact(AddContactRequest contactRequest)
    {
        var contactToBeAdded = await _contactsService.AddContact(contactRequest);
        return Ok(contactToBeAdded);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id,  UpdateContactRequest updateContactRequest)
    {
        var contact = await _contactsService.UpdateContact(id, updateContactRequest);

        if (contact == null)
        {
            return NotFound();
        }
        
        return Ok(contact);
    }
    
    [HttpPatch]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateContactPatch([FromRoute] Guid id, [FromBody] JsonPatchDocument updateContactRequest)
    {
        var contact = await _contactsService.UpdateContactById(id, updateContactRequest);

        if (contact == null)
        {
            return NotFound();
        }
        
        return Ok(contact);
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
    {
        var contactId = await _contactsService.DeleteContactById(id);

        if (contactId == null)
        {
            return NotFound();
        }
        
        return Ok(contactId);
    }
}