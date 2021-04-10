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
    public partial class NhanVien : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;
        string tMaNV, tHoten, tNgaysinh, tSDT, tDiachi;

        public NhanVien()
        {
            InitializeComponent();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select * from Nhanvien order by MaNV";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
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

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            i = grdData.CurrentRow.Index;
            if (i < grdData.RowCount - 1)
            {
                grdData.CurrentCell = grdData[0, i + 1];
            }
            NapCT();
        }

        private void btnLast_Click_1(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            NapCT();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            txtMaNV.Focus();
            addnewFlag = true;
            btnUpdate.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            gb1.Enabled = true;
            btnUpdate.Enabled = true;
            txtMaNV.Enabled = false;
            txtHoten.Focus();
            //addnewFlag = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa ban ghi hien thoi?", "Xac nhan yeu cau", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                sql = "Delete from Nhanvien where manv='" + txtMaNV.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);// xóa bản ghi hiện thời 
            }
            else
            {
                MessageBox.Show("Khong xoa dong", "Xac nhan yeu cau",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grdData_MouseUp_1(object sender, MouseEventArgs e)
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
                    tMaNV = grdData[0, i].Value.ToString();
                    tHoten = grdData[1, i].Value.ToString();
                    tNgaysinh = grdData[2, i].Value.ToString();
                    tSDT = grdData[3, i].Value.ToString();
                    tDiachi = grdData[4, i].Value.ToString();
                    sql = "Update Nhanvien set MaNV='" + tMaNV + "',Hoten=N'" + tHoten + "',Ngaysinh='" + tNgaysinh
+ "',SDT=N'" + tSDT + "',Diachi=N'" + tDiachi + "' where MaNV='" + tMaNV + "'";
                    clsMain.DoSQL(sql);
                    NapGrd();

                }
                MessageBox.Show("Da cap nhat thanh cong!");
                gb1.Enabled = false;
                btnUpdate.Enabled = false;

            }
            else
            {
                sql = "Insert into Nhanvien Values('" + txtMaNV.Text + "','" + txtHoten.Text + "', '" + txtNgaysinh.Text + "', '" +
                    txtSDT.Text + "', '" + txtDiachi.Text +  "')";
                clsMain.DoSQL(sql);
                grdData.Refresh();
                sql = "Select * from Nhanvien order by MaNV";
                grdData.DataSource = clsMain.GetDataToTable(sql);
                MessageBox.Show("Them moi thanh cong");
                addnewFlag = false;
                btnUpdate.Enabled = false;
            }
        }

        private void comTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = "Select Distinct " + comTruong.Text + " From Nhanvien ";
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
            sql = "Select * from Nhanvien where " + comTruong.Text + "=N'" + comGT.Text + "'";
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
            //lấy số thứ tự dòng hiện thời
            txtMaNV.Text = grdData[0, i].Value.ToString();
            txtHoten.Text = grdData[1, i].Value.ToString();
            txtNgaysinh.Text = grdData[2, i].Value.ToString();
            txtSDT.Text = grdData[3, i].Value.ToString();
            txtDiachi.Text = grdData[4, i].Value.ToString();

        }
        private void NapGrd()
        {
            int i = grdData.CurrentRow.Index;
            grdData[0, i].Value = txtMaNV.Text;
            grdData[1, i].Value = txtHoten.Text;
            grdData[2, i].Value = txtNgaysinh.Text;
            grdData[3, i].Value = txtSDT.Text;
            grdData[4, i].Value = txtDiachi.Text;

        }


    }
}
