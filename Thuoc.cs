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
    public partial class Thuoc : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;
        string tMathuoc, tTenthuoc, tSDK, tHoatcc, tHamluong, tHangsx, tQuycach, tDvt;

        public Thuoc()
        {
            InitializeComponent();
        }
        private void Thuoc_Load_1(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select * from Thuoc order by MaThuoc";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }

        private void Thuoc_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }


        private void grdData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }

        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, 0];
            NapCT();
        }

        private void btnPrv_Click_1(object sender, EventArgs e)
        {
            i = grdData.CurrentRow.Index;
            if (i > 0)
            {
                grdData.CurrentCell = grdData[0, i - 1];
            }
            NapCT();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            i = grdData.CurrentRow.Index;
            if (i < grdData.RowCount - 1)
            {
                grdData.CurrentCell = grdData[0, i + 1];
            }
            NapCT();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            NapCT();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (addnewFlag == false)
            {
                int n = grdData.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tMathuoc = grdData[0, i].Value.ToString();
                    tTenthuoc = grdData[1, i].Value.ToString();
                    tSDK = grdData[2, i].Value.ToString();
                    tHoatcc = grdData[3, i].Value.ToString();
                    tHamluong = grdData[4, i].Value.ToString();
                    tHangsx = grdData[5, i].Value.ToString();
                    tQuycach = grdData[6, i].Value.ToString();
                    tDvt = grdData[7, i].Value.ToString();
                    sql = "Update Thuoc set MaThuoc='" + tMathuoc + "',tenthuoc=N'" + tTenthuoc + "',SDK='" + tSDK
+ "',Hoatcc=N'" + tHoatcc + "',Hamluong=N'" + tHamluong + "',Hangsx=N'" + tHangsx +
"',Quycach=N'" + tQuycach + "',Dvt=N'" + tDvt + "' where Mathuoc='" + tMathuoc + "'";
                    clsMain.DoSQL(sql);
                    NapGrd();
                }
                MessageBox.Show("Da cap nhat thanh cong!");
                gb1.Enabled = false;
                btnUpdate.Enabled = false;

            }
            else
            {
                sql = "Insert into Thuoc Values('" + txtMathuoc.Text + "','" + txtTenthuoc.Text + "', '" + txtSDK.Text + "', '" +
                    txtHoatcc.Text + "', '" + txtHamluong.Text + "', '" + txtHangsx.Text + "', '" + txtQuycach.Text + "', '" + txtDvt.Text + "')";
                clsMain.DoSQL(sql);
                NapGrd();
                MessageBox.Show("Them moi thanh cong");
                addnewFlag = false;
                btnUpdate.Enabled = false;
            }
        }

        private void btnDel_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa ban ghi hien thoi?", "Xac nhan yeu cau", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                sql = "Delete from Thuoc where mathuoc='" + txtMathuoc.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);// xóa bản ghi hiện thời 
            }
            else
            {
                MessageBox.Show("Khong xoa dong", "Xac nhan yeu cau",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            txtMathuoc.Focus();
            addnewFlag = true;
            btnUpdate.Enabled = true;
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            gb1.Enabled = true;
            btnUpdate.Enabled = true;
            txtMathuoc.Enabled = false;
            txtTenthuoc.Focus();
            addnewFlag = false;
            NapGrd();
        }

        private void comTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = "Select Distinct " + comTruong.Text + " From Thuoc ";
            DataTable comtb = new DataTable();
            da = new SqlDataAdapter(sql, conn);
            //comtb.Clear();
            da.Fill(comtb);
            comGT.DataSource = comtb;
            comGT.DisplayMember = comTruong.Text;
            comGT.ValueMember = comTruong.Text;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {

            sql = "Select * from Thuoc where " + comTruong.Text + "=N'" + comGT.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
    }


        public void NapCT()
        {
            int i = grdData.CurrentRow.Index;
            txtMathuoc.Text = grdData.Rows[i].Cells["Mathuoc"].Value.ToString();
            txtTenthuoc.Text = grdData.Rows[i].Cells["Tenthuoc"].Value.ToString();
            txtSDK.Text = grdData.Rows[i].Cells["SDK"].Value.ToString();
            txtHoatcc.Text = grdData.Rows[i].Cells["Hoatcc"].Value.ToString();
            txtHamluong.Text = grdData.Rows[i].Cells["Hamluong"].Value.ToString();
            txtHangsx.Text = grdData.Rows[i].Cells["Hangsx"].Value.ToString();
            txtQuycach.Text = grdData.Rows[i].Cells["Quycach"].Value.ToString();
            txtDvt.Text = grdData.Rows[i].Cells["Dvt"].Value.ToString();
        }
        private void NapGrd()
        {
            int i = grdData.CurrentRow.Index;
            grdData[0, i].Value = txtMathuoc.Text;
            grdData[1, i].Value = txtTenthuoc.Text;
            grdData[2, i].Value = txtSDK.Text;
            grdData[3, i].Value = txtHoatcc.Text;
            grdData[4, i].Value = txtHamluong.Text;
            grdData[5, i].Value = txtHangsx.Text;
            grdData[6, i].Value = txtQuycach.Text;
            grdData[7, i].Value = txtDvt.Text;
        }
    }
}
