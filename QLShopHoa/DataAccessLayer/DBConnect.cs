using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataAccessLayer
{
    class DBConnect
    {
        private static DBConnect instance;
        private SqlConnection conn;

        internal static DBConnect Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBConnect();
                return DBConnect.instance;
            }

            private set
            {
                DBConnect.instance = value;
            }
        }

        private DBConnect()
        {
            conn = new SqlConnection(DataAccessLayer.Properties.Settings.Default.strConnectDAO);
        }

        public void CloseConnect()
        {
            conn.Close();
        }
        public DataTable GetDataTable(string strSQL) // select
        {
            DataTable data = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQL, conn);
            conn.Open();
            dataAdapter.Fill(data);
            conn.Close();
            return data;
        }
        public DataTable GetDataTable(string procName, SqlParameter[] param)
        {
            DataTable data = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            if (param != null)
                cmd.Parameters.AddRange(param);
            cmd.Connection = conn;
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;
            conn.Open();
            dataAdapter.Fill(data);
            conn.Close();
            return data;
        }
        public int ExecuteSQL(string strSQL)
        {
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            int row = cmd.ExecuteNonQuery();
            conn.Close();
            return row;
        }
        public int ExecuteSQL(string procName, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            if (param != null)
                cmd.Parameters.AddRange(param);
            conn.Open();
            int row;

            try
            {
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                row = -1;
            }
            conn.Close();
            return row;
        }
    }
}
