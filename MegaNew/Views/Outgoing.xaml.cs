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
    public partial class Outgoing : UserControl
    {
        public OutgoingViewModel outgoing{ get; set; }
        public Outgoing()
        {
            try {
                outgoing = new OutgoingViewModel();
                InitializeComponent();
                DataContext = outgoing;
            }catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Nalog = nalog.Text;
                outgoing.AddCommand.Execute(Nalog);
                outgoing.LoadData();
            }catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while adding:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is int nalogout)
                {
                    OutgoingEdit outgoingEdit = new OutgoingEdit(nalogout);
                    MainWindow main = Window.GetWindow(this) as MainWindow;
                    if (main != null)
                    {
                        main.CC.Content = outgoingEdit;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.DataContext is IncomingModel incomingModel)
                {
                    int nalogNr = incomingModel.NalogNr;
                    outgoing.DeleteCommand.Execute(nalogNr);
                    outgoing.LoadData();
                }
            }catch(Exception ex) 
            {
                MessageBox.Show($"An error occurred while Deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.DataContext is IncomingModel incomingModel)
                {
                    int nalogNr = incomingModel.NalogNr;
                    outgoing.PrintCommand.Execute(nalogNr);
                    outgoing.LoadData();
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while Printing:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
