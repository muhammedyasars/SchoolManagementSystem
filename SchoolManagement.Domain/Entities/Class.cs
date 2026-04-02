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

        // Teacher name details users-il mathram mathi
        // Id mathram mathi teacher-ne identify cheyyan
        public int TeacherUserId { get; set; }
        public User TeacherUser { get; set; }

        public int SchoolId { get; set; }
        public School School { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
