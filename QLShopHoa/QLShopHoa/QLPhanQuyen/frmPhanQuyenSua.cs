using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogicLayer;
using DevExpress.XtraEditors;
using ValueObject;

namespace QLShopHoa.QLPhanQuyen
{
    public partial class frmPhanQuyenSua : DevExpress.XtraEditors.XtraForm
    {
        public int IDNhom;
        NhomQuyenBUS busNQ = new NhomQuyenBUS();
        PhanQuyen obj = new PhanQuyen();
        PhanQuyenBUS busPQ = new PhanQuyenBUS();
        public frmPhanQuyenSua()
        {
            InitializeComponent();
        }

        private void frmPhanQuyenSua_Load(object sender, EventArgs e)
        {
            DataTable dt = busNQ.GetDataByID(IDNhom);
            txtName.Text = "Cập nhật nhóm: " + dt.Rows[0]["TenNhom"].ToString();
            //Khách hàng
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "khachhang");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbKHXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbKHThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbKHSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbKHXoa.Checked = true;
            //Nhà cung cấp
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "nhacungcap");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbNCCXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbNCCThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbNCCSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbNCCXoa.Checked = true;
            //Đơn vị tính
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "donvitinh");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbDVTXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbDVTThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbDVTSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbDVTXoa.Checked = true;
            // Loại hàng
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "loaihang");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbLHXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbLHThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbLHSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbLHXoa.Checked = true;
            // Sản phẩm
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "sanpham");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbSPXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbSPThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbSPSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbSPXoa.Checked = true;
            
            // Quản lý nhập hàng
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "qlnhaphang");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbQLNHXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbQLNHThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbQLNHSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbQLNHXoa.Checked = true;
            // Quản lý bán hàng
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "qlbanhang");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbQLBHXem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Them"].ToString()) == true)
                cbQLBHThem.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Sua"].ToString()) == true)
                cbQLBHSua.Checked = true;
            if (Convert.ToBoolean(dt.Rows[0]["Xoa"].ToString()) == true)
                cbQLBHXoa.Checked = true;
            // Quản lý báo cáo
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "baocao");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbBCXem.Checked = true;
            // Quản lý nhập hàng
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "nhaphang");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbNHXem.Checked = true;
            // Quản lý nhập hàng
            dt = busPQ.GetDataByIDNhomAndIDChucNang(IDNhom, "banhang");
            if (Convert.ToBoolean(dt.Rows[0]["Xem"].ToString()) == true)
                cbBHXem.Checked = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            obj.IDNhom = IDNhom;
            //Group Khách Hàng
            obj.IDChucNang = "khachhang";
            obj.Xem = cbKHXem.Checked ? 1 : 0;
            obj.Them = cbKHThem.Checked ? 1 : 0;
            obj.Sua = cbKHSua.Checked ? 1 : 0;
            obj.Xoa = cbKHXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            //Group Nhà Cung Cấp
            obj.IDChucNang = "nhacungcap";
            obj.Xem = cbNCCXem.Checked ? 1 : 0;
            obj.Them = cbNCCThem.Checked ? 1 : 0;
            obj.Sua = cbNCCSua.Checked ? 1 : 0;
            obj.Xoa = cbNCCXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            //Group Nhà Cung Cấp
            obj.IDChucNang = "donvitinh";
            obj.Xem = cbDVTXem.Checked ? 1 : 0;
            obj.Them = cbDVTThem.Checked ? 1 : 0;
            obj.Sua = cbDVTSua.Checked ? 1 : 0;
            obj.Xoa = cbDVTXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            //Group Loại Hàng
            obj.IDChucNang = "loaihang";
            obj.Xem = cbLHXem.Checked ? 1 : 0;
            obj.Them = cbLHThem.Checked ? 1 : 0;
            obj.Sua = cbLHSua.Checked ? 1 : 0;
            obj.Xoa = cbLHXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            //Group Sản Phẩm
            obj.IDChucNang = "sanpham";
            obj.Xem = cbSPXem.Checked ? 1 : 0;
            obj.Them = cbSPThem.Checked ? 1 : 0;
            obj.Sua = cbSPSua.Checked ? 1 : 0;
            obj.Xoa = cbSPXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            
            //Group Quản Lý Nhập Hàng
            obj.IDChucNang = "qlnhaphang";
            obj.Xem = cbQLNHXem.Checked ? 1 : 0;
            obj.Them = cbQLNHThem.Checked ? 1 : 0;
            obj.Sua = cbQLNHSua.Checked ? 1 : 0;
            obj.Xoa = cbQLNHXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            //Group Quản Lý Bán Hàng
            obj.IDChucNang = "qlbanhang";
            obj.Xem = cbQLBHXem.Checked ? 1 : 0;
            obj.Them = cbQLBHThem.Checked ? 1 : 0;
            obj.Sua = cbQLBHSua.Checked ? 1 : 0;
            obj.Xoa = cbQLBHXoa.Checked ? 1 : 0;
            busPQ.Update(obj);
            //Group Quản Lý Báo Cáo
            obj.IDChucNang = "baocao";
            obj.Xem = cbBCXem.Checked ? 1 : 0;
            obj.Them = 0;
            obj.Sua = 0;
            obj.Xoa = 0;
            busPQ.Update(obj);
            //Group Nhập Hàng
            obj.IDChucNang = "nhaphang";
            obj.Xem = cbNHXem.Checked ? 1 : 0;
            obj.Them = 0;
            obj.Sua = 0;
            obj.Xoa = 0;
            busPQ.Update(obj);
            //Group Bán Hàng
            obj.IDChucNang = "banhang";
            obj.Xem = cbBHXem.Checked ? 1 : 0;
            obj.Them = 0;
            obj.Sua = 0;
            obj.Xoa = 0;
            busPQ.Update(obj);
            XtraMessageBox.Show("Cập nhật phân quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            if (XtraMessageBox.Show("Bạn có muốn hủy?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
        }

    
    }
}