using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLKhachHang;
using ValueObject;

namespace QLShopHoa.QLBanHang
{
    public partial class frmBanHang : DevExpress.XtraEditors.XtraForm
    {
        HoaDon obj = new HoaDon();
        HoaDonBUS busHD = new HoaDonBUS();
        ChiTietHoaDon objCTHD = new ChiTietHoaDon();
        ChiTietHoaDonBUS busCTHD = new ChiTietHoaDonBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        KhachHangBUS busKH = new KhachHangBUS();
        NhanVienBUS busNV = new NhanVienBUS();
        DonViTinhBUS busDVT = new DonViTinhBUS();
        private List<SanPham> listSanPham = new List<SanPham>();
        private bool checkInsert = true;
        public frmBanHang()
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
        private void SinhIDTuDong()
        {
            string IDTuDong = "";
            DataTable dt = busHD.GetData();
            if (dt.Rows.Count <= 0)
            {
                IDTuDong = "HD000001";
            }
            else
            {
                int number;
                IDTuDong = "HD";
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
            txtIDDonHang.Text = IDTuDong;
        }
        private void HienThiSanPham()
        {
            cbbSanPham.Properties.DataSource = busSP.GetDataByStatus_Quantity();
            cbbSanPham.Properties.ValueMember = "IDSanPham";
            cbbSanPham.Properties.DisplayMember = "TenSanPham";
        }
        private void HienThiKhachHang()
        {
            cbbKhachHang.Properties.DataSource = busKH.GetData();
            cbbKhachHang.Properties.ValueMember = "IDKhachHang";
            cbbKhachHang.Properties.DisplayMember = "HoTen";
        }
        private void HienThiTenNhanVien()
        {
            DataTable dt = busNV.GetDataByID(frmMain.IDNhanVien);
            txtTenNhanVien.Text = dt.Rows[0]["HoTen"].ToString();
        }
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHangThem frmKH = new frmKhachHangThem();
            frmKH.ShowDialog();
            HienThiKhachHang();
        }
        private void XoaText()
        {
            txtSoLuong.Value = 1;
        }
        private bool ValidateDataSanPham()
        {
            DataTable dt = busSP.GetDataByID(cbbSanPham.EditValue.ToString());
            if (this.cbbSanPham.Text.Trim().Equals(string.Empty))
            {
                this.cbbSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa chọn sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (Convert.ToInt32(txtSoLuong.Value) == 0)
            {
                this.txtSoLuong.Focus();
                XtraMessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (CheckQuantity(Convert.ToInt32(dt.Rows[0]["SoLuong"]), cbbSanPham.EditValue.ToString()))
            {
                XtraMessageBox.Show("Số lượng bạn vừa nhập lớn hơn số lượng tồn kho của sản phẩm này, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Value = Convert.ToDecimal(dt.Rows[0]["SoLuong"]);
                txtSoLuong.Focus();
                return false;
            }
            return true;
        }
        private bool CheckQuantity(int quantityOfProduct, string IDProduct)
        {
            int quantity = Convert.ToInt32(txtSoLuong.Value);
            if (listSanPham.Count > 0)
            {
                SanPham res = FindProductOfList(IDProduct);
                if(res != null)
                    quantity += res.SoLuong;
            }
            if (quantity > quantityOfProduct)
                return true;
            return false;
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
            }
            else listSanPham.Add(obj);
        }
        private void UpdateList(SanPham obj)
        {
            SanPham res = FindProductOfList(obj.IDSanPham);
            if (res != null)
            {
                res.SoLuong = obj.SoLuong;
            }
        }
        private void RemoveProduct(string IDSanPham)
        {
            SanPham res = FindProductOfList(IDSanPham);
            if (res != null)
                this.listSanPham.Remove(res);
        }
        private void ViewTotalPrice()
        {
            double sum = 0;
            foreach (SanPham item in listSanPham)
                sum += (item.SoLuong * item.GiaBan);
            txtTongTien.Value = Convert.ToDecimal(sum);
        }
        private bool ValidateData()
        {
            if (this.cbbKhachHang.Text.Trim().Equals(string.Empty))
            {
                this.cbbKhachHang.Focus();
                XtraMessageBox.Show("Bạn chưa chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtIDDonHang.Text.Trim().Equals(string.Empty))
            {
                this.txtIDDonHang.Focus();
                XtraMessageBox.Show("Bạn chưa nhập mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.txtNgayLap.Text.Trim().Equals(string.Empty))
            {
                this.txtNgayLap.Focus();
                XtraMessageBox.Show("Bạn chưa chọn ngày lập đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (this.listSanPham.Count == 0)
            {
                this.cbbSanPham.Focus();
                XtraMessageBox.Show("Bạn chưa chọn sản phẩm nào cho đơn hàng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            txtNgayLap.Text = DateTime.Now.ToString("dd-MMM-yy");
            txtNgayGiao.Text = DateTime.Now.ToString("dd-MMM-yy");
            HienThiKhachHang();
            HienThiSanPham();
            SinhIDTuDong();
            KhoaDieuKhien();
            HienThiTenNhanVien();
            ViewTotalPrice();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            XoaText();
            cbbKhachHang.Text = string.Empty;
            txtTenNguoiNhan.Text = string.Empty;
            txtSDTNguoiNhan.Value = 0;
            txtDiaChiNguoiNhan.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            SinhIDTuDong();
            txtNgayLap.Text = DateTime.Now.ToString("dd-MMM-yy");
            txtNgayGiao.Text = DateTime.Now.ToString("dd-MMM-yy");
            cbThanhToan.Checked = false;
            cbGiaoHang.Checked = false;
            txtTongTien.Value = 0;
            this.listSanPham.Clear();
            HienThiDanhSachSanPham();
            HienThiSanPham();
            HienThiTenNhanVien();
            ViewTotalPrice();
        }
        private double TotalPrice()
        {
            double sum = 0;
            foreach (SanPham item in listSanPham)
                sum += (item.SoLuong * item.GiaBan);
            return sum;
        }
        private int TotalQuantity()
        {
            int sum = 0;
            foreach (SanPham item in listSanPham)
                sum += item.SoLuong;
            return sum;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DataTable dt = busHD.GetDataByID(txtIDDonHang.Text);
                if (dt.Rows.Count > 0)
                {
                    this.txtIDDonHang.Focus();
                    XtraMessageBox.Show("Mã đơn hàng này đã tồn tại, vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Đơn hàng
                    obj.IDHoaDon = txtIDDonHang.Text;
                    obj.IDNhanVien = frmMain.IDNhanVien;
                    obj.GhiChu = txtGhiChu.Text;
                    obj.IDKhachHang = cbbKhachHang.EditValue.ToString();
                    obj.NgayLap = txtNgayLap.Text;
                    obj.TenNguoiNhan = txtTenNguoiNhan.Text;
                    obj.DienThoaiNguoiNhan = txtSDTNguoiNhan.Value.ToString();
                    obj.DiaChiNguoiNhan = txtDiaChiNguoiNhan.Text;
                    obj.NgayGiao = txtNgayGiao.Text;
                    obj.TrangThaiThanhToan = cbThanhToan.Checked ? 1 : 0;
                    obj.TrangThaiGiaoHang = cbGiaoHang.Checked ? 1 : 0;
                    obj.SoLuongSanPham = TotalQuantity();
                    obj.TongTien = TotalPrice();
                    if (busHD.Insert(obj) == -1)
                    {
                        XtraMessageBox.Show("Ngày giao không được nhỏ hơn ngày lập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Chi tiết đơn hàng
                        foreach (SanPham item in listSanPham)
                        {
                            objCTHD.IDHoaDon = txtIDDonHang.Text;
                            objCTHD.IDSanPham = item.IDSanPham;
                            objCTHD.SoLuong = item.SoLuong;
                            objCTHD.DonGia = item.GiaBan;
                            objCTHD.IDDonViTinh = item.IDDonViTinh;
                            busCTHD.Insert(objCTHD);
                            busSP.UpdateQuantitySub(item);
                        }
                        XtraMessageBox.Show("Lưu đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnTaoMoi_Click(sender, e);
                    }
                    
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string IDSanPham = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            SanPham res = FindProductOfList(IDSanPham);
            cbbSanPham.EditValue = cbbSanPham.Properties.GetKeyValueByDisplayText(res.TenSanPham);
            txtSoLuong.Value = Convert.ToDecimal(res.SoLuong);
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateDataSanPham())
            {
                SanPham data = new SanPham();
                data.IDSanPham = cbbSanPham.EditValue.ToString();
                data.TenSanPham = cbbSanPham.Text;
                DataTable dt = new DataTable();
                if (cbFixDirtyRead.Checked)
                    dt = busSP.GetDataByID_Fix_DirtyRead(data.IDSanPham);

                else
                    dt = busSP.GetDataByID(data.IDSanPham);
                data.GiaBan = Convert.ToDouble(dt.Rows[0]["GiaBan"]);
                data.SoLuong = Convert.ToInt32(txtSoLuong.Value);
                dt = busDVT.GetDataByID(Convert.ToInt32(dt.Rows[0]["IDDonViTinh"]));
                data.TenDonViTinh = dt.Rows[0]["TenDonViTinh"].ToString();
                data.IDDonViTinh = Convert.ToInt32(dt.Rows[0]["IDDonViTinh"]);
                if (checkInsert)
                {
                    AddProductToList(data);
                    HienThiDanhSachSanPham();
                    XoaText();
                    ViewTotalPrice();
                }
                else
                {
                    UpdateList(data);
                    HienThiDanhSachSanPham();
                    XoaText();
                    btnThem.Text = "Thêm";
                    ViewTotalPrice();
                    checkInsert = true;
                }
            }
        }

        private void msdsDanhSachHang_Click(object sender, EventArgs e)
        {
            MoKhoaDieuKhien();
        }
    }
}