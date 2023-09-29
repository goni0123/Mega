using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using CheckBox = System.Windows.Controls.CheckBox;
using ComboBox = System.Windows.Controls.ComboBox;
using TextBox = System.Windows.Controls.TextBox;

namespace MegaNew.ViewModels
{
    public class CostEditViewModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        public void Fill(int Nalog, ComboBox truck, TextBox driver1, TextBox driver2, TextBox termmk1, TextBox termmk2,
            TextBox putmk1, TextBox putmk2, TextBox naftmk1, TextBox naftmk2, TextBox naftcostmk1, TextBox naftcostmk2,
            TextBox bluecostmk1, TextBox bluecostmk2, TextBox bluemk1, TextBox bluemk2, TextBox nafteu1, TextBox nafteu2,
            TextBox naftcosteu1, TextBox naftcosteu2, TextBox bluecosteu1, TextBox bluecosteu2, TextBox blueeu1,
            TextBox blueeu2, TextBox takssrb1, TextBox taks2, TextBox putsrb1, TextBox putsrb2, TextBox puthu1,
            TextBox puthu2, TextBox putsk1, TextBox putsk2, TextBox putcz1, TextBox putcz2, TextBox putcro1,
            TextBox putcro2, TextBox putslo1, TextBox putslo2, TextBox putat1, TextBox putat2, TextBox putde1,
            TextBox putde2, TextBox putnl1, TextBox putnl2, TextBox phyto, TextBox phyto2, TextBox tel1, TextBox tel2,
            TextBox md11, TextBox md12, TextBox md21, TextBox md22, TextBox extra1, TextBox extra2, TextBox tot1, TextBox tot2,
            TextBox alltot, TextBox name11, TextBox name12, TextBox name21, TextBox name22, TextBox name31, TextBox name32, TextBox name41,
            TextBox name42, TextBox cost11, TextBox cost12, TextBox cost21, TextBox cost22, TextBox cost31, TextBox cost32, TextBox cost41, TextBox cost42,TextBox currency)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Cost where Nalog=@Nalog", connection))
                    {
                        command.Parameters.AddWithValue("@Nalog", Nalog);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Truck = reader["Truck"].ToString();
                                string Driver1 = reader["Driver1"].ToString();
                                string Driver2 = reader["Driver2"].ToString();
                                string Termmk1 = reader["Terminal_mk_1"].ToString();
                                string Putmk1 = reader["Putarina_mk_1"].ToString();
                                string Naftmk1 = reader["Naft_mk_1"].ToString();
                                string Naftcostmk1 = reader["Naft_mk_1_liter"].ToString();
                                string Bluemk1 = reader["Adblue_mk_1"].ToString();
                                string Bluecostmk1 = reader["Adblue_mk_1_liter"].ToString();
                                string Takssrb1 = reader["Taks_srb_1"].ToString();
                                string Putsrb1 = reader["Putarina_srb_1"].ToString();
                                string Puthu1 = reader["Putarina_hu_1"].ToString();
                                string Putsk1 = reader["Putarina_sk_1"].ToString();
                                string Putcz1 = reader["Putarina_cz_1"].ToString();
                                string Putcro1 = reader["Putarina_cro_1"].ToString();
                                string Putslo1 = reader["Putarina_slo_1"].ToString();
                                string Putat1 = reader["Putarina_at_1"].ToString();
                                string Putde1 = reader["Putarina_de_1"].ToString();
                                string Putnl1 = reader["Putarina_nl_1"].ToString();
                                string Phyto1 = reader["Phyto_1"].ToString();
                                string Tel1 = reader["Telephone_1"].ToString();
                                string Md11 = reader["M1_Shofer_1"].ToString();
                                string Md21 = reader["M2_Shofer_1"].ToString();
                                string Extra1 = reader["Extra_Sh_1"].ToString();
                                string Nafteu1 = reader["Naft_eu_1"].ToString();
                                string Naftcosteu1 = reader["Naft_eu_1_liter"].ToString();
                                string Blueeu1 = reader["Adblue_eu_1"].ToString();
                                string Bluecosteu1 = reader["Adblue_eu_1_liter"].ToString();
                                string Blankname11 = reader["Blank1_name_1"].ToString();
                                string Blankname21 = reader["Blank2_name_1"].ToString();
                                string Blankname31 = reader["Blank3_name_1"].ToString();
                                string Blankname41 = reader["Blank4_name_1"].ToString();
                                string Blankcost11 = reader["Blank1_cost_1"].ToString();
                                string Blankcost21 = reader["Blank2_cost_1"].ToString();
                                string Blankcost31 = reader["Blank3_cost_1"].ToString();
                                string Blankcost41 = reader["Blank4_cost_1"].ToString();
                                string Tot1 = reader["Total_1"].ToString();
                                string Termmk2 = reader["Terminal_mk_2"].ToString();
                                string Putmk2 = reader["Putarina_mk_2"].ToString();
                                string Naftmk2 = reader["Naft_mk_2"].ToString();
                                string Naftcostmk2 = reader["Naft_mk_2_liter"].ToString();
                                string Bluemk2 = reader["Adblue_mk_2"].ToString();
                                string Bluecostmk2 = reader["Adblue_mk_2_liter"].ToString();
                                string Takssrb2 = reader["Taks_srb_2"].ToString();
                                string Putsrb2 = reader["Putarina_srb_2"].ToString();
                                string Puthu2 = reader["Putarina_hu_2"].ToString();
                                string Putsk2 = reader["Putarina_sk_2"].ToString();
                                string Putcz2 = reader["Putarina_cz_2"].ToString();
                                string Putcro2 = reader["Putarina_cro_2"].ToString();
                                string Putslo2 = reader["Putarina_slo_2"].ToString();
                                string Putat2 = reader["Putarina_at_2"].ToString();
                                string Putde2 = reader["Putarina_de_2"].ToString();
                                string Putnl2 = reader["Putarina_nl_2"].ToString();
                                string Phyto2 = reader["Phyto_2"].ToString();
                                string Tel2 = reader["Telephone_2"].ToString();
                                string Md12 = reader["M1_Shofer_2"].ToString();
                                string Md22 = reader["M2_Shofer_2"].ToString();
                                string Extra2 = reader["Extra_Sh_2"].ToString();
                                string Nafteu2 = reader["Naft_eu_2"].ToString();
                                string Naftcosteu2 = reader["Naft_eu_2_liter"].ToString();
                                string Blueeu2 = reader["Adblue_eu_2"].ToString();
                                string Bluecosteu2 = reader["Adblue_eu_2_liter"].ToString();
                                string Blankname12 = reader["Blank1_name_2"].ToString();
                                string Blankname22 = reader["Blank2_name_2"].ToString();
                                string Blankname32 = reader["Blank3_name_2"].ToString();
                                string Blankname42 = reader["Blank4_name_2"].ToString();
                                string Blankcost12 = reader["Blank1_cost_2"].ToString();
                                string Blankcost22 = reader["Blank2_cost_2"].ToString();
                                string Blankcost32 = reader["Blank3_cost_2"].ToString();
                                string Blankcost42 = reader["Blank4_cost_2"].ToString();
                                string Tot2 = reader["Total_2"].ToString();
                                string Tota = reader["All_Total"].ToString();
                                string Currency = reader["currency"].ToString();
                                truck.Text = Truck;
                                driver1.Text = Driver1;
                                driver2.Text = Driver2;
                                termmk1.Text = Termmk1;
                                termmk2.Text = Termmk2;
                                putmk1.Text = Putmk1;
                                putmk2.Text = Putmk2;
                                naftmk1.Text = Naftmk1;
                                naftmk2.Text = Naftmk2;
                                naftcostmk1.Text = Naftcostmk1;
                                naftcostmk2.Text = Naftcostmk2;
                                bluecostmk1.Text = Bluecostmk1;
                                bluecostmk2.Text = Bluecostmk2;
                                bluemk1.Text = Bluemk1;
                                bluemk2.Text = Bluemk2;
                                nafteu1.Text = Nafteu1;
                                nafteu2.Text = Nafteu2;
                                naftcosteu1.Text = Naftcosteu1;
                                naftcosteu2.Text = Naftcosteu2;
                                bluecosteu1.Text = Bluecosteu1;
                                bluecosteu2.Text = Bluecosteu2;
                                blueeu1.Text = Blueeu1;
                                blueeu2.Text = Blueeu2;
                                takssrb1.Text = Takssrb1;
                                taks2.Text = Takssrb2;
                                putsrb1.Text = Putsrb1;
                                putsrb2.Text = Putsrb2;
                                puthu1.Text = Puthu1;
                                puthu2.Text = Puthu2;
                                putsk1.Text = Putsk1;
                                putsk2.Text = Putsk2;
                                putcz1.Text = Putcz1;
                                putcz2.Text = Putcz2;
                                putcro1.Text = Putcro1;
                                putcro2.Text = Putcro2;
                                putslo1.Text = Putslo1;
                                putslo2.Text = Putslo2;
                                putat1.Text = Putat1;
                                putat2.Text = Putat2;
                                putde1.Text = Putde1;
                                putde2.Text = Putde2;
                                putnl1.Text = Putnl1;
                                putnl2.Text = Putnl2;
                                phyto.Text = Phyto1;
                                phyto2.Text = Phyto2;
                                tel1.Text = Tel1;
                                tel2.Text = Tel2;
                                md11.Text = Md11;
                                md12.Text = Md12;
                                md21.Text = Md21;
                                md22.Text = Md22;
                                extra1.Text = Extra1;
                                extra2.Text = Extra2;
                                tot1.Text = Tot1;
                                tot2.Text = Tot2;
                                alltot.Text = Tota;
                                name11.Text = Blankname11;
                                name12.Text = Blankname12;
                                name21.Text = Blankname21;
                                name22.Text = Blankname22;
                                name31.Text = Blankname31;
                                name32.Text = Blankname32;
                                name41.Text = Blankname41;
                                name42.Text = Blankname42;
                                cost11.Text = Blankcost11;
                                cost12.Text = Blankcost12;
                                cost21.Text = Blankcost21;
                                cost22.Text = Blankcost22;
                                cost31.Text = Blankcost31;
                                cost32.Text = Blankcost32;
                                cost41.Text = Blankcost41;
                                cost42.Text = Blankcost42;
                                currency.Text = Currency;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data: " + ex.Message);
            }
        }
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
        public int CalculateTotal1(TextBox termmk, TextBox putmk, TextBox naftmk, TextBox bluemk, TextBox nafteu, TextBox blueeu,
            TextBox taksrb, TextBox putsrb, TextBox puthu, TextBox putsk, TextBox putcz, TextBox putcro, TextBox putslo, TextBox putat,
            TextBox putde, TextBox putnl, TextBox phyto, TextBox md1, TextBox md2, TextBox extra, TextBox cost1, TextBox cost2,
            TextBox cost3, TextBox cost4, TextBox tel)
        {
            int trmk = GetIntValue(termmk);
            int mk = GetIntValue(putmk);
            int ftmk = GetIntValue(naftmk);
            int uemk = GetIntValue(bluemk);
            int aksrb = GetIntValue(taksrb);
            int fteu = GetIntValue(nafteu);
            int ueeu = GetIntValue(blueeu);
            int srb = GetIntValue(putsrb);
            int hu = GetIntValue(puthu);
            int sk = GetIntValue(putsk);
            int cz = GetIntValue(putcz);
            int cro = GetIntValue(putcro);
            int slo = GetIntValue(putslo);
            int at = GetIntValue(putat);
            int de = GetIntValue(putde);
            int nl = GetIntValue(putnl);
            int d1 = GetIntValue(md1);
            int d2 = GetIntValue(md2);
            int ex = GetIntValue(extra);
            int co1 = GetIntValue(cost1);
            int co2 = GetIntValue(cost2);
            int co3 = GetIntValue(cost3);
            int co4 = GetIntValue(cost4);
            int py = GetIntValue(phyto);
            int telValue = GetIntValue(tel);

            int sum = trmk + py + mk + ftmk + uemk + fteu + ueeu + aksrb + srb + hu + sk + cz + cro + slo + at + de + nl + d1 + d2 + ex + co1 + co2 + co3 + co4 + telValue;

            return sum;
        }
        public int CalculateTotal2(TextBox termmk2, TextBox putmk2, TextBox naftmk2, TextBox bluemk2, TextBox nafteu2, TextBox blueeu2,
            TextBox taksrb2, TextBox putsrb2, TextBox puthu2, TextBox putsk2, TextBox putcz2, TextBox putcro2, TextBox putslo2, TextBox putat2,
            TextBox putde2, TextBox putnl2, TextBox phyto2, TextBox md12, TextBox md22, TextBox extra2, TextBox cost12, TextBox cost22,
            TextBox cost32, TextBox cost42, TextBox tel2)
        {
            int trmk = GetIntValue(termmk2);
            int mk = GetIntValue(putmk2);
            int ftmk = GetIntValue(naftmk2);
            int uemk = GetIntValue(bluemk2);
            int aksrb = GetIntValue(taksrb2);
            int fteu = GetIntValue(nafteu2);
            int ueeu = GetIntValue(blueeu2);
            int srb = GetIntValue(putsrb2);
            int hu = GetIntValue(puthu2);
            int sk = GetIntValue(putsk2);
            int cz = GetIntValue(putcz2);
            int cro = GetIntValue(putcro2);
            int slo = GetIntValue(putslo2);
            int at = GetIntValue(putat2);
            int de = GetIntValue(putde2);
            int nl = GetIntValue(putnl2);
            int d1 = GetIntValue(md12);
            int d2 = GetIntValue(md22);
            int ex = GetIntValue(extra2);
            int co1 = GetIntValue(cost12);
            int co2 = GetIntValue(cost22);
            int co3 = GetIntValue(cost32);
            int co4 = GetIntValue(cost42);
            int py = GetIntValue(phyto2);
            int telValue = GetIntValue(tel2);

            int sum = trmk + py + mk + ftmk + uemk + fteu + ueeu + aksrb + srb + hu + sk + cz + cro + slo + at + de + nl + d1 + d2 + ex + co1 + co2 + co3 + co4 + telValue;

            return sum;
        }
        public int AllTotal(int tot1, int tot2)
        {
            return tot1 + tot2;
        }
        private int GetIntValue(TextBox textBox)
        {
            if (int.TryParse(textBox.Text, out int value))
            {
                return value;
            }
            return 0;
        }
        public void Submit(int Nalog, ComboBox truck, TextBox driver1, TextBox driver2, TextBox termmk1, TextBox termmk2,
            TextBox putmk1, TextBox putmk2, TextBox naftmk1, TextBox naftmk2, TextBox naftcostmk1, TextBox naftcostmk2,
            TextBox bluecostmk1, TextBox bluecostmk2, TextBox bluemk1, TextBox bluemk2, TextBox nafteu1, TextBox nafteu2,
            TextBox naftcosteu1, TextBox naftcosteu2, TextBox bluecosteu1, TextBox bluecosteu2, TextBox blueeu1,
            TextBox blueeu2, TextBox takssrb1, TextBox taks2, TextBox putsrb1, TextBox putsrb2, TextBox puthu1,
            TextBox puthu2, TextBox putsk1, TextBox putsk2, TextBox putcz1, TextBox putcz2, TextBox putcro1,
            TextBox putcro2, TextBox putslo1, TextBox putslo2, TextBox putat1, TextBox putat2, TextBox putde1,
            TextBox putde2, TextBox putnl1, TextBox putnl2, TextBox phyto, TextBox phyto2, TextBox tel1, TextBox tel2,
            TextBox md11, TextBox md12, TextBox md21, TextBox md22, TextBox extra1, TextBox extra2, TextBox tot1, TextBox tot2,
            TextBox alltot, TextBox name11, TextBox name12, TextBox name21, TextBox name22, TextBox name31, TextBox name32, TextBox name41,
            TextBox name42, TextBox cost11, TextBox cost12, TextBox cost21, TextBox cost22, TextBox cost31, TextBox cost32, TextBox cost41, TextBox cost42, CheckBox Done,TextBox currency)
        {
            try
            {
                string Termmk = termmk1.Text;
                string Putmk = putmk1.Text;
                string Naftmk = naftmk1.Text;
                string Bluemk = bluemk1.Text;
                string Nafteu = nafteu1.Text;
                string Blueeu = blueeu1.Text;
                string Takssrb = takssrb1.Text;
                string Putsrb = putsrb1.Text;
                string Puthu = puthu1.Text;
                string Putsk = putsk1.Text;
                string Putcz = putcz1.Text;
                string Putcro = putcro1.Text;
                string Putslo = putslo1.Text;
                string Putat = putat1.Text;
                string Putde = putde1.Text;
                string Putnl = putnl1.Text;
                string Phyto = phyto.Text;
                string Md1 = md11.Text;
                string Md2 = md21.Text;
                string Extra = extra1.Text;
                string Termmk2 = termmk2.Text;
                string Putmk2 = putmk2.Text;
                string Naftmk2 = naftmk2.Text;
                string Bluemk2 = bluemk2.Text;
                string Nafteu2 = nafteu2.Text;
                string Blueeu2 = blueeu2.Text;
                string Takssrb2 = taks2.Text;
                string Putsrb2 = putsrb2.Text;
                string Puthu2 = puthu2.Text;
                string Putsk2 = putsk2.Text;
                string Putcz2 = putcz2.Text;
                string Putcro2 = putcro2.Text;
                string Putslo2 = putslo2.Text;
                string Putat2 = putat2.Text;
                string Putde2 = putde2.Text;
                string Putnl2 = putnl2.Text;
                string Phyto2 = phyto2.Text;
                string Md12 = md12.Text;
                string Md22 = md22.Text;
                string Extr2 = extra2.Text;
                string Truck = truck.Text;
                string Driver1 = driver1.Text;
                string Driver2 = driver2.Text;
                string Naftcostmk = naftcostmk1.Text;
                string Naftcostmk2 = naftcostmk2.Text;
                string Bluecostmk = bluecostmk1.Text;
                string Bluecostmk2 = bluecostmk2.Text;
                string Naftcosteu = naftcosteu1.Text;
                string Naftcosteu2 = naftcosteu2.Text;
                string Bluecosteu = bluecosteu1.Text;
                string Bluecosteu2 = bluecosteu2.Text;
                string Tel = tel1.Text;
                string Tel2 = tel2.Text;
                string Name1 = name11.Text;
                string Name2 = name21.Text;
                string Name3 = name31.Text;
                string Name4 = name41.Text;
                string Name12 = name12.Text;
                string Name22 = name22.Text;
                string Name32 = name32.Text;
                string Name42 = name42.Text;
                string Cost1 = cost11.Text;
                string Cost2 = cost21.Text;
                string Cost3 = cost31.Text;
                string Cost4 = cost41.Text;
                string Cost12 = cost12.Text;
                string Cost22 = cost22.Text;
                string Cost32 = cost32.Text;
                string Cost42 = cost42.Text;
                string Tot = tot1.Text;
                string Tot2 = tot2.Text;
                string Alltot = alltot.Text;
                string Currency = currency.Text;
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
                    using (SqlCommand command = new SqlCommand(@"UPDATE dbo.Cost
SET Truck = @truck,
    Driver1 = @driver1,
    Driver2 = @driver2,
    Done = @done,
    Terminal_mk_1 = @termmk1,
    Putarina_mk_1 = @putmk1,
    Naft_mk_1_liter = @naftcostmk1,
    Naft_mk_1 = @naftmk1,
    Adblue_mk_1_liter = @bluecostmk1,
    Adblue_mk_1 = @bluemk1,
    Taks_srb_1 = @takssrb1,
    Putarina_srb_1 = @putsrb1,
    Putarina_hu_1 = @puthu1,
    Putarina_sk_1 = @putsk1,
    Putarina_cz_1 = @putcz1,
    Putarina_cro_1 = @putcro1,
    Putarina_slo_1 = @putslo1,
    Putarina_at_1 = @putat1,
    Putarina_de_1 = @putde1,
    Putarina_nl_1 = @putnl1,
    Phyto_1 = @phyto1,
    Telephone_1 = @tel1,
    M1_Shofer_1 = @md11,
    M2_Shofer_1 = @md21,
    Extra_Sh_1 = @extra1,
    Naft_eu_1_liter = @naftcosteu1,
    Naft_eu_1 = @nafteu1,
    Adblue_eu_1_liter = @bluecosteu1,
    Adblue_eu_1 = @blueeu1,
    Blank1_name_1 = @name11,
    Blank1_cost_1 = @cost11,
    Blank2_name_1 = @name21,
    Blank2_cost_1 = @cost21,
    Blank3_name_1 = @name31,
    Blank3_cost_1 = @cost31,
    Blank4_name_1 = @name41,
    Blank4_cost_1 = @cost41,
    Total_1 = @tot1,
    Terminal_mk_2 = @termmk2,
    Putarina_mk_2 = @putmk2,
    Naft_mk_2_liter = @naftcostmk2,
    Naft_mk_2 = @naftmk2,
    Adblue_mk_2_liter = @bluecostmk2,
    Adblue_mk_2 = @bluemk2,
    Taks_srb_2 = @takssrb2,
    Putarina_srb_2 = @putsrb2,
    Putarina_hu_2 = @puthu2,
    Putarina_sk_2 = @putsk2,
    Putarina_cz_2 = @putcz2,
    Putarina_cro_2 = @putcro2,
    Putarina_slo_2 = @putslo2,
    Putarina_at_2 = @putat2,
    Putarina_de_2 = @putde2,
    Putarina_nl_2 = @putnl2,
    Phyto_2 = @phyto2,
    Telephone_2 = @tel2,
    M1_Shofer_2 = @md12,
    M2_Shofer_2 = @md22,
    Extra_Sh_2 = @extra2,
    Naft_eu_2_liter = @naftcosteu2,
    Naft_eu_2 = @nafteu2,
    Adblue_eu_2_liter = @bluecosteu2,
    Adblue_eu_2 = @blueeu2,
    Blank1_name_2 = @name12,
    Blank1_cost_2 = @cost12,
    Blank2_name_2 = @name22,
    Blank2_cost_2 = @cost22,
    Blank3_name_2 = @name32,
    Blank3_cost_2 = @cost32,
    Blank4_name_2 = @name42,
    Blank4_cost_2 = @cost42,
    Total_2 = @tot2,
    All_Total = @alltot,
    Currency = @currency
WHERE Nalog = @nalog", connection))
                    {
                        command.Parameters.AddWithValue("@nalog", Nalog);
                        command.Parameters.AddWithValue("@extra1", Extra);
                        command.Parameters.AddWithValue("@extra2", Extr2);
                        command.Parameters.AddWithValue("@done", check);
                        command.Parameters.AddWithValue("@truck", Truck);
                        command.Parameters.AddWithValue("@driver1", Driver1);
                        command.Parameters.AddWithValue("@driver2", Driver2);
                        command.Parameters.AddWithValue("@termmk1", Termmk);
                        command.Parameters.AddWithValue("@putmk1", Putmk);
                        command.Parameters.AddWithValue("@naftcostmk1", Naftcostmk);
                        command.Parameters.AddWithValue("@naftmk1", Naftmk);
                        command.Parameters.AddWithValue("@bluecostmk1", Bluecostmk);
                        command.Parameters.AddWithValue("@bluemk1", Bluemk);
                        command.Parameters.AddWithValue("@takssrb1", Takssrb);
                        command.Parameters.AddWithValue("@putsrb1", Putsrb);
                        command.Parameters.AddWithValue("@puthu1", Puthu);
                        command.Parameters.AddWithValue("@putsk1", Putsk);
                        command.Parameters.AddWithValue("@putcz1", Putcz);
                        command.Parameters.AddWithValue("@putcro1", Putcro);
                        command.Parameters.AddWithValue("@putslo1", Putslo);
                        command.Parameters.AddWithValue("@putat1", Putat);
                        command.Parameters.AddWithValue("@putde1", Putde);
                        command.Parameters.AddWithValue("@putnl1", Putnl);
                        command.Parameters.AddWithValue("@phyto1", Phyto);
                        command.Parameters.AddWithValue("@tel1", Tel);
                        command.Parameters.AddWithValue("@md11", Md1);
                        command.Parameters.AddWithValue("@md21", Md2);
                        command.Parameters.AddWithValue("@naftcosteu1", Naftcosteu);
                        command.Parameters.AddWithValue("@nafteu1", Nafteu);
                        command.Parameters.AddWithValue("@bluecosteu1", Bluecosteu);
                        command.Parameters.AddWithValue("@blueeu1", Blueeu);
                        command.Parameters.AddWithValue("@name11", Name1);
                        command.Parameters.AddWithValue("@name21", Name2);
                        command.Parameters.AddWithValue("@name31", Name3);
                        command.Parameters.AddWithValue("@name41", Name4);
                        command.Parameters.AddWithValue("@cost11", Cost1);
                        command.Parameters.AddWithValue("@cost21", Cost2);
                        command.Parameters.AddWithValue("@cost31", Cost3);
                        command.Parameters.AddWithValue("@cost41", Cost4);
                        command.Parameters.AddWithValue("@tot1", Tot);
                        command.Parameters.AddWithValue("@termmk2", Termmk2);
                        command.Parameters.AddWithValue("@putmk2", Putmk2);
                        command.Parameters.AddWithValue("@naftcostmk2", Naftcostmk2);
                        command.Parameters.AddWithValue("@naftmk2", Naftmk2);
                        command.Parameters.AddWithValue("@bluecostmk2", Bluecostmk2);
                        command.Parameters.AddWithValue("@bluemk2", Bluemk2);
                        command.Parameters.AddWithValue("@takssrb2", Takssrb2);
                        command.Parameters.AddWithValue("@putsrb2", Putsrb2);
                        command.Parameters.AddWithValue("@puthu2", Puthu2);
                        command.Parameters.AddWithValue("@putsk2", Putsk2);
                        command.Parameters.AddWithValue("@putcz2", Putcz2);
                        command.Parameters.AddWithValue("@putcro2", Putcro2);
                        command.Parameters.AddWithValue("@putslo2", Putslo2);
                        command.Parameters.AddWithValue("@putat2", Putat2);
                        command.Parameters.AddWithValue("@putde2", Putde2);
                        command.Parameters.AddWithValue("@putnl2", Putnl2);
                        command.Parameters.AddWithValue("@phyto2", Phyto2);
                        command.Parameters.AddWithValue("@tel2", Tel2);
                        command.Parameters.AddWithValue("@md12", Md12);
                        command.Parameters.AddWithValue("@md22", Md22);
                        command.Parameters.AddWithValue("@naftcosteu2", Naftcosteu2);
                        command.Parameters.AddWithValue("@nafteu2", Nafteu2);
                        command.Parameters.AddWithValue("@bluecosteu2", Bluecosteu2);
                        command.Parameters.AddWithValue("@blueeu2", Blueeu2);
                        command.Parameters.AddWithValue("@name12", Name12);
                        command.Parameters.AddWithValue("@name22", Name22);
                        command.Parameters.AddWithValue("@name32", Name32);
                        command.Parameters.AddWithValue("@name42", Name42);
                        command.Parameters.AddWithValue("@cost12", Cost12);
                        command.Parameters.AddWithValue("@cost22", Cost22);
                        command.Parameters.AddWithValue("@cost32", Cost32);
                        command.Parameters.AddWithValue("@cost42", Cost42);
                        command.Parameters.AddWithValue("@tot2", Tot2);
                        command.Parameters.AddWithValue("@alltot", Alltot);
                        command.Parameters.AddWithValue("@currency", Currency);
                        command.ExecuteNonQuery();
                    }
                    System.Windows.MessageBox.Show("Data inserted\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                }
            }
            catch
            {
                throw new Exception("An error occured while inserting\n");
            }
        }
    }
}
