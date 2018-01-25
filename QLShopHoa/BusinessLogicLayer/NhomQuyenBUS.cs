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
    public class NhomQuyenBUS
    {
        NhomQuyenDAO dao = new NhomQuyenDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(int IDNhom)
        {
            return dao.GetDataByID(IDNhom);
        }
        public DataTable GetDataNotInPhanQuyen()
        {
            return dao.GetDataNotInPhanQuyen();
        }
        public DataTable GetDataInPhanQuyen()
        {
            return dao.GetDataInPhanQuyen();
        }
        public int Insert(NhomQuyen obj)
        {
            return dao.Insert(obj);
        }
        public int Update(NhomQuyen obj)
        {
            return dao.Update(obj);
        }
        public int Delete(int IDNhom)
        {
            return dao.Delete(IDNhom);
        }
    }
}
