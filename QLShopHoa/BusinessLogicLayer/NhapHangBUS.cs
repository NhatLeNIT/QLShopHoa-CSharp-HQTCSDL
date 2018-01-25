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
    public class NhapHangBUS
    {
        NhapHangDAO dao = new NhapHangDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(string IDNhapHang)
        {
            return dao.GetDataByID(IDNhapHang);
        }
        public DataTable GetDataByDate(string NgayDau, string NgayCuoi)
        {
            return dao.GetDataByDate(NgayDau, NgayCuoi);
        }
        public int Insert(NhapHang obj)
        {
            return dao.Insert(obj);
        }
        public int Update(NhapHang obj)
        {
            return dao.Update(obj);
        }
        public int Delete(string IDNhapHang)
        {
            return dao.Delete(IDNhapHang);
        }
        public DataTable ChiTietSanPhamTheoNCC_Tuan(string idSanPham)
        {
            return dao.ChiTietSanPhamTheoNCC_Tuan(idSanPham);
        }
        public DataTable ChiTietSanPhamTheoNCC_Thang(string idSanPham)
        {
            return dao.ChiTietSanPhamTheoNCC_Thang(idSanPham);
        }

        public DataTable ChiTietSanPhamTheoNCC_Ngay(string idSanPham, string ngayDau, string ngayCuoi)
        {
            return dao.ChiTietSanPhamTheoNCC_Ngay(idSanPham, ngayDau, ngayCuoi);
        }
        public DataTable ChiTietNCCTheoNhapHang_Tuan(string IDNhaCungCap)
        {
            return dao.ChiTietNCCTheoNhapHang_Tuan(IDNhaCungCap);
        }
        public DataTable ChiTietNCCTheoNhapHang_Thang(string IDNhaCungCap)
        {
            return dao.ChiTietNCCTheoNhapHang_Thang(IDNhaCungCap);
        }
        public DataTable ChiTietNCCTheoNhapHang_Ngay(string IDNhaCungCap, string ngayDau, string ngayCuoi)
        {
            return dao.ChiTietNCCTheoNhapHang_Ngay(IDNhaCungCap, ngayDau, ngayCuoi);
        }
    }
}
