using CursosExamen.Models;
using CursosExamen.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CursosExamen.ViewModel
{
    public class ModalidaViewModel
    {
        Conexion cn = new Conexion();

        public IEnumerable<Modalidad> GetModalidades()
        {

            List<Modalidad> modalidades = new List<Modalidad>();

            Modalidad modalidad;

            SqlCommand cmd = new SqlCommand("SP_listar_modalidades", cn.Conectar());

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                modalidad = new Modalidad();

                modalidad.Id = Convert.ToInt32(dr["Id"]);
                modalidad.Tipo = dr["tipo"] is DBNull ? "-" : dr["tipo"].ToString();

                modalidades.Add(modalidad);

            }
            dr.Close();
            cn.Desconectar();

            return modalidades;
        }

    }
}