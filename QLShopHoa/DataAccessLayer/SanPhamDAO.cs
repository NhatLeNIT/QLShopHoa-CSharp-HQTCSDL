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
    public class SanPhamDAO
    {
        //DBConnect db = new DBConnect();
        public DataTable GetData()
        {
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_All", null);
        }
        public DataTable GetDataByStatus()
        {
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_ByStatus", null);
        }
        public DataTable GetDataByStatus_Quantity()
        {
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_ByStatus_Quantity", null);
        }
        public DataTable GetDataByID(string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_ByID", param);
        }

        public DataTable GetDataByID_Fix_DirtyRead(string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_ByID_Fix_DirtyRead", param);
        }

        public DataTable GetDataByID_Quantity(string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_ByID_Quantity", param);
        }
        public DataTable GetDataAll()
        {
            return DBConnect.Instance.GetDataTable("sp_SanPham_Select_AllData", null);
        }
        public int Insert(SanPham obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("TenSanPham", obj.TenSanPham),
                new SqlParameter("GiaVon", obj.GiaVon),
                new SqlParameter("GiaBan", obj.GiaBan),
                new SqlParameter("SoLuong", obj.SoLuong),
                new SqlParameter("Hinh", obj.Hinh),
                new SqlParameter("MoTa", obj.MoTa),
                new SqlParameter("TrangThai", obj.TrangThai),
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("IDLoaiHang", obj.IDLoaiHang),
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh),
                new SqlParameter("IDNhanVien", obj.IDNhanVien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_Insert", param);
        }
        public int Update(SanPham obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("TenSanPham", obj.TenSanPham),
                new SqlParameter("GiaVon", obj.GiaVon),
                new SqlParameter("GiaBan", obj.GiaBan),
                new SqlParameter("SoLuong", obj.SoLuong),
                new SqlParameter("Hinh", obj.Hinh),
                new SqlParameter("MoTa", obj.MoTa),
                new SqlParameter("TrangThai", obj.TrangThai),
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("IDLoaiHang", obj.IDLoaiHang),
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh),
                new SqlParameter("IDNhanVien", obj.IDNhanVien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_Update", param);
        }

        public int Update_DirtyRead(SanPham obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("TenSanPham", obj.TenSanPham),
                new SqlParameter("GiaVon", obj.GiaVon),
                new SqlParameter("GiaBan", obj.GiaBan),
                new SqlParameter("SoLuong", obj.SoLuong),
                new SqlParameter("Hinh", obj.Hinh),
                new SqlParameter("MoTa", obj.MoTa),
                new SqlParameter("TrangThai", obj.TrangThai),
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("IDLoaiHang", obj.IDLoaiHang),
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh),
                new SqlParameter("IDNhanVien", obj.IDNhanVien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_Update_DirtyRead", param);
        }

        public int Update_LostUpdate(SanPham obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("TenSanPham", obj.TenSanPham),
                new SqlParameter("GiaVon", obj.GiaVon),
                new SqlParameter("GiaBan", obj.GiaBan),
                new SqlParameter("SoLuong", obj.SoLuong),
                new SqlParameter("Hinh", obj.Hinh),
                new SqlParameter("MoTa", obj.MoTa),
                new SqlParameter("TrangThai", obj.TrangThai),
                new SqlParameter("IDNhaCungCap", obj.IDNhaCungCap),
                new SqlParameter("IDLoaiHang", obj.IDLoaiHang),
                new SqlParameter("IDDonViTinh", obj.IDDonViTinh),
                new SqlParameter("IDNhanVien", obj.IDNhanVien)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_Update_LostUpdate", param);
        }

        public int UpdateQuantity(SanPham obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("SoLuong", obj.SoLuong)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_UpdateQuantity", param);
        }
        public int UpdateQuantitySub(SanPham obj)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", obj.IDSanPham),
                new SqlParameter("SoLuong", obj.SoLuong)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_UpdateQuantitySub", param);
        }
        public int Delete(string IDSanPham)
        {
            SqlParameter[] param =
            {
                new SqlParameter("IDSanPham", IDSanPham)
            };
            return DBConnect.Instance.ExecuteSQL("sp_SanPham_Delete", param);
        }
    }
}
