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

namespace QLShopHoa
{
    public partial class Dashboard : DevExpress.XtraEditors.XtraForm
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        QuerySQLBUS query = new QuerySQLBUS();
        private void Dashboard_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 8 TenSanPham,  SUM(ChiTietHoaDon.SoLuong) AS SoLuong FROM ChiTietHoaDon, SanPham WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham";
            DataTable dt = query.GetDataBySQL(sql);
            BieuDo.DataSource = dt;
            BieuDo.ChartAreas["ChartArea1"].AxisX.Title="Sản phẩm";
            BieuDo.ChartAreas["ChartArea1"].AxisY.Title = "Số lượng";
            BieuDo.Series["Series1"].XValueMember = "TenSanPham";
            BieuDo.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}