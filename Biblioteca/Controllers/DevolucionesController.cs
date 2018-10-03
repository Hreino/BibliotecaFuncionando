using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class DevolucionesController : Controller
    {
        private Model db = new Model();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var devoluciones = db.Devoluciones.Include(d => d.Prestamo);
            return View(devoluciones.ToList());
        }

        // GET: Devoluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            if (devoluciones == null)
            {
                return HttpNotFound();
            }
            return View(devoluciones);
        }

        // GET: Devoluciones/Create
        public ActionResult Create()
        {
            ViewBag.ListadoDePrestamos = db.Prestamo.ToList();
            ViewBag.Id_prestamo = new SelectList(db.Prestamo, "Id_prestamo", "Id_prestamo");
            return View();
        }

        // POST: Devoluciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_devoluciones,Id_prestamo,FechaDevolucion,Observaciones")] Devoluciones devoluciones)
        {
           

            if (ModelState.IsValid)
            {
                
                db.Devoluciones.Add(devoluciones);
                db.SaveChanges();
                Prestamo prestamo = db.Prestamo.Find(devoluciones.Id_prestamo);
                foreach (var item in prestamo.detallePrestamo)
                {
                    Libro libro = db.Libro.Find(item.Libro.Id_libro);
                    libro.Cantidad = libro.Cantidad + item.cantidad;
                    db.Entry(libro).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            ViewBag.Id_prestamo = new SelectList(db.Prestamo, "Id_prestamo", "Estado", devoluciones.Id_prestamo);
            return View(devoluciones);
        }

        // GET: Devoluciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            if (devoluciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_prestamo = new SelectList(db.Prestamo, "Id_prestamo", "Id_prestamo", devoluciones.Id_prestamo);
            return View(devoluciones);
        }

        // POST: Devoluciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_devoluciones,Id_prestamo,FechaDevolucion,Observaciones")] Devoluciones devoluciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devoluciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_prestamo = new SelectList(db.Prestamo, "Id_prestamo", "Estado", devoluciones.Id_prestamo);
            return View(devoluciones);
        }

        // GET: Devoluciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            if (devoluciones == null)
            {
                return HttpNotFound();
            }
            return View(devoluciones);
        }

        // POST: Devoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            db.Devoluciones.Remove(devoluciones);
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
