using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManamentSystem.Models
{
    public class EnrollViewModel
    {
        public int stuId { get; set; }
        public int classId { get; set; }
        public string grade { get; set; }
        public bool IsDeleted { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string classNumber { get; set; }
        public string fullname { get { return string.Format("{0} {1}", firstName, lastName); } }
    }
}