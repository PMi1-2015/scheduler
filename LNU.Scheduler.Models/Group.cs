using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LNU.Scheduler.Models
{
    /// <summary>
    /// University group entity
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Like Pmi-31
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// All lectures for group
        /// </summary>
        public ICollection<Lecture> Lectures { get; set;}
        /// <summary>
        /// All hours gfor all subject for group
        /// </summary>
        public ICollection<SubjectPerWeek> SubjectsPerWeek { get; set; }

    }
}
