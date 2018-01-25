using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLNhaCungCap
{
    public partial class frmNhaCungCapThem : DevExpress.XtraEditors.XtraForm
    {
        public frmNhaCungCapThem()
        {
            InitializeComponent();
        }

        NhaCungCap obj = new NhaCungCap();
        NhaCungCapBUS bus = new NhaCungCapBUS();

        private void frmNhaCungCapThem_Load(object sender, EventArgs e)
        {
            txtIDNhaCungCap.Focus();
            SinhIDTuDong();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = new DataTable();
                dt = bus.GetDataByID(txtIDNhaCungCap.Text);
                if (dt.Rows.Count > 0)
                {
                    this.txtIDNhaCungCap.Focus();
                    XtraMessageBox.Show("Mã nhà cung cấp này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    obj.IDNhaCungCap = txtIDNhaCungCap.Text;
                    obj.TenNhaCungCap = txtTenNhaCungCap.Text;
                    obj.DienThoai = txtDienThoai.Text;
                    obj.DiaChi = txtDiaChi.Text;
                    obj.GhiChu = txtGhiChu.Text;
                    bus.Insert(obj);
                    XtraMessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (this.txtIDNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.txtIDNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtTenNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.txtTenNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void SinhIDTuDong()
        {
            string IDTuDong = "";
            DataTable dt = new DataTable();
            dt = bus.GetData();
            if (dt.Rows.Count <= 0)
            {
                IDTuDong = "NCC000001";
            }
            else
            {
                int number;
                IDTuDong = "NCC";
                number = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(3, 6));
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
            txtIDNhaCungCap.Text = IDTuDong;
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
            if (!int.TryParse(e.KeyChar.ToString(), out isNumber))
            {
                XtraMessageBox.Show("Không được nhập chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}