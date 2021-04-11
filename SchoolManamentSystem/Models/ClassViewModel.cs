using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManamentSystem.Models
{
    public class ClassViewModel
    {
        public int classId { get; set; }
        public string classNumber { get; set; }
        public int facId { get; set; }
        public string schedule { get; set; }
        public string room { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string name { get; set; }
    }
}