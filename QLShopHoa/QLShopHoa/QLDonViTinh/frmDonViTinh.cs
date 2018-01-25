using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using ValueObject;

namespace QLShopHoa.QLDonViTinh
{
    public partial class frmDonViTinh : DevExpress.XtraEditors.XtraForm
    {
        private bool checkODau = false;
        DonViTinh obj = new DonViTinh();
        DonViTinhBUS bus = new DonViTinhBUS();
     
        public frmDonViTinh()
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
                if (dataRow["IDChucNang"].Equals("donvitinh") && Convert.ToInt32(dataRow["Them"]) == 1)
                    btnThem.Enabled = true;
                if (dataRow["IDChucNang"].Equals("donvitinh") && Convert.ToInt32(dataRow["Sua"]) == 1)
                    btnSua.Enabled = true;
                if (dataRow["IDChucNang"].Equals("donvitinh") && Convert.ToInt32(dataRow["Xoa"]) == 1)
                    btnXoa.Enabled = true;
            }
        }
        private void HienThi()
        {
            msdsDonViTinh.DataSource = bus.GetData();
        }
        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            MoKhoaDieuKhien();
            HienThi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmDonViTinhThem frmAdd = new frmDonViTinhThem();
            frmAdd.ShowDialog();
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmDonViTinhSua frmEdit = new frmDonViTinhSua();
            frmEdit.TenDonViTinh = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            frmEdit.GhiChu = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            frmEdit.IDDonViTinh = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString());
            frmEdit.ShowDialog();
            HienThi();
            KhoaDieuKhien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa đơn vị tính này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bus.Delete(Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString()));
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
            if (cbUnRepeatable.Checked)
            {
                layoutDemoLoi.Visibility = LayoutVisibility.Always;
                int IDDonViTinh = Convert.ToInt32(gridView1
                    .GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString());
                if (cbFixUnRepeatable.Checked)
                    msdsDonViTinh2.DataSource = bus.GetData_Fix_UnRepeatable(IDDonViTinh);
                else msdsDonViTinh2.DataSource = bus.GetData_UnRepeatable(IDDonViTinh);
            }
            else if (cbPhantom.Checked)
            {
                layoutDemoLoi.Visibility = LayoutVisibility.Always;if (cbFixPhantom.Checked)
                    msdsDonViTinh2.DataSource = bus.GetData_Fix_Phantom();
                else msdsDonViTinh2.DataSource = bus.GetData_Phantom();
            }
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

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoKhoaDieuKhien();
        }
    }
}