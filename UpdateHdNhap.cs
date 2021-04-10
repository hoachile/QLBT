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
    public partial class UpdateHdNhap : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        string constr, sql;
        int i;
        Boolean addnewFlag = false;
        string tMaThuoc, tTenthuoc, tSoluong, tGianhap, tNsx, tHsd, tThanhtien, tGhichu;

        public UpdateHdNhap()
        {
            InitializeComponent();
            fillcombobox();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            sql = "Update Hdnhap set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
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
            sql = "Update Hdnhap set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
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

        private void txtThanhtien_TextChanged(object sender, EventArgs e)
        {
           /* double tt, tongtien;
            if (txtThanhtien.Text == "")
                tt = 0;
            else
                tt = Convert.ToDouble(txtThanhtien.Text);
            tongtien = Convert.ToDouble(clsMain.GetFieldValues("Update Hdban set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '" + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'"));
            txtTongtien.Text = tongtien.ToString();*/
        }

        private void UpdateHdNhap_Load(object sender, EventArgs e)
        {            

            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select a.Mathuoc, Tenthuoc, soluong, a.gianhap, nsx, hsd, thanhtien, ghichu from CT_Nhap a join Hdnhap b on a.Mahd = b.Mahd join thuoc c on a.MaThuoc = c.MaThuoc  where a.mahd='" + txtMaHd.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdData.DataSource = dt;
            grdData.Refresh();
            NapCT();
        }

        private void grdData_MouseUp(object sender, MouseEventArgs e)
        {
            NapCT();
        }

        private void comMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (comMaNV.Text == "")
                txtHoten.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select Hoten from NhanVien where MaNV =N'" + comMaNV.Text+ "'";
            txtHoten.Text = clsMain.GetFieldValues(str);
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
            sql = "Update Hdnhap set Tongtien = '" + txtTongtien.Text + "', Chietkhau = '"
                + txtChietkhau.Text + "', Thanhtoan = '" + txtThanhtoan.Text + "' WHERE MAHD='" + txtMaHd.Text + "'";
            clsMain.DoSQL(sql);
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
                sql = "Update Hdnhap set Tongtien =" + txtTongtien.Text + ", Chietkhau = '"
                    + txtChietkhau.Text + "', Thanhtoan = " + txtThanhtoan.Text + "" + " WHERE MAHd= '" + txtMaHd.Text + "'";
                clsMain.DoSQL(sql);
            }
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
