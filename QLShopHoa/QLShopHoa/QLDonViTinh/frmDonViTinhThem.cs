using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLDonViTinh
{
    public partial class frmDonViTinhThem : DevExpress.XtraEditors.XtraForm
    {
        public frmDonViTinhThem()
        {
            InitializeComponent();
        }

        DonViTinh obj = new DonViTinh();
        DonViTinhBUS bus = new DonViTinhBUS();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.TenDonViTinh = txtTenDonViTinh.Text;
                obj.GhiChu = txtGhiChu.Text;
                bus.Insert(obj);
                XtraMessageBox.Show("Thêm đơn vị tính thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }
        private bool ValidateData()
        {
            if (this.txtTenDonViTinh.Text.Trim().Equals(string.Empty))
            {
                this.txtTenDonViTinh.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}