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
    public class detallePrestamoesController : Controller
    {
        private Model db = new Model();

        // GET: detallePrestamoes
        public ActionResult Index()
        {
            var detallePrestamo = db.detallePrestamo.Include(d => d.Libro).Include(d => d.Prestamo);
            return View(detallePrestamo.ToList());
        }

        // GET: detallePrestamoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallePrestamo detallePrestamo = db.detallePrestamo.Find(id);
            if (detallePrestamo == null)
            {
                return HttpNotFound();
            }
            return View(detallePrestamo);
        }

        // GET: detallePrestamoes/Create
        public ActionResult Create()
        {
            ViewBag.idLibro = new SelectList(db.Libro, "Id_libro", "Titulo");
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "Id_prestamo", "Estado");
            return View();
        }

        // POST: detallePrestamoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idLibro,idPrestamo,cantidad")] detallePrestamo detallePrestamo)
        {
            if (ModelState.IsValid)
            {
                db.detallePrestamo.Add(detallePrestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLibro = new SelectList(db.Libro, "Id_libro", "Titulo", detallePrestamo.idLibro);
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "Id_prestamo", "Estado", detallePrestamo.idPrestamo);
            return View(detallePrestamo);
        }

        // GET: detallePrestamoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallePrestamo detallePrestamo = db.detallePrestamo.Find(id);
            if (detallePrestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLibro = new SelectList(db.Libro, "Id_libro", "Titulo", detallePrestamo.idLibro);
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "Id_prestamo", "Estado", detallePrestamo.idPrestamo);
            return View(detallePrestamo);
        }

        // POST: detallePrestamoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idLibro,idPrestamo,cantidad")] detallePrestamo detallePrestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePrestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLibro = new SelectList(db.Libro, "Id_libro", "Titulo", detallePrestamo.idLibro);
            ViewBag.idPrestamo = new SelectList(db.Prestamo, "Id_prestamo", "Estado", detallePrestamo.idPrestamo);
            return View(detallePrestamo);
        }

        // GET: detallePrestamoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallePrestamo detallePrestamo = db.detallePrestamo.Find(id);
            if (detallePrestamo == null)
            {
                return HttpNotFound();
            }
            return View(detallePrestamo);
        }

        // POST: detallePrestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detallePrestamo detallePrestamo = db.detallePrestamo.Find(id);
            db.detallePrestamo.Remove(detallePrestamo);
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
