using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhomQuyen
{
    public partial class frmNhomQuyenThem : DevExpress.XtraEditors.XtraForm
    {
        public frmNhomQuyenThem()
        {
            InitializeComponent();
        }

        NhomQuyen obj = new NhomQuyen();
        NhomQuyenBUS bus = new NhomQuyenBUS();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.TenNhom = txtTenNhom.Text;
                obj.MoTa = txtMoTa.Text;
                //obj.Xem = cbXem.Checked ? 1 : 0;
                //obj.Them = cbThem.Checked ? 1 : 0;
                //obj.Sua = cbSua.Checked ? 1 : 0;
                //obj.Xoa = cbXoa.Checked ? 1 : 0;
                bus.Insert(obj);
                XtraMessageBox.Show("Thêm nhóm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (this.txtTenNhom.Text.Trim().Equals(string.Empty))
            {
                this.txtTenNhom.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên nhóm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
           return true;
        }
    }
}