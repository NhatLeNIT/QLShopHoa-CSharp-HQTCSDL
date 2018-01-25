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
    public class NhomQuyenDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_NhomQuyen_Select_All", null);
        }
        public DataTable GetDataByID(int IDNhom)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", IDNhom)
            };
            return DBConnect.Instance.GetDataTable("sp_NhomQuyen_Select_ByID", param);
        }
        public DataTable GetDataNotInPhanQuyen()
        {
            return DBConnect.Instance.GetDataTable("sp_NhomQuyen_NotInPhanQuyen", null);
        }
        public DataTable GetDataInPhanQuyen()
        {
            return DBConnect.Instance.GetDataTable("sp_NhomQuyen_SelectInPhanQuyen", null);
        }
        public int Insert(NhomQuyen obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("TenNhom", obj.TenNhom),
                new SqlParameter("MoTa", obj.MoTa)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhomQuyen_Insert", param);
        }
        public int Update(NhomQuyen obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", obj.IDNhom),
                new SqlParameter("TenNhom", obj.TenNhom),
                new SqlParameter("MoTa", obj.MoTa)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhomQuyen_Update", param);
        }
        public int Delete(int IDNhom)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", IDNhom)
            };
            return DBConnect.Instance.ExecuteSQL("sp_NhomQuyen_Delete", param);
        }
    }
}
