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
    public class EstadosController : Controller
    {
        private PruebaEntities db = new PruebaEntities();

        
        public ActionResult Index()
        {
            return View(db.Estados.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return View(estados);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ViewModel.EstadosViewModel model)
        {
            try
            {



                if (ModelState.IsValid)
                {

                    using (PruebaEntities DTB = new PruebaEntities())
                    {
                        /**/
                        var oTabla = new Estados();
                        oTabla.ID_Estado = model.ID_Estado;
                        oTabla.Estado = model.Estado;
                        if (DTB.Estados.Where(d => d.ID_Estado == model.ID_Estado).Count() > 0)
                        {

                        }
                        db.Estados.Add(oTabla);

                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return View(estados);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Estado,Estado")] Estados estados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estados);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return View(estados);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {




            Estados estados = db.Estados.Find(id);
            db.Estados.Remove(estados);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                   ex.InnerException.InnerException != null &&
                   ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "El registro no puede borrarse porque tiene otros registros relacionados.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return View(estados);
            }


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
