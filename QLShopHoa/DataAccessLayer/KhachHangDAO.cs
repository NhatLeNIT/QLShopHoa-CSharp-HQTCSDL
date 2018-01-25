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
    public class KhachHangDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_KhachHang_Select_All", null);
        }
        public DataTable GetDataByID(string IDKhachHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", IDKhachHang)
            };
            return DBConnect.Instance.GetDataTable("sp_KhachHang_Select_ByID", param);
        }
        public int Insert(KhachHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", obj.IDKhachHang),
                new SqlParameter("HoTen", obj.HoTen),
                new SqlParameter("DienThoai", obj.DienThoai),
                new SqlParameter("DiaChi", obj.DiaChi),
                new SqlParameter("NgaySinh", obj.NgaySinh),
                new SqlParameter("GioiTinh", obj.GioiTinh),
                new SqlParameter("GhiChu", obj.GhiChu)
            };
            return DBConnect.Instance.ExecuteSQL("sp_KhachHang_Insert", param);
        }
        public int Update(KhachHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", obj.IDKhachHang),
                new SqlParameter("HoTen", obj.HoTen),
                new SqlParameter("DienThoai", obj.DienThoai),
                new SqlParameter("DiaChi", obj.DiaChi),
                new SqlParameter("NgaySinh", obj.NgaySinh),
                new SqlParameter("GioiTinh", obj.GioiTinh),
                new SqlParameter("GhiChu", obj.GhiChu)

            };
            return DBConnect.Instance.ExecuteSQL("sp_KhachHang_Update", param);
        }
        public int Delete(string IDKhachHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDKhachHang", IDKhachHang)
            };
            return DBConnect.Instance.ExecuteSQL("sp_KhachHang_Delete", param);
        }
    }
}
