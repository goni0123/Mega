using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace MegaNew.DocView
{
    public partial class We : Form
    {
        public int we{ get; set; }
        public int ni { get; set; }
        readonly string connectionString = ConfigurationManager.ConnectionStrings["RegisterEntities"].ConnectionString;
        public We(int nid,int wid)
        {
            ni = nid;
            we = wid;
            InitializeComponent();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Weeks WHERE Week_id=@Nalog", conn);
                command.Parameters.AddWithValue("@Nalog", we);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SqlCommand com = new SqlCommand("SELECT * FROM Inland_week WHERE Nalog_nr=@Nalog", conn);
                com.Parameters.AddWithValue("@Nalog", ni);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                DataTable d = new DataTable();
                ad.Fill(d);
                SqlCommand co = new SqlCommand("SELECT * FROM Inland_week_more WHERE Nalog_nr=@Nalog", conn);
                co.Parameters.AddWithValue("@Nalog", ni);
                SqlDataAdapter a = new SqlDataAdapter(co);
                DataTable b = new DataTable();
                a.Fill(b);
                reportViewer1.LocalReport.DataSources.Clear();
                Microsoft.Reporting.WinForms.ReportDataSource source = new Microsoft.Reporting.WinForms.ReportDataSource("Week", dt);
                Microsoft.Reporting.WinForms.ReportDataSource so = new Microsoft.Reporting.WinForms.ReportDataSource("Inland", d);
                Microsoft.Reporting.WinForms.ReportDataSource s = new Microsoft.Reporting.WinForms.ReportDataSource("More", b);
                string filename = "Week.rdlc";
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

        private void We_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}

