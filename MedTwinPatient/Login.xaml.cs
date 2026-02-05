using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace MedTwinPatient
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public bool CheckToLogin()
        {
            string connectionString = "Data Source=LOCDATE\\MEDTWINPATIENT; Database=master; Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Email, Password FROM Doctor", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"]?.ToString() == loginTextBox.Text)
                        {
                            if (reader["Password"]?.ToString() == passwordTextBox.Text)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            if (CheckToLogin())
            {
                mainWin.Show();
                this.Close();
            }
        }
    }
}
