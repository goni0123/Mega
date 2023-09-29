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
    /// Interaction logic for OrderIn.xaml
    /// </summary>
    public partial class OrderIn : UserControl
    {
        public OrderInViewModel orderIn{ get; set; }
        public OrderIn()
        {
            try
            {
                orderIn = new OrderInViewModel();
                InitializeComponent();
                DataContext = orderIn;
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
                orderIn.AddCommand.Execute(Nalog);
                orderIn.LoadData();
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
                    OrderInEdit orderInEdit = new OrderInEdit(nalogin);
                    MainWindow main = Window.GetWindow(this) as MainWindow;
                    if (main != null)
                    {
                        main.CC.Content = orderInEdit;
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
                if (sender is Button button && button.DataContext is OrderInModel orderInModel)
                {
                    int nalogNr = orderInModel.Nalog;
                    orderIn.DeleteCommand.Execute(nalogNr);
                    orderIn.LoadData();
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
                if (sender is Button button && button.DataContext is OrderInModel orderInModel)
                {
                    int nalogNr = orderInModel.Nalog;
                    orderIn.PrintCommand.Execute(nalogNr);
                    orderIn.LoadData();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred while Printing:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
