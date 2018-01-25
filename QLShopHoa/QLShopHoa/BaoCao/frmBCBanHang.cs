using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using QLShopHoa.QLBanHang;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCBanHang : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        HoaDonBUS bus = new HoaDonBUS();
        public frmBCBanHang()
        {
            InitializeComponent();
        }

        private void frmBCBanHang_Load(object sender, EventArgs e)
        {
            HienThiTheoTuan();
            txtNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
        }

        private void HienThiTheoTuan()
        {
            DataTable dt = bus.BHStatistic_ByWeek();
            msdsThoiGian.DataSource = dt;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
            }
            lbThongKe.Text = "Thống kê theo tuần này: Tổng doanh thu " + tongDoanhThu.ToString("N0") + " đồng, tổng lợi nhuận đạt được: " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void HienThiTheoThang()
        {
            DataTable dt = bus.BHStatistic_ByMonth();
            msdsThoiGian.DataSource = dt;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
            }
            lbThongKe.Text = "Thống kê theo tháng này: Tổng doanh thu " + tongDoanhThu.ToString("N0") + " đồng, tổng lợi nhuận đạt được: " + tongLoiNhuan.ToString("N0") + " đồng";
        }
        private void HienThiTheoNgay()
        {
            string ngayDau = txtNgayDau.Text;
            string ngayCuoi = txtNgayCuoi.Text;
            DataTable dt = bus.BHStatistic_ByDate(ngayDau, ngayCuoi);
            msdsThoiGian.DataSource = dt;
            double tongDoanhThu = 0;
            double tongLoiNhuan = 0;
            foreach (DataRow r in dt.Rows)
            {
                tongDoanhThu += Convert.ToDouble(r["DoanhThu"]);
                tongLoiNhuan += Convert.ToDouble(r["LoiNhuan"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbThongKe.Text = "Thống kê từ ngày " + ngayDau + " đến " + ngayCuoi + ": Tổng doanh thu " + tongDoanhThu.ToString("N0") + " đồng, tổng lợi nhuận đạt được: " + tongLoiNhuan.ToString("N0") + " đồng";
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn1)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void btnTheoTuan_Click(object sender, EventArgs e)
        {
            HienThiTheoTuan();
        }

        private void btnTheoThang_Click(object sender, EventArgs e)
        {
            HienThiTheoThang();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            HienThiTheoNgay();
        }

        private void msdsThoiGian_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCBanHangTheoThoiGianCT
            {
                NgayLap = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString()
            };
            frm.ShowDialog();
        }
    }
}