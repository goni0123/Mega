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
    public partial class LoadOut : Form
    {
        public int SelectedNalogNr { get; set; }
        readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;

        public LoadOut(int Nalog)
        {
            SelectedNalogNr = Nalog;
            InitializeComponent();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("Select * From Loading_Order_Out where Loading_Order_number_out=@Nalog", conn);
                command.Parameters.AddWithValue("@Nalog", SelectedNalogNr);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                reportViewer1.LocalReport.DataSources.Clear();
                Microsoft.Reporting.WinForms.ReportDataSource source = new Microsoft.Reporting.WinForms.ReportDataSource("LoadOut", dt);
                string filename = "LoadOut.rdlc";
                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RDLC");
                string filepath = Path.Combine(directoryPath, filename);
                reportViewer1.LocalReport.ReportPath = filepath;
                reportViewer1.LocalReport.DataSources.Add(source);
                reportViewer1.RefreshReport();

                conn.Close();
            }
        }

        private void LoadOut_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
