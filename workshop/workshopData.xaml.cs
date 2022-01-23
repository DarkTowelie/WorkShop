using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace workshop
{
    /// <summary>
    /// Логика взаимодействия для workshopData.xaml
    /// </summary>
    public partial class WorkshopData : Window
    {
        public void LoadData()
        {
            using(SqlDataAdapter adap = new SqlDataAdapter(@"SELECT * FROM [orders]", MainWindow.worshopDB.con))
            {
                DataTable t = new DataTable();
                adap.Fill(t);
                dgWorkshop.DataContext = t;
            }
        }
        public WorkshopData()
        {
            InitializeComponent();
            tbLogin.Text = MainWindow.currentUser.login;
            LoadData();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.currentUser.id = 0;
            MainWindow.currentUser.login = "";
            Authorization authorization = new Authorization();
            authorization.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            authorization.Show();
            this.Close();
        }

        private void b_addToDB_Click(object sender, RoutedEventArgs e)
        {
            DateTime fixedDate = DateTime.Now.AddDays(5);
            if (!String.IsNullOrEmpty(tb_Employee.Text) &&
               !String.IsNullOrEmpty(tb_Serial.Text))
            {
                string sqlExpression = @"INSERT INTO [orders](serialNumber, userId, fixedDate, employeeSurname, status)" +
                                       $"VALUES('{tb_Serial.Text}', '{MainWindow.currentUser.id}', '{fixedDate.ToString("yyyy-MM-dd h:mm tt")}'," +
                                       $"'{tb_Employee.Text}', 'On diagnostics');";
                SqlCommand command = new SqlCommand(sqlExpression, MainWindow.worshopDB.con);
                command.ExecuteNonQuery();
                LoadData();
            }
        }

        private void b_deleteFromDB_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if(int.TryParse(tb_Id.Text, out id))
            {
                string sqlExpression = @$"DELETE FROM orders WHERE id = {id};";
                SqlCommand command = new SqlCommand(sqlExpression, MainWindow.worshopDB.con);
                command.ExecuteNonQuery();
                LoadData();
            }
        }

        private void b_UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if (int.TryParse(tb_IdUpdate.Text, out id))
            {
                string sqlExpression = @$"UPDATE orders SET status = '{tb_StatusUpdate.Text}' WHERE id = {id};";
                SqlCommand command = new SqlCommand(sqlExpression, MainWindow.worshopDB.con);
                command.ExecuteNonQuery();
                LoadData();
            }
        }
    }
}
