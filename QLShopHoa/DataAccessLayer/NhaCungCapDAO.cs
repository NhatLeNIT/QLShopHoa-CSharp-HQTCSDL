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
    public class NhaCungCapDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_NhaCungCap_Select_All", null);
        }
        public DataTable GetDataByID(string IDNhaCungCap)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", IDNhaCungCap)
            };
            return DBConnect.Instance.GetDataTable("sp_NhaCungCap_Select_ByID", param);
        }
        public int Insert(NhaCungCap obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("TenNhaCungCap", obj.TenNhaCungCap),
                new SqlParameter("DienThoai", obj.DienThoai),
                new SqlParameter("DiaChi", obj.DiaChi),
                new SqlParameter("GhiChu", obj.GhiChu)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhaCungCap_Insert", param);
        }
        public int Update(NhaCungCap obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("TenNhaCungCap", obj.TenNhaCungCap),
                new SqlParameter("DienThoai", obj.DienThoai),
                new SqlParameter("DiaChi", obj.DiaChi),
                new SqlParameter("GhiChu", obj.GhiChu)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhaCungCap_Update", param);
        }
        public int Delete(string IDNhaCungCap)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhaCungCap", IDNhaCungCap)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhaCungCap_Delete", param);
        }
    }
}
