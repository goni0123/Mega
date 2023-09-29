using MegaNew.Models;
using MegaNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MegaNew.Views
{
    /// <summary>
    /// Interaction logic for ComboEdit.xaml
    /// </summary>
    public partial class ComboEdit : UserControl
    {
        public ComboEditViewModel combo { get; set; }
        public ComboEdit()
        {
            try
            {
                combo = new ComboEditViewModel();
                InitializeComponent();
                DataContext = combo;
                combo.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_trailor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                combo.AddTrailor(Trailor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_trailor_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                combo.DeleteTrailor(Trailor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                combo.AddTruck(Truck.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                combo.DeleteTruck(Truck.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
