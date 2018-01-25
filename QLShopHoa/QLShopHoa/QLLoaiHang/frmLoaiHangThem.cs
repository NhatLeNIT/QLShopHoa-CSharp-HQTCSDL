using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLLoaiHang
{
    public partial class frmLoaiHangThem : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiHangThem()
        {
            InitializeComponent();
        }
        LoaiHang obj = new LoaiHang();
        LoaiHangBUS bus = new LoaiHangBUS();


        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.TenLoaiHang = txtTenLoaiHang.Text;
                obj.MoTa = txtMoTa.Text;
                obj.TinhTrang = cbTrangThai.Checked ? 1 : 0;
                bus.Insert(obj);
                XtraMessageBox.Show("Thêm loại hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            bool bValidated = false;
            if(this.txtTenLoaiHang.Text.Trim().Equals(string.Empty))
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