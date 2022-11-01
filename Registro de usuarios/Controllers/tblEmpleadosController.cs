using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Registro_de_usuarios.Models;

namespace Registro_de_usuarios.Controllers

{
    public class tblEmpleadosController : Controller
    {
        private EmpresaEntities db = new EmpresaEntities();

        // GET: tblEmpleados
        public ActionResult Index()
        {
            var model = new EmpleadoModel();
            model.Empleados = db.tblEmpleados.ToList().OrderByDescending(e => e.ID);
            model.Puestos = db.tblCatPuestos.ToList();
            model.Paises = db.tblCatPaises.ToList();
            model.Estados = db.tblCatEstados.ToList();

            return View(model);
        }

        // GET: tblEmpleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entidad = db.tblEmpleados.Find(id);

            if (entidad == null)
            {
                return HttpNotFound();
            }

            var model = new EmpleadoModel
            {
                CurrentEmpleado = entidad,
                CurrentPuesto = db.tblCatPuestos.FirstOrDefault( p => p.idPuesto == entidad.IdPuesto),
                CurrentPaisVAR = db.tblCatPaises.FirstOrDefault(o => o.idPais == entidad.IdPais),
                CurrentEstado = db.tblCatEstados.FirstOrDefault(q => q.idEstado == entidad.IdEstado)
            };

            return View(model);
        }

        // GET: tblEmpleados/Create
        public ActionResult Create()
        {
            this.LoadCatalogoPuestos();
            this.LoadCatalogoPais();
            this.LoadCatalogoEstados();


            return View();
        }

        // POST: tblEmpleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Apellido,IdPuesto,Edad,IdPais,IdEstado,Email")] tblEmpleados tblEmpleados)
        {

            if (db.tblEmpleados.Any(a => a.Email == tblEmpleados.Email))
            {
                ModelState.AddModelError("Email", "Ya existe un usuario regristrado con ese email ");

            }


            if (ModelState.IsValid)
            {
               // return View(tblEmpleados); 
                
                db.tblEmpleados.Add(tblEmpleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.LoadCatalogoPuestos();
            this.LoadCatalogoPais();
            this.LoadCatalogoEstados();

            return View(tblEmpleados);
        }

        // GET: tblEmpleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleados tblEmpleados = db.tblEmpleados.Find(id);
            if (tblEmpleados == null)
            {
                return HttpNotFound();
            }

            this.LoadCatalogoPuestos();
            this.LoadCatalogoPais();
            this.LoadCatalogoEstados();

            return View(tblEmpleados);
        }

        // POST: tblEmpleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Apellido,IdPuesto,Edad,IdPais,IdEstado,Email")] tblEmpleados tblEmpleados)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(tblEmpleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.LoadCatalogoPuestos();
            this.LoadCatalogoPais();
            this.LoadCatalogoEstados();

            return View(tblEmpleados);
        }

        // GET: tblEmpleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblEmpleados tblEmpleados = db.tblEmpleados.Find(id);
            if (tblEmpleados == null)
            {
                return HttpNotFound();
            }

            return View(tblEmpleados);
        }

        // POST: tblEmpleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmpleados tblEmpleados = db.tblEmpleados.Find(id);
            db.tblEmpleados.Remove(tblEmpleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        } 

        private void LoadCatalogoPuestos()
        {
            var catalogo = db.tblCatPuestos
                                 .Select(p => new SelectListItem
                                 {
                                     Value = p.idPuesto.ToString(),
                                     Text = p.Descripcion
                                 }).ToArray();

            ViewBag.Puestos = catalogo;
        }

        public ActionResult LoadCatalogoPais()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst = (from d in db.tblCatPaises
                   select new SelectListItem
                   {
                       Value = d.idPais.ToString(),
                       Text = d.Pais
                   }).ToList();

            ViewBag.Paises = lst;

        }

        public JsonResult LoadCatalogoEstados(int? IdPais)
        {
            List<ElementJsonKey> lstestados = new List<ElementJsonKey>();

            lstestados = (from d in db.tblCatEstados
                          where d.IdPais == IdPais
                          select new ElementJsonKey
                          {
                              Value = d.idEstado,
                              Text = d.Estado
                          }).ToList();
            return Json(lstestados, JsonRequestBehavior.AllowGet);
            // ViewBag.Estados = lstestados;
        }


        public class ElementJsonKey
        {
            public int Value { get; set; }
            public string Text { get; set; }
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
