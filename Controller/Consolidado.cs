using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Projeem.Controller;

namespace Controller
{
    public class Consolidado
    {
        public Consolidado() {
        }



        public void AddConsolidado(SqlConnection cn, int id, string referencia, DateTime dt_fechamento, int MaxSb, decimal saldo)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("AddConsolidado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.Transaction = trans;

                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@referencia", referencia));
                cmd.Parameters.Add(new SqlParameter("@dt_fechamento", dt_fechamento));
                cmd.Parameters.Add(new SqlParameter("@MaxSb", MaxSb));
                cmd.Parameters.Add(new SqlParameter("@saldo", saldo));

                cmd.ExecuteNonQuery();
                CSharpUtil.Util.Msg("Consolidado Gravado com Sucesso!");

            }
            catch (Exception ex)
            {
                CSharpUtil.Util.MsgErro(ex.Message);
            }


        }//Fim do Método Add;

        
        public static decimal GetSaldoAnteriorConsolidado(SqlConnection MyConn, string referencia) {

            decimal vRetVal = decimal.MinValue;
            string InstrucaoSql = "Select sum(saldo) as saldo_anterior from consolidado where referencia = '" + referencia + "'";
            SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, MyConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "con");
            DataTable dt = new DataTable();
            dt = ds.Tables["con"];
            
            vRetVal = Convert.ToDecimal(dt.Rows[0]["saldo_anterior"].ToString());
            //vRetVal = 0;
            return vRetVal;

        }


        public static void ZeraContabilidade(SqlConnection cn) {

            try
            {
                Indices.ZeraContadores(cn, "Processing_115");
                Indices.ZeraContadores(cn);
                Indices.UpdateStatusProcessamento("DEBITO", cn);
                ControleAplicacao.DeleteAll_WithSProc(cn);
                Grupos.ZeraDebitosEDeducoes(cn);
            }
            catch (Exception ex) {
                CSharpUtil.Util.Msg(ex.Message);
            }

        }


    }
}
