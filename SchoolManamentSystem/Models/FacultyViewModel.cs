using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManamentSystem.Models
{
    public class FacultyViewModel
    {
        public int facId { get; set; }
        public string facNum { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string rank { get; set; }
    }
}