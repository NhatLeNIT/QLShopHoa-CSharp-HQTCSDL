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
    public class DonViTinhDAO
    {
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_DonViTinh_Select_All", null);
        }

        public DataTable GetData_UnRepeatable(int IDDonViTinh)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDDonViTinh", IDDonViTinh)
            };
            return DBConnect.Instance.GetDataTable("sp_DonViTinh_Select_All_UnRepeatable_View", param);
        }

        public DataTable GetData_Fix_UnRepeatable(int IDDonViTinh)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDDonViTinh", IDDonViTinh)
            };
            return DBConnect.Instance.GetDataTable("sp_DonViTinh_Select_All_Fix_UnRepeatable_View", param);
        }

        public DataTable GetData_Phantom()
        {
            return DBConnect.Instance.GetDataTable("sp_DonViTinh_Select_All_Phantom_View", null);
        }

        public DataTable GetData_Fix_Phantom()
        {
            return DBConnect.Instance.GetDataTable("sp_DonViTinh_Select_All_Fix_Phantom_View", null);
        }

        public DataTable GetDataByID(int IDDonViTinh)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDDonViTinh", IDDonViTinh)
            };
            return DBConnect.Instance.GetDataTable("sp_DonViTinh_Select_ByID", param);
        }
        public int Insert(DonViTinh obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("TenDonViTinh", obj.TenDonViTinh),
                new SqlParameter("GhiChu", obj.GhiChu)

            };
            return DBConnect.Instance.ExecuteSQL("sp_DonViTinh_Insert", param);
        }
        public int Update(DonViTinh obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh),
                new SqlParameter("TenDonViTinh", obj.TenDonViTinh),
                new SqlParameter("GhiChu", obj.GhiChu)
            };
            return DBConnect.Instance.ExecuteSQL("sp_DonViTinh_Update", param);
        }
        public int Delete(int IDDonViTinh)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDDonViTinh", IDDonViTinh)
            };
            return DBConnect.Instance.ExecuteSQL("sp_DonViTinh_Delete", param);
        }
    }
}
