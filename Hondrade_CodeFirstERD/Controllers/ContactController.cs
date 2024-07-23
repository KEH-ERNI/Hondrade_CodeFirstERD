using AutoMapper;
using Hondrade_CodeFirstERD.DTOs;
using Hondrade_CodeFirstERD.Entities;
using Hondrade_CodeFirstERD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hondrade_CodeFirstERD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAllContact()
        {
            var contacts = await _context.Contacts
                .Include(d => d.Citizen)
                .Include(d => d.Employee)
                .ToListAsync();

            var contactDtos = _mapper.Map<List<ContactDto>>(contacts);
            return Ok(contactDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Contact>>> GetContact(int id)
        {
            var contact = await _context.Contacts
                .Include(d => d.Citizen)
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(d => d.ContactID == id);

            if(contact is null)
            {
                return NotFound("Contact not found");
            }

            var contactDto = _mapper.Map<ContactDto>(contact);
            return Ok(contactDto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> AddContact(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);

            var employee = await _context.Employees.FindAsync(contactDto.EmpID);
            var citizen = await _context.Citizens.FindAsync(contactDto.CitizenID);

            if (employee == null || citizen == null)
            {
                return BadRequest("Invalid Employee/Citizen ID");
            }

            contact.Employee = employee;
            contact.Citizen = citizen;

            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync();

            var contactResultDto = _mapper.Map<ContactDto>(contact);
            return CreatedAtAction(nameof(GetContact), new {id = contact.ContactID}, contactResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<ContactDto>>> UpdateContact(int id, ContactDto contactDto)
        {
            var contact = await _context.Contacts
                .Include(d => d.Citizen)
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(d => d.ContactID == id);

            if(contact == null)
            {
                return NotFound();
            }

            _mapper.Map(contactDto, contact);
            _context.Entry(contact.Citizen).State = EntityState.Unchanged;
            _context.Entry(contact.Employee).State = EntityState.Unchanged;

            try
            {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException)
            {
                if(!ContactExists(id))
                {
                    return NotFound("Contact not found");
                }
                else
                {
                    throw;
                }
            }

            var updatedContactDto = _mapper.Map<ContactDto>(contact);
            return Ok(updatedContactDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> DeleteContact(int id)
        {
            var delData = await _context.Contacts
                .Include(d => d.Citizen)
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(d => d.ContactID == id);

            if(delData == null)
            {
                return NotFound("Contact not found");
            }

            _context.Contacts.Remove(delData);
            await _context.SaveChangesAsync();

            var remainingContacts = await _context.Contacts
                .Include(d => d.Citizen)
                .Include(d => d.Employee)
                .ToListAsync();

            var remainingContactDtos = _mapper.Map<List<ContactDto>>(remainingContacts);
            return Ok(remainingContactDtos);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(d => d.ContactID == id);
        }
    }
}
