using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QLBT
{
    public partial class NhaCC : Form
    {
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string constr, sql;
            int i;
            Boolean addnewFlag = false;
            string tMaNCC, tTenNCC, tSDT, tDiachi;

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            txtMaNCC.Focus();
            addnewFlag = true;
            btnUpdate.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            gb1.Enabled = true;
            btnUpdate.Enabled = true;
            txtMaNCC.Enabled = false;
            txtTenNCC.Focus();
            addnewFlag = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa ban ghi hien thoi?", "Xac nhan yeu cau", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                sql = "Delete from NhaCC where maNCC='" + txtMaNCC.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);// xóa bản ghi hiện thời 
            }
            else
            {
                MessageBox.Show("Khong xoa dong", "Xac nhan yeu cau",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewFlag == false)
            {
                int n = grdData.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tMaNCC = grdData.Rows[i].Cells["MaNCC"].Value.ToString();
                    tTenNCC = grdData.Rows[i].Cells["TenNCC"].Value.ToString();
                    tSDT = grdData.Rows[i].Cells["SDT"].Value.ToString();
                    tDiachi = grdData.Rows[i].Cells["Diachi"].Value.ToString();
                    sql = "Update NhaCC set MaNCC='" + tMaNCC + "',TenNCC=N'" + tTenNCC
+ "',SDT=N'" + tSDT + "',Diachi=N'" + tDiachi + "' where MaNCC='" + tMaNCC + "'";
                    clsMain.DoSQL(sql);
                    NapGrd();

                }
                MessageBox.Show("Da cap nhat thanh cong!");
                gb1.Enabled = false;
                btnUpdate.Enabled = false;

            }
            else
            {
                sql = "Insert into NhaCC Values('" + txtMaNCC.Text + "','" + txtTenNCC.Text + "', '" +
                    txtSDT.Text + "', '" + txtDiachi.Text + "')";
                clsMain.DoSQL(sql);
                NapGrd();
                MessageBox.Show("Them moi thanh cong");
                addnewFlag = false;
                btnUpdate.Enabled = false;
            }
        }

        public NhaCC()
        {
            InitializeComponent();
        }

        private void NhaCC_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select * from NhaCC order by MaNCC";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }
        public void NapCT()
        {
            int i = grdData.CurrentRow.Index;
            //lấy số thứ tự dòng hiện thời
            txtMaNCC.Text = grdData[0, i].Value.ToString();
            txtTenNCC.Text = grdData[1, i].Value.ToString();
            txtSDT.Text = grdData[2, i].Value.ToString();
            txtDiachi.Text = grdData[3, i].Value.ToString();

        }
        private void NapGrd()
        {
            int i = grdData.CurrentRow.Index;
            grdData[0, i].Value = txtMaNCC.Text;
            grdData[1, i].Value = txtTenNCC.Text;
            grdData[2, i].Value = txtSDT.Text;
            grdData[3, i].Value = txtDiachi.Text;

        }
    }
}
