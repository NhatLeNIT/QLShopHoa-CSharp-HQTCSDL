using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using QLShopHoa.QLDonViTinh;
using QLShopHoa.QLSanPham;
using ValueObject;

namespace QLShopHoa.QLNhapHang
{
    public partial class frmQLNhapHangSPThem : DevExpress.XtraEditors.XtraForm
    {
        ValueObject.NhapHang obj = new ValueObject.NhapHang();
        ChiTietNhapHang objCT = new ChiTietNhapHang();
        NhapHangBUS busNH = new NhapHangBUS();
        ChiTietNhapHangBUS busCTNH = new ChiTietNhapHangBUS();
        DonViTinhBUS busDVT = new DonViTinhBUS();
        SanPhamBUS busSP = new SanPhamBUS();
        QuerySQLBUS query = new QuerySQLBUS();
        public string IDNhapHang { get; set; }
        public frmQLNhapHangSPThem()
        {
            InitializeComponent();
        }

        private void frmSanPhamNhapHangThem_Load(object sender, EventArgs e)
        {
            HienThiSanPham();
            HienThiDonViTinh();
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
        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPhamThem frmSP = new frmSanPhamThem();
            frmSP.ShowDialog();
            HienThiSanPham();
        }

        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            frmDonViTinhThem frmDVT = new frmDonViTinhThem();
            frmDVT.ShowDialog();
            HienThiDonViTinh();
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
        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            if(ValidateDataSanPham())
            {
                DataTable dt = busCTNH.GetDataByIDSanPham(IDNhapHang, cbbSanPham.EditValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    XtraMessageBox.Show("Sản phẩm này đã tồn tại trong phiếu hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    objCT.IDSanPham = cbbSanPham.EditValue.ToString();
                    objCT.IDNhapHang = IDNhapHang;
                    objCT.SoLuong = Convert.ToInt32(txtSoLuong.Value);
                    objCT.DonGia = Convert.ToDouble(txtDonGia.Value);
                    objCT.IDDonViTinh = Convert.ToInt32(cbbDonViTinh.EditValue.ToString());
                    busCTNH.Insert(objCT);
                    SanPham objSP = new SanPham();
                    objSP.IDSanPham = objCT.IDSanPham;
                    objSP.SoLuong = objCT.SoLuong;
                    busSP.UpdateQuantity(objSP);
                    //update số lượng sản phẩm và tổng tiền trong phiếu nhập hàng
                    string sql = "SELECT SoLuongSanPham, TongTien FROM NhapHang WHERE IDNhapHang='" + IDNhapHang + "'";
                    dt = query.GetDataBySQL(sql);
                    int quantity = Convert.ToInt32(dt.Rows[0]["SoLuongSanPham"]) + objCT.SoLuong;
                    double totalPrice = Convert.ToDouble(dt.Rows[0]["TongTien"]) + (objCT.SoLuong * objCT.DonGia);
                    sql = "UPDATE NhapHang SET SoLuongSanPham = " + quantity + ", TongTien = " + totalPrice + " WHERE IDNhapHang = '" + IDNhapHang + "'";
                    query.ExecuteBySQL(sql);
                    XtraMessageBox.Show("Thêm sản phẩm vào phiếu hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }
    }
}