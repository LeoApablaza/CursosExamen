using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursosExamen.Models
{
    public class Curso
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Legajo { get; set; }

        public string Descripcion { get; set; }

        public int ModalidadId { get; set; }

        public bool Activo { get; set; }



    }
}