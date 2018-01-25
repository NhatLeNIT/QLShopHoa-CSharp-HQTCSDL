﻿using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLNhapHang;
using ValueObject;

namespace QLShopHoa.NhapHang
{
    public partial class frmQLNhapHangChiTiet : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        ChiTietNhapHangBUS busCTNH = new ChiTietNhapHangBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        public string IDNhapHang { get; set; }
        public frmQLNhapHangChiTiet()
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
                if (dataRow["IDChucNang"].Equals("qlnhaphang") && Convert.ToInt32(dataRow["Them"]) == 1)
                    btnThem.Enabled = true;
                if (dataRow["IDChucNang"].Equals("qlnhaphang") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("qlnhaphang") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void HienThi()
        {
            msds.DataSource = busCTNH.GetDataByID(IDNhapHang);
        }

        private void frmThongKeNhapHangChiTiet_Load(object sender, EventArgs e)
        {
            HienThi();
            MoKhoaDieuKhien();
            this.Text = "Nhập Hàng Chi Tiết Của Phiếu Nhập: " + IDNhapHang;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmQLNhapHangSPSua frmEdit = new frmQLNhapHangSPSua();
            frmEdit.IDNhapHang = IDNhapHang;
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
                    DataTable dt = busCTNH.GetDataByIDSanPham(IDNhapHang, IDSanPham);
                    SanPham objSP = new SanPham();
                    objSP.IDSanPham = IDSanPham;
                    objSP.SoLuong = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
                    busSP.UpdateQuantitySub(objSP);
                    busCTNH.DeleteByIDSanPham(IDNhapHang, IDSanPham);
                    XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (e.Column == gridColumn1)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmQLNhapHangSPThem frm = new frmQLNhapHangSPThem();
            frm.IDNhapHang = IDNhapHang;
            frm.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
        }
    }
}