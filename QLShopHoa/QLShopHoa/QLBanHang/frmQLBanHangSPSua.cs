using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLBanHang
{
    public partial class frmQLBanHangSPSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDHoaDon { get; set; }
        public string IDSanPham { get; set; }
        ChiTietHoaDon objCTHD = new ChiTietHoaDon();
        ChiTietHoaDonBUS busCTHD = new ChiTietHoaDonBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        HoaDon objHD = new HoaDon();
        SanPham objSanPham = new SanPham();
        QuerySQLBUS query = new QuerySQLBUS();
        public frmQLBanHangSPSua()
        {
            InitializeComponent();
        }

        private void frmQLBanHangSPSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busCTHD.GetDataByIDSanPham(IDHoaDon, IDSanPham);
            txtIDSanPham.Text = IDSanPham;
            txtSoLuong.Value = Convert.ToDecimal(dt.Rows[0]["SoLuong"]);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DataTable dataTemp = busSP.GetDataByID(IDSanPham);

            DataTable dt = busCTHD.GetDataByIDSanPham(IDHoaDon, IDSanPham);
            objCTHD.IDHoaDon = IDHoaDon;
            objCTHD.IDSanPham = IDSanPham;
            objCTHD.SoLuong = Convert.ToInt32(txtSoLuong.Value);//so luong moi nhap vao

            //update quantity product
            objSanPham.IDSanPham = IDSanPham;
            objSanPham.SoLuong = Convert.ToInt32(dt.Rows[0]["SoLuong"]); // so luong ban dau trong hoa don

            //****************cập nhật lại số lượng và tổng tiền cho hóa đơn
            int soLuongBanDauDaMua = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
            double tongTienBanDauDaMua = soLuongBanDauDaMua * Convert.ToDouble(dt.Rows[0]["DonGia"]);

            //== Tính số lượng và tổng tiền sau khi cập nhật
            int soLuongSoVoiLucBanDau = objCTHD.SoLuong - soLuongBanDauDaMua;
            double tongTienCuaSoLuongLucSau = soLuongSoVoiLucBanDau * Convert.ToDouble(dt.Rows[0]["DonGia"]);
            if (Convert.ToInt32(dataTemp.Rows[0]["SoLuong"]) < soLuongSoVoiLucBanDau) //so luong san pham so voi so luong them vao
            {
                XtraMessageBox.Show("Sản phẩm không đủ số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //== Update lại trong bảng hóa đơn
                string sql = "UPDATE HoaDon SET SoLuongSanPham = SoLuongSanPham + " + soLuongSoVoiLucBanDau + ", TongTien = TongTien + " + tongTienCuaSoLuongLucSau + "WHERE IDHoaDon = '" + IDHoaDon + "'";
                query.ExecuteBySQL(sql);
                //busSP.UpdateQuantity(objSanPham); //cộng lại số lượng sản phẩm đã bán vào lại sản phẩm
                busCTHD.UpdateQuantity(objCTHD);//cập nhật lại số lượng sản phẩm mới của đơn hàng
                dt = busCTHD.GetDataByIDSanPham(IDHoaDon, IDSanPham);
                objSanPham.IDSanPham = IDSanPham;
                objSanPham.SoLuong = Convert.ToInt32(dt.Rows[0]["SoLuong"]);

                //if (soLuongBanDauDaMua > objCTHD.SoLuong)// so voi so luong moi nhap vao
                //{
                //    busSP.UpdateQuantitySub(objSanPham);//trừ đi số lượng sản phẩm mới cập nhật
                //}
                //else
                //{
                    objSanPham.SoLuong = -soLuongSoVoiLucBanDau;
                    busSP.UpdateQuantity(objSanPham);
                //}
                XtraMessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }
    }
}