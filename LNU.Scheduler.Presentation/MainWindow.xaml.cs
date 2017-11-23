using LNU.Scheduler.Contracts;
using LNU.Scheduler.Models;
using System.Windows;

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        }

        public MainWindow(IUnitOfWork<Room> test)
        {
            // TODO: remove test code
            var result = test.Repository.GetAll(x => x.Number == 43);
            test.Repository.Add(new Room() {Number = 43});
            test.Save();
            // End

            InitializeComponent();
        }
    }
}
