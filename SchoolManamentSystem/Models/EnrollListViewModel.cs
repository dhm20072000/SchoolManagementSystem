using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace SchoolManamentSystem.Models
{
    public class EnrollListViewModel
    {
        public IPagedList<EnrollViewModel> ipage { get; set; }
        public EnrollViewModel enroll { get; set; }
    }
}