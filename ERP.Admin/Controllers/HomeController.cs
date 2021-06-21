using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        //private proancodbEntities db = new proancodbEntities();
        //public ActionResult RegistroTraslado()
        //{
        //    ViewBag.Lugares = db.tbl_lugar_embarque.OrderBy(x => x.lugar).ToList();
        //    ViewBag.Zonas = db.tbl_zona_embarque.OrderBy(x => x.zona_embarque).ToList();
        //    ViewBag.LugaresD = db.tbl_lugar_embarque.OrderByDescending(x => x.lugar).ToList();
        //    return View();
        //}
        //public async Task<ActionResult> ListarPaletLoad()
        //{
        //    ViewBag.Anios = (from x in db.t_qr_pallet
        //                     select new AnioViewModel
        //                     {
        //                         Anio = x.fecha_generacion.Year
        //                     }).ToList().Distinct();
        //    return View();
        //}
        //public async Task<ActionResult> ListarPalet()
        //{
        //    using (proancodbEntities dbc = new proancodbEntities())
        //    {
        //        var t_qr_pallet = (from a in db.t_qr_pallet
        //                           join e in db.tbl_trabajador on a.fk_usuario equals e.id
        //                           join b in db.t_detalle_qr_pallet on a.id_qr_pallet equals b.fk_qr_pallet_id_qr_pallet
        //                           join c in db.tbl_codigo_qr on b.fk_codigo_qr_id equals c.id
        //                           join d in db.tbl_productoProduccion on c.producto equals d.id
        //                           join f in db.tbl_camaraFrio on a.fk_camara equals f.id
        //                           join g in db.tbl_muestreoFormato on c.formato equals g.id
        //                           join h in db.tbl_muestreoEmpaque on c.empaque equals h.id
        //                           //where d.productName.Contains(searchString)
        //                           group a by new
        //                           {
        //                               a.id_qr_pallet,
        //                               a.fk_usuario,
        //                               e.nombres,
        //                               e.apellidos,
        //                               c.producto,
        //                               d.productName,
        //                               g.formato,
        //                               h.empaque,
        //                               a.cantidad,
        //                               a.fecha_generacion,
        //                               a.tipo_proceso,
        //                               a.fk_camara,
        //                               f.camara,
        //                               a.peso_neto,
        //                               a.estado,
        //                               a.fk_productoFinal,
        //                               a.encriptado
        //                           } into agrupado
        //                           select new PalletsGrupales
        //                           {
        //                               Id = agrupado.Key.id_qr_pallet,
        //                               Fk_Usuario = agrupado.Key.fk_usuario,
        //                               Nombre = agrupado.Key.nombres,
        //                               Apellido = agrupado.Key.apellidos,
        //                               IdProducto = agrupado.Key.producto,
        //                               Producto = agrupado.Key.productName,
        //                               Formato = agrupado.Key.formato,
        //                               Empaque = agrupado.Key.empaque,
        //                               Bultos = agrupado.Key.cantidad,
        //                               FechaG = agrupado.Key.fecha_generacion,
        //                               TipoProceso = agrupado.Key.tipo_proceso,
        //                               Fk_Camara = agrupado.Key.fk_camara,
        //                               Camara = agrupado.Key.camara,
        //                               Peso = agrupado.Key.peso_neto,
        //                               Statu = agrupado.Key.estado,
        //                               FkProductoFinal = agrupado.Key.fk_productoFinal,
        //                               Encriptado = agrupado.Key.encriptado
        //                           }).ToList();

        //        return View(t_qr_pallet.Take(100).OrderByDescending(x => x.FechaG));
        //    }
        //}

        public ActionResult Blank()
        {
            return View();
        }
        public ActionResult Elements()
        {
            return View();
        }
        public ActionResult Tabs()
        {
            return View();
        }
        public ActionResult Modals()
        {
            return View();
        }
        public ActionResult Buttons()
        {
            return View();
        }
        public ActionResult FormLayouts()
        {
            return View();
        }
        public ActionResult FormInputs()
        {
            return View();
        }
        public ActionResult Widgets()
        {
            return View();
        }
        public ActionResult Databoxes()
        {
            return View();
        }
        public ActionResult Alerts()
        {
            return View();
        }
        public async Task<ActionResult> Index()
        {
            //return View();
            CuentasPorPagarController CtrlCtasPorPagar = new CuentasPorPagarController();
            List<CuentasPorPagar> lstCuentasPorPagar = null;
            lstCuentasPorPagar = await CtrlCtasPorPagar.GetCuentasPorPagarPendientes();
            ViewBag.CuentasPorPagarPendientes = lstCuentasPorPagar;
            return View();
        }
        public ActionResult FontAwesome()
        {
            return View();
        }
        public ActionResult GlyphIcons()
        {
            return View();
        }
        public ActionResult Typicons()
        {
            return View();
        }
        public ActionResult WeatherIcons()
        {
            return View();
        }
        public ActionResult NestableList()
        {
            return View();
        }
        public ActionResult TreeView()
        {
            return View();
        }
        public ActionResult SimpleTables()
        {
            return View();
        }
        public ActionResult DataTables()
        {
            return View();
        }
        public ActionResult DataPickers()
        {
            return View();
        }

        public ActionResult Wizards()
        {
            return View();
        }

        public ActionResult FormValidation()
        {
            return View();
        }

        public ActionResult FormInputMask()
        {
            return View();
        }
        public ActionResult FormEditors()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult FlotCharts()
        {
            return View();
        }
        public ActionResult MorrisCharts()
        {
            return View();
        }
        public ActionResult SparklineCharts()
        {
            return View();
        }
        public ActionResult EasyPieCharts()
        {
            return View();
        }
        public ActionResult ChartJS()
        {
            return View();
        }
        public ActionResult Inbox()
        {
            return View();
        }
        public ActionResult Compose()
        {
            return View();
        }
        public ActionResult ViewMessage()
        {
            return View();
        }
        public ActionResult Timeline()
        {
            return View();
        }
        public ActionResult PricingTables()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult Typography()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult Grid()
        {
            return View();
        }
        public ActionResult Persian()
        {
            return View();
        }
        public ActionResult Arabic()
        {
            return View();
        }
    }
}
