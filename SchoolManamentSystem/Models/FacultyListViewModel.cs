using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace SchoolManamentSystem.Models
{
    public class FacultyListViewModel
    {
        public FacultyViewModel faculty { get; set; }
        public IPagedList<FacultyViewModel> ipage { get; set;}
    }
}