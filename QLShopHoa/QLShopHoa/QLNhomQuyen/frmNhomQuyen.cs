using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhomQuyen
{
    public partial class frmNhomQuyen : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        public frmNhomQuyen()
        {
            InitializeComponent();
        }
        NhomQuyen obj = new NhomQuyen();
        NhomQuyenBUS bus = new NhomQuyenBUS();
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
            msdsKhachHang.DataSource = bus.GetData();
        }

        private void frmNhomQuyen_Load(object sender, EventArgs e)
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmNhomQuyenThem frmAdd = new frmNhomQuyenThem();
            frmAdd.ShowDialog();
            HienThi();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            HienThi();
            KhoaDieuKhien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa nhóm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //try
                //{
                if (bus.Delete(Convert.ToInt32(gridView1
                        .GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString())) != -1)
                    XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                else
                {
                    XtraMessageBox.Show("Nhóm quyền này đã được phân quyền, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    HienThi();
                    KhoaDieuKhien();
                //}
                //catch
                //{
                //    XtraMessageBox.Show("Nhóm quyền này đã được phân quyền, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    QuerySQLBUS busSQL = new QuerySQLBUS();
                //    busSQL.CloseConnect();}
            }
        }

        private void msdsKhachHang_Click(object sender, EventArgs e)
        {
            MoKhoaDieuKhien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmNhomQuyenSua frmEdit = new frmNhomQuyenSua();
            frmEdit.IDNhom = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
            frmEdit.TenNhom = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            frmEdit.MoTa = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            frmEdit.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }
    }
}