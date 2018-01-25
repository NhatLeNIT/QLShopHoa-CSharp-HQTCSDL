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
    public class ChiTietNhapHangBUS
    {
        ChiTietNhapHangDAO dao = new ChiTietNhapHangDAO();

        public DataTable GetDataByID(string IDNhapHang)
        {
            return dao.GetDataByID(IDNhapHang);
        }
        public DataTable GetDataByIDSanPham(string IDNhapHang, string IDSanPham)
        {
            return dao.GetDataByIDSanPham(IDNhapHang, IDSanPham);
        }
        public int Insert(ChiTietNhapHang obj)
        {
            return dao.Insert(obj);
        }
        public int Delete(string IDNhapHang)
        {
            return dao.Delete(IDNhapHang);
        }
        public int DeleteByIDSanPham(string IDNhapHang, string IDSanPham)
        {
            return dao.DeleteByIDSanPham(IDNhapHang, IDSanPham);
        }
        public int UpdateQuantity(ChiTietNhapHang obj)
        {
            return dao.UpdateQuantity(obj);
        }
        public DataTable StatisticNCC_ByWeek()
        {
            return dao.StatisticNCC_ByWeek();
        }
        public DataTable StatisticNCC_ByMonth()
        {
            return dao.StatisticNCC_ByMonth();
        }
        public DataTable StatisticNCC_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.StatisticNCC_ByDate(NgayDau, NgayCuoi);
        }
        public DataTable NCCStatistic_ByWeek()
        {
            return dao.NCCStatistic_ByWeek();
        }
        public DataTable NCCStatistic_ByMonth()
        {
            return dao.NCCStatistic_ByMonth();
        }
        public DataTable NCCStatistic_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.NCCStatistic_ByDate(NgayDau, NgayCuoi);
        }
    }
}
