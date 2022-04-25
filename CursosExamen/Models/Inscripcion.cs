using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursosExamen.Models
{
    public class Inscripcion
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int AlumnoId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Seleccionar un curso")]
        public int CursoId { get; set; }

       
        [Display(Name = "Nombre Curso")]
        public string CursoNombre { get; set; }

        public string Descripcion { get; set; }

        // Nombre del alumno
        [Required(ErrorMessage = "Campo obligatorio.")]
       
        public string Nombre { get; set; }

      
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Seleccione una persona.")]
        [Display(Name = "Alumno")]
        public string InfoAlumno { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int Dni { get; set; }

        public int GeneroId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Genero { get; set; }


        [Required(ErrorMessage = "Campo obligatorio.")]
        public int Edad { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Seleccionar un curso")]
        public int ModalidadId { get; set; }


        [Display(Name = "Modalidad")]
        public string Modalidad { get; set; }

        [Display(Name = "Fecha Inscripción")]
        public DateTime FechaInscripcion { get; set; }



    }
}