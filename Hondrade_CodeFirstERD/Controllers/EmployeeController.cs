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
                .ToListAsync();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(d => d.Department)
                 .FirstOrDefaultAsync(d => d.EmpID == id);

            if (employee == null)
            {
                return NotFound();
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
                    .FirstOrDefaultAsync(s => s.EmpID == id);

            if (employee == null)
            {
                return NotFound();
            }

            _mapper.Map(employeeDto, employee);
            _context.Entry(employee.Department).State = EntityState.Unchanged;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var updatedEmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(updatedEmployeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            var remainingEmployees = await _context.Services.Include(s => s.Department).ToListAsync();

            var remainingEmployeeDtos = _mapper.Map<List<EmployeeDto>>(remainingEmployees);

            return Ok(remainingEmployeeDtos);

        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpID == id);
        }
    }
}
