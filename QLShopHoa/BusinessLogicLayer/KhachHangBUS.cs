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
    public class KhachHangBUS
    {
        KhachHangDAO dao = new KhachHangDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(string IDKhachHang)
        {
            return dao.GetDataByID(IDKhachHang);
        }
        public int Insert(KhachHang obj)
        {
            return dao.Insert(obj);
        }
        public int Update(KhachHang obj)
        {
            return dao.Update(obj);
        }
        public int Delete(string IDKhachHang)
        {
            return dao.Delete(IDKhachHang);
        }
    }
}
