namespace LNU.Scheduler.Models
{ 
    /// <summary>
    /// Subject for the lecture
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of subject(e.g., Programming)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Teacher for the lecture
        /// </summary>
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        /// <summary>
        /// Number of this subject as lectures per week
        /// </summary>
        public int NumberPerWeek { get; set; }
    }
}
