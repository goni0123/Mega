using GalaSoft.MvvmLight.Command;
using MegaNew.DocView;
using MegaNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MegaNew.ViewModels
{
    public class InlandViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        private ObservableCollection<InlandModel> inlandData;
        private List<InlandModel> filteredData;

        public ObservableCollection<InlandModel> InlandData
        {
            get { return inlandData; }
            set { inlandData = value; }
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

        public InlandViewModel()
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
            inlandData = new ObservableCollection<InlandModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Top(250)  * FROM Inland_driving ORDER BY Nalog_nr_inland DESC";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        InlandModel inland= new InlandModel
                        {
                            Nalog = reader.IsDBNull(reader.GetOrdinal("Nalog_nr_inland")) ? 0 : reader.GetInt32(reader.GetOrdinal("Nalog_nr_inland")),
                            Invoice = reader.IsDBNull(reader.GetOrdinal("Invoice_Inland")) ? string.Empty : reader.GetString(reader.GetOrdinal("Invoice_Inland")),
                            Check = reader.IsDBNull(reader.GetOrdinal("Check_inl")) ? false : reader.GetBoolean(reader.GetOrdinal("Check_inl"))
                        };

                        InlandData.Add(inland);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data: " + ex.Message);
                }
            }

            filteredData = new List<InlandModel>(inlandData);
        }
        public void FilterData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                InlandData.Clear();
                foreach (var item in filteredData)
                {
                    InlandData.Add(item);
                }
            }
            else
            {
                InlandData.Clear();
                foreach (var item in filteredData)
                {
                    if (item.Nalog.ToString().Contains(searchText) ||
                        item.Invoice.Contains(searchText))
                    {
                        InlandData.Add(item);
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
                            using (SqlCommand command = new SqlCommand("INSERT INTO Inland_driving (Nalog_nr_inland) VALUES (@Nalog)", connection))
                            {
                                command.Parameters.AddWithValue("@Nalog", nalogNr);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Data inserted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            InlandModel inland= new InlandModel
                            {
                                Nalog = nalogNr,
                                Invoice = string.Empty,
                                Check = false
                            };

                            InlandData.Insert(0, inland);
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
                        using (SqlCommand deleteLoadingCommand = new SqlCommand("DELETE FROM Inland_driving_more WHERE Nalog_nr_inland = @Nalog", connection))
                        {
                            deleteLoadingCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteLoadingCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand deleteIncomingCommand = new SqlCommand("DELETE FROM inland_driving WHERE Nalog_nr_inland = @Nalog", connection))
                        {
                            deleteIncomingCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteIncomingCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Nalog and associated records deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                        InlandModel itemToRemove = inlandData.FirstOrDefault(item => item.Nalog == nalogNr);
                        if (itemToRemove != null)
                        {
                            inlandData.Remove(itemToRemove);
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
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Inland_driving WHERE Nalog_nr_inland = @Nalog", connection))
                {
                    command.Parameters.AddWithValue("@Nalog", nalog);
                    int count = (int)command.ExecuteScalar();
                    exists = count > 0;
                }
            }
            return exists;
        }

        private void ExecutePrint(int nalog)
        {
            try
            {
                Inl INL = new Inl(nalog);
                INL.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
