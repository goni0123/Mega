using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using CheckBox = System.Windows.Controls.CheckBox;
using ComboBox = System.Windows.Controls.ComboBox;
using DataGrid = System.Windows.Controls.DataGrid;
using TextBox = System.Windows.Controls.TextBox;

namespace MegaNew.ViewModels
{
    public class InlandEditViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        /// <summary>
        /// getting file path
        /// </summary>
        /// <returns></returns>
        public OpenFileDialog FilePathReturn()
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            return dlg;
        }
        /// <summary>
        /// For loading a file with teh file path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Process ProcessReturn(string Path)
        {
            return Process.Start(Path);
        }
        public void LoadData(int Nalog, DataGrid Loading, ComboBox truck, ComboBox trailor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Inland_driving_more where Nalog_nr_inland=@Nalog Order By ID Desc ", con))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        Loading.ItemsSource = dt.DefaultView;
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Truck", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            truck.Items.Add(reader["truck"].ToString());
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Trailor", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            trailor.Items.Add(reader["trailor"].ToString());
                        }
                        reader.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the route:\n\n{ex.Message}");
            }
        }
        public void Fill(int Nalog, TextBox invoice, TextBox ina)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Inland_driving where Nalog_nr_inland=@Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Invoice = reader["Invoice_inland"].ToString();
                                string InvoiceA = reader["Invoice_Inland_Attachment"].ToString();
                                invoice.Text = Invoice;
                                ina.Text = InvoiceA;
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while loading:\n\n{ex.Message}");
            }
        }
        public void FillTextboxesFromDataGrid(DataGrid loadingDataGrid, DatePicker date, TextBox km, ComboBox truck, ComboBox trailor, TextBox route)
        {
            if (loadingDataGrid.SelectedItem != null)
            {
                DataRowView selectedItem = (DataRowView)loadingDataGrid.SelectedItem;

                int id = Convert.ToInt32(selectedItem["ID"]);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("SELECT * FROM Inland_driving_more WHERE ID=@ID Order By ID Desc", connection))
                        {
                            command.Parameters.AddWithValue("@ID", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string Date = reader["Data"].ToString();
                                    string Km = reader["KM_ind"].ToString();
                                    string Truck = reader["Truck"].ToString();
                                    string Trailor = reader["Trailor"].ToString();
                                    string Route = reader["City_out"].ToString();
                                    date.Text = Date;
                                    km.Text = Km;
                                    truck.Text = Truck;
                                    trailor.Text = Trailor;
                                    route.Text = Route;
                                }
                                reader.Close();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while loading data:\n\n{ex.Message}");
                }
            }
        }
        public void DeleteMore(int Nalog, int Id, DataGrid Loading)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Inland_driving_more WHERE ID = @id", con))
                    {
                        command.Parameters.AddWithValue("@id", Id);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Inland_driving_more where Nalog_nr_inland=@Nalog Order By ID Desc ", con))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        Loading.ItemsSource = dt.DefaultView;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the route:\n\n{ex.Message}");
            }
        }
        public void MoreInsertData(int Nalog, int ID, DataGrid loading, DatePicker date, TextBox km, ComboBox truck, ComboBox trailor, TextBox route)
        {
            string Data = date.Text;
            string KM = km.Text;
            string Truck = truck.Text;
            string Trailor = trailor.Text;
            string City = route.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Inland_driving_more WHERE ID=@ID", connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID", ID);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Inland_driving_more SET Data=@Data,KM_ind=@Km,Trailor=@Trailor,Truck=@Truck,City_out=@City WHERE ID=@ID", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", ID);
                            updateCommand.Parameters.AddWithValue("@Data", Data);
                            updateCommand.Parameters.AddWithValue("@Km", string.IsNullOrEmpty(KM) ? 0 : Convert.ToInt32(KM));
                            updateCommand.Parameters.AddWithValue("@Trailor", Trailor);
                            updateCommand.Parameters.AddWithValue("@Truck", Truck);
                            updateCommand.Parameters.AddWithValue("@City", City);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand insertCommand = new SqlCommand("INSERT INTO Inland_driving_more (Nalog_nr_inland,Data,KM_ind,Trailor,Truck,City_out) VALUES (@Nalog,@Data,@Km,@Trailor,@Truck,@City)", connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Nalog", Nalog);
                            insertCommand.Parameters.AddWithValue("@Data", Data);
                            insertCommand.Parameters.AddWithValue("@Km", string.IsNullOrEmpty(KM) ? 0 : Convert.ToInt32(KM));
                            insertCommand.Parameters.AddWithValue("@Trailor", Trailor);
                            insertCommand.Parameters.AddWithValue("@Truck", Truck);
                            insertCommand.Parameters.AddWithValue("@City", City);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    using (SqlCommand co = new SqlCommand("SELECT * FROM Inland_driving_more where Nalog_nr_inland=@Nalog Order By ID Desc", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", Nalog);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        loading.ItemsSource = dt.DefaultView;
                    }
                }

                connection.Close();
            }
        }
        public void UpdateData(int Nalog, TextBox invoice, TextBox ina, CheckBox doneCheckBox)
        {
            bool doneChecked = doneCheckBox.IsChecked ?? false;
            string Invoice = invoice.Text;
            string Ina = ina.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Inland_driving SET Invoice_inland=@Invoice,Invoice_Inland_Attachment=@Ina,Check_inl = @Check WHERE Nalog_nr_inland = @Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        command.Parameters.AddWithValue("@Check", doneChecked ? 1 : 0);
                        command.Parameters.AddWithValue("@Invoice", Invoice);
                        command.Parameters.AddWithValue("@Ina", Ina);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                System.Windows.MessageBox.Show("Update successful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the data:\n\n" + ex.Message);
            }
        }
    }
}
