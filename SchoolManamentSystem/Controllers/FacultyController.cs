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
    public class FacultyController : Controller
    {
        // GET: Faculty
        courseEntities db = new courseEntities();
        public ActionResult Index(string searchBy, string search, int? page, int? pagesize)
        {
            List<FacultyViewModel> l = db.Faculties.Select(x => new FacultyViewModel
            {
                facId = x.facId,
                facNum = x.facNum,
                name = x.name,
                department = x.department,
                rank = x.rank
            }).ToList();

            int defaultpagesize = 2;
            if(pagesize != null)
            {
                defaultpagesize = (int)pagesize;
            }

            FacultyListViewModel f = new FacultyListViewModel();

            if(search != null)
            {
                if(searchBy == "Department")
                {
                    f.ipage = l.Where(x => x.department.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(f);
                }
                else if(searchBy == "Rank")
                {
                    f.ipage = l.Where(x => x.rank.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(f);
                }
                else if(searchBy == "Name")
                {
                    f.ipage = l.Where(x => x.name.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(f);
                }
            }

            f.ipage = l.ToList().ToPagedList(page ?? 1, defaultpagesize);
            return View(f);
        }

        public JsonResult GetFacultyById(int facultyId)
        {
            Faculty f = db.Faculties.SingleOrDefault(x => x.facId == facultyId);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(f, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(FacultyListViewModel f)
        {
            var result = false;
            try
            {
                if(f.faculty.facId > 0)
                {
                    Faculty f1 = db.Faculties.SingleOrDefault(x => x.facId == f.faculty.facId);
                    f1.facNum = f.faculty.facNum;
                    f1.name = f.faculty.name;
                    f1.department = f.faculty.department;
                    f1.rank = f.faculty.rank;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Faculty f1 = new Faculty();
                    f1.facNum = f.faculty.facNum;
                    f1.name = f.faculty.name;
                    f1.department = f.faculty.department;
                    f1.rank = f.faculty.rank;
                    db.Faculties.Add(f1);
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

        public JsonResult DeleteStudentRecord(int facultyId)
        {
            var result = false;
            Faculty f = db.Faculties.SingleOrDefault(x => x.facId == facultyId);
            if(f != null)
            {
                db.Faculties.Remove(f);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}