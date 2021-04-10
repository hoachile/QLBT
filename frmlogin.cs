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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=AD-PC\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("Select * from TaiKhoan where Username = '" + txtUsers.Text + "' and Password = '" + txtPass.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Giaodien gd = new Giaodien(dt.Rows[0][2].ToString());// Phân quyền
                gd.Show();

            }
            else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng, thử lại nhé!!");
                    txtPass.Clear();
                    txtUsers.Clear();
                    txtUsers.Focus();
                }
            
  
            if (CboRemember.Checked)
            {
                Properties.Settings.Default.Users = txtUsers.Text;
                Properties.Settings.Default.Password = txtPass.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Users = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }


        }

private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHienmk_Click(object sender, EventArgs e)
        {
            txtPass. UseSystemPasswordChar = true;
        }

        private void cboShowmk_CheckedChanged(object sender, EventArgs e)
        {
            if(cboShowmk.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
                txtPass.UseSystemPasswordChar = true;
        }

        private void frmlogin_Load_1(object sender, EventArgs e)
        {
            txtUsers.Text = Properties.Settings.Default.Users;
            txtPass.Text = Properties.Settings.Default.Password;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
