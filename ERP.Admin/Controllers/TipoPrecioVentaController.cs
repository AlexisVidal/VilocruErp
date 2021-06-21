using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class TipoPrecioVentaController : Controller
    {
        // GET: TipoPrecioVenta
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Registro(string id_tipo_precio_venta)
        {
            List<TipoPrecioVenta> _entidad = new List<TipoPrecioVenta>();

            if (id_tipo_precio_venta != "0")
            {
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_tipo_precio_venta);
                    _entidad = await GetEntidadIdAsync(identidad);
                    if (_entidad != null)
                    {
                        ViewBag.TipoPrecioVenta = _entidad[0];
                    }
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    ViewBag.TipoPrecioVenta = null;
                }

            }
            else
            {
                ViewBag.TipoPrecioVenta = null;
            }
            return PartialView();
        }

        private async Task<List<TipoPrecioVenta>> GetEntidadIdAsync(int id_tipo_precio_venta)
        {
            List<TipoPrecioVenta> lentidad = null;
            TipoPrecioVenta identidadc = new TipoPrecioVenta { id_tipo_precio_venta = id_tipo_precio_venta };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("TipoPrecioVenta/buscar", identidadc);
            var model = response.Content.ReadAsAsync<List<TipoPrecioVenta>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Elimina(string id_tipo_precio_venta)
        {
            List<TipoPrecioVenta> _entidad = new List<Models.TipoPrecioVenta>();
            _entidad = await GetEntidadIdAsync(Convert.ToInt32(id_tipo_precio_venta));
            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    TipoPrecioVenta identidadc = new TipoPrecioVenta { id_tipo_precio_venta = _entidad[0].id_tipo_precio_venta };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "TipoPrecioVenta/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, identidadc);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = respuesta.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public async Task<JsonResult> Guardar(string id_tipo_precio_venta, string nombre, string porcentaje)
        {
            TipoPrecioVenta _entidad = new TipoPrecioVenta();
            if (id_tipo_precio_venta == "0")
            {
                _entidad.id_tipo_precio_venta = 0;
            }
            else
            {
                _entidad.id_tipo_precio_venta = Convert.ToInt32(id_tipo_precio_venta);
            }
            _entidad.nombre = nombre;
            try
            {
                _entidad.porcentaje = Convert.ToDecimal(porcentaje);
            }
            catch (Exception ex)
            {
                _entidad.porcentaje = 0;
            }
            _entidad.nombre = nombre;

            _entidad.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.id_tipo_precio_venta == 0)
                {
                    metodo = "TipoPrecioVenta/agregar";
                }
                else
                {
                    metodo = "TipoPrecioVenta/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }
    }
}