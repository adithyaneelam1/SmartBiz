using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SunnyPractice.Models;
using EntityState = System.Data.Entity.EntityState;

namespace SunnyPractice.Controllers
{

    [RoutePrefix("api/EmployeesApi")]
    public class EmployeesApiController : ApiController
    {
        private SunnyDBEntities db = new SunnyDBEntities();

        // GET: api/EmployeesApi
        [HttpGet]
        [Route("")]
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/EmployeesApi/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return Content(
            HttpStatusCode.NotFound,
            new
            {
                success = false,
                message = "Employee not found"
            });
            }

            return Ok(employee);
        }

        // POST: api/EmployeesApi
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return Ok(employee.Id);
        }

        // PUT: api/EmployeesApi/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch");
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return Ok(new
            {
                success = true,
                message = "Employee updated successfully",
                id = employee.Id
            });
        }

        // DELETE: api/EmployeesApi/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Any(e => e.Id == id);
        }
    }
}