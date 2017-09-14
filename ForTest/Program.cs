using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNU.Scheduler.DataAccess;
using LNU.Scheduler.Models;

namespace ForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new UnitOfWork();

            a.RoomRepository.Delete(2);
            a.Save();
            
        }
    }
}
