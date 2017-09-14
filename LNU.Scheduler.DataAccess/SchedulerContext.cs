using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNU.Scheduler.Models;

namespace LNU.Scheduler.DataAccess
{
    public class SchedulerContext : 
        DbContext
    {
        public  DbSet<Subject> Subjects { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }
}
