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
    public partial class HdBan : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dtreport = new DataTable();
        SqlCommand cmd = new SqlCommand();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;
        string tMaThuoc, tTenthuoc, tSoluong, tGiaban, tThanhtien, tGhichu;


        public HdBan()
        {
            InitializeComponent();
            fillcombobox();
            fillcombobox1();
            fillcombobox2();
        }
        private void HdBan_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select a.Mathuoc, Tenthuoc, soluong, c.giaban,  thanhtien, ghichu from CT_Ban a join Hdban b on a.Mahd = b.Mahd join thuoc c on a.MaThuoc = c.MaThuoc  where a.mahd='" + txtMaHd.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }

        public void fillcombobox()
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from thuoc";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string mt = rd.GetString(0);
                    comMaThuoc.Items.Add(mt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void fillcombobox1()
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from nhanvien";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string mnv = rd.GetString(0);
                    comMaNV.Items.Add(mnv);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void fillcombobox2()
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from khachhang";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string mkh = rd.GetString(0);
                    comKH.Items.Add(mkh);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTongtien_TextChanged(object sender, EventArgs e)
        {
            double tt, tongtien, ck;
            if (txtTongtien.Text == "")
                tongtien = 0;
            else
                tongtien = Convert.ToDouble(txtTongtien.Text);
            if (txtChietkhau.Text == "")
                ck = 0;
            else
                ck = Convert.ToDouble(txtChietkhau.Text);
            tt = tongtien - tongtien * ck / 100;
            txtThanhtoan.Text = tt.ToString();
            sql = "Update Hdban set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            gb1.Enabled = false;
            btnUpdate.Enabled = true;
            txtMaHd.Enabled = false;
            comMaThuoc.Focus();
            addnewFlag = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdData.CurrentCell = grdData[0, grdData.RowCount - 1];
            comMaThuoc.Focus();
            addnewFlag = true;
            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double sl, slton, sum, tongmoi;
            if (addnewFlag == false)
            {
                int n = grdData.RowCount - 1;
                for (i = 0; i < n; i++)
                {
                    tMaThuoc = grdData.Rows[i].Cells["MaThuoc"].Value.ToString();
                    tTenthuoc = grdData.Rows[i].Cells["Tenthuoc"].Value.ToString();
                    tSoluong = grdData.Rows[i].Cells["Soluong"].Value.ToString();
                    tGiaban= grdData.Rows[i].Cells["Giaban"].Value.ToString();
                    tThanhtien = grdData.Rows[i].Cells["Thanhtien"].Value.ToString();
                    tGhichu = grdData.Rows[i].Cells["Ghichu"].Value.ToString();

                    sql = "Update CT_Ban Soluong='" + tSoluong + "',Giaban=N'" + tGiaban + "',Thanhtien='" + 
                        tThanhtien + "',Ghichu=N'" + tGhichu + "' where MaHd='" + txtMaHd.Text + "' and mathuoc='" + comMaThuoc.Text + "'";
                    clsMain.DoSQL(sql);
                    NapGrd();
                }
                MessageBox.Show("Da cap nhat thanh cong!");
                gb1.Enabled = false;
                btnUpdate.Enabled = false;

            }
            else
            {
                
                sql = "Insert into CT_Ban values ('" + txtMaHd.Text + "', '" + comMaThuoc.Text + "', '" + txtSoluong.Text
    + "','" + txtGiaban.Text + "','"  + txtThanhtien.Text + "','" + txtGhichu.Text + "')";
                clsMain.DoSQL(sql);
                grdData.Refresh();
                sql = "Select a.Mathuoc, Tenthuoc, soluong, c.giaban, thanhtien, ghichu from CT_Ban a join Hdban b on a.Mahd = b.Mahd join thuoc c on a.MaThuoc = c.MaThuoc  where a.mahd='" + txtMaHd.Text + "'";
                grdData.DataSource = clsMain.GetDataToTable(sql);
                MessageBox.Show("Them moi thanh cong");
                addnewFlag = false;
                btnUpdate.Enabled = false;
                gb1.Enabled = false;
            }

            sl = Convert.ToInt32(clsMain.GetFieldValues("SELECT SoLuongton FROM Thuoc WHERE Mathuoc = N'" + comMaThuoc.Text + "'"));
            slton = sl - Convert.ToInt32(txtSoluong.Text);
            sql = "UPDATE Thuoc SET SoLuongton =" + slton + " WHERE Mathuoc= N'" + comMaThuoc.Text + "'";
            clsMain.DoSQL(sql);
            sum = Convert.ToDouble(clsMain.GetFieldValues("SELECT sum(thanhtien) FROM ct_ban WHERE Mahd ='" + txtMaHd.Text + "'"));
            txtTongtien.Text = sum.ToString();
            sql = "Update Hdban set Tongtien = " + txtTongtien.Text + ", Chietkhau = '"
                + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
            //tongmoi = sum + Convert.ToDouble(txtThanhtien.Text);
            //txtTongtien.Text = tongmoi.ToString();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            double thanhtienxoa, sum, tongmoi;
            if (MessageBox.Show("Ban co muon xoa ban ghi hien thoi?", "Xac nhan yeu cau", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (grdData.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string mathuocxoa = grdData.CurrentRow.Cells["MaThuoc"].Value.ToString();
                int slxoa = Convert.ToInt32(grdData.CurrentRow.Cells["Soluong"].Value.ToString());
                // thanhtienxoa = Convert.ToDouble(grdData.CurrentRow.Cells["Thanhtien"].Value.ToString());
                sql = "Delete from Ct_ban where mahd='" + txtMaHd.Text + "' and mathuoc= '" + comMaThuoc.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);
                // Cập nhật lại số lượng cho các mặt hàng
                int sl = Convert.ToInt32(clsMain.GetFieldValues("SELECT SoLuongton FROM Thuoc WHERE MaThuoc = N'" + comMaThuoc.Text + "'"));
                int slcon = sl - slxoa;
                sql = "UPDATE Thuoc SET Soluongton =" + slcon + " WHERE MaThuoc= N'" + mathuocxoa + "'";
                clsMain.DoSQL(sql); ;
                sum = Convert.ToDouble(clsMain.GetFieldValues("SELECT sum(thanhtien) FROM ct_ban WHERE Mahd ='" + txtMaHd.Text + "'"));
                //tongmoi = sum - thanhtienxoa;
                txtTongtien.Text = sum.ToString();
                sql = "Update Hdban set Tongtien = " + txtTongtien.Text + ", Chietkhau = '"
                    + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
                clsMain.DoSQL(sql);
            }
            else
            {
                MessageBox.Show("Khong xoa dong", "Xac nhan yeu cau",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtChietkhau_TextChanged(object sender, EventArgs e)
        {
            double tt, tongtien, ck;
            if (txtChietkhau.Text == "")
                ck = 0;
            else
                ck = Convert.ToDouble(txtChietkhau.Text);
            tongtien = Convert.ToDouble(txtTongtien.Text);
            tt = tongtien - tongtien * ck / 100;
            txtThanhtoan.Text = tt.ToString();
            sql = "Update Hdban set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
        }

        private void btnAddKH_Click(object sender, EventArgs e)
        {
            Khachhang f = new Khachhang();
            f.Show();
        }
        private void comKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from khachhang where makh='" + comKH.Text + "'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string ht = rd.GetString(1);
                    txtHt.Text = ht;
                    string sdt = rd.GetString(2);
                    txtSDT.Text = sdt;
                    string dc = rd.GetString(3);
                    txtDiachi.Text = dc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddHd_Click(object sender, EventArgs e)
        {
            sql = "Insert into Hdban values ('" + txtMaHd.Text + "','" + comKH.Text + "','" + comMaNV.Text + "','" +
 dtpNgayban.Text + "','" + txtTongtien.Text + "','" + txtChietkhau.Text + "','" + txtThanhtoan.Text + "')";
            clsMain.DoSQL(sql);
            MessageBox.Show("Them moi hoa don thanh cong");
            btnUpdate.Enabled = false;
            comMaThuoc.Text = "";
            txtTenthuoc.Text = "";
            txtSoluong.Text = "";
            txtGiaban.Text = "";
            txtThanhtien.Text = "";
            txtGhichu.Text = "";
        }

        private void comMaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from thuoc where mathuoc='" + comMaThuoc.Text + "'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string tt = rd.GetString(1);
                    txtTenthuoc.Text = tt;
                    double gb = rd.GetDouble(10);
                    txtGiaban.Text = gb.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txtGiaban_TextChanged(object sender, EventArgs e)
        {

            double thanhtien, sl, gb;
            if (txtSoluong.Text == "")
                sl = 0;
            else
            {
                sl = Convert.ToDouble(txtSoluong.Text);
            }
            if (txtGiaban.Text == "")
                gb = 0;
            else
                gb = Convert.ToDouble(txtGiaban.Text);
            thanhtien = sl * gb;
            txtThanhtien.Text = thanhtien.ToString();
        }

        private void txtSoluong_TextChanged_1(object sender, EventArgs e)
        {
            double thanhtien, sl, gb;
            if (txtSoluong.Text == "")
                sl = 0;
            else
            {
                sl = Convert.ToDouble(txtSoluong.Text);
                if (sl > Convert.ToInt32(clsMain.GetFieldValues("SELECT soluongton FROM thuoc WHERE Mathuoc ='" + comMaThuoc.Text + "'")))
                { MessageBox.Show("Sô lượng thuốc chỉ còn : "+ Convert.ToString(clsMain.GetFieldValues("SELECT soluongton FROM thuoc WHERE Mathuoc ='" + comMaThuoc.Text + "'")));
                    txtSoluong.Text = ""; }
            }
            if (txtGiaban.Text == "")
                gb = 0;
            else
                gb = Convert.ToDouble(txtGiaban.Text);
            thanhtien = sl * gb;
            txtThanhtien.Text = thanhtien.ToString();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void comMaNV_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from nhanvien where manv='" + comMaNV.Text + "'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string ht = rd.GetString(1);
                    txtHoten.Text = ht;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            sql = sql = "Update HdBan set MaHd = '" + txtMaHd.Text + "', Makh = '" + comKH.Text + "', manv = '" + comMaNV.Text
+ "',ngayBan='" + dtpNgayban.Text + "',tongtien='" + txtTongtien.Text + "',Chietkhau='" + txtChietkhau.Text + "',Thanhtoan='" + txtThanhtoan.Text + "' where MaHd='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
            MessageBox.Show("Hóa đơn đã được lưu");
        }

        private void btnInhd_Click(object sender, EventArgs e)
        {
            sql = "select Tenthuoc,soluong,a.Giaban,ThanhTien from Thuoc b join CT_Ban a on a.MaThuoc = b.MaThuoc where mahd = '" + txtMaHd.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            dtreport.Clear();
            da.Fill(dtreport);
            rptHdBan rpt = new QLBT.rptHdBan();
            rpt.SetDataSource(dtreport);
            // truyền tên nhóm hàng vào biến
            rpt.DataDefinition.FormulaFields["MaHd"].Text = "'" + txtMaHd.Text + "'";
            rpt.DataDefinition.FormulaFields["MaKH"].Text = "'" + comKH.Text + "'";
            rpt.DataDefinition.FormulaFields["Hoten"].Text = "'" + txtHoten.Text + "'";
            rpt.DataDefinition.FormulaFields["SDT"].Text = "'" + txtSDT.Text + "'";
            rpt.DataDefinition.FormulaFields["Diachi"].Text = "'" + txtDiachi.Text + "'";
           
            // truyền rpt vaò form để in ra
            frmBaocaoBanViewer f = new frmBaocaoBanViewer(rpt);
            f.Show();
        }

        private void ResetValues()
        {
            txtMaHd.Text = "";
            comMaNV.Text = "";
            comKH.Text = "";
            txtThanhtien.Text = "";
            txtTongtien.Text = "";
            comMaThuoc.Text = "";
            txtSoluong.Text = "";
            txtChietkhau.Text = "";
           
        }
        public void NapCT()
        {
            if (grdData.Rows.Count == 1)
            {
                comMaThuoc.Text = "";
                txtTenthuoc.Text = "";
                txtSoluong.Text = "";
                txtGiaban.Text = "";
                txtThanhtien.Text = "";
                txtGhichu.Text = "";
            }
            int i = grdData.CurrentRow.Index;
            comMaThuoc.Text = grdData.Rows[i].Cells["MaThuoc"].Value.ToString();
            txtTenthuoc.Text = grdData.Rows[i].Cells["Tenthuoc"].Value.ToString();
            txtSoluong.Text = grdData.Rows[i].Cells["Soluong"].Value.ToString();
            txtGiaban.Text = grdData.Rows[i].Cells["Giaban"].Value.ToString();
            txtThanhtien.Text = grdData.Rows[i].Cells["Thanhtien"].Value.ToString();
            txtGhichu.Text = grdData.Rows[i].Cells["Ghichu"].Value.ToString();
        }
        private void NapGrd()
        {
            if (grdData.Rows.Count == 1)
            {
                grdData.Rows[0].Cells["MaThuoc"].Value = "";
                grdData.Rows[0].Cells["Tenthuoc"].Value = "";
                grdData.Rows[0].Cells["Soluong"].Value = "";
                grdData.Rows[0].Cells["Giaban"].Value = "";
                grdData.Rows[0].Cells["Thanhtien"].Value = "";
                grdData.Rows[0].Cells["Ghichu"].Value = "";
            }
            int i = grdData.CurrentRow.Index;
            grdData.Rows[i].Cells["MaThuoc"].Value = comMaThuoc.Text;
            grdData.Rows[i].Cells["Tenthuoc"].Value = txtTenthuoc.Text;
            grdData.Rows[i].Cells["Soluong"].Value = txtSoluong.Text;
            grdData.Rows[i].Cells["Giaban"].Value = txtGiaban.Text;
            grdData.Rows[i].Cells["Thanhtien"].Value = txtThanhtien.Text;
            grdData.Rows[i].Cells["Ghichu"].Value = txtGhichu.Text;

        }

    }
    }
