namespace LNU.Scheduler.Models
{
    /// <summary>
    /// Room in which lectures is conducted
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Room number (e.g., 439)
        /// </summary>
        public int Number { get; set; }
    }
}
