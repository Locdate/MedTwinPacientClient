using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedTwinPatient
{
    public partial class MainWindow : Window
    {
        public List<Patient> patients = new List<Patient>();
        public List<string> patientDataList = new List<string>();
        
        public MainWindow()
        {
            InitializeComponent();
            LoadDataInListBox();
        }
        public void LoadDataInListBox()
        {
            string connectionString = "Data Source=LOCDATE\\MEDTWINPATIENT; Database=master; Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name, Surname, LastName, GenderID, Birthday, Height, Weight FROM Patient", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Patient patient = new Patient(reader["Name"]?.ToString() ?? "", reader["Surname"]?.ToString() ?? "", reader["LastName"]?.ToString() ?? "", Convert.ToInt32(reader["GenderID"]?.ToString() ?? ""), Convert.ToDateTime(reader["Birthday"]?.ToString() ?? ""), Convert.ToInt32(reader["Height"]?.ToString() ?? ""), Convert.ToInt32(reader["Weight"].ToString() ?? ""));
                        patients.Add(patient);
                    }
                }
                for (int i = 0; i < patients.Count; i++)
                {
                    patientDataList.Add(patients[i].name.ToCharArray()[0].ToString() + ". " + patients[i].lastName.ToCharArray()[0].ToString() + ". " + patients[i].surname);
                }
                listBoxData.ItemsSource = patientDataList;           
            }
        }
        public int GetSQLRowCount(string _connectionString, string _sqlCommand)
        {
            int rowCount;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(_sqlCommand, conn))
                {
                    rowCount = (int)cmd.ExecuteScalar();
                }
            }
            return rowCount;
        }

        private void listBoxData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            name.Content = patients[((ListBox)sender).SelectedIndex].name;
            lastName.Content = patients[((ListBox)sender).SelectedIndex].lastName;
            surname.Content = patients[((ListBox)sender).SelectedIndex].surname;
            birthday.Content = patients[((ListBox)sender).SelectedIndex].birthday;
            age.Content = patients[((ListBox)sender).SelectedIndex].age + " лет";
            height.Content = patients[((ListBox)sender).SelectedIndex].height + " см";
            weight.Content = patients[((ListBox)sender).SelectedIndex].weight + " кг";
        }
    }
}