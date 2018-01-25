using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using ValueObject;using System.Data;

namespace BusinessLogicLayer
{
    public class ChucNangBUS
    {
        ChucNangDAO dao = new ChucNangDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(string IDChucNang)
        {
            return dao.GetDataByID(IDChucNang);
        }
    }
}
