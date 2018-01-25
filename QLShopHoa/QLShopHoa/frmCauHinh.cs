using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLShopHoa
{
    public partial class frmCauHinh : DevExpress.XtraEditors.XtraForm
    {
        public frmCauHinh()
        {
            InitializeComponent();
        }

        private void frmCauHinh_Load(object sender, EventArgs e)
        {
            background.RunWorkerAsync();
        }

        private void cbAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAuth.Checked)
            {
                txtUser.Enabled = true;
                txtPass.Enabled = true;
            }
            else
            {
                txtUser.Enabled = false;
                txtPass.Enabled = false;
            }
        }

        private void background_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(getNameServer));
            t.Start();
            for (int i = 1; i <= 100; i++)
            {
                background.ReportProgress(i, i);
                Thread.Sleep(500);
            }

        }

        private void getNameServer()
        {
            string myServer = Environment.MachineName;
            // Retrieve the enumerator instance and then the data.  
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable servers = instance.GetDataSources();

            if (servers.Rows.Count != 0)
            {
                cbbNameServer.DataSource = servers;
                cbbNameServer.DisplayMember = "ServerName";
            }
            else
            {
                if (cbbNameServer.InvokeRequired)
                {
                    cbbNameServer.Invoke(new MethodInvoker(delegate { cbbNameServer.Items.Add(myServer); }));
                }
                   // cbbNameServer.Items.Add(myServer);
            }
        }
        private void background_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            barPercent.Text = e.ProgressPercentage.ToString() + " %";
            Application.DoEvents();
        }

        private void background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cbbNameServer.Enabled = true;
            cbAuth.Enabled = true;
            btnConnect.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();}

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strConnect = "";
            if (!cbAuth.Checked)
                strConnect = "Server=" + cbbNameServer.Text + ";Database=QuanLyShopHoa;Trusted_Connection=True;";
            else
            {
                if(cbbNameServer.Text.Contains(","))
                    strConnect = "Data Source=" + cbbNameServer.Text + "; Network Library=DBMSSOCN;Initial Catalog=QuanLyShopHoa; User ID=" + txtUser.Text + "; Password=" + txtPass.Text + "; ";
                else
                    strConnect = "Server=" + cbbNameServer.Text + ";Database=QuanLyShopHoa;User Id=" + txtUser.Text + ";Password = " + txtPass.Text + "; ";
            }
            SqlConnection sqlcon = new SqlConnection(strConnect);
            try
            {
                sqlcon.Open();
                DataAccessLayer.Properties.Settings.Default.strConnectDAO = strConnect;
                DataAccessLayer.Properties.Settings.Default.Save();
                MessageBox.Show("Kết nối thành công!");
                sqlcon.Close();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối không thành công, xin kiểm tra lại!");
                sqlcon.Close();
            }
        }
    }
}