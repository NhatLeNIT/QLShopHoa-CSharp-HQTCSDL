using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueObject
{
    public class NhapHang
    {
        public string IDNhapHang { get; set; }
        public string NgayNhap { get; set; }
        public string IDNhaCungCap { get; set; }
        public string IDNhanVien { get; set; }
        public string GhiChu { get; set; }
        public int SoLuongSanPham { get; set; }
        public double TongTien { get; set; }
    }
}
