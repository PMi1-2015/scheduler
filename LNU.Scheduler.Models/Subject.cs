using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LNU.Scheduler.Models
{
    /// <summary>
    /// Subject for the lecture
    /// </summary>
    public class Subject
    {
        public Subject()
        {
            this.Teachers = new HashSet<Teacher>();
        }
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of subject(e.g., Programming)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// All teachers that can conduct this lecture
        /// </summary>
        public ICollection<Teacher> Teachers { get; set;}
    }
}
