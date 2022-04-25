using CursosExamen.Models;
using CursosExamen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursosExamen.Controllers
{
    public class InscripcionesController : Controller
    {
        AlumnoViewModel AVM;
        ModalidaViewModel MVM;
        CursoViewModel CVM;
        InscripcionViewModel IVM;

        // GET: Inscripciones
        public ActionResult Index()
        {

            IVM = new InscripcionViewModel();
            IEnumerable<Inscripcion> inscripciones = IVM.ListarInscriciones();
            return View(inscripciones);
        }

        // GET: Inscripciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inscripciones/Create
        public ActionResult Create()
        {

            MVM = new ModalidaViewModel();
            AVM = new AlumnoViewModel();

            ViewBag.listaModalidades = new SelectList(MVM.GetModalidades(), "Id", "Tipo");

          
            ViewBag.alumnos = AVM.GetAlumnos();


            return View();
        }

        // POST: Inscripciones/Create
        [HttpPost]
        public ActionResult Create(Inscripcion inscripcion)
        {

            IVM = new InscripcionViewModel();
            MVM = new ModalidaViewModel();
            AVM = new AlumnoViewModel();
     

            try
            {
                ViewBag.listaModalidades = new SelectList(MVM.GetModalidades(), "Id", "Tipo");

                IEnumerable<Alumno> listaAlumnos = AVM.GetAlumnos();
                ViewBag.alumnos = listaAlumnos;
              

                if (ModelState.IsValid)
                {

                    // Valido que la persona no esté inscripta en un curso de la misma modalidad
                    if (IVM.AlumnoRepetido(inscripcion))
                    {
                        ModelState.AddModelError("InfoAlumno", "Esta persona ya se encuentra registrada en un curso de esta modalidad");

                        return View(inscripcion);
                    }

                    bool insertExitoso;
                    insertExitoso = IVM.Insert(inscripcion);

                    if (insertExitoso)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("Id", "Error al guardar la información");
                        return View(inscripcion);
                    }
                }
                else
                {
                    return View(inscripcion);
                }

            }
            catch (Exception ex)
            {

                ViewBag.listaModalidades = new SelectList(MVM.GetModalidades(), "Id", "Tipo");
                ViewBag.alumnos = AVM.GetAlumnos();

                return View();
            }
        }

        [HttpPost]
        public JsonResult GetCursos(int modalidadId)
        {
            CVM = new CursoViewModel();

            ViewBag.listaCursos = CVM.GetCursosByModalidadId(modalidadId); 
            return Json(ViewBag.listaCursos, JsonRequestBehavior.AllowGet);

        }

        // GET: Inscripciones/Edit/5
        public ActionResult Edit(int id)
        {

            IVM = new InscripcionViewModel();
            MVM = new ModalidaViewModel();
            AVM = new AlumnoViewModel();
            CVM = new CursoViewModel();

           
            Inscripcion inscripcion = IVM.GetInscripcionById(id);

            IEnumerable<Curso> cursos = CVM.GetCursosByModalidadId(inscripcion.ModalidadId);

            ViewBag.listaCursos = cursos;
            ViewBag.listaCursos2 = new SelectList(cursos, "Id", "Nombre");
            ViewBag.listaModalidades = new SelectList(MVM.GetModalidades(), "Id", "Tipo");
            ViewBag.alumnos = AVM.GetAlumnos();

            return View(inscripcion);
        }

        // POST: Inscripciones/Edit/5
        [HttpPost]
        public ActionResult Edit(Inscripcion inscripcion)
        {

            IVM = new InscripcionViewModel();
            MVM = new ModalidaViewModel();
            AVM = new AlumnoViewModel();
            CVM = new CursoViewModel();

            try
            {
                ViewBag.listaModalidades = new SelectList(MVM.GetModalidades(), "Id", "Tipo");

                IEnumerable<Alumno> listaAlumnos = AVM.GetAlumnos();
                ViewBag.alumnos = listaAlumnos;

                IEnumerable<Curso> cursos = CVM.GetCursosByModalidadId(inscripcion.ModalidadId);

                ViewBag.listaCursos = cursos;
                ViewBag.listaCursos2 = new SelectList(cursos, "Id", "Nombre");


                if (ModelState.IsValid)
                {

                    if (IVM.AlumnoRepetido(inscripcion))
                    {
                        ModelState.AddModelError("InfoAlumno", "Esta persona ya se encuentra registrada en un curso de esta modalidad");

                        return View(inscripcion);
                    }

                    bool updateExitoso;
                    updateExitoso = IVM.Update(inscripcion);

                    if (updateExitoso)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("Id", "Error al guardar la información");
                        return View(inscripcion);
                    }
                }
                else
                {
                    return View(inscripcion);
                }

            }
            catch (Exception ex)
            {

                IEnumerable<Curso> cursos = CVM.GetCursosByModalidadId(inscripcion.ModalidadId);

                ViewBag.listaCursos = cursos;
                ViewBag.listaCursos2 = new SelectList(cursos, "Id", "Nombre");

                ViewBag.listaModalidades = new SelectList(MVM.GetModalidades(), "Id", "Tipo");
                ViewBag.alumnos = AVM.GetAlumnos();

                return View();
            }

        }

       

        [HttpPost]
        public JsonResult Delete(int id)
        {

            IVM = new InscripcionViewModel();

            return Json(IVM.Delete(id));
        }

       
    }
}
