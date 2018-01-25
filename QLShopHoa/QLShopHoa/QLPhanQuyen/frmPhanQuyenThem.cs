using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLPhanQuyen
{
    public partial class frmPhanQuyenThem : DevExpress.XtraEditors.XtraForm
    {
        NhomQuyenBUS busNQ = new NhomQuyenBUS();
        PhanQuyenBUS busPQ = new PhanQuyenBUS();
        PhanQuyen obj = new PhanQuyen();
        public int IDNhom { get; set; }
        public frmPhanQuyenThem()
        {
            InitializeComponent();
        }

        private void frmPhanQuyenThem_Load(object sender, EventArgs e)
        {
            cbbNhom.Properties.DataSource = busNQ.GetDataNotInPhanQuyen();
            cbbNhom.Properties.ValueMember = "IDNhom";
            cbbNhom.Properties.DisplayMember = "TenNhom";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                obj.IDNhom = Convert.ToInt32(cbbNhom.EditValue.ToString());
                IDNhom = obj.IDNhom;
                //Group Khách Hàng
                obj.IDChucNang = "khachhang";
                obj.Xem = cbKHXem.Checked ? 1 : 0;
                obj.Them = cbKHThem.Checked ? 1 : 0;
                obj.Sua = cbKHSua.Checked ? 1 : 0;
                obj.Xoa = cbKHXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);
                //Group Nhà Cung Cấp
                obj.IDChucNang = "nhacungcap";
                obj.Xem = cbNCCXem.Checked ? 1 : 0;
                obj.Them = cbNCCThem.Checked ? 1 : 0;
                obj.Sua = cbNCCSua.Checked ? 1 : 0;
                obj.Xoa = cbNCCXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);
                //Group Đơn Vị Tính
                obj.IDChucNang = "donvitinh";
                obj.Xem = cbDVTXem.Checked ? 1 : 0;
                obj.Them = cbDVTThem.Checked ? 1 : 0;
                obj.Sua = cbDVTSua.Checked ? 1 : 0;
                obj.Xoa = cbDVTXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);
                //Group Loại Hàng
                obj.IDChucNang = "loaihang";
                obj.Xem = cbLHXem.Checked ? 1 : 0;
                obj.Them = cbLHThem.Checked ? 1 : 0;
                obj.Sua = cbLHSua.Checked ? 1 : 0;
                obj.Xoa = cbLHXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);//Group Sản Phẩm
                obj.IDChucNang = "sanpham";
                obj.Xem = cbSPXem.Checked ? 1 : 0;
                obj.Them = cbSPThem.Checked ? 1 : 0;
                obj.Sua = cbSPSua.Checked ? 1 : 0;
                obj.Xoa = cbSPXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);
                
                //Group Quản Lý Nhập Hàng
                obj.IDChucNang = "qlnhaphang";
                obj.Xem = cbQLNHXem.Checked ? 1 : 0;
                obj.Them = cbQLNHThem.Checked ? 1 : 0;
                obj.Sua = cbQLNHSua.Checked ? 1 : 0;
                obj.Xoa = cbQLNHXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);
                //Group Quản Lý Bán Hàng
                obj.IDChucNang = "qlbanhang";
                obj.Xem = cbQLBHXem.Checked ? 1 : 0;
                obj.Them = cbQLBHThem.Checked ? 1 : 0;
                obj.Sua = cbQLBHSua.Checked ? 1 : 0;
                obj.Xoa = cbQLBHXoa.Checked ? 1 : 0;
                busPQ.Insert(obj);
                //Group Quản Lý Báo Cáo
                obj.IDChucNang = "baocao";
                obj.Xem = cbBCXem.Checked ? 1 : 0;
                obj.Them = 0;
                obj.Sua = 0;
                obj.Xoa = 0;
                busPQ.Insert(obj);
                //Group Nhập Hàng
                obj.IDChucNang = "nhaphang";
                obj.Xem = cbNHXem.Checked ? 1 : 0;
                obj.Them = 0;
                obj.Sua = 0;
                obj.Xoa = 0;
                busPQ.Insert(obj);
                //Group Bán Hàng
                obj.IDChucNang = "banhang";
                obj.Xem = cbBHXem.Checked ? 1 : 0;
                obj.Them = 0;
                obj.Sua = 0;
                obj.Xoa = 0;
                busPQ.Insert(obj);
                XtraMessageBox.Show("Thêm phân quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }
        private bool ValidateData()
        {
            if (this.cbbNhom.Text.Trim().Equals(string.Empty))
            {
                this.cbbNhom.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhóm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}