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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace workshop
{
    public partial class MainWindow : Window
    {
        public static WorshopDB worshopDB;
        public static CurrentUser currentUser;
        public MainWindow()
        {
            InitializeComponent();
            worshopDB = new WorshopDB();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            string login = tbUserName.Text;
            string email = tbEmail.Text;
            string password1 = tbPassword1.Password;
            string password2 = tbPassword2.Password;
            bool dataCorrect = true;

            Regex regexLog = new Regex(@"^[\w\d!*]{1,15}$");
            Regex regexPas = new Regex(@"^[\w\d!*$#]{5,25}$");
            Regex regexEmail = new Regex(@"^[\w\d]{3,20}@[\w\d]{3,6}.[\w]{2,5}$");

            if (regexLog.IsMatch(login))
            {
                tbUserName.Foreground = Brushes.Green;
            }
            else
            {
                tbUserName.Foreground = Brushes.Red;
                dataCorrect = false;
            }

            if (regexEmail.IsMatch(email))
            {
                tbEmail.Foreground = Brushes.Green;
            }
            else
            {
                tbEmail.Foreground = Brushes.Red;
                dataCorrect = false;
            }

            if (regexPas.IsMatch(password1))
            {
                tbPassword1.Foreground = Brushes.Green;
            }
            else
            {
                tbPassword1.Foreground = Brushes.Red;
                dataCorrect = false;
            }

            if (regexPas.IsMatch(password2))
            {
                tbPassword2.Foreground = Brushes.Green;
            }
            else
            {
                tbPassword2.Foreground = Brushes.Red;
                dataCorrect = false;
            }

            if (password1 != password2)
            {
                tbPassword1.Foreground = Brushes.Red;
                tbPassword2.Foreground = Brushes.Red;
                dataCorrect = false;
            }

            if(dataCorrect)
            {
                string sqlExpression = @"INSERT INTO [users](login, password, email)" +
                                       $"VALUES('{tbUserName.Text}', '{tbPassword1.Password}', '{tbEmail.Text}');";
                SqlCommand command = new SqlCommand(sqlExpression, worshopDB.con);
                command.ExecuteNonQuery();
                MessageBox.Show("Запись создана!");
            }
        }

        private void ChangeToLogButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            authorization.Show();
            this.Close();
        }
    }
}
