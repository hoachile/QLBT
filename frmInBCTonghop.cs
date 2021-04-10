using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBT
{
    public partial class frmInBCTonghop : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtcom = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtreport = new DataTable();
        string constr, sql;

        public frmInBCTonghop()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInBCTonghop_Load(object sender, EventArgs e)
        {
            constr = "Data Source=AD-PC\\SQLEXPRESS;Initial Catalog=QLBT;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql = "SELECT * from Thuoc";
            da = new SqlDataAdapter(sql, conn);
            dtreport.Clear();
            da.Fill(dtreport);
            rptBcHangton rpt = new rptBcHangton();
            rpt.SetDataSource(dtreport);
            
            frmBaocaoHangtonViewercs f = new frmBaocaoHangtonViewercs(rpt);
            f.Show();
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            sql = "SELECT CT_Ban.MaThuoc, Thuoc.Tenthuoc, CT_Ban.Soluong, CT_Ban.Giaban, HdBan.Ngayban"
                      + "  FROM CT_Ban INNER JOIN HdBan ON CT_Ban.MaHd = HdBan.MaHd INNER JOIN "
                     + " Thuoc ON CT_Ban.MaThuoc = Thuoc.MaThuoc"
                     +" where Ngayban between'" + dateTuNgay.Value +"' and '" + dateDenngay.Value +"'";
            da = new SqlDataAdapter(sql, conn);
            dtreport.Clear();
            da.Fill(dtreport);
            rptTongHopDT rpt = new rptTongHopDT();
            rpt.SetDataSource(dtreport);
            rpt.DataDefinition.FormulaFields["tungay"].Text = "'" + dateTuNgay.Value + "'";
            rpt.DataDefinition.FormulaFields["denngay"].Text = "'" + dateDenngay.Value + "'";
            frmThdoanhthuViewer f = new frmThdoanhthuViewer(rpt);
            f.Show();
            this.Close();
     
        }
    }
}
