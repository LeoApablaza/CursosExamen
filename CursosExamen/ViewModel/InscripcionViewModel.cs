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
    public class InscripcionViewModel
    {
        SqlCommand cmd;
        Conexion cn;
        SqlDataReader dr;
        public bool AlumnoRepetido(Inscripcion inscripcion)
        {
            int i;

            try
            {

                cn = new Conexion();
                cmd = new SqlCommand("SP_verificar_inscripcion");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_inscripcion",inscripcion.Id);
                cmd.Parameters.AddWithValue("@id_alumno", inscripcion.AlumnoId);
                cmd.Parameters.AddWithValue("@modalidad_id", inscripcion.ModalidadId);

                cmd.Connection = cn.Conectar();

                i = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                i = 1;
            }
            finally
            {
            
                cn.Desconectar();
                cmd.Dispose();

            }


            if (i > 0)
                return true;
            else
                return false;
        }

        public bool Insert(Inscripcion inscripcion)
        {
            try
            {

                cn = new Conexion();

                cmd = new SqlCommand("SP_crear_inscripcion");

                cmd.Connection = cn.Conectar();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_alumno", inscripcion.AlumnoId);
                cmd.Parameters.AddWithValue("@nombre_alumno", inscripcion.Nombre);
                cmd.Parameters.AddWithValue("@apellido_alumno", inscripcion.Apellido);
                cmd.Parameters.AddWithValue("@dni_alumno", inscripcion.Dni);
                cmd.Parameters.AddWithValue("@genero_alumno", inscripcion.Genero);
                cmd.Parameters.AddWithValue("@edad_alumno", inscripcion.Edad);
                cmd.Parameters.AddWithValue("@curso_id", inscripcion.CursoId);
           
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                    return true;

            }
            catch (Exception ex)  
            {
                string error = ex.Message;
            }
            finally
            {

                cn.Desconectar();
                cmd.Dispose();

            }

            return false;
        }

        public bool Update(Inscripcion inscripcion)
        {
            try
            {

                cn = new Conexion();

                cmd = new SqlCommand("SP_editar_inscripcion");

                cmd.Connection = cn.Conectar();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", inscripcion.Id);
                cmd.Parameters.AddWithValue("@id_alumno", inscripcion.AlumnoId);
                cmd.Parameters.AddWithValue("@nombre_alumno", inscripcion.Nombre);
                cmd.Parameters.AddWithValue("@apellido_alumno", inscripcion.Apellido);
                cmd.Parameters.AddWithValue("@dni_alumno", inscripcion.Dni);
                cmd.Parameters.AddWithValue("@genero_alumno", inscripcion.Genero);
                cmd.Parameters.AddWithValue("@edad_alumno", inscripcion.Edad);
                cmd.Parameters.AddWithValue("@curso_id", inscripcion.CursoId);

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                    return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {

                cn.Desconectar();
                cmd.Dispose();

            }

            return false;
        }

        public IEnumerable<Inscripcion> ListarInscriciones()
        {
            List<Inscripcion> inscripciones = new List<Inscripcion>();
            Inscripcion inscripcion;

            try
            {

                cn = new Conexion();
                SqlCommand cmd = new SqlCommand("SP_listar_inscripciones");

                cmd.Connection = cn.Conectar();
                cmd.CommandType = CommandType.StoredProcedure;

                
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    inscripcion = new Inscripcion();
                    inscripcion.Id = Convert.ToInt32(dr["id"]);
                    inscripcion.CursoId = Convert.ToInt32(dr["curso_id"]);
                    inscripcion.CursoNombre = dr["curso_nombre"].ToString();
                    inscripcion.ModalidadId = Convert.ToInt32(dr["modalidad_id"]);
                    inscripcion.Modalidad = dr["tipo"].ToString();
                    inscripcion.AlumnoId = Convert.ToInt32(dr["id_alumno"]);
                    inscripcion.Apellido = dr["apellido_alumno"].ToString();
                    inscripcion.Nombre = dr["nombre_alumno"].ToString();
                    inscripcion.Dni = Convert.ToInt32(dr["dni_alumno"]);
                    inscripcion.Edad = Convert.ToInt32(dr["edad_alumno"]);
                    inscripcion.AlumnoId = Convert.ToInt32(dr["id_alumno"]);
                    inscripcion.Genero = dr["genero_alumno"].ToString();
                    inscripcion.FechaInscripcion = Convert.ToDateTime(dr["fecha_inscripcion"]);

                    inscripcion.InfoAlumno = $"{inscripcion.Apellido} {inscripcion.Nombre}";


                    inscripciones.Add(inscripcion);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                cn.Desconectar();
                dr.Close();
                //cmd.Dispose();
            }


            return inscripciones;
        }

        public Inscripcion GetInscripcionById(int id)
        {

            Inscripcion inscripcion = new Inscripcion();

            try
            {
                cn = new Conexion();

                SqlCommand cmd = new SqlCommand("SP_get_inscripcion", cn.Conectar());

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    inscripcion.Id = Convert.ToInt32(dr["id"]);
                    inscripcion.CursoId = Convert.ToInt32(dr["curso_id"]);
                    inscripcion.CursoNombre = dr["curso_nombre"].ToString();
                    inscripcion.Descripcion = dr["descripcion"].ToString();
                    inscripcion.ModalidadId = Convert.ToInt32(dr["modalidad_id"]);
                    inscripcion.Modalidad = dr["tipo"].ToString();
                    inscripcion.AlumnoId = Convert.ToInt32(dr["id_alumno"]);
                    inscripcion.Apellido = dr["apellido_alumno"].ToString();
                    inscripcion.Nombre = dr["nombre_alumno"].ToString();
                    inscripcion.Dni = Convert.ToInt32(dr["dni_alumno"]);
                    inscripcion.Edad = Convert.ToInt32(dr["edad_alumno"]);
                    inscripcion.AlumnoId = Convert.ToInt32(dr["id_alumno"]);
                    inscripcion.Genero = dr["genero_alumno"].ToString();
                    inscripcion.FechaInscripcion = Convert.ToDateTime(dr["fecha_inscripcion"]);


                    string razonSocial = $"{inscripcion.Apellido}, {inscripcion.Nombre}";

                    inscripcion.InfoAlumno = $"{inscripcion.AlumnoId} - {razonSocial} - {inscripcion.Dni}";

                }
                dr.Close();
                cn.Desconectar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return inscripcion;
        }

        public bool Delete(int id)
        {
            try
            {

                cn = new Conexion();

                cmd = new SqlCommand("SP_eliminar_inscripcion");

                cmd.Connection = cn.Conectar();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
               

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                    return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {

                cn.Desconectar();
                cmd.Dispose();

            }

            return false;
        }


    }
}