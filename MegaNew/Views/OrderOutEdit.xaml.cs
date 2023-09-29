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
    /// Interaction logic for OrderOutEdit.xaml
    /// </summary>
    public partial class OrderOutEdit : UserControl
    {
        public int Nalog
        {
            get { return (int)GetValue(NalogProperty); }
            set { SetValue(NalogProperty, value); }
        }

        public static readonly DependencyProperty NalogProperty =
            DependencyProperty.Register("Nalog", typeof(int), typeof(OrderOutEdit), new PropertyMetadata(0));

        OrderOutEditViewModel orderOutEdit;
        MainViewModel main;
        public OrderOutEdit(int nalog)
        {
            try
            {
                InitializeComponent();
                Nalog = nalog;
                DataContext = this;
                main = new MainViewModel();
                main.TestConnection();
                orderOutEdit = new OrderOutEditViewModel(); 
                orderOutEdit.LoadData(truck);
                orderOutEdit.Fill(Nalog, An, From, Company1,
                    Company2, Phone1, Phone2, Email, truck, Driver,
                    LoadingDate, LoadingAddress, Exporter, Goods, Packing, Byorder,
                    Importer, Offloadplace, FreightPrice, FreightPaid, Notice, REF, Date);
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
                orderOutEdit.Fill(Nalog, An, From, Company1,
                 Company2, Phone1, Phone2, Email, truck, Driver,
                 LoadingDate, LoadingAddress, Exporter, Goods, Packing, Byorder,
                 Importer, Offloadplace, FreightPrice, FreightPaid, Notice, REF, Date);
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
                orderOutEdit.Submit(Nalog, An, From, Company1,
                 Company2, Phone1, Phone2, Email, truck, Driver,
                 LoadingDate, LoadingAddress, Exporter, Goods, Packing, Byorder,
                 Importer, Offloadplace, FreightPrice, FreightPaid, Notice, REF, Date, Done);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
