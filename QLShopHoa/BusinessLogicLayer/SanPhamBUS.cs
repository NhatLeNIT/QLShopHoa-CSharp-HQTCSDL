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
    public class SanPhamBUS
    {
        SanPhamDAO dao = new SanPhamDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByStatus()
        {
            return dao.GetDataByStatus();
        }
        public DataTable GetDataByStatus_Quantity()
        {
            return dao.GetDataByStatus_Quantity();
        }
        public DataTable GetDataByID(string IDSanPham)
        {
            return dao.GetDataByID(IDSanPham);
        }
        public DataTable GetDataByID_Fix_DirtyRead(string IDSanPham)
        {
            return dao.GetDataByID_Fix_DirtyRead(IDSanPham);
        }
        public DataTable GetDataByID_Quantity(string IDSanPham)
        {
            return dao.GetDataByID_Quantity(IDSanPham);
        }
        public DataTable GetDataAll()
        {
            return dao.GetDataAll();
        }
        public int Insert(SanPham obj)
        {
            return dao.Insert(obj);
        }
        public int Update(SanPham obj)
        {
            return dao.Update(obj);
        }

        public int Update_DirtyRead(SanPham obj)
        {
            return dao.Update_DirtyRead(obj);
        }

        public int Update_LostUpdate(SanPham obj)
        {
            return dao.Update_LostUpdate(obj);
        }

        public int UpdateQuantity(SanPham obj)
        {
            return dao.UpdateQuantity(obj);
        }
        public int UpdateQuantitySub(SanPham obj)
        {
            return dao.UpdateQuantitySub(obj);
        }
        public int Delete(string IDSanPham)
        {
            return dao.Delete(IDSanPham);
        }
    }
}
