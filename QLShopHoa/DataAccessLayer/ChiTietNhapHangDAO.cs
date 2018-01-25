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
    public class ChiTietNhapHangDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetDataByID(string IDNhapHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", IDNhapHang)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_Select_ByID", param);
        }
        public DataTable GetDataByIDSanPham(string IDNhapHang, string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", IDNhapHang),
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_Select_ByIDSanPham", param);
        }
        public int Insert(ChiTietNhapHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", obj.IDNhapHang),
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("SoLuong", obj.SoLuong),
                new SqlParameter("DonGia", obj.DonGia),
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietNhapHang_Insert", param);
        }
        public int Delete(string IDNhapHang)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", IDNhapHang)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietNhapHang_Delete", param);
        }
        public int DeleteByIDSanPham(string IDNhapHang, string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", IDNhapHang),
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietNhapHang_Delete_ByIDSanPham", param);
        }
        public int UpdateQuantity(ChiTietNhapHang obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDNhapHang", obj.IDNhapHang),
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("SoLuong", obj.SoLuong)
            };
            return DBConnect.Instance.ExecuteSQL("sp_ChiTietNhapHang_Update_Quantity", param);
        }
        public DataTable StatisticNCC_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_StatisticNCC_ByWeek");
        }
        public DataTable StatisticNCC_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_StatisticNCC_ByMonth");
        }
        public DataTable StatisticNCC_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_StatisticNCC_ByDate", param);
        }
        public DataTable NCCStatistic_ByWeek()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_NCCStatistic_ByWeek");
        }
        public DataTable NCCStatistic_ByMonth()
        {
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_NCCStatistic_ByMonth");
        }
        public DataTable NCCStatistic_ByDate(string NgayDau, string NgayCuoi)
        {
            SqlParameter[] param =
            {
                new SqlParameter("NgayDau", NgayDau),
                new SqlParameter("NgayCuoi", NgayCuoi)
            };
            return DBConnect.Instance.GetDataTable("sp_ChiTietNhapHang_NCCStatistic_ByDate", param);
        }
    }
}
