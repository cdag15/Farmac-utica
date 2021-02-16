using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmacéutica.Models;

namespace Farmacéutica.Controllers
{
    public class EmpleadosController : Controller
    {
        private PruebaEntities db = new PruebaEntities();

        
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Estados);
            return View(empleados.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        
        public ActionResult Create()
        {
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Estado");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(Models.ViewModel.EmpleadosViewModel model)
        {
            try
            {

            
            if (ModelState.IsValid)
            {

                using (PruebaEntities DTB = new PruebaEntities())
                {
                    /**/
                    int total = DTB.Empleados.Count();
                    var oTabla = new Empleados();
                    oTabla.ID_Empleado = total + 1;
                    oTabla.Nombre = model.Nombre;
                    oTabla.Apellido = model.Apellido;
                    oTabla.Telefono = model.Telefono;
                    oTabla.Correo = model.Correo;
                    oTabla.ID_Estado = model.ID_Estado;
                    //if (DTB.Estados.Where(d => d.ID_Estado == model.ID_Estado).Count() > 0)
                    //{

                    //}
                    db.Empleados.Add(oTabla);

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Estado", empleados.ID_Estado);
            return View(empleados);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Empleado,Nombre,Apellido,Telefono,Correo,ID_Estado")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Estado = new SelectList(db.Estados, "ID_Estado", "Estado", empleados.ID_Estado);
            return View(empleados);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
