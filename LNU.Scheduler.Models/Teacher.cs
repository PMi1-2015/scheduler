using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.Scheduler.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Subject> TeachedSubjects { get; set; }
    }
}
