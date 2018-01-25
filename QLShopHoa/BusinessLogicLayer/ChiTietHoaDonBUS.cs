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
    public class ChiTietHoaDonBUS
    {
        ChiTietHoaDonDAO dao = new ChiTietHoaDonDAO();

        public DataTable GetDataByID(string IDHoaDon)
        {
            return dao.GetDataByID(IDHoaDon);
        }
        public DataTable GetDataByIDSanPham(string IDDonHang, string IDSanPham)
        {
            return dao.GetDataByIDSanPham(IDDonHang, IDSanPham);
        }
        public int Insert(ChiTietHoaDon obj)
        {
            return dao.Insert(obj);
        }
        public int Delete(string IDHoaDon)
        {
            return dao.Delete(IDHoaDon);
        }
        public int DeleteByIDSanPham(string IDHoaDon, string IDSanPham)
        {
            return dao.DeleteByIDSanPham(IDHoaDon, IDSanPham);
        }
        public int UpdateQuantity(ChiTietHoaDon obj)
        {
            return dao.UpdateQuantity(obj);
        }

        public DataTable StatisticLN_ByWeek()
        {
            return dao.StatisticLN_ByWeek();
        }
        public DataTable StatisticLN_ByMonth()
        {
            return dao.StatisticLN_ByMonth();
        }
        public DataTable StatisticLN_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.StatisticLN_ByDate(NgayDau, NgayCuoi);
        }
        public DataTable StatisticNV_ByWeek()
        {
            return dao.StatisticNV_ByWeek();
        }
        public DataTable StatisticNV_ByMonth()
        {
            return dao.StatisticNV_ByMonth();
        }
        public DataTable StatisticNV_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.StatisticNV_ByDate(NgayDau, NgayCuoi);
        }
        public DataTable StatisticKH_ByWeek()
        {
            return dao.StatisticKH_ByWeek();
        }
        public DataTable StatisticKH_ByMonth()
        {
            return dao.StatisticKH_ByMonth();
        }
        public DataTable StatisticKH_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.StatisticKH_ByDate(NgayDau, NgayCuoi);
        }

        public DataTable KHStatisticLN_ByWeek()
        {
            return dao.KHStatisticLN_ByWeek();
        }
        public DataTable KHStatisticLN_ByMonth()
        {
            return dao.KHStatisticLN_ByMonth();
        }
        public DataTable KHStatisticLN_ByDate(string NgayDau, string NgayCuoi)
        {
            return dao.KHStatisticLN_ByDate(NgayDau, NgayCuoi);
        }
    }
}
