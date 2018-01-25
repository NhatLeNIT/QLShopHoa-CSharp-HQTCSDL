using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class checkPhanQuyenBUS
    {
        public DataTable GetDataTablePhanQuyen(string IDNhanVien)
        {
            NhanVienBUS busNV = new NhanVienBUS();
            var dt = busNV.GetDataByID(IDNhanVien);
            int IDNhom = Convert.ToInt32(dt.Rows[0]["IDNhom"]);
            PhanQuyenBUS busPQ = new PhanQuyenBUS();
            return busPQ.GetDataByIDNhom(IDNhom);
        }
    }
}
