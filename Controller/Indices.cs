using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Projeem.Controller
{
    public class Indices
    {
        SqlConnection cn;

        public static string dbInUse = "";
 
        public Indices(SqlConnection cn) {
            this.cn = cn;
        }


        public static void ZeraContadores(SqlConnection con)
        {
            string InstrucaoSql = "update indice set sb_atual = 1, MaxCGContabil = 0, MaxCGReal = 0";
            SqlCommand cmd = new SqlCommand(InstrucaoSql, con);
            cmd.ExecuteNonQuery();
            //CSharpUtil.Util.MsgInfo("Tabela [ Indice ] Atualizada com Sucesso!");
            cmd = null;
        }

        public static void ZeraContadores(SqlConnection con, string status)
        {
            SqlCommand cmd = new SqlCommand("update series set carencia = 0, mor_carencia = 0, carencia_recap = 0, qtde_premio = 0, status = '" + status + "'", con);
            cmd.ExecuteNonQuery();
            //CSharpUtil.Util.MsgInfo("Tabela [ Series ] Atualizada com Sucesso!");
        }


        

        public static string GetMaxCGContabil(SqlConnection cn)
        {
            string retVal = "0.00";
            string InstrucaoSql = "Select MaxCGContabil from Indice";

            SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "indiceMaxCG");
            DataTable dt = ds.Tables["indiceMaxCG"];

            retVal = dt.Rows[0]["MaxCGContabil"].ToString();
            return retVal;
        }

        public static string GetMaxCGReal(SqlConnection cn)
        {
            string retVal = "0.00";
            string InstrucaoSql = "Select MaxCGReal from Indice";

            SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "indiceCGReal");
            DataTable dt = ds.Tables["indiceCGReal"];

            retVal = dt.Rows[0]["MaxCGReal"].ToString();
            return retVal;
        }



        public static int GetSubperiodo(SqlConnection cn) {
            int retVal = 0;
            string InstrucaoSql = "Select sb_atual from Indice";
            SqlDataAdapter da = new SqlDataAdapter(InstrucaoSql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "indice3");
            DataTable dt = ds.Tables["indice3"];
            retVal = int.Parse(dt.Rows[0]["sb_atual"].ToString());
            return retVal;
        }

        public static void AddSubperiodo(SqlConnection con, SqlTransaction tr){
            string InstrucaoSql = "update indice set sb_atual = sb_atual + 1";
            SqlCommand cmd = new SqlCommand(InstrucaoSql, con);
            cmd.Transaction = tr;
            cmd.ExecuteNonQuery();
            cmd = null;
        }

        public static void SetMaxCGContabil(string vMaxCGContabil, SqlConnection con)
        {
            string InstrucaoSql = "update indice set MaxCGContabil = '" + vMaxCGContabil + "'";
            SqlCommand cmd = new SqlCommand(InstrucaoSql, con);
            cmd.ExecuteNonQuery();
            cmd = null;
        }

        public static void SetMaxCGReal(string vMaxCGReal, SqlConnection con)
        {
            string InstrucaoSql = "update indice set MaxCGReal = '" + vMaxCGReal + "'";
            SqlCommand cmd = new SqlCommand(InstrucaoSql, con);
            cmd.ExecuteNonQuery();
            cmd = null;
        }

  

        public static void UpdateStatusProcessamento(string status, SqlConnection cn) {
            string vSql = "UPDATE indice SET status_processamento = '" + status + "'";
            SqlCommand cmd = new SqlCommand(vSql, cn);
            cmd.ExecuteNonQuery();
            cmd = null;
        }

        public static string GetStatusProcessamento(SqlConnection cn)
        {
            string vSql = "select status_processamento from indice";
            SqlDataAdapter da = new SqlDataAdapter(vSql, cn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "status");
            dt = ds.Tables["status"];
            return dt.Rows[0]["status_processamento"].ToString();
        }

        public static void SetIdContabilidade(string id, SqlConnection cn, SqlTransaction tr)
        {
            string vSql = "UPDATE indice SET id_contabilidade = " + id;
            SqlCommand cmd = new SqlCommand(vSql, cn);
            cmd.Transaction = tr;
            cmd.ExecuteNonQuery();
            cmd = null;
        }

        public static int GetIdContabilidade(SqlConnection cn)
        {
            string vSql = "select id_contabilidade from indice";
            SqlDataAdapter da = new SqlDataAdapter(vSql, cn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "idctb");
            dt = ds.Tables["idctb"];
            return int.Parse(dt.Rows[0]["id_contabilidade"].ToString());
        }



    // ------------ //

    }// Fim da Classe;
}// Fim do NameSpace;
