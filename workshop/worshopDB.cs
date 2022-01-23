using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace workshop
{
    public struct CurrentUser
    {
        public int id;
        public string login;
    }
    public class WorshopDB
    {
        public SqlConnection con { get; set; }
        public WorshopDB()
        {
            con = new SqlConnection();
            string prPath = AppDomain.CurrentDomain.BaseDirectory;
            prPath = prPath.Substring(0, prPath.Length - 1);
            prPath = System.IO.Directory.GetParent(prPath).FullName;
            prPath = System.IO.Directory.GetParent(prPath).FullName;
            prPath = System.IO.Directory.GetParent(prPath).FullName;
            con.ConnectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={prPath}\LocalWorkshopDB.mdf;Integrated Security=True";
            con.Open();
        }
    }
}
