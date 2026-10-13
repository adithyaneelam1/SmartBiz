using SunnyPractice.Models;
using System.Linq;
using System.Web.Mvc;

namespace SunnyPractice.Controllers
{
    public class Emp3Controller : Controller
    {
        MyDbContext db = new MyDbContext();


       
        // EMPLOYEE INSERT / UPDATE PAGE
    
        [HttpGet]
        public ActionResult Emp(int Eid = 0)
        {
            if (Eid == 0)
            {
                return View(new EMP3());
            }

            var employee = db.Emp.Find(Eid);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }


        
        // EMPLOYEE LIST PAGE

        [HttpGet]
        public ActionResult Emplist()
        {
            var employees = db.Emp.ToList();

            return View(employees);
        }


       
        // INSERT EMPLOYEE - AJAX
     
        [HttpPost]
        public JsonResult Create(EMP3 emp)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Please enter valid employee details."
                });
            }

            db.Emp.Add(emp);

            db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Employee inserted successfully!",
                Eid = emp.Eid
            });
        }


    
        // UPDATE EMPLOYEE - AJAX


        [HttpPost]
        public JsonResult Edit(EMP3 emp)
        {
            var employee = db.Emp.Find(emp.Eid);

            if (employee == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Employee not found."
                });
            }

            employee.Ename = emp.Ename;

            employee.salary = emp.salary;

            db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Employee updated successfully!"
            });
        }


       
        // GET EMPLOYEE - AJAX
     
      
        [HttpGet]
        public JsonResult GetEmp(int Eid)
        {
            var employee = db.Emp.Find(Eid);

            if (employee == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Employee not found."
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,

                Eid = employee.Eid,

                Ename = employee.Ename,

                salary = employee.salary

            },
            JsonRequestBehavior.AllowGet);
        }


        // DELETE EMPLOYEE - AJAX
        

        [HttpPost]
        public JsonResult Delete(int Eid)
        {
            var employee = db.Emp.Find(Eid);

            if (employee == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Employee not found."
                });
            }

            db.Emp.Remove(employee);

            db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Employee deleted successfully!"
            });
        }
    }
}