using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.Auth
{
    public partial class frmDoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        public string IDNhanVien { get; set; }
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        NhanVien obj = new NhanVien();
        NhanVienBUS bus = new NhanVienBUS();
        md5Convert md5 = new md5Convert();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = bus.GetDataByID(IDNhanVien);
                if (dt.Rows.Count == 1)
                {
                    if (md5.md5(txtMatKhauHienTai.Text).Equals(dt.Rows[0]["MatKhau"].ToString()))
                    {
                        bus.UpdatePassword(IDNhanVien, md5.md5(txtMatKhauMoi.Text));
                        XtraMessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Mật khẩu hiện tại không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMatKhauHienTai.Text = string.Empty;
                        txtMatKhauHienTai.Focus();
                    }
                }
            }       
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();

        }
        private bool ValidateData()
        {
            if (this.txtMatKhauHienTai.Text.Trim().Equals(string.Empty))
            {
                this.txtMatKhauHienTai.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mật khẩu hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtMatKhauMoi.Text.Trim().Equals(string.Empty))
            {
                this.txtMatKhauMoi.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!this.txtNhapLaiMatKhauMoi.Text.Trim().Equals(this.txtMatKhauMoi.Text.Trim()))
            {
                this.txtNhapLaiMatKhauMoi.Focus();
                XtraMessageBox.Show("Mật khẩu nhập lại không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}