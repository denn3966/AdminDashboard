using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows;

namespace HydacAdminPage
{
    public class ViewModel : INotifyPropertyChanged
    {
        //EMPLOYEE

        public List<Guest> guests { get; set; } = new List<Guest>();
        public List<Employee> employees { get; set; } = new List<Employee>();

        private string ConnectionString = "Server=10.56.8.32;Database=A_GRUPEDB02_2019;User Id=A_GRUPE02;Password=A_OPENDB02";
        public ViewModel()
        {
            string nowTime = DateTime.Now.ToString("yyyy-dd-MM");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                //Selects all from Employee tabel in database
                string query = "SELECT DISTINCT CheckIn.Id AS CheckIn_Id, Employee.Id, Employee.Name, Employee.Email, Employee.MobilePhone, Employee.LandlinePhone, Employee.Initials, Employee.PinCode, Role.Title AS RoleTitle, Department.Title AS DepartmentTitle FROM Employee INNER JOIN Role ON Employee.Role_Id = Role.Id INNER JOIN Department ON Role.Department_Id = Department.Id INNER JOIN CheckIn ON CheckIn.Employee_Id = Employee.Id WHERE CheckIn.FromDateTime >= '" + nowTime + "' AND CheckIn.ToDatetime = '1900-01-01';";

                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        //Adds relavent column to properties in Employee object. Then adds an employee to employees list.
                        employee.Name = reader.GetString("Name");
                        employee.Initials = reader.GetString("Initials");
                        employee.LandlinePhone = reader.GetString("LandlinePhone");
                        employee.PinCode = reader.GetString("PinCode");
                        employee.Email = reader.GetString("Email");
                        employee.Role = reader.GetString("RoleTitle");
                        employee.MobilePhone = reader.GetString("MobilePhone");
                        employee.Department = reader.GetString("DepartmentTitle");
                        employee.Id = reader.GetInt32("Id");
                        employee.CheckIn_Id = reader.GetInt32("CheckIn_Id");
                        employees.Add(employee);
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                //Selects all from Employee tabel in database
                string query = "SELECT DISTINCT Guest.Id AS Id, Name, Email, Company, CheckIn.Id AS CheckIn_Id FROM Guest INNER JOIN CheckIn ON CheckIn.Guest_Id = Guest.Id WHERE CheckIn.FromDateTime >= '" + nowTime + "' AND CheckIn.ToDatetime = '1900-01-01';";

                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guest guest = new Guest();

                        //Adds relavent column to properties in Employee object. Then adds an employee to employees list.
                        guest.Id = reader.GetInt32(0);
                        guest.Name = reader.GetString(1);
                        guest.Email = reader.GetString(2);
                        guest.Company = reader.GetString(3);
                        guest.CheckIn_Id = reader.GetInt32(4);
                        guests.Add(guest);
                    }
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
