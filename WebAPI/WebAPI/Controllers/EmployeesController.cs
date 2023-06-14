using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [EnableCors("AllowLocal")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _db;
        public EmployeesController(NorthwindContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {
            var result=  _db.Employees.Select(x => new EmployeesDTO
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title
            });

            return Ok(result);
        }
        //GET{id}
        [HttpGet("{id}")]
        public IEnumerable<EmployeesDTO> GetEmployees(int id)
        {
            return _db.Employees.Where(e => e.EmployeeId == id).Select(x => new EmployeesDTO
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title
            });
        }

        //Post
        [HttpPost]
        public IActionResult PostEmployees(EmployeesDTO employees)
        {
            Employees emp = new Employees()
            {
                EmployeeId = employees.EmployeeId,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Title = employees.Title
            };
            _db.Employees.Add(emp);
            _db.SaveChanges();
            return Ok(emp);

        }

        [HttpPut("{id}")]
        public IActionResult PutEmployees(int id,EmployeesDTO employees)
        {
            var findresult=_db.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (findresult != null)
            {
                findresult.FirstName = employees.FirstName;
                findresult.LastName = employees.LastName;
                findresult.Title = employees.Title;
                _db.Employees.Update(findresult);
                _db.SaveChanges();
                return Ok(employees);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(int id,EmployeesDTO employees)
        {
            var findresult = _db.Employees.Find(id);
            if (findresult != null)
            {
                _db.Employees.Remove(findresult);
                _db.SaveChanges();
                return Ok($"員工編號:{id},資料已刪除!");
            }
            return BadRequest($"找不到編號為{id}的員工");
        }

        

    }
}
