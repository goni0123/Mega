﻿using MegaNew.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using CheckBox = System.Windows.Controls.CheckBox;
using ComboBox = System.Windows.Controls.ComboBox;
using TextBox = System.Windows.Controls.TextBox;

namespace MegaNew.ViewModels
{
    public class OrderInEditViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        public void LoadData(ComboBox truck)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT truck FROM Truck", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            truck.Items.Add(reader["truck"].ToString());
                        }
                        reader.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No rows are present\n", ex);
            }
        }
        public void Fill(int Nalog, TextBox An, TextBox From, TextBox Company1, TextBox Company2, TextBox Phone1, TextBox Phone2, 
            TextBox Email, ComboBox truck, TextBox Driver, DatePicker LoadingDate, TextBox LoadingAddress, TextBox Exporter, TextBox Goods,
            TextBox Packing, TextBox Byorder, TextBox Importer, TextBox Offloadplace, TextBox FreightPrice, TextBox FreightPaid, TextBox Notice, TextBox REF, DatePicker Date)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Loading_Order_In where Loading_Order_number_in=@Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                string Fr = reader["Von_From_in"].ToString();
                                string To = reader["An_Attn_in"].ToString();
                                string Com1 = reader["Company_1_in"].ToString();
                                string Com2 = reader["Company_2_in"].ToString();
                                string Ph1 = reader["Phone_1_number_in"].ToString();
                                string Ph2 = reader["Phone_2_number_in"].ToString();
                                string email = reader["Email_in"].ToString();
                                string Truck = reader["Truck_plate_in"].ToString();
                                string driver = reader["Driver_order_in"].ToString();
                                string Load_date = reader["Loading_Date_in"].ToString();
                                string Load_Add = reader["Loading_Address_in"].ToString();
                                string Export = reader["Exporter_order_in"].ToString();
                                string Good = reader["Goods_order_in"].ToString();
                                string Pack = reader["Packing_in"].ToString();
                                string By_o = reader["By_order_in"].ToString();
                                string Import = reader["Importer_order_in"].ToString();
                                string Offload = reader["Offload_in"].ToString();
                                string F_Price = reader["Freight_Price_in"].ToString();
                                string F_paid = reader["Freight_paid_by_in"].ToString();
                                string notice = reader["Notice_in"].ToString();
                                string Ref = reader["REF_number_in"].ToString();
                                string date = reader["Date_Document_in"].ToString();
                                An.Text = To;
                                From.Text = Fr;
                                Company1.Text = Com1;
                                Company2.Text = Com2;
                                Phone1.Text = Ph1;
                                Phone2.Text = Ph2;
                                Email.Text = email;
                                truck.Text = Truck;
                                Driver.Text = driver;
                                LoadingDate.Text = Load_date;
                                LoadingAddress.Text = Load_Add;
                                Exporter.Text = Export;
                                Goods.Text = Good;
                                Packing.Text = Pack;
                                Byorder.Text = By_o;
                                Importer.Text = Import;
                                Offloadplace.Text = Offload;
                                FreightPrice.Text = F_Price;
                                FreightPaid.Text = F_paid;
                                Notice.Text = notice;
                                REF.Text = Ref;
                                Date.Text = date;
                            }
                            reader.Close();
                        }
                        connection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error retrieving data: " + ex.Message);
            }
        }
        public void Submit(int Nalog, TextBox An, TextBox From, TextBox Company1, TextBox Company2, TextBox Phone1, TextBox Phone2,
            TextBox Email, ComboBox truck, TextBox Driver, DatePicker LoadingDate, TextBox LoadingAddress, TextBox Exporter, TextBox Goods,
            TextBox Packing, TextBox Byorder, TextBox Importer, TextBox Offloadplace, TextBox FreightPrice, TextBox FreightPaid, TextBox Notice, TextBox REF, DatePicker Date,CheckBox Done)
        {
            try
            {
                string To = An.Text;
                string Fr = From.Text;
                string Com1 = Company1.Text;
                string Com2 = Company2.Text;
                string phone1 = Phone1.Text;
                string phone2 = Phone2.Text;
                string email = Email.Text;
                string Truck = truck.Text;
                string driver = Driver.Text;
                string loadDate = LoadingDate.Text;
                string loadAdd = LoadingAddress.Text;
                string ex = Exporter.Text;
                string good = Goods.Text;
                string pack = Packing.Text;
                string byorder = Byorder.Text;
                string imp = Importer.Text;
                string offload = Offloadplace.Text;
                string freightPrice = FreightPrice.Text;
                string freightPaid = FreightPaid.Text;
                string notice = Notice.Text;
                string Ref = REF.Text;
                string date = Date.Text;
                int check;
                if (Done.IsChecked == true)
                {
                    check = 1;
                }
                else
                {
                    check = 0;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Loading_Order_In SET " +
                        "An_Attn_in=@To, Von_From_in=@Fr, Company_1_in=@Com1, Company_2_in=@Com2, " +
                        "Phone_1_number_in=@phone1, Phone_2_number_in=@phone2, Email_in=@email, " +
                        "Truck_plate_in=@truck, Driver_order_in=@driver, Loading_Date_in=@loadDate, " +
                        "Loading_Address_in=@loadAdd, Exporter_order_in=@ex, Goods_order_in=@good, " +
                        "Packing_in=@pack, By_order_in=@byorder, Importer_order_in=@imp, " +
                        "Offload_in=@offload, Freight_Price_in=@FpValue, Freight_paid_by_in=@freightPaid, " +
                        "Notice_in=@notice, REF_number_in=@Ref, Date_Document_in=@date, Check_l=@check " +
                        "WHERE Loading_Order_number_in=@Ordernum", connection))
                    {
                        command.Parameters.AddWithValue("@To", To);
                        command.Parameters.AddWithValue("@Fr", Fr);
                        command.Parameters.AddWithValue("@Com1", Com1);
                        command.Parameters.AddWithValue("@Com2", Com2);
                        command.Parameters.AddWithValue("@phone1", phone1);
                        command.Parameters.AddWithValue("@phone2", phone2);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@Ordernum", Nalog);
                        command.Parameters.AddWithValue("@truck", Truck);
                        command.Parameters.AddWithValue("@driver", driver);
                        command.Parameters.AddWithValue("@loadDate", loadDate);
                        command.Parameters.AddWithValue("@loadAdd", loadAdd);
                        command.Parameters.AddWithValue("@ex", ex);
                        command.Parameters.AddWithValue("@good", good);
                        command.Parameters.AddWithValue("@pack", pack);
                        command.Parameters.AddWithValue("@byorder", byorder);
                        command.Parameters.AddWithValue("@imp", imp);
                        command.Parameters.AddWithValue("@offload", offload);
                        command.Parameters.AddWithValue("@FpValue",string.IsNullOrEmpty(freightPrice) ? 0 : Convert.ToDecimal(freightPrice));
                        command.Parameters.AddWithValue("@freightPaid", freightPaid);
                        command.Parameters.AddWithValue("@notice", notice);
                        command.Parameters.AddWithValue("@Ref", Ref);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@check", check);
                        command.ExecuteNonQuery();
                    }
                }
                System.Windows.MessageBox.Show("Update successful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
               throw new Exception("An error occurred during the update:\n" + ex.Message);
            }

        }
    }
}
