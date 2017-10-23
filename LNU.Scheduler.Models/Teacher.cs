using System.Collections.Generic;

namespace LNU.Scheduler.Models
{ 
    /// <summary>
    /// Teacher for the lecture
    /// </summary>
    public class Teacher
    {
        public Teacher()
        {
            this.Subjects = new HashSet<Subject>();
        }
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Teacher's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Teacher's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// All lectures for the week
        /// </summary>
        public ICollection<Lecture> Lectures { get; set; }

        /// <summary>
        /// All subjects that the teacher can conduct
        /// </summary>
        public ICollection<Subject> Subjects { get; set; } 
    }
}
