using MegaNew.Models;
using MegaNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for IncomingEdit.xaml
    /// </summary>
    public partial class IncomingEdit : UserControl
    {
        public int Nalog
        {
            get { return (int)GetValue(NalogProperty); }
            set { SetValue(NalogProperty, value); }
        }

        public static readonly DependencyProperty NalogProperty =
            DependencyProperty.Register("Nalog", typeof(int), typeof(IncomingEdit), new PropertyMetadata(0));

        IncomingEditViewModel incomingEdit;
        MainViewModel main;
        public IncomingEdit(int nalog)
        {
            try 
            {
                InitializeComponent();
                Nalog = nalog;
                DataContext = this;
                main = new MainViewModel();
                main.TestConnection();
                incomingEdit = new IncomingEditViewModel();
                incomingEdit.LoadData(Loading, Route, truck, trailor, Nalog);
                incomingEdit.Fill(Nalog, Loading, Route, trailor, truck, rit, sdate, edate, km, workday, cost, invoice, comment, driver, invoiceA, commentA, costA);
            } 
            catch (Exception ex) 
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                incomingEdit.UpdateData(Nalog, sdate, edate, truck, rit, workday, km, comment, cost, invoice, Done, costA, commentA, invoiceA, driver, trailor);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void coma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                commentA.Text = incomingEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            try {
                incomingEdit.Fill(Nalog, Loading, Route, trailor, truck, rit, sdate, edate, km, workday, cost, invoice, comment, driver, invoiceA, commentA, costA);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filling getting the data:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void invoiceOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Inv= invoiceA.Text;
                incomingEdit.ProcessReturn(Inv);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void invoicea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoiceA.Text = incomingEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void costa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                costA.Text = incomingEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CostOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Cost= costA.Text;
                incomingEdit.ProcessReturn(Cost);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ComentOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Com= commentA.Text;
                incomingEdit.ProcessReturn(Com);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    selectedID = (int)selectedRow["LCI_id"];
                }
                incomingEdit.DeleteLoading(Nalog, selectedID, Loading);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Insert_Loading_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedID = 0;
                if (Loading.SelectedIndex >= 0)
                {
                    DataRowView selectedRow = (DataRowView)Loading.SelectedItem;
                    selectedID = (int)selectedRow["LCI_id"];
                }
                incomingEdit.LoadingInsertData(selectedID, Nalog, exp, imp, coli, kg, DocA, TransA,Loading);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TranOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Tran = TransA.Text;
                incomingEdit.ProcessReturn(Tran);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void transa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TransA.Text = incomingEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DocOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Doc=DocA.Text;
                incomingEdit.ProcessReturn(Doc);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void doc_Click(object sender, RoutedEventArgs e)
        {
            try {
                DocA.Text = incomingEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Insert_Route_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                incomingEdit.InsertRoute(Nalog, route.Text, trailor.Text, Route);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Route_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                int selectedID = 0;
                if (Route.SelectedIndex >= 0)
                {
                    DataRowView selectedRow = (DataRowView)Route.SelectedItem;
                    selectedID = (int)selectedRow["RI"];
                }
                incomingEdit.DeleteRoute(Nalog, selectedID, Route);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Loading_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                incomingEdit.FillTextboxesFromDataGrid(Loading, exp, imp, coli, kg, DocA, TransA);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filling text:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
