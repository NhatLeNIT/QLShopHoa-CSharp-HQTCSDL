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
    public class LoaiHangDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_LoaiHang_Select_All", null);
        }
        public DataTable GetDataByID(int IDLoaiHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDLoaiHang", IDLoaiHang)
            };
            return DBConnect.Instance.GetDataTable("sp_LoaiHang_Select_ByID", param);
        }
        public DataTable GetDataActive()
        {
            return DBConnect.Instance.GetDataTable("sp_LoaiHang_Select_Active", null);
        }
        public int Insert(LoaiHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("TenLoaiHang", obj.TenLoaiHang),
                new SqlParameter("MoTa", obj.MoTa),
                new SqlParameter("TinhTrang", obj.TinhTrang)

            };
            return DBConnect.Instance.ExecuteSQL("sp_LoaiHang_Insert", param);
        }
        public int Update(LoaiHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDLoaiHang", obj.IDLoaiHang),
                new SqlParameter("TenLoaiHang", obj.TenLoaiHang),
                new SqlParameter("MoTa", obj.MoTa),
                new SqlParameter("TinhTrang", obj.TinhTrang)
            };
            return DBConnect.Instance.ExecuteSQL("sp_LoaiHang_Update", param);
        }
        public int Delete(int IDLoaiHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDLoaiHang", IDLoaiHang)
            };
            return DBConnect.Instance.ExecuteSQL("sp_LoaiHang_Delete", param);
        }
    }
}
