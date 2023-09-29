using MegaNew.Views;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MegaNew.ViewModels
{
    public class MainViewModel
    {
        private string connectionString;

        public MainViewModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
            try
            {
                TestConnection();
            }
            catch (Exception ex)
            {
                string errorMessage = "The server is currently not working. " + ex.Message;
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TestConnection()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("Unable to open the SQL connection.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Connection failed: " + ex.Message);
            }
        }
        //public void UpdateContent(ContentControl cc)
        //{
        //    try
        //    {
        //        Inland inland = cc.Content as Inland;
        //        Incoming incoming = cc.Content as Incoming;
        //        Outgoing outgoing = cc.Content as Outgoing;
        //        OrderIn loading_in = cc.Content as OrderIn;
        //        OrderOut loading_out = cc.Content as OrderOut;
        //        Cost cost = cc.Content as Cost;

        //        if (inland != null)
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                con.Open();
        //                string Nalog = inland.nalog.Text;
        //                using (SqlCommand commandInland = new SqlCommand("SELECT COUNT(*) FROM Inland_driving WHERE Nalog_nr_inland = @Nalog", con))
        //                {
        //                    commandInland.Parameters.AddWithValue("@Nalog", Nalog);
        //                    int count = (int)commandInland.ExecuteScalar();
        //                    string Invoice = inland.invoice.Text;
        //                    string Ina = inland.ina.Text;
        //                    int check;
        //                    if (inland.Done.IsChecked == true)
        //                    {
        //                        check = 1;
        //                    }
        //                    else
        //                    {
        //                        check = 0;
        //                    }
        //                    string ID = inland.id.Text;
        //                    string Data = inland.date.Text;
        //                    string KM = inland.km.Text;
        //                    int KValue;
        //                    if (KM == "")
        //                    {
        //                        KValue = 0;
        //                    }
        //                    else
        //                    {
        //                        KValue = int.Parse(KM);
        //                    }
        //                    string Truck = inland.truck.Text;
        //                    string Trailor = inland.trailor.Text;
        //                    string City = inland.route.Text;
        //                    if (count > 0)
        //                    {
        //                        // Update record
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            using (SqlCommand commandAt = new SqlCommand("Update Inland_driving Set Invoice_Inland_Attachment=@InvoiceA Where Nalog_nr_inland=@Nalog", connection))
        //                            {
        //                                if (Ina == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    commandAt.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandAt.Parameters.AddWithValue("@InvoiceA", Ina);
        //                                    commandAt.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Invoice
        //                            using (SqlCommand commandI = new SqlCommand("Update Inland_driving Set Invoice_Inland=@Invoice Where Nalog_nr_inland=@Nalog", connection))
        //                            {
        //                                if (Invoice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    commandI.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandI.Parameters.AddWithValue("@Invoice", Invoice);
        //                                    commandI.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Check
        //                            using (SqlCommand commandC = new SqlCommand("Update Inland_driving Set Check_inl=@Check Where Nalog_nr_inland=@Nalog", connection))
        //                            {
        //                                if (check == 0)
        //                                {
        //                                    commandC.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandC.Parameters.AddWithValue("@Check", check);
        //                                    commandC.ExecuteNonQuery();
        //                                }
        //                                else
        //                                {
        //                                    commandC.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandC.Parameters.AddWithValue("@Check", check);
        //                                    commandC.ExecuteNonQuery();
        //                                }
        //                            }
        //                            using (SqlCommand commandD = new SqlCommand("Update Inland_driving_more SET Data=@Data Where Nalog_nr_inland=@Nalog and ID=@id", connection))
        //                            {
        //                                if (Data == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    commandD.Parameters.AddWithValue("@id", ID);
        //                                    commandD.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandD.Parameters.AddWithValue("@Data", Data);
        //                                    commandD.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished km
        //                            using (SqlCommand commandK = new SqlCommand("Update Inland_driving_more SET KM_ind=@KM Where Nalog_nr_inland=@Nalog and ID=@id", connection))
        //                            {
        //                                if (KM == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    commandK.Parameters.AddWithValue("@id", ID);
        //                                    commandK.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandK.Parameters.AddWithValue("@KM", KValue);
        //                                    commandK.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Trailor
        //                            using (SqlCommand commandT = new SqlCommand("Update Inland_driving_more SET Trailor=@Trailor Where Nalog_nr_inland=@Nalog and ID=@id", connection))
        //                            {
        //                                if (Trailor == "")
        //                                {

        //                                }
        //                                else
        //                                {
        //                                    commandT.Parameters.AddWithValue("@id", ID);
        //                                    commandT.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandT.Parameters.AddWithValue("@Trailor", Trailor);
        //                                    commandT.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Truck
        //                            using (SqlCommand commandTr = new SqlCommand("Update Inland_driving_more SET Truck=@Truck Where Nalog_nr_inland=@Nalog and ID=@id", connection))
        //                            {
        //                                if (Truck == "")
        //                                {

        //                                }
        //                                else
        //                                {
        //                                    commandTr.Parameters.AddWithValue("@id", ID);
        //                                    commandTr.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandTr.Parameters.AddWithValue("@Truck", Truck);
        //                                    commandTr.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished City
        //                            using (SqlCommand commandC = new SqlCommand("Update Inland_driving_more SET City=@City Where Nalog_nr_inland=@Nalog and ID=@id", connection))
        //                            {
        //                                if (City == "")
        //                                {

        //                                }
        //                                else
        //                                {
        //                                    commandC.Parameters.AddWithValue("@id", ID);
        //                                    commandC.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    commandC.Parameters.AddWithValue("@City", City);
        //                                    commandC.ExecuteNonQuery();
        //                                }
        //                            }
        //                            connection.Close();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Insert record
        //                        using (SqlCommand commandInsert = new SqlCommand("Insert Into Inland_driving(Nalog_nr_inland," +
        //                            "Invoice_Inland,Invoice_Inland_Attachment,Check_inl)values(@Nalog,@Invoice,@InvoiceA," +
        //                            "@Check)", con))
        //                        {
        //                            commandInsert.Parameters.AddWithValue("@Nalog", Nalog);
        //                            commandInsert.Parameters.AddWithValue("@InvoiceA", Ina);
        //                            commandInsert.Parameters.AddWithValue("@Invoice", Invoice);
        //                            commandInsert.Parameters.AddWithValue("@Check", check);

        //                            commandInsert.ExecuteNonQuery();
        //                        }
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }
        //        ///Incoming Done for updateing
        //        else if (incoming != null)
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                con.Open();
        //                string Nalog_in = incoming.nalog.Text;
        //                using (SqlCommand commandIncoming = new SqlCommand("SELECT COUNT(*) FROM Incoming_NL_MK WHERE Nalog_nr_in = @Nalog", con))
        //                {
        //                    commandIncoming.Parameters.AddWithValue("@Nalog", Nalog_in);

        //                    int count = (int)commandIncoming.ExecuteScalar();
        //                    string Sdate = incoming.sdate.Text;
        //                    string Edate = incoming.edate.Text;
        //                    string Truck = incoming.truck.Text;
        //                    string Trailor = incoming.trailor.Text;
        //                    string RIT = incoming.rit.Text;
        //                    string WorkDays = incoming.workday.Text;
        //                    int WValue;
        //                    if (WorkDays == "")
        //                    {
        //                        WValue = 0;
        //                    }
        //                    else
        //                    {
        //                        WValue = int.Parse(WorkDays);
        //                    }
        //                    string Comm = incoming.comment.Text;
        //                    string KM = incoming.km.Text;
        //                    int KValue;
        //                    if (KM == "")
        //                    {
        //                        KValue = 0;
        //                    }
        //                    else
        //                    {
        //                        KValue = int.Parse(KM);
        //                    }
        //                    string Extra = incoming.cost.Text;
        //                    string Invoice = incoming.invoice.Text;
        //                    int check;
        //                    if (incoming.Done.IsChecked == true)
        //                    {
        //                        check = 1;
        //                    }
        //                    else
        //                    {

        //                        check = 0;

        //                    }
        //                    string ExtraA = incoming.costA.Text;
        //                    string CommA = incoming.commentA.Text;
        //                    string InvoiceA = incoming.invoiceA.Text;
        //                    string Driver = incoming.driver.Text;
        //                    string Exp = incoming.exp.Text;
        //                    string Imp = incoming.imp.Text;
        //                    string Colli = incoming.coli.Text;
        //                    int CValue;
        //                    if (Colli == "")
        //                    {
        //                        CValue = 0;
        //                    }
        //                    else
        //                    {
        //                        CValue = int.Parse(Colli);
        //                    }
        //                    string Kg = incoming.kg.Text;
        //                    decimal IValue;
        //                    if (Kg == "")
        //                    {
        //                        IValue = 0;
        //                    }
        //                    else
        //                    {
        //                        IValue = decimal.Parse(Kg);
        //                    }
        //                    string Doc = incoming.DocA.Text;
        //                    string Tran = incoming.TransA.Text;
        //                    if (count > 0)
        //                    {
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();   ///Finished Extra Atachment
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Extra_Costs_Attachment_in=@ExtraA Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (ExtraA == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@ExtraA", ExtraA);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Comment Attachment
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Comment_Attachment_in=@CommA Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (CommA == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@CommA", CommA);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Invoice Attachment
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Invoice_Attachment_in=@InvoiceA Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (InvoiceA == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@InvoiceA", InvoiceA);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Truck
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Truck_in=@Truck Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Truck == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Truck", Truck);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished RIT
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set RIT_in=@RIT Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (RIT == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@RIT", RIT);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Start date
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Start_date_in=@Sdate Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Sdate == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Sdate", Sdate);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished End date
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set End_date_in=@Edate Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Edate == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Edate", Edate);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished KM
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set KM_in=@Km Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (KM == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Km", KValue);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Word days
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Work_days_in=@Wd Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (WorkDays == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Wd", WValue);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Extra Costs
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Extra_Costs_in=@Ec Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Extra == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Ec", Extra);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Invoice
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Invoice_in=@Invoice Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Invoice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Invoice", Invoice);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Comment
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Comment_in=@Comm Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Comm == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Comm", Comm);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Check
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Check_in=@check Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (check == 0)
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Driver
        //                            using (SqlCommand command = new SqlCommand("Update Incoming_NL_MK Set Driver_in=@Driver Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Driver == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Driver", Driver);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Trailor
        //                            using (SqlCommand command = new SqlCommand("Update Route_In Set Trailor_in=@Trailor Where Nalog_nr_in=@Nalog", connection))
        //                            {
        //                                if (Trailor == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                                    command.Parameters.AddWithValue("@Trailor", Trailor);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///If LCI is filled
        //                            string Lci = incoming.lci.Text;
        //                            if (Lci == "") { }
        //                            else
        //                            {
        //                                int lc = int.Parse(Lci);

        //                                ///Finished Export
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_In Set Export_in=@Exp where LCI_id=@lci", connection))
        //                                {
        //                                    if (Exp == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Exp", Exp);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Colli
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_In Set Colli_in=@Colli where LCI_id=@lci", connection))
        //                                {
        //                                    if (Colli == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Colli", CValue);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Kg
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_In Set KG_in=@Kg where LCI_id=@lci", connection))
        //                                {

        //                                    if (Kg == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Kg", IValue);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Import
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_In Set Importer_in=@Imp where LCI_id=@lci", connection))
        //                                {
        //                                    if (Imp == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Imp", Imp);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Document att
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_In Set Document_in=@Doc where LCI_id=@lci", connection))
        //                                {
        //                                    if (Doc == "")
        //                                    {
        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Doc", Doc);
        //                                        command.ExecuteNonQuery();

        //                                    }
        //                                }
        //                                ///Finished Trasport invoice
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_In Set Transport_invoice_in=@Trans where LCI_id=@lci", connection))
        //                                {
        //                                    if (Tran == "")
        //                                    {
        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Trans", Tran);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                            }
        //                            connection.Close();
        //                        }

        //                    }
        //                    else
        //                    {
        //                        using (SqlCommand command = new SqlCommand("Insert Into Incoming_NL_MK(Nalog_nr_in,Truck_in,RIT_in,Start_date_in" +
        //                            ",End_date_in,KM_in,Work_days_in,Extra_Costs_in,Extra_Costs_Attachment_in,Invoice_in,Invoice_Attachment_in,Comment_in," +
        //                            "Comment_Attachment_in,Check_in,Driver_in)values(@Nalog,@Truck,@RIT,@Sdate," +
        //                            "@Edate,@KValue,@WValue,@Extra,@ExtraA,@Invoice,@InvoiceA,@Comm,@CommA,@Check,@Driver)", con))
        //                        {
        //                            command.Parameters.AddWithValue("@Nalog", Nalog_in);
        //                            command.Parameters.AddWithValue("@Truck", Truck);
        //                            command.Parameters.AddWithValue("@RIT", RIT);
        //                            command.Parameters.AddWithValue("@Sdate", Sdate);
        //                            command.Parameters.AddWithValue("@Edate", Edate);
        //                            command.Parameters.AddWithValue("@KValue", KValue);
        //                            command.Parameters.AddWithValue("@WValue", WValue);
        //                            command.Parameters.AddWithValue("@Extra", Extra);
        //                            command.Parameters.AddWithValue("@ExtraA", ExtraA);
        //                            command.Parameters.AddWithValue("@Comma", CommA);
        //                            command.Parameters.AddWithValue("@Invoice", Invoice);
        //                            command.Parameters.AddWithValue("@InvoiceA", InvoiceA);
        //                            command.Parameters.AddWithValue("@Comm", Comm);
        //                            command.Parameters.AddWithValue("@Check", check);
        //                            command.Parameters.AddWithValue("@Driver", Driver);
        //                            command.ExecuteNonQuery();

        //                        }
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }
        //        else if (outgoing != null)
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                con.Open();
        //                string Nalog = outgoing.nalog.Text;
        //                using (SqlCommand commandIncoming = new SqlCommand("SELECT COUNT(*) FROM Outgoing_MK_NL WHERE Nalog_nr_out = @Nalog", con))
        //                {
        //                    commandIncoming.Parameters.AddWithValue("@Nalog", Nalog);
        //                    int count = (int)commandIncoming.ExecuteScalar();
        //                    string Sdate = outgoing.sdate.Text;
        //                    string Edate = outgoing.edate.Text;
        //                    string Truck = outgoing.truck.Text;
        //                    string Trailor = outgoing.trailor.Text;
        //                    string RIT = outgoing.rit.Text;
        //                    string WorkDays = outgoing.workday.Text;
        //                    int WValue;
        //                    if (WorkDays == "")
        //                    {
        //                        WValue = 0;
        //                    }
        //                    else
        //                    {
        //                        WValue = int.Parse(WorkDays);
        //                    }
        //                    string Comm = outgoing.comment.Text;
        //                    string KM = outgoing.km.Text;
        //                    int KValue;
        //                    if (KM == "")
        //                    {
        //                        KValue = 0;
        //                    }
        //                    else
        //                    {
        //                        KValue = int.Parse(KM);
        //                    }
        //                    string Extra = outgoing.cost.Text;
        //                    string Invoice = outgoing.invoice.Text;
        //                    int check;
        //                    if (outgoing.Done.IsChecked == true)
        //                    {
        //                        check = 1;
        //                    }
        //                    else
        //                    {

        //                        check = 0;

        //                    }
        //                    string ExtraA = outgoing.costA.Text;
        //                    string CommA = outgoing.commentA.Text;
        //                    string InvoiceA = outgoing.invoiceA.Text;
        //                    string Driver = outgoing.driver.Text;
        //                    string Exp = outgoing.exp.Text;
        //                    string Imp = outgoing.imp.Text;
        //                    string Colli = outgoing.coli.Text;
        //                    int CValue;
        //                    if (Colli == "")
        //                    {
        //                        CValue = 0;
        //                    }
        //                    else
        //                    {
        //                        CValue = int.Parse(Colli);
        //                    }
        //                    string Kg = outgoing.kg.Text;
        //                    decimal IValue;
        //                    if (Kg == "")
        //                    {
        //                        IValue = 0;
        //                    }
        //                    else
        //                    {
        //                        IValue = decimal.Parse(Kg);
        //                    }
        //                    string Doc = outgoing.DocA.Text;
        //                    string Tran = outgoing.TransA.Text;
        //                    if (count > 0)
        //                    {
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            ///Finished Extra Atachment
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Extra_Costs_Attachment_out=@ExtraA Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (ExtraA == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@ExtraA", ExtraA);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Comment Attachment
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Comment_Attachment_out=@CommA Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (CommA == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@CommA", CommA);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Invoice Attachment
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Invoice_Attachment_out=@InvoiceA Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (InvoiceA == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@InvoiceA", InvoiceA);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Truck
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Truck_out=@Truck Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Truck == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Truck", Truck);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished RIT
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set RIT_out=@RIT Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (RIT == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@RIT", RIT);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Start date
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Start_date_out=@Sdate Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Sdate == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Sdate", Sdate);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished End date
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set End_date_out=@Edate Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Edate == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Edate", Edate);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished KM
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set KM_out=@Km Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (KM == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Km", KValue);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Word days
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Work_days_out=@Wd Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (WorkDays == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Wd", WValue);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Extra Costs
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Extra_Costs_out=@Ec Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Extra == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Ec", Extra);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Invoice
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Invoice_out=@Invoice Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Invoice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Invoice", Invoice);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Comment
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Comment_out=@Comm Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Comm == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Comm", Comm);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Check
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Check_out=@check Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (check == 0)
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Driver
        //                            using (SqlCommand command = new SqlCommand("Update Outgoing_MK_NL Set Driver_out=@Driver Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Driver == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Driver", Driver);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Trailor
        //                            using (SqlCommand command = new SqlCommand("Update Route_Out Set Trailor_out=@Trailor Where Nalog_nr_out=@Nalog", connection))
        //                            {
        //                                if (Trailor == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                    command.Parameters.AddWithValue("@Trailor", Trailor);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            string Lci = outgoing.lci.Text;

        //                            if (Lci == "") { }
        //                            else
        //                            {
        //                                int lc = int.Parse(Lci);
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_Out Set Export_out=@Exp where LCO_id=@lci", connection))
        //                                {
        //                                    if (Exp == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Exp", Exp);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Colli
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_Out Set Colli_Out=@Colli where LCO_id=@lci", connection))
        //                                {
        //                                    if (Colli == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Colli", Colli);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Kg
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_Out Set KG_out=@Kg where LCO_id=@lci", connection))
        //                                {
        //                                    if (Kg == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Kg", IValue);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Import
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_Out Set Importer_out=@Imp where LCO_id=@lci", connection))
        //                                {
        //                                    if (Imp == "")
        //                                    {

        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Imp", Imp);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                                ///Finished Document att
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_Out Set Document_out=@Doc where LCO_id=@lci", connection))
        //                                {
        //                                    if (Doc == "")
        //                                    {
        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Doc", Doc);
        //                                        command.ExecuteNonQuery();

        //                                    }
        //                                }
        //                                ///Finished Trasport invoice
        //                                using (SqlCommand command = new SqlCommand("Update Loading_Company_Out Set Transport_invoice_out=@Trans where LCO_id=@lci", connection))
        //                                {
        //                                    if (Tran == "")
        //                                    {
        //                                    }
        //                                    else
        //                                    {
        //                                        command.Parameters.AddWithValue("@lci", lc);
        //                                        command.Parameters.AddWithValue("@Trans", Tran);
        //                                        command.ExecuteNonQuery();
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {

        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            using (SqlCommand command = new SqlCommand("Insert Into Outgoing_MK_NL(Nalog_nr_out,Truck_out,RIT_out,Start_date_out" +
        //                                ",End_date_out,KM_out,Work_days_out,Extra_Costs_out,Extra_Costs_Attachment_out,Invoice_out,Invoice_Attachment_out,Comment_out," +
        //                                "Comment_Attachment_out,Check_out,Driver_out)values(@Nalog,@Truck,@RIT,@Sdate," +
        //                                "@Edate,@KValue,@WValue,@Extra,@ExtraA,@Invoice,@InvoiceA,@Comm,@CommA,@Check,@Driver)", connection))
        //                            {
        //                                command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                command.Parameters.AddWithValue("@Truck", Truck);
        //                                command.Parameters.AddWithValue("@RIT", RIT);
        //                                command.Parameters.AddWithValue("@Sdate", Sdate);
        //                                command.Parameters.AddWithValue("@Edate", Edate);
        //                                command.Parameters.AddWithValue("@KValue", KValue);
        //                                command.Parameters.AddWithValue("@WValue", WValue);
        //                                command.Parameters.AddWithValue("@Extra", Extra);
        //                                command.Parameters.AddWithValue("ExtraA", ExtraA);
        //                                command.Parameters.AddWithValue("@CommA", CommA);
        //                                command.Parameters.AddWithValue("InvoiceA", InvoiceA);
        //                                command.Parameters.AddWithValue("@Invoice", Invoice);
        //                                command.Parameters.AddWithValue("@Comm", Comm);
        //                                command.Parameters.AddWithValue("@Check", check);
        //                                command.Parameters.AddWithValue("@Driver", Driver);
        //                                command.ExecuteNonQuery();
        //                            }
        //                            connection.Close();
        //                        }
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }
        //        else if (OrderIn != null)
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                con.Open();
        //                string Ordernum = OrderIn.OrderNum.Text;
        //                string To = OrderIn.An.Text;
        //                string Fr = OrderIn.From.Text;
        //                string Com1 = OrderIn.Company1.Text;
        //                string Com2 = OrderIn.Company2.Text;
        //                string phone1 = OrderIn.Phone1.Text;
        //                string phone2 = OrderIn.Phone2.Text;
        //                string email = OrderIn.Email.Text;
        //                string Truck = OrderIn.truck.Text;
        //                string driver = OrderIn.Driver.Text;
        //                string loadDate = OrderIn.LoadingDate.Text;
        //                string loadAdd = OrderIn.LoadingAddress.Text;
        //                string ex = OrderIn.Exporter.Text;
        //                string good = OrderIn.Goods.Text;
        //                string pack = OrderIn.Packing.Text;
        //                string byorder = OrderIn.Byorder.Text;
        //                string imp = OrderIn.Importer.Text;
        //                string offload = OrderIn.Offloadplace.Text;
        //                string freightPrice = OrderIn.FreightPrice.Text;
        //                decimal FpValue;
        //                if (freightPrice == "")
        //                {
        //                    FpValue = 0;
        //                }
        //                else
        //                {
        //                    FpValue = decimal.Parse(freightPrice);
        //                }
        //                string freightPaid = OrderIn.FreightPaid.Text;
        //                string notice = OrderIn.Notice.Text;
        //                string Ref = OrderIn.REF.Text;
        //                string date = OrderIn.Date.Text;
        //                int check;
        //                if (OrderIn.Done.IsChecked == true)
        //                {
        //                    check = 1;
        //                }
        //                else
        //                {
        //                    check = 0;
        //                }
        //                using (SqlCommand commandLoad = new SqlCommand("SELECT COUNT(*) FROM Loading_Order_In WHERE Loading_Order_number_in = @Order", con))
        //                {
        //                    commandLoad.Parameters.AddWithValue("@Order", Ordernum);
        //                    int count = (int)commandLoad.ExecuteScalar();
        //                    if (count > 0)
        //                    {
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            ///Finished To
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set An_Attn_in=@To Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (To == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@To", To);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished From
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Von_From_in=@Fr Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (Fr == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Fr", Fr);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Company 1
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Company_1_in=@Com1 Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (Com1 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Com1", Com1);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Company 2
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Company_2_in=@Com2 Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (Com2 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Com2", Com2);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Phone_1
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Phone_1_number_in=@phone1 Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (phone1 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@phone1", phone1);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Phone_2
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Phone_2_number_in=@phone2 Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (phone2 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@phone2", phone2);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Email
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Email_in=@email Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (email == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@email", email);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Truck
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Truck_plate_in=@truck Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (Truck == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@truck", Truck);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Driver
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Driver_order_in=@driver Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (driver == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@driver", driver);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Loading Date
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Loading_Date_in=@loadDate Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (loadDate == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@loadDate", loadDate);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Loading Address
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Loading_Address_in=@loadAdd Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (loadAdd == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@loadAdd", loadAdd);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Goods
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Goods_order_in=@good Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (good == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@good", good);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Packing
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Packing_in=@pack Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (pack == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@pack", pack);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished By order
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set By_order_in=@byorder Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (byorder == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@byorder", byorder);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Importer
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Importer_order_in=@imp Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (imp == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@imp", imp);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }

        //                            ///Finished Exporter
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Exporter_order_in=@ex Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (ex == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@ex", ex);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Offload
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Offload_in=@offload Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (offload == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@offload", offload);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Frieght Price
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Freight_Price_in=@FpValue Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (freightPrice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@FpValue", FpValue);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Frieght Paid
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Freight_paid_by_in=@freightPaid Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (freightPaid == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@freightPaid", freightPaid);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Notice
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Notice_in=@notice Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (notice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@notice", notice);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished REF
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set REF_number_in=@Ref Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (Ref == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Ref", Ref);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Date
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Date_Document_in=@date Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (date == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@date", date);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Check
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_In Set Check_l=@check Where Loading_Order_number_in=@Ordernum", connection))
        //                            {
        //                                if (check == 0)
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            using (SqlCommand command = new SqlCommand("Insert Into Loading_Order_In(An_Attn_in, Von_From_in, " +
        //                            "Company_1_in,Company_2_in,Phone_1_number_in,Phone_2_number_in,Email_in," +
        //                            "Loading_Order_number_in,Truck_plate_in,Driver_order_in,Loading_Date_in,Loading_Address_in," +
        //                            "Exporter_order_in,Goods_order_in,Packing_in,By_order_in,Importer_order_in," +
        //                            "Offload_in,Freight_Price_in,Freight_paid_by_in,Notice_in,REF_number_in,Date_Document_in,Check_l)" +
        //                            " values (@To,@Fr,@Com1,@Com2,@phone1,@phone2,@email,@Ordernum,@truck,@driver," +
        //                            "@loadDate,@loadAdd,@ex,@good,@pack,@byorder,@imp,@offload,@FpValue," +
        //                            "@freightPaid,@notice,@Ref,@date,@check)", connection))
        //                            {
        //                                command.Parameters.AddWithValue("@To", To);
        //                                command.Parameters.AddWithValue("@Fr", Fr);
        //                                command.Parameters.AddWithValue("@Com1", Com1);
        //                                command.Parameters.AddWithValue("@Com2", Com2);
        //                                command.Parameters.AddWithValue("@phone1", phone1);
        //                                command.Parameters.AddWithValue("@phone2", phone2);
        //                                command.Parameters.AddWithValue("@email", email);
        //                                command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                command.Parameters.AddWithValue("@truck", Truck);
        //                                command.Parameters.AddWithValue("@driver", driver);
        //                                command.Parameters.AddWithValue("@loadDate", loadDate);
        //                                command.Parameters.AddWithValue("@loadAdd", loadAdd);
        //                                command.Parameters.AddWithValue("@ex", ex);
        //                                command.Parameters.AddWithValue("@good", good);
        //                                command.Parameters.AddWithValue("@pack", pack);
        //                                command.Parameters.AddWithValue("@byorder", byorder);
        //                                command.Parameters.AddWithValue("@imp", imp);
        //                                command.Parameters.AddWithValue("@offload", offload);
        //                                command.Parameters.AddWithValue("@FpValue", FpValue);
        //                                command.Parameters.AddWithValue("@freightPaid", freightPaid);
        //                                command.Parameters.AddWithValue("@notice", notice);
        //                                command.Parameters.AddWithValue("@Ref", Ref);
        //                                command.Parameters.AddWithValue("@date", date);
        //                                command.Parameters.AddWithValue("@check", check);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else if (OrderOut != null)
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                con.Open();
        //                string Ordernum = OrderOut.OrderNum.Text;
        //                string To = OrderOut.An.Text;
        //                string Fr = OrderOut.From.Text;
        //                string Com1 = OrderOut.Company1.Text;
        //                string Com2 = OrderOut.Company2.Text;
        //                string phone1 = OrderOut.Phone1.Text;
        //                string phone2 = OrderOut.Phone2.Text;
        //                string email = OrderOut.Email.Text;
        //                string Truck = OrderOut.truck.Text;
        //                string driver = OrderOut.Driver.Text;
        //                string loadDate = OrderOut.LoadingDate.Text;
        //                string loadAdd = OrderOut.LoadingAddress.Text;
        //                string ex = OrderOut.Exporter.Text;
        //                string good = OrderOut.Goods.Text;
        //                string pack = OrderOut.Packing.Text;
        //                string byorder = OrderOut.Byorder.Text;
        //                string imp = OrderOut.Importer.Text;
        //                string offload = OrderOut.Offloadplace.Text;
        //                string freightPrice = OrderOut.FreightPrice.Text;
        //                decimal FpValue;
        //                if (freightPrice == "")
        //                {
        //                    FpValue = 0;
        //                }
        //                else
        //                {
        //                    FpValue = decimal.Parse(freightPrice);
        //                }
        //                string freightPaid = OrderOut.FreightPaid.Text;
        //                string notice = OrderOut.Notice.Text;
        //                string Ref = OrderOut.REF.Text;
        //                string date = OrderOut.Date.Text;
        //                int check;
        //                if (OrderOut.Done.IsChecked == true)
        //                {
        //                    check = 1;
        //                }
        //                else
        //                {
        //                    check = 0;
        //                }
        //                using (SqlCommand commandLoad = new SqlCommand("SELECT COUNT(*) FROM Loading_Order_Out WHERE Loading_Order_number_out = @Order", con))
        //                {
        //                    commandLoad.Parameters.AddWithValue("@Order", Ordernum);
        //                    int count = (int)commandLoad.ExecuteScalar();
        //                    if (count > 0)
        //                    {
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            ///Finished To
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set An_Attn_out=@To Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (To == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@To", To);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished From
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Von_From_out=@Fr Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (Fr == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Fr", Fr);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Company 1
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Company_1_out=@Com1 Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (Com1 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Com1", Com1);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Company 2
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Company_2_out=@Com2 Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (Com2 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Com2", Com2);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Phone_1
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Phone_1_number_out=@phone1 Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (phone1 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@phone1", phone1);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Phone_2
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Phone_2_number_out=@phone2 Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (phone2 == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@phone2", phone2);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Email
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Email_out=@email Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (email == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@email", email);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Truck
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Truck_plate_out=@truck Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (Truck == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@truck", Truck);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Driver
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Driver_order_out=@driver Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (driver == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@driver", driver);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Loading Date
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Loading_Date_out=@loadDate Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (loadDate == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@loadDate", loadDate);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Loading Address
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Loading_Address_out=@loadAdd Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (loadAdd == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@loadAdd", loadAdd);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Goods
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Goods_order_out=@good Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (good == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@good", good);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Packing
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Packing_out=@pack Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (pack == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@pack", pack);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished By order
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set By_order_out=@byorder Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (byorder == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@byorder", byorder);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Importer
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Importer_order_out=@imp Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (imp == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@imp", imp);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }

        //                            ///Finished Exporter
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Exporter_order_out=@ex Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (ex == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@ex", ex);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Offload
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Offload_out=@offload Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (offload == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@offload", offload);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Frieght Price
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Freight_Price_out=@FpValue Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (freightPrice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@FpValue", FpValue);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Frieght Paid
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Freight_paid_by_out=@freightPaid Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (freightPaid == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@freightPaid", freightPaid);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Notice
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Notice_out=@notice Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (notice == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@notice", notice);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished REF
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set REF_number_out=@Ref Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (Ref == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@Ref", Ref);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Date
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Date_Document_out=@date Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (date == "")
        //                                {
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@date", date);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                            ///Finished Check
        //                            using (SqlCommand command = new SqlCommand("Update Loading_Order_Out Set Check_lo=@check Where Loading_Order_number_out=@Ordernum", connection))
        //                            {
        //                                if (check == 0)
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                                else
        //                                {
        //                                    command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                    command.Parameters.AddWithValue("@check", check);
        //                                    command.ExecuteNonQuery();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (SqlConnection connection = new SqlConnection(connectionString))
        //                        {
        //                            connection.Open();
        //                            using (SqlCommand command = new SqlCommand("Insert Into Loading_Order_Out(An_Attn_out, Von_From_out, " +
        //                            "Company_1_out,Company_2_out,Phone_1_number_out,Phone_2_number_out,Email_out," +
        //                            "Loading_Order_number_out,Truck_plate_out,Driver_order_out,Loading_Date_out,Loading_Address_out," +
        //                            "Exporter_order_out,Goods_order_out,Packing_out,By_order_out,Importer_order_out," +
        //                            "Offload_out,Freight_Price_out,Freight_paid_by_out,Notice_out,REF_number_out,Date_Document_out,Check_lo)" +
        //                            " values (@To,@Fr,@Com1,@Com2,@phone1,@phone2,@email,@Ordernum,@truck,@driver," +
        //                            "@loadDate,@loadAdd,@ex,@good,@pack,@byorder,@imp,@offload,@FpValue," +
        //                            "@freightPaid,@notice,@Ref,@date,@check)", connection))
        //                            {
        //                                command.Parameters.AddWithValue("@To", To);
        //                                command.Parameters.AddWithValue("@Fr", Fr);
        //                                command.Parameters.AddWithValue("@Com1", Com1);
        //                                command.Parameters.AddWithValue("@Com2", Com2);
        //                                command.Parameters.AddWithValue("@phone1", phone1);
        //                                command.Parameters.AddWithValue("@phone2", phone2);
        //                                command.Parameters.AddWithValue("@email", email);
        //                                command.Parameters.AddWithValue("@Ordernum", Ordernum);
        //                                command.Parameters.AddWithValue("@truck", Truck);
        //                                command.Parameters.AddWithValue("@driver", driver);
        //                                command.Parameters.AddWithValue("@loadDate", loadDate);
        //                                command.Parameters.AddWithValue("@loadAdd", loadAdd);
        //                                command.Parameters.AddWithValue("@ex", ex);
        //                                command.Parameters.AddWithValue("@good", good);
        //                                command.Parameters.AddWithValue("@pack", pack);
        //                                command.Parameters.AddWithValue("@byorder", byorder);
        //                                command.Parameters.AddWithValue("@imp", imp);
        //                                command.Parameters.AddWithValue("@offload", offload);
        //                                command.Parameters.AddWithValue("@FpValue", FpValue);
        //                                command.Parameters.AddWithValue("@freightPaid", freightPaid);
        //                                command.Parameters.AddWithValue("@notice", notice);
        //                                command.Parameters.AddWithValue("@Ref", Ref);
        //                                command.Parameters.AddWithValue("@date", date);
        //                                command.Parameters.AddWithValue("@check", check);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        if (cost != null)
        //        {
        //            string Nalog = cost.nalog.Text;
        //            string Termmk = cost.termmk1.Text;
        //            string Putmk = cost.putmk1.Text;
        //            string Naftmk = cost.naftmk1.Text;
        //            string Bluemk = cost.bluemk1.Text;
        //            string Nafteu = cost.nafteu1.Text;
        //            string Blueeu = cost.blueeu1.Text;
        //            string Takssrb = cost.takssrb1.Text;
        //            string Putsrb = cost.putsrb1.Text;
        //            string Puthu = cost.puthu1.Text;
        //            string Putsk = cost.putsk1.Text;
        //            string Putcz = cost.putcz1.Text;
        //            string Putcro = cost.putcro1.Text;
        //            string Putslo = cost.putslo1.Text;
        //            string Putat = cost.putat1.Text;
        //            string Putde = cost.putde1.Text;
        //            string Putnl = cost.putnl1.Text;
        //            string Phyto = cost.phyto.Text;
        //            string Md1 = cost.md11.Text;
        //            string Md2 = cost.md21.Text;
        //            string Extra = cost.extra1.Text;
        //            string Termmk2 = cost.termmk2.Text;
        //            string Putmk2 = cost.putmk2.Text;
        //            string Naftmk2 = cost.naftmk2.Text;
        //            string Bluemk2 = cost.bluemk2.Text;
        //            string Nafteu2 = cost.nafteu2.Text;
        //            string Blueeu2 = cost.blueeu2.Text;
        //            string Takssrb2 = cost.taks2.Text;
        //            string Putsrb2 = cost.putsrb2.Text;
        //            string Puthu2 = cost.puthu2.Text;
        //            string Putsk2 = cost.putsk2.Text;
        //            string Putcz2 = cost.putcz2.Text;
        //            string Putcro2 = cost.putcro2.Text;
        //            string Putslo2 = cost.putslo2.Text;
        //            string Putat2 = cost.putat2.Text;
        //            string Putde2 = cost.putde2.Text;
        //            string Putnl2 = cost.putnl2.Text;
        //            string Phyto2 = cost.phyto2.Text;
        //            string Md12 = cost.md12.Text;
        //            string Md22 = cost.md22.Text;
        //            string Extr2 = cost.extra2.Text;
        //            string Truck = cost.truck.Text;
        //            string Driver1 = cost.driver1.Text;
        //            string Driver2 = cost.driver2.Text;
        //            string Naftcostmk = cost.naftcostmk1.Text;
        //            string Naftcostmk2 = cost.naftcostmk2.Text;
        //            string Bluecostmk = cost.bluecostmk1.Text;
        //            string Bluecostmk2 = cost.bluecostmk2.Text;
        //            string Naftcosteu = cost.naftcosteu1.Text;
        //            string Naftcosteu2 = cost.naftcosteu2.Text;
        //            string Bluecosteu = cost.bluecosteu1.Text;
        //            string Bluecosteu2 = cost.bluecosteu2.Text;
        //            string Tel = cost.tel1.Text;
        //            string Tel2 = cost.tel2.Text;
        //            string Name1 = cost.name11.Text;
        //            string Name2 = cost.name21.Text;
        //            string Name3 = cost.name31.Text;
        //            string Name4 = cost.name41.Text;
        //            string Name12 = cost.name12.Text;
        //            string Name22 = cost.name22.Text;
        //            string Name32 = cost.name32.Text;
        //            string Name42 = cost.name42.Text;
        //            string Cost1 = cost.cost11.Text;
        //            string Cost2 = cost.cost21.Text;
        //            string Cost3 = cost.cost31.Text;
        //            string Cost4 = cost.cost41.Text;
        //            string Cost12 = cost.cost12.Text;
        //            string Cost22 = cost.cost22.Text;
        //            string Cost32 = cost.cost32.Text;
        //            string Cost42 = cost.cost42.Text;
        //            string Tot = cost.tot1.Text;
        //            string Tot2 = cost.tot2.Text;
        //            string Alltot = cost.alltot.Text;
        //            int check;
        //            if (cost.Done.IsChecked == true)
        //            {
        //                check = 1;
        //            }
        //            else
        //            {
        //                check = 0;
        //            }
        //            using (SqlConnection con = new SqlConnection(connectionString))
        //            {
        //                con.Open();
        //                using (SqlCommand commandcost = new SqlCommand("SELECT COUNT(*) FROM Cost WHERE Nalog = @Nalog", con))
        //                {
        //                    commandcost.Parameters.AddWithValue("@Nalog", Nalog);
        //                    int count = (int)commandcost.ExecuteScalar();
        //                    if (count > 0)
        //                    {
        //                        ///Finished Truck
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Truck=@truck where Nalog=@nalog", con))
        //                        {
        //                            if (Truck == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@truck", Truck);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Driver1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Driver1=@driver1 where Nalog=@nalog", con))
        //                        {
        //                            if (Driver1 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@driver1", Driver1);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Driver2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Driver2=@driver2 where Nalog=@nalog", con))
        //                        {
        //                            if (Driver2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@driver2", Driver2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Done
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Done=@Check where Nalog=@nalog", con))
        //                        {
        //                            if (check == 0)
        //                            {
        //                                command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                command.Parameters.AddWithValue("@Check", check);
        //                                command.ExecuteNonQuery();
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@Nalog", Nalog);
        //                                command.Parameters.AddWithValue("@Check", check);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Terminal1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Terminal_mk_1=@termmk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Termmk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@termmk1", Termmk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished PutarinaMk1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_mk_1=@putmk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putmk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putmk1", Putmk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft mk liter1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_mk_1_liter=@naftcostmk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Naftcostmk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@naftcostmk1", Naftcostmk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft mk 1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_mk_1=@naftmk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Naftmk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@naftmk1", Naftmk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue mk liter 1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_mk_1_liter=@bluecostmk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Bluecostmk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@bluecostmk1", Bluecostmk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue mk 1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_mk_1=@bluemk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Bluemk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@bluemk1", Bluemk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Taks SRB1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Taks_srb_1=@takssrb1 where Nalog=@nalog", con))
        //                        {
        //                            if (Takssrb == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@takssrb1", Takssrb);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina SRB1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_srb_1=@putsrb1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putsrb == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putsrb1", Putsrb);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina HU1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_hu_1=@puthu1 where Nalog=@nalog", con))
        //                        {
        //                            if (Puthu == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@puthu1", Puthu);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina SK1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_sk_1=@putsk1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putsk == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putsk1", Putsk);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina CZ1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_cz_1=@putcz1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putcz == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putcz1", Putcz);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina CRO1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_cro_1=@putcro1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putcro == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putcro1", Putcro);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina SLO1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_slo_1=@putslo1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putslo == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putslo1", Putslo);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina AT1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_at_1=@putat1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putat == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putat1", Putat);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina DE1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_de_1=@putde1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putde == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putde1", Putde);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina NL1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_nl_1=@putnl1 where Nalog=@nalog", con))
        //                        {
        //                            if (Putnl == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putnl1", Putnl);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }

        //                        ///Finished Phyto1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Phyto_1=@phyto1 where Nalog=@nalog", con))
        //                        {
        //                            if (Phyto == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@phyto1", Phyto);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished telephone1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Telephone_1=@tel1 where Nalog=@nalog", con))
        //                        {
        //                            if (Tel == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@tel1", Tel);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished MD11
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set M1_Shofer_1=@md11 where Nalog=@nalog", con))
        //                        {
        //                            if (Md1 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@md11", Md1);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished MD21
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set M2_Shofer_1=@md21 where Nalog=@nalog", con))
        //                        {
        //                            if (Md2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@md21", Md2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft eu liter1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_eu_1_liter=@naftcosteu1 where Nalog=@nalog", con))
        //                        {
        //                            if (Naftcosteu == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@naftcosteu1", Naftcosteu);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft eu 1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_eu_1=@nafteu1 where Nalog=@nalog", con))
        //                        {
        //                            if (Nafteu == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@nafteu1", Nafteu);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue eu liter 1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_eu_1_liter=@bluecosteu1 where Nalog=@nalog", con))
        //                        {
        //                            if (Bluecosteu == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@bluecosteu1", Bluecosteu);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue eu 1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_eu_1=@blueeu1 where Nalog=@nalog", con))
        //                        {
        //                            if (Blueeu == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@blueeu1", Blueeu);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name11
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank1_name_1=@name11 where Nalog=@nalog", con))
        //                        {
        //                            if (Name1 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name11", Name1);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name21
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank2_name_1=@name21 where Nalog=@nalog", con))
        //                        {
        //                            if (Name2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name21", Name2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name31
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank3_name_1=@name31 where Nalog=@nalog", con))
        //                        {
        //                            if (Name3 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name31", Name3);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name41
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank4_name_1=@name41 where Nalog=@nalog", con))
        //                        {
        //                            if (Name4 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name41", Name4);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost11
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank1_cost_1=@cost11 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost1 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost11", Cost1);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost21
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank2_cost_1=@cost21 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost21", Cost2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost31
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank3_cost_1=@cost31 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost3 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost31", Cost3);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost41
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank4_cost_1=@cost41 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost4 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost41", Cost4);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Tot1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Total_1=@tot1 where Nalog=@nalog", con))
        //                        {
        //                            if (Tot == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@tot1", Tot);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Terminal2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Terminal_mk_2=@termmk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Termmk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@termmk2", Termmk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished PutarinaMk2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_mk_2=@putmk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putmk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putmk2", Putmk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft mk liter2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_mk_2_liter=@naftcostmk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Naftcostmk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@naftcostmk2", Naftcostmk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft mk 2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_mk_2=@naftmk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Naftmk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@naftmk2", Naftmk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue mk liter 2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_mk_2_liter=@bluecostmk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Bluecostmk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@bluecostmk2", Bluecostmk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue mk 2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_mk_2=@bluemk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Bluemk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@bluemk2", Bluemk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Taks SRB2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Taks_srb_2=@takssrb2 where Nalog=@nalog", con))
        //                        {
        //                            if (Takssrb2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@takssrb2", Takssrb2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina SRB2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_srb_2=@putsrb2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putsrb2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putsrb2", Putsrb2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina HU2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_hu_2=@puthu2 where Nalog=@nalog", con))
        //                        {
        //                            if (Puthu2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@puthu2", Puthu2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina SK2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_sk_2=@putsk2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putsk2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putsk2", Putsk2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina CZ2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_cz_2=@putcz2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putcz2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putcz2", Putcz2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina CRO2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_cro_2=@putcro2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putcro2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putcro2", Putcro2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina SLO2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_slo_2=@putslo2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putslo2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putslo2", Putslo2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina AT2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_at_2=@putat2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putat2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putat2", Putat2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina DE2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_de_2=@putde2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putde2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putde2", Putde2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Putarina NL2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Putarina_nl_2=@putnl2 where Nalog=@nalog", con))
        //                        {
        //                            if (Putnl2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@putnl2", Putnl2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Phyto2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Phyto_2=@phyto2 where Nalog=@nalog", con))
        //                        {
        //                            if (Phyto2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@phyto2", Phyto2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished telephone2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Telephone_2=@tel2 where Nalog=@nalog", con))
        //                        {
        //                            if (Tel2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@tel2", Tel2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished MD12
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set M1_Shofer_2=@md12 where Nalog=@nalog", con))
        //                        {
        //                            if (Md12 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@md12", Md12);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished MD22
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set M2_Shofer_2=@md22 where Nalog=@nalog", con))
        //                        {
        //                            if (Md22 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@md22", Md22);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft eu liter2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_eu_2_liter=@naftcosteu2 where Nalog=@nalog", con))
        //                        {
        //                            if (Naftcosteu2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@naftcosteu2", Naftcosteu2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Naft eu 2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Naft_eu_2=@nafteu2 where Nalog=@nalog", con))
        //                        {
        //                            if (Nafteu2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@nafteu2", Nafteu2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue eu liter 2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_eu_2_liter=@bluecosteu2 where Nalog=@nalog", con))
        //                        {
        //                            if (Bluecosteu2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@bluecosteu2", Bluecosteu2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Adblue eu 2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Adblue_eu_2=@blueeu2 where Nalog=@nalog", con))
        //                        {
        //                            if (Blueeu2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@blueeu2", Blueeu2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name12
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank1_name_2=@name12 where Nalog=@nalog", con))
        //                        {
        //                            if (Name12 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name12", Name12);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name22
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank2_name_2=@name22 where Nalog=@nalog", con))
        //                        {
        //                            if (Name22 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name22", Name22);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name32
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank3_name_2=@name32 where Nalog=@nalog", con))
        //                        {
        //                            if (Name32 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name32", Name32);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Name42
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank4_name_2=@name42 where Nalog=@nalog", con))
        //                        {
        //                            if (Name42 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@name42", Name42);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost12
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank1_cost_2=@cost12 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost12 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost12", Cost12);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost22
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank2_cost_2=@cost22 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost22 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost22", Cost22);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost32
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank3_cost_2=@cost32 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost32 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost32", Cost32);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Cost42
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set blank4_cost_2=@cost42 where Nalog=@nalog", con))
        //                        {
        //                            if (Cost42 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@cost42", Cost42);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Tot2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Total_2=@tot2 where Nalog=@nalog", con))
        //                        {
        //                            if (Tot2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@tot2", Tot2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished All Tot
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set All_Total=@alltot where Nalog=@nalog", con))
        //                        {
        //                            if (Alltot == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@alltot", Alltot);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Extra1
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Extra_Sh_1=@extra1 where Nalog=@nalog", con))
        //                        {
        //                            if (Extra == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@extra1", Extra);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                        ///Finished Extra2
        //                        using (SqlCommand command = new SqlCommand("Update Cost Set Extra_Sh_2=@extra2 where Nalog=@nalog", con))
        //                        {
        //                            if (Extr2 == "")
        //                            {
        //                            }
        //                            else
        //                            {
        //                                command.Parameters.AddWithValue("@nalog", Nalog);
        //                                command.Parameters.AddWithValue("@extra2", Extr2);
        //                                command.ExecuteNonQuery();
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Cost (Nalog, Truck, Driver1, Driver2, Done, Terminal_mk_1, Putarina_mk_1, Naft_mk_1_liter, Naft_mk_1, Adblue_mk_1_liter, Adblue_mk_1, Taks_srb_1, Putarina_srb_1, Putarina_hu_1," +
        //                                               " Putarina_sk_1, Putarina_cz_1, Putarina_cro_1, Putarina_slo_1, Putarina_at_1, Putarina_de_1, Putarina_nl_1, Phyto_1, Telephone_1, M1_Shofer_1, M2_Shofer_1, Extra_Sh_1, Naft_eu_1_liter, Naft_eu_1, Adblue_eu_1_liter, Adblue_eu_1, Blank1_name_1," +
        //                                               " Blank1_cost_1, Blank2_name_1, Blank2_cost_1, Blank3_name_1, Blank3_cost_1, Blank4_name_1, Blank4_cost_1, Total_1, Terminal_mk_2, Putarina_mk_2, Naft_mk_2_liter, Naft_mk_2, Adblue_mk_2_liter, Adblue_mk_2, Taks_srb_2, Putarina_srb_2, Putarina_hu_2, " +
        //                                               "Putarina_sk_2, Putarina_cz_2, Putarina_cro_2, Putarina_slo_2, Putarina_at_2, Putarina_de_2, Putarina_nl_2, Phyto_2, Telephone_2, M1_Shofer_2, M2_Shofer_2, Extra_Sh_2, Naft_eu_2_liter, Naft_eu_2, Adblue_eu_2_liter, Adblue_eu_2, Blank1_name_2, Blank1_cost_2, " +
        //                                               "Blank2_name_2, Blank2_cost_2, Blank3_name_2, Blank3_cost_2, Blank4_name_2, Blank4_cost_2, Total_2, All_Total) VALUES" +
        //                                               " (@nalog, @truck, @driver1, @driver2, @done, @termmk1, @putmk1, @naftcostmk1, @naftmk1, @bluecostmk1, @bluemk1, @takssrb1, @putsrb1, @puthu1, @putsk1," +
        //                                               " @putcz1, @putcro1, @putslo1, @putat1, @putde1, @putnl1, @phyto1, @tel1, @md11, @md21, @extra1, " +
        //                                               "@naftcosteu1, @nafteu1, @bluecosteu1, @blueeu1, @name11, @cost11, @name21, @cost21, @name31, @cost31, @name41, " +
        //                                               "@cost41, @tot1, @termmk2, @putmk2, @naftcostmk2, @naftmk2, @bluecostmk2, @bluemk2, @takssrb2, @putsrb2, @puthu2, " +
        //                                               "@putsk2, @putcz2, @putcro2,@putslo2, @putat2,@putde2,@putnl2,@phyto2,@tel2,@md12,@md22,@extra2,@naftcosteu2,@nafteu2," +
        //                                               "@bluecosteu2,@blueeu2,@name12,@cost12,@name22,@cost22,@name32,@cost32,@name42,@cost42,@tot2, @alltot)", con))
        //                        {
        //                            command.Parameters.AddWithValue("@Nalog", Nalog);
        //                            command.Parameters.AddWithValue("@extra1", Extra);
        //                            command.Parameters.AddWithValue("@extra2", Extr2);
        //                            command.Parameters.AddWithValue("@done", check);
        //                            command.Parameters.AddWithValue("@truck", Truck);
        //                            command.Parameters.AddWithValue("@driver1", Driver1);
        //                            command.Parameters.AddWithValue("@driver2", Driver2);
        //                            command.Parameters.AddWithValue("@termmk1", Termmk);
        //                            command.Parameters.AddWithValue("@putmk1", Putmk);
        //                            command.Parameters.AddWithValue("@naftcostmk1", Naftcostmk);
        //                            command.Parameters.AddWithValue("@naftmk1", Naftmk);
        //                            command.Parameters.AddWithValue("@bluecostmk1", Bluecostmk);
        //                            command.Parameters.AddWithValue("@bluemk1", Bluemk);
        //                            command.Parameters.AddWithValue("@takssrb1", Takssrb);
        //                            command.Parameters.AddWithValue("@putsrb1", Putsrb);
        //                            command.Parameters.AddWithValue("@puthu1", Puthu);
        //                            command.Parameters.AddWithValue("@putsk1", Putsk);
        //                            command.Parameters.AddWithValue("@putcz1", Putcz);
        //                            command.Parameters.AddWithValue("@putcro1", Putcro);
        //                            command.Parameters.AddWithValue("@putslo1", Putslo);
        //                            command.Parameters.AddWithValue("@putat1", Putat);
        //                            command.Parameters.AddWithValue("@putde1", Putde);
        //                            command.Parameters.AddWithValue("@putnl1", Putnl);
        //                            command.Parameters.AddWithValue("@phyto1", Phyto);
        //                            command.Parameters.AddWithValue("@tel1", Tel);
        //                            command.Parameters.AddWithValue("@md11", Md1);
        //                            command.Parameters.AddWithValue("@md21", Md2);
        //                            command.Parameters.AddWithValue("@naftcosteu1", Naftcosteu);
        //                            command.Parameters.AddWithValue("@nafteu1", Nafteu);
        //                            command.Parameters.AddWithValue("@bluecosteu1", Bluecosteu);
        //                            command.Parameters.AddWithValue("@blueeu1", Blueeu);
        //                            command.Parameters.AddWithValue("@name11", Name1);
        //                            command.Parameters.AddWithValue("@name21", Name2);
        //                            command.Parameters.AddWithValue("@name31", Name3);
        //                            command.Parameters.AddWithValue("@name41", Name4);
        //                            command.Parameters.AddWithValue("@cost11", Cost1);
        //                            command.Parameters.AddWithValue("@cost21", Cost2);
        //                            command.Parameters.AddWithValue("@cost31", Cost3);
        //                            command.Parameters.AddWithValue("@cost41", Cost4);
        //                            command.Parameters.AddWithValue("@tot1", Tot);
        //                            command.Parameters.AddWithValue("@termmk2", Termmk2);
        //                            command.Parameters.AddWithValue("@putmk2", Putmk2);
        //                            command.Parameters.AddWithValue("@naftcostmk2", Naftcostmk2);
        //                            command.Parameters.AddWithValue("@naftmk2", Naftmk2);
        //                            command.Parameters.AddWithValue("@bluecostmk2", Bluecostmk2);
        //                            command.Parameters.AddWithValue("@bluemk2", Bluemk2);
        //                            command.Parameters.AddWithValue("@takssrb2", Takssrb2);
        //                            command.Parameters.AddWithValue("@putsrb2", Putsrb2);
        //                            command.Parameters.AddWithValue("@puthu2", Puthu2);
        //                            command.Parameters.AddWithValue("@putsk2", Putsk2);
        //                            command.Parameters.AddWithValue("@putcz2", Putcz2);
        //                            command.Parameters.AddWithValue("@putcro2", Putcro2);
        //                            command.Parameters.AddWithValue("@putslo2", Putslo2);
        //                            command.Parameters.AddWithValue("@putat2", Putat2);
        //                            command.Parameters.AddWithValue("@putde2", Putde2);
        //                            command.Parameters.AddWithValue("@putnl2", Putnl2);
        //                            command.Parameters.AddWithValue("@phyto2", Phyto2);
        //                            command.Parameters.AddWithValue("@tel2", Tel2);
        //                            command.Parameters.AddWithValue("@md12", Md12);
        //                            command.Parameters.AddWithValue("@md22", Md22);
        //                            command.Parameters.AddWithValue("@naftcosteu2", Naftcosteu2);
        //                            command.Parameters.AddWithValue("@nafteu2", Nafteu2);
        //                            command.Parameters.AddWithValue("@bluecosteu2", Bluecosteu2);
        //                            command.Parameters.AddWithValue("@blueeu2", Blueeu2);
        //                            command.Parameters.AddWithValue("@name12", Name12);
        //                            command.Parameters.AddWithValue("@name22", Name22);
        //                            command.Parameters.AddWithValue("@name32", Name32);
        //                            command.Parameters.AddWithValue("@name42", Name42);
        //                            command.Parameters.AddWithValue("@cost12", Cost12);
        //                            command.Parameters.AddWithValue("@cost22", Cost22);
        //                            command.Parameters.AddWithValue("@cost32", Cost32);
        //                            command.Parameters.AddWithValue("@cost42", Cost42);
        //                            command.Parameters.AddWithValue("@tot2", Tot2);
        //                            command.Parameters.AddWithValue("@alltot", Alltot);
        //                            command.ExecuteNonQuery();
        //                        }
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("No rows are present", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("An error occurred while updating the content.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        public void ShowIncoming(ContentControl cc)
        {
            cc.Content = new Incoming();
        }
        public void ShowOutgoing(ContentControl cc)
        {
            cc.Content = new Outgoing();
        }
        public void ShowInland(ContentControl cc)
        {
            cc.Content = new Inland();
        }
        public void ShowCost(ContentControl cc)
        {
            cc.Content = new Cost();
        }
        public void ShowWeek(ContentControl cc)
        {
            cc.Content = new Week();
        }
        public void ShowOrderIn(ContentControl cc)
        {
            cc.Content = new OrderIn();
        }
        public void ShowOrderOut(ContentControl cc)
        {
            cc.Content = new OrderOut();
        }
        public void ShowCombo(ContentControl cc)
        {
            cc.Content = new ComboEdit();
        }

    }
}
