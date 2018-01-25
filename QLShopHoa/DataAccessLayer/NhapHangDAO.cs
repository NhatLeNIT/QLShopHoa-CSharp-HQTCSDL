using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValueObject;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class NhapHangDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_NhapHang_Select_All", null);
        }
        public DataTable GetDataByID(string IDNhapHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", IDNhapHang)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_Select_ByID", param);
        }
        public DataTable GetDataByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_Select_ByDate", param);
        }
        public int Insert(NhapHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", obj.IDNhapHang),
                new SqlParameter("NgayNhap", obj.NgayNhap),
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("IDNhanVien", obj.IDNhanVien),
                new SqlParameter("GhiChu", obj.GhiChu),
                new SqlParameter("SoLuongSanPham", obj.SoLuongSanPham),
                new SqlParameter("TongTien", obj.TongTien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhapHang_Insert", param);
        }
        public int Update(NhapHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", obj.IDNhapHang),
                new SqlParameter("NgayNhap", obj.NgayNhap),
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("IDNhanVien", obj.IDNhanVien),
                new SqlParameter("GhiChu", obj.GhiChu),
                new SqlParameter("SoLuongSanPham", obj.SoLuongSanPham),
                new SqlParameter("TongTien", obj.TongTien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhapHang_Update", param);
        }
        public int Delete(string IDNhapHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", IDNhapHang)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhapHang_Delete", param);
        }
        public DataTable ChiTietSanPhamTheoNCC_Tuan(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_ChiTietSanPhamTheoNCC_Tuan", param);
        }
        public DataTable ChiTietSanPhamTheoNCC_Thang(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_ChiTietSanPhamTheoNCC_Thang", param);
        }
        public DataTable ChiTietSanPhamTheoNCC_Ngay(string idSanPham, string ngayDau, string ngayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham),
                new SqlParameter("NgayDau", ngayDau),
                new SqlParameter("NgayCuoi", ngayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_ChiTietSanPhamTheoNCC_Ngay", param);
        }
        public DataTable ChiTietNCCTheoNhapHang_Tuan(string IDNhaCungCap)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", IDNhaCungCap)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_ChiTietNCCTheoNhapHang_Tuan", param);
        }
        public DataTable ChiTietNCCTheoNhapHang_Thang(string IDNhaCungCap)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", IDNhaCungCap)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_ChiTietNCCTheoNhapHang_Thang", param);
        }
        public DataTable ChiTietNCCTheoNhapHang_Ngay(string IDNhaCungCap, string ngayDau, string ngayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", IDNhaCungCap),
                new SqlParameter("NgayDau", ngayDau),
                new SqlParameter("NgayCuoi", ngayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_NhapHang_ChiTietNCCTheoNhapHang_Ngay", param);
        }
    }
}
