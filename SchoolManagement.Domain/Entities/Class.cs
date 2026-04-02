using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string? ClassCode { get; set; }

        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string? TeacherEmail { get; set; }

        public int SchoolId { get; set; }
        public School School { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
