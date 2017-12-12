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
        bool edit;
        public GroupManagerWindow()
        {
            InitializeComponent();
            groups = new UnitOfWork();
            foreach (var item in groups.Repository.GetAll(x => true))
                listGroups.Items.Add(item.Name);
            edit = false;
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void groupAdd_Click(object sender, RoutedEventArgs e)
        {            
            if (groupText.Text != string.Empty && !edit)
            {
                AddGroup();
            }
            else if(groupText.Text != string.Empty && edit)
            {
                EditGroup();
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

        private void editGroup_Click(object sender, RoutedEventArgs e)
        {
            if (listGroups.SelectedItem != null)
            {
                edit = true;
                groupText.Text = listGroups.SelectedItem.ToString();
                addGroup.Content = "Edit";
            }
            else MessageBox.Show("Select subject in list");
        }


        private void EditGroup()
        {
            try
            {
                var group = groups.Repository.GetAll(x => x.Name == listGroups.SelectedItem.ToString()).FirstOrDefault();
                group.Name = groupText.Text;
                groups.Repository.Update(group);
                groups.Save();
                listGroups.Items[listGroups.SelectedIndex] = groupText.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing group");
            }
            finally
            {
                groupText.Text = string.Empty;
                edit = false;
                addGroup.Content = "Add";
            }
        }

        private void AddGroup()
        {
            try
            {
                groups.Repository.Add(new Group() { Name = groupText.Text });
                groups.Save();
                listGroups.Items.Add(groupText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding group.");
            }
            finally
            {
                groupText.Text = string.Empty;
            }
        }
    }
}
