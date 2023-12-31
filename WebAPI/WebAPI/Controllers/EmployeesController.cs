﻿using Microsoft.AspNetCore.Cors;
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
        public IActionResult GetEmployees(int id)
        {
            var result=_db.Employees.Where(e => e.EmployeeId == id).Select(x => new EmployeesDTO
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title
            });
            return Ok(result);
        }

        //Post
        [HttpPost]
        public IActionResult PostEmployees(EmployeesDTO employees)
        {
            Employees emp = new Employees()
            {
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Title = employees.Title
            };
            _db.Employees.Add(emp);
            _db.SaveChanges();
            return Ok("新增成功");

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
                return Ok("修改成功!!");
            }
            return BadRequest("修改失敗!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(int id)
        {
            var findresult = _db.Employees.Find(id);
            if (findresult != null)
            {
                _db.Employees.Remove(findresult);
                _db.SaveChanges();
                return Ok($"員工編號:{id},資料已刪除!");
            }
            return BadRequest("刪除失敗");
        }

        

    }
}
