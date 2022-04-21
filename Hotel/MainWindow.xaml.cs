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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }
       
        public void Load()
        {
            using (db = new ApplicationContext())
            {
                db.Reservations.Load();
                db.RoomNumbers.Load();
                db.ClientLists.Load();
                db.RoomClasses.Load();
                db.Genders.Load();
                dataGridRes.ItemsSource = db.Reservations.Local.ToBindingList();
                dataGridRoom.ItemsSource = db.RoomNumbers.Local.ToBindingList();
                dataGridClient.ItemsSource = db.ClientLists.Local.ToBindingList();
            }
        }
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                Gender gender = new Gender
                {
                    GenderName = "Жіноча"
                };
                int? keygender = db.Genders.SingleOrDefault(x => x.GenderName == gender.GenderName)?.IdGender;
                if (keygender == null)
                {
                    db.Genders.Add(gender);
                    db.SaveChanges();
                }
                ClientList clientList = new ClientList
                {
                    FullNameClient = "qw",
                    PassportID = 123,
                    GenderId = gender.IdGender
                };
                if (keygender != null)
                {
                    clientList.GenderId = (int)keygender;
                    db.SaveChanges();
                }
                db.Add(clientList);
                db.SaveChanges();
                RoomClass roomClass = new RoomClass
                {
                    Class = "Бізнес"
                };
                int? keyroomClass = db.RoomClasses.SingleOrDefault(x => x.Class == roomClass.Class)?.IdClass;
                if (keyroomClass == null)
                {
                    db.RoomClasses.Add(roomClass);
                    db.SaveChanges();
                }
                RoomNumber roomNumber = new RoomNumber
                {
                    CodeNumber = 1,
                    RoomClassId = roomClass.IdClass,
                    Price = 2000
                };
                if (keyroomClass != null)
                {
                    roomNumber.RoomClassId = (int)keyroomClass;
                    db.SaveChanges();
                }
                db.Add(roomNumber);
                db.SaveChanges();
                Reservation reservation = new Reservation
                {
                    ClientListId = clientList.IdClient,
                    RoomNumberId = roomNumber.IdRoom
                };
                db.Add(reservation);
                db.SaveChanges();

                Load();
            }
        }

        private void btn_UpdateReserv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    int IdLib = (dataGridRes.SelectedItem as Reservation).IdReservation;

                    Reservation updateRes = (from d in db.Reservations
                                           where d.IdReservation == IdLib
                                           select d).Single();

                    updateRes.ClientListId = (dataGridRes.SelectedItem as Reservation).ClientListId;
                    updateRes.RoomNumberId = (dataGridRes.SelectedItem as Reservation).RoomNumberId; 
                    updateRes.DateReservation = (dataGridRes.SelectedItem as Reservation).DateReservation; 
                    
                    updateRes.DateOfEntry = (dataGridRes.SelectedItem as Reservation).DateOfEntry; 
                    updateRes.DateDeparture = (dataGridRes.SelectedItem as Reservation).DateDeparture;
                    
                    db.SaveChanges();
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    int IdRoom = (dataGridRoom.SelectedItem as RoomNumber).IdRoom;
                    int IdClass = (dataGridRoom.SelectedItem as RoomNumber).RoomClass.IdClass;

                    RoomNumber updateRoom = (from d in db.RoomNumbers
                                             where d.IdRoom == IdRoom
                                             select d).Single();
                    RoomClass updateroomClass = (from d in db.RoomClasses
                                                 where d.IdClass == IdClass
                                                 select d).Single();
                    updateRoom.CodeNumber = (dataGridRoom.SelectedItem as RoomNumber).CodeNumber;
                    updateroomClass.Class = (dataGridRoom.SelectedItem as RoomNumber).RoomClass.Class;
                    updateRoom.Price = (dataGridRoom.SelectedItem as RoomNumber).Price;
                    db.SaveChanges();
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    int IdClient = (dataGridClient.SelectedItem as ClientList).IdClient;
                    int IdGender = (dataGridClient.SelectedItem as ClientList).Gender.IdGender;

                    ClientList updateClient = (from d in db.ClientLists
                                             where d.IdClient == IdClient
                                             select d).Single();
                    Gender updateGender = (from d in db.Genders
                                                 where d.IdGender == IdGender
                                                 select d).Single();
                    updateClient.FullNameClient = (dataGridClient.SelectedItem as ClientList).FullNameClient;                    
                    updateClient.DateOfBirth = (dataGridClient.SelectedItem as ClientList).DateOfBirth;        
                    updateClient.PassportID = (dataGridClient.SelectedItem as ClientList).PassportID;
                    updateGender.GenderName = (dataGridClient.SelectedItem as ClientList).Gender.GenderName;
                    db.SaveChanges();
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_DeleteRes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    Reservation delRes = new Reservation() { IdReservation = int.Parse(tb_DelRes.Text) };
                    db.Reservations.Attach(delRes);
                    db.Reservations.Remove(delRes);
                    db.SaveChanges();
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_UpdateRoom1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    RoomNumber delRoom = new RoomNumber() { IdRoom = int.Parse(tb_DelRoom.Text) };
                    db.RoomNumbers.Attach(delRoom);
                    db.RoomNumbers.Remove(delRoom);
                    db.SaveChanges();
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    ClientList delClient = new ClientList() { IdClient = int.Parse(tb_DelClient.Text) };
                    db.ClientLists.Attach(delClient);
                    db.ClientLists.Remove(delClient);
                    db.SaveChanges();
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        ////    var lib = db.Librarys.Include(p => p.AuthorTb.NameAuthor);

        ////    db.Librarys.Load();
        ////    db.Authors.Load();
        ////    dataGrid.ItemsSource = lib.ToList();
        //    if (comboBox.SelectedIndex == 0)
        //    {
        //        try
        //        {
        //            using (db = new ApplicationContext())
        //            {
        //                var comps = db.Reservations.FromSqlRaw("SELECT IdReservation, ClientListId FROM [Reservations]");

        //                //var q = db.Reservations.Include(x => x.ClientList).Where(x => x.ClientListId == x.ClientList.IdClient); 
        //                //db.Reservations.Load();
        //                //db.RoomNumbers.Load();
        //                //db.ClientLists.Load();
        //                //db.RoomClasses.Load();
        //                dataGridRes.ItemsSource = comps.ToList();
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    if (comboBox.SelectedIndex == 1)
        //    {
        //        using (db = new ApplicationContext())
        //        {
        //            db.RoomNumbers.Load();
        //            dataGridRes.ItemsSource = db.RoomNumbers.Local.ToBindingList();
        //        }
        //    }
        //    if (comboBox.SelectedIndex == 2)
        //    {
        //        using (db = new ApplicationContext())
        //        {
        //            db.ClientLists.Load();
        //            dataGridRes.ItemsSource = db.ClientLists.Local.ToBindingList();
        //        }
        //    }


        //}
    }
}
