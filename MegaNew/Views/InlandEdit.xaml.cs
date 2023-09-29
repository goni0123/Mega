using MegaNew.ViewModels;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;

namespace MegaNew.Views
{
    /// <summary>
    /// Interaction logic for InlandEdit.xaml
    /// </summary>
    public partial class InlandEdit : UserControl
    {
        public int Nalog
        {
            get { return (int)GetValue(NalogProperty); }
            set { SetValue(NalogProperty, value); }
        }

        public static readonly DependencyProperty NalogProperty =
            DependencyProperty.Register("Nalog", typeof(int), typeof(InlandEdit), new PropertyMetadata(0));

        InlandEditViewModel inlandEdit;
        MainViewModel main;
        public InlandEdit(int nalog)
        {
            try
            {
                InitializeComponent();
                Nalog = nalog;
                DataContext = this;
                main = new MainViewModel();
                main.TestConnection();
                inlandEdit = new InlandEditViewModel();
                inlandEdit.Fill(Nalog, invoice, ina);
                inlandEdit.LoadData(Nalog, Loading, truck, trailor);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                inlandEdit.Fill(Nalog, invoice, ina);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while filling getting the data:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvoiceOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                inlandEdit.ProcessReturn(ina.Text);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Loading_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                inlandEdit.FillTextboxesFromDataGrid(Loading, date, km, truck, trailor, route);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filling text:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ina_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ina.Text = inlandEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Delete_Loading_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedID = 0;
                if (Loading.SelectedIndex >= 0)
                {
                    DataRowView selectedRow = (DataRowView)Loading.SelectedItem;
                    selectedID = (int)selectedRow["ID"];
                }
                inlandEdit.DeleteMore(Nalog, selectedID, Loading);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Submit_More_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedID = 0;
                if (Loading.SelectedIndex >= 0)
                {
                    DataRowView selectedRow = (DataRowView)Loading.SelectedItem;
                    selectedID = (int)selectedRow["ID"];
                }
                inlandEdit.MoreInsertData(Nalog,selectedID,Loading,date,km,truck,trailor,route);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                inlandEdit.UpdateData(Nalog, invoice, ina, Done);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
