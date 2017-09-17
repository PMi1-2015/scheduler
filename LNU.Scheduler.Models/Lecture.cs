using LNU.Scheduler.Models.enums;

namespace LNU.Scheduler.Models
{
    /// <summary>
    /// Lecture which is teached by teachers. Main entity
    /// </summary>
    public class Lecture
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lecture subject
        /// </summary>
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }

        /// <summary>
        /// Lecture for which group
        /// </summary>
        public Group Group { get; set; }
        public int GroupId { get; set; }

        /// <summary>
        /// In which room lecture
        /// </summary>
        public Room Room { get; set; }
        public int RoomId { get; set; }

        /// <summary>
        /// In which room lecture
        /// </summary>
        public WeekDays WeekDay { get; set; }
    }
}
