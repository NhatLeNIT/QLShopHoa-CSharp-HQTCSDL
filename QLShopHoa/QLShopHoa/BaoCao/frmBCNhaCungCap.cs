using System;
using System.Data;
using BusinessLogicLayer;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCNhaCungCap : DevExpress.XtraEditors.XtraForm
    {
        private bool _checkODau = false;
        ChiTietNhapHangBUS bus = new ChiTietNhapHangBUS();
        private int _checkThoiGian; //tuan = 1, thang = 2, ngay = 3
        private string ngayDau;
        private string ngayCuoi;
        public frmBCNhaCungCap()
        {
            InitializeComponent();
            txtBHNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
        }

        private void frmBCNhaCungCap_Load(object sender, EventArgs e)
        {
            HienThiBhTheoTuan();
        }

        private void btnNHTheoTuan_Click(object sender, EventArgs e)
        {
            HienThiBhTheoTuan();
        }

        private void btnNHTheoThang_Click(object sender, EventArgs e)
        {
            HienThiBhTheoThang();
        }

        private void btnNHLoc_Click(object sender, EventArgs e)
        {
            HienThiBhTheoNgay();
        }
        private void HienThiBhTheoTuan()
        {
            _checkThoiGian = 1;
            DataTable dt = bus.NCCStatistic_ByWeek();
            msdsNH.DataSource = dt;
            double sum = 0;
            int soLuongNhap = 0;
            int soNhaCungCap = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                sum += Convert.ToDouble(r["TongTien"]);
                soLuongNhap += Convert.ToInt32(r["SoLuong"]);
            }
            lbThongKe.Text = "Thống kê nhập hàng theo nhà cung cấp của tuần này: Tất cả có " + soNhaCungCap + " nhà cung cấp, số lượng sản phẩm đã nhập " + soLuongNhap + ", tổng giá trị: " + sum.ToString("N0") + " đồng";
        }
        private void HienThiBhTheoThang()
        {
            _checkThoiGian = 2;
            DataTable dt = bus.NCCStatistic_ByMonth();
            msdsNH.DataSource = dt;
            double sum = 0;
            int soLuongNhap = 0;
            int soNhaCungCap = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                sum += Convert.ToDouble(r["TongTien"]);
                soLuongNhap += Convert.ToInt32(r["SoLuong"]);
            }
            lbThongKe.Text = "Thống kê nhập hàng theo nhà cung cấp của tháng này: Tất cả có " + soNhaCungCap + " nhà cung cấp, số lượng sản phẩm đã nhập " + soLuongNhap + ", tổng giá trị: " + sum.ToString("N0") + " đồng";
        }
        private void HienThiBhTheoNgay()
        {
            _checkThoiGian = 3;
            this.ngayDau = txtBHNgayDau.Text;
            this.ngayCuoi = txtBHNgayCuoi.Text;
            string ngayDau = txtBHNgayDau.Text;
            string ngayCuoi = txtBHNgayCuoi.Text;
            DataTable dt = bus.NCCStatistic_ByDate(ngayDau, ngayCuoi);
            msdsNH.DataSource = dt;
            double sum = 0;
            int soLuongNhap = 0;
            int soNhaCungCap = dt.Rows.Count;
            foreach (DataRow r in dt.Rows)
            {
                sum += Convert.ToDouble(r["TongTien"]);
                soLuongNhap += Convert.ToInt32(r["SoLuong"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbThongKe.Text = "Thống kê nhập hàng theo nhà cung cấp từ " + ngayDau + " đến " + ngayCuoi + ": Tất cả có " + soNhaCungCap + " nhà cung cấp, số lượng sản phẩm đã nhập " + soLuongNhap + ", tổng giá trị: " + sum.ToString("N0") + " đồng";
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

        private void msdsNH_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmBCNhaCungCapTheoNhapHangCT()
            {
                IDNhaCungCap = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString(),
                CheckThoiGian = _checkThoiGian,
                NgayDau = ngayDau,
                NgayCuoi = ngayCuoi
            };
            frm.ShowDialog();
        }
    }
}