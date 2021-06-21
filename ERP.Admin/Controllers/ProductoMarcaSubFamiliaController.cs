using Cromoplastic.Admin.App_Start;
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
    [SessionAuthorize]
    public class ProductoMarcaSubFamiliaController : Controller
    {
        // GET: ProductoMarcaSubFamilia
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Registro(string id_save)
        {
            List<ProductoMarcaSubFamilia> _lentidad = new List<Models.ProductoMarcaSubFamilia>();
            List<ProductoSubFamilia> _lsubentidad = new List<Models.ProductoSubFamilia>();
            List<ProductoMarca> _lsubentidad2 = new List<Models.ProductoMarca>();
            List<ProductoFamilia> _lsubentidad3 = new List<Models.ProductoFamilia>();
            _lsubentidad = await GetProductoSubFamiliaAsync();
            _lsubentidad2 = await GetProductoMarcaAsync();
            _lsubentidad3 = await GetProductoFamiliaAsync();
            ViewBag.ProductoSubFamilia = _lsubentidad;
            ViewBag.ProductoMarca = _lsubentidad2;
            ViewBag.ProductoFamilia = _lsubentidad3;
            if (id_save != "0")
            {
                int id_savei = 0;
                try
                {
                    id_savei = Convert.ToInt32(id_save);
                    _lentidad = await GetProductoMarcaSubFamiliaIdAsync(id_savei);
                    if (_lentidad != null)
                    {
                        ViewBag.ProductoMarcaSubFamilia = _lentidad[0];
                    }
                    else
                    {
                        ViewBag.ProductoMarcaSubFamilia = null;
                    }
                }
                catch (Exception ex)
                {
                    id_savei = 0;
                    ViewBag.ProductoMarcaSubFamilia = null;
                }

            }
            else
            {
                ViewBag.ProductoMarcaSubFamilia = null;
            }
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> Filter_ProductoSubFamiliaAsync(string id)
        {
            List<ProductoSubFamilia> lproductomarcasf = null;
            int idx = Convert.ToInt32(id);
            ProductoSubFamilia id_ = new ProductoSubFamilia { fk_producto_familia = idx };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("ProductoSubFamilia/buscarfamilia", id_);

            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductomarcasf = model.Result.ToList();
            }
            return Json(lproductomarcasf, JsonRequestBehavior.AllowGet);
        }
        private async Task<List<ProductoMarcaSubFamilia>> GetProductoMarcaSubFamiliaIdAsync(int id_save)
        {
            List<ProductoMarcaSubFamilia> lentidad = null;
            ProductoMarcaSubFamilia _entidad = new ProductoMarcaSubFamilia { id_producto_marca_subfamilia = id_save };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoMarcaSubFamilia/buscar", _entidad);
            var model = response.Content.ReadAsAsync<List<ProductoMarcaSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        private async Task<List<ProductoMarca>> GetProductoMarcaAsync()
        {
            List<ProductoMarca> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoMarca/all");
            var model = response.Content.ReadAsAsync<List<ProductoMarca>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Models.ProductoMarca>();
            }

            return lentidad;
        }
        private async Task<List<ProductoFamilia>> GetProductoFamiliaAsync()
        {
            List<ProductoFamilia> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Models.ProductoFamilia>();
            }

            return lentidad;
        }
        private async Task<List<ProductoSubFamilia>> GetProductoSubFamiliaAsync()
        {
            List<ProductoSubFamilia> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoSubFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Models.ProductoSubFamilia>();
            }

            return lentidad;
        }

        [HttpPost]
        public async Task<JsonResult> Elimina(string id_save)
        {
            List<ProductoMarcaSubFamilia> _entidad = new List<ProductoMarcaSubFamilia>();
            _entidad = await GetIdAsync(Convert.ToInt32(id_save));
            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    ProductoMarcaSubFamilia id = new ProductoMarcaSubFamilia { id_producto_marca_subfamilia = _entidad[0].id_producto_marca_subfamilia };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "ProductoMarcaSubFamilia/eliminar";
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

        private async Task<List<ProductoMarcaSubFamilia>> GetIdAsync(int id_save)
        {
            List<ProductoMarcaSubFamilia> lentidad = null;
            ProductoMarcaSubFamilia id_ = new ProductoMarcaSubFamilia { id_producto_marca_subfamilia = id_save };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoMarcaSubFamilia/buscar", id_);
            var model = response.Content.ReadAsAsync<List<ProductoMarcaSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Guarda(string id_producto_marca_subfamilia, string fk_producto_marca, string fk_producto_subfamilia)
        {
            ProductoMarcaSubFamilia _entidad = new ProductoMarcaSubFamilia();
            if (id_producto_marca_subfamilia == "0")
            {
                _entidad.id_producto_marca_subfamilia = 0;
            }
            else
            {
                _entidad.id_producto_marca_subfamilia = Convert.ToInt32(id_producto_marca_subfamilia);
            }
            _entidad.fk_producto_marca = Convert.ToInt32(fk_producto_marca);
            _entidad.fk_producto_subfamilia = Convert.ToInt32(fk_producto_subfamilia);
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
                if (_entidad.id_producto_marca_subfamilia == 0)
                {
                    metodo = "ProductoMarcaSubFamilia/agregar";
                }
                else
                {
                    metodo = "ProductoMarcaSubFamilia/modificar";
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