using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLKhachHang;
using ValueObject;

namespace QLShopHoa.QLBanHang
{
    public partial class frmQLBanHangSua : DevExpress.XtraEditors.XtraForm
    {
        KhachHangBUS busKH = new KhachHangBUS();
        HoaDon obj = new HoaDon();
        HoaDonBUS busHD = new HoaDonBUS();
        public string IDHoaDon { get; set; }
        public frmQLBanHangSua()
        {
            InitializeComponent();
        }
        private void HienThiKhachHang()
        {
            cbbKhachHang.Properties.DataSource = busKH.GetData();
            cbbKhachHang.Properties.ValueMember = "IDKhachHang";
            cbbKhachHang.Properties.DisplayMember = "HoTen";
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHangThem frmKH = new frmKhachHangThem();
            frmKH.ShowDialog();
            HienThiKhachHang();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                DataTable dt = busHD.GetDataByID(IDHoaDon);
                obj.IDHoaDon = IDHoaDon;
                obj.IDKhachHang = cbbKhachHang.EditValue.ToString();
                obj.GhiChu = txtGhiChu.Text;
                obj.IDNhanVien = frmMain.IDNhanVien;
                obj.TenNguoiNhan = txtTenNguoiNhan.Text;
                obj.DiaChiNguoiNhan = txtDiaChiNguoiNhan.Text;
                obj.DienThoaiNguoiNhan = txtDienThoaiNguoiNhan.Value.ToString();
                obj.NgayGiao = txtNgayGiao.Text;
                obj.TrangThaiGiaoHang = cbGiaoHang.Checked ? 1 : 0;
                obj.TrangThaiThanhToan = cbThanhToan.Checked ? 1 : 0;
                obj.NgayLap = dt.Rows[0]["NgayLap"].ToString();
                obj.SoLuongSanPham = Convert.ToInt32(dt.Rows[0]["SoLuongSanPham"].ToString());
                obj.TongTien = Convert.ToDouble(dt.Rows[0]["TongTien"].ToString());
                busHD.Update(obj);
                XtraMessageBox.Show("Cập nhật đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool ValidateData()
        {
            if (this.cbbKhachHang.Text.Trim().Equals(string.Empty))
            {
                this.cbbKhachHang.Focus();
                XtraMessageBox.Show("Bạn chưa chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void frmQLBanHangSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busHD.GetDataByID(IDHoaDon);
            txtIDHoaDon.Text = IDHoaDon;
            txtGhiChu.Text = dt.Rows[0]["GhiChu"].ToString();
            txtTenNguoiNhan.Text = dt.Rows[0]["TenNguoiNhan"].ToString();
            txtDiaChiNguoiNhan.Text = dt.Rows[0]["DiaChiNguoiNhan"].ToString();
            txtDienThoaiNguoiNhan.Text = dt.Rows[0]["DienThoaiNguoiNhan"].ToString();
            txtNgayGiao.Text = dt.Rows[0]["NgayGiao"].ToString();
            cbThanhToan.Checked = Convert.ToBoolean(dt.Rows[0]["TrangThaiThanhToan"]);
            cbGiaoHang.Checked = Convert.ToBoolean(dt.Rows[0]["TrangThaiGiaoHang"]);
            HienThiKhachHang();
            dt = busKH.GetDataByID(dt.Rows[0]["IDKhachHang"].ToString());
            cbbKhachHang.EditValue = cbbKhachHang.Properties.GetKeyValueByDisplayText(dt.Rows[0]["HoTen"].ToString());
        }
    }
}