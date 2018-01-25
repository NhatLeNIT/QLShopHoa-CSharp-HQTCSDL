using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using ValueObject;
using System.Data;

namespace BusinessLogicLayer
{
    public class HoaDonBUS
    {
        HoaDonDAO dao = new HoaDonDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(string IDHoaDon)
        {
            return dao.GetDataByID(IDHoaDon);
        }
        public DataTable GetDataByDate(string NgayDau, string NgayCuoi)
        {
            return dao.GetDataByDate(NgayDau, NgayCuoi);
        }
        public int Insert(HoaDon obj)
        {
            return dao.Insert(obj);
        }
        public int Update(HoaDon obj)
        {
            return dao.Update(obj);
        }
        public int Delete(string IDHoaDon)
        {
            return dao.Delete(IDHoaDon);
        }
        
        
        public DataTable NVStatistic_ByWeek()
        {
            return dao.NVStatistic_ByWeek();
        }
        public DataTable NVStatistic_ByMonth()
        {
            return dao.NVStatistic_ByMonth();
        }
        public DataTable NVStatistic_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.NVStatistic_ByDate(NgayDau, NgayCuoi);
        }
        
        public DataTable BHStatistic_ByWeek()
        {
            return dao.BHStatistic_ByWeek();
        }
        public DataTable BHStatistic_ByMonth()
        {
            return dao.BHStatistic_ByMonth();
        }
        public DataTable BHStatistic_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.BHStatistic_ByDate(NgayDau, NgayCuoi);
        }
        public DataTable ChiTietBanHangTheoThoiGian(string ngayLap)
        {
            return dao.ChiTietBanHangTheoThoiGian(ngayLap);
        }
        public DataTable ChiTietSanPhamTheoLoiNhuan(string IDSanPham)
        {
            return dao.ChiTietSanPhamTheoLoiNhuan(IDSanPham);
        }

        

        public DataTable ChiTietSanPhamTheoNhanVien_Tuan(string idSanPham)
        {
            return dao.ChiTietSanPhamTheoNhanVien_Tuan(idSanPham);
        }
        public DataTable ChiTietSanPhamTheoNhanVien_Thang(string idSanPham)
        {
            return dao.ChiTietSanPhamTheoNhanVien_Thang(idSanPham);
        }

        public DataTable ChiTietSanPhamTheoNhanVien_Ngay(string idSanPham, string ngayDau, string ngayCuoi)
        {
            return dao.ChiTietSanPhamTheoNhanVien_Ngay(idSanPham, ngayDau, ngayCuoi);
        }
        public DataTable ChiTietSanPhamTheoKhachHang_Tuan(string idSanPham)
        {
            return dao.ChiTietSanPhamTheoKhachHang_Tuan(idSanPham);
        }
        public DataTable ChiTietSanPhamTheoKhachHang_Thang(string idSanPham)
        {
            return dao.ChiTietSanPhamTheoKhachHang_Thang(idSanPham);
        }

        public DataTable ChiTietSanPhamTheoKhachHang_Ngay(string idSanPham, string ngayDau, string ngayCuoi)
        {
            return dao.ChiTietSanPhamTheoKhachHang_Ngay(idSanPham, ngayDau, ngayCuoi);
        }
        public DataTable ChiTietKhachHangTheoLoiNhuan_Tuan(string idKhachHang)
        {
            return dao.ChiTietKhachHangTheoLoiNhuan_Tuan(idKhachHang);
        }
        public DataTable ChiTietKhachHangTheoLoiNhuan_Thang(string idKhachHang)
        {
            return dao.ChiTietKhachHangTheoLoiNhuan_Thang(idKhachHang);
        }

        public DataTable ChiTietKhachHangTheoLoiNhuan_Ngay(string idKhachHang, string ngayDau, string ngayCuoi)
        {
            return dao.ChiTietKhachHangTheoLoiNhuan_Ngay(idKhachHang, ngayDau, ngayCuoi);
        }
        public DataTable ChiTietNhanVienTheoBanHang_Tuan(string IDNhanVien)
        {
            return dao.ChiTietNhanVienTheoBanHang_Tuan(IDNhanVien);
        }
        public DataTable ChiTietNhanVienTheoBanHang_Thang(string IDNhanVien)
        {
            return dao.ChiTietNhanVienTheoBanHang_Thang(IDNhanVien);
        }

        public DataTable ChiTietNhanVienTheoBanHang_Ngay(string IDNhanVien, string ngayDau, string ngayCuoi)
        {
            return dao.ChiTietNhanVienTheoBanHang_Ngay(IDNhanVien, ngayDau, ngayCuoi);
        }
    }
}
