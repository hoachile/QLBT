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
    public partial class DsHdNhap : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;

        public DsHdNhap()
        {
            InitializeComponent();
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa ban ghi hien thoi?", "Xac nhan yeu cau", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                sql = "Delete from ct_nhap where mahd='" + txtMaHd.Text + "'";
                clsMain.DoSQL(sql);
                sql = "Delete from Hdnhap where mahd='" + txtMaHd.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);// xóa bản ghi hiện thời 
            }
            else
            {
                MessageBox.Show("Khong xoa dong", "Xac nhan yeu cau",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateHdNhap ub = new UpdateHdNhap();
            ub.txtMaHd.Text = this.grdData.CurrentRow.Cells[0].Value.ToString();
            ub.comNCC.Text = this.grdData.CurrentRow.Cells[1].Value.ToString();
            ub.comMaNV.Text = this.grdData.CurrentRow.Cells[2].Value.ToString();
            ub.dtpNgaynhap.Text = this.grdData.CurrentRow.Cells[3].Value.ToString();
            ub.txtTongtien.Text = this.grdData.CurrentRow.Cells[4].Value.ToString();
            ub.txtChietkhau.Text = this.grdData.CurrentRow.Cells[5].Value.ToString();
            ub.txtThanhtoan.Text = this.grdData.CurrentRow.Cells[6].Value.ToString();
            ub.txtHoten.Text = clsMain.GetFieldValues("Select Hoten from NhanVien where MaNV =N'" + ub.comMaNV.Text + "'");
            ub.txtTenNCC.Text = clsMain.GetFieldValues("Select Tenncc from nhacc where MaNCC =N'" + ub.comNCC.Text + "'");
            ub.txtSDT.Text = clsMain.GetFieldValues("Select SDT from nhacc where MaNCC =N'" + ub.comNCC.Text + "'");
            ub.txtDiachi.Text = clsMain.GetFieldValues("Select Diachi from nhacc where MaNCC =N'" + ub.comNCC.Text + "'");
            ub.Show();
            this.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            HdNhap f = new HdNhap();
            f.Show();
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void DsHdNhap_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select * from hdnhap";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }

        private void gb1_Enter(object sender, EventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            grdData.Enabled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void NapCT()
        {
            int i = grdData.CurrentRow.Index;
            //lấy số thứ tự dòng hiện thời
            txtMaHd.Text = grdData[0, i].Value.ToString();
            txtNhaCC.Text = grdData[1, i].Value.ToString();
            txtMaNV.Text = grdData[2, i].Value.ToString();
            txtNgaynhap.Text = grdData[3, i].Value.ToString();
            txtTongtien.Text = grdData[4, i].Value.ToString();
            txtChietkhau.Text = grdData[5, i].Value.ToString();
            txtThanhtoan.Text = grdData[6, i].Value.ToString();

        }
    }
}
