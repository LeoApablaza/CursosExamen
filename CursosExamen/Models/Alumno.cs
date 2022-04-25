using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursosExamen.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string RazonSocial { get; set; }

        public string InfoAlumno { get; set; }

        public int Dni { get; set; }

        public int GeneroId { get; set; }

        public Genero Genero { get; set; }

        public int Edad { get; set; }

        public DateTime FechaNacimiento { get; set; }


    }
}