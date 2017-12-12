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

namespace LNU.Scheduler.Presentation
{
    /// <summary>
    /// Interaction logic for TeacherManagerWindow.xaml
    /// </summary>
    public partial class TeacherManagerWindow : Window
    {
        IUnitOfWork<Teacher> teachers;
        public TeacherManagerWindow()
        {
            InitializeComponent();
            teachers = new UnitOfWork();
            foreach (var item in teachers.Repository.GetAll(x => true))
                listTeachers.Items.Add(string.Format("{0} {1}", item.FirstName, item.LastName));
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != string.Empty && lastName.Text != string.Empty)
            {
                try
                {
                    teachers.Repository.Add(new Teacher() { FirstName = firstName.Text, LastName = lastName.Text });
                    teachers.Save();
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
                }
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
                        int id = teachers.Repository.GetAll(x=>listTeachers.SelectedItem.ToString().Contains(x.FirstName) && listTeachers.SelectedItem.ToString().Contains(x.LastName)).FirstOrDefault().Id;
                        teachers.Repository.Delete(id);
                        teachers.Save();
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
    }
}
