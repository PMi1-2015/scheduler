using LNU.Scheduler.Contracts;
using LNU.Scheduler.DataAccess;
using LNU.Scheduler.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();

            var unitOfWork = new UnitOfWork();
            container.RegisterInstance<IUnitOfWork<Room>>(unitOfWork);

            //container.Resolve<MainWindow>();
        }
    }
}
