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
    public class NhanVienBUS
    {
        NhanVienDAO dao = new NhanVienDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }
        public DataTable GetDataByID(string IDNhanVien)
        {
            return dao.GetDataByID(IDNhanVien);
        }
        public DataTable GetDataByUserName(string TaiKhoan)
        {
            return dao.GetDataByUserName(TaiKhoan);
        }
        public int Insert(NhanVien obj)
        {
            return dao.Insert(obj);
        }
        public int Update(NhanVien obj)
        {
            return dao.Update(obj);
        }
        public int UpdatePassword(string IDNhanVien, string MatKhau)
        {
            return dao.UpdatePassword(IDNhanVien, MatKhau);
        }
        public int Delete(string IDNhanVien)
        {
            return dao.Delete(IDNhanVien);
        }
    }
}
