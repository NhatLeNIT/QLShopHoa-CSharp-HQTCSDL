using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

using BusinessLogicLayer;
using QLShopHoa.Auth;
using QLShopHoa.BaoCao;
using QLShopHoa.NhapHang;
using QLShopHoa.QLBanHang;
using QLShopHoa.QLDonViTinh;
using QLShopHoa.QLKhachHang;
using QLShopHoa.QLLoaiHang;
using QLShopHoa.QLNhaCungCap;
using QLShopHoa.QLNhanVien;
using QLShopHoa.QLNhapHang;
using QLShopHoa.QLNhomQuyen;
using QLShopHoa.QLPhanQuyen;
using QLShopHoa.QLSanPham;

namespace QLShopHoa
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public bool checkDangXuat = false;
        static public string IDNhanVien { get; set; }
        static public int IDNhom { get; set; }
        QuerySQLBUS query = new QuerySQLBUS();
        public frmMain()
        {
            InitializeComponent();
        }

        Form CheckForm(Type fType)
        {
            foreach (var f in MdiChildren)
            {
                if (f.GetType() == fType)
                    return f;
            }
            return null;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            timer.Start();
            Form frm = CheckForm(typeof(Dashboard));
            if (frm != null)
                frm.Activate();
            else
            {
                Dashboard frmN = new Dashboard();
                frmN.MdiParent = this;
                frmN.Show();
            }
            //user login label
            NhanVienBUS busNV = new NhanVienBUS();
            var dt = busNV.GetDataByID(IDNhanVien);
            this.txtUserLogin.Caption = " Tài khoản đang đăng nhập: " + dt.Rows[0]["HoTen"].ToString() + " (" +
                                      dt.Rows[0]["TaiKhoan"].ToString() + ")";
            //check quyen han
            PhanQuyenBUS busPQ = new PhanQuyenBUS();
            dt = busPQ.GetDataByIDNhom(IDNhom);
            foreach (DataRow dataRow in dt.Rows)
            {
               
                if (dataRow["IDChucNang"].Equals("baocao") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnBCBanHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnBCSanPham.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnBCKhachHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnBCNhaCungCap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnBCNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    ribbonBaoCao.Visible = true;
                    //btnBCBanHang.Enabled = true;
                    //btnBCSanPham.Enabled = true;
                    //btnBCKhachHang.Enabled = true;
                    //btnBCNhaCungCap.Enabled = true;
                    //btnBCNhanVien.Enabled = true;
                }

                //============================= Group doi tac ===============================
              
                if (dataRow["IDChucNang"].Equals("khachhang") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnKhachHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonDoiTac.Visible = true;
                    //btnKhachHang.Enabled = true;

                }

                if (dataRow["IDChucNang"].Equals("nhacungcap") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnNhaCungCap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonDoiTac.Visible = true;
                    //btnNhaCungCap.Enabled = true;
                }



                //============================= Group hang hoa =============================


                if (dataRow["IDChucNang"].Equals("loaihang") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnLoaiHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonHangHoa.Visible = true;
                    //btnLoaiHang.Enabled = true;
                }


                if (dataRow["IDChucNang"].Equals("sanpham") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnSanPham.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonHangHoa.Visible = true;
                    //btnSanPham.Enabled = true;
                }


                if (dataRow["IDChucNang"].Equals("donvitinh") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnDonViTinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonHangHoa.Visible = true;
                    //btnDonViTinh.Enabled = true;
                }



                //=============================  Group Nhap xuat hang ============================= 
               
                if (dataRow["IDChucNang"].Equals("nhaphang") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnNhapHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonNhapXuatHang.Visible = true;
                    //btnNhapHang.Enabled = true;
                }

                if (dataRow["IDChucNang"].Equals("banhang") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnBanHang.Enabled = true;
                    ribbonNhapXuatHang.Visible = true;
                }

                if (dataRow["IDChucNang"].Equals("qlbanhang") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnQLBanHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonNhapXuatHang.Visible = true;
                    //btnQLBanHang.Enabled = true;
                }

                if (dataRow["IDChucNang"].Equals("qlnhaphang") && Convert.ToInt32(dataRow["Xem"]) == 1)
                {
                    btnQLKhoHang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    ribbonNhapXuatHang.Visible = true;
                    //btnQLKhoHang.Enabled = true;
                }

                    
            }
            if (IDNhom == 1)
            {
                btnNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnNhomQuyen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnPhanQuyen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //btnNhanVien.Enabled = true;
                //btnNhomQuyen.Enabled = true;
                //btnPhanQuyen.Enabled = true;
            }
        }
        private void btnKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmKhachHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frmKhachHang frmN = new frmKhachHang();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnNhaCungCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmNhaCungCap));
            if (frm != null)
                frm.Activate();
            else
            {
                frmNhaCungCap frmN = new frmNhaCungCap();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnLoaiHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmLoaiHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frmLoaiHang frmN = new frmLoaiHang();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmNhanVien));
            if (frm != null)
                frm.Activate();
            else
            {
                frmNhanVien frmN = new frmNhanVien();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnNhomQuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmNhomQuyen));
            if (frm != null)
                frm.Activate();
            else
            {
                frmNhomQuyen frmN = new frmNhomQuyen();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnKetThuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát phần mềm?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }

        private void btnDangXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            this.checkDangXuat = true;
        }

        private void btnPhanQuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmPhanQuyen));
            if (frm != null)
                frm.Activate();
            else
            {
                frmPhanQuyen frmN = new frmPhanQuyen();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnDoiMatKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.IDNhanVien = IDNhanVien;
            frm.ShowDialog();
        }

        private void btnSanPham_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmSanPham));
            if (frm != null)
                frm.Activate();
            else
            {
                frmSanPham frmN = new frmSanPham();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }



        private void btnDonViTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmDonViTinh));
            if (frm != null)
                frm.Activate();
            else
            {
                frmDonViTinh frmN = new frmDonViTinh();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnNhapHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmNhapHang frm = new frmNhapHang();
            frm.ShowDialog();
        }

        private void btnBanHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBanHang frm = new frmBanHang();
            frm.ShowDialog();
        }



        private void btnQLKhoHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmQLNhapHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frmQLNhapHang frmN = new frmQLNhapHang();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnQLBanHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmQLBanHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frmQLBanHang frmN = new frmQLBanHang();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnBCBanHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmBCBanHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frmBCBanHang frmN = new frmBCBanHang();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }


        private void btnBCSanPham_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmBCSanPham));
            if (frm != null)
                frm.Activate();
            else
            {
                frmBCSanPham frmN = new frmBCSanPham();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnBCKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmBCKhachHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frmBCKhachHang frmN = new frmBCKhachHang();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnBCNhaCungCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmBCNhaCungCap));
            if (frm != null)
                frm.Activate();
            else
            {
                frmBCNhaCungCap frmN = new frmBCNhaCungCap();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void btnBCNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckForm(typeof(frmBCNhanVien));
            if (frm != null)
                frm.Activate();
            else
            {
                frmBCNhanVien frmN = new frmBCNhanVien();
                frmN.MdiParent = this;
                frmN.Show();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            txtThoiGian.Caption = "Thời gian " + (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " - " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;
        }
    }
}
