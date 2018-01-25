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

using  BusinessLogicLayer;

namespace QLShopHoa.BaoCao
{
    public partial class frmBCBanHangTheoThoiGianCT : DevExpress.XtraEditors.XtraForm
    {
        private bool _checkODau = false;
        HoaDonBUS bus = new HoaDonBUS();
        public string NgayLap { get; set; }
        public frmBCBanHangTheoThoiGianCT()
        {
            InitializeComponent();
        }

        private void frmBCBanHangTheoThoiGianCT_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void HienThi()
        {
            var dt = bus.ChiTietBanHangTheoThoiGian(NgayLap);
            msds.DataSource = dt;
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
    }
}