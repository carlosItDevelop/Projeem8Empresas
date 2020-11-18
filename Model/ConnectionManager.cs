using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CamadaDeDados.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace Projeem.Model
{
    public class ConnectionManager: IConnect
    {

        private string strConn;
        private SqlConnection objConn = null;
        private String server;
        private String database;

        
        public ConnectionManager(SqlConnection cn) {
            this.objConn = cn;
        }

        #region::  IConnect Membros
        public bool Conectar()
        {
            try {
                strConn = "Data Source=" + this.server + ";Initial Catalog=" + this.database + ";Integrated Security=True;";
                objConn.ConnectionString = strConn;
                objConn.Open();
                CSharpUtil.Util.SetaValGlobal(objConn);
            }
            catch {
                return false;
            }
            return true;
        }

        public bool Desconectar()
        {
            try
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
            } catch {
                return false;
            }
            return true;
        }

        public DataTable RetornaDataTable(string p_strConn, List<SqlParameter> p_lstParam, string p_NomeDaTabela)
        {
            if (!this.Conectar()) {
                return null;
            }
            SqlCommand cmd = new SqlCommand(p_strConn, objConn);
            foreach (SqlParameter param in p_lstParam)
            {
                cmd.Parameters.Add(param);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try { 
                da.Fill(ds,p_NomeDaTabela);
            } catch {
                return null;
            }
            this.Desconectar(); 
            return ds.Tables[p_NomeDaTabela];

        }

        public bool ExecutaComando(string strSql, List<SqlParameter> p_lstParam)
        {
            bool vRetval = false;
            if (!this.Conectar()) {
                return false;
            }
            SqlCommand cmd = new SqlCommand(strSql, objConn);
            foreach (SqlParameter param in p_lstParam) {
                cmd.Parameters.Add(param);
            }
            try
            {
                vRetval = (cmd.ExecuteNonQuery() > 0 ? true : false);
            }
            catch {
                vRetval = false;
            }

            this.Desconectar();
            return vRetval;
        }
        #endregion:: IConnect Membros

        #region: Propriedades

        public SqlConnection Conexao {
            get { return this.objConn; }
        }

        public string GetStrConnection
        {
            get { return this.strConn; }
        }

        public String Server
        {
            get { return server; }
            set { server = value; }
        }

        public String Database
        {
            get { return database; }
            set { database = value; }
        }


        #endregion: Propriedades

        /*------------ */
    }// Fim da Classe
}// Fim do namespace
