using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLDonViTinh;
using QLShopHoa.QLNhaCungCap;
using QLShopHoa.QLSanPham;
using ValueObject;

namespace QLShopHoa.QLNhapHang
{
    public partial class frmNhapHang : DevExpress.XtraEditors.XtraForm
    {
        ValueObject.NhapHang obj = new ValueObject.NhapHang();
        ChiTietNhapHang objCT = new ChiTietNhapHang();
        NhapHangBUS busNH = new NhapHangBUS();
        ChiTietNhapHangBUS busCTNH = new ChiTietNhapHangBUS();
        NhaCungCapBUS busNCC = new NhaCungCapBUS();
        DonViTinhBUS busDVT = new DonViTinhBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        private List<SanPham> listSanPham = new List<SanPham>();
        private bool checkInsert = true;
        public frmNhapHang()
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
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            txtNgayNhap.Text = DateTime.Now.ToString("dd-MMM-yy");
            HienThiNhaCungCap();
            HienThiSanPham();
            HienThiDonViTinh();
            SinhIDTuDong();
            KhoaDieuKhien();

        }
        private void SinhIDTuDong()
        {
            string IDTuDong = "";
            DataTable dt = new DataTable();
            dt = busNH.GetData();
            if (dt.Rows.Count <= 0)
            {
                IDTuDong = "PN000001";
            }
            else
            {
                int number;
                IDTuDong = "PN";
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
            txtIDPhieuNhap.Text = IDTuDong;
        }
        private void HienThiNhaCungCap()
        {
            cbbNhaCungCap.Properties.DataSource = busNCC.GetData();
            cbbNhaCungCap.Properties.ValueMember = "IDNhaCungCap";
            cbbNhaCungCap.Properties.DisplayMember = "TenNhaCungCap";
        }
        private void HienThiSanPham()
        {
            cbbSanPham.Properties.DataSource = busSP.GetData();
            cbbSanPham.Properties.ValueMember = "IDSanPham";
            cbbSanPham.Properties.DisplayMember = "TenSanPham";
        }
        private void HienThiDonViTinh()
        {
            cbbDonViTinh.Properties.DataSource = busDVT.GetData();
            cbbDonViTinh.Properties.ValueMember = "IDDonViTinh";
            cbbDonViTinh.Properties.DisplayMember = "TenDonViTinh";
        }

        private void btnThemNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCapThem frmNCC = new frmNhaCungCapThem();
            frmNCC.ShowDialog();
            HienThiNhaCungCap();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPhamThem frmSP = new frmSanPhamThem();
            frmSP.ShowDialog();
            HienThiSanPham();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateDataSanPham())
            {
                SanPham data = new SanPham();
                data.IDSanPham = cbbSanPham.EditValue.ToString();
                data.TenSanPham = cbbSanPham.Text;
                data.GiaVon = Convert.ToDouble(txtDonGia.Value);
                data.SoLuong = Convert.ToInt32(txtSoLuong.Value);
                data.IDDonViTinh = Convert.ToInt32(cbbDonViTinh.EditValue.ToString());
                data.TenDonViTinh = cbbDonViTinh.Text.ToString();
                if (checkInsert)
                {
                    AddProductToList(data);
                    HienThiDanhSachSanPham();
                    XoaText();
                }
                else
                {
                    UpdateList(data);
                    HienThiDanhSachSanPham();
                    XoaText();
                    btnThem.Text = "Thêm";
                    checkInsert = true;
                }
            }
        }
        private void XoaText()
        {
            //cbbSanPham.Text = string.Empty;
            //cbbDonViTinh.Text = string.Empty;
            HienThiSanPham();
            HienThiDonViTinh();
            txtSoLuong.Value = 0;
            txtDonGia.Value = 0;
        }
        private bool ValidateDataSanPham()
        {
            if (this.cbbSanPham.Text.Trim().Equals(string.Empty))
            {
                this.cbbSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa chọn sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (Convert.ToInt32(txtDonGia.Value) == 0)
            {
                this.txtDonGia.Focus();
                XtraMessageBox.Show("Bạn chưa nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (Convert.ToInt32(txtSoLuong.Value) == 0)
            {
                this.txtSoLuong.Focus();
                XtraMessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.cbbDonViTinh.Text.Trim().Equals(string.Empty))
            {
                this.cbbDonViTinh.Focus();
                XtraMessageBox.Show("Bạn chưa chọn đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void HienThiDanhSachSanPham()
        {
            DataTable dt = ConvertToDataTable(listSanPham);
            msdsDanhSachHang.DataSource = dt;
        }
        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        private SanPham FindProductOfList(string IDSanPham)
        {
            foreach (SanPham item in listSanPham)
            {
                if (item.IDSanPham.Equals(IDSanPham))
                    return item;
            }
            return null;
        }
        private void AddProductToList(SanPham obj)
        {
            SanPham res = FindProductOfList(obj.IDSanPham);
            if (res != null)
            {
                res.SoLuong += obj.SoLuong;
                res.GiaVon += obj.GiaVon;
            }
            else listSanPham.Add(obj);
        }
        private void UpdateList(SanPham obj)
        {
            SanPham res = FindProductOfList(obj.IDSanPham);
            if (res != null)
            {
                res.SoLuong = obj.SoLuong;
                res.GiaVon = obj.GiaVon;
                res.IDDonViTinh = obj.IDDonViTinh;
                res.TenDonViTinh = obj.TenDonViTinh;
            }
        }
        private void RemoveProduct(string IDSanPham)
        {
            SanPham res = FindProductOfList(IDSanPham);
            if (res != null)
                this.listSanPham.Remove(res);
        }
        private double TotalPrice()
        {
            double sum = 0;
            foreach (SanPham item in listSanPham)
                sum += (item.SoLuong * item.GiaVon);
            return sum;
        }
        private int TotalQuantity()
        {
            int sum = 0;
            foreach (SanPham item in listSanPham)
                sum += item.SoLuong;
            return sum;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string IDSanPham = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            SanPham res = FindProductOfList(IDSanPham);
            cbbSanPham.EditValue = cbbSanPham.Properties.GetKeyValueByDisplayText(res.TenSanPham);
            txtDonGia.Value = Convert.ToDecimal(res.GiaVon);
            txtSoLuong.Value = Convert.ToDecimal(res.SoLuong);
            cbbDonViTinh.EditValue = cbbDonViTinh.Properties.GetKeyValueByDisplayText(res.TenDonViTinh);

            btnThem.Text = "Cập nhật";
            checkInsert = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa sản phẩm này ra khỏi danh sách?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    RemoveProduct(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString());
                    HienThiDanhSachSanPham();
                    KhoaDieuKhien();
                }
                catch
                {
                }
            }
        }

        private void msdsDanhSachHang_Click(object sender, EventArgs e)
        {
            if (this.listSanPham.Count > 0)
                MoKhoaDieuKhien();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            XoaText();
            cbbNhaCungCap.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            SinhIDTuDong();
            txtNgayNhap.Text = DateTime.Now.ToString("dd-MMM-yy");
            this.listSanPham.Clear();
            HienThiDanhSachSanPham();
        }
        private bool ValidateData()
        {
            if (this.cbbNhaCungCap.Text.Trim().Equals(string.Empty))
            {
                this.cbbNhaCungCap.Focus();
                XtraMessageBox.Show("Bạn chưa chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtIDPhieuNhap.Text.Trim().Equals(string.Empty))
            {
                this.txtIDPhieuNhap.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mã phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtNgayNhap.Text.Trim().Equals(string.Empty))
            {
                this.txtNgayNhap.Focus();
                XtraMessageBox.Show("Bạn chưa chọn ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.listSanPham.Count == 0)
            {
                this.cbbSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa chọn sản phẩm nào cho phiếu nhập này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = busNH.GetDataByID(txtIDPhieuNhap.Text);
                if (dt.Rows.Count > 0)
                {
                    this.txtIDPhieuNhap.Focus();
                    XtraMessageBox.Show("Mã đơn hàng này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Phiếu nhập
                    obj.IDNhapHang = txtIDPhieuNhap.Text;
                    obj.IDNhanVien = frmMain.IDNhanVien;
                    obj.GhiChu = txtGhiChu.Text;
                    obj.IDNhaCungCap = cbbNhaCungCap.EditValue.ToString();
                    obj.NgayNhap = txtNgayNhap.Text;
                    obj.SoLuongSanPham = TotalQuantity();
                    obj.TongTien = TotalPrice();
                    busNH.Insert(obj);
                    //Chi tiết phiếu nhập
                    foreach (SanPham item in listSanPham)
                    {
                        objCT.IDNhapHang = txtIDPhieuNhap.Text;
                        objCT.IDSanPham = item.IDSanPham;
                        objCT.SoLuong = item.SoLuong;
                        objCT.DonGia = item.GiaVon;
                        objCT.IDDonViTinh = item.IDDonViTinh;
                        busCTNH.Insert(objCT);
                        busSP.UpdateQuantity(item);
                    }
                    XtraMessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnTaoMoi_Click(sender, e);
                }
            }
        }

        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            frmDonViTinhThem frmDVT = new frmDonViTinhThem();
            frmDVT.ShowDialog();
            HienThiDonViTinh();
        }
    }
}