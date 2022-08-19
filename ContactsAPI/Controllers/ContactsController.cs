using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ContactsAPIDbContext DbContext { get; }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id) 
            {
            var contacts = await dbContext.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            return Ok(contacts);
        }



       [HttpPost]
        public async Task <IActionResult> AddContacts(AddContactRequest addContactRequest)
        {
            var contacts = new Contacts()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
            };

            await dbContext.Contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();

            return Ok(contacts);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateContactsRequest updateContactsRequest)
        {
        var contacts = await dbContext.Contacts.FindAsync(id);

            if (contacts != null)
            { 
            contacts.FullName = updateContactsRequest.FullName;
            contacts.Phone = updateContactsRequest.Phone;
            contacts.Email = updateContactsRequest.Email;   
            contacts.Address = updateContactsRequest.Address;

                await dbContext.SaveChangesAsync();
                return Ok(contacts);
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
        if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    }
}
