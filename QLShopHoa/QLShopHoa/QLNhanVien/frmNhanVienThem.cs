using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhanVien
{
    public partial class frmNhanVienThem : DevExpress.XtraEditors.XtraForm
    {
        public frmNhanVienThem()
        {
            InitializeComponent();
        }

        NhanVien obj = new NhanVien();
        NhanVienBUS busNV = new NhanVienBUS();
        NhomQuyenBUS busNQ = new NhomQuyenBUS();
        md5Convert md5 = new md5Convert();
        private void frmNhanVienThem_Load(object sender, EventArgs e)
        {
            cbbNhom.Properties.DataSource = busNQ.GetData();
            cbbNhom.Properties.ValueMember = "IDNhom";
            cbbNhom.Properties.DisplayMember = "TenNhom";
            txtIDNhanVien.Focus();
            SinhIDTuDong();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = new DataTable();
                dt = busNV.GetDataByID(txtIDNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    this.txtIDNhanVien.Focus();
                    XtraMessageBox.Show("Mã nhân viên này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dt = busNV.GetDataByUserName(txtTaiKhoan.Text);
                if (dt.Rows.Count > 0)
                {
                    this.txtIDNhanVien.Focus();
                    XtraMessageBox.Show("Tên tài khoản này đã tồn tại, vui lòng nhập tên tài khoản khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    obj.IDNhanVien = txtIDNhanVien.Text;
                    obj.HoTen = txtHoTen.Text;
                    obj.DienThoai = txtDienThoai.Text;
                    obj.DiaChi = txtDiaChi.Text;
                    obj.NgaySinh = txtNgaySinh.Text;
                    obj.GhiChu = txtGhiChu.Text;
                    obj.GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                    obj.TaiKhoan = txtTaiKhoan.Text;
                    obj.MatKhau = md5.md5(txtMatKhau.Text);
                    obj.TrangThai = cbTrangThai.Checked ? 1 : 0;
                    obj.IDNhom = Convert.ToInt32(cbbNhom.EditValue.ToString());
                    busNV.Insert(obj);
                    XtraMessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }
        private bool ValidateData()
        {

            if (this.txtIDNhanVien.Text.Trim().Equals(string.Empty))
            {
                this.txtIDNhanVien.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtTaiKhoan.Text.Trim().Equals(string.Empty))
            {
                this.txtTaiKhoan.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtMatKhau.Text.Trim().Equals(string.Empty))
            {
                this.txtMatKhau.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtHoTen.Text.Trim().Equals(string.Empty))
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
                this.cbbNhom.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhóm quyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!this.txtMatKhau.Text.Trim().Equals(this.txtNhapLaiMatKhau.Text.Trim()))
            {
                this.txtNhapLaiMatKhau.Focus();
                XtraMessageBox.Show("Mật khẩu nhập lại không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (Convert.ToInt32(cbbNhom.EditValue.ToString()) == 1 && !frmMain.IDNhanVien.Equals("NV000001"))
            {
                this.cbbNhom.Focus();
                XtraMessageBox.Show("Bạn không được phép chọn nhóm quyền này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!txtMatKhau.Text.Trim().Equals(txtNhapLaiMatKhau.Text))
            {
                XtraMessageBox.Show("Mật Khẩu nhập lại không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNhapLaiMatKhau.Text = string.Empty;
                txtNhapLaiMatKhau.Focus();
                return false;
            }
            else
                return true;
        }
        private void SinhIDTuDong()
        {
            string IDTuDong = "";
            DataTable dt = new DataTable();
            dt = busNV.GetData();
            if (dt.Rows.Count <= 0)
            {
                IDTuDong = "NV000001";
            }
            else
            {
                int number;
                IDTuDong = "NV";
                number = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 6));
                number++;
                if (number < 10)
                    IDTuDong += "00000";
                else if (number < 100)
                    IDTuDong += "0000";
                else if (number < 1000)
                    IDTuDong += "000";
                else if (number < 10000)
                    IDTuDong += "00";
                else if (number < 100000)
                    IDTuDong += "0";
                IDTuDong += number.ToString();
            }
            txtIDNhanVien.Text = IDTuDong;
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            //int isNumber = 0;
            //e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
            //if (!int.TryParse(e.KeyChar.ToString(), out isNumber))
            //{
            //    XtraMessageBox.Show("Không được nhập chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void txtNhapLaiMatKhau_Leave(object sender, EventArgs e)
        {

        }
    }
}