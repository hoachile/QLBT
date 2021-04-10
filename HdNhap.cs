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
    public partial class HdNhap : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        DataTable dtreport = new DataTable();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;
        string tMaThuoc, tTenthuoc, tSoluong, tGianhap, tNsx, tHsd, tThanhtien, tGhichu;

        public HdNhap()
        {
            InitializeComponent();
            fillcombobox();
            fillcombobox1();
            fillcombobox2();
        }

        private void HdNhap_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select a.Mathuoc, Tenthuoc, soluong, a.gianhap, nsx, hsd, thanhtien, ghichu from CT_Nhap a join Hdnhap b on a.Mahd = b.Mahd join thuoc c on a.MaThuoc = c.MaThuoc  where a.mahd='" + txtMaHd.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
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
            sql = "Select*from nhacc";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string ncc = rd.GetString(0);
                    comNCC.Items.Add(ncc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from nhacc where mancc='" + comNCC.Text + "'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string tenncc = rd.GetString(1);
                    txtTenNCC.Text = tenncc;
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

        private void comMaNV_SelectedIndexChanged(object sender, EventArgs e)
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

        private void txtGianhap_TextChanged(object sender, EventArgs e)
        {
            
            double thanhtien, sl, gn;
            if (txtSoluong.Text == "")
                sl = 0;
            else
            {
                sl = Convert.ToDouble(txtSoluong.Text);
            }
            if (txtGianhap.Text == "")
                gn = 0;
            else
                gn = Convert.ToDouble(txtGianhap.Text);
            thanhtien = sl * gn;
            txtThanhtien.Text = thanhtien.ToString();
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            double thanhtien, sl, gn;
            if (txtSoluong.Text == "")
                sl = 0;
            else
            {
                sl = Convert.ToDouble(txtSoluong.Text);
            }
            if (txtGianhap.Text == "")
                gn = 0;
            else
                gn = Convert.ToDouble(txtGianhap.Text);
            thanhtien = sl * gn;
            txtThanhtien.Text = thanhtien.ToString();
        }

        private void btnAddHd_Click(object sender, EventArgs e)
        {
            sql = "Insert into Hdnhap values ('" + txtMaHd.Text + "','" + comNCC.Text + "','" + comMaNV.Text + "','" +
 dtpNgaynhap.Text + "','" + txtTongtien.Text + "','" + txtChietkhau.Text + "','" + txtThanhtoan.Text + "')";
            clsMain.DoSQL(sql);
            MessageBox.Show("Them moi hoa don thanh cong");
            btnUpdate.Enabled = false;
            comMaThuoc.Text = "";
            txtTenthuoc.Text = "";
            txtSoluong.Text = "";
            txtGianhap.Text = "";
            dtpNsx.Text = "";
            txtHsd.Text = "";
            txtThanhtien.Text = "";
            txtGhichu.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sql = "Update Hdnhap set MaHd = '" + txtMaHd.Text + "', MaNCC = '" + comNCC.Text + "', manv = '" + comMaNV.Text
+ "',ngaynhap='" + dtpNgaynhap.Text + "',tongtien='" + txtTongtien.Text + "',Chietkhau='" + txtChietkhau.Text + "',Thanhtoan='" + txtThanhtoan.Text + "' where MaHd='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
            MessageBox.Show("Hóa đơn đã được lưu");
            sql = "update thuoc set gianhap = a.avg_gianhap from thuoc join (select mathuoc, avg(gianhap) as avg_gianhap from ct_nhap group by mathuoc) a on thuoc.mathuoc = a.mathuoc";
            clsMain.DoSQL(sql);
            sql = "update thuoc set giaban = gianhap * 1.2";
            clsMain.DoSQL(sql);
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
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
            sql = "Update Hdnhap set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHd='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
        }

        private void btnAddKH_Click(object sender, EventArgs e)
        {

        }

        private void btnInhdNhap_Click(object sender, EventArgs e)
        {
            sql = "select a.MaThuoc, Tenthuoc,a.gianhap, soluong, thanhtien from Thuoc b join CT_Nhap a on a.MaThuoc = b.MaThuoc where mahd = N'" + txtMaHd.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            dtreport.Clear();
            da.Fill(dtreport);
            rptHdNhap rpt = new QLBT.rptHdNhap();
            rpt.SetDataSource(dtreport);
            // truyền tên nhóm hàng vào biến

           rpt.DataDefinition.FormulaFields["MaHd"].Text = "'" + txtMaHd.Text + "'";
            rpt.DataDefinition.FormulaFields["MaNCC"].Text = "'" + comMaNV.Text + "'";
            rpt.DataDefinition.FormulaFields["TenNCC"].Text = "'" + txtTenNCC.Text + "'";
            rpt.DataDefinition.FormulaFields["Diachi"].Text = "'" + txtDiachi.Text + "'";
            rpt.DataDefinition.FormulaFields["SDT"].Text = "'" + txtSDT.Text + "'";

            // truyền rpt vaò form để in ra
            frmBaocaoNhapViewer f = new frmBaocaoNhapViewer(rpt);
            f.Show();
        }

        private void comMaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True");
            sql = "Select*from thuoc where mathuoc='" + comMaThuoc.Text + "'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader rd;
            
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string tt = rd.GetString(1);
                    txtTenthuoc.Text = tt;
                }
            }
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
            {
                if (addnewFlag == false)
                {
                    int n = grdData.RowCount - 1;
                    for (i = 0; i < n; i++)
                    {
                        tMaThuoc = grdData.Rows[i].Cells["MaThuoc"].Value.ToString();
                        tTenthuoc = grdData.Rows[i].Cells["Tenthuoc"].Value.ToString();
                        tSoluong = grdData.Rows[i].Cells["Soluong"].Value.ToString();
                        tGianhap = grdData.Rows[i].Cells["Gianhap"].Value.ToString();
                        tNsx = grdData.Rows[i].Cells["Nsx"].Value.ToString();
                        tHsd = grdData.Rows[i].Cells["Hsd"].Value.ToString();
                        tThanhtien = grdData.Rows[i].Cells["Thanhtien"].Value.ToString();
                        tGhichu = grdData.Rows[i].Cells["Ghichu"].Value.ToString();

                        sql = "Update CT_Nhap set Soluong='" + tSoluong + "',Gianhap=N'" + tGianhap + "',Nsx='" + tNsx +
                            "',Hsd='" + tHsd + "',Thanhtien='" + tThanhtien + "',Ghichu=N'" + tGhichu + "' where MaHd='" + txtMaHd.Text + "' and mathuoc='" + comMaThuoc.Text + "'";
                        clsMain.DoSQL(sql);
                        NapGrd();
                    }
                    MessageBox.Show("Da cap nhat thanh cong!");
                    gb1.Enabled = false;
                    btnUpdate.Enabled = false;

                }
                else
                {
                    sql = "Insert into CT_Nhap values ('" + txtMaHd.Text + "', '" + comMaThuoc.Text + "', '" + txtSoluong.Text
        + "','" + txtGianhap.Text + "','" + dtpNsx.Text + "','" + txtHsd.Text + "','" + txtThanhtien.Text + "','" + txtGhichu.Text + "')";
                    clsMain.DoSQL(sql);
                    grdData.Refresh();
                    sql = "Select a.Mathuoc, Tenthuoc, soluong, a.gianhap, nsx, hsd, thanhtien, ghichu from CT_Nhap a join Hdnhap b on a.Mahd = b.Mahd join thuoc c on a.MaThuoc = c.MaThuoc  where a.mahd='" + txtMaHd.Text + "'";
                    grdData.DataSource = clsMain.GetDataToTable(sql);
                    MessageBox.Show("Them moi thanh cong");
                    addnewFlag = false;
                    btnUpdate.Enabled = false;
                    gb1.Enabled = false;
                }

                sl = Convert.ToInt32(clsMain.GetFieldValues("SELECT SoLuongton FROM Thuoc WHERE Mathuoc = N'" + comMaThuoc.Text + "'"));
                slton = sl + Convert.ToInt32(txtSoluong.Text);
                sql = "UPDATE Thuoc SET SoLuongton =" + slton + " WHERE Mathuoc= N'" + comMaThuoc.Text + "'";
                clsMain.DoSQL(sql);
                sum = Convert.ToDouble(clsMain.GetFieldValues("SELECT sum(thanhtien) FROM ct_nhap WHERE Mahd ='" + txtMaHd.Text + "'"));
                txtTongtien.Text = sum.ToString();
                sql = "Update Hdnhap set Tongtien = " + txtTongtien.Text + ", Chietkhau = '"
                    + txtChietkhau.Text + "', Thanhtoan = " + txtThanhtoan.Text + " WHERE MAHD= '" + txtMaHd.Text + "'";
                clsMain.DoSQL(sql);
                //tongmoi = sum + Convert.ToDouble(txtThanhtien.Text);
                //txtTongtien.Text = tongmoi.ToString();
            }
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
                sql = "Delete from Ct_nhap where mahd='" + txtMaHd.Text + "' and mathuoc= '" + comMaThuoc.Text + "'";
                clsMain.DoSQL(sql);
                grdData.Rows.RemoveAt(grdData.CurrentRow.Index);
                // Cập nhật lại số lượng cho các mặt hàng
                int sl = Convert.ToInt32(clsMain.GetFieldValues("SELECT SoLuongton FROM Thuoc WHERE MaThuoc = N'" + comMaThuoc.Text + "'"));
                int slcon = sl - slxoa;
                sql = "UPDATE Thuoc SET Soluongton =" + slcon + " WHERE MaThuoc= N'" + mathuocxoa + "'";
                clsMain.DoSQL(sql); ;
                sum = Convert.ToDouble(clsMain.GetFieldValues("SELECT sum(thanhtien) FROM ct_nhap WHERE Mahd ='" + txtMaHd.Text + "'"));
                //tongmoi = sum - thanhtienxoa;
                txtTongtien.Text = sum.ToString();
                sql = "Update Hdnhap set Tongtien = " + txtTongtien.Text + ", Chietkhau = '" 
                    + txtChietkhau.Text + "', Thanhtoan = " + txtThanhtoan.Text + " WHERE MAHD='" + txtMaHd.Text + "'";
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
            tt = tongtien - tongtien*ck / 100;
            txtThanhtoan.Text = tt.ToString();
            sql="Update Hdnhap set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text +"' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
        }

        public void NapCT()
        {
                if (grdData.Rows.Count == 1)
            {
                comMaThuoc.Text = "";
                txtTenthuoc.Text = "";
                txtSoluong.Text = "";
                txtGianhap.Text = "";
                dtpNsx.Text = "";
                txtHsd.Text = "";
                txtThanhtien.Text = "";
                txtGhichu.Text = "";
            }
            int i = grdData.CurrentRow.Index;
            comMaThuoc.Text = grdData.Rows[i].Cells["MaThuoc"].Value.ToString();
            txtTenthuoc.Text = grdData.Rows[i].Cells["Tenthuoc"].Value.ToString();
            txtSoluong.Text = grdData.Rows[i].Cells["Soluong"].Value.ToString();
            txtGianhap.Text = grdData.Rows[i].Cells["Gianhap"].Value.ToString();
            dtpNsx.Text = grdData.Rows[i].Cells["Nsx"].Value.ToString();
            txtHsd.Text = grdData.Rows[i].Cells["Hsd"].Value.ToString();
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
                grdData.Rows[0].Cells["Gianhap"].Value = "";
                grdData.Rows[0].Cells["Nsx"].Value = "";
                grdData.Rows[0].Cells["Hsd"].Value = "";
                grdData.Rows[0].Cells["Thanhtien"].Value = "";
                grdData.Rows[0].Cells["Ghichu"].Value = "";
            }
                int i = grdData.CurrentRow.Index;
            grdData.Rows[i].Cells["MaThuoc"].Value = comMaThuoc.Text;
            grdData.Rows[i].Cells["Tenthuoc"].Value = txtTenthuoc.Text;
            grdData.Rows[i].Cells["Soluong"].Value = txtSoluong.Text;
            grdData.Rows[i].Cells["Gianhap"].Value = txtGianhap.Text;
            grdData.Rows[i].Cells["Nsx"].Value = dtpNsx.Text;
            grdData.Rows[i].Cells["Hsd"].Value = txtHsd.Text;
            grdData.Rows[i].Cells["Thanhtien"].Value = txtThanhtien.Text;
            grdData.Rows[i].Cells["Ghichu"].Value = txtGhichu.Text;

        }
    }
}

