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
using LNU.Scheduler.Contracts;
using LNU.Scheduler.Models;
using LNU.Scheduler.DataAccess;
using Newtonsoft.Json;

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        int day;
        IUnitOfWork<Teacher> teachers;
        IUnitOfWork<Subject> subjects;
        IUnitOfWork<Lecture> lectures;
        IUnitOfWork<Room> rooms;
        Dictionary<string, List<string>> data;
        int group;
        public ScheduleWindow(int groupId)
        {
            group = groupId;
            day = 1;
            teachers = new UnitOfWork();
            subjects = new UnitOfWork();
            lectures = new UnitOfWork();
            rooms = new UnitOfWork();
            data = new Dictionary<string, List<string>>();
            try
            {
                string json = System.IO.File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "data.json"));
                data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error read data. Reload this window.");
            }
            InitializeComponent();
            foreach(var item in teachers.Repository.GetAll(x=>true))
            {
                teacher1.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher2.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher3.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher4.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher5.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher6.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));                
            }
            foreach(var item in rooms.Repository.GetAll(x=>true))
            {
                room1.Items.Add(item.Number);
                room2.Items.Add(item.Number);
                room3.Items.Add(item.Number);
                room4.Items.Add(item.Number);
                room5.Items.Add(item.Number);
                room6.Items.Add(item.Number);
            }
            subject1.IsEnabled = false;
            subject2.IsEnabled = false;
            subject3.IsEnabled = false;
            subject4.IsEnabled = false;
            subject5.IsEnabled = false;
            subject6.IsEnabled = false;

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

        private void NewDay(int day)
        {
            switch(day)
            {
                case 1:
                    dayLabel.Content = "Monday";
                    break;
                case 2:
                    dayLabel.Content = "Tuesday";
                    break;
                case 3:
                    dayLabel.Content = "Wednesday";
                    break;
                case 4:
                    dayLabel.Content = "Thursday";
                    break;
                case 5:
                    dayLabel.Content = "Friday";
                    mainButton.Content = "Save";
                    break;
            }
            teacher1.Items.Clear();
            teacher2.Items.Clear();
            teacher3.Items.Clear();
            teacher4.Items.Clear();
            teacher5.Items.Clear();
            teacher6.Items.Clear();
            subject1.Items.Clear();
            subject2.Items.Clear();
            subject3.Items.Clear();
            subject4.Items.Clear();
            subject5.Items.Clear();
            subject6.Items.Clear();
            room1.Items.Clear();
            room2.Items.Clear();
            room3.Items.Clear();
            room4.Items.Clear();
            room5.Items.Clear();
            room6.Items.Clear();
            teacher1.SelectedItem = null;
            teacher2.SelectedItem = null;
            teacher3.SelectedItem = null;
            teacher4.SelectedItem = null;
            teacher5.SelectedItem = null;
            teacher6.SelectedItem = null;
            subject1.SelectedItem = null;
            subject2.SelectedItem = null;
            subject3.SelectedItem = null;
            subject4.SelectedItem = null;
            subject5.SelectedItem = null;
            subject6.SelectedItem = null;
            room1.SelectedItem = null;
            room2.SelectedItem = null;
            room3.SelectedItem = null;
            room4.SelectedItem = null;
            room5.SelectedItem = null;
            room6.SelectedItem = null;
            foreach (var item in teachers.Repository.GetAll(x => true))
            {
                teacher1.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher2.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher3.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher4.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher5.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
                teacher6.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
            }
            foreach (var item in rooms.Repository.GetAll(x => true))
            {
                room1.Items.Add(item.Number);
                room2.Items.Add(item.Number);
                room3.Items.Add(item.Number);
                room4.Items.Add(item.Number);
                room5.Items.Add(item.Number);
                room6.Items.Add(item.Number);
            }
            subject1.IsEnabled = false;
            subject2.IsEnabled = false;
            subject3.IsEnabled = false;
            subject4.IsEnabled = false;
            subject5.IsEnabled = false;
            subject6.IsEnabled = false;
        }

        private void Save()
        {
            if (teacher1.SelectedItem != null && subject1.SelectedItem != null && room1.SelectedItem != null)
            {
                var lecture = new Lecture();
                lecture.GroupId = group;
                var subj = new Subject() { Name = subject1.SelectedItem.ToString() };
                lecture.Subject = subj;
                subj = null;
                var teach = new Teacher() { FirstName = teacher1.SelectedItem.ToString() };
                lecture.Teacher = teach;
                teach = null;
                switch(day)
                {
                    case 1:
                        lecture.WeekDay = Models.enums.WeekDays.Monday;
                        break;
                    case 2:
                        lecture.WeekDay = Models.enums.WeekDays.Tuesday;
                        break;
                    case 3:
                        lecture.WeekDay = Models.enums.WeekDays.Wedwednesday;
                        break;
                    case 4:
                        lecture.WeekDay = Models.enums.WeekDays.Thursday;
                        break;
                    case 5:
                        lecture.WeekDay = Models.enums.WeekDays.Friday;
                        break;
                }
                lecture.Number = 1;
                var room = new Room() { Number = (int)room1.SelectedItem };
                lecture.Room = room;
                lectures.Repository.Add(lecture);
                lectures.Save();
            }
            if (teacher2.SelectedItem != null && subject2.SelectedItem != null && room2.SelectedItem != null)
            {
                var lecture = new Lecture();
                lecture.GroupId = group;
                var subj = new Subject() { Name = subject2.SelectedItem.ToString() };
                lecture.Subject = subj;
                subj = null;
                var teach = new Teacher() { FirstName = teacher2.SelectedItem.ToString() };
                lecture.Teacher = teach;
                teach = null;
                switch (day)
                {
                    case 1:
                        lecture.WeekDay = Models.enums.WeekDays.Monday;
                        break;
                    case 2:
                        lecture.WeekDay = Models.enums.WeekDays.Tuesday;
                        break;
                    case 3:
                        lecture.WeekDay = Models.enums.WeekDays.Wedwednesday;
                        break;
                    case 4:
                        lecture.WeekDay = Models.enums.WeekDays.Thursday;
                        break;
                    case 5:
                        lecture.WeekDay = Models.enums.WeekDays.Friday;
                        break;
                }
                lecture.Number = 2;
                var room = new Room() { Number = (int)room2.SelectedItem };
                lecture.Room = room;
                lectures.Repository.Add(lecture);
                lectures.Save();
            }
            if (teacher3.SelectedItem != null && subject3.SelectedItem != null && room3.SelectedItem != null)
            {
                var lecture = new Lecture();
                lecture.GroupId = group;
                var subj = new Subject() { Name = subject3.SelectedItem.ToString() };
                lecture.Subject = subj;
                subj = null;
                var teach = new Teacher() { FirstName = teacher3.SelectedItem.ToString() };
                lecture.Teacher = teach;
                teach = null;
                switch (day)
                {
                    case 1:
                        lecture.WeekDay = Models.enums.WeekDays.Monday;
                        break;
                    case 2:
                        lecture.WeekDay = Models.enums.WeekDays.Tuesday;
                        break;
                    case 3:
                        lecture.WeekDay = Models.enums.WeekDays.Wedwednesday;
                        break;
                    case 4:
                        lecture.WeekDay = Models.enums.WeekDays.Thursday;
                        break;
                    case 5:
                        lecture.WeekDay = Models.enums.WeekDays.Friday;
                        break;
                }
                lecture.Number = 3;
                var room = new Room() { Number = (int)room3.SelectedItem };
                lecture.Room = room;
                lectures.Repository.Add(lecture);
                lectures.Save();
            }
            if (teacher4.SelectedItem != null && subject4.SelectedItem != null && room4.SelectedItem != null)
            {
                var lecture = new Lecture();
                lecture.GroupId = group;
                var subj = new Subject() { Name = subject4.SelectedItem.ToString() };
                lecture.Subject = subj;
                subj = null;
                var teach = new Teacher() { FirstName = teacher4.SelectedItem.ToString() };
                lecture.Teacher = teach;
                teach = null;
                switch (day)
                {
                    case 1:
                        lecture.WeekDay = Models.enums.WeekDays.Monday;
                        break;
                    case 2:
                        lecture.WeekDay = Models.enums.WeekDays.Tuesday;
                        break;
                    case 3:
                        lecture.WeekDay = Models.enums.WeekDays.Wedwednesday;
                        break;
                    case 4:
                        lecture.WeekDay = Models.enums.WeekDays.Thursday;
                        break;
                    case 5:
                        lecture.WeekDay = Models.enums.WeekDays.Friday;
                        break;
                }
                lecture.Number = 4;
                var room = new Room() { Number = (int)room4.SelectedItem };
                lecture.Room = room;
                lectures.Repository.Add(lecture);
                lectures.Save();
            }
            if (teacher5.SelectedItem != null && subject5.SelectedItem != null && room5.SelectedItem != null)
            {
                var lecture = new Lecture();
                lecture.GroupId = group;
                var subj = new Subject() { Name = subject5.SelectedItem.ToString() };
                lecture.Subject = subj;
                subj = null;
                var teach = new Teacher() { FirstName = teacher5.SelectedItem.ToString() };
                lecture.Teacher = teach;
                teach = null;
                switch (day)
                {
                    case 1:
                        lecture.WeekDay = Models.enums.WeekDays.Monday;
                        break;
                    case 2:
                        lecture.WeekDay = Models.enums.WeekDays.Tuesday;
                        break;
                    case 3:
                        lecture.WeekDay = Models.enums.WeekDays.Wedwednesday;
                        break;
                    case 4:
                        lecture.WeekDay = Models.enums.WeekDays.Thursday;
                        break;
                    case 5:
                        lecture.WeekDay = Models.enums.WeekDays.Friday;
                        break;
                }
                lecture.Number = 5;
                var room = new Room() { Number = (int)room5.SelectedItem };
                lecture.Room = room;
                lectures.Repository.Add(lecture);
                lectures.Save();
            }
            if (teacher6.SelectedItem != null && subject6.SelectedItem != null && room6.SelectedItem != null)
            {
                var lecture = new Lecture();
                lecture.GroupId = group;
                var subj = new Subject() { Name = subject6.SelectedItem.ToString() };
                lecture.Subject = subj;
                subj = null;
                var teach = new Teacher() { FirstName = teacher6.SelectedItem.ToString() };
                lecture.Teacher = teach;
                teach = null;
                switch (day)
                {
                    case 1:
                        lecture.WeekDay = Models.enums.WeekDays.Monday;
                        break;
                    case 2:
                        lecture.WeekDay = Models.enums.WeekDays.Tuesday;
                        break;
                    case 3:
                        lecture.WeekDay = Models.enums.WeekDays.Wedwednesday;
                        break;
                    case 4:
                        lecture.WeekDay = Models.enums.WeekDays.Thursday;
                        break;
                    case 5:
                        lecture.WeekDay = Models.enums.WeekDays.Friday;
                        break;
                }
                lecture.Number = 6;
                var room = new Room() { Number = (int)room6.SelectedItem };
                lecture.Room = room;
                lectures.Repository.Add(lecture);
                lectures.Save();
            }
        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {

            switch(day)
            { 
                case 1:
                case 2:
                case 3:
                case 4:
                    Save();
                    day++;
                    NewDay(day);
                    break;
                case 5:
                    Save();
                    this.Close();
                    break;
            }
        }

        private void teacher1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher1.SelectedItem != null)
            {
                subject1.Items.Clear();
                var subjs = data[teacher1.SelectedItem.ToString()];
                foreach (var item in subjs)
                    subject1.Items.Add(item);
                subject1.IsEnabled = true;
            }
        }

        private void teacher2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher2.SelectedItem != null)
            {
                subject2.Items.Clear();
                var subjs = data[teacher2.SelectedItem.ToString()];
                foreach (var item in subjs)
                    subject2.Items.Add(item);
                subject2.IsEnabled = true;
            }
        }

        private void teacher3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher3.SelectedItem != null)
            {
                subject3.Items.Clear();
                var subjs = data[teacher3.SelectedItem.ToString()];
                foreach (var item in subjs)
                    subject3.Items.Add(item);
                subject3.IsEnabled = true;
            }
        }

        private void teacher4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher4.SelectedItem != null)
            {
                subject4.Items.Clear();
                var subjs = data[teacher4.SelectedItem.ToString()];
                foreach (var item in subjs)
                    subject4.Items.Add(item);
                subject4.IsEnabled = true;
            }
        }

        private void teacher5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher5.SelectedItem != null)
            {
                subject5.Items.Clear();
                var subjs = data[teacher5.SelectedItem.ToString()];
                foreach (var item in subjs)
                    subject5.Items.Add(item);
                subject5.IsEnabled = true;
            }
        }

        private void teacher6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher6.SelectedItem != null)
            {
                subject6.Items.Clear();
                var subjs = data[teacher6.SelectedItem.ToString()];
                foreach (var item in subjs)
                    subject6.Items.Add(item);
                subject6.IsEnabled = true;
            }
        }
    }
}
