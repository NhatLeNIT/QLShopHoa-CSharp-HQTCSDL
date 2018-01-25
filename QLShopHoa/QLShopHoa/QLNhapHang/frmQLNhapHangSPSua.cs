using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLDonViTinh;
using ValueObject;

namespace QLShopHoa.QLNhapHang
{
    public partial class frmQLNhapHangSPSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDNhapHang { get; set; }
        public string IDSanPham { get; set; }
        public int IDDonViTinh { get; set; }
        ChiTietNhapHang objCTNH = new ChiTietNhapHang();
        ChiTietNhapHangBUS busCTNH = new ChiTietNhapHangBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        DonViTinhBUS busDVT = new DonViTinhBUS();
        ValueObject.NhapHang objNH = new ValueObject.NhapHang();
        SanPham objSanPham = new SanPham();
        QuerySQLBUS query = new QuerySQLBUS();
        public frmQLNhapHangSPSua()
        {
            InitializeComponent();
        }

        private void frmSanPhamNhapHangSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busCTNH.GetDataByIDSanPham(IDNhapHang, IDSanPham);
            IDDonViTinh = Convert.ToInt32(dt.Rows[0]["IDDonViTinh"].ToString());
            txtIDSanPham.Text = IDSanPham;
            txtDonGia.Value = Convert.ToDecimal(dt.Rows[0]["DonGia"]);
            txtSoLuong.Value = Convert.ToDecimal(dt.Rows[0]["SoLuong"]);
            HienThiDonViTinh(IDDonViTinh);
        }

        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            frmDonViTinhThem frmDVT = new frmDonViTinhThem();
            frmDVT.ShowDialog();
            HienThiDonViTinh(IDDonViTinh);
        }
        private void HienThiDonViTinh(int IDDonViTinh)
        {
            cbbDonViTinh.Properties.DataSource = busDVT.GetData();
            cbbDonViTinh.Properties.ValueMember = "IDDonViTinh";
            cbbDonViTinh.Properties.DisplayMember = "TenDonViTinh";
            DataTable dt = busDVT.GetDataByID(IDDonViTinh);
            cbbDonViTinh.EditValue = cbbDonViTinh.Properties.GetKeyValueByDisplayText(dt.Rows[0]["TenDonViTinh"].ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DataTable dt = busCTNH.GetDataByIDSanPham(IDNhapHang, IDSanPham);
            objCTNH.IDNhapHang = IDNhapHang;
            objCTNH.IDSanPham = IDSanPham;
            objCTNH.SoLuong = Convert.ToInt32(txtSoLuong.Value);
            objCTNH.IDDonViTinh = Convert.ToInt32(cbbDonViTinh.EditValue.ToString());
            //update quantity product
            objSanPham.IDSanPham = IDSanPham;
            objSanPham.SoLuong = Convert.ToInt32(dt.Rows[0]["SoLuong"]);

            //cập nhật lại số lượng và tổng tiền cho phiếu nhập
            int soLuongBanDauDaNhap = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
            double tongTienBanDauDaNhap = soLuongBanDauDaNhap * Convert.ToDouble(dt.Rows[0]["DonGia"]);
            //== Tính số lượng và tổng tiền sau khi cập nhật
            int soLuongSauCapNhat = objCTNH.SoLuong - soLuongBanDauDaNhap;
            double tongTienSauKhiCapNhat = soLuongSauCapNhat * Convert.ToDouble(dt.Rows[0]["DonGia"]);
            //== Update lại trong bảng hóa đơn
            string sql = "UPDATE NhapHang SET SoLuongSanPham = SoLuongSanPham + " + soLuongSauCapNhat + ", TongTien = TongTien + " + tongTienSauKhiCapNhat + "WHERE IDNhapHang = '" + IDNhapHang + "'";
            query.ExecuteBySQL(sql);

            busSP.UpdateQuantitySub(objSanPham);//trừ đi số lượng sản phẩm đã nhập vào lại sản phẩm
            busCTNH.UpdateQuantity(objCTNH);//cập nhật lại số lượng sản phẩm mới của phiếu nhập
            dt = busCTNH.GetDataByIDSanPham(IDNhapHang, IDSanPham);
            objSanPham.IDSanPham = IDSanPham;
            objSanPham.SoLuong = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
            busSP.UpdateQuantity(objSanPham);//cộng lại số lượng sản phẩm mới cập nhật
            XtraMessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }
    }
}