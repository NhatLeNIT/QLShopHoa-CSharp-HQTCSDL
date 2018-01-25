using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class QuerySQLDAO
    {
        public DataTable GetDataBySQL(string SQL)
        {
            return DBConnect.Instance.GetDataTable(SQL);
        }
        public int ExecuteBySQL(string SQL)
        {
            return DBConnect.Instance.ExecuteSQL(SQL);
        }

        public void CloseConnect()
        {
            DBConnect.Instance.CloseConnect();
        }
    }
}
