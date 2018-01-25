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
    public class NhanVienDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_NhanVien_Select_All", null);
        }
        public DataTable GetDataByID(string IDNhanVien)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", IDNhanVien)
            };
            return DBConnect.Instance.GetDataTable("sp_NhanVien_Select_ByID", param);
        }
        public DataTable GetDataByUserName(string TaiKhoan)
        {
            SqlParameter[] param =
            {
                new SqlParameter("TaiKhoan", TaiKhoan)
            };
            return DBConnect.Instance.GetDataTable("sp_NhanVien_Select_ByUserName", param);
        }
        public int Insert(NhanVien obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", obj.IDNhanVien),
                new SqlParameter("TaiKhoan", obj.TaiKhoan),
                new SqlParameter("MatKhau", obj.MatKhau),
                new SqlParameter("HoTen", obj.HoTen),
                new SqlParameter("DienThoai", obj.DienThoai),
                new SqlParameter("DiaChi", obj.DiaChi),
                new SqlParameter("NgaySinh", obj.NgaySinh),
                new SqlParameter("GioiTinh", obj.GioiTinh),
                new SqlParameter("TrangThai", obj.TrangThai),
                new SqlParameter("IDNhom", obj.IDNhom),
                new SqlParameter("GhiChu", obj.GhiChu)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhanVien_Insert", param);
        }
        public int Update(NhanVien obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", obj.IDNhanVien),
                new SqlParameter("MatKhau", obj.MatKhau),
                new SqlParameter("HoTen", obj.HoTen),
                new SqlParameter("DienThoai", obj.DienThoai),
                new SqlParameter("DiaChi", obj.DiaChi),
                new SqlParameter("NgaySinh", obj.NgaySinh),
                new SqlParameter("GioiTinh", obj.GioiTinh),
                new SqlParameter("TrangThai", obj.TrangThai),
                new SqlParameter("IDNhom", obj.IDNhom),
                new SqlParameter("GhiChu", obj.GhiChu)

            };
            return DBConnect.Instance.ExecuteSQL("sp_NhanVien_Update", param);
        }
        public int UpdatePassword(string IDNhanVien, string MatKhau)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", IDNhanVien),
                new SqlParameter("MatKhau", MatKhau)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhanVien_Update_Password", param);
        }
        public int Delete(string IDNhanVien)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhanVien", IDNhanVien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhanVien_Delete", param);
        }
    }
}
