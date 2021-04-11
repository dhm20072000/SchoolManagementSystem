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
    public class ClassController : Controller
    {
        // GET: Class
        courseEntities db = new courseEntities();
        public ActionResult Index(string searchBy, string search, int? page, int? pagesize)
        {
            List<Faculty> flist = db.Faculties.ToList();
            ViewBag.FacultyList = new SelectList(flist, "facId", "name");

            List<ClassViewModel> l = db.Classes.Where(x => x.IsDeleted == false).Select(x => new ClassViewModel
            {
                classId = x.classId,
                classNumber = x.classNumber,
                name = x.Faculty.name,
                schedule = x.schedule,
                room = x.room
            }).ToList();

            int defaultpagesize = 2;
            if(pagesize != null)
            {
                defaultpagesize = (int)pagesize;
            }

            ClassListViewModel c = new ClassListViewModel();

            if(search != null)
            {
                if(searchBy == "Name")
                {
                    c.ipage = l.Where(x => x.name.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(c);
                }
                else if(searchBy == "Number")
                {
                    c.ipage = l.Where(x => x.classNumber.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(c);
                }
                else if(searchBy == "Room")
                {
                    c.ipage = l.Where(x => x.room.ToLower().StartsWith(search.ToLower()) || search == null).ToList().ToPagedList(page ?? 1, defaultpagesize);
                    return View(c);
                }
            }

            c.ipage = l.ToList().ToPagedList(page ?? 1, defaultpagesize);
            return View(c);
        }

        public JsonResult GetClassById(int classId)
        {
            Class c = db.Classes.SingleOrDefault(x => x.classId == classId);
            string value = JsonConvert.SerializeObject(c, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(ClassListViewModel c)
        {
            var result = false;
            try
            {
                if(c.classes.classId > 0)
                {
                    Class c1 = db.Classes.SingleOrDefault(x => x.IsDeleted == false && x.classId == c.classes.classId);
                    c1.classNumber = c.classes.classNumber;
                    c1.facId = c.classes.facId;
                    c1.schedule = c.classes.schedule;
                    c1.room = c.classes.room;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Class c1 = new Class();
                    c1.classNumber = c.classes.classNumber;
                    c1.facId = c.classes.facId;
                    c1.schedule = c.classes.schedule;
                    c1.room = c.classes.room;
                    c1.IsDeleted = false;
                    db.Classes.Add(c1);
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

        public JsonResult DeleteStudentRecord(int classId)
        {
            var result = false;
            Class c = db.Classes.SingleOrDefault(x => x.IsDeleted == false && x.classId == classId);
            if(c != null)
            {
                c.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}