using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.Auth
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        NhanVien obj = new NhanVien();
        NhanVienBUS bus = new NhanVienBUS();
        md5Convert md5 = new md5Convert();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = bus.GetDataByUserName(txtTaiKhoan.Text);
                if (dt.Rows.Count == 1)
                {
                    if (md5.md5(txtMatKhau.Text).Equals(dt.Rows[0]["MatKhau"].ToString()))
                    {
                        if (Convert.ToInt32(dt.Rows[0]["TrangThai"]) == 1)
                        {
                            this.Hide();
                            frmMain frm = new frmMain();
                            frmMain.IDNhanVien = dt.Rows[0]["IDNhanVien"].ToString();
                            frmMain.IDNhom = Convert.ToInt32(dt.Rows[0]["IDNhom"]);
                            frm.ShowDialog();
                            if (frm.checkDangXuat)
                            {
                                this.Show();
                                txtTaiKhoan.Text = string.Empty;
                                txtMatKhau.Text = string.Empty;
                                txtTaiKhoan.Focus();
                            }
                            else this.Close();
                        }
                        else
                        {
                            XtraMessageBox.Show("Bạn không có quyền truy cập vào hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtMatKhau.Text = string.Empty;
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Mật khẩu bạn nhập không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMatKhau.Text = string.Empty;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tài khoản " + txtTaiKhoan.Text + " không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTaiKhoan.Text = string.Empty;
                    txtMatKhau.Text = string.Empty;
                }
            }
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidateData()
        {
            string strConnect = Convert.ToString(DataAccessLayer.Properties.Settings.Default.strConnectDAO);
            //MessageBox.Show(strConnect);
            SqlConnection sqlcon = new SqlConnection(strConnect);
            try{
                if (this.txtTaiKhoan.Text.Trim().Equals(string.Empty))
                {
                    this.txtTaiKhoan.Focus();
                    XtraMessageBox.Show("Bạn chưa nhập tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (this.txtMatKhau.Text.Trim().Equals(string.Empty))
                {
                    this.txtMatKhau.Focus();
                    XtraMessageBox.Show("Bạn chưa nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                sqlcon.Open();
                sqlcon.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối. Vui lòng cấu hình lại kết nối của phần mềm");
                sqlcon.Close();
                return false;
            }
            return true;
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDangNhap.PerformClick();
        }

        private void txtTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDangNhap.PerformClick();
        }

        private void txtTaiKhoan_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDangNhap.PerformClick();
        }

        private void btnCauHinh_Click(object sender, EventArgs e)
        {
            frmCauHinh frm = new frmCauHinh();
            frm.ShowDialog();
        }
    }
}