using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class QuerySQLBUS
    {
        QuerySQLDAO dao = new QuerySQLDAO();
        public DataTable GetDataBySQL(string SQL)
        {
            return dao.GetDataBySQL(SQL);
        }
        public int ExecuteBySQL(string SQL)
        {
            return dao.ExecuteBySQL(SQL);
        }

        public void CloseConnect()
        {
            dao.CloseConnect();
        }
    }
}
