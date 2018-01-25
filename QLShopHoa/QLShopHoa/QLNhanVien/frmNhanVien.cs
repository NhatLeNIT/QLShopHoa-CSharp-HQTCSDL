using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhanVien
{
    public partial class frmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        public frmNhanVien()
        {
            InitializeComponent();
        }

        NhanVien obj = new NhanVien();
        NhanVienBUS bus = new NhanVienBUS();

        private void KhoaDieuKhien()
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void MoKhoaDieuKhien()
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void HienThi()
        {
            msdsNhanVien.DataSource = bus.GetData();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            KhoaDieuKhien();
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


        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            HienThi();
            KhoaDieuKhien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmNhanVienThem frmAdd = new frmNhanVienThem();
            frmAdd.ShowDialog();
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string IDNhanVien = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            var dt = bus.GetDataByID(IDNhanVien);
            if (frmMain.IDNhom != Convert.ToInt32(dt.Rows[0]["IDNhom"]) ||
                frmMain.IDNhanVien.Equals("NV000001") || frmMain.IDNhanVien.Equals(IDNhanVien)){
                frmNhanVienSua frmEdit = new frmNhanVienSua();
                frmEdit.IDNhanVien = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1])
                    .ToString();
                frmEdit.ShowDialog();
                HienThi();
                KhoaDieuKhien();
            }
            else
            {
                XtraMessageBox.Show("Bạn không có quyền sửa thông tin nhân viên này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string IDNhanVien = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
                    var dt = bus.GetDataByID(IDNhanVien);
                    if (!IDNhanVien.Equals("NV000001"))
                    {
                        if (frmMain.IDNhom != Convert.ToInt32(dt.Rows[0]["IDNhom"]) ||
                            frmMain.IDNhanVien.Equals("NV000001"))
                        {
                            bus.Delete(IDNhanVien);
                            XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HienThi();
                            KhoaDieuKhien();
                        }
                        else
                        {
                            XtraMessageBox.Show("Bạn không được phép xóa nhân viên này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Bạn không có quyền xóa nhân viên này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch
                {
                }
            }
        }

        private void msdsNhanVien_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
            if (frmMain.IDNhanVien.Equals(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString()))
            {
                btnXoa.Enabled = false;
            }
        }
    }
}