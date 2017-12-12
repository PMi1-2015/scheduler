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
    /// Interaction logic for RoomManagerWindow.xaml
    /// </summary>
    public partial class RoomManagerWindow : Window
    {
        IUnitOfWork<Room> rooms;
        bool edit;
        
        public RoomManagerWindow()
        {
            InitializeComponent();
            edit = false;
            rooms = new UnitOfWork();
            foreach (var item in rooms.Repository.GetAll(x => true))
            {
                listRoom.Items.Add(item.Number);
            }
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addRoom_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            if (roomNumber.Text != string.Empty && Int32.TryParse(roomNumber.Text, out number) && !edit)
            {
                AddRoom(number);
            }
            else if(edit && roomNumber.Text != string.Empty && Int32.TryParse(roomNumber.Text, out number))
            {
                EditRoom(number);
            }
            else MessageBox.Show("Input correct number.");
        }

        private void deleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (listRoom.SelectedItem != null)
            {
                try
                {
                    int id = rooms.Repository.GetAll(x => x.Number == (int)listRoom.SelectedItem).FirstOrDefault().Id;
                    rooms.Repository.Delete(id);
                    rooms.Save();
                    listRoom.Items.Remove(listRoom.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cant delete this room.");
                }
            }
            else MessageBox.Show("Select room in list.");
        }

        private void editRoom_Click(object sender, RoutedEventArgs e)
        {
            if (listRoom.SelectedItem != null)
            {
                edit = true;
                roomNumber.Text = listRoom.SelectedItem.ToString();
                addRoom.Content = "Edit";
            }
            else MessageBox.Show("Select room in list");
        }

        private void EditRoom(int number)
        {
            try
            {
                var room = rooms.Repository.GetAll(x => x.Number == (int)listRoom.SelectedItem).FirstOrDefault();
                room.Number = number;
                rooms.Repository.Update(room);
                rooms.Save();
                listRoom.Items[listRoom.SelectedIndex] = number;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing room");
            }
            finally
            {
                roomNumber.Text = string.Empty;
                edit = false;
                addRoom.Content = "Add";
            }
        }

        private void AddRoom(int number)
        {
            try
            {
                rooms.Repository.Add(new Room() { Number = number });
                rooms.Save();
                listRoom.Items.Add(number);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding room.");
            }
            finally
            {
                roomNumber.Text = string.Empty;
            }
        }
    }
}
