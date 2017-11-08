using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchedulerViews
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        public ScheduleWindow()
        {
            InitializeComponent();
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
