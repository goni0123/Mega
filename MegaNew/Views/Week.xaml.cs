using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MegaNew.DocView;
using UserControl = System.Windows.Controls.UserControl;

namespace MegaNew.Views
{
    /// <summary>
    /// Interaction logic for Week.xaml
    /// </summary>
    public partial class Week : UserControl
    {
        readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        public Week()
        {
            InitializeComponent();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Inland_week ORDER BY Week_id DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        weekData.ItemsSource = dt.DefaultView;
                    }
                    using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Weeks ORDER BY Week_id DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        we.ItemsSource = dt.DefaultView;
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("No rows are present\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Extract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string week = Weeks.Text;
                    string from = From.Text;
                    string to = To.Text;
                    int[] n = new int[20];
                    using (SqlCommand com = new SqlCommand("SELECT DISTINCT Nalog_nr_inland FROM Inland_driving_more WHERE Data BETWEEN @From AND @To", connection))
                    {
                        com.Parameters.AddWithValue("@To", to);
                        com.Parameters.AddWithValue("@From", from);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 0;
                            while (reader.Read() && i < 20)
                            {
                                n[i] = reader.GetInt32(0);
                                i++;
                            }
                            reader.Close();
                        }
                    }
                    using (SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM Weeks WHERE Week_id=@Nal", connection))
                    {
                        com.Parameters.AddWithValue("Nal", week);
                        int count = (int)com.ExecuteScalar();
                        if (count > 0)
                        {

                        }
                        else
                        {
                            using (SqlCommand comm = new SqlCommand("INSERT INTO Weeks(week_id) values(@Week)", connection))
                            {
                                comm.Parameters.AddWithValue("@Week", week);
                                comm.ExecuteNonQuery();
                            }
                            for (int j = 0; j < 8; j++)
                            {
                                using (SqlCommand comm = new SqlCommand("INSERT INTO Inland_week(Nalog_nr, Invoice_Inland, Invoice_Inland_Attachment, Check_inl, Week_id) SELECT Nalog_nr_inland, Invoice_Inland, Invoice_Inland_Attachment, Check_inl, @Week as Week_id FROM Inland_driving where Nalog_nr_inland = @Nal", connection))
                                {
                                    comm.Parameters.AddWithValue("@Week", week);
                                    comm.Parameters.AddWithValue("@Nal", n[j]);
                                    comm.ExecuteNonQuery();
                                }
                            }
                            for (int y = 0; y < 8; y++)
                            {
                                using (SqlCommand comp = new SqlCommand("INSERT INTO Inland_week_more(Nalog_nr, Data, KM_ind, Trailor, Truck, City_out, Week_id) SELECT Nalog_nr_inland, Data, KM_ind, Trailor, Truck, City_out ,@Week as Week_id FROM Inland_driving_more  where Nalog_nr_inland=@Nal", connection))
                                {
                                    comp.Parameters.AddWithValue("@Week", week);
                                    comp.Parameters.AddWithValue("@Nal", n[y]);
                                    comp.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Inland_week ORDER BY Week_id DESC", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        weekData.ItemsSource = dt.DefaultView;
                    }
                    using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Weeks ORDER BY Week_id DESC", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        we.ItemsSource = dt.DefaultView;
                    }
                    connection.Close();
                    for (int i = 0; i < n.Length; i++)
                    {
                        n[i] = 0;
                    }
                }
            }
            catch
            {
                string week = Weeks.Text;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("DELETE From Inland_week_more Where Week_id=@Week", connection))
                    {
                        com.Parameters.AddWithValue("@Week", week);
                        com.ExecuteNonQuery();
                    }
                }
                System.Windows.MessageBox.Show("Problem may be in the Week if it exists", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string week = Weeks.Text;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("DELETE From Inland_week_more Where Week_id=@Week", connection))
                    {
                        com.Parameters.AddWithValue("@Week", week);
                        com.ExecuteNonQuery();
                    }
                    using (SqlCommand com = new SqlCommand("DELETE From Inland_week Where Week_id=@Week", connection))
                    {
                        com.Parameters.AddWithValue("@Week", week);
                        com.ExecuteNonQuery();
                    }
                    using (SqlCommand com = new SqlCommand("DELETE From Weeks Where Week_id=@Week", connection))
                    {
                        com.Parameters.AddWithValue("@Week", week);
                        com.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Inland_week ORDER BY Week_id DESC", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        weekData.ItemsSource = dt.DefaultView;
                    }
                    using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Weeks ORDER BY Week_id DESC", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        we.ItemsSource = dt.DefaultView;
                    }
                    connection.Close();
                }
            }
            catch
            {

                System.Windows.MessageBox.Show("The nalog can't be deleted it dosent exit\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string week = Weeks.Text;
                    if (week == "")
                    {
                        using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Inland_week ORDER BY Week_id DESC", connection))
                        {
                            SqlDataAdapter sda = new SqlDataAdapter(command);
                            DataTable dt = new DataTable("Nalog");
                            sda.Fill(dt);
                            weekData.ItemsSource = dt.DefaultView;
                        }
                        using (SqlCommand command = new SqlCommand("SELECT TOP (100) * FROM Weeks ORDER BY Week_id DESC", connection))
                        {
                            SqlDataAdapter sda = new SqlDataAdapter(command);
                            DataTable dt = new DataTable("Nalog");
                            sda.Fill(dt);
                            we.ItemsSource = dt.DefaultView;
                        }
                    }
                    else
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Inland_week Where Week_id=@Week", connection))
                        {
                            command.Parameters.AddWithValue("@Week", week);
                            SqlDataAdapter sda = new SqlDataAdapter(command);
                            DataTable dt = new DataTable("Nalog");
                            sda.Fill(dt);
                            weekData.ItemsSource = dt.DefaultView;
                        }
                    }
                    connection.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Problem may be in the Week if it doesn't exists", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string week = Weeks.Text;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT MAX(Week_id) AS data FROM Weeks Where Week_id=@Week", connection))
                    {
                        command.Parameters.AddWithValue("@Week", week);
                        int data = (int)command.ExecuteScalar();

                        System.Windows.MessageBox.Show("The last Order Number Inserted is: " + data);
                    }
                    connection.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Problem may be in the Week if it doesn't exists", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void weekData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (weekData.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)weekData.SelectedItem;
                    string n = row["Nalog_nr"].ToString();
                    string w= row["Week_id"].ToString();
                    int nid = int.Parse(n);
                    int wid = int.Parse(w);
                    We OUT = new We(nid,wid);
                    OUT.Show();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Invalid Colum\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}

