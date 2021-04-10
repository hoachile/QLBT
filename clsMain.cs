using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
namespace QLBT
{
    class clsMain
    {
        public static void DoSQL(string sql)
        {
            string strcn = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strcn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static string GetFieldValues(string sql)
        {
            string strcn = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strcn);
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
        public static DataTable GetDataToTable(string sql)
        {
            string constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();//Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;
        }
    }
}
