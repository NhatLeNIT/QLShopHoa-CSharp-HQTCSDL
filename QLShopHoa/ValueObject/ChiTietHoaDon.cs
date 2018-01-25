using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueObject
{
    public class ChiTietHoaDon
    {
        public string IDHoaDon { get; set; }
        public string IDSanPham { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public int IDDonViTinh { get; set; }
    }
}
