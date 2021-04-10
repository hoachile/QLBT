using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBT
{
    public partial class frmBaocaoNhapViewer : Form
    {
        public frmBaocaoNhapViewer(rptHdNhap rpt)
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = rpt;


        }

    }
}
