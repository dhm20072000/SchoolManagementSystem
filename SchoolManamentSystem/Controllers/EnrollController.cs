using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using SchoolManamentSystem.Models;

namespace SchoolManamentSystem.Controllers
{
    public class EnrollController : Controller
    {
        courseEntities db = new courseEntities();
        // GET: Enroll
        public ActionResult Index(string searchBy, string search, int? page, int? pagesize)
        {
            List<Student> studentl = db.Students.ToList();
            List<Class> classl = db.Classes.ToList();
            ViewBag.StudentList = new SelectList(studentl, "stuId", "fullname");
            ViewBag.ClassList = new SelectList(classl, "classId", "classNumber");


            int defaultpagesize = 6;
            if (pagesize != null)
            {
                defaultpagesize = (int)pagesize;
            }


            List<EnrollViewModel> l = db.Enrolls.Where(x => x.IsDeleted == false).Select(x => new EnrollViewModel
            {
                stuId = x.stuId,
                classId = x.classId,
                grade = x.grade,
                firstName = x.Student.firstName,
                lastName = x.Student.lastName,
                classNumber = x.Class.classNumber

            }).ToList();

            EnrollListViewModel e = new EnrollListViewModel();
            EnrollViewModel enroll = new EnrollViewModel();

            if (search != null)
            {
                if (searchBy == "Student Name")
                {
                    e.ipage = l.Where(x => x.fullname.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    //e.enroll = enroll;
                    return View(e);
                }
                else if (searchBy == "Class Number")
                {
                    e.ipage = l.Where(x => x.classNumber.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    //e.enroll = enroll;
                    return View(e);
                }
                else if (searchBy == "Grade")
                {
                    e.ipage = l.Where(x => x.grade.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    //e.enroll = enroll;
                    return View(e);
                }
            }
            //else
            //{
            e.ipage = l.ToList().ToPagedList(page ?? 1, defaultpagesize);
            //e.enroll = enroll;
            return View(e);
            //}

        }

        public JsonResult GetEnrollById(int studentId, int classId)
        {
            Enroll e = db.Enrolls.SingleOrDefault(x => x.stuId == studentId && x.classId == classId);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(e, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }



        public JsonResult SaveDataInDatabase(int stuId, int classId, int stuId1, int classId1, string grade)
        {
            var result = false;
            try
            {
                if (stuId > 0 && classId > 0)
                {
                    Enroll e = db.Enrolls.SingleOrDefault(x => x.stuId == stuId && x.classId == classId);
                    e.grade = grade;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Enroll e = new Enroll();
                    Enroll e1 = db.Enrolls.SingleOrDefault(x => x.stuId == stuId1 && x.classId == classId1 && x.IsDeleted == true);
                    if (e1 != null)
                    {
                        e1.IsDeleted = false;
                        db.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        e.stuId = stuId1;
                        e.classId = classId1;
                        e.grade = grade;
                        e.IsDeleted = false;
                        db.Enrolls.Add(e);
                        db.SaveChanges();
                        result = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEnrollRecord(int stuId, int classId)
        {
            var result = false;
            Enroll e = db.Enrolls.SingleOrDefault(x => x.stuId == stuId && x.classId == classId && x.IsDeleted == false);
            if (e != null)
            {
                e.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}