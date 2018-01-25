using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLKhachHang
{
    public partial class frmKhachHangSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDKhachHang = "";
        public string HoTen = "";
        public string DienThoai = "";
        public string DiaChi = "";
        public string NgaySinh = "";
        public string GioiTinh = "";
        public string GhiChu = "";

        public frmKhachHangSua()
        {
            InitializeComponent();
        }
        KhachHang obj = new KhachHang();
        KhachHangBUS bus = new KhachHangBUS();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.IDKhachHang = txtIDKhachHang.Text;
                obj.HoTen = txtHoTen.Text;
                obj.DienThoai = txtDienThoai.Text;
                obj.DiaChi = txtDiaChi.Text;
                obj.NgaySinh = txtNgaySinh.Text;
                obj.GhiChu = txtGhiChu.Text;
                obj.GioiTinh = txtNam.Checked ? "Nam" : "Nữ";
                bus.Update(obj);
                XtraMessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void frmKhachHangSua_Load(object sender, EventArgs e)
        {
            txtIDKhachHang.Text = IDKhachHang;
            txtHoTen.Text = HoTen;
            txtDienThoai.Text = DienThoai;
            txtDiaChi.Text = DiaChi;
            txtNgaySinh.Text = NgaySinh;
            txtGhiChu.Text = GhiChu;
            if (GioiTinh.Equals("Nam"))
                txtNam.Checked = true;
            else txtNu.Checked = true;
            txtIDKhachHang.Enabled = false;
        }
        private bool ValidateData()
        {

            if (this.txtHoTen.Text.Trim().Equals(string.Empty))
            {
                this.txtHoTen.Focus();
                XtraMessageBox.Show("Bạn chưa nhập họ tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtDienThoai.Text.Trim().Equals(string.Empty))
            {
                this.txtDienThoai.Focus();
                XtraMessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}