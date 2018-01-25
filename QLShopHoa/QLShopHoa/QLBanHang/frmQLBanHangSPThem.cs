using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLSanPham;
using ValueObject;

namespace QLShopHoa.QLBanHang
{
    public partial class frmQLBanHangSPThem : DevExpress.XtraEditors.XtraForm
    {
        HoaDon obj = new HoaDon();
        ChiTietHoaDon objCT = new ChiTietHoaDon();
        ChiTietHoaDonBUS busCT = new ChiTietHoaDonBUS();
        HoaDonBUS busHD = new HoaDonBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        QuerySQLBUS query = new QuerySQLBUS();
        public string IDHoaDon { get; set; }

        public frmQLBanHangSPThem()
        {
            InitializeComponent();
        }

        private void frmQLBanHangSPThem_Load(object sender, EventArgs e)
        {
            HienThiSanPham();
        }
        private void HienThiSanPham()
        {
            cbbSanPham.Properties.DataSource = busSP.GetDataByStatus();
            cbbSanPham.Properties.ValueMember = "IDSanPham";
            cbbSanPham.Properties.DisplayMember = "TenSanPham";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateDataSanPham())
            {
                DataTable dt = busCT.GetDataByIDSanPham(IDHoaDon, cbbSanPham.EditValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    XtraMessageBox.Show("Sản phẩm này đã tồn tại trong đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    dt = busSP.GetDataByID(cbbSanPham.EditValue.ToString());
                    objCT.IDSanPham = cbbSanPham.EditValue.ToString();
                    objCT.IDHoaDon = IDHoaDon;
                    objCT.SoLuong = Convert.ToInt32(txtSoLuong.Value);
                    objCT.DonGia = Convert.ToDouble(dt.Rows[0]["GiaBan"]);
                    objCT.IDDonViTinh = Convert.ToInt32(dt.Rows[0]["IDDonViTinh"]);
                    busCT.Insert(objCT);
                    SanPham objSP = new SanPham();
                    objSP.IDSanPham = objCT.IDSanPham;
                    objSP.SoLuong = objCT.SoLuong;
                    busSP.UpdateQuantitySub(objSP);
                    //update số lượng sản phẩm và tổng tiền trong đơn hàng
                    string sql = "SELECT SoLuongSanPham, TongTien FROM HoaDon WHERE IDHoaDon='" + IDHoaDon + "'";
                    dt = query.GetDataBySQL(sql);
                    int quantity = Convert.ToInt32(dt.Rows[0]["SoLuongSanPham"]) + objCT.SoLuong;
                    double totalPrice = Convert.ToDouble(dt.Rows[0]["TongTien"]) + (objCT.SoLuong * objCT.DonGia);
                    sql = "UPDATE HoaDon SET SoLuongSanPham = " + quantity + ", TongTien = " + totalPrice + " WHERE IDHoaDon = '" + IDHoaDon + "'";
                    query.ExecuteBySQL(sql);
                    XtraMessageBox.Show("Thêm sản phẩm vào đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPhamThem frmSP = new frmSanPhamThem();
            frmSP.ShowDialog();
            HienThiSanPham();
        }
        private bool ValidateDataSanPham()
        {
            if (this.cbbSanPham.Text.Trim().Equals(string.Empty))
            {
                this.cbbSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa chọn sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (Convert.ToInt32(txtSoLuong.Value) == 0)
            {
                this.txtSoLuong.Focus();
                XtraMessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}