using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.Scheduler.Models
{
    public class SubjectPerWeek
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Subject for which to determine hours
        /// </summary>
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        
        /// <summary>
        /// Group for which to determine hours
        /// </summary>
        public int GroupId { get; set; }
        public Group Group { get; set; }
        /// <summary>
        /// Number of hours for subject
        /// </summary>
        public int Hours { get; set; }

    }
}
