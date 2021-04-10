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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;
        string tId, tho, tten, ttuoi;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select * from abc";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            txtId.Focus();
            addnewFlag = true;
            btnUpdate.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
         
            btnUpdate.Enabled = true;
            txtId.Enabled = false;
            txtho.Focus();
           addnewFlag = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa ban ghi hien thoi?", "Xac nhan yeu cau", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                sql = "Delete from abc where id='" + txtId.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);// xóa bản ghi hiện thời 
            }
            else
            {
                MessageBox.Show("Khong xoa dong", "Xac nhan yeu cau",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewFlag == false)
            {
                int n = grdData.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tId = grdData.Rows[i].Cells["Id"].Value.ToString();
                    tho = grdData.Rows[i].Cells["ho"].Value.ToString();
                    tten = grdData.Rows[i].Cells["ten"].Value.ToString();
                    ttuoi = grdData.Rows[i].Cells["tuoi"].Value.ToString();
                    sql = "Update abc set Id='" + tId + "',Ho=N'" + tho+ "',ten=N'" + tten + "',tuoi=N'" + ttuoi + "' where id='" + tId + "'";
                    clsMain.DoSQL(sql);
                    NapGrd();

                }
                MessageBox.Show("Da cap nhat thanh cong!");
                gb1.Enabled = false;
                btnUpdate.Enabled = false;

            }
            else
            {
                sql = "Insert into abc Values('" + txtId.Text + "','" + txtho.Text + "', '" +
                   txtten.Text + "', '" + txttuoi.Text + "')";
                clsMain.DoSQL(sql);
                NapGrd();
                MessageBox.Show("Them moi thanh cong");
                addnewFlag = false;
                btnUpdate.Enabled = false;
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
                //lấy số thứ tự dòng hiện thời
            txtId.Text = grdData.Rows[index].Cells["Id"].Value.ToString();
            txtho.Text = grdData.Rows[index].Cells["ho"].Value.ToString();
            txtten.Text = grdData.Rows[index].Cells["ten"].Value.ToString();
            txttuoi.Text = grdData.Rows[index].Cells["tuoi"].Value.ToString();
        }

        public void NapCT()
        {
            if (grdData.Rows.Count== 1)
            {
                txtId.Text = "";
                txtho.Text = "";
                txtten.Text = "";
                txttuoi.Text = "";
            }
            else
            {
                int i = grdData.CurrentRow.Index;
                //lấy số thứ tự dòng hiện thời
                txtId.Text = grdData[0, i].Value.ToString();
                txtho.Text = grdData[1, i].Value.ToString();
                txtten.Text = grdData[2, i].Value.ToString();
                txttuoi.Text = grdData[3, i].Value.ToString();
            }
        }


        private void NapGrd()
        {
            int i = grdData.CurrentRow.Index;
            grdData[0, i].Value = txtId.Text;
            grdData[1, i].Value = txtho.Text;
            grdData[2, i].Value = txtten.Text;
            grdData[3, i].Value = txttuoi.Text;
        }

    }
}

