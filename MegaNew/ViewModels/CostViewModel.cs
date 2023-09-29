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

namespace MegaNew.ViewModels
{
    public class CostViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        private ObservableCollection<CostModel> costData;
        private List<CostModel> filteredData;

        public ObservableCollection<CostModel> CostData
        {
            get { return costData; }
            set { costData = value; }
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

        public CostViewModel()
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
            costData = new ObservableCollection<CostModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Top(250) Nalog,Truck,Driver1,Driver2,Done,All_Total FROM Cost ORDER BY Nalog DESC";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CostModel cost = new CostModel
                        {
                            Nalog = reader.IsDBNull(reader.GetOrdinal("Nalog")) ? 0 : reader.GetInt32(reader.GetOrdinal("Nalog")),
                            Truck = reader.IsDBNull(reader.GetOrdinal("Truck")) ? string.Empty : reader.GetString(reader.GetOrdinal("Truck")),
                            Driver1 = reader.IsDBNull(reader.GetOrdinal("Driver1")) ? string.Empty : reader.GetString(reader.GetOrdinal("Driver1")),
                            Driver2 = reader.IsDBNull(reader.GetOrdinal("Driver2")) ? string.Empty : reader.GetString(reader.GetOrdinal("Driver2")),
                            Total = reader.IsDBNull(reader.GetOrdinal("All_Total")) ? 0 : reader.GetInt32(reader.GetOrdinal("All_Total")),
                            Done = reader.IsDBNull(reader.GetOrdinal("Done")) ? false : reader.GetBoolean(reader.GetOrdinal("Done"))
                        };

                        CostData.Add(cost);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data: " + ex.Message);
                }
            }

            filteredData = new List<CostModel>(costData);
        }
        public void FilterData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                CostData.Clear();
                foreach (var item in filteredData)
                {
                    CostData.Add(item);
                }
            }
            else
            {
                CostData.Clear();
                foreach (var item in filteredData)
                {
                    if (item.Nalog.ToString().Contains(searchText) ||
                        item.Truck.Contains(searchText) ||
                        item.Driver1.Contains(searchText) ||
                        item.Driver2.Contains(searchText) ||
                        item.Total.ToString().Contains(searchText))
                    {
                        CostData.Add(item);
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
        private void ExecuteAdd(string Nalog)
        {
            try
            {
                bool nalogExists = CheckIfNalogExists(Nalog);
                if (!nalogExists)
                {
                    if (int.TryParse(Nalog, out int nalog))
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand("INSERT INTO Cost (Nalog) VALUES (@Nalog)", connection))
                            {
                                command.Parameters.AddWithValue("@Nalog", nalog);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Data inserted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            CostModel cost = new CostModel
                            {
                                Nalog = nalog,
                                Truck = string.Empty,
                                Driver1 = string.Empty,
                                Driver2 = string.Empty,
                                Total = 0,
                                Done = false
                            };

                            CostData.Insert(0, cost);
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
                        using (SqlCommand deleteLoadingCommand = new SqlCommand("DELETE FROM Cost WHERE Nalog = @Nalog", connection))
                        {
                            deleteLoadingCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteLoadingCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Nalog and associated records deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                        CostModel itemToRemove = costData.FirstOrDefault(item => item.Nalog == nalogNr);
                        if (itemToRemove != null)
                        {
                            costData.Remove(itemToRemove);
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
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Cost WHERE Nalog = @Nalog", connection))
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
                Co Cost= new Co(nalog);
                Cost.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}