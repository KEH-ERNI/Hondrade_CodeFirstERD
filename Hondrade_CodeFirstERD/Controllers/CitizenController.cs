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
        private readonly IWebHostEnvironment _environment;
        public CitizenController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<List<Citizen>>> GetAllCitizen()
        {
            var citizens = await _context.Citizens.ToListAsync();

            return Ok(citizens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Citizen>>> GetCitizen(int id)
        {

            var citizens = await _context.Citizens.FindAsync(id);
            if (citizens is null)
            {
                return NotFound("Citizen not found.");
            }

            return Ok(citizens);
        }

        [HttpPost]
        public async Task<ActionResult<List<Citizen>>> AddCitizen(CitizenDto citizen)
        {

            // _context.Citizens.Add(citizen);
            // await _context.SaveChangesAsync();

            var newCitizen = new Citizen
            {
                FName = citizen.FName,
                MName = citizen.MName,
                LName = citizen.LName,
                Address = citizen.Address,
                Bday = citizen.Bday,
                Phone = citizen.Phone,
                Email = citizen.Email,
                UName = citizen.UName,
                Password = citizen.Password,
                ImgName = citizen.ImgName,
                Applications = new List<Application>()
            };

            //return Ok(await _context.Citizens.ToListAsync());
            //var application = new Application { SubmittedDate = citizen.Applications.SubmittedDate, Citizen = newCitizen };

            //newCitizen.Application = application;

            // _context.Citizens.Add(newCitizen);
            // await _context.SaveChangesAsync();
            //return Ok(await _context.Citizens.Include(c => c.Application.ToListAsync());

         /*   foreach (var applicationDto in citizen.Applications)
            {

                var application = new Application
                {
                    SubmittedDate = applicationDto.SubmittedDate,
                    ServiceID = applicationDto.ServiceID,
                    Citizen = newCitizen
                };
                newCitizen.Applications.Add(application);
            } */

            _context.Citizens.Add(newCitizen);
            await _context.SaveChangesAsync();

            return Ok(await _context.Citizens.Include(c => c.Applications).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Citizen>>> UpdateCitizen(Citizen update)
        {

            var updateData = await _context.Citizens.FindAsync(update.CitizenID);
            if (updateData is null)
            {
                return NotFound("Citizen not found");
            }

            updateData.FName = update.FName;
            updateData.MName = update.MName;
            updateData.LName = update.LName;
            updateData.Address = update.Address;
            updateData.Bday = update.Bday;
            updateData.Phone = update.Phone;
            updateData.Email = update.Email;
            updateData.UName = update.UName;
            updateData.Password = update.Password;
            updateData.ImgName = update.ImgName;


            await _context.SaveChangesAsync();

            return Ok(await _context.Citizens.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Citizen>>> DeleteCitizen(int id)
        {
            var delData = await _context.Citizens.FindAsync(id);
            if (delData is null)
            {
                return NotFound("Sample not found");

            }

            _context.Citizens.Remove(delData);
            await _context.SaveChangesAsync();

            return Ok(await _context.Citizens.ToListAsync());
        }
    }
}
