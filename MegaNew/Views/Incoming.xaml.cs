using MegaNew.Models;
using MegaNew.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MegaNew.Views
{
    public partial class Incoming : UserControl
    {
        public IncomingViewModel incoming { get; set; }
        public Incoming()
        {
            try
            {
                incoming = new IncomingViewModel();
                InitializeComponent();
                DataContext = incoming;
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Nalog = nalog.Text;
                incoming.AddCommand.Execute(Nalog);
                incoming.LoadData();
            }
            catch(Exception ex)
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
                    IncomingEdit incomingEdit = new IncomingEdit(nalogin);
                    MainWindow main = Window.GetWindow(this) as MainWindow;
                    if (main != null)
                    {
                        main.CC.Content = incomingEdit;
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
                if (sender is Button button && button.DataContext is IncomingModel incomingModel)
                {
                    int nalogNr = incomingModel.NalogNr;
                    incoming.DeleteCommand.Execute(nalogNr);
                    incoming.LoadData();
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.DataContext is IncomingModel incomingModel)
                {
                    int nalogNr = incomingModel.NalogNr;
                    incoming.PrintCommand.Execute(nalogNr);
                    incoming.LoadData();
                }
            }catch (Exception ex)
            {

                MessageBox.Show($"An error occurred while Printing:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
