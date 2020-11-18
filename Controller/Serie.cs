using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Projeem.Controller
{
    public class Serie
    {
        #region::: Variáveis de Classe
        SqlConnection cn;
        int curReg = 0;
        int totalReg = 0;
        int registro_atual;
        int num_registro;
        
        int id_serie;
        int gr_01;
        int gr_02;
        int gr_03;

        int carencia;
        int qtde_premio;
        int mor_carencia;
        int pr_aplicacao;
        int num_aplicacao;
        int carencia_recap;
        string status;
        DataTable dt;

        bool finalDeArquivo = false;

        #endregion


        /// <summary>
        /// Construtor do Objeto
        /// </summary>
        /// <param name="cn">Objeto Conexão passada como referência (como todo objeto)</param>
        public Serie(SqlConnection cn) {
            this.cn = cn;
        }

        public void AumentaCarencia(int id_serie) {
            try
            {
                SqlCommand cmd = new SqlCommand("update series set carencia = (carencia + 1) where id_serie = "+id_serie, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }

 
        public void ZeraCarencia(int id_serie)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update series set carencia = 0 where id_serie = " + id_serie, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }



        public void AumentaQtdePremio(int id_serie) {
            try
            {
                SqlCommand cmd = new SqlCommand("update series set qtde_premio = (qtde_premio + 1) where id_serie = " + id_serie, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }

        public void ComparaCarencia(int id_serie) {
            if (carencia > mor_carencia)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update series set mor_carencia = carencia where id_serie = " + id_serie, cn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    CSharpUtil.Util.MsgErro(ex.Message);
                }
            }
        }




        /// <summary>
        ///  Método Sobrecarregado ProcessaCredito,
        ///  em caso de prêmio!
        /// </summary>

        public void ProcessaCredito(bool Premiada, int gp_premiado, ref decimal[] CtrlGpCredito, ref string[,] aMapaCapi, int car)
        {

            if (Premiada)
            {

                    ZeraCarencia(id_serie);
                    AumentaQtdePremio(id_serie);
                    CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);


            } else if (!Premiada) {
                    AumentaCarencia(id_serie);
                    ComparaCarencia(id_serie);

           }

       }



        public void IniciaNewStatus(string new_status, int id, SqlConnection cn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update series set status = '"+new_status+"', carencia_recap = 1 where id_serie = "+id, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }

        public void MudaStatus(string new_status, int id_ser, SqlConnection conn) {
            try
            {
                SqlCommand cmd = new SqlCommand("update series set status = '"+new_status+"' where id_serie = "+id_ser, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }


        public void CarregaSeries(SqlConnection cn)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from series where status <> '"+"OUT"+"'", cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Carrega_Series");
            dt = ds.Tables["Carrega_Series"];
            curReg = 0;
            registro_atual = curReg+1;
            totalReg = dt.Rows.Count;
            num_registro = totalReg;

            ds.Dispose();

            if (num_registro > 0) SetRst();

        } // Fim de CarregaSeries;

        public int ContaStatus(string status)
        {
            int vRetVal = 0;
            SqlDataAdapter da = new SqlDataAdapter("select count(*) as total from series where status = '"+status+"'", this.cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Conta_Status");
            dt = ds.Tables["Conta_Status"];
            vRetVal = Convert.ToInt32(dt.Rows[0]["total"].ToString());

            ds.Dispose();

            return vRetVal;

        }
        
        public void Proximo()
        {
            curReg++;
            registro_atual = curReg+1;
            if (curReg > totalReg - 1)
            {
                curReg = totalReg - 1;
                finalDeArquivo = true;
                //CSharpUtil.Util.MsgInfo("Final de Arquivo!");
            }
            else
            {
                SetRst();
            }
        } // Fim de ProximoRegistro;

        public void Anterior()
        {
            finalDeArquivo = false;
            curReg--;
            registro_atual = curReg+1;
            if (curReg < 0)
            {
                curReg = 0;
                CSharpUtil.Util.MsgInfo("Início de Arquivo!");
            }
            else
            {
                SetRst();
            }
        } // Fim de Anterior;

        public void Primeiro()
        {
            curReg = 0;
            registro_atual = curReg+1;
            finalDeArquivo = false;
            SetRst();
        } // Fim de Primeiro;

        void SetRst()
        {
            id_serie = Convert.ToInt32(dt.Rows[curReg]["id_serie"].ToString());
            gr_01 = Convert.ToInt32(dt.Rows[curReg]["gr_01"].ToString());
            gr_02 = Convert.ToInt32(dt.Rows[curReg]["gr_02"].ToString());
            gr_03 = Convert.ToInt32(dt.Rows[curReg]["gr_03"].ToString());
            carencia = Convert.ToInt32(dt.Rows[curReg]["carencia"].ToString());
            carencia_recap = Convert.ToInt32(dt.Rows[curReg]["carencia_recap"].ToString());
            qtde_premio = Convert.ToInt32(dt.Rows[curReg]["qtde_premio"].ToString());
            mor_carencia = Convert.ToInt32(dt.Rows[curReg]["mor_carencia"].ToString());
            pr_aplicacao = Convert.ToInt32(dt.Rows[curReg]["pr_aplicacao"].ToString());
            status = dt.Rows[curReg]["status"].ToString();
        }


        #region::: Propriedades do Objeto

        public bool FinalDeArquivo {
            get { return finalDeArquivo; }
            set { finalDeArquivo = value; }
        }

        public int Registro_atual
        {
            get { return registro_atual; }
            set { registro_atual = value; }
        }

        public int Num_registro
        {
            get { return num_registro; }
            set { num_registro = value; }
        }

        public int Id_serie
        {
            get { return id_serie; }
            set { id_serie = value; }
        }

        public int Gr_01
        {
            get { return gr_01; }
            set { gr_01 = value; }
        }

        public int Gr_02 {
            get { return gr_02; }
            set { gr_02 = value; }
        }

        public int Gr_03
        {
            get { return gr_03; }
            set { gr_03 = value; }
        }

        public int Carencia {
            get { return carencia; }
            set { carencia = value; }
        }

        public int Qtde_premio {
            get { return qtde_premio; }
            set { qtde_premio = value; }
        }

        public int Mor_carencia {
            get { return mor_carencia; }
            set { mor_carencia = value; }
        }

        public int Pr_aplicacao {
            get { return pr_aplicacao; }
            set { pr_aplicacao = value; }
        }

        public int Num_aplicacao {
            get { return num_aplicacao; }
            set { num_aplicacao = value; }
        }

        public string Status {
            get { return status; }
            set { status = value; }
        }

        public int Carencia_recap
        {
            get { return carencia_recap; }
            set { carencia_recap = value; }
        }

        #endregion

    }} // Fim do namespace
