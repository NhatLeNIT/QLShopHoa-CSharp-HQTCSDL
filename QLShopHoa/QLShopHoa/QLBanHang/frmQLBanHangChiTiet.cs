using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLBanHang
{
    public partial class frmQLBanHangChiTiet : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        ChiTietHoaDonBUS busCTHD = new ChiTietHoaDonBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        HoaDonBUS busHD = new HoaDonBUS();
        public string IDHoaDon { get; set; }
        QuerySQLBUS query = new QuerySQLBUS();
        public frmQLBanHangChiTiet()
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
                if (dataRow["IDChucNang"].Equals("qlbanhang") && Convert.ToInt32(dataRow["Them"]) == 1)
                    btnThem.Enabled = true;
                if (dataRow["IDChucNang"].Equals("qlbanhang") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("qlbanhang") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void HienThi()
        {
            msds.DataSource = busCTHD.GetDataByID(IDHoaDon);
        }
        private void frmQLBanHangChiTiet_Load(object sender, EventArgs e)
        {
            HienThi();
            MoKhoaDieuKhien();
            this.Text = "Bán Hàng Chi Tiết Của Hóa Đơn: " + IDHoaDon;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmQLBanHangSPThem frm = new frmQLBanHangSPThem();
            frm.IDHoaDon = IDHoaDon;
            frm.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmQLBanHangSPSua frmEdit = new frmQLBanHangSPSua();
            frmEdit.IDHoaDon = IDHoaDon;
            frmEdit.IDSanPham = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            frmEdit.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string IDSanPham = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
                    DataTable dt = busCTHD.GetDataByIDSanPham(IDHoaDon, IDSanPham);
                    //update số lượng sản phẩm
                    SanPham objSP = new SanPham();
                    objSP.IDSanPham = IDSanPham;
                    objSP.SoLuong = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
                    busSP.UpdateQuantity(objSP);
                    double donGia = Convert.ToDouble(dt.Rows[0]["DonGia"]);
                    //update số lượng sản phẩm và tổng tiền trong đơn hàng
                    string sql = "SELECT SoLuongSanPham, TongTien FROM HoaDon WHERE IDHoaDon='" + IDHoaDon + "'";
                    dt = query.GetDataBySQL(sql);
                    int quantity = Convert.ToInt32(dt.Rows[0]["SoLuongSanPham"]) - objSP.SoLuong;
                    double totalPrice = Convert.ToDouble(dt.Rows[0]["TongTien"]) - (objSP.SoLuong * donGia);
                    sql = "UPDATE HoaDon SET SoLuongSanPham = " + quantity + ", TongTien = " + totalPrice + " WHERE IDHoaDon = '" + IDHoaDon + "'";
                    query.ExecuteBySQL(sql);
                    busCTHD.DeleteByIDSanPham(IDHoaDon, IDSanPham);
                    XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThi();
                    KhoaDieuKhien();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
            if (e.Column == gridColumn1)
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