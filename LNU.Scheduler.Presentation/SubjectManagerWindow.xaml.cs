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
    /// Interaction logic for SubjectManagerWindow.xaml
    /// </summary>
    public partial class SubjectManagerWindow : Window
    {
        IUnitOfWork<Subject> subjects;
        public SubjectManagerWindow()
        {
            InitializeComponent();
            subjects = new UnitOfWork();
            foreach (var item in subjects.Repository.GetAll(x => true))
                listSubjects.Items.Add(item.Name);
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            if (subjectText.Text != string.Empty)
            {
                try
                {
                    subjects.Repository.Add(new Subject { Name = subjectText.Text });
                    subjects.Save();
                    listSubjects.Items.Add(subjectText.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding new subject.");
                }
                finally
                {
                    subjectText.Text = string.Empty;
                }
            }
            else MessageBox.Show("Input correct name.");
        }

        private void deleteSubject_Click(object sender, RoutedEventArgs e)
        {
            if (listSubjects.SelectedItem != null)
            {
                try
                {
                    int id = subjects.Repository.GetAll(x => x.Name == (string)listSubjects.SelectedItem).First().Id;
                    subjects.Repository.Delete(id);
                    subjects.Save();
                    listSubjects.Items.Remove(listSubjects.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cant delete this subject");
                }
                finally
                {
                    listSubjects.SelectedItem = null;
                }
            }
            else MessageBox.Show("Select subject in list.");
        }
    }
}
