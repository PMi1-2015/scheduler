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
            //test.Repository.Add(new Room() {Number = 42});
            //test.Save();
            // End

            InitializeComponent();
        }

        public void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {
            var window = new ScheduleWindow();
            window.Show();
        }

        public void OpenTeacherManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new TeacherManagerWindow();
            window.Show();
        }

        public void OpenRoomManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new RoomManagerWindow();
            window.Show();
        }

        public void OpenSubjectManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new SubjectManagerWindow();
            window.Show();
        }

        public void OpenGroupManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new GroupManagerWindow();
            window.Show();
        }
    }
}
