using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlExpression = @"SELECT * FROM [users]" +
                                    $"WHERE CONVERT(VARCHAR, login) = '{tbLogUserName.Text}'" +
                                    $"AND CONVERT(VARCHAR, password) = '{tbLogPassword1.Password}'";

            SqlCommand cmd = new SqlCommand(sqlExpression, MainWindow.worshopDB.con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read() &&
                (string)reader.GetValue(1) == tbLogUserName.Text &&
                (string)reader.GetValue(2) == tbLogPassword1.Password)
            {
                MainWindow.currentUser.id = (int)reader.GetValue(0);
                MainWindow.currentUser.login = (string)reader.GetValue(1);

                MessageBox.Show("Добро пожаловать!");
                reader.Close();
                WorkshopData workshopData = new WorkshopData();
                workshopData.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                workshopData.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
            
        }
    }
}
