using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValueObject;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ChiTietHoaDonDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetDataByID(string IDHoaDon)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", IDHoaDon)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_Select_ByID", param);
        }
        public DataTable GetDataByIDSanPham(string IDHoaDon, string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", IDHoaDon),
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_Select_ByIDSanPham", param);
        }
        public int Insert(ChiTietHoaDon obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", obj.IDHoaDon),
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("SoLuong", obj.SoLuong),
                new SqlParameter("DonGia", obj.DonGia),
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietHoaDon_Insert", param);
        }
        public int Delete(string IDHoaDon)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", IDHoaDon)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietHoaDon_Delete", param);
        }
        public int DeleteByIDSanPham(string IDHoaDon, string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", IDHoaDon),
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietHoaDon_Delete_ByIDSanPham", param);
        }
        public int UpdateQuantity(ChiTietHoaDon obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDHoaDon", obj.IDHoaDon),
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("SoLuong", obj.SoLuong)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietHoaDon_Update_Quantity", param);
        }

        public DataTable StatisticLN_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticLN_ByWeek");
        }
        public DataTable StatisticLN_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticLN_ByMonth");
        }
        public DataTable StatisticLN_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticLN_ByDate", param);
        }
        public DataTable StatisticNV_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticNV_ByWeek");
        }
        public DataTable StatisticNV_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticNV_ByMonth");
        }
        public DataTable StatisticNV_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticNV_ByDate", param);
        }
        public DataTable StatisticKH_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticKH_ByWeek");
        }
        public DataTable StatisticKH_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticKH_ByMonth");
        }
        public DataTable StatisticKH_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_StatisticKH_ByDate", param);
        }
        public DataTable KHStatisticLN_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_KHStatisticLN_ByWeek");
        }
        public DataTable KHStatisticLN_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_KHStatisticLN_ByMonth");
        }
        public DataTable KHStatisticLN_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietHoaDon_KHStatisticLN_ByDate", param);
        }
    }
}
