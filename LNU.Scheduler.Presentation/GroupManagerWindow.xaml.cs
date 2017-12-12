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
    /// Interaction logic for GroupManagerWindow.xaml
    /// </summary>
    public partial class GroupManagerWindow : Window
    {
        IUnitOfWork<Group> groups;
        public GroupManagerWindow()
        {
            InitializeComponent();
            groups = new UnitOfWork();
            foreach (var item in groups.Repository.GetAll(x => true))
                listGroups.Items.Add(item.Name);
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void groupAdd_Click(object sender, RoutedEventArgs e)
        {
            if (groupName.Text != string.Empty)
            {
                try
                {
                    groups.Repository.Add(new Group() { Name = groupName.Text });
                    groups.Save();
                    listGroups.Items.Add(groupName.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding new group");
                }
                finally
                {
                    groupName.Text = string.Empty;
                }
            }
            else MessageBox.Show("Input correct name");
        }

        private void deleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (listGroups.SelectedItem != null)
            {
                try
                {
                    int id = groups.Repository.GetAll(x => x.Name == (string)listGroups.SelectedItem).FirstOrDefault().Id;
                    groups.Repository.Delete(id);
                    groups.Save();
                    listGroups.Items.Remove(listGroups.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cant delete this group");
                }
                finally
                {
                    listGroups.SelectedItem = null;
                }
            }
            else MessageBox.Show("Select group in list");
        }
    }
}
