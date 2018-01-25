﻿using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhaCungCap
{
    public partial class frmNhaCungCap : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        NhaCungCap obj = new NhaCungCap();
        NhaCungCapBUS bus = new NhaCungCapBUS();
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
                if (dataRow["IDChucNang"].Equals("nhacungcap") && Convert.ToInt32(dataRow["Them"]) == 1)
                    btnThem.Enabled = true;
                if (dataRow["IDChucNang"].Equals("nhacungcap") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("nhacungcap") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void HienThi()
        {
            msdsKhachHang.DataSource = bus.GetData();
        }
        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            MoKhoaDieuKhien();
            HienThi();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn0)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmNhaCungCapThem frmAdd = new frmNhaCungCapThem();
            frmAdd.ShowDialog();
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmNhaCungCapSua frmEdit = new frmNhaCungCapSua();
            frmEdit.IDNhaCungCap = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            frmEdit.TenNhaCungCap = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            frmEdit.DienThoai = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
            frmEdit.DiaChi = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[4]).ToString();
            frmEdit.GhiChu = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[5]).ToString();
            frmEdit.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa nhà cung cấp này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bus.Delete(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString());
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

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
        }
    }
}