using AutoMapper;
using Hondrade_CodeFirstERD.DTOs;
using Hondrade_CodeFirstERD.Entities;
using Hondrade_CodeFirstERD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hondrade_CodeFirstERD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CitizenController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Citizen>>> GetAllCitizen()
        {
            var citizens = await _context.Citizens
                .Include(d => d.Applications)
                .Include(d => d.Contacts)
                .ToListAsync();

            var citizenDtos = _mapper.Map<List<CitizenDto>>(citizens);

            return Ok(citizenDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Citizen>>> GetCitizen(int id)
        {

            var citizens = await _context.Citizens
                .Include(d => d.Applications)
                .Include(d => d.Contacts)
                .FirstOrDefaultAsync(d => d.CitizenID == id);

            if (citizens is null)
            {
                return NotFound("Citizen not found.");
            }

            var citizensDto = _mapper.Map<CitizenDto>(citizens);
            return Ok(citizensDto);
        }

        [HttpPost]
        public async Task<ActionResult<CitizenDto>> AddCitizen(CitizenDto citizenDto)
        {
            var citizen = _mapper.Map<Citizen>(citizenDto);
            _context.Citizens.Add(citizen);
            await _context.SaveChangesAsync();

            var citizenResultDto = _mapper.Map<CitizenDto>(citizen);
            return CreatedAtAction(nameof(GetCitizen), new { id = citizen.CitizenID}, citizenResultDto);
        } 

        [HttpPut("{id}")]
        public async Task<ActionResult<List<CitizenDto>>> UpdateCitizen(int id, CitizenDto updateDto)
        {
            var updateData = await _context.Citizens
                .Include(d => d.Applications)
                .Include(d => d.Contacts)
                .FirstOrDefaultAsync(d => d.CitizenID == id);

            if(updateData is null)
            {
                return NotFound("Citizen not found");
            }

            _mapper.Map(updateDto, updateData);

            await _context.SaveChangesAsync();

            var citizenDtos = _mapper.Map<CitizenDto>(updateData);
            return Ok(citizenDtos);
         
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CitizenDto>>> DeleteCitizen(int id)
        {
            var delData = await _context.Citizens
                .Include(d => d.Applications)
                .Include(d => d.Contacts)
                .FirstOrDefaultAsync(d => d.CitizenID == id);

            if (delData is null)
            {
                return NotFound("Citizen not found");

            }

            _context.Citizens.Remove(delData);
            await _context.SaveChangesAsync();

            var citizens = await _context.Citizens
                .Include(d => d.Applications)
                .Include(d => d.Contacts)
                .ToListAsync();

            var citizenDtos = _mapper.Map<List<CitizenDto>>(citizens);
            return Ok(citizenDtos);
        }
    }
}
