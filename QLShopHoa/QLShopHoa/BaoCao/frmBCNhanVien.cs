using System;
using System.Data;
using BusinessLogicLayer;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCNhanVien : DevExpress.XtraEditors.XtraForm
    {
        private bool _checkODau = false;
        HoaDonBUS bus = new HoaDonBUS();
        private int _checkThoiGian = 1; //tuan = 1, thang = 2, ngay = 3
        private string ngayDau = "";
        private string ngayCuoi = "";
        public frmBCNhanVien()
        {
            InitializeComponent();
        }

        private void btnBHTheoTuan_Click(object sender, EventArgs e)
        {
            HienThiBhTheoTuan();
            
        }

        private void btnBHTheoThang_Click(object sender, EventArgs e)
        {
            HienThiBhTheoThang();
        }

        private void btnBHLoc_Click(object sender, EventArgs e)
        {
            HienThiBhTheoNgay();
        }
        private void HienThiBhTheoTuan()
        {
            _checkThoiGian = 1;
            DataTable dt = bus.NVStatistic_ByWeek();
            msdsBH.DataSource = dt;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soNhanVien = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
            }
        
            lbThongKe.Text = "Thống kê bán hàng theo nhân viên của tuần này: Tất cả có " + soNhanVien + " nhân viên, tổng doanh thu " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được: " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void HienThiBhTheoThang()
        {
            _checkThoiGian = 2;
            DataTable dt = bus.NVStatistic_ByMonth();
            msdsBH.DataSource = dt;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soNhanVien = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
            }
            lbThongKe.Text = "Thống kê bán hàng theo nhân viên của tháng này: Tất cả có " + soNhanVien + " nhân viên, tổng doanh thu " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được: " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void HienThiBhTheoNgay()
        {
            _checkThoiGian = 3;
            this.ngayDau = txtBHNgayDau.Text;
            this.ngayCuoi = txtBHNgayCuoi.Text;
            string ngayDau = txtBHNgayDau.Text;
            string ngayCuoi = txtBHNgayCuoi.Text;
            DataTable dt = bus.NVStatistic_ByDate(ngayDau, ngayCuoi);
            msdsBH.DataSource = dt;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            int soNhanVien = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbThongKe.Text = "Thống kê bán hàng theo nhân viên từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soNhanVien + " nhân viên, tổng doanh thu " + tongDoanhThu.ToString("N0") + " đồng, lợi nhuận đạt được: " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn1)
            {
                if (_checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!_checkODau) _checkODau = true;
        }

        private void frmBCNhanVien_Load(object sender, EventArgs e)
        {
            HienThiBhTheoTuan();
            txtBHNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
        }

        private void msdsBH_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCNhanVienTheoBanHangCT()
            {
                IDNhanVien = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString(),
                CheckThoiGian = _checkThoiGian,
                NgayDau = ngayDau,
                NgayCuoi = ngayCuoi
            };
            frm.ShowDialog();
        }
    }
}