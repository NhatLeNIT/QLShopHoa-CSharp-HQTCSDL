using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using ValueObject;
using System.Data;

namespace BusinessLogicLayer
{
    public class DonViTinhBUS
    {
        DonViTinhDAO dao = new DonViTinhDAO();
        public DataTable GetData()
        {
            return dao.GetData();
        }

        public DataTable GetData_UnRepeatable(int IDDonViTinh)
        {
            return dao.GetData_UnRepeatable(IDDonViTinh);
        }

        public DataTable GetData_Fix_UnRepeatable(int IDDonViTinh)
        {
            return dao.GetData_Fix_UnRepeatable(IDDonViTinh);
        }

        public DataTable GetData_Phantom()
        {
            return dao.GetData_Phantom();
        }

        public DataTable GetData_Fix_Phantom()
        {
            return dao.GetData_Fix_Phantom();
        }

        public DataTable GetDataByID(int IDDonViTinh)
        {
            return dao.GetDataByID(IDDonViTinh);
        }
        public int Insert(DonViTinh obj)
        {
            return dao.Insert(obj);
        }
        public int Update(DonViTinh obj)
        {
            return dao.Update(obj);
        }
        public int Delete(int IDDonViTinh)
        {
            return dao.Delete(IDDonViTinh);
        }
    }
}
