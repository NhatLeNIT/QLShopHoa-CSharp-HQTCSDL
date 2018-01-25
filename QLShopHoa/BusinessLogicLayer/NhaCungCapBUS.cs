using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using ValueObject;
using System.Data;

namespace BusinessLogicLayer
{
    public class NhaCungCapBUS
    {
        NhaCungCapDAO dao = new NhaCungCapDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(string IDNhaCungCap)
        {
            return dao.GetDataByID(IDNhaCungCap);
        }
        public int Insert(NhaCungCap obj)
        {
            return dao.Insert(obj);
        }
        public int Update(NhaCungCap obj)
        {
            return dao.Update(obj);
        }
        public int Delete(string IDNhaCungCap)
        {
            return dao.Delete(IDNhaCungCap);
        }
    }
}
