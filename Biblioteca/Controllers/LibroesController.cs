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
    public class LibroesController : Controller
    {
        private Model db = new Model();

        // GET: Libroes
        public ActionResult Index()
        {
            var libro = db.Libro.Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Editorial);
            return View(libro.ToList());
        }


        /*Comienzo de Busqueda*/
        public ActionResult busqueda4(String b)
        {
            var libro = db.Libro;
            var busqueda4 = from a in libro.ToList() where a.Titulo.Contains(b) select a;
            return PartialView(busqueda4);
        }
        /*Fin de Busqueda*/



        // GET: Libroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // GET: Libroes/Create
        public ActionResult Create()
        {
            ViewBag.Id_autor = new SelectList(db.Autor, "Id_autor", "Nombre");
            ViewBag.Id_categoria = new SelectList(db.Categoria, "Id_categoria", "Categoria1");
            ViewBag.Id_editorial = new SelectList(db.Editorial, "Id_editorial", "Nombre");
            return View();
        }

        // POST: Libroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_libro,ISBN,Titulo,Edicion,Fecha_lanzamiento,Descripcion,Idioma,NumeroPaginas,Cantidad,Id_categoria,Id_autor,Id_editorial")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Libro.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_autor = new SelectList(db.Autor, "Id_autor", "Nombre", libro.Id_autor);
            ViewBag.Id_categoria = new SelectList(db.Categoria, "Id_categoria", "Categoria1", libro.Id_categoria);
            ViewBag.Id_editorial = new SelectList(db.Editorial, "Id_editorial", "Nombre", libro.Id_editorial);
            return View(libro);
        }

        // GET: Libroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_autor = new SelectList(db.Autor, "Id_autor", "Nombre", libro.Id_autor);
            ViewBag.Id_categoria = new SelectList(db.Categoria, "Id_categoria", "Categoria1", libro.Id_categoria);
            ViewBag.Id_editorial = new SelectList(db.Editorial, "Id_editorial", "Nombre", libro.Id_editorial);
            return View(libro);
        }

        // POST: Libroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_libro,ISBN,Titulo,Edicion,Fecha_lanzamiento,Descripcion,Idioma,NumeroPaginas,Cantidad,Id_categoria,Id_autor,Id_editorial")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_autor = new SelectList(db.Autor, "Id_autor", "Nombre", libro.Id_autor);
            ViewBag.Id_categoria = new SelectList(db.Categoria, "Id_categoria", "Categoria1", libro.Id_categoria);
            ViewBag.Id_editorial = new SelectList(db.Editorial, "Id_editorial", "Nombre", libro.Id_editorial);
            return View(libro);
        }

        // GET: Libroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libro libro = db.Libro.Find(id);
            try
            {
                db.Libro.Remove(libro);
                db.SaveChanges();

            }
            catch (Exception)
            {
                ViewBag.ErrorEliminar = "El libro esta siendo utilizado por un usuario. Imposible eliminar";

                return View("Delete", libro);

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
