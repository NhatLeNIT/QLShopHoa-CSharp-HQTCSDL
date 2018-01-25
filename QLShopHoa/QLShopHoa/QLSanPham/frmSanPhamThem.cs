using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLDonViTinh;
using QLShopHoa.QLLoaiHang;
using QLShopHoa.QLNhaCungCap;
using ValueObject;

namespace QLShopHoa.QLSanPham
{
    public partial class frmSanPhamThem : DevExpress.XtraEditors.XtraForm
    {
        SanPham obj = new SanPham();
        SanPhamBUS busSP = new SanPhamBUS();
        NhanVienBUS busNV = new NhanVienBUS();
        LoaiHangBUS busLH = new LoaiHangBUS();
        NhaCungCapBUS busNCC = new NhaCungCapBUS();
        DonViTinhBUS busDVT = new DonViTinhBUS();
        private string DuongDanHinh = "";

        public frmSanPhamThem()
        {
            InitializeComponent();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ptbHinh.ImageLocation = openFileDialog.FileName;
                txtDuongDanHinh.Text = DuongDanHinh = openFileDialog.FileName;
                ptbHinh.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void SinhIDTuDong()
        {
            string IDTuDong = "";
            DataTable dt = busSP.GetData();
            if (dt.Rows.Count <= 0)
            {
                IDTuDong = "SP000001";
            }
            else
            {
                int number;
                IDTuDong = "SP";
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
            txtIDSanPham.Text = IDTuDong;
        }

        private void HienThiLoaiHang()
        {
            cbbLoaiHang.Properties.DataSource = busLH.GetDataActive();
            cbbLoaiHang.Properties.ValueMember = "IDLoaiHang";
            cbbLoaiHang.Properties.DisplayMember = "TenLoaiHang";
        }
        private void HienThiNhaCungCap()
        {
            cbbNhaCungCap.Properties.DataSource = busNCC.GetData();
            cbbNhaCungCap.Properties.ValueMember = "IDNhaCungCap";
            cbbNhaCungCap.Properties.DisplayMember = "TenNhaCungCap";
        }
        private void HienThiDonViTinh()
        {
            cbbDonViTinh.Properties.DataSource = busDVT.GetData();
            cbbDonViTinh.Properties.ValueMember = "IDDonViTinh";
            cbbDonViTinh.Properties.DisplayMember = "TenDonViTinh";
        }
        private void frmSanPhamThem_Load(object sender, EventArgs e)
        {
            HienThiLoaiHang();
            HienThiNhaCungCap();
            HienThiDonViTinh();
            SinhIDTuDong();
        }

        private void btnThemLoaiHang_Click(object sender, EventArgs e)
        {
            frmLoaiHangThem frmLH = new frmLoaiHangThem();
            frmLH.ShowDialog();
            HienThiLoaiHang();
        }

        private void btnThemNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCapThem frmNCC = new frmNhaCungCapThem();
            frmNCC.ShowDialog();
            HienThiNhaCungCap();
        }
        private bool ValidateData()
        {
            if (this.cbbLoaiHang.Text.Trim().Equals(string.Empty))
            {
                this.cbbLoaiHang.Focus();
                XtraMessageBox.Show("Bạn chưa chọn loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.cbbNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.cbbNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtIDSanPham.Text.Trim().Equals(string.Empty))
            {
                this.txtIDSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtTenSanPham.Text.Trim().Equals(string.Empty))
            {
                this.txtTenSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.cbbDonViTinh.Text.Trim().Equals(string.Empty))
            {
                this.cbbDonViTinh.Focus();
                XtraMessageBox.Show("Bạn chưa chọn đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtGiaVon.Text.Trim().Equals(string.Empty))
            {
                this.txtGiaVon.Focus();
                XtraMessageBox.Show("Bạn chưa nhập giá vốn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {

                DataTable dt = busSP.GetDataByID(txtIDSanPham.Text);
                if (dt.Rows.Count > 0)
                {
                    this.txtIDSanPham.Focus();
                    XtraMessageBox.Show("Mã sản phẩm này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        obj.IDSanPham = txtIDSanPham.Text;
                        obj.TenSanPham = txtTenSanPham.Text;
                        obj.GiaVon = Convert.ToDouble(txtGiaVon.Value);
                        obj.GiaBan = Convert.ToDouble(txtGiaBan.Value);
                        obj.SoLuong = Convert.ToInt32(txtSoLuong.Value);
                        if (!DuongDanHinh.Equals(string.Empty))
                            obj.Hinh = ConvertImageToBytes();
                        else
                        {
                            byte[] picByte = new byte[0];
                            obj.Hinh = picByte;
                        }
                        obj.MoTa = txtMoTa.Text;
                        obj.TrangThai = cbTrangThai.Checked ? 1 : 0;
                        obj.IDLoaiHang = Convert.ToInt32(cbbLoaiHang.EditValue.ToString());
                        obj.IDNhaCungCap = cbbNhaCungCap.EditValue.ToString();
                        obj.IDDonViTinh = Convert.ToInt32(cbbDonViTinh.EditValue.ToString());
                        obj.IDNhanVien = frmMain.IDNhanVien;
                        busSP.Insert(obj);
                        XtraMessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (System.Exception ex)
                    {
                        XtraMessageBox.Show("Lỗi " + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }
        private byte[] ConvertImageToBytes()
        {
            FileStream fs = new FileStream(DuongDanHinh, FileMode.Open, FileAccess.Read);
            byte[] picByte = new byte[fs.Length];
            fs.Read(picByte, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return picByte;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            frmDonViTinhThem frmDVT = new frmDonViTinhThem();
            frmDVT.ShowDialog();
            HienThiDonViTinh();
        }
    }
}