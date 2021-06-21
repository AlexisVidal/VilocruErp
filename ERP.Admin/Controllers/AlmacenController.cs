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
    public class AlmacenController : Controller
    {
        public ActionResult Inicio()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        // GET: Almacen
        public ActionResult Index()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> EliminaAlmacen(string id_almacen)
        {
            List<Almacen> _almacen = new List<Models.Almacen>();
            _almacen = await GetAlmacenIdAsync(Convert.ToInt32(id_almacen));
            string idinserted = "0";
            if (_almacen != null)
            {
                try
                {
                    Almacen idalmac = new Almacen { id_almacen = _almacen[0].id_almacen };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Almacen/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idalmac);
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

        public async Task<List<Almacen>> GetAlmacenIdAsync(int id_almacen)
        {
            List<Almacen> lempresa = null;
            Almacen idalmac = new Almacen { id_almacen = id_almacen };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Almacen/buscar", idalmac);
            var model = response.Content.ReadAsAsync<List<Almacen>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }
        [HttpPost]
        public async Task<JsonResult> GuardaAlmacen(string id_almacen, string ubicacion, string cod_almacen, string nombre)
        {
            Almacen _almacen = new Almacen();
            int maxid = await GetMaxIdAlmacenAsync();
            if (id_almacen == "0")
            {
                _almacen.id_almacen = 0;
            }
            else
            {
                _almacen.id_almacen = Convert.ToInt32(id_almacen);
            }
            _almacen.ubicacion = ubicacion;
            _almacen.nombre = nombre;
            _almacen.cod_almacen = cod_almacen;
            _almacen.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_almacen.id_almacen == 0)
                {
                    metodo = "Almacen/agregar";
                }
                else
                {
                    metodo = "Almacen/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _almacen);
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

        private async Task<int> GetMaxIdAlmacenAsync()
        {
            int maxidalmacen = 0;
            List<Almacen> lalmacen = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Almacen/all");
            var model = response.Content.ReadAsAsync<List<Almacen>>();
            if (model.Result.Count > 0)
            {
                lalmacen = model.Result.ToList();
            }
            if (lalmacen != null)
            {
                var itemmax = lalmacen.Max(i => i.id_almacen);
                try
                {
                    maxidalmacen = Convert.ToInt32(itemmax);
                }
                catch (Exception ex)
                {
                    return maxidalmacen;
                }
            }
            else
            {
                return maxidalmacen;
            }
            return maxidalmacen;

        }

        //Luis
        public async Task<List<Almacen>> GetAlmacenAll()
        {
            List<Almacen> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Almacen/all");
                var model = response.Content.ReadAsAsync<List<Almacen>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Almacen>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> ViewCmbAlmacenActivos(int IdEntidad, string CallBy)
        {
            List<Almacen> lstEntidad = null;
            lstEntidad = await GetAlmacenAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => !i.estado.Equals("0")).ToList();
            }
            ViewBag.IdEntidad = IdEntidad;
            ViewBag.CallBy = CallBy;
            ViewBag.Almacen = lstEntidad;
            return PartialView();
        }
        //Fin Luis




        public async Task<ActionResult> Servicio()
        {
            List<ServicioOtrosErp> lservicios = null;
            try
            {
                lservicios = await GetServicios();
            }
            catch (Exception)
            {
                lservicios = new List<ServicioOtrosErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lservicios);
        }

        private async Task<List<ServicioOtrosErp>> GetServicios()
        {
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Servicioerp/t_servicio_otrosSelectAll");
                var model = response.Content.ReadAsAsync<List<ServicioOtrosErp>>();
                if (model.Result.Count > 0)
                {
                    return model.Result.ToList();
                }
                else
                {
                    return new List<ServicioOtrosErp>();
                }
            }
            catch (Exception ex)
            {
                return new List<ServicioOtrosErp>();
            }
        }

        public async Task<ActionResult> RegistroServicio(string id_servicio)
        {
            List<ServicioOtrosErp> _empresa = new List<ServicioOtrosErp>();

            if (id_servicio != "0")
            {
                int idservicio = 0;
                try
                {
                    idservicio = Convert.ToInt32(id_servicio);
                    _empresa = await GetServicioIdAsync(idservicio);
                    if (_empresa != null)
                    {
                        ViewBag.Servicios = _empresa[0];
                    }
                }
                catch (Exception ex)
                {
                    idservicio = 0;
                    ViewBag.Servicios = null;
                }

            }
            else
            {
                ViewBag.Servicios = null;
            }
            return PartialView();
        }
        private async Task<List<ServicioOtrosErp>> GetServicioIdAsync(int id_servicio_otros)
        {
            List<ServicioOtrosErp> lempresa = null;
            ServicioOtrosErp idempres = new ServicioOtrosErp { id_servicio_otros = id_servicio_otros };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ServicioErp/t_servicio_otrosSelect", idempres);
            var model = response.Content.ReadAsAsync<List<ServicioOtrosErp>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaServicio(string id_servicio_otros_save, string codigo_sunat, string descripcion, string observacion)
        {
            ServicioOtrosErp _entidad = new ServicioOtrosErp();
            if (id_servicio_otros_save == "0")
            {
                _entidad.id_servicio_otros = 0;
            }
            else
            {


                _entidad.id_servicio_otros = Convert.ToInt32(id_servicio_otros_save);
            }
            _entidad.codigo_sunat = codigo_sunat;
            _entidad.descripcion = descripcion;
            _entidad.observacion = observacion;
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
                if (_entidad.id_servicio_otros == 0)
                {
                    metodo = "Servicioerp/t_servicio_otrosInsert";
                }
                else
                {
                    metodo = "Servicioerp/t_servicio_otrosUpdate";
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