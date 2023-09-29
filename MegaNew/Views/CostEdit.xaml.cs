using MegaNew.ViewModels;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MegaNew.Views
{
    /// <summary>
    /// Interaction logic for CostEdit.xaml
    /// </summary>
    public partial class CostEdit : UserControl
    {
        public int Nalog
        {
            get { return (int)GetValue(NalogProperty); }
            set { SetValue(NalogProperty, value); }
        }

        public static readonly DependencyProperty NalogProperty =
            DependencyProperty.Register("Nalog", typeof(int), typeof(CostEdit), new PropertyMetadata(0));

        CostEditViewModel costEdit;
        MainViewModel main;
        public CostEdit(int nalog)
        {
            try
            {
                InitializeComponent();
                Nalog = nalog;
                DataContext = this;
                main = new MainViewModel();
                main.TestConnection();
                costEdit = new CostEditViewModel();
                costEdit.Fill(Nalog,  truck,  driver1,  driver2,  termmk1,  termmk2,  putmk1,  putmk2,  naftmk1,  naftmk2,  naftcostmk1,  naftcostmk2,  bluecostmk1,
                    bluecostmk2,  bluemk1,  bluemk2,  nafteu1,  nafteu2,  naftcosteu1,  naftcosteu2,  bluecosteu1,  bluecosteu2,  blueeu1,  blueeu2,  takssrb1,  taks2,
                    putsrb1,  putsrb2,  puthu1,  puthu2,  putsk1,  putsk2,  putcz1,  putcz2,  putcro1,  putcro2,  putslo1,  putslo2,  putat1,  putat2,  putde1,  putde2,
                    putnl1,  putnl2,  phyto,  phyto2,  tel1,  tel2,  md11,  md12,  md21,  md22,  extra1,  extra2,  Tot1,  Tot2,  alltot,  name11,  name12,  name21,  name22,
                    name31,  name32,  name41,  name42,  cost11,  cost12,  cost21,  cost22,  cost31,  cost32,  cost41,  cost42,currency);
                costEdit.LoadData(truck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SumAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tot1 = costEdit.CalculateTotal1(termmk1, putmk1, naftmk1, bluemk1, nafteu1, blueeu1,
                 takssrb1, putsrb1, puthu1, putsk1, putcz1, putcro1, putslo1, putat1,
                 putde1, putnl1, phyto, md11, md21, extra1, cost11, cost21,
                 cost31, cost41, tel1);
                int tot2 = costEdit.CalculateTotal2(termmk2, putmk2, naftmk2, bluemk2, nafteu2, blueeu2,
                 taks2, putsrb2, puthu2, putsk2, putcz2, putcro2, putslo2, putat2,
                 putde2, putnl2, phyto2, md12, md22, extra2, cost12, cost22,
                 cost32, cost42, tel2);
                Tot1.Text=tot1.ToString(); 
                Tot2.Text=tot2.ToString();
                alltot.Text=costEdit.AllTotal(tot1, tot2).ToString();
            }catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while performing the calculation. Please try again.:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SumAll_Click(sender, e);
                costEdit.Submit(Nalog,  truck,  driver1,  driver2,  termmk1,  termmk2,
                                putmk1,  putmk2,  naftmk1,  naftmk2,  naftcostmk1,  naftcostmk2,
                                bluecostmk1,  bluecostmk2,  bluemk1,  bluemk2,  nafteu1,  nafteu2,
                                naftcosteu1,  naftcosteu2,  bluecosteu1,  bluecosteu2,  blueeu1,
                                blueeu2,  takssrb1,  taks2,  putsrb1,  putsrb2,  puthu1,
                                puthu2,  putsk1,  putsk2,  putcz1,  putcz2,  putcro1,
                                putcro2,  putslo1,  putslo2,  putat1,  putat2,  putde1,
                                putde2,  putnl1,  putnl2,  phyto,  phyto2,  tel1,  tel2,
                                md11,  md12,  md21,  md22,  extra1,  extra2,  Tot1,  Tot2,
                                alltot,  name11,  name12,  name21,  name22,  name31,  name32,  name41,
                                name42,  cost11,  cost12,  cost21,  cost22,  cost31,  cost32,  cost41,  cost42, Done,  currency);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while inserting:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                costEdit.Fill(Nalog, truck, driver1, driver2, termmk1, termmk2, putmk1, putmk2, naftmk1, naftmk2, naftcostmk1, naftcostmk2, bluecostmk1,
                    bluecostmk2, bluemk1, bluemk2, nafteu1, nafteu2, naftcosteu1, naftcosteu2, bluecosteu1, bluecosteu2, blueeu1, blueeu2, takssrb1, taks2,
                    putsrb1, putsrb2, puthu1, puthu2, putsk1, putsk2, putcz1, putcz2, putcro1, putcro2, putslo1, putslo2, putat1, putat2, putde1, putde2,
                    putnl1, putnl2, phyto, phyto2, tel1, tel2, md11, md12, md21, md22, extra1, extra2, Tot1, Tot2, alltot, name11, name12, name21, name22,
                    name31, name32, name41, name42, cost11, cost12, cost21, cost22, cost31, cost32, cost41, cost42,currency);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
