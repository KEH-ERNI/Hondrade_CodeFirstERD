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
    public class ApplicationController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ApplicationController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationDto>>> GetAllApplication()
        {
            var application = await _context.Applications
                .Include(d => d.Service)
                .Include(d => d.Citizen)
                .ToListAsync();

            var applicationDtos = _mapper.Map<List<ApplicationDto>>(application);

            return Ok(applicationDtos);
        }

       [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> GetApplication(int id)
        {
            var application = await _context.Applications
                .Include (d => d.Service)
                .Include (d => d.Citizen)
                .FirstOrDefaultAsync(d => d.ApplicationID == id);

            if(application == null)
            {
                return NotFound();
            }

            var applicationDto = _mapper.Map<ApplicationDto>(application);
            return Ok(applicationDto);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationDto>> AddApplication(ApplicationDto applicationDto)
        {
            var application = _mapper.Map<Application>(applicationDto);

            var service = await _context.Services.FindAsync(applicationDto.ServiceID);
            var citizen = await _context.Citizens.FindAsync(applicationDto.CitizenID);
            
            if (service == null || citizen == null)
            {
                return BadRequest("Invalid Service/ Citizen ID");
            }

            application.Service = service;
            application.Citizen = citizen;

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            var applicationResultDto = _mapper.Map<ApplicationDto>(application);
            return CreatedAtAction(nameof(GetApplication), new {id = application.ApplicationID}, applicationResultDto);
        }


       
       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateApplication(int id, ApplicationDto applicationDto)
       {
           var application = await _context.Applications
               .Include(d => d.Service)
               .Include(d => d.Citizen)
               .FirstOrDefaultAsync(d => d.ApplicationID == id);

           if(application == null)
           {
               return NotFound();
           }

           _mapper.Map(applicationDto, application);
           _context.Entry(application.Service).State = EntityState.Unchanged;
           _context.Entry(application.Citizen).State = EntityState.Unchanged;

           try
           {
               await _context.SaveChangesAsync();
           }
           catch (DbUpdateConcurrencyException) 
           { 
               if(!ApplicationExists(id))
               {
                   return NotFound();
               } else
               {
                   throw;
               }
           }

           var updatedApplicationDto = _mapper.Map<ApplicationDto>(application);
           return Ok(updatedApplicationDto);
       }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null) {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            var remainingApplication = await _context.Applications
                .Include(s => s.Service)
                .Include(s => s.Citizen)
                .ToListAsync();

            var remainingApplicationDtos = _mapper.Map<List<ApplicationDto>>(remainingApplication);

            return Ok(remainingApplicationDtos);
        }
       
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationID == id);
        }

    }
}
