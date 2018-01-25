using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLKhachHang
{
    public partial class frmKhachHangThem : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangThem()
        {
            InitializeComponent();
        }
        KhachHang obj = new KhachHang();
        KhachHangBUS bus = new KhachHangBUS();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = new DataTable();
                dt = bus.GetDataByID(txtIDKhachHang.Text);
                if(dt.Rows.Count > 0)
                {
                    this.txtIDKhachHang.Focus();
                    XtraMessageBox.Show("Mã khách hàng này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                {
                    obj.IDKhachHang = txtIDKhachHang.Text;
                    obj.HoTen = txtHoTen.Text;
                    obj.DienThoai = txtDienThoai.Text;
                    obj.DiaChi = txtDiaChi.Text;obj.NgaySinh = txtNgaySinh.Text;
                    obj.GhiChu = txtGhiChu.Text;
                    obj.GioiTinh = txtNam.Checked ? "Nam" : "Nữ";
                    bus.Insert(obj);
                    XtraMessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (this.txtIDKhachHang.Text.Trim().Equals(string.Empty))
            {
                this.txtIDKhachHang.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtHoTen.Text.Trim().Equals(string.Empty))
            {
                this.txtHoTen.Focus();
                XtraMessageBox.Show("Bạn chưa nhập họ tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtDienThoai.Text.Trim().Equals(string.Empty))
            {
                this.txtDienThoai.Focus();
                XtraMessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }

        private void frmKhachHangThem_Load(object sender, EventArgs e)
        {
            txtIDKhachHang.Focus();
            SinhIDTuDong();
        }
        private void SinhIDTuDong()
        {
            string IDTuDong = "";
            DataTable dt = new DataTable();
            dt = bus.GetData();
            if (dt.Rows.Count <= 0)
            {
                IDTuDong = "KH000001";
            }
            else
            {
                int number;
                IDTuDong = "KH";
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
            txtIDKhachHang.Text = IDTuDong;
        }

        
    }
}