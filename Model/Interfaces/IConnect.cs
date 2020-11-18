using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CamadaDeDados.Interfaces
{
    public interface IConnect
    {
        bool Conectar();
        bool Desconectar();
        DataTable RetornaDataTable(string p_strConn, List<SqlParameter> p_lstParam, string P_NomeDaTabela);
        bool ExecutaComando(string strSql, List<SqlParameter> p_lstParam);
    }
}
