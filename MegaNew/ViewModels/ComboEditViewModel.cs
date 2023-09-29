using MegaNew.Models;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace MegaNew.ViewModels
{
    public class ComboEditViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        private ObservableCollection<TruckModel> truckData;
        public ObservableCollection<TruckModel> TruckData
        {
            get { return truckData; }
            set { truckData = value; }
        }
        private ObservableCollection<TrailorModel> trailorData;
        public ObservableCollection<TrailorModel> TrailorData
        {
            get { return trailorData; }
            set { trailorData = value; }
        }
        public void LoadData()
        {
            truckData = new ObservableCollection<TruckModel>();
            trailorData = new ObservableCollection<TrailorModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM trailor ORDER BY trailor";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TrailorModel trailor = new TrailorModel
                        {
                            Trailor = reader.IsDBNull(reader.GetOrdinal("trailor")) ? string.Empty : reader.GetString(reader.GetOrdinal("trailor"))
                        };

                        TrailorData.Add(trailor);
                    }
                    reader.Close();
                    string queryt = "SELECT * FROM Truck ORDER BY truck";
                    SqlCommand commandt = new SqlCommand(queryt, connection);

                    SqlDataReader readert = commandt.ExecuteReader();

                    while (readert.Read())
                    {
                        TruckModel truck = new TruckModel
                        {
                            Truck = readert.IsDBNull(readert.GetOrdinal("truck")) ? string.Empty : readert.GetString(readert.GetOrdinal("truck"))
                        };

                        TruckData.Add(truck);
                    }
                    readert.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data: " + ex.Message);
                }
            }
        }
        public void AddTruck(string truck)
        {
            try
            {
                bool nalogExists = CheckIfTruckExists(truck);
                if (!nalogExists)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("INSERT INTO Truck (truck) VALUES (@Truck)", connection))
                        {
                            command.Parameters.AddWithValue("@Truck", truck);
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Data inserted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                        TruckModel incoming = new TruckModel
                        {
                            Truck = truck
                        };

                        TruckData.Insert(0, incoming);
                        connection.Close();
                    }
                    System.Windows.MessageBox.Show("Added successful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public void DeleteTruck(string truck)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the Nalog and associated records?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Truck WHERE truck = @Truck", connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@Truck", truck);
                            deleteCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Nalog and associated records deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                        TruckModel itemToRemove = truckData.FirstOrDefault(item => item.Truck== truck);
                        if (itemToRemove != null)
                        {
                            truckData.Remove(itemToRemove);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the Nalog: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void AddTrailor(string trailor)
        {
            try
            {
                bool nalogExists = CheckIfTrailorExists(trailor);
                if (!nalogExists)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("INSERT INTO Trailor (trailor) VALUES (@Trailor)", connection))
                        {
                            command.Parameters.AddWithValue("@Trailor", trailor);
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Data inserted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                        TrailorModel incoming = new TrailorModel
                        {
                            Trailor = trailor
                        };

                        TrailorData.Insert(0, incoming);
                        connection.Close();
                    }
                    System.Windows.MessageBox.Show("Added successful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public void DeleteTrailor(string trailor)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the Nalog and associated records?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Trailor WHERE trailor = @Trailor", connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@Trailor", trailor);
                            deleteCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Nalog and associated records deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                        TrailorModel itemToRemove = trailorData.FirstOrDefault(item => item.Trailor == trailor);
                        if (itemToRemove != null)
                        {
                            trailorData.Remove(itemToRemove);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the Nalog: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool CheckIfTruckExists(string truck)
        {
            bool exists = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Truck WHERE truck= @Truck", connection))
                {
                    command.Parameters.AddWithValue("@Truck", truck);
                    int count = (int)command.ExecuteScalar();
                    exists = count > 0;
                }
            }
            return exists;
        }
        private bool CheckIfTrailorExists(string trailor)
        {
            bool exists = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Trailor WHERE trailor= @Trailor", connection))
                {
                    command.Parameters.AddWithValue("@Trailor", trailor);
                    int count = (int)command.ExecuteScalar();
                    exists = count > 0;
                }
            }
            return exists;
        }
    }
}
