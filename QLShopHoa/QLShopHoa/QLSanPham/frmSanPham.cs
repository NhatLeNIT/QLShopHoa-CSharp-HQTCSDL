using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLSanPham
{
    public partial class frmSanPham : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        public frmSanPham()
        {
            InitializeComponent();
        }

        SanPham obj = new SanPham();
        SanPhamBUS bus = new SanPhamBUS();

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
                if (dataRow["IDChucNang"].Equals("sanpham") && Convert.ToInt32(dataRow["Them"]) == 1)
                    btnThem.Enabled = true;
                if (dataRow["IDChucNang"].Equals("sanpham") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("sanpham") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void HienThi()
        {
            msdsSanPham.DataSource = bus.GetDataAll();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            frmSanPhamThem frmAdd = new frmSanPhamThem();
            frmAdd.ShowDialog();
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmSanPhamSua frmEdit = new frmSanPhamSua();
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

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn0)
            {
                if (checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!checkODau) checkODau = true;
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            MoKhoaDieuKhien();
            HienThi();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
        }
    }
}