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
using System.IO;

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for TeacherManagerWindow.xaml
    /// </summary>
    public partial class TeacherManagerWindow : Window
    {
        IUnitOfWork<Teacher> teachers;
        IUnitOfWork<Subject> subjects;
        Dictionary<string, List<string>> data;
        bool edit;
        public TeacherManagerWindow()
        {
            InitializeComponent();
            teachers = new UnitOfWork();
            subjects = new UnitOfWork();
            data = new Dictionary<string, List<string>>();
            edit = false;
            foreach (var item in teachers.Repository.GetAll(x => true))
                listTeachers.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
            foreach (var item in subjects.Repository.GetAll(x => true))
                listSubjects.Items.Add(item.Name);
            try
            {
                string json = File.ReadAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data.json"));
                data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error reading data. Reload this window.");
            }
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != string.Empty && lastName.Text != string.Empty && listSubjects.SelectedItems.Count>0 && !edit)
            {
                AddTeacher();
            }
            else if (firstName.Text != string.Empty && lastName.Text != string.Empty && listSubjects.SelectedItems.Count > 0&& edit)
            {
                EditTeacher();
            }
            else MessageBox.Show("Input correct teacher name");
        }

        private void deleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (listTeachers.SelectedItem != null)
            {
                string[] names = listTeachers.SelectedItem.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 2)
                {
                    try
                    {
                        int id = teachers.Repository.GetAll(x=>listTeachers.SelectedItem.ToString().Contains(x.FirstName) && listTeachers.SelectedItem.ToString().Contains(x.LastName))
                            .FirstOrDefault().Id;
                        teachers.Repository.Delete(id);
                        teachers.Save();
                        data.Remove(listTeachers.SelectedItem.ToString());
                        string json = JsonConvert.SerializeObject(data);
                        File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data.json"), json);
                        listTeachers.Items.Remove(listTeachers.SelectedItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cant delete this teacher");
                    }
                    finally
                    {
                        listTeachers.SelectedItem = null;
                    }
                } 
            }
            else MessageBox.Show("Select teacher in list");
        }

        private void AddTeacher()
        {
            try
            {
                List<Subject> selectedSubjects = new List<Subject>();
                foreach (var item in listSubjects.SelectedItems)
                {
                    selectedSubjects.Add(new Subject() { Name = item.ToString() });
                }
                teachers.Repository.Add(new Teacher() { FirstName = firstName.Text, LastName = lastName.Text, Subjects = selectedSubjects });
                teachers.Save();
                List<string> names = new List<string>();
                foreach (var item in selectedSubjects)
                {
                    names.Add(item.Name);
                }
                data.Add(firstName.Text + " " + lastName.Text, names);
                string json = JsonConvert.SerializeObject(data);
                File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data.json"), json);
                listTeachers.Items.Add(string.Format("{0} {1}", firstName.Text, lastName.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding new teacher");
            }
            finally
            {
                firstName.Text = string.Empty;
                lastName.Text = string.Empty;
                listSubjects.SelectedItem = null;
            }
        }

        private void EditTeacher()
        {
            try
            {
                var teacher = teachers.Repository.GetAll(x => listTeachers.SelectedItem.ToString().Contains(x.FirstName) && listTeachers.SelectedItem.ToString().Contains(x.LastName))
                    .FirstOrDefault();
                List<Subject> selectedSubjects = new List<Subject>();
                foreach (var item in listSubjects.SelectedItems)
                {
                    selectedSubjects.Add(new Subject() { Name = item.ToString() });
                }
                teacher.Subjects = selectedSubjects;
                teacher.FirstName = firstName.Text;
                teacher.LastName = lastName.Text;
                teachers.Repository.Update(teacher);
                teachers.Save();
                data.Remove(listTeachers.SelectedItem.ToString());
                List<string> names = new List<string>();
                foreach (var item in selectedSubjects)
                {
                    names.Add(item.Name);
                }
                data.Add(firstName.Text + " " + lastName.Text, names);
                string json = JsonConvert.SerializeObject(data);
                File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data.json"), json);
                listTeachers.Items[listTeachers.SelectedIndex] = string.Format("{0} {1}", firstName.Text, lastName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing new teacher");
            }
            finally
            {
                firstName.Text = string.Empty;
                lastName.Text = string.Empty;
                listSubjects.SelectedItem = null;
                edit = false;
                addTeacher.Content = "Add";
            }
        }

        private void editTeacher_Click(object sender, RoutedEventArgs e)
        {
            edit = true;
            string[] names = listTeachers.SelectedItem.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 2)
            {
                firstName.Text = names[0];
                lastName.Text = names[1];
            }
            addTeacher.Content = "Edit";
            var subs = teachers.Repository.GetAll(x => listTeachers.SelectedItem.ToString().Contains(x.FirstName) && listTeachers.SelectedItem.ToString().Contains(x.LastName))
                .FirstOrDefault().Subjects;            
        }
    }
}