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
    public class CursoViewModel
    {

        Conexion cn = new Conexion();

        public IEnumerable<Curso> GetCursosByModalidadId(int modalidadId)
        {

            List<Curso> cursos = new List<Curso>();

            Curso curso;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_listar_cursos", cn.Conectar());

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("modalidad_id", modalidadId);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    curso = new Curso();

                    curso.Id = Convert.ToInt32(dr["Id"]);
                    curso.Legajo = Convert.ToInt32(dr["legajo"]);
                    curso.Nombre = dr["nombre"].ToString();
                    curso.Descripcion = dr["descripcion"].ToString();
                    curso.ModalidadId = Convert.ToInt32(dr["modalidad_id"]);


                    cursos.Add(curso);

                }
                dr.Close();
                cn.Desconectar();
            }
            catch
            {

            }

            return cursos;
        }


    }
}