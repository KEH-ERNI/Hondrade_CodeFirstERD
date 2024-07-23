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
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAllDepartment()
        {
            var departments = await _context.Departments
                .Include(d => d.Services)
                .Include(d => d.Employees)
                .ToListAsync();

            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

            return Ok(departmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Services)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d=> d.DepID == id);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return Ok(departmentDto);
        }


        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> AddDepartment(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var departmentResultDto = _mapper.Map<DepartmentDto>(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepID }, departmentResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<DepartmentDto>>> UpdateDepartment(int id, DepartmentDto updateDto)
        {

            var updateData = await _context.Departments
                .Include(d => d.Services)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.DepID == id);
            
            if (updateData is null)
            {
                return NotFound("Department not found");
            }

            _mapper.Map(updateDto, updateData);

            await _context.SaveChangesAsync();

            var departmentDtos = _mapper.Map<DepartmentDto>(updateData);

            return Ok(departmentDtos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<DepartmentDto>>> DeleteDepartment(int id)
        {
            var delData = await _context.Departments
                    .Include(d => d.Services)
                    .Include(d => d.Employees)
                    .FirstOrDefaultAsync(d => d.DepID == id);

            if (delData is null)
            {
                return NotFound("Department not found");

            }

            _context.Departments.Remove(delData);
            await _context.SaveChangesAsync();

            var departments = await _context.Departments
                    .Include(d => d.Services)
                    .Include(d => d.Employees)
                    .ToListAsync();

            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }
    }
}
