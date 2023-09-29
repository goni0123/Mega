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
    public partial class Inl : Form
    {
        public int SelectedNalogNr { get; set; }
        readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        public Inl(int Nalog)
        {
            SelectedNalogNr = Nalog;
            InitializeComponent();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Inland_driving WHERE Nalog_nr_inland=@Nalog", conn);
                command.Parameters.AddWithValue("@Nalog", SelectedNalogNr);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SqlCommand com = new SqlCommand("SELECT * FROM Inland_driving_more WHERE Nalog_nr_inland=@Nalog", conn);
                com.Parameters.AddWithValue("@Nalog", SelectedNalogNr);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                DataTable d = new DataTable();
                ad.Fill(d);
                reportViewer1.LocalReport.DataSources.Clear();
                Microsoft.Reporting.WinForms.ReportDataSource source = new Microsoft.Reporting.WinForms.ReportDataSource("In", dt);
                Microsoft.Reporting.WinForms.ReportDataSource so = new Microsoft.Reporting.WinForms.ReportDataSource("Inm", d);
                string filename = "Inl.rdlc";
                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RDLC");
                string filepath = Path.Combine(directoryPath, filename);
                reportViewer1.LocalReport.ReportPath = filepath;
                reportViewer1.LocalReport.DataSources.Add(source);
                reportViewer1.LocalReport.DataSources.Add(so);
                reportViewer1.RefreshReport();

                conn.Close();
            }
        }

        private void Inl_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
