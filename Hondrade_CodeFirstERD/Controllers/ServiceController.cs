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
    public class ServiceController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ServiceController(DataContext context, IMapper mapper) {

            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>>> GetAllService()
        {
            var services = await _context.Services
                .Include(d => d.Department)
                .Include(d => d.Applications)
                .ToListAsync();

            var serviceDtos = _mapper.Map<List<ServiceDto>>(services);

            return Ok(serviceDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _context.Services
                .Include(d => d.Department)
                .Include(d => d.Applications)
                .FirstOrDefaultAsync(d => d.ServiceID == id);

            if (service == null)
            {
                return NotFound();
            }

            var servicesDto = _mapper.Map<ServiceDto>(service);
            return Ok(servicesDto);
        }

        /*  [HttpPost]
          public async Task<ActionResult<ServiceDto>> AddService(ServiceDto serviceDto)
          {
              var service = _mapper.Map<Service>(serviceDto);
              _context.Entry(service.Department).State = EntityState.Unchanged;

              _context.Services.Add(service);
              await _context.SaveChangesAsync();

              var serviceResultDto = _mapper.Map<ServiceDto>(service);
              return CreatedAtAction(nameof(GetService), new { id = service.ServiceID }, serviceResultDto);
          } */

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> AddService(ServiceDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);

           
            var department = await _context.Departments.FindAsync(serviceDto.DepID);
            if (department == null)
            {
                return BadRequest("Invalid DepID");
            }

            service.Department = department;

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            var serviceResultDto = _mapper.Map<ServiceDto>(service);
            return CreatedAtAction(nameof(GetService), new { id = service.ServiceID }, serviceResultDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, ServiceDto serviceDto)
        {
            var service = await _context.Services
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.ServiceID == id);

            if (service == null)
            {
                return NotFound();
            }

            _mapper.Map(serviceDto, service);
            _context.Entry(service.Department).State = EntityState.Unchanged;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var updatedServiceDto = _mapper.Map<ServiceDto>(service);
            return Ok(updatedServiceDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            var remainingServices = await _context.Services
                .Include(s => s.Department) 
                .ToListAsync();

            var remainingServiceDtos = _mapper.Map<List<ServiceDto>>(remainingServices);

            return Ok(remainingServiceDtos);
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceID == id);
        }

    }
    
}
