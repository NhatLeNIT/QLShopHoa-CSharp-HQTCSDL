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
    public class HoaDonDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_HoaDon_Select_All", null);
        }
        public DataTable GetDataByID(string IDHoaDon)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", IDHoaDon)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_Select_ByID", param);
        }
        public DataTable GetDataByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_Select_ByDate", param);
        }
        public int Insert(HoaDon obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", obj.IDHoaDon),
                new SqlParameter("NgayLap", obj.NgayLap),
                new SqlParameter("IDNhanVien", obj.IDNhanVien),
                new SqlParameter("IDKhachHang", obj.IDKhachHang),
                new SqlParameter("TenNguoiNhan", obj.TenNguoiNhan),
                new SqlParameter("DiaChiNguoiNhan", obj.DiaChiNguoiNhan),
                new SqlParameter("DienThoaiNguoiNhan", obj.DienThoaiNguoiNhan),
                new SqlParameter("NgayGiao", obj.NgayGiao),
                new SqlParameter("TrangThaiThanhToan", obj.TrangThaiThanhToan),
                new SqlParameter("TrangThaiGiaoHang", obj.TrangThaiGiaoHang),
                new SqlParameter("GhiChu", obj.GhiChu),
                new SqlParameter("SoLuongSanPham", obj.SoLuongSanPham),
                new SqlParameter("TongTien", obj.TongTien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_HoaDon_Insert", param);
        }
        public int Update(HoaDon obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", obj.IDHoaDon),
                new SqlParameter("NgayLap", obj.NgayLap),
                new SqlParameter("IDNhanVien", obj.IDNhanVien),
                new SqlParameter("IDKhachHang", obj.IDKhachHang),
                new SqlParameter("TenNguoiNhan", obj.TenNguoiNhan),
                new SqlParameter("DiaChiNguoiNhan", obj.DiaChiNguoiNhan),
                new SqlParameter("DienThoaiNguoiNhan", obj.DienThoaiNguoiNhan),
                new SqlParameter("NgayGiao", obj.NgayGiao),
                new SqlParameter("TrangThaiThanhToan", obj.TrangThaiThanhToan),
                new SqlParameter("TrangThaiGiaoHang", obj.TrangThaiGiaoHang),
                new SqlParameter("GhiChu", obj.GhiChu),
                new SqlParameter("SoLuongSanPham", obj.SoLuongSanPham),
                new SqlParameter("TongTien", obj.TongTien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_HoaDon_Update", param);
        }
        public int Delete(string IDHoaDon)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", IDHoaDon)
            };
            return DBConnect.Instance.ExecuteSQL("sp_HoaDon_Delete", param);
        }

        public DataTable NVStatistic_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_HoaDon_NVStatistic_ByWeek", null);
        }
        public DataTable NVStatistic_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_HoaDon_NVStatistic_ByMonth", null);
        }
        public DataTable NVStatistic_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_NVStatistic_ByDate", param);
        }

        public DataTable BHStatistic_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_HoaDon_BHStatistic_ByWeek", null);
        }
        public DataTable BHStatistic_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_HoaDon_BHStatistic_ByMonth", null);
        }
        public DataTable BHStatistic_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_BHStatistic_ByDate", param);
        }
        public DataTable ChiTietBanHangTheoThoiGian(string ngayLap)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayLap", ngayLap)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietBanHangTheoThoiGian", param);
        }
        public DataTable ChiTietSanPhamTheoLoiNhuan(string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoLoiNhuan", param);
        }

        public DataTable ChiTietSanPhamTheoNhanVien(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoNhanVien", param);
        }

        public DataTable ChiTietSanPhamTheoNhanVien_Tuan(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoNhanVien_Tuan", param);
        }
        public DataTable ChiTietSanPhamTheoNhanVien_Thang(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoNhanVien_Thang", param);
        }
        public DataTable ChiTietSanPhamTheoNhanVien_Ngay(string idSanPham, string ngayDau, string ngayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham),
                new SqlParameter("NgayDau", ngayDau),
                new SqlParameter("NgayCuoi", ngayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoNhanVien_Ngay", param);
        }
        public DataTable ChiTietSanPhamTheoKhachHang_Tuan(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoKhachHang_Tuan", param);
        }
        public DataTable ChiTietSanPhamTheoKhachHang_Thang(string idSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoKhachHang_Thang", param);
        }
        public DataTable ChiTietSanPhamTheoKhachHang_Ngay(string idSanPham, string ngayDau, string ngayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", idSanPham),
                new SqlParameter("NgayDau", ngayDau),
                new SqlParameter("NgayCuoi", ngayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietSanPhamTheoKhachHang_Ngay", param);
        }

        public DataTable ChiTietKhachHangTheoLoiNhuan_Tuan(string idKhachHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", idKhachHang)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Tuan", param);
        }
        public DataTable ChiTietKhachHangTheoLoiNhuan_Thang(string idKhachHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", idKhachHang)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Thang", param);
        }
        public DataTable ChiTietKhachHangTheoLoiNhuan_Ngay(string idKhachHang, string ngayDau, string ngayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", idKhachHang),
                new SqlParameter("NgayDau", ngayDau),
                new SqlParameter("NgayCuoi", ngayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Ngay", param);
        }
        public DataTable ChiTietNhanVienTheoBanHang_Tuan(string IDNhanVien)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", IDNhanVien)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietNhanVienTheoBanHang_Tuan", param);
        }
        public DataTable ChiTietNhanVienTheoBanHang_Thang(string IDNhanVien)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", IDNhanVien)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietNhanVienTheoBanHang_Thang", param);
        }
        public DataTable ChiTietNhanVienTheoBanHang_Ngay(string IDNhanVien, string ngayDau, string ngayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", IDNhanVien),
                new SqlParameter("NgayDau", ngayDau),
                new SqlParameter("NgayCuoi", ngayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_HoaDon_ChiTietNhanVienTheoBanHang_Ngay", param);
        }
    }
}
