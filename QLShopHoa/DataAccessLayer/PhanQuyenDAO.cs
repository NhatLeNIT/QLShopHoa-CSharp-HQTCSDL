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
    public class PhanQuyenDAO
    {
        public DataTable GetDataByIDNhom(int IDNhom)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", IDNhom)
            };
            return DBConnect.Instance.GetDataTable("sp_PhanQuyen_Select_ByIDNhom", param);
        }
        public DataTable GetDataByIDNhomAndIDChucNang(int IDNhom, string IDChucNang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", IDNhom),
                new SqlParameter("IDChucNang", IDChucNang)
            };
            return DBConnect.Instance.GetDataTable("sp_PhanQuyen_Select_ByIDNhomAndIDChucNang", param);
        }
        public int Insert(PhanQuyen obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", obj.IDNhom),
                new SqlParameter("IDChucNang", obj.IDChucNang),
                new SqlParameter("Xem", obj.Xem),
                new SqlParameter("Them", obj.Them),
                new SqlParameter("Sua", obj.Sua),
                new SqlParameter("Xoa", obj.Xoa)
            };
            return DBConnect.Instance.ExecuteSQL("sp_PhanQuyen_Insert", param);
        }
        public int Update(PhanQuyen obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", obj.IDNhom),
                new SqlParameter("IDChucNang", obj.IDChucNang),
                new SqlParameter("Xem", obj.Xem),
                new SqlParameter("Them", obj.Them),
                new SqlParameter("Sua", obj.Sua),
                new SqlParameter("Xoa", obj.Xoa)
            };
            return DBConnect.Instance.ExecuteSQL("sp_PhanQuyen_Update", param);
        }

        public int Delete(int IDNhom)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhom", IDNhom)
            };
            return DBConnect.Instance.ExecuteSQL("sp_PhanQuyen_Delete", param);
        }
    }
}
