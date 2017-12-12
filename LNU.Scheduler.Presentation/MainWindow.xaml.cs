using LNU.Scheduler.Contracts;
using LNU.Scheduler.Models;
using LNU.Scheduler.DataAccess;
using System.Windows;

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUnitOfWork<Group> groups;
        IUnitOfWork<Room> rooms;
        public MainWindow()
        {
            groups = new UnitOfWork();
            rooms = new UnitOfWork();
            InitializeComponent();
            selectGroup.IsEnabled = false;
            foreach (var item in groups.Repository.GetAll(x => true))
            {
                groupList.Items.Add(item.Name);
            }
        }
        public MainWindow(IUnitOfWork<Room> test)
        {
            //groupList = new System.Windows.Controls.ListView();

            // TODO: remove test code
            //test.Repository.Add(new Room() {Number = 42});
            //test.Save();
            // End
            
            
            //groupList.Items.Refresh();
            InitializeComponent();
            foreach (var item in test.Repository.GetAll(x => true))
            {
                groupList.Items.Add(item);
            }

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

        private void groupList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectGroup.IsEnabled = true;
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            groupList.Items.Clear();
            foreach (var item in groups.Repository.GetAll(x => true))
            {
                groupList.Items.Add(item.Name);
            }
        }
    }
}
