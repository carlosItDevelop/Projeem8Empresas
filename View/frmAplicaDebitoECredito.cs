#region:: Diretivas Using's

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using Projeem.Controller;
using View;

#endregion: Diretivas Using's

namespace Projeem.View
{
    public partial class frmAplicaDebitoECredito : Form
    {

        #region:: Variáveis da Classe
        string[,] aMapaCapi = new string[41, 8];


        #region:: Arrays que controlam os debitos e créditos dos Gps até 15º SB
        static decimal[] CtrlGpID = new decimal[25];
        static decimal[] CtrlGpDebito = new decimal[25];
        static decimal[] CtrlGpDebitoDeduzido = new decimal[25];
        static decimal[] CtrlGpCredito = new decimal[25];
        #endregion

        int car;
        int totalDeSerie;
        int regAtualSerie;
        string status = string.Empty;
        string vTipoDeProcessamento;

        ControleAplicacao ctrl = null;
        SqlConnection cn;
        Hashtable hashContabilidade = new Hashtable();
        Indices ind = null;

        string fileSoundDemora = "Demora.wav";
        string fileSoundGuardaCarta = "GuardaCarta.wav";
        string fileSoundRenuncia = "Renuncia.wav";
        //string fileSoundInicioJogo = "Inicio.wav"; // Mudar o arquivo!!!
        string appSoundPath = Application.StartupPath + @"\sounds\"; 

        #endregion:: Variáveis da Classe


        #region:: Contrutor do Objeto Form
        public frmAplicaDebitoECredito(SqlConnection cn)
        {

            this.cn = cn;
            ind = new Indices(cn);
            ctrl = new ControleAplicacao(cn);

            hashContabilidade["DebitoReal"] = 0.00M;
            hashContabilidade["CreditoReal"] = 0.00M;
            hashContabilidade["SaldoSbReal"] = 0.00M;
            hashContabilidade["SaldoAcumuladoReal"] = 0.00M;

            InitializeComponent();

        }
        #endregion


        private void frmAplicaDebitoECredito_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Funcoes.MontaMapaCapi(txtNumSB, txtValorMedia, cboNumGP, ref aMapaCapi, lvMapaCapi);
            
            Funcoes.PreencheCombo(this.cn, cboGrupoAtual, "grupos", "grupo");

            barProgresso.Minimum = 0;
            barProgresso.Maximum = 100;
            barProgresso.Value = 0;

            lblIdContabilidade.Text = Indices.GetIdContabilidade(this.cn).ToString();
            lblUser.Text = CSharpUtil.Util.User;
            lblData.Text = CSharpUtil.Util.Date;
            lblReferencia.Text = CSharpUtil.Util.Referencia;

            lblMaximoCGReal.Text = Indices.GetMaxCGReal(this.cn);
            btnStartProcess.Enabled = true;
            lblDbInUse.Text = "Empresa: ["+Indices.dbInUse+"]";

            Funcoes.PreencheCtrlDebito(ref lvControlGP, this.cn);

            #region:: Exibe e Seta os Hash de Contabilidades;
            ctrl.ExibeDadosContabilidade();
            if (ctrl.Num_registro > 0) {
                ctrl.Ultimo();
                PreencheCampos();

                hashContabilidade["DebitoReal"] = Convert.ToDecimal(lblDebitoReal.Text);
                hashContabilidade["CreditoReal"] = Convert.ToDecimal(lblCreditoReal.Text);
                hashContabilidade["SaldoSbReal"] = Convert.ToDecimal(lblSaldoRealSb.Text);
                hashContabilidade["SaldoAcumuladoReal"] = Convert.ToDecimal(lblSaldoRealAcumulado.Text);

            }
            #endregion:: Exibe e Seta os Hash de Contabilidades;
            
            
            vTipoDeProcessamento = Indices.GetStatusProcessamento(this.cn);
            if (vTipoDeProcessamento.Equals("DEBITO")){
                lblStatusDeProcessamento.Text = "--> DÉBITO!";
            } else { lblStatusDeProcessamento.Text = "--> CRÉDITO!"; }

        }


        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            if (vTipoDeProcessamento.Equals("DEBITO"))
            {
                try {
                    cboGrupoAtual.Text = "";
                    lblStatusDeProcessamento.Text = "PROCESSANDO DÉBITO!";
                    btnFecharCapitalizacao.Enabled = false; 
                    btnConsolidar.Enabled = false;

                    ProcessaDebito();

                    if (Convert.ToDecimal(lblSaldoRealAcumulado.Text) < Convert.ToDecimal(lblMaximoCGReal.Text))
                    {
                        lblMaximoCGReal.Text = lblSaldoRealAcumulado.Text;
                        Indices.SetMaxCGReal(lblSaldoRealAcumulado.Text, this.cn);
                    }

                    Indices.UpdateStatusProcessamento("CREDITO", this.cn);
                    btnFecharCapitalizacao.Enabled = true; 
                    btnConsolidar.Enabled = true;
                    vTipoDeProcessamento = "CREDITO";
                    lblStatusDeProcessamento.Text = "--> CRÉDITO!";
                } catch (Exception ex) {
                    CSharpUtil.Util.MsgErro(ex.Message);
                }
            } else {
                if (cboGrupoAtual.Text.Trim().Length <= 0) {
                    CSharpUtil.Util.MsgErro("Você precisa informar o grupo anterior");
                } else {
                    try {                        
                        lblStatusDeProcessamento.Text = "PROCESSANDO CRÉDITO!";
                        btnFecharCapitalizacao.Enabled = false; 
                        btnConsolidar.Enabled = false;

                        ProcessaCredito();

                        Indices.UpdateStatusProcessamento("DEBITO", this.cn);
                        btnFecharCapitalizacao.Enabled = true;
                        btnConsolidar.Enabled = true;

                        btnSair.Enabled = true; 
                        vTipoDeProcessamento = "DEBITO";
                        lblStatusDeProcessamento.Text = "--> DÉBITO!";
                        
                    } catch (Exception ex) {
                        CSharpUtil.Util.MsgErro(ex.Message);
                    }
                } 
            }
        
        }


        private void ProcessaDebito()
        {
            for (int i = 0; i < 25; i++)
            {
                CtrlGpID[i] = i + 1;
                CtrlGpDebito[i] = 0;
                CtrlGpDebitoDeduzido[i] = 0;
            }

            btnStartProcess.Enabled = false;
            barProgresso.Value = 0;
            int vCar = int.MinValue;

            int curReg = 0;
            int numReg = 0;

            SqlDataAdapter da = new SqlDataAdapter("SELECT gr_01, gr_02, gr_03, carencia from series "+
                                                   "where status = '"+"Processing_115"+"'", this.cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SomaDebito_Proc");
            DataTable dt = new DataTable();
            dt = ds.Tables["SomaDebito_Proc"];
            numReg = dt.Rows.Count;


            if (numReg > 0)
            {
                while (curReg < numReg)
                {
                    vCar = (int)dt.Rows[curReg]["carencia"];

                    CtrlGpDebito[(int)dt.Rows[curReg]["gr_01"] - 1] += Convert.ToDecimal(aMapaCapi[vCar, 2]);
                    CtrlGpDebito[(int)dt.Rows[curReg]["gr_02"] - 1] += Convert.ToDecimal(aMapaCapi[vCar, 2]);
                    CtrlGpDebito[(int)dt.Rows[curReg]["gr_03"] - 1] += Convert.ToDecimal(aMapaCapi[vCar, 2]);

                    barProgresso.Value = curReg * 100 / numReg;

                    curReg++;
                }
            }
            ds.Dispose();


            hashContabilidade["CreditoReal"] = 0.00M;
            lblCreditoReal.Text = 0.00M.ToString("0.00");
            
            decimal vCount = 0.00M;
            for (int i = 0; i < 25; i++) {
                vCount += CtrlGpDebito[i];
            }

            decimal lmvtc = Funcoes.OtimizaDebito(ref CtrlGpDebitoDeduzido, CtrlGpDebito, this.cn);
               
            vCount = 0.00M;
            lvControlGP.Items.Clear();
            for (int j = 0; j < 25; j++)
            {
                lvControlGP.Items.Add(new ListViewItem(new string[] { 
                                            CtrlGpID[j].ToString(),
                                            CtrlGpDebito[j].ToString("0.000"),
                                            CtrlGpDebitoDeduzido[j].ToString("0.000")}));
                                            vCount += CtrlGpDebitoDeduzido[j];
            }

            hashContabilidade["DebitoReal"] = vCount;
            lblDebitoReal.Text = ((decimal)hashContabilidade["DebitoReal"]).ToString("0.00");


            hashContabilidade["SaldoSbReal"] = Convert.ToDecimal(hashContabilidade["CreditoReal"]) - Convert.ToDecimal(hashContabilidade["DebitoReal"]);            
            lblSaldoRealSb.Text = Convert.ToDecimal(hashContabilidade["SaldoSbReal"]).ToString("0.00");
            hashContabilidade["SaldoAcumuladoReal"] = Convert.ToDecimal(hashContabilidade["SaldoAcumuladoReal"]) - Convert.ToDecimal(hashContabilidade["DebitoReal"]);
            lblSaldoRealAcumulado.Text = Convert.ToDecimal(hashContabilidade["SaldoAcumuladoReal"]).ToString("0.00");

            GravaDados();

            lblIdContabilidade.Text = Indices.GetIdContabilidade(this.cn).ToString();


            btnStartProcess.Enabled = true;
            Funcoes.TocaWav(appSoundPath + fileSoundRenuncia);

        }


        private void ProcessaCredito() 
        {
            int gp_premiado = Convert.ToInt32(cboGrupoAtual.Text);
            
            btnStartProcess.Enabled = false;
            Serie serie = new Serie(this.cn);
            serie.CarregaSeries(this.cn);
            serie.Primeiro();
            totalDeSerie = serie.Num_registro;
            
            for (int i = 0; i < 25; i++)
            {
                CtrlGpCredito[i] = 0;
            }
           
            while (!serie.FinalDeArquivo)
            {
                regAtualSerie = serie.Registro_atual;
                barProgresso.Value = regAtualSerie * 100 / totalDeSerie;

                status = serie.Status;
                car = serie.Carencia;

                if (((gp_premiado == serie.Gr_01) || (gp_premiado == serie.Gr_02) || (gp_premiado == serie.Gr_03)) && (status != "OUT" && status != "Subsidio_wait" && status != "Subsidio_Ready"))
                {                    
                    serie.ProcessaCredito(true, gp_premiado, ref CtrlGpCredito, ref aMapaCapi, car);
                } else {
                    serie.ProcessaCredito(false, gp_premiado, ref CtrlGpCredito, ref aMapaCapi, car);
                }
                serie.Proximo();
            }


            #region:: Recupera da Tabela Grupos se o Premio foi Deduzido ou não e vCreditoReal!
            Grupos grp = new Grupos(this.cn);
            decimal vCreditoReal = decimal.MinValue;            

            string DebitoDeduzido = grp.IsDebidoDeduzido(cboGrupoAtual.Text.Trim(), ref vCreditoReal);
            
            hashContabilidade["CreditoReal"] = vCreditoReal;
            lblCreditoReal.Text = vCreditoReal.ToString("0.00");
            grp = null;
            #endregion


            hashContabilidade["SaldoSbReal"] =  Convert.ToDecimal(hashContabilidade["CreditoReal"]) - Convert.ToDecimal(hashContabilidade["DebitoReal"]);            
            lblSaldoRealSb.Text = ((decimal)hashContabilidade["SaldoSbReal"]).ToString("0.000");
            hashContabilidade["SaldoAcumuladoReal"] = Convert.ToDecimal(hashContabilidade["SaldoAcumuladoReal"]) + Convert.ToDecimal(hashContabilidade["CreditoReal"]);
            lblSaldoRealAcumulado.Text = ((decimal)hashContabilidade["SaldoAcumuladoReal"]).ToString("0.00");

            EditaDados();

            ctrl.ExibeDadosContabilidade();
            ctrl.Ultimo();
            PreencheCampos();

            serie = null;

            btnStartProcess.Enabled = true;
            Funcoes.TocaWav(appSoundPath + fileSoundDemora);

        }


        #region :: Método VerificaCampos
        bool VerificaCampos()
        {
            string msgInconsistencia = "RELATÓRIO DE INCONSISTÊNCIA:\r\n=======================\r\n\n";
            int k = 0;
            if (cboGrupoAtual.Text.Trim().Equals("") && (vTipoDeProcessamento.Equals("CREDITO")))
            {
                k++;
                msgInconsistencia += (k < 10 ? "0" + k.ToString() + " - Campo Prêmio é Obrigatório!\r\n" : k.ToString() +
                    " - Campo Prêmio é Obrigatório!\r\n");
            }
            if (lblCreditoReal.Text.Trim().Length == 0)
            {
                k++;
                msgInconsistencia += (k < 10 ? "0" + k.ToString() + " - Campo Crédito Real é Obrigatório!\r\n" : k.ToString() +
                    " - Campo Crédito Real é Obrigatório!\r\n");
            }


            if (k > 0)
            {
                CSharpUtil.Util.MsgErro(msgInconsistencia);
                return false;
            }
            return true;
        }// Fim do Método VerificaCampos;
        #endregion


        void PreencheCampos()
        {
            lblResult_gp.Text = ctrl.Grupo.ToString();
            lblDebitoReal.Text = ctrl.Debito_real.ToString("0.00");
            lblCreditoReal.Text = ctrl.Credito_real.ToString("0.00");
            lblSaldoRealSb.Text = ctrl.Saldo_sb_real.ToString("0.00");    
            lblSaldoRealAcumulado.Text = ctrl.Saldo_real_acumulado.ToString("0.00");
            lblCtrlNavegacao.Text = "Reg.: " + ctrl.Registro_atual.ToString() + " de: " + ctrl.Num_registro.ToString();


        }


        #region:: Método GravaDados
        /// <summary>
        /// GravaDados
        /// </summary>
        void GravaDados()
        {
            // transação que é levada para 
            // dentro dos métodos de adição 
            // abaixo, juntamente com o objeto Connection;
            SqlTransaction trans = null;
            try
            {
                if (VerificaCampos()) // Mudar para (CamposConfere -> Conferri campo por campo);
                {
                    ControleAplicacao ctrl_app = new ControleAplicacao(this.cn);
                    int id = int.Parse(CSharpUtil.Util.GerarID(this.cn, "id_contabilidade"));
                    int gp = (cboGrupoAtual.Text.Trim() != ""  ? int.Parse(cboGrupoAtual.Text) : 0) ;
                    int sb = Indices.GetSubperiodo(this.cn);

                    trans = this.cn.BeginTransaction("GravaDados");

                    ctrl_app.Add_Contabilidade(this.cn, trans, id, gp, sb,                                 
                                Convert.ToDecimal(hashContabilidade["DebitoReal"]).ToString(),
                                Convert.ToDecimal(hashContabilidade["CreditoReal"]).ToString(),
                                Convert.ToDecimal(hashContabilidade["SaldoSbReal"]).ToString(),
                                Convert.ToDecimal(hashContabilidade["SaldoAcumuladoReal"]).ToString());      


                    Indices.AddSubperiodo(this.cn, trans);
                    Indices.SetIdContabilidade(id.ToString(), this.cn, trans);


                   trans.Commit();

                }
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
                trans.Rollback("GravaDados");
            }

            Funcoes.TocaWav(appSoundPath + fileSoundGuardaCarta);

        /* ----------------------- */
        }

        #endregion: Método GravaDados


        #region:: Método EditaDados
        /// <summary>
        /// GravaDados
        /// </summary>
        void EditaDados()
        {
            // transação que é levada para 
            // dentro dos métodos de adição 
            // abaixo, juntamente com o objeto Connection;
            SqlTransaction trans = null;
            try
            {
                if (VerificaCampos()) // Mudar para (CamposConfere -> Conferri campo por campo);
                {
                    ControleAplicacao ctrl_app = new ControleAplicacao(this.cn);
                                        
                    // Como estou editando não gero id e sim recupero de Indices;
                    int id = int.Parse(lblIdContabilidade.Text);
                    int gp = int.Parse(cboGrupoAtual.Text);

                    trans = this.cn.BeginTransaction("EditaDados");

                    ctrl_app.Upd_Contabilidade(this.cn, trans, id, gp, 
                                Convert.ToDecimal(hashContabilidade["DebitoReal"]).ToString(),
                                Convert.ToDecimal(hashContabilidade["CreditoReal"]).ToString(),
                                Convert.ToDecimal(hashContabilidade["SaldoSbReal"]).ToString(), 
                                Convert.ToDecimal(hashContabilidade["SaldoAcumuladoReal"]).ToString());

               
                    trans.Commit();

                }
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
                trans.Rollback("EditaDados");
            }

            Funcoes.TocaWav(appSoundPath + fileSoundGuardaCarta);

            /* ----------------------- */
        }

        #endregion: Método EditaDados


        #region:: Métodos de Navegação - Contabilidade
        private void btnProximo_Click(object sender, EventArgs e)
        {
            ctrl.Proximo();
            PreencheCampos();
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            ctrl.Anterior();
            PreencheCampos();
        }
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            ctrl.Primeiro();
            PreencheCampos();
        }
        private void btnUltimo_Click(object sender, EventArgs e)
        {
            ctrl.Ultimo();
            PreencheCampos();
        }
        #endregion: Métodos de Navegação - Contabilidade
 

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnMontaCapi_Click(object sender, EventArgs e)
        {
            Funcoes.MontaMapaCapi(txtNumSB, txtValorMedia, cboNumGP, ref aMapaCapi, lvMapaCapi);
        }


        private void btnFecharCapitalizacao_Click(object sender, EventArgs e)
        {
            try{
                Indices.ZeraContadores(this.cn, "Processing_115");
                Indices.ZeraContadores(this.cn);
                Indices.UpdateStatusProcessamento("DEBITO", this.cn);
                ControleAplicacao.DeleteAll_WithSProc(this.cn);
                Grupos.ZeraDebitosEDeducoes(this.cn);
                lblSaldoRealAcumulado.Text = "0.00";

                this.Dispose();
                this.Close();
                CSharpUtil.Util.Msg("Dados Zerados com Sucesso!");
            }
            catch (Exception ex) {
                CSharpUtil.Util.Msg(ex.Message);
            }

        }

        private void btnConsolidar_Click(object sender, EventArgs e)
        {
            SqlConnection cnE = null;
            SqlDataAdapter da;
            DataSet ds;
            DataTable dt;
            int vCountEmpresa = 0;
            string[] aSb = new string[8] { "0", "0", "0", "0", "0", "0", "0", "0"};
            decimal[] SdRealAcumulado = new decimal[8] { 0.00M, 0.00M, 0.00M, 0.00M, 0.00M, 0.00M, 0.00M, 0.00M};
            string[] catalogo = new string[8] {"newprojeem_iii_1", "newprojeem_iii_2", "newprojeem_iii_3", "newprojeem_iii_4", 
                                                   "newprojeem_iii_5", "newprojeem_iii_6", "newprojeem_iii_7", "newprojeem_iii_8"};
            while (vCountEmpresa < 8)
            {

                cnE = new SqlConnection("Data Source=DESKTOP-D3LJP8S\\SQLEXP2014;Initial Catalog=" + catalogo[vCountEmpresa] + ";Integrated Security=True;");
                try
                {
                    cnE.Open();

                    string InstrucaoSql = "Select * from contabilidade";

                    da = new SqlDataAdapter(InstrucaoSql, cnE);
                    ds = new DataSet();
                    da.Fill(ds, "ctbl01");
                    dt = ds.Tables["ctbl01"];

                    int UltimoReg = dt.Rows.Count - 1;

                    if (UltimoReg >= 0)
                    {
                        aSb[vCountEmpresa] = dt.Rows[UltimoReg]["subperiodo"].ToString();
                        SdRealAcumulado[vCountEmpresa] = Convert.ToDecimal(dt.Rows[UltimoReg]["saldo_real_acumulado"].ToString());
                    }


                }
                catch (Exception ex1)
                {
                    CSharpUtil.Util.Msg(ex1.Message);
                }
                finally
                {
                    cnE.Dispose();
                    cnE.Close();
                }

                vCountEmpresa++;

            }// Fim do while

            frmConsolidado frm = new frmConsolidado(this.cn, aSb, SdRealAcumulado);
            frm.ShowDialog();

        }

        private void txtValorMedia_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabResultados_Click(object sender, EventArgs e)
        {

        }


        // -------------------------- //
    }// FIM DA CLASSE GERAL DO FORM;
}// FIM DO NAMESPACE;
