using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controller;
using System.Data.SqlClient;
using System.Configuration;

namespace View
{
    public partial class frmConsolidado : Form
    {

        string vServer;

        SqlConnection cn;
        public frmConsolidado(SqlConnection cn, string[] aSb, decimal[] Saldo)
        {

            vServer = ConfigurationManager.AppSettings["servidor"];

            InitializeComponent();

            this.cn = cn;

            PreencheLabelComZero();

            lblConsolidadoSBEmp01.Text = aSb[0];
            lblConsolidadoSaldoEmp01.Text = Saldo[0].ToString("0.00");

            lblConsolidadoSBEmp02.Text = aSb[1];
            lblConsolidadoSaldoEmp02.Text = Saldo[1].ToString("0.00");

            lblConsolidadoSBEmp03.Text = aSb[2];
            lblConsolidadoSaldoEmp03.Text = Saldo[2].ToString("0.00");

            lblConsolidadoSBEmp04.Text = aSb[3];
            lblConsolidadoSaldoEmp04.Text = Saldo[3].ToString("0.00");

            lblConsolidadoSBEmp05.Text = aSb[4];
            lblConsolidadoSaldoEmp05.Text = Saldo[4].ToString("0.00");

            lblConsolidadoSBEmp06.Text = aSb[5];
            lblConsolidadoSaldoEmp06.Text = Saldo[5].ToString("0.00");

            lblConsolidadoSBEmp07.Text = aSb[6];
            lblConsolidadoSaldoEmp07.Text = Saldo[6].ToString("0.00");

            lblConsolidadoSBEmp08.Text = aSb[7];
            lblConsolidadoSaldoEmp08.Text = Saldo[7].ToString("0.00");

            lblConsolidadoGeral.Text = (Saldo[0]+Saldo[1]+Saldo[2]+Saldo[3]+Saldo[4]+Saldo[5]+Saldo[6]+Saldo[7]).ToString("0.00");

            int vMaior = 0;
            for (int k = 0; k < 8; k++) {
                if ((!string.IsNullOrEmpty(aSb[k])) && int.Parse(aSb[k]) > vMaior)
                {
                    vMaior = int.Parse(aSb[k]);
                }                    
            }

            lblTotalDeSB.Text = vMaior.ToString();

        }

        private void PreencheLabelComZero()
        {
            lblConsolidadoSBEmp01.Text = "0";
            lblConsolidadoSBEmp02.Text = "0";
            lblConsolidadoSBEmp03.Text = "0";
            lblConsolidadoSBEmp04.Text = "0";
            lblConsolidadoSBEmp05.Text = "0";
            lblConsolidadoSBEmp06.Text = "0";
            lblConsolidadoSBEmp07.Text = "0";
            lblConsolidadoSBEmp08.Text = "0";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }



        private void btnConfirmarConsolidado_Click(object sender, EventArgs e)
        {
            if (int.Parse(lblTotalDeSB.Text) <= 0)
            {
                CSharpUtil.Util.Msg("Não há Aplicação para Consolidar!");
            }
            else
            {


                SqlConnection cn = new SqlConnection("Data Source=" + vServer + ";Initial Catalog=" + "newprojeem_consolidado" + ";Integrated Security=True;");
                Consolidado consol = new Consolidado();
                try
                {
                    cn.Open();
                    string id = CSharpUtil.Util.GerarID(cn, "id_consolidado");

                    consol.AddConsolidado(cn, int.Parse(id), lblReferencia.Text,
                                          Convert.ToDateTime(txtDataDoFechamento.Text), int.Parse(lblTotalDeSB.Text),
                                          Convert.ToDecimal(lblConsolidadoGeral.Text));


                    // Zera dados 
                    // desta Aplicação
                    ZeraContabil();

                }
                catch (Exception ex)
                {
                    CSharpUtil.Util.Msg(ex.Message);
                }
                cn.Dispose();
                cn.Close();
                this.Close();
            }

        }

        void ZeraContabil() {
            

            SqlConnection cn = null;
            string[] catalogo = new string[8] {"newprojeem_iii_1", "newprojeem_iii_2", "newprojeem_iii_3", "newprojeem_iii_4", 
                                               "newprojeem_iii_5", "newprojeem_iii_6", "newprojeem_iii_7", "newprojeem_iii_8"};
            for (int k = 0; k < 8; k++) {
                cn = new SqlConnection("Data Source="+ VServer +";Initial Catalog=" + catalogo[k] + ";Integrated Security=True;");
                cn.Open();
                Consolidado.ZeraContabilidade(cn);
                cn.Dispose();
            }
            CSharpUtil.Util.Msg("Base de Dados Zerada Para Nova Aplicação!");

        }

        private void frmConsolidado_Load(object sender, EventArgs e)
        {


            lblReferencia.Text = CSharpUtil.Util.Referencia;
            txtDataDoFechamento.Text = CSharpUtil.Util.Date;

            SqlConnection MyConn;
            MyConn = new SqlConnection("Data Source=" + VServer + ";Initial Catalog=" + "newprojeem_consolidado" + ";Integrated Security=True;");
            MyConn.Open();

            lblSaldoAnteriorNaReferencia.Text = Consolidado.GetSaldoAnteriorConsolidado(MyConn, lblReferencia.Text).ToString("0.00");
            MyConn.Dispose();
            MyConn.Close();

            

        }

        public string VServer
        {
            get { return vServer; }
            set { vServer = value; }
        }


 
    }
}
