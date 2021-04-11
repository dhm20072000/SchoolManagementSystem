using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace SchoolManamentSystem.Models
{
    public class StudentListViewModel
    {
        public StudentViewModel student { get; set; }
        public IPagedList<StudentViewModel> ipage { get; set; }
    }
}