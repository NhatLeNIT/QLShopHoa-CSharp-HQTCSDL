using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;

namespace QLShopHoa.QLPhanQuyen
{
    public partial class frmPhanQuyen : DevExpress.XtraEditors.XtraForm
    {
        private static int IDNhom { get; set; }
        public frmPhanQuyen()
        {
            InitializeComponent();
        }

        NhomQuyenBUS busNQ = new NhomQuyenBUS(); PhanQuyenBUS busPQ = new PhanQuyenBUS();

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

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            HienThiNhomQuyen();
            KhoaDieuKhien();
        }
        void HienThiNhomQuyen()
        {
            listNhomQuyen.DataSource = busNQ.GetDataInPhanQuyen();
            listNhomQuyen.KeyFieldName = "IDNhom";
        }
        private void listNhomQuyen_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            frmPhanQuyen.IDNhom = Convert.ToInt32(e.Node.GetValue("IDNhom").ToString());
            HienThi(frmPhanQuyen.IDNhom);
            if (frmPhanQuyen.IDNhom != 1)
                MoKhoaDieuKhien();
            else
                KhoaDieuKhien();
        }
        void HienThi(int IDNhom)
        {
            DataTable dt = busNQ.GetDataByID(IDNhom);
            //txtName.Text = "Chức năng của nhóm: " + dt.Rows[0]["TenNhom"].ToString();
            msdsPhanQuyen.DataSource = busPQ.GetDataByIDNhom(IDNhom);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (busNQ.GetDataNotInPhanQuyen().Rows.Count == 0)
            {
                XtraMessageBox.Show("Không còn nhóm quyền nào chưa thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmPhanQuyenThem frmAdd = new frmPhanQuyenThem();
                frmAdd.ShowDialog();
                HienThiNhomQuyen();
                KhoaDieuKhien();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            frmPhanQuyenSua frmEdit = new frmPhanQuyenSua();
            frmEdit.IDNhom = IDNhom;
            frmEdit.ShowDialog();
            HienThiNhomQuyen();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa phân quyền của nhóm này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    busPQ.Delete(IDNhom);
                    XtraMessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiNhomQuyen();
                }
                catch
                {
                }
            }
        }
    }
}