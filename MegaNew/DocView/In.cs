using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaNew.DocView
{
    public partial class In : Form
    {
        public int SelectedNalogNr { get; set; }
        readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        public In(int Nalog)
        {
            SelectedNalogNr= Nalog;
            InitializeComponent();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Incoming_NL_MK WHERE Nalog_nr_in=@Nalog", conn);
                command.Parameters.AddWithValue("@Nalog", SelectedNalogNr);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SqlCommand com = new SqlCommand("SELECT * FROM Loading_Company_In WHERE Nalog_nr_in=@Nalog", conn);
                com.Parameters.AddWithValue("@Nalog", SelectedNalogNr);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                DataTable d = new DataTable();
                ad.Fill(d);
                SqlCommand co = new SqlCommand("SELECT * FROM Route_in WHERE Nalog_nr_in=@Nalog ORDER BY RI ", conn);
                co.Parameters.AddWithValue("@Nalog", SelectedNalogNr);
                SqlDataAdapter a = new SqlDataAdapter(co);
                DataTable b = new DataTable();
                a.Fill(b);
                reportViewer1.LocalReport.DataSources.Clear();
                Microsoft.Reporting.WinForms.ReportDataSource source = new Microsoft.Reporting.WinForms.ReportDataSource("Incoming", dt);
                Microsoft.Reporting.WinForms.ReportDataSource so = new Microsoft.Reporting.WinForms.ReportDataSource("LoadCompany", d);
                Microsoft.Reporting.WinForms.ReportDataSource s = new Microsoft.Reporting.WinForms.ReportDataSource("Route", b);
                string filename = "Incoming.rdlc";
                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RDLC");
                string filepath = Path.Combine(directoryPath, filename);
                reportViewer1.LocalReport.ReportPath = filepath;
                reportViewer1.LocalReport.DataSources.Add(source);
                reportViewer1.LocalReport.DataSources.Add(so);
                reportViewer1.LocalReport.DataSources.Add(s);
                reportViewer1.RefreshReport();

                conn.Close();
            }
        }

        private void In_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
