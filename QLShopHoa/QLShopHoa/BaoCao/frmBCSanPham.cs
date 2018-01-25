using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCSanPham : DevExpress.XtraEditors.XtraForm
    {
        private bool _checkODau = false;
        ChiTietHoaDonBUS bus = new ChiTietHoaDonBUS();
        ChiTietNhapHangBUS busCTNH = new ChiTietNhapHangBUS();
        private int _checkThoiGian; //tuan = 1, thang = 2, ngay = 3
        private string ngayDau;
        private string ngayCuoi;
        public frmBCSanPham()
        {
            InitializeComponent();
        }

        //Lợi nhuận
        private void btnLNTuan_Click(object sender, EventArgs e)
        {
            HienThiLnTheoTuan();
        }
        private void btnLNThang_Click(object sender, EventArgs e)
        {
            HienThiLnTheoThang();
        }
        private void btnLNLoc_Click(object sender, EventArgs e)
        {
            HienThiLnTheoNgay();
        }
        private void HienThiLnTheoTuan()
        {
            DataTable dt = bus.StatisticLN_ByWeek();
            msdsLN.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbLNThongKe.Text = "Thống kê bán hàng theo lợi nhuận của tuần này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng doanh thu: " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void HienThiLnTheoThang()
        {
            DataTable dt = bus.StatisticLN_ByMonth();
            msdsLN.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbLNThongKe.Text = "Thống kê bán hàng theo lợi nhuận của tháng này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng doanh thu: " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được " + tongLoiNhuan.ToString("N0") + " đồng";

        }
        private void HienThiLnTheoNgay()
        {
            string ngayDau = txtLNNgayDau.Text;
            string ngayCuoi = txtLNNgayCuoi.Text;
            DataTable dt = bus.StatisticLN_ByDate(ngayDau, ngayCuoi);
            msdsLN.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbLNThongKe.Text = "Thống kê bán hàng theo lợi nhuận từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng doanh thu: " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn8)
            {
                if (_checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!_checkODau) _checkODau = true;
        }
        //Nhân Viên

        private void btnNVTuan_Click(object sender, EventArgs e)
        {
            HienThiNvTheoTuan();
        }

        private void btnNVThang_Click(object sender, EventArgs e)
        {
            HienThiNvTheoThang();
        }

        private void btnNVLoc_Click(object sender, EventArgs e)
        {
            HienThiNvTheoNgay();
        }
        private void HienThiNvTheoTuan()
        {
            _checkThoiGian = 1;
            DataTable dt = bus.StatisticNV_ByWeek();
            msdsNV.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            var soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbNVThongKe.Text = "Thống kê bán hàng nhân viên theo sản phẩm của tuần này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng giá trị: " + tongDoanhThu.ToString("N0") + " đồng";
        }
        private void HienThiNvTheoThang()
        {
            _checkThoiGian = 2;
            DataTable dt = bus.StatisticNV_ByMonth();
            msdsNV.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbNVThongKe.Text = "Thống kê bán hàng nhân viên theo sản phẩm của tháng này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng giá trị: " + tongDoanhThu.ToString("N0") + " đồng";
        }
        private void HienThiNvTheoNgay()
        {
            _checkThoiGian = 3;
            this.ngayDau = txtNVNgayDau.Text;
            this.ngayCuoi = txtNVNgayCuoi.Text;
            string ngayDau = txtNVNgayDau.Text;
            string ngayCuoi = txtNVNgayCuoi.Text;
            DataTable dt = bus.StatisticNV_ByDate(ngayDau, ngayCuoi);
            msdsNV.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbNVThongKe.Text = "Thống kê bán hàng nhân viên theo sản phẩm từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng giá trị: " + tongDoanhThu.ToString("N0") + " đồng";
        }
        private void gridView10_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn40)
            {
                if (_checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!_checkODau) _checkODau = true;
        }

        private void frmBCSanPham_Load(object sender, EventArgs e)
        {
            txtLNNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
            txtNVNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
            txtKHNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
            txtNCCNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
            HienThiLnTheoTuan();
            HienThiNvTheoTuan();
            HienThiKhTheoTuan();
            HienThiNccTheoTuan();
        }
        //Khách hàng
        private void btnKHTuan_Click(object sender, EventArgs e)
        {
            HienThiKhTheoTuan();
        }

        private void btnKHThang_Click(object sender, EventArgs e)
        {
            HienThiKhTheoThang();
        }

        private void btnKHLoc_Click(object sender, EventArgs e)
        {
            HienThiKhTheoNgay();
        }
        private void HienThiKhTheoTuan()
        {
            _checkThoiGian = 1;
            DataTable dt = bus.StatisticKH_ByWeek();
            msdsKH.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbKHThongKe.Text = "Thống kê bán hàng khách hàng theo sản phẩm của tuần này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng giá trị: " + tongDoanhThu.ToString("N0") + " đồng";
        }
        private void HienThiKhTheoThang()
        {
            _checkThoiGian = 2;
            DataTable dt = bus.StatisticKH_ByMonth();
            msdsKH.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbKHThongKe.Text = "Thống kê bán hàng khách hàng theo sản phẩm của tháng này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng giá trị: " + tongDoanhThu.ToString("N0") + " đồng";
        }
        private void HienThiKhTheoNgay()
        {
            _checkThoiGian = 3;
            this.ngayDau = txtKHNgayDau.Text;
            this.ngayCuoi = txtKHNgayCuoi.Text;
            string ngayDau = txtKHNgayDau.Text;
            string ngayCuoi = txtKHNgayCuoi.Text;
            DataTable dt = bus.StatisticKH_ByDate(ngayDau, ngayCuoi);
            msdsKH.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbKHThongKe.Text = "Thống kê bán hàng khách hàng theo sản phẩm từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng giá trị: " + tongDoanhThu.ToString("N0") + " đồng";
        }
        private void gridView18_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn93)
            {
                if (_checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!_checkODau) _checkODau = true;
        }
        //Nhà cung cấp
        private void btnNCCTuan_Click(object sender, EventArgs e)
        {
            HienThiNccTheoTuan();
        }

        private void btnNCCThang_Click(object sender, EventArgs e)
        {
            HienThiNccTheoThang();
        }

        private void btnNCCLoc_Click(object sender, EventArgs e)
        {
            HienThiNccTheoNgay();
        }
        private void HienThiNccTheoTuan()
        {
            DataTable dt = busCTNH.StatisticNCC_ByWeek();
            msdsNCC.DataSource = dt;
            int soLuongNhap = 0;
            double tongGiaTri = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongGiaTri += Convert.ToDouble(r["GiaTri"]);
                soLuongNhap += Convert.ToInt32(r["SoLuongSP"]);
            }
            lbNCCThongKe.Text = "Thống kê nhà cung cấp theo sản phẩm nhập của tuần này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã nhập: " + soLuongNhap.ToString("N0") + ", tổng giá trị: " + tongGiaTri.ToString("N0") + " đồng";
        }
        private void HienThiNccTheoThang()
        {
            var dt = busCTNH.StatisticNCC_ByMonth();
            msdsNCC.DataSource = dt;
            var soLuongNhap = 0;
            double tongGiaTri = 0;
            var soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongGiaTri += Convert.ToDouble(r["GiaTri"]);
                soLuongNhap += Convert.ToInt32(r["SoLuongSP"]);
            }
            lbNCCThongKe.Text = "Thống kê nhà cung cấp theo sản phẩm nhập của tháng này: Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã nhâp: " + soLuongNhap.ToString("N0") + ", tổng giá trị: " + tongGiaTri.ToString("N0") + " đồng";
        }
        private void HienThiNccTheoNgay()
        {
            string ngayDau = txtNCCNgayDau.Text;
            string ngayCuoi = txtNCCNgayCuoi.Text;
            DataTable dt = busCTNH.StatisticNCC_ByDate(ngayDau, ngayCuoi);
            msdsNCC.DataSource = dt;
            int soLuongNhap = 0;
            double tongGiaTri = 0;
            int soSanPham = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongGiaTri += Convert.ToDouble(r["GiaTri"]);
                soLuongNhap += Convert.ToInt32(r["SoLuongSP"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbNCCThongKe.Text = "Thống kê nhà cung cấp theo sản phẩm nhập từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soSanPham.ToString("N0") + " sản phẩm, số lượng sản phẩm đã nhập " + soLuongNhap.ToString("N0") + ", tổng giá trị: " + tongGiaTri.ToString("N0") + " đồng";
        }
        private void gridView20_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn109)
            {
                if (_checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!_checkODau) _checkODau = true;
        }

        private void msdsLN_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCSanPhamTheoLoiNhuanCT()
            {
                IDSanPham = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridView2.Columns[1]).ToString()
            };
            frm.ShowDialog();
        }

        private void msdsNV_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCSanPhamTheoNhanVienCT()
            {
                IDSanPham = gridView10.GetRowCellValue(gridView10.FocusedRowHandle, gridView10.Columns[1]).ToString(),
                CheckThoiGian = _checkThoiGian,
                NgayDau = ngayDau,
                NgayCuoi = ngayCuoi
            };
            frm.ShowDialog();
        }

        private void msdsKH_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCSanPhamTheoKhachHang()
            {
                IDSanPham = gridView18.GetRowCellValue(gridView18.FocusedRowHandle, gridView18.Columns[1]).ToString(),
                CheckThoiGian = _checkThoiGian,
                NgayDau = ngayDau,
                NgayCuoi = ngayCuoi
            };frm.ShowDialog();
        }

        private void msdsNCC_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCSanPhamTheoNCC()
            {
                IDSanPham = gridView20.GetRowCellValue(gridView20.FocusedRowHandle, gridView20.Columns[1]).ToString(),
                CheckThoiGian = _checkThoiGian,
                NgayDau = ngayDau,
                NgayCuoi = ngayCuoi
            }; frm.ShowDialog();
        }
    }
}