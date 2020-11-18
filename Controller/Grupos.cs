using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace Projeem.Controller
{
    public class Grupos
    {
        #region:: Variáveis de Classe
        SqlConnection cn;
        int id_grupo;
        int grupo;
        int carencia;
        int qtde_premio;
        decimal debito_previsto;
        decimal deducao_por_mvtc;
        decimal credito;

        int curReg;
        int registro_atual;
        int totalReg;
        int num_registro;
        bool finalDeArquivo = false;

        #endregion

        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="cn">Objeto Conection herdado</param>
        public Grupos(SqlConnection cn) {
            this.cn = cn;
        }


        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        public void CarregaGrupos()
        {
            try
            {
                da = new SqlDataAdapter("select * from grupos", this.cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                ds = new DataSet();
                da.Fill(ds, "Carrega_Grupos");
                dt = ds.Tables["Carrega_Grupos"];
                curReg = 0;
                registro_atual = curReg + 1;
                totalReg = dt.Rows.Count;
                num_registro = totalReg;

                ds.Dispose();

                if (num_registro > 0) SetRst();

            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }


        void SetRst()
        {
            id_grupo = Convert.ToInt32(dt.Rows[curReg]["id_grupo"].ToString());
            grupo = Convert.ToInt32(dt.Rows[curReg]["grupo"].ToString());
            carencia = Convert.ToInt32(dt.Rows[curReg]["carencia"].ToString());
            qtde_premio = Convert.ToInt32(dt.Rows[curReg]["qtde_premio"].ToString());
            debito_previsto = Convert.ToDecimal(dt.Rows[curReg]["debito_previsto"].ToString());
            deducao_por_mvtc = Convert.ToDecimal(dt.Rows[curReg]["deducao_por_mvtc"].ToString());
            credito = Convert.ToDecimal(dt.Rows[curReg]["credito"].ToString());            
        }

        public static void ZeraDebitosEDeducoes(SqlConnection cn) {
            SqlCommand cmd = new SqlCommand("update grupos set credito = 0, debito_previsto = 0, deducao_por_mvtc = 0", cn);
            cmd.ExecuteNonQuery();
        }

        public decimal DeduzirRedundancia()
        {
            string vSql = "";
            decimal lMvtc = 0;
            try
            {
                string InstrucaoSql = "select top  18 * from grupos order by deducao_por_mvtc DESC";                

                SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, this.cn);
                DataSet ds = new DataSet();
                da.Fill(ds, "mor18grupos2");
                DataTable dt = new DataTable();
                dt = ds.Tables["mor18grupos2"];

                //Separa o menor dos 18
                lMvtc = Convert.ToDecimal(dt.Rows[17]["deducao_por_mvtc"].ToString()); //MVTC                

                if (lMvtc > 0)
                {
                    //Percorre os 18 maiores e 
                    //subtrai destes 18 o MVTC;
                    string vDed = "0";
                    decimal vAuxDecimal = 0;
                    int vAuxID = 0;
                    for (int i = 0; i < 18; i++)
                    {
                        vAuxID = Convert.ToInt32(dt.Rows[i]["id_grupo"].ToString());
                        vAuxDecimal = Convert.ToDecimal(dt.Rows[i]["deducao_por_mvtc"].ToString());
                        vAuxDecimal -= lMvtc;
                        vDed = vAuxDecimal.ToString().Replace(".", "");
                        vDed = vDed.Replace(",", ".");

                        vSql = "Update grupos set deducao_por_mvtc = " +
                                vDed + ", deduzido = '" + "S" + "' where id_grupo = " + vAuxID;
                        SqlCommand cmd = new SqlCommand(vSql, this.cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                return lMvtc;

            } catch (Exception ex) {
                CSharpUtil.Util.MsgErro(ex.Message);
                return 0;
            }
        }


        public string IsDebidoDeduzido(string gr, ref decimal vCreditoReal)
        {
            string vRetVal = string.Empty;
            string InstrucaoSql = "Select id_grupo, deduzido, deducao_por_mvtc from grupos where id_grupo = " + int.Parse(gr);
            SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, this.cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ded");
            DataTable dt = new DataTable();
            dt = ds.Tables["ded"];
            vRetVal = dt.Rows[0]["deduzido"].ToString();

            // Será retornado por Referência!!!
            vCreditoReal = (Convert.ToDecimal(dt.Rows[0]["deducao_por_mvtc"].ToString()) * 18);
            return vRetVal;
        }


        public void AtualizaDebito(string debito, int id_gp)
        {
            debito = debito.Replace(".", "");
            debito = debito.Replace(",", ".");
            string vSql = "update grupos set debito_previsto = "
                           + debito + ", deducao_por_mvtc = "
                           + debito + ", deduzido = '"
                           + "N" + "' where id_grupo = " + id_gp;
            SqlCommand cmd = new SqlCommand(vSql, this.cn);
            cmd.ExecuteNonQuery();
        }

        public void PreencheCtrlGpDebitoDeduzido(ref decimal[] CtrlGpDebitoDeduzido) {
            string InstrucaoSql = "Select id_grupo, deducao_por_mvtc from grupos order by id_grupo";
            SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, this.cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "deducao");
            DataTable dt = new DataTable();
            dt = ds.Tables["deducao"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    CtrlGpDebitoDeduzido[i] = Convert.ToDecimal(dt.Rows[i]["deducao_por_mvtc"].ToString());
                }
            }
        /* ----------------------------------------- */
        }



     

        #region:: Métodos de Navegação
        public void Proximo()
        {
            curReg++;
            registro_atual = curReg + 1;
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
            registro_atual = curReg + 1;
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
            registro_atual = curReg + 1;
            finalDeArquivo = false;
            SetRst();
        } // Fim de Primeiro;
        #endregion

        #region:: Propriedades do Objeto

        public SqlConnection Cn
        {
            get { return cn; }
            set { cn = value; }
        }        
        public int Id_grupo {
            get { return id_grupo; }
            set { id_grupo = value; }
        }
        public int Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }
        public int Carencia
        {
            get { return carencia; }
            set { carencia = value; }
        }
        public int Qtde_premio
        {
            get { return qtde_premio; }
            set { qtde_premio = value; }
        }
        public decimal Debito_previsto
        {
            get { return debito_previsto; }
            set { debito_previsto = value; }
        }
        public decimal Deducao_por_mvtc
        {
            get { return deducao_por_mvtc; }
            set { deducao_por_mvtc = value; }
        }
        public decimal Credito
        {
            get { return credito; }
            set { credito = value; }
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

        public bool FinalDeArquivo
        {
            get { return finalDeArquivo; }
            set { finalDeArquivo = value; }
        }


        #endregion


        // ------------ //


    }// Fim da Classe;
}//Fim do Namespace;
