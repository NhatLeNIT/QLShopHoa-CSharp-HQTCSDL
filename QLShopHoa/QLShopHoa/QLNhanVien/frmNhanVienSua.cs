using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhanVien
{
    public partial class frmNhanVienSua : DevExpress.XtraEditors.XtraForm
    {

        public string IDNhanVien = "";
        public frmNhanVienSua()
        {
            InitializeComponent();
        }

        NhanVien obj = new NhanVien();
        NhanVienBUS busNV = new NhanVienBUS();
        NhomQuyenBUS busNQ = new NhomQuyenBUS();
        md5Convert md5 = new md5Convert();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = busNV.GetDataByID(txtIDNhanVien.Text);

                obj.IDNhanVien = txtIDNhanVien.Text;
                obj.HoTen = txtHoTen.Text;
                obj.DienThoai = txtDienThoai.Text;
                obj.DiaChi = txtDiaChi.Text;
                obj.NgaySinh = txtNgaySinh.Text;
                obj.GhiChu = txtGhiChu.Text;
                obj.GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                obj.TaiKhoan = txtTaiKhoan.Text;
                if (!txtMatKhau.Text.Equals(string.Empty))
                    obj.MatKhau = md5.md5(txtMatKhau.Text);
                else obj.MatKhau = dt.Rows[0]["MatKhau"].ToString();
                obj.TrangThai = cbTrangThai.Checked ? 1 : 0;
                obj.IDNhom = Convert.ToInt32(cbbNhom.EditValue.ToString());
                busNV.Update(obj);
                XtraMessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                
            }
        }
       
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmNhanVienSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busNV.GetDataByID(IDNhanVien);
            txtIDNhanVien.Text = IDNhanVien;
            txtTaiKhoan.Text = dt.Rows[0]["TaiKhoan"].ToString();
            txtHoTen.Text = dt.Rows[0]["HoTen"].ToString();
            txtDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            txtNgaySinh.Text = dt.Rows[0]["NgaySinh"].ToString();
            if (dt.Rows[0]["GioiTinh"].ToString().Equals("Nam"))
                rbNam.Checked = true;
            else rbNu.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["TrangThai"].ToString()) == true)
                cbTrangThai.Checked = true;
            txtDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
            txtGhiChu.Text = dt.Rows[0]["GhiChu"].ToString();
            cbbNhom.Properties.DataSource = busNQ.GetData();
            cbbNhom.Properties.ValueMember = "IDNhom";
            cbbNhom.Properties.DisplayMember = "TenNhom";
            DataTable dtNhom = busNQ.GetDataByID(Convert.ToInt32(dt.Rows[0]["IDNhom"].ToString()));
            cbbNhom.EditValue = cbbNhom.Properties.GetKeyValueByDisplayText(dtNhom.Rows[0]["TenNhom"].ToString());
            txtIDNhanVien.Enabled = false;
            txtTaiKhoan.Enabled = false;
        }
        private bool ValidateData()
        {

           if (this.txtHoTen.Text.Trim().Equals(string.Empty))
            {
                this.txtHoTen.Focus();
                XtraMessageBox.Show("Bạn chưa nhập họ tên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtDienThoai.Text.Trim().Equals(string.Empty))
            {
                this.txtDienThoai.Focus();
                XtraMessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.cbbNhom.Text.Trim().Equals(string.Empty))
            {
                this.txtDienThoai.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhóm quyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if ((!this.txtMatKhau.Text.Trim().Equals(string.Empty)) && (!this.txtMatKhau.Text.Trim().Equals(this.txtNhapLaiMatKhau.Text.Trim())))
            {
                this.txtNhapLaiMatKhau.Focus();
                XtraMessageBox.Show("Mật khẩu nhập lại không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMatKhau.Text = string.Empty;
                txtNhapLaiMatKhau.Focus();
                return false;
            }
            else
                return true;
        }
    }
}