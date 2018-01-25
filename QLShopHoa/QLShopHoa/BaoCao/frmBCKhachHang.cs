using System;
using System.Data;
using BusinessLogicLayer;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCKhachHang : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        HoaDonBUS bus = new HoaDonBUS();
        ChiTietHoaDonBUS busCTHD = new ChiTietHoaDonBUS();
        private int _checkThoiGian = 1; //tuan = 1, thang = 2, ngay = 3
        private string ngayDau = "";
        private string ngayCuoi = "";
        public frmBCKhachHang()
        {
            InitializeComponent();
        }
        private void frmBCKhachHang_Load(object sender, EventArgs e)
        {
            HienThiLNTheoTuan();
            txtLNNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
        }


        //Lợi nhuận
        private void btnLNTuan_Click(object sender, EventArgs e)
        {
            HienThiLNTheoTuan();
        }

        private void btnLNThang_Click(object sender, EventArgs e)
        {
            HienThiLNTheoThang();
        }

        private void btnLNLoc_Click(object sender, EventArgs e)
        {
            HienThiLNTheoNgay();
        }
        private void HienThiLNTheoTuan()
        {
            _checkThoiGian = 1;
            DataTable dt = busCTHD.KHStatisticLN_ByWeek();
            msdsLN.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soKhachHang = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbLNThongKe.Text = "Thống kê lợi nhuận theo khách hàng của tuần này: Tất cả có " + soKhachHang.ToString("N0") + " khách hàng, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng doanh thu: " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void HienThiLNTheoThang()
        {
            _checkThoiGian = 2;
            DataTable dt = busCTHD.KHStatisticLN_ByMonth();
            msdsLN.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soKhachHang = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            lbLNThongKe.Text = "Thống kê lợi nhuận theo khách hàng của tháng này: Tất cả có " + soKhachHang.ToString("N0") + " khách hàng, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng doanh thu: " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được " + tongLoiNhuan.ToString("N0") + " đồng";

        }
        private void HienThiLNTheoNgay()
        {
            _checkThoiGian = 3;
            this.ngayDau = txtLNNgayDau.Text;
            this.ngayCuoi = txtLNNgayCuoi.Text;
            string ngayDau = txtLNNgayDau.Text;
            string ngayCuoi = txtLNNgayCuoi.Text;
            DataTable dt = busCTHD.KHStatisticLN_ByDate(ngayDau, ngayCuoi);
            msdsLN.DataSource = dt;
            int soLuongBan = 0;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soKhachHang = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
                soLuongBan += Convert.ToInt32(r["SoLuongBan"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbLNThongKe.Text = "Thống kê lợi nhuận theo khách hàng từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soKhachHang.ToString("N0") + " khách hàng, số lượng sản phẩm đã bán " + soLuongBan.ToString("N0") + ", tổng doanh thu: " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn8)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void msdsLN_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCKhachHangTheoLoiNhuanCT()
            {
                IDKhachHang = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridView2.Columns[1]).ToString(),
                CheckThoiGian = _checkThoiGian,
                NgayDau = ngayDau,
                NgayCuoi = ngayCuoi
            };
            frm.ShowDialog();
        }
    }
}