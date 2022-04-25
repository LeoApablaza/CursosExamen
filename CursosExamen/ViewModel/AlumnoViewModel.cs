using CursosExamen.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CursosExamen.ViewModel
{
    public class AlumnoViewModel
    {

        public IEnumerable<Alumno> GetAlumnos()
        {
            List<Alumno> L = new List<Alumno>();

            try
            {
                // Traigo el listado de personas de la API
                string url = $"https://weblogin.muninqn.gov.ar/api/Examen";
                string respuesta = GetHttp(url);
                Respuesta objRespuesta = JsonConvert.DeserializeObject<Respuesta>(respuesta);


                foreach (var result in objRespuesta.value)
                {
                    Alumno alumno = new Alumno();

                    alumno.Id = Convert.ToInt32(result["id"]);
                    alumno.Dni = Convert.ToInt32(result["dni"]);
                    alumno.RazonSocial = result["razonSocial"].ToString();
                    alumno.Genero = JsonConvert.DeserializeObject<Genero>(result["genero"].ToString());
                    alumno.FechaNacimiento = Convert.ToDateTime(result["fechaNacimiento"]);

                    alumno.Apellido = alumno.RazonSocial.Substring(0, alumno.RazonSocial.IndexOf(","));
                    alumno.Nombre = alumno.RazonSocial.Substring(alumno.RazonSocial.LastIndexOf(",") + 1);

                    alumno.InfoAlumno = $"{alumno.Id} - {alumno.RazonSocial} - {alumno.Dni}";

                    int edadDias = (DateTime.Now - alumno.FechaNacimiento).Days;

                    alumno.Edad = edadDias / 365;


                    L.Add(alumno);


                }

                JArray json_alumnos = JArray.Parse(objRespuesta.value);

                
            }
            catch (Exception ex)
            {
                
            }

            return L;
        }


        // Función para realizar una petición HttpGet
        private string GetHttp(string url)
        {

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

      

    }
}