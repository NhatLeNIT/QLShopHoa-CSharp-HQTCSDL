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
    public class ChucNangDAO
    {
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_ChucNang_Select_All", null);
        }
        public DataTable GetDataByID(string IDChucNang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDChucNang", IDChucNang)
            };
            return DBConnect.Instance.GetDataTable("sp_ChucNang_Select_ByID", param);
        }
    }
}
