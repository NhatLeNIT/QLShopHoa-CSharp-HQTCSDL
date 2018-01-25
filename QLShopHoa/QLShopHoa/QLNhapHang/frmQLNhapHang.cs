using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;

namespace QLShopHoa.NhapHang
{
    public partial class frmQLNhapHang : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        ValueObject.NhapHang obj = new ValueObject.NhapHang();
        NhapHangBUS bus = new NhapHangBUS();
        ChiTietNhapHangBUS busCTNH = new ChiTietNhapHangBUS();
        public frmQLNhapHang()
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
                if (dataRow["IDChucNang"].Equals("qlnhaphang") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("qlnhaphang") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void frmThongKeNhapHang_Load(object sender, EventArgs e)
        {
            txtNgayCuoi.Text = DateTime.Now.ToString("dd-MMM-yy");
            MoKhoaDieuKhien();
            HienThi();
        }
        private void HienThi()
        {
            var dt = bus.GetData();
            msds.DataSource = dt;
            double sum = 0;
            var soPhieuNhap = dt.Rows.Count;
            var soSanPham = 0;
            foreach (DataRow r in dt.Rows){
                sum += Convert.ToDouble(r["TongTien"]);
                soSanPham += Convert.ToInt32(r["SoLuongSanPham"]);
            }
            lbThongKe.Text = "Thống kê: Tất cả có " + soPhieuNhap+ " phiếu nhập hàng, " + soSanPham  + " sản phẩm đã nhập, tổng tiền: " + sum.ToString("N0") + " đồng";
        }


        private void msds_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmQLNhapHangChiTiet
            {
                IDNhapHang = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString()
            };
            frm.ShowDialog();
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
            var frmEdit = new frmQLNhapHangSua
            {
                IDNhapHang = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString()
            };
            frmEdit.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập hàng này không?", "Thông báo", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                //busCTNH.Delete(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
                if(bus.Delete(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString()) != -1)
                    XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else XtraMessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HienThi();
                KhoaDieuKhien();
            }
            catch
            {
                // ignored
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            HienThi();
            KhoaDieuKhien();
        }

        private void gridView1_CustomDrawCell_1(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn8)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            var ngayDau = txtNgayDau.Text;
            var ngayCuoi = txtNgayCuoi.Text;
            var dt = bus.GetDataByDate(ngayDau, ngayCuoi);
            msds.DataSource = dt;
            double sum = 0;
            var soPhieuNhap = dt.Rows.Count;
            var soSanPham = 0;
            foreach (DataRow r in dt.Rows)
            {
                sum += Convert.ToDouble(r["TongTien"]);
                soSanPham += Convert.ToInt32(r["SoLuongSanPham"]);
            }
            if (ngayDau.Trim().Equals(string.Empty)) ngayDau = "đầu tiên";
            lbThongKe.Text = "Thống kê từ ngày " + ngayDau + " tới " + ngayCuoi + ": Tất cả có " + soPhieuNhap + " phiếu nhập hàng, " + soSanPham + " sản phẩm đã nhập, tổng tiền: " + sum.ToString("N0") + " đồng";
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
        }


    }
}