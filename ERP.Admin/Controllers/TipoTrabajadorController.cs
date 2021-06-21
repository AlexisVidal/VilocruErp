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
    public class TipoTrabajadorController : Controller
    {
        // GET: TipoTrabajador
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> RegistroTipoTrabajador(string id_tipo_trabajador)
        {
            List<TipoTrabajador> _entidad = new List<Models.TipoTrabajador>();

            if (id_tipo_trabajador != "0")
            {
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_tipo_trabajador);
                    _entidad = await GetTipoTrabajadorIdAsync(identidad);
                    if (_entidad != null)
                    {
                        ViewBag.TipoTrabajador = _entidad[0];
                    }
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    ViewBag.TipoTrabajador = null;
                }

            }
            else
            {
                ViewBag.TipoTrabajador = null;
            }
            return PartialView();
        }
        private async Task<List<TipoTrabajador>> GetTipoTrabajadorIdAsync(int identidad)
        {
            List<TipoTrabajador> lentidad = null;
            TipoTrabajador identidads = new TipoTrabajador { id_tipo_trabajador = identidad };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("TipoTrabajador/buscar", identidads);
            var model = response.Content.ReadAsAsync<List<TipoTrabajador>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Elimina(string id_tipo_trabajador)
        {
            List<TipoTrabajador> _entidad = new List<Models.TipoTrabajador>();
            _entidad = await GetEntidadIdAsync(Convert.ToInt32(id_tipo_trabajador));
            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    TipoTrabajador identidadc = new TipoTrabajador { id_tipo_trabajador = _entidad[0].id_tipo_trabajador };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "TipoTrabajador/eliminar";
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

        private async Task<List<TipoTrabajador>> GetEntidadIdAsync(int id_tipo_trabajador)
        {
            List<TipoTrabajador> lentidad = null;
            TipoTrabajador identidadc = new TipoTrabajador { id_tipo_trabajador = id_tipo_trabajador };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("TipoTrabajador/buscar", identidadc);
            var model = response.Content.ReadAsAsync<List<TipoTrabajador>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Guardar(string id_tipo_trabajador, string descripcion)
        {
            TipoTrabajador _entidad = new TipoTrabajador();
            if (id_tipo_trabajador == "0")
            {
                _entidad.id_tipo_trabajador = 0;
            }
            else
            {
                _entidad.id_tipo_trabajador = Convert.ToInt32(id_tipo_trabajador);
            }
            _entidad.descripcion = descripcion;
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
                if (_entidad.id_tipo_trabajador == 0)
                {
                    metodo = "TipoTrabajador/agregar";
                }
                else
                {
                    metodo = "TipoTrabajador/modificar";
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