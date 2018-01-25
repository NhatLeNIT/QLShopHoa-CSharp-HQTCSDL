using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhomQuyen
{
    public partial class frmNhomQuyenSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDNhom = "";
        public string TenNhom = "";
        public string MoTa = "";
        NhomQuyen obj = new NhomQuyen();
        NhomQuyenBUS bus = new NhomQuyenBUS();
        public frmNhomQuyenSua()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.IDNhom = Convert.ToInt32(IDNhom);
                obj.TenNhom = txtTenNhom.Text;
                obj.MoTa = txtMoTa.Text;
                bus.Update(obj);
                XtraMessageBox.Show("Cập nhật nhóm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void frmNhomQuyenSua_Load(object sender, EventArgs e)
        {
            txtTenNhom.Text = TenNhom;
            txtMoTa.Text = MoTa;
            //if (Convert.ToBoolean(Xem) == true) cbXem.Checked = true;
            //if (Convert.ToBoolean(Them) == true) cbThem.Checked = true;
            //if (Convert.ToBoolean(Sua) == true) cbSua.Checked = true;
            //if (Convert.ToBoolean(Xoa) == true) cbXoa.Checked = true;
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