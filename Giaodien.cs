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
    public partial class Giaodien : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dtreport = new DataTable();
        string constr, sql;
        SqlCommand cmd = new SqlCommand();
        string Username = "", Password = "", Quyen = "";// Phân quyền

        private void BaocaoTonghop_Click(object sender, EventArgs e)
        {
            if(Quyen == "admin")
            {
                frmInBCTonghop f1 = new QLBT.frmInBCTonghop();
                f1.Show();
            }
            else
            {
                MessageBox.Show("Bạn ko được xem báo cáo nhé!");
            }

    }

    private void Giaodien_Load(object sender, EventArgs e)
        {

        }

        private void accordionControlElement54_Click(object sender, EventArgs e)
        {
            Khachhang f2 = new Khachhang();
            f2.Show();
        }

        private void accordionControlElement55_Click(object sender, EventArgs e)
        {
            NhaCC f2 = new NhaCC();
            f2.Show();
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            DsHdNhap f3 = new DsHdNhap();
            f3.Show();
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            HdNhap f4 = new HdNhap();
            f4.Show();
        }

        private void accordionControlElement58_Click(object sender, EventArgs e)
        {
            DsHdban f5 = new DsHdban();
            f5.Show();
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            HdBan f6 = new HdBan();
            f6.Show();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            this.Close();
            frmlogin f = new frmlogin();
            f.Show();
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
           /* sql = "select MaThuoc, Tenthuoc, Soluong from Thuoc where MaThuoc = N'" + comMaThuoc.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            dtreport.Clear();
            da.Fill(dtreport);
            rptBcHangton rpt = new QLBT.rptBcHangton();
            rpt.SetDataSource(dtreport);
            // truyền tên nhóm hàng vào biến
            // truyền rpt vaò form để in ra
            frmBaocaoHangtonViewercs f = new frmBaocaoHangtonViewercs(rpt);
            f.Show();*/
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {

        }

        public Giaodien(string Q)
        {
            InitializeComponent();
            this.Quyen = Q.Trim ();// phân quyền 
        }

      
        private void Nhanvien_Click_1(object sender, EventArgs e)
        {
            if (Quyen == "admin")// Phân quyền
            {
                NhanVien f7 = new NhanVien();
                f7.Show();
            }
            else
            {

                MessageBox.Show("Bạn ko có quyền nhé!");

            }
        }




        private void accordionControlElement53_Click(object sender, EventArgs e)
        {
            Thuoc f8 = new QLBT.Thuoc();
            f8.Show();
        }

    }
}
