using LNU.Scheduler.Contracts;
using LNU.Scheduler.Models;
using LNU.Scheduler.DataAccess;
using System.Windows;
using System.Linq;

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUnitOfWork<Group> groups;
        IUnitOfWork<Lecture> lectures;
        IUnitOfWork<Teacher> teachers;
        IUnitOfWork<Room> rooms;
        IUnitOfWork<Subject> subjects;
        public MainWindow()
        {
            groups = new UnitOfWork();
            lectures = new UnitOfWork();
            teachers = new UnitOfWork();
            rooms = new UnitOfWork();
            subjects = new UnitOfWork();
            InitializeComponent();
            selectGroup.IsEnabled = false;
            foreach (var item in groups.Repository.GetAll(x => true))
            {
                groupList.Items.Add(item.Name);
            }
        }
        public MainWindow(IUnitOfWork<Room> test)
        {            
            InitializeComponent();
            foreach (var item in test.Repository.GetAll(x => true))
            {
                groupList.Items.Add(item);
            }

        }

        public void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {
            
            var id = groups.Repository.GetAll(x => true).Where(x => x.Name == groupList.SelectedItem.ToString()).FirstOrDefault().Id;
            //lectures.Repository.Delete(id);
            //lectures.Save();
            var window = new ScheduleWindow(id);
            window.ShowDialog();            
        }

        public void OpenTeacherManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new TeacherManagerWindow();
            window.ShowDialog();
        }

        public void OpenRoomManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new RoomManagerWindow();
            window.ShowDialog();
        }

        public void OpenSubjectManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new SubjectManagerWindow();
            window.ShowDialog();
        }

        public void OpenGroupManagerWindow(object sender, RoutedEventArgs e)
        {
            var window = new GroupManagerWindow();
            window.ShowDialog();
        }

        private void groupList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (groupList.SelectedItem != null)
            {
                selectGroup.IsEnabled = true;
                var id = groups.Repository.GetAll(x => true).Where(x => x.Name == groupList.SelectedItem.ToString()).FirstOrDefault().Id;
                var result = lectures.Repository.GetAll(x => x.GroupId == id);
                schedule.Items.Clear();
                foreach (var item in result.OrderBy(x=>x.WeekDay).ThenBy(x => x.Number))
                {
                    var teacher = teachers.Repository.GetAll(x => x.Id == item.TeacherId).FirstOrDefault();
                    var subject = subjects.Repository.GetAll(x => x.Id == item.SubjectId).FirstOrDefault();
                    var room = rooms.Repository.GetAll(x => x.Id == item.RoomId).FirstOrDefault();
                    schedule.Items.Add(string.Format("[{3}]Teacher: {0} | Subject: {1} | Room: {2} | Day: {3}",
                        teacher.FirstName + " " + teacher.LastName, subject.Name, room.Number, item.Number, item.WeekDay));
                }
            }
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
