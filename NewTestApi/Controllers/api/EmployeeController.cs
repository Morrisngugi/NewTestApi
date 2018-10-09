using NewTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewTestApi.Controllers
{
    public class EmployeeController : ApiController
    {
        //Get All Employees
       public IHttpActionResult GetAllEmployees()
        {
            IList<EmployeeViewModel> employees = null;
            using (var ctx = new EmployeesEntities())
            {
                employees = ctx.Employees.Include("Employee").Select(s => new EmployeeViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    JoiningDate = s.JoiningDate
                }).ToList<EmployeeViewModel>();
            }
            if (employees.Count==0)
            {
                return NotFound();
            }
            return Ok(employees);
        }
        //Get Employee by Id
        public IHttpActionResult GetEmployeeById(int id)
        {
            EmployeeViewModel employee = null;
            using (var ctx = new EmployeesEntities())
            {
                employee = ctx.Employees.Include("Employee").Where(s => s.Id == id).Select(s => new EmployeeViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    JoiningDate = s.JoiningDate
                }).FirstOrDefault<EmployeeViewModel>();
            }
            if (employee==null)
            {
                return NotFound();
            }
                return Ok(employee);
        }
          // New Employee Detals
        public IHttpActionResult AddEmployee(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            using (var ctx = new EmployeesEntities())
            {
                ctx.Employees.Add(new Employee()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    JoiningDate = employee.JoiningDate
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        //Update Employee details
        public IHttpActionResult Put(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not valid");
            }
            using (var ctx = new EmployeesEntities())
            {
                var existingEmployee = ctx.Employees.Where(s => s.Id == employee.Id).FirstOrDefault();
                if (existingEmployee!=null)
                {
                    existingEmployee.Name = employee.Name;
                    existingEmployee.Age = employee.Age;
                    existingEmployee.JoiningDate = employee.JoiningDate;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        //Delete an Employee
             public IHttpActionResult Delete(int id)
        {
            if (id<0)
            {
                return BadRequest("Invalid employee Id");
            }
            using (var ctx = new EmployeesEntities())
            {
                var employee = ctx.Employees.Where(s => s.Id == id).FirstOrDefault();
                ctx.Entry(employee).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
