using Cromoplastic.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cromoplastic.Admin.Controllers
{
    public class ProductoProveedorController : Controller
    {
        // GET: ProductoProveedor
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Registro()
        {
            List<ProductoProveedor> _entidad = new List<ProductoProveedor>();
            List<ProductoSinProveedor> _producto = new List<ProductoSinProveedor>();
            List<Proveedor> _proveedor = new List<Proveedor>();
            _proveedor = await GetProveedorAllAsync();
            ViewBag.Proveedor = _proveedor;
            _producto = await GetProductoSinProveedorAsync();
            ViewBag.Producto = _producto;
            return PartialView();
        }

        private async Task<List<ProductoSinProveedor>> GetProductoSinProveedorAsync()
        {
            List<ProductoSinProveedor> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/sinproveedor");
            var model = response.Content.ReadAsAsync<List<ProductoSinProveedor>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<ProductoSinProveedor>();
            }
            return lentidad;
        }

        private async Task<List<Proveedor>> GetProveedorAllAsync()
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
                lentidad = new List<Proveedor>();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Guardar(string fkproveedor, List<string> afkproductos)
        {
            int contador = 0;
            foreach (var item in afkproductos)
            {
                ProductoProveedor _entidad = new ProductoProveedor();
                _entidad.fk_proveedor = Convert.ToInt32(fkproveedor);
                _entidad.fk_producto = Convert.ToInt32(item);
                _entidad.estado = "1";
                int idinserted = 0;
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "ProductoProveedor/agregar";
                    var response = await client.PostAsJsonAsync(metodo, _entidad);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = Convert.ToInt32(respuesta.Result.ToString());
                        if (idinserted >0)
                        {
                            contador++;
                        }
                        else
                        {
                            contador = 0;
                            return Json("0", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(contador, JsonRequestBehavior.AllowGet);
        }
    }
}