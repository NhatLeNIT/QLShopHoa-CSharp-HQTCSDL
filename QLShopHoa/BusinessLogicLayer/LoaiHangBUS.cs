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
    public class LoaiHangBUS
    {
        LoaiHangDAO dao = new LoaiHangDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(int IDLoaiHang)
        {
            return dao.GetDataByID(IDLoaiHang);
        }
        public DataTable GetDataActive()
        {
            return dao.GetDataActive();
        }
        public int Insert(LoaiHang obj)
        {
            return dao.Insert(obj);
        }
        public int Update(LoaiHang obj)
        {
            return dao.Update(obj);
        }
        public int Delete(int IDLoaiHang)
        {
            return dao.Delete(IDLoaiHang);
        }
    }
}
