using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CursosExamen.Tools
{
    public class Conexion
    {

        SqlConnection cn;

        public SqlConnection Conectar()
        {

            // Realizo la conexión a la base de datos usando la cadena de conexión que se encuentra en Web.config
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString + ";Pooling=False");

            if (cn.State == ConnectionState.Closed)
                cn.Open();

            return cn;
        }

       
        public void Desconectar()
        {
            if (cn != null)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                cn = null;
            }

        }

    }
}