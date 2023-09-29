using MegaNew.ViewModels;
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

namespace MegaNew.Views
{
    /// <summary>
    /// Interaction logic for OutgoingEdit.xaml
    /// </summary>
    public partial class OutgoingEdit : UserControl
    {
        public int Nalog
        {
            get { return (int)GetValue(NalogProperty); }
            set { SetValue(NalogProperty, value); }
        }

        public static readonly DependencyProperty NalogProperty =
            DependencyProperty.Register("Nalog", typeof(int), typeof(OutgoingEdit), new PropertyMetadata(0));

        OutgoingEditViewModel outgoingEdit;
        MainViewModel main;
        public OutgoingEdit(int nalog)
        {
            try
            {
                InitializeComponent();
                Nalog = nalog;
                DataContext = this;
                main = new MainViewModel();
                main.TestConnection();
                outgoingEdit = new OutgoingEditViewModel();
                outgoingEdit.LoadData(Loading, Route, truck, trailor, Nalog);
                outgoingEdit.Fill(Nalog, Loading, Route, trailor, truck, rit, sdate, edate, km, workday, cost, invoice, comment, driver, invoiceA, commentA, costA);
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                outgoingEdit.UpdateData(Nalog, sdate, edate, truck, rit, workday, km, comment, cost, invoice, Done, costA, commentA, invoiceA, driver, trailor);
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
                commentA.Text = outgoingEdit.FilePathReturn().FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                outgoingEdit.Fill(Nalog, Loading, Route, trailor, truck, rit, sdate, edate, km, workday, cost, invoice, comment, driver, invoiceA, commentA, costA);
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
                string Inv = invoiceA.Text;
                outgoingEdit.ProcessReturn(Inv);
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
                invoiceA.Text = outgoingEdit.FilePathReturn().FileName;
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
                costA.Text = outgoingEdit.FilePathReturn().FileName;
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
                string Cost = costA.Text;
                outgoingEdit.ProcessReturn(Cost);
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
                string Com = commentA.Text;
                outgoingEdit.ProcessReturn(Com);
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
                    selectedID = (int)selectedRow["LCO_id"];
                }
                outgoingEdit.DeleteLoading(Nalog, selectedID, Loading);
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
                    selectedID = (int)selectedRow["LCO_id"];
                }
                outgoingEdit.LoadingInsertData(selectedID, Nalog, exp, imp, coli, kg, DocA, TransA, Loading);
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
                outgoingEdit.ProcessReturn(Tran);
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
                TransA.Text = outgoingEdit.FilePathReturn().FileName;
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
                string Doc = DocA.Text;
                outgoingEdit.ProcessReturn(Doc);
            }
            catch
            {
                MessageBox.Show("Please Load the the file, Or the file has changed it's path\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void doc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocA.Text = outgoingEdit.FilePathReturn().FileName;
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
                outgoingEdit.InsertRoute(Nalog, route.Text, trailor.Text, Route);
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
                    selectedID = (int)selectedRow["RO"];
                }
                outgoingEdit.DeleteRoute(Nalog, selectedID, Route);
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
                outgoingEdit.FillTextboxesFromDataGrid(Loading, exp, imp, coli, kg, DocA, TransA);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filling text:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                outgoingEdit.LastCity(trailor);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while finding the trailor:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

