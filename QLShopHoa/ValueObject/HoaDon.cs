using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueObject
{
    public class HoaDon
    {
        public string IDHoaDon { get; set; }
        public string NgayLap { get; set; }
        public string IDNhanVien { get; set; }
        public string IDKhachHang { get; set; }
        public string TenNguoiNhan { get; set; }
        public string DiaChiNguoiNhan { get; set; }
        public string DienThoaiNguoiNhan { get; set; }
        public string NgayGiao { get; set; }
        public int TrangThaiThanhToan { get; set; }
        public int TrangThaiGiaoHang { get; set; }
        public string GhiChu { get; set; }
        public int SoLuongSanPham { get; set; }
        public double TongTien { get; set; }
    }
}
