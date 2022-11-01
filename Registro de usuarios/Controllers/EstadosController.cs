using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registro_de_usuarios.Controllers
{
    public class EstadosController : Controller
    {
        // GET: Estados
        public ActionResult Index()
        {

            return View();
        }

        private void LoadCatalogoPais()
        {
            var catalogoP = db.tblCatPaises
                            .Select(o => new SelectListItem
                            {
                                Value = o.idPais.ToString(),
                                Text = o.Pais
                            }).ToArray();

            ViewBag.Paises = catalogoP;

        }
        public JsonResult Pais(int IdPaisClass)
        {
            List<ElementJsonIntKey> lst = new List<ElementJsonIntKey>();
             var catalogoE = db.tblCatEstados
                .Select(tbl.idPais,Pais,Estado)from 

            {
                model.Estado 
            }
        }

        public class ElementJsonIntKey
            {
            public int  Value { get; set }
            public string Text { get; set }

            }

    }