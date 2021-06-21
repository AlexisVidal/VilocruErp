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
    public class ProveedorController : Controller
    {

        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Elimina(string id_proveedor)
        {
            List<Proveedor> _proveedor = new List<Proveedor>();
            _proveedor = await GetIdAsync(Convert.ToInt32(id_proveedor));
            string idinserted = "0";
            if (_proveedor != null)
            {
                try
                {
                    Proveedor id = new Proveedor { id_proveedor = _proveedor[0].id_proveedor };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Proveedor/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, id);
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

        private async Task<List<Proveedor>> GetIdAsync(int id_proveedor)
        {
            List<Proveedor> lentidad = null;
            Proveedor idalmac = new Proveedor { id_proveedor = id_proveedor };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Proveedor/buscar", idalmac);
            var model = response.Content.ReadAsAsync<List<Proveedor>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        public async Task<ActionResult> Registro(string id_proveedor)
        {
            List<Proveedor> _lentidad = new List<Proveedor>();

            if (id_proveedor != "0")
            {
                int id_ = 0;
                try
                {
                    id_ = Convert.ToInt32(id_proveedor);
                    _lentidad = await GetIdAsync(id_);
                    if (_lentidad != null)
                    {
                        ViewBag.Proveedor = _lentidad[0];
                    }
                }
                catch (Exception ex)
                {
                    id_ = 0;
                    ViewBag.Proveedor = null;
                }

            }
            else
            {
                ViewBag.Proveedor = null;
            }
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> Guarda(string id_proveedor, string ruc, string razon_social, string direccion, string contacto,
                             string telefono, string nro_cuenta, string fk_empresa)
        {
            Proveedor _proveedor = new Proveedor();
            Empresa _empresa = new Empresa();
            if (id_proveedor == "0")
            {
                _proveedor.id_proveedor = 0;
            }
            else
            {
                _proveedor.id_proveedor = Convert.ToInt32(id_proveedor);
            }

            if (fk_empresa == "0")
            {
                _empresa.id_empresa = 0;
                _proveedor.fk_empresa = 0;
            }
            else
            {
                _empresa.id_empresa = Convert.ToInt32(fk_empresa);
                _proveedor.fk_empresa = Convert.ToInt32(fk_empresa);
            }
            _empresa.ruc = ruc;
            _empresa.razon_social = razon_social;
            _empresa.direccion = direccion;
            _empresa.estado = "1";
            int maxid = await GetMaxIdAsync();
            maxid = maxid + 1;
            _proveedor.cod_proveedor = "P" + maxid.ToString().PadLeft(5, '0');
            _proveedor.contacto = contacto;
            _proveedor.telefono = telefono;
            _proveedor.nro_cuenta = nro_cuenta;
            string idempresain = "0";
            string idproveedorin = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_empresa.id_empresa == 0)
                {
                    metodo = "Empresa/agregar";
                }
                else
                {
                    metodo = "Empresa/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _empresa);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idempresain = respuesta.Result.ToString();
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

            if (Convert.ToInt32(idempresain) > 0)
            {
                _proveedor.fk_empresa = Convert.ToInt32(idempresain);
                try
                {
                    var client2 = new HttpClient();
                    string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client2.BaseAddress = new Uri(connectionInfo2);
                    client2.DefaultRequestHeaders.Accept.Clear();
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo2 = "";
                    if (_proveedor.id_proveedor == 0)
                    {
                        metodo2 = "Proveedor/agregar";
                    }
                    else
                    {
                        metodo2 = "Proveedor/modificar";
                    }
                    _proveedor.estado = "1";
                    var response2 = await client2.PostAsJsonAsync(metodo2, _proveedor);
                    var respuesta2 = response2.Content.ReadAsAsync<string>();
                    if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                    {
                        idproveedorin = respuesta2.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }


            return Json(idempresain, JsonRequestBehavior.AllowGet);
        }
        private async Task<int> GetMaxIdAsync()
        {
            int maxid = 0;
            List<Proveedor> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Proveedor/all");
            var model = response.Content.ReadAsAsync<List<Proveedor>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            if (lentidad != null)
            {
                var itemmax = lentidad.Max(i => i.id_proveedor);
                try
                {
                    maxid = Convert.ToInt32(itemmax);
                }
                catch (Exception ex)
                {
                    return maxid;
                }
            }
            else
            {
                return maxid;
            }
            return maxid;

        }
        //Luis
        public async Task<List<Proveedor>> GetProveedorAll()
        {
            List<Proveedor> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Proveedor/all");
            var model = response.Content.ReadAsAsync<List<Proveedor>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = null;
            }

            return lentidad;
        }

        public async Task<ActionResult> ListaProveedor(string CallBy)
        {
            List<Proveedor> lstProveedor = null;
            try
            {
                lstProveedor = await GetProveedorAll();
                lstProveedor = lstProveedor.Where(i => i.estado.Equals("1")).ToList();
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                return null;
            }
            return PartialView(lstProveedor);
        }
        //Fin Luis
    }
}