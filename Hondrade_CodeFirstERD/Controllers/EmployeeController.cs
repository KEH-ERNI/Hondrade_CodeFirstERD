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
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(DataContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployee()
        {
            var employees = await _context.Employees
                .Include(d => d.Department)
                .Include(d => d.Contacts)
                .ToListAsync();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(d => d.Department)
                .Include(d => d.Contacts)
                .FirstOrDefaultAsync(d => d.EmpID == id);

            if (employee is null)
            {
                return NotFound("Employee not found.");
            }

            var employeeDtos = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDtos);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>>AddEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            var department = await _context.Departments.FindAsync(employeeDto.DepID);

            if(department == null)
            {
                return BadRequest("Invaid DepID");
            }

            employee.Department = department;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeResultDto = _mapper.Map<EmployeeDto>(employee);
            return CreatedAtAction(nameof(GetEmployee), new {id = employee.EmpID}, employeeResultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto) {
            
            var employee = await _context.Employees
                    .Include(d => d.Department)
                    .Include(d => d.Contacts)
                    .FirstOrDefaultAsync(s => s.EmpID == id);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            _mapper.Map(employeeDto, employee);
            _context.Entry(employee.Department).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();

            var updatedEmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(updatedEmployeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            var remainingEmployees = await _context.Employees
                .Include(s => s.Department)
                .Include(d => d.Contacts)
                .ToListAsync();

            var remainingEmployeeDtos = _mapper.Map<List<EmployeeDto>>(remainingEmployees);

            return Ok(remainingEmployeeDtos);

        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpID == id);
        }
    }
}
