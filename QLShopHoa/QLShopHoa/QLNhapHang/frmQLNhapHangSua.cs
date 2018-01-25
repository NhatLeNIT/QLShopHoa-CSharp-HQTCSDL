using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ValueObject;
using BusinessLogicLayer;
using QLShopHoa.QLNhaCungCap;

namespace QLShopHoa
{
    public partial class frmQLNhapHangSua : DevExpress.XtraEditors.XtraForm
    {
        NhaCungCapBUS busNCC = new NhaCungCapBUS();
        ValueObject.NhapHang obj = new ValueObject.NhapHang();
        NhapHangBUS busNH = new NhapHangBUS();
        public string IDNhapHang { get; set; }
        public frmQLNhapHangSua()
        {
            InitializeComponent();
        }
      
        private void HienThiNhaCungCap()
        {
            cbbNhaCungCap.Properties.DataSource = busNCC.GetData();
            cbbNhaCungCap.Properties.ValueMember = "IDNhaCungCap";
            cbbNhaCungCap.Properties.DisplayMember = "TenNhaCungCap";
        }
        private void btnThemNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCapThem frmNCC = new frmNhaCungCapThem();
            frmNCC.ShowDialog();
            HienThiNhaCungCap();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(Validate())
            {
                DataTable dt = busNH.GetDataByID(IDNhapHang);
                obj.IDNhapHang = IDNhapHang;
                obj.IDNhaCungCap = cbbNhaCungCap.EditValue.ToString();
                obj.GhiChu = txtGhiChu.Text;
                obj.IDNhanVien = frmMain.IDNhanVien;
                obj.NgayNhap = dt.Rows[0]["NgayNhap"].ToString();
                obj.SoLuongSanPham = Convert.ToInt32(dt.Rows[0]["SoLuongSanPham"].ToString());
                obj.TongTien = Convert.ToDouble(dt.Rows[0]["TongTien"].ToString());
                busNH.Update(obj);
                XtraMessageBox.Show("Cập nhật phiếu nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private bool ValidateData()
        {
            if (this.cbbNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.cbbNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void frmNhapHangSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busNH.GetDataByID(IDNhapHang);
            txtIDNhapHang.Text = IDNhapHang;
            txtGhiChu.Text = dt.Rows[0]["GhiChu"].ToString();
            HienThiNhaCungCap();
            dt = busNCC.GetDataByID(dt.Rows[0]["IDNhaCungCap"].ToString());
            cbbNhaCungCap.EditValue = cbbNhaCungCap.Properties.GetKeyValueByDisplayText(dt.Rows[0]["TenNhaCungCap"].ToString());
        }
    }
}