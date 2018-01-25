using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLLoaiHang
{
    public partial class frmLoaiHangSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDLoaiHang = "";
        public string TenLoaiHang = "";
        public string MoTa = "";
        public string TrangThai = "";
        LoaiHang obj = new LoaiHang();
        LoaiHangBUS bus = new LoaiHangBUS();
        public frmLoaiHangSua()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void frmLoaiHangSua_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            //    e.Cancel = true;
        }

        private void frmLoaiHangSua_Load(object sender, EventArgs e)
        {
            txtTenLoaiHang.Text = TenLoaiHang;
            txtMoTa.Text = MoTa;
            if (Convert.ToBoolean(TrangThai) == true)
                cbTrangThai.Checked = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.IDLoaiHang = Convert.ToInt32(IDLoaiHang);
                obj.TenLoaiHang = txtTenLoaiHang.Text;
                obj.MoTa = txtMoTa.Text;
                obj.TinhTrang = cbTrangThai.Checked ? 1 : 0;
                bus.Update(obj);
                XtraMessageBox.Show("Cập nhật loại hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private bool ValidateData()
        {
            bool bValidated = false;
            if (this.txtTenLoaiHang.Text.Trim().Equals(string.Empty))
            {
                this.txtTenLoaiHang.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bValidated = false;
            }
            else
                bValidated = true;
            return bValidated;
        }

    }
}