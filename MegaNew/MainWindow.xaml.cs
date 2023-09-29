using MegaNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace MegaNew
{
    public partial class MainWindow : Window
    {
        public MainViewModel Main { get; set; }
        public MainWindow()
        {
            Main = new MainViewModel();
            InitializeComponent();
        }
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new MainWindow();
            newForm.Show();
            this.Close();
        }
        private void Incoming_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowIncoming(CC);
        }
        private void Outgoing_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowOutgoing(CC);
        }
        private void Inland_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowInland(CC);
        }
        private void OrderIn_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowOrderIn(CC);
        }
        private void OrderOut_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowOrderOut(CC);
        }
        private void Cost_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowCost(CC);
        }
        private void Week_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowWeek(CC);
        }
        private void ComboBox_Click(object sender, RoutedEventArgs e)
        {
            Main.ShowCombo(CC);
        }
    }
}
