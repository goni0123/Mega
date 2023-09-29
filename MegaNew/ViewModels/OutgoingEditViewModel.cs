using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;
using DataGrid = System.Windows.Controls.DataGrid;
using ComboBox = System.Windows.Controls.ComboBox;
using System.Threading;
using CheckBox = System.Windows.Controls.CheckBox;
namespace MegaNew.ViewModels
{
    public class OutgoingEditViewModel
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
        /// <summary>
        /// For Inserting the route
        /// </summary>
        /// <param name="Nalog"></param>
        /// <param name="City"></param>
        /// <param name="Trailor"></param>
        /// <param name="Route"></param>
        /// <exception cref="Exception"></exception>
        public void InsertRoute(int Nalog, string City, string Trailor, DataGrid Route)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Route_Out (Nalog_nr_out, City_out, Trailor_out) VALUES" +
                        "(@Nalog, @City, @Trailor)", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        command.Parameters.AddWithValue("@City", City);
                        command.Parameters.AddWithValue("@Trailor", Trailor);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand co = new SqlCommand("SELECT * FROM Route_Out WHERE Nalog_nr_out = @Nalog ORDER BY RO DESC", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", Nalog);
                        DataTable dt = new DataTable("Route");
                        sda.Fill(dt);
                        Route.ItemsSource = dt.DefaultView;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while inserting the route:\n\n{ex.Message}");
            }
        }
        /// <summary>
        /// For deleting the route
        /// </summary>
        /// <param name="Nalog"></param>
        /// <param name="Route"></param>
        /// <param name="route"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteRoute(int Nalog, int Route, DataGrid route)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Route_Out WHERE RO = @id", con))
                    {
                        command.Parameters.AddWithValue("@id", Route);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand co = new SqlCommand("SELECT * FROM Route_Out WHERE Nalog_nr_out = @Nalog ORDER BY RO DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", Nalog);
                        DataTable dt = new DataTable("Route");
                        sda.Fill(dt);
                        route.ItemsSource = dt.DefaultView;
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the route:\n\n{ex.Message}");
            }
        }
        /// <summary>
        /// Loading the datagrid's
        /// </summary>
        /// <param name="Loading"></param>
        /// <param name="Route"></param>
        /// <param name="truck"></param>
        /// <param name="trailor"></param>
        /// <param name="Nalog"></param>
        /// <exception cref="Exception"></exception>
        public void LoadData(DataGrid Loading, DataGrid Route, ComboBox truck, ComboBox trailor, int Nalog)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT truck FROM Truck", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            truck.Items.Add(reader["truck"].ToString());
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT trailor FROM Trailor", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            trailor.Items.Add(reader["trailor"].ToString());
                        }
                        reader.Close();
                    }
                    using (SqlCommand co = new SqlCommand("SELECT LCO_id,Export_out,Importer_out,Colli_out,KG_out,Nalog_nr_out FROM Loading_Company_out Where Nalog_nr_out=@Nalog ORDER BY LCO_id DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", Nalog);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        Loading.ItemsSource = dt.DefaultView;
                    }
                    using (SqlCommand co = new SqlCommand("SELECT * FROM Route_out Where Nalog_nr_out=@Nalog ORDER BY RO DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", Nalog);
                        DataTable dt = new DataTable("Route");
                        sda.Fill(dt);
                        Route.ItemsSource = dt.DefaultView;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No rows are present\n", ex);
            }
        }
        /// <summary>
        /// For filling the textboxes
        /// </summary>
        /// <param name="Nalog"></param>
        /// <param name="Loading"></param>
        /// <param name="Route"></param>
        /// <param name="trailor"></param>
        /// <param name="truck"></param>
        /// <param name="rit"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <param name="km"></param>
        /// <param name="workday"></param>
        /// <param name="cost"></param>
        /// <param name="invoice"></param>
        /// <param name="comment"></param>
        /// <param name="driver"></param>
        /// <param name="invoiceA"></param>
        /// <param name="commentA"></param>
        /// <param name="costA"></param>
        /// <exception cref="Exception"></exception>
        public void Fill(int Nalog, DataGrid Loading, DataGrid Route, ComboBox trailor, ComboBox truck, TextBox rit, DatePicker sdate, DatePicker edate, TextBox km, TextBox workday, TextBox cost, TextBox invoice, TextBox comment, TextBox driver, TextBox invoiceA, TextBox commentA, TextBox costA)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Outgoing_MK_NL where Nalog_nr_out=@Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Truck = reader["Truck_out"].ToString();
                                string RIT = reader["RIT_out"].ToString();
                                string Start_date = reader["Start_date_out"].ToString();
                                string End_date = reader["End_date_out"].ToString();
                                string KM = reader["KM_out"].ToString();
                                string Wd = reader["Work_days_out"].ToString();
                                string Ex = reader["Extra_Costs_out"].ToString();
                                string Invoice = reader["Invoice_out"].ToString();
                                string Comment = reader["Comment_out"].ToString();
                                string Driver = reader["Driver_out"].ToString();
                                string InA = reader["Invoice_Attachment_out"].ToString();
                                string ComA = reader["Comment_Attachment_out"].ToString();
                                string ExA = reader["Extra_Costs_Attachment_out"].ToString();
                                truck.Text = Truck;
                                rit.Text = RIT;
                                sdate.Text = Start_date;
                                edate.Text = End_date;
                                km.Text = KM;
                                workday.Text = Wd;
                                cost.Text = Ex;
                                invoice.Text = Invoice;
                                comment.Text = Comment;
                                driver.Text = Driver;
                                invoiceA.Text = InA;
                                commentA.Text = ComA;
                                costA.Text = ExA;
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Route_out where Nalog_nr_out=@Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Trailor = reader["Trailor_out"].ToString();
                                trailor.Text = Trailor;
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand("SELECT LCO_id,Export_out,Importer_out,Colli_out,KG_out,Nalog_nr_out FROM Loading_Company_out WHERE Nalog_nr_out=@Nalog ORDER BY LCO_id DESC", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        Loading.ItemsSource = dt.DefaultView;
                    }
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Route_out WHERE Nalog_nr_out=@Nalog ORDER BY RO DESC", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Route");
                        sda.Fill(dt);
                        Route.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the route:\n\n{ex.Message}");
            }
        }
        /// <summary>
        /// For Updating the Nalog
        /// </summary>
        /// <param name="Nalog"></param>
        /// <param name="sdateTextBox"></param>
        /// <param name="edateTextBox"></param>
        /// <param name="truckTextBox"></param>
        /// <param name="ritTextBox"></param>
        /// <param name="workdayTextBox"></param>
        /// <param name="kmTextBox"></param>
        /// <param name="commentTextBox"></param>
        /// <param name="costTextBox"></param>
        /// <param name="invoiceTextBox"></param>
        /// <param name="doneCheckBox"></param>
        /// <param name="costATextBox"></param>
        /// <param name="commentATextBox"></param>
        /// <param name="invoiceATextBox"></param>
        /// <param name="driverTextBox"></param>
        /// <param name="trailorTextBox"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateData(int Nalog, DatePicker sdateTextBox, DatePicker edateTextBox, ComboBox truckTextBox, TextBox ritTextBox, TextBox workdayTextBox, TextBox kmTextBox, TextBox commentTextBox, TextBox costTextBox, TextBox invoiceTextBox, CheckBox doneCheckBox, TextBox costATextBox, TextBox commentATextBox, TextBox invoiceATextBox, TextBox driverTextBox, ComboBox trailorTextBox)
        {
            string sdate = sdateTextBox.Text;
            string edate = edateTextBox.Text;
            string truck = truckTextBox.Text;
            string rit = ritTextBox.Text;
            string workday = workdayTextBox.Text;
            string km = kmTextBox.Text;
            string comment = commentTextBox.Text;
            string cost = costTextBox.Text;
            string invoice = invoiceTextBox.Text;
            bool doneChecked = doneCheckBox.IsChecked ?? false;
            string costA = costATextBox.Text;
            string commentA = commentATextBox.Text;
            string invoiceA = invoiceATextBox.Text;
            string driver = driverTextBox.Text;
            string trailor = trailorTextBox.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Outgoing_MK_NL SET Extra_Costs_Attachment_out = @ExtraA, Comment_Attachment_out = @CommA, Invoice_Attachment_out = @InvoiceA, Truck_out = @Truck, RIT_out = @RIT, Start_date_out = @Sdate, End_date_out = @Edate, KM_out = @Km, Work_days_out = @Wd, Extra_Costs_out = @Ec, Invoice_out = @Invoice, Comment_out = @Comm, Check_out = @Check, Driver_out = @Driver WHERE Nalog_nr_out = @Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        command.Parameters.AddWithValue("@Sdate", sdate);
                        command.Parameters.AddWithValue("@Edate", edate);
                        command.Parameters.AddWithValue("@Truck", truck);
                        command.Parameters.AddWithValue("@RIT", rit);
                        command.Parameters.AddWithValue("@Wd", string.IsNullOrEmpty(workday) ? 0 : Convert.ToInt32(workday));
                        command.Parameters.AddWithValue("@Comm", comment);
                        command.Parameters.AddWithValue("@Km", string.IsNullOrEmpty(km) ? 0 : Convert.ToInt32(km));
                        command.Parameters.AddWithValue("@Ec", cost);
                        command.Parameters.AddWithValue("@Invoice", invoice);
                        command.Parameters.AddWithValue("@Check", doneChecked ? 1 : 0);
                        command.Parameters.AddWithValue("@ExtraA", costA);
                        command.Parameters.AddWithValue("@CommA", commentA);
                        command.Parameters.AddWithValue("@InvoiceA", invoiceA);
                        command.Parameters.AddWithValue("@Driver", driver);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand("UPDATE Route_out SET Trailor_out = @Trailor WHERE Nalog_nr_out = @Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        command.Parameters.AddWithValue("@Trailor", trailor);

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
        /// <summary>
        /// Inserting or updating in Loading Company
        /// </summary>
        /// <param name="lci"></param>
        /// <param name="nalog"></param>
        /// <param name="exp"></param>
        /// <param name="imp"></param>
        /// <param name="coli"></param>
        /// <param name="kg"></param>
        /// <param name="DocA"></param>
        /// <param name="TransA"></param>
        public void LoadingInsertData(int lci, int nalog, TextBox exp, TextBox imp, TextBox coli, TextBox kg, TextBox DocA, TextBox TransA, DataGrid loading)
        {
            string Exp = exp.Text;
            string Imp = imp.Text;
            string Colli = coli.Text;
            string Kg = kg.Text;
            string Doc = DocA.Text;
            string Tran = TransA.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Loading_Company_out WHERE LCO_id=@ID", connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID", lci);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Loading_Company_out SET Export_out=@Exp, Importer_out=@Imp, Colli_out=@Colli, KG_out=@Kg, Document_out=@Doc, Transport_invoice_out=@Trans WHERE LCO_id=@ID", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", lci);
                            updateCommand.Parameters.AddWithValue("@Exp", Exp);
                            updateCommand.Parameters.AddWithValue("@Imp", Imp);
                            updateCommand.Parameters.AddWithValue("@Colli", string.IsNullOrEmpty(Colli) ? 0 : Convert.ToInt32(Colli));
                            updateCommand.Parameters.AddWithValue("@Kg", string.IsNullOrEmpty(Kg) ? 0 : Convert.ToDecimal(Kg));
                            updateCommand.Parameters.AddWithValue("@Doc", Doc);
                            updateCommand.Parameters.AddWithValue("@Trans", Tran);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand insertCommand = new SqlCommand("INSERT INTO Loading_Company_out (Export_out, Importer_out, Colli_out, KG_out, Document_out, Transport_invoice_out, Nalog_nr_out) VALUES (@Exp, @Imp, @Colli, @Kg, @Doc, @Trans, @Nalog)", connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Nalog", nalog);
                            insertCommand.Parameters.AddWithValue("@Exp", Exp);
                            insertCommand.Parameters.AddWithValue("@Imp", Imp);
                            insertCommand.Parameters.AddWithValue("@Colli", string.IsNullOrEmpty(Colli) ? 0 : Convert.ToInt32(Colli));
                            insertCommand.Parameters.AddWithValue("@Kg", string.IsNullOrEmpty(Kg) ? 0 : Convert.ToDecimal(Kg));
                            insertCommand.Parameters.AddWithValue("@Doc", Doc);
                            insertCommand.Parameters.AddWithValue("@Trans", Tran);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    using (SqlCommand co = new SqlCommand("SELECT LCO_id,Export_out,Importer_out,Colli_out,KG_out,Nalog_nr_out FROM Loading_Company_out Where Nalog_nr_out=@Nalog ORDER BY LCO_id DESC", connection))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", nalog);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        loading.ItemsSource = dt.DefaultView;
                    }
                }

                connection.Close();
            }
        }
        /// <summary>
        /// Deleting Loading Company
        /// </summary>
        /// <param name="Nalog"></param>
        /// <param name="Loading"></param>
        /// <param name="loading"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteLoading(int Nalog, int Loading, DataGrid loading)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Loading_Company_out WHERE LCO_id = @id", con))
                    {
                        command.Parameters.AddWithValue("@id", Loading);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand co = new SqlCommand("SELECT LCO_id,Export_out,Importer_out,Colli_out,KG_out,Nalog_nr_out FROM Loading_Company_out Where Nalog_nr_out=@Nalog ORDER BY LCO_id DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(co);
                        co.Parameters.AddWithValue("@Nalog", Nalog);
                        DataTable dt = new DataTable("Loading");
                        sda.Fill(dt);
                        loading.ItemsSource = dt.DefaultView;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the route:\n\n{ex.Message}");
            }
        }
        /// <summary>
        /// Filling the textboxs with data for loading company
        /// </summary>
        /// <param name="loadingDataGrid"></param>
        /// <param name="exp"></param>
        /// <param name="imp"></param>
        /// <param name="coli"></param>
        /// <param name="kgt"></param>
        /// <param name="docA"></param>
        /// <param name="transA"></param>
        public void FillTextboxesFromDataGrid(DataGrid loadingDataGrid, TextBox exp, TextBox imp, TextBox coli, TextBox kgt, TextBox docA, TextBox transA)
        {
            if (loadingDataGrid.SelectedItem != null)
            {
                DataRowView selectedItem = (DataRowView)loadingDataGrid.SelectedItem;

                int id = Convert.ToInt32(selectedItem["LCO_id"]);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT * FROM Loading_Company_out WHERE LCO_id = @ID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", id);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                string exporter = reader["Export_out"].ToString();
                                string importer = reader["Importer_out"].ToString();
                                string colli = reader["Colli_out"].ToString();
                                string kg = reader["KG_out"].ToString();
                                string doc = reader["Document_out"].ToString();
                                string trans = reader["Transport_invoice_out"].ToString();

                                exp.Text = exporter;
                                imp.Text = importer;
                                coli.Text = colli;
                                kgt.Text = kg;
                                docA.Text = doc;
                                transA.Text = trans;
                            }
                            reader.Close();
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"An error occurred while loading data:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /// <summary>
        /// Last City
        /// </summary>
        /// <param name="trailor"></param>
        /// <exception cref="Exception"></exception>
        public void LastCity(ComboBox trailor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string Tra = trailor.Text;
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 City, Trailor FROM Last_Route AS data WHERE Trailor = @tra ORDER BY Last_route DESC", connection))
                    {
                        command.Parameters.AddWithValue("@tra", Tra);
                        string data = (string)command.ExecuteScalar();

                        System.Windows.MessageBox.Show("The last Order Number Inserted is: " + data);
                    }
                    connection.Close();
                }
            }catch (Exception ex)
            {
                throw new Exception("Error accured when getting the city" + ex);
            }
        }
    }
}
