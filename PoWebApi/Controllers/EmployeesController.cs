using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoWebApi.Data;
using PoWebApi.Models;

namespace PoWebApi.Controllers
{
    [Route("api/[controller]")]//this route message says http://localhost:4072/api/employees
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly PoContext _context;


        public EmployeesController(PoContext context)
        {
            _context = context;
        }

        //GET: api/Employees/sgeorge/password
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Employee>> Login(string login, string password)
        {
            var empl = await _context.Employee.SingleOrDefaultAsync(e => e.Login == login && e.Password == password);
            if(empl == null)
            {
                return NotFound();
            }
            return empl;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]//when you have the "{}" it is asking for a path variable for the url path (see the employees/5 above)
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;//manipulates the casche and keeps track of the state of the data and allows for modification
            //takes the data you take in and treats it as read and updates it 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)//exception class - means the update and change was already pushed and you need to read the data and do the modification again
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

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee); //dont need the await here as its just uploading into a casche
            await _context.SaveChangesAsync();//await instead goes here as it then will communicate the change into the database

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);//CreatedAtAction is a function that creates the response 
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")] 
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
