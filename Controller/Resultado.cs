using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projeem;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Projeem.Controller
{    
    class Resultado {
        SqlConnection cn;
        int curReg = 0;
        int totalReg = 0;

        int registro_atual;
        int num_registro;

        int id_result_gp;
        int gp;

        bool finalDeArquivo = false;

        public Resultado(SqlConnection cn) {
            this.cn = cn;
        }

        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        public void SetRstResultado() {
            try {
                    string InstrucaoSql = "select * from result_gp";
                    da = new SqlDataAdapter(InstrucaoSql, this.cn);
                    ds = new DataSet();
                    da.Fill(ds, "resultado");

                    dt = ds.Tables["resultado"];
                    curReg = 0;
                    registro_atual = curReg+1;
                    totalReg = dt.Rows.Count;
                    num_registro = totalReg;

                    if (num_registro > 0) SetRst();

                } catch (Exception ex) {
                   CSharpUtil.Util.MsgErro(ex.Message);
            }
        } // Fim de SetRstResultado;


        public void ProximoReg() {
            curReg++;
            registro_atual = curReg+1;
            if (curReg > totalReg - 1) {
                curReg = totalReg - 1;
                finalDeArquivo = true;
                //CSharpUtil.Util.MsgInfo("Final de Arquivo!");
            } else {
                SetRst();
            }
        } // Fim de ProximoRegistro;

        public void AnteriorReg() {
            curReg--;
            registro_atual = curReg+1;
            finalDeArquivo = false;
            if (curReg < 0)
            {
                curReg = 0;
                CSharpUtil.Util.MsgInfo("Início de Arquivo!");
            } else {
                SetRst();
            }
        } // Fim de AnteriorRegistro;

        public void Primeiro()
        {
            curReg = 0;
            registro_atual = curReg+1;
            finalDeArquivo = false;
            SetRst();
        } // Fim de Primeiro;



        void SetRst() {
            id_result_gp = Convert.ToInt32(dt.Rows[curReg]["id_result_gp"].ToString());
            gp = Convert.ToInt32( dt.Rows[curReg]["gp"].ToString());
        }

        public bool FinalDeArquivo{
            get { return finalDeArquivo;}
            set { finalDeArquivo = value;}
        }

        public int Id_result_gp {
            get { return id_result_gp; }
            set { id_result_gp = value; }
        }

        public int Gp
        {
            get { return gp; }
            set { gp = value; }
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


    } // Fim da Classe;

    // ----------------------------------------------- //
} // Fim do NameSpace;
