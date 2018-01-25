using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhaCungCap
{
    public partial class frmNhaCungCapSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDNhaCungCap = "";
        public string TenNhaCungCap = "";
        public string DienThoai = "";
        public string DiaChi = "";
        public string GhiChu = "";
        public frmNhaCungCapSua()
        {
            InitializeComponent();
        }

        NhaCungCap obj = new NhaCungCap();
        NhaCungCapBUS bus = new NhaCungCapBUS();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.IDNhaCungCap = txtIDNhaCungCap.Text;
                obj.TenNhaCungCap = txtTenNhaCungCap.Text;
                obj.DienThoai = txtDienThoai.Text;
                obj.DiaChi = txtDiaChi.Text;
                obj.GhiChu = txtGhiChu.Text;
                bus.Update(obj);
                XtraMessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void frmNhaCungCapSua_Load(object sender, EventArgs e)
        {
            txtIDNhaCungCap.Text = IDNhaCungCap;
            txtTenNhaCungCap.Text = TenNhaCungCap;
            txtDienThoai.Text = DienThoai;
            txtDiaChi.Text = DiaChi;
            txtGhiChu.Text = GhiChu;
            txtIDNhaCungCap.Enabled = false;
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
            if (!int.TryParse(e.KeyChar.ToString(), out isNumber))
            {
                XtraMessageBox.Show("Không được nhập chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool ValidateData()
        {

            if (this.txtTenNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.txtTenNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}