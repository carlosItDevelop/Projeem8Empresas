using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Projeem.Controller
{
    public class ControleAplicacao
    {
        SqlConnection cn;
        DataTable dt;
        public ControleAplicacao(SqlConnection cn) {
            this.cn = cn;
        }

        #region:: Método Add para Gravar os Dados de Controle da Aplicação
        public void Add_Contabilidade(SqlConnection cnn, SqlTransaction tr, int id, int gp, int sb,  
                        string dbt_real,  string cto_real,  string sdo_sb_real, string sdo_real_acum ) 
            { 
            try
            {
                #region:: Conversões de , por .
                //----------------------
                dbt_real = dbt_real.Replace(".", "");
                dbt_real = dbt_real.Replace(",", ".");
                //----------------------
                cto_real = cto_real.Replace(".", "");
                cto_real = cto_real.Replace(",", ".");
                //----------------------
                sdo_sb_real = sdo_sb_real.Replace(".", "");
                sdo_sb_real = sdo_sb_real.Replace(",", ".");
                //----------------------
                sdo_real_acum = sdo_real_acum.Replace(".", "");
                sdo_real_acum = sdo_real_acum.Replace(",", ".");
                //----------------------
                #endregion

                SqlCommand cmd = new SqlCommand("Add_Contabilidade", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;

                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@gp", gp));
                cmd.Parameters.Add(new SqlParameter("@sb", sb));
                cmd.Parameters.Add(new SqlParameter("@dbt_real", dbt_real));
                cmd.Parameters.Add(new SqlParameter("@cto_real", cto_real));
                cmd.Parameters.Add(new SqlParameter("@sdo_sb_real", sdo_sb_real));
                cmd.Parameters.Add(new SqlParameter("@sdo_real_acum", sdo_real_acum));

                cmd.ExecuteNonQuery();
                
            } catch (Exception ex) {
                CSharpUtil.Util.MsgErro(ex.Message);
            }


                // ---------------  //
        }//Fim do Método Add;
        #endregion


        public void Upd_Contabilidade(SqlConnection cnn, SqlTransaction tr, int id, int gp, 
                        string dbt_real, string cto_real, string sdo_sb_real, string sdo_real_acum)
        {
            try
            {
                #region:: Conversões de , por .
                //----------------------
                dbt_real = dbt_real.Replace(".", "");
                dbt_real = dbt_real.Replace(",", ".");
                //----------------------
                cto_real = cto_real.Replace(".", "");
                cto_real = cto_real.Replace(",", ".");
                //----------------------
                sdo_sb_real = sdo_sb_real.Replace(".", "");
                sdo_sb_real = sdo_sb_real.Replace(",", ".");
                //----------------------
                sdo_real_acum = sdo_real_acum.Replace(".", "");
                sdo_real_acum = sdo_real_acum.Replace(",", ".");
                //----------------------
                #endregion

                SqlCommand cmd = new SqlCommand("Upd_Contabilidade", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tr;
                cmd.Parameters.Add(new SqlParameter("@gp", gp));
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@dbt_real", dbt_real));
                cmd.Parameters.Add(new SqlParameter("@cto_real", cto_real));
                cmd.Parameters.Add(new SqlParameter("@sdo_sb_real", sdo_sb_real));
                cmd.Parameters.Add(new SqlParameter("@sdo_real_acum", sdo_real_acum));

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }


            // ---------------  //
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cn"></param>
        public static void DeleteAll_WithSProc(SqlConnection cn)
        {
            try
            {
                SqlCommand cmd;


                cmd = new SqlCommand("delete from contabilidade", cn);
                cmd.ExecuteNonQuery();

               // CSharpUtil.Util.MsgInfo("Registros [contabilidade] Excluídos com Sucesso!");
            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }
        }

        public void ExibeDadosContabilidade() {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from contabilidade", this.cn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Exibe_Contabilidade");
                dt = new DataTable();
                dt = ds.Tables["Exibe_Contabilidade"];
                curReg = 0;
                registro_atual = curReg + 1;
                num_registro = dt.Rows.Count;

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

            id_aplicacao = Convert.ToInt32(dt.Rows[curReg]["id_aplicacao"].ToString());
            grupo = Convert.ToInt32(dt.Rows[curReg]["num_gp"].ToString());
            subperiodo = Convert.ToInt32(dt.Rows[curReg]["subperiodo"].ToString());
            debito_real = Convert.ToDecimal(dt.Rows[curReg]["debito_real"].ToString());
            credito_real = Convert.ToDecimal(dt.Rows[curReg]["credito_real"].ToString());
            saldo_sb_real = Convert.ToDecimal(dt.Rows[curReg]["saldo_sb_real"].ToString());
            saldo_real_acumulado = Convert.ToDecimal(dt.Rows[curReg]["saldo_real_acumulado"].ToString());

            

        }

        #region:: Métodos de Navegação
        public void Proximo()
        {
            curReg++;
            if (curReg > num_registro - 1)
            {
                curReg = num_registro - 1;
                finalDeArquivo = true;
                CSharpUtil.Util.MsgInfo("Final de Arquivo!");
            }
            else
            {
                registro_atual = curReg + 1;
                SetRst();
            }
        } // Fim de ProximoRegistro;

        public void Anterior()
        {
            finalDeArquivo = false;
            curReg--;
            if (curReg < 0)
            {
                curReg = 0;
                CSharpUtil.Util.MsgInfo("Início de Arquivo!");
            }
            else
            {
                registro_atual = curReg + 1;
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

        public void Ultimo()
        {
            curReg = num_registro - 1;
            registro_atual = curReg + 1;
            finalDeArquivo = false;
            SetRst();
        } // Fim de Primeiro;

        #endregion

        #region:: Variáveis da Classe
        int id_aplicacao;
        int grupo;
        int subperiodo;
        decimal debito_contabil;
        decimal debito_real;
        decimal credito_contabil;
        decimal credito_real;
        decimal saldo_sb_contabil;
        decimal saldo_sb_real;
        decimal capital_subsidiario;


        int curReg; // ÚNICA SEM PROPRIEDADE!!!
        int registro_atual;
        int num_registro;
        bool finalDeArquivo = false;

        decimal saldo_contabil_acumulado;
        decimal saldo_real_acumulado;




        #endregion

        #region:: Propriedades da Classe

        

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

        public int Id_aplicacao {
            get { return id_aplicacao; }
            set { id_aplicacao = value; }
        }
        public int Grupo {
            get { return grupo; }
            set { grupo = value; }
        }
        public int Subperiodo {
            get { return subperiodo; }
            set { subperiodo = value; }
        }
        public decimal Debito_contabil {
            get { return debito_contabil; }
            set { debito_contabil = value; }
        }
        public decimal Debito_real {
            get { return debito_real; }
            set { debito_real = value; }
        }
        public decimal Credito_contabil {
            get { return credito_contabil; }
            set { credito_contabil = value; }
        }
        public decimal Credito_real {
            get { return credito_real; }
            set { credito_real = value; }
        }
        public decimal Saldo_sb_contabil {
            get { return saldo_sb_contabil; }
            set { saldo_sb_contabil = value; }
        }
        public decimal Saldo_sb_real {
            get { return saldo_sb_real; }
            set { saldo_sb_real = value; }
        }
        public decimal Capital_subsidiario {
            get { return capital_subsidiario; }
            set { capital_subsidiario = value; }
        }


        public decimal Saldo_contabil_acumulado {
            get { return saldo_contabil_acumulado; }
            set { saldo_contabil_acumulado = value; }
        }
        public decimal Saldo_real_acumulado
        {
            get { return saldo_real_acumulado; }
            set { saldo_real_acumulado = value; }
        }

        public bool FinalDeArquivo
        {
            get { return finalDeArquivo; }
            set { finalDeArquivo = true; }
        }


        #endregion


        // ------------ //
    }//Fim da Classe;
}//Fim do NameSpace;
