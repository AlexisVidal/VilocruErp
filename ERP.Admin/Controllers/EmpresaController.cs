using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class EmpresaController : Controller
    {
        public async Task<int> InsertEmpresa(Empresa empresa)
        {
            int idinserted = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Empresa/agregar", empresa);

                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return idinserted;
        }

        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> EliminaEmpresa(string id_empresa)
        {
            List<Empresa> _empresa = new List<Models.Empresa>();
            _empresa = await GetEmpresaIdAsync(Convert.ToInt32(id_empresa));
            string idinserted = "0";
            if (_empresa != null)
            {
                try
                {
                    Empresa idempres = new Empresa { id_empresa = _empresa[0].id_empresa };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Empresa/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idempres);
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

        private async Task<List<Empresa>> GetEmpresaIdAsync(int id_empresa)
        {
            List<Empresa> lempresa = null;
            Empresa idempres = new Empresa { id_empresa = id_empresa };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Empresa/buscar", idempres);
            var model = response.Content.ReadAsAsync<List<Empresa>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }
        [HttpPost]
        public async Task<JsonResult> GuardaEmpresa(string id_empresa, string ruc, string razon_social, string direccion, string email, string contacto, string telefono, string tipo, string tipocliente)
        {
            Empresa _empresa = new Empresa();
            if (id_empresa == "0")
            {
                _empresa.id_empresa = 0;
            }
            else
            {
                _empresa.id_empresa = Convert.ToInt32(id_empresa);
            }
            _empresa.ruc = ruc;
            _empresa.razon_social = razon_social;
            _empresa.direccion = direccion;
            _empresa.email = email;
            _empresa.contacto = contacto;
            _empresa.telefono = telefono;
            _empresa.tipo = tipo;
            _empresa.estado = "1";
            _empresa.fk_cliente_tipo = Convert.ToInt32(tipocliente);
            string idinserted = "0";

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
                    metodo = "Empresa/agregar2";
                }
                else
                {
                    metodo = "Empresa/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _empresa);
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
        [HttpPost]
        public async Task<JsonResult> GetEmpresaRucAsync(string rucc)
        {
            List<Empresa> _empresa = new List<Models.Empresa>();
            _empresa = await GetEmpresaRuc(rucc);
            string finded = "0";
            if (_empresa != null)
            {
                try
                {
                    if (_empresa.Count > 0)
                    {
                        finded = "1";
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
            return Json(finded, JsonRequestBehavior.AllowGet);
        }
        private async Task<List<Empresa>> GetEmpresaRuc(string rucc)
        {
            List<Empresa> lempresa = null;
            Empresa _empres = new Empresa { ruc = rucc };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Empresa/buscarruc", _empres);
            var model = response.Content.ReadAsAsync<List<Empresa>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            else
            {
                lempresa = new List<Models.Empresa>();
            }
            return lempresa;
        }

        [HttpPost]
        public async Task<JsonResult> GetEmpresaRucMigo(string rucc, string fromto)
        {
            MigoRucErp lentidad = new MigoRucErp();
            MigoRucErp enti = new MigoRucErp()
            {
                nombre_o_razon_social = "",
                direccion = ""
            };
            var registrado = await GetEmpresaRuc(rucc);
            if (registrado.Count > 0)
            {
                lentidad.ruc = registrado[0].ruc;
                lentidad.nombre_o_razon_social = registrado[0].razon_social;
                lentidad.direccion = registrado[0].direccion;
                
            }
            else
            {
                lentidad = await GetEmpresaRucFromMigo(rucc);
                if (lentidad != null)
                {
                    if (fromto == "liquidacion")
                    {
                        Empresa nempresa = new Empresa()
                        {
                            ruc = lentidad.ruc,
                            razon_social = lentidad.nombre_o_razon_social,
                            direccion = lentidad.direccion,
                            tipo = "P",
                            estado= "1",
                            email = "",
                            contacto = "",
                            telefono = ""
                        };
                        int rexix = await InsertEmpresa(nempresa);
                    }
                }
            }
            if (lentidad == null)
            {
                return Json(enti, JsonRequestBehavior.AllowGet);
            }
            else
            {
                
            }
            return Json(lentidad, JsonRequestBehavior.AllowGet);
        }

        private async Task<MigoRucErp> GetEmpresaRucFromMigo(string rucc)
        {
            string metodo = System.Configuration.ConfigurationManager.AppSettings["ApiMigo"];
            string token = System.Configuration.ConfigurationManager.AppSettings["MigoToken"];
            MigoRucErp empresa = null;
            MigoRucErp _empres = new MigoRucErp { ruc = rucc, token = token };
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ApiMigo"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ruc", _empres);
                var model = response.Content.ReadAsAsync<MigoRucErp>();
                if (model.IsCompleted && model.Result.nombre_o_razon_social != null )
                {
                    var lresult = model.Result;
                    empresa = lresult;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return empresa;
        }
    }
}