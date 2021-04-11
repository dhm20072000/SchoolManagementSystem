using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace SchoolManamentSystem.Models
{
    public class ClassListViewModel
    {
        public ClassViewModel classes { get; set;}
        public IPagedList<ClassViewModel> ipage { get; set; }
    }
}