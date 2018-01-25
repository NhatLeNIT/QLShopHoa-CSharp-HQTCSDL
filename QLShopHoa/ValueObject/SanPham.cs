using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueObject
{
    public class SanPham
    {
        public string IDSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double GiaVon { get; set; }
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public byte[] Hinh { get; set; }
        public string MoTa { get; set; }
        public int TrangThai { get; set; }
        public string IDNhaCungCap { get; set; }
        public int IDLoaiHang { get; set; }
        public int IDDonViTinh { get; set; }
        public string IDNhanVien { get; set; }
        public string TenDonViTinh { get; set; }
    }
}
