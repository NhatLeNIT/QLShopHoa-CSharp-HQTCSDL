using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLBanHang
{
    public partial class frmQLBanHang : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        HoaDon obj = new HoaDon();
        HoaDonBUS bus = new HoaDonBUS();
        ChiTietHoaDonBUS busCTHD = new ChiTietHoaDonBUS();
        public frmQLBanHang()
        {
            InitializeComponent();
        }
        private void KhoaDieuKhien()
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void MoKhoaDieuKhien()
        {
            checkPhanQuyenBUS busPQ = new checkPhanQuyenBUS();
            var dt = busPQ.GetDataTablePhanQuyen(frmMain.IDNhanVien);
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["IDChucNang"].Equals("qlbanhang") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("qlbanhang") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void frmQLBanHang_Load(object sender, EventArgs e)
        {
            txtNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
            MoKhoaDieuKhien();
            HienThi();
        }
        private void HienThi()
        {
            DataTable dt = bus.GetData();
            msds.DataSource = dt;
            double sum = 0;
            int soHoaDon = dt.Rows.Count;
            int soSanPham = 0;
            foreach (DataRow r in dt.Rows)
            {
                sum += Convert.ToDouble(r["TongTien"]);
                soSanPham += Convert.ToInt32(r["SoLuongSanPham"]);
            }
            lbThongKe.Text = "Thống kê: Tất cả có " + soHoaDon + " hóa đơn, " + soSanPham + " sản phẩm đã bán, tổng tiền: " + sum.ToString("N0") + " đồng";
        }

        private void msds_DoubleClick(object sender, EventArgs e)
        {
            frmQLBanHangChiTiet frm = new frmQLBanHangChiTiet();
            frm.IDHoaDon = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            frm.ShowDialog();
            HienThi();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            string NgayDau = txtNgayDau.Text;
            string NgayCuoi = txtNgayCuoi.Text;
            DataTable dt = bus.GetDataByDate(NgayDau, NgayCuoi);
            msds.DataSource = dt;
            double sum = 0;
            int soHoaDon = dt.Rows.Count;
            int soSanPham = 0;
            foreach (DataRow r in dt.Rows)
            {
                sum += Convert.ToDouble(r["TongTien"]);
                soSanPham += Convert.ToInt32(r["SoLuongSanPham"]);
            }
            if (NgayDau.Trim().Equals(string.Empty)) NgayDau = "đầu tiên";
            lbThongKe.Text = "Thống kê từ ngày " + NgayDau + " tới " + NgayCuoi + ": Tất cả có " + soHoaDon + " hóa đơn, " + soSanPham + " sản phẩm đã bán, tổng tiền: " + sum.ToString("N0") + " đồng";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
            frmQLBanHangSua frmEdit = new frmQLBanHangSua();
            frmEdit.IDHoaDon = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            frmEdit.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa đơn hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    //busCTHD.Delete(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
                    if(bus.Delete(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString()) != -1)
                        XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else XtraMessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HienThi();
                    KhoaDieuKhien();
                }
                catch
                {
                }
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            HienThi();
            KhoaDieuKhien();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn8)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
        }
    }
}