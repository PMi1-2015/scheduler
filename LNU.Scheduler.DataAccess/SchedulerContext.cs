using System.Data.Entity;
using LNU.Scheduler.Models;

namespace LNU.Scheduler.DataAccess
{
    public class SchedulerContext : 
        DbContext
    {
        public SchedulerContext()
            : base("name=Connection")
        {
        }
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<SubjectPerWeek> SubjectsPerWeek { get; set; }
    }
}
