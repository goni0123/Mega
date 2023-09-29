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
    public class OrderOutViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        private ObservableCollection<OrderOutModel> orderOutData;
        private List<OrderOutModel> filteredData;

        public ObservableCollection<OrderOutModel> OrderOutData
        {
            get { return orderOutData; }
            set { orderOutData = value; }
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

        public OrderOutViewModel()
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
            orderOutData = new ObservableCollection<OrderOutModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Top(250) An_Attn_out,Von_From_out,Loading_Order_number_out,Freight_Price_out,Check_lo FROM Loading_Order_out ORDER BY Loading_Order_number_out DESC";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        OrderOutModel orderout = new OrderOutModel
                        {
                            Nalog = reader.IsDBNull(reader.GetOrdinal("Loading_Order_number_out")) ? 0 : reader.GetInt32(reader.GetOrdinal("Loading_Order_number_out")),
                            To = reader.IsDBNull(reader.GetOrdinal("An_Attn_out")) ? string.Empty : reader.GetString(reader.GetOrdinal("An_Attn_out")),
                            From = reader.IsDBNull(reader.GetOrdinal("Von_From_out")) ? string.Empty : reader.GetString(reader.GetOrdinal("Von_From_out")),
                            FreightPrice = reader.IsDBNull(reader.GetOrdinal("Freight_Price_out")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Freight_Price_out")),
                            Check = reader.IsDBNull(reader.GetOrdinal("Check_lo")) ? false : reader.GetBoolean(reader.GetOrdinal("Check_lo"))
                        };

                        OrderOutData.Add(orderout);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data: " + ex.Message);
                }
            }

            filteredData = new List<OrderOutModel>(orderOutData);
        }
        public void FilterData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                OrderOutData.Clear();
                foreach (var item in filteredData)
                {
                    OrderOutData.Add(item);
                }
            }
            else
            {
                OrderOutData.Clear();
                foreach (var item in filteredData)
                {
                    if (item.Nalog.ToString().Contains(searchText) ||
                        item.To.Contains(searchText) ||
                        item.From.Contains(searchText))
                    {
                        OrderOutData.Add(item);
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
                            using (SqlCommand command = new SqlCommand("INSERT INTO Loading_Order_out (Loading_Order_number_out) VALUES (@Nalog)", connection))
                            {
                                command.Parameters.AddWithValue("@Nalog", nalogNr);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Data inserted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            OrderOutModel outgoing = new OrderOutModel
                            {
                                Nalog = nalogNr,
                                To = string.Empty,
                                From = string.Empty,
                                FreightPrice = 0,
                                Check = false
                            };

                            OrderOutData.Insert(0, outgoing);
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

                        using (SqlCommand deleteIncomingCommand = new SqlCommand("DELETE FROM Loading_Order_out WHERE Loading_Order_number_out= @Nalog", connection))
                        {
                            deleteIncomingCommand.Parameters.AddWithValue("@Nalog", nalogNr);
                            deleteIncomingCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Nalog and associated records deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        connection.Close();
                        OrderOutModel itemToRemove = orderOutData.FirstOrDefault(item => item.Nalog == nalogNr);
                        if (itemToRemove != null)
                        {
                            orderOutData.Remove(itemToRemove);
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
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Loading_Order_out WHERE Loading_Order_number_out= @Nalog", connection))
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
                LoadOut Out = new LoadOut(nalog);
                Out.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
