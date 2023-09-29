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

namespace MegaNew.Views
{
    /// <summary>
    /// Interaction logic for Inland.xaml
    /// </summary>
    public partial class Inland : UserControl
    {
        public InlandViewModel inland{ get; set; }
        public Inland()
        {
            try
            {
                inland = new InlandViewModel();
                InitializeComponent();
                DataContext = inland;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Nalog = nalog.Text;
                inland.AddCommand.Execute(Nalog);
                inland.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is int nalogin)
                {
                    InlandEdit inlandEdit = new InlandEdit(nalogin);
                    MainWindow main = Window.GetWindow(this) as MainWindow;
                    if (main != null)
                    {
                        main.CC.Content = inlandEdit;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.DataContext is InlandModel inlandModel)
                {
                    int nalogNr = inlandModel.Nalog;
                    inland.DeleteCommand.Execute(nalogNr);
                    inland.LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.DataContext is InlandModel inlandModel)
                {
                    int nalogNr = inlandModel.Nalog;
                    inland.PrintCommand.Execute(nalogNr);
                    inland.LoadData();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred while Printing:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
}
}
