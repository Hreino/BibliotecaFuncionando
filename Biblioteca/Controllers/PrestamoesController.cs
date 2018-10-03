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
    public class PrestamoesController : Controller
    {
        private Model db = new Model();
        static List<detallePrestamo> detalleAux = new List<detallePrestamo>();

        // GET: Prestamoes
        public ActionResult Index()
        {
            var prestamo = db.Prestamo.Include(p => p.Cliente).Include(p => p.Usuarios);
            return View(prestamo.ToList());
        }

        /*Comienzo de Busqueda*/
        public ActionResult busqueda5(String f)
        {
            var Prestamo = db.Prestamo.Include(Cliente => Cliente.Cliente);
            var busqueda5 = from a in Prestamo.ToList() where a.Cliente.Nombre.Contains(f) select a;
            return PartialView(busqueda5);
        }
        /*Fin de Busqueda*/

        // GET: Prestamoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        public ActionResult DetallesPrestamo(int IdLibro, int cantidad)
        {
            if ((IdLibro > 0 || IdLibro != null) && (cantidad > 0 || cantidad != null))
            {
                detallePrestamo item = new detallePrestamo();

                item.Libro = db.Libro.Find(IdLibro);
                item.idLibro = IdLibro;
                item.cantidad = cantidad;

                detalleAux.Add(item);


                return PartialView(detalleAux);
            }
            else
            {
                return PartialView(detalleAux);
            }

        }



        // GET: Prestamoes/Create
        public ActionResult Create()
        {

            ViewBag.ListadoLibros = db.Libro.ToList();


            ViewBag.Id_cliente = new SelectList(db.Cliente, "Id_cliente", "Nombre");
            ViewBag.Id_usuario = new SelectList(db.Usuarios, "Id_usuario", "Nombre");
            return View();
        }

        // POST: Prestamoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_prestamo,Fecha_entrega,Fecha_devolucion,Estado,Id_usuario,Id_cliente")] Prestamo prestamo)
        {
            foreach (var item in detalleAux)
            {

                prestamo.detallePrestamo.Add(new detallePrestamo
                {
                    idLibro = item.idLibro,
                    idPrestamo = prestamo.Id_prestamo,
                    cantidad = item.cantidad
                    
                });
            }

            
            if (ModelState.IsValid)
            {

                db.Prestamo.Add(prestamo);
                db.SaveChanges();
                foreach (var item in detalleAux)
                {
                    Libro libro = db.Libro.Find(item.idLibro);
                    libro.Cantidad = libro.Cantidad - item.cantidad;
                    db.Entry(libro).State = EntityState.Modified;
                    db.SaveChanges();
                }
                detalleAux = new List<detallePrestamo>();
                return RedirectToAction("Index");
            }

            ViewBag.Id_cliente = new SelectList(db.Cliente, "Id_cliente", "Nombre", prestamo.Id_cliente);
            ViewBag.Id_usuario = new SelectList(db.Usuarios, "Id_usuario", "Nombre", prestamo.Id_usuario);
            return View(prestamo);
        }

        // GET: Prestamoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cliente = new SelectList(db.Cliente, "Id_cliente", "Nombre", prestamo.Id_cliente);
            ViewBag.Id_usuario = new SelectList(db.Usuarios, "Id_usuario", "Nombre", prestamo.Id_usuario);
            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_prestamo,Fecha_entrega,Fecha_devolucion,Estado,Id_usuario,Id_cliente")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_cliente = new SelectList(db.Cliente, "Id_cliente", "Nombre", prestamo.Id_cliente);
            ViewBag.Id_usuario = new SelectList(db.Usuarios, "Id_usuario", "Nombre", prestamo.Id_usuario);
            return View(prestamo);
        }

        // GET: Prestamoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            try
            {
                db.Prestamo.Remove(prestamo);
                db.SaveChanges();

            }
            catch (Exception)
            {
                ViewBag.ErrorEliminar = "El prestamo esta siendo utilizado por un usuario. Imposible eliminar";

                return View("Delete", prestamo);

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
