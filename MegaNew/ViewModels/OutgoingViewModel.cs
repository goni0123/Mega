using MegaNew.DocView;
using MegaNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MegaNew.Views;

namespace MegaNew.ViewModels
{
    public class OutgoingViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        private ObservableCollection<OutgoingModel> outgoingData;
        private List<OutgoingModel> filteredData;

        public ObservableCollection<OutgoingModel> OutgoingData
        {
            get { return outgoingData; }
            set { outgoingData = value; }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                FilterData(searchText);
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PrintCommand { get; set; }

        public OutgoingViewModel()
        {
            try
            {
                TestConnection();
                LoadData();
                InitializeCommands();
            }
            catch (Exception ex)
            {
                string errorMessage = "The server is currently not working. " + ex.Message;
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TestConnection()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("Unable to open the SQL connection.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Connection failed: " + ex.Message);
            }
        }

        public void LoadData()
        {
            outgoingData = new ObservableCollection<OutgoingModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Top(250)  Nalog_nr_out, Truck_out, RIT_out, Invoice_out, Driver_out,Check_out FROM Outgoing_MK_NL ORDER BY Nalog_nr_out DESC";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        OutgoingModel outgoing = new OutgoingModel
                        {
                            NalogNr = reader.IsDBNull(reader.GetOrdinal("Nalog_nr_out")) ? 0 : reader.GetInt32(reader.GetOrdinal("Nalog_nr_out")),
                            Truck = reader.IsDBNull(reader.GetOrdinal("Truck_out")) ? string.Empty : reader.GetString(reader.GetOrdinal("Truck_out")),
                            Rit = reader.IsDBNull(reader.GetOrdinal("RIT_out")) ? string.Empty : reader.GetString(reader.GetOrdinal("RIT_out")),
                            Invoice = reader.IsDBNull(reader.GetOrdinal("Invoice_out")) ? string.Empty : reader.GetString(reader.GetOrdinal("Invoice_out")),
                            Driver = reader.IsDBNull(reader.GetOrdinal("Driver_out")) ? string.Empty : reader.GetString(reader.GetOrdinal("Driver_out")),
                            Check = reader.IsDBNull(reader.GetOrdinal("Check_out")) ? false : reader.GetBoolean(reader.GetOrdinal("Check_out"))
                        };

                        OutgoingData.Add(outgoing);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data: " + ex.Message);
                }
            }

            filteredData = new List<OutgoingModel>(outgoingData);
        }
        public void FilterData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                OutgoingData.Clear();
                foreach (var item in filteredData)
                {
                    OutgoingData.Add(item);
                }
            }
            else
            {
                OutgoingData.Clear();
                foreach (var item in filteredData)
                {
                    if (item.NalogNr.ToString().Contains(searchText) ||
                        item.Truck.Contains(searchText) ||
                        item.Rit.Contains(searchText) ||
                        item.Invoice.Contains(searchText) ||
                        item.Driver.Contains(searchText))
                    {
                        OutgoingData.Add(item);
                    }
                }
            }
        }

        private void InitializeCommands()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            AddCommand = new RelayCommand<string>(ExecuteAdd);
            DeleteCommand = new RelayCommand<int>(ExecuteDelete);
            PrintCommand = new RelayCommand<int>(ExecutePrint);
        }
        private void ExecuteSearch()
        {
            FilterData(SearchText);
        }
        private void ExecuteAdd(string nalog)
        {
            try
            {
                bool nalogExists = CheckIfNalogExists(nalog);
                if (!nalogExists)
                { 
                    if (int.TryParse(nalog, out int nalogNr))
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand("INSERT INTO Outgoing_MK_NL (Nalog_nr_out) VALUES (@Nalog)", connection))
                            {
                                command.Parameters.AddWithValue("@Nalog", nalogNr);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Data inserted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            OutgoingModel outgoing = new OutgoingModel
                            {
                                NalogNr = nalogNr,
                                Truck = string.Empty,
                                Rit = string.Empty,
                                Invoice = string.Empty,
                                Driver = string.Empty,
                                Check = false
                            };

                            OutgoingData.Insert(0, outgoing);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Nalog number. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The Nalog already exists. Please update the data.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while inserting the data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteDelete(int nalogNr)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the Nalog and associated records?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand deleteLoadingCommand = new SqlCommand("DELETE FROM Loading_Company_out WHERE Nalog_nr_out = @Nalog", connection))
                        {
                            deleteLoadingCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteLoadingCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand deleteRouteCommand = new SqlCommand("DELETE FROM Route_out WHERE Nalog_nr_out = @Nalog", connection))
                        {
                            deleteRouteCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteRouteCommand.ExecuteNonQuery();
                        }

                        using (SqlCommand deleteIncomingCommand = new SqlCommand("DELETE FROM Outgoing_MK_NL WHERE Nalog_nr_out = @Nalog", connection))
                        {
                            deleteIncomingCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteIncomingCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Nalog and associated records deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        connection.Close();
                        OutgoingModel itemToRemove = outgoingData.FirstOrDefault(item => item.NalogNr == nalogNr);
                        if (itemToRemove != null)
                        {
                            outgoingData.Remove(itemToRemove);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the Nalog: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool CheckIfNalogExists(string nalog)
        {
            bool exists = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Outgoing_MK_NL WHERE Nalog_nr_out = @Nalog", connection))
                {
                    command.Parameters.AddWithValue("@Nalog", nalog);
                    int count = (int)command.ExecuteScalar();
                    exists = count > 0;
                }
                connection.Close();
            }
            return exists;
        }

        private void ExecutePrint(int nalog)
        {
            try
            {
                Out Out = new Out(nalog);
                Out.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

