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
using System.Data;
using System.Data.SqlClient;

namespace HydacAdminPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel vmw = new ViewModel();
            DataContext = vmw;
            EmployeeList.ItemsSource = vmw.employees;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //General connection
            string ConnectionString = "Server=10.56.8.32;Database=A_GRUPEDB02_2019;User Id=A_GRUPE02;Password=A_OPENDB02";
            //Todays DateTime
            DateTime dateTime = DateTime.Now;

            //Employee
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                foreach (Employee employee in EmployeeList.SelectedItems)
                {
                    string query = "UPDATE CheckIn SET ToDateTime = '" + dateTime + "' WHERE Employee_Id =" + employee.Id + "AND Checkin.Id = " + employee.CheckIn_Id;

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        int result = command.ExecuteNonQuery();
                    }
                }
                foreach (Guest guest in GuestList.SelectedItems)
                {
                    string query = "UPDATE CheckIn SET ToDateTime = '" + dateTime + "' WHERE Guest_Id =" + guest.Id + "AND Checkin.Id = " + guest.CheckIn_Id;

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        int result = command.ExecuteNonQuery();
                    }
                }
                //Refresh GuestList
                ViewModel vmw1 = new ViewModel();
                GuestList.ItemsSource = vmw1.guests;
                //Refresh EmployeeList
                ViewModel vmw2 = new ViewModel();
                EmployeeList.ItemsSource = vmw2.employees;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Refresh GuestList
            ViewModel vmw1 = new ViewModel();
            GuestList.ItemsSource = vmw1.guests;
            //Refresh EmployeeList
            ViewModel vmw2 = new ViewModel();
            EmployeeList.ItemsSource = vmw2.employees;

        }
    }
}
