using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        
        // Student login cheyyanam, so User-umayi link cheyyanam
        // Email, FirstName, LastName okke User tablil mathi
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int Points { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}
