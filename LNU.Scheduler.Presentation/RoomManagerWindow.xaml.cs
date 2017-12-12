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
        public RoomManagerWindow(IUnitOfWork<Room> data)
        {
            InitializeComponent();
            rooms = data;
            foreach (var item in rooms.Repository.GetAll(x => true))
            {
                listRoom.Items.Add(item.Number);
            }
        }

        public RoomManagerWindow()
        {
            InitializeComponent();
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
            if (roomNumber.Text != string.Empty && Int32.TryParse(roomNumber.Text, out number))
            {
                try
                {
                    rooms.Repository.Add(new Room() { Number = number });
                    rooms.Save();
                    listRoom.Items.Add(number);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error adding room.");
                }
                finally
                {
                    roomNumber.Text = string.Empty;
                }
            } else MessageBox.Show("Input correct number.");
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
    }
}
