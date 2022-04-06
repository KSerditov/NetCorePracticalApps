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
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace ResponsiveWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true;MultipleActiveResultSets=true;Encrypt=false";
        private const string query = "WAITFOR DELAY '00:00:10';SELECT EmployeeId, FirstName, LastName FROM Employees";

        public MainWindow()
        {
            InitializeComponent();
        }

        public void GetEmployeesSyncButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new(connString))
            {
                conn.Open();
                SqlCommand sql = new(query, conn);
                SqlDataReader cmd = sql.ExecuteReader();
                while (cmd.Read())
                {
                    string employee = String.Format("{0} {1} {2}", cmd.GetInt32(0), cmd.GetString(1), cmd.GetString(2));
                    EmployeeListBox.Items.Add(employee);
                }
                cmd.Close();
            }
        }

        public async void GetEmployeesAsyncButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new(connString))
            {
                await conn.OpenAsync();
                SqlCommand sql = new(query, conn);
                SqlDataReader cmd = await sql.ExecuteReaderAsync();
                while (await cmd.ReadAsync())
                {
                    string employee = String.Format(
                        "{0} {1} {2}",
                        await cmd.GetFieldValueAsync<int>(0),
                        await cmd.GetFieldValueAsync<string>(1),
                        await cmd.GetFieldValueAsync<string>(2)
                    );
                    EmployeeListBox.Items.Add(employee);
                }
                await cmd.CloseAsync();
            }
        }
    }
}
