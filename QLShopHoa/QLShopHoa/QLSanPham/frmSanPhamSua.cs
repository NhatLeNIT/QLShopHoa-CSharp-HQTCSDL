using System;
using System.Data;
using System.Drawing;
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
    public partial class frmSanPhamSua : DevExpress.XtraEditors.XtraForm
    {
        public string IDSanPham { get; set; }
        private int IDLoaiHang { get; set; }
        private string IDNhaCungCap { get; set; }
        private int IDDonViTinh { get; set; }
        private string DuongDanHinh = "";
        SanPham obj = new SanPham();
        SanPhamBUS busSP = new SanPhamBUS();
        LoaiHangBUS busLH = new LoaiHangBUS();
        NhaCungCapBUS busNCC = new NhaCungCapBUS();
        DonViTinhBUS busDVT = new DonViTinhBUS();

        public frmSanPhamSua()
        {
            InitializeComponent();
        }

        private void frmSanPhamSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busSP.GetDataByID(IDSanPham);
            IDLoaiHang = Convert.ToInt32(dt.Rows[0]["IDLoaiHang"].ToString());
            IDDonViTinh = Convert.ToInt32(dt.Rows[0]["IDDonViTinh"].ToString());
            IDNhaCungCap = dt.Rows[0]["IDNhaCungCap"].ToString();
            txtIDSanPham.Text = dt.Rows[0]["IDSanPham"].ToString();
            txtTenSanPham.Text = dt.Rows[0]["TenSanPham"].ToString();
            txtGiaVon.Value = Convert.ToDecimal(dt.Rows[0]["GiaVon"].ToString());
            txtGiaBan.Value = Convert.ToDecimal(dt.Rows[0]["GiaBan"].ToString());
            txtSoLuong.Value = Convert.ToDecimal(dt.Rows[0]["SoLuong"].ToString());
            txtMoTa.Text = dt.Rows[0]["MoTa"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["TrangThai"].ToString()) == true)
                cbTrangThai.Checked = true;
            HienThiLoaiHang(IDLoaiHang);
            HienThiNhaCungCap(IDNhaCungCap);
            HienThiDonViTinh(IDDonViTinh);
            byte[] picByte = new byte[0];
            byte[] picByteDB = (Byte[])(dt.Rows[0]["Hinh"]);
            if (picByteDB.Length != picByte.Length)
            {
                Byte[] data = new Byte[0];
                data = (Byte[])(dt.Rows[0]["Hinh"]);
                MemoryStream mem = new MemoryStream(data);
                ptbHinh.Image = Image.FromStream(mem);
            }
            ptbHinh.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = busSP.GetDataByID(IDSanPham);
                obj.IDSanPham = IDSanPham;
                obj.TenSanPham = txtTenSanPham.Text;
                obj.GiaVon = Convert.ToDouble(txtGiaVon.Value);
                obj.GiaBan = Convert.ToDouble(txtGiaBan.Value);
                obj.SoLuong = Convert.ToInt32(txtSoLuong.Value);
                obj.MoTa = txtMoTa.Text;
                obj.TrangThai = cbTrangThai.Checked ? 1 : 0;
                obj.IDLoaiHang = Convert.ToInt32(cbbLoaiHang.EditValue.ToString());
                obj.IDNhaCungCap = cbbNhaCungCap.EditValue.ToString();
                obj.IDDonViTinh = Convert.ToInt32(cbbDonViTinh.EditValue.ToString());
                if (!DuongDanHinh.Equals(string.Empty))
                    obj.Hinh = convertImageToBytes();
                else obj.Hinh = (Byte[])(dt.Rows[0]["Hinh"]);
                obj.IDNhanVien = frmMain.IDNhanVien;

                if (cbDirtyRead.Checked)
                    busSP.Update_DirtyRead(obj);
                else if(cbLostUpdate.Checked)
                    busSP.Update_LostUpdate(obj);
                else busSP.Update(obj);

                if (!cbDirtyRead.Checked)
                {
                    XtraMessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else XtraMessageBox.Show("Cập nhật sản phẩm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

        private void btnThemLoaiHang_Click(object sender, EventArgs e)
        {
            frmLoaiHangThem frmLH = new frmLoaiHangThem();
            frmLH.ShowDialog();
            HienThiLoaiHang(IDLoaiHang);
        }

        private void btnThemNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCapThem frmNCC = new frmNhaCungCapThem();
            frmNCC.ShowDialog();
            HienThiNhaCungCap(IDNhaCungCap);
        }
        private void HienThiLoaiHang(int IDLoaiHang)
        {
            cbbLoaiHang.Properties.DataSource = busLH.GetDataActive();
            cbbLoaiHang.Properties.ValueMember = "IDLoaiHang";
            cbbLoaiHang.Properties.DisplayMember = "TenLoaiHang";
            DataTable dt = busLH.GetDataByID(IDLoaiHang);
            cbbLoaiHang.EditValue = cbbLoaiHang.Properties.GetKeyValueByDisplayText(dt.Rows[0]["TenLoaiHang"].ToString());
        }
        private void HienThiNhaCungCap(string IDNhaCungCap)
        {
            cbbNhaCungCap.Properties.DataSource = busNCC.GetData();
            cbbNhaCungCap.Properties.ValueMember = "IDNhaCungCap";
            cbbNhaCungCap.Properties.DisplayMember = "TenNhaCungCap";
            DataTable dt = busNCC.GetDataByID(IDNhaCungCap);
            cbbNhaCungCap.EditValue = cbbNhaCungCap.Properties.GetKeyValueByDisplayText(dt.Rows[0]["TenNhaCungCap"].ToString());
        }
        private void HienThiDonViTinh(int IDDonViTinh)
        {
            cbbDonViTinh.Properties.DataSource = busDVT.GetData();
            cbbDonViTinh.Properties.ValueMember = "IDDonViTinh";
            cbbDonViTinh.Properties.DisplayMember = "TenDonViTinh";
            DataTable dt = busDVT.GetDataByID(IDDonViTinh);
            cbbDonViTinh.EditValue = cbbDonViTinh.Properties.GetKeyValueByDisplayText(dt.Rows[0]["TenDonViTinh"].ToString());
        }
        private bool ValidateData()
        {
            if (this.cbbLoaiHang.Text.Trim().Equals(string.Empty))
            {
                this.cbbLoaiHang.Focus();
                XtraMessageBox.Show("Bạn chưa chọn loại hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.cbbNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.cbbNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtTenSanPham.Text.Trim().Equals(string.Empty))
            {
                this.txtTenSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private byte[] convertImageToBytes()
        {
            FileStream fs = new FileStream(DuongDanHinh, FileMode.Open, FileAccess.Read);
            byte[] picByte = new byte[fs.Length];
            fs.Read(picByte, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return picByte;
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
            }
        }

        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            frmDonViTinhThem frmDVT = new frmDonViTinhThem();
            frmDVT.ShowDialog();
            HienThiDonViTinh(IDDonViTinh);
        }
    }
}