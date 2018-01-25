using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using BusinessLogicLayer;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCNhaCungCapTheoNhapHangCT : DevExpress.XtraEditors.XtraForm
    {
        private bool _checkODau = false;
        public string IDNhaCungCap { get; set; }
        public int CheckThoiGian { get; set; }
        public string NgayDau { get; set; }
        public string NgayCuoi { get; set; }
        NhapHangBUS bus = new NhapHangBUS();
        public frmBCNhaCungCapTheoNhapHangCT()
        {
            InitializeComponent();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn1)
            {
                if (_checkODau)
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
            if (!_checkODau) _checkODau = true;
        }

        private void frmBCNhaCungCapTheoNhapHangCT_Load(object sender, EventArgs e)
        {
            HienThi();
        }
        private void HienThi()
        {
            DataTable dt = new DataTable();
            if (CheckThoiGian == 1)
                dt = bus.ChiTietNCCTheoNhapHang_Tuan(IDNhaCungCap);
            else if (CheckThoiGian == 2)
                dt = bus.ChiTietSanPhamTheoNCC_Thang(IDNhaCungCap);
            else dt = bus.ChiTietNCCTheoNhapHang_Ngay(IDNhaCungCap, NgayDau, NgayCuoi);
            msds.DataSource = dt;
        }
    }
}