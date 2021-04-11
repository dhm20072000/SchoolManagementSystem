using SchoolManamentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;

namespace SchoolManamentSystem.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        courseEntities db = new courseEntities();
        public ActionResult Index(string searchBy, string search, int? page, int? pagesize )
        {
            List<StudentViewModel> l = db.Students.Select(x => new StudentViewModel
            {
                stuId = x.stuId,
                stuNum = x.stuNum,
                lastName = x.lastName,
                firstName = x.firstName,
                major = x.major,
                credits = x.credits
            }).ToList();

            int defaultpagesize = 5;
            if(pagesize != null)
            {
                defaultpagesize = (int)pagesize;
            }

            StudentListViewModel s = new StudentListViewModel();

            if(search != null)
            {
                if(searchBy == "FirstName")
                {
                    s.ipage = l.Where(x => x.firstName.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(s);
                }
                else if(searchBy == "LastName")
                {
                    s.ipage = l.Where(x => x.lastName.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(s);
                }
                else if(searchBy == "Major")
                {
                    s.ipage = l.Where(x => x.major.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(s);
                }
            }

            s.ipage = l.ToList().ToPagedList(page ?? 1, defaultpagesize);
            return View(s);

        }

        public JsonResult GetStudentById(int studentId)
        {
            Student s = db.Students.SingleOrDefault(x => x.stuId == studentId);
            string value = JsonConvert.SerializeObject(s, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(StudentListViewModel s)
        {
            var result = false;
            try
            {
                if(s.student.stuId > 0)
                {
                    Student s1 = db.Students.SingleOrDefault(x => x.stuId == s.student.stuId);
                    s1.stuNum = s.student.stuNum;
                    s1.lastName = s.student.lastName;
                    s1.firstName = s.student.firstName;
                    s1.major = s.student.major;
                    s1.credits = s.student.credits;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Student s1 = new Student();
                    s1.stuNum = s.student.stuNum;
                    s1.lastName = s.student.lastName;
                    s1.firstName = s.student.firstName;
                    s1.major = s.student.major;
                    s1.credits = s.student.credits;
                    db.Students.Add(s1);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStudentRecord(int studentId)
        {
            var result = false;
            Student s = db.Students.SingleOrDefault(x => x.stuId == studentId);
            if(s != null)
            {
                db.Students.Remove(s);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}