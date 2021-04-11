using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManamentSystem.Models
{
    public class StudentViewModel
    {
        public int stuId { get; set; }
        public string stuNum { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string major { get; set; }
        public Nullable<int> credits { get; set; }
    }
}