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
    public class PhanQuyenBUS
    {
        PhanQuyenDAO dao = new PhanQuyenDAO();
        public DataTable GetDataByIDNhom(int IDNhom)
        {
            return dao.GetDataByIDNhom(IDNhom);
        }
        public DataTable GetDataByIDNhomAndIDChucNang(int IDNhom, string IDChucNhang)
        {
            return dao.GetDataByIDNhomAndIDChucNang(IDNhom, IDChucNhang);
        }
        public int Insert(PhanQuyen obj)
        {
            return dao.Insert(obj);
        }
        public int Update(PhanQuyen obj)
        {
            return dao.Update(obj);
        }
        public int Delete(int IDNhom)
        {
            return dao.Delete(IDNhom);
        }
    }
}
