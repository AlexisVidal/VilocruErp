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
    public class ProductoFamiliaController : Controller
    {
        // GET: ProductoFanilia
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> EliminaProductoFamilia(string id_producto_familia)
        {
            List<ProductoFamilia> _producto_familia = new List<Models.ProductoFamilia>();
            _producto_familia = await GetProductoFamiliaIdAsync(Convert.ToInt32(id_producto_familia));
            string idinserted = "0";
            if (_producto_familia != null)
            {
                try
                {
                    ProductoFamilia idproductfamils = new ProductoFamilia { id_producto_familia = _producto_familia[0].id_producto_familia };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "ProductoFamilia/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idproductfamils);
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

        private async Task<List<ProductoFamilia>> GetProductoFamiliaIdAsync(int id_producto_familia)
        {
            List<ProductoFamilia> lproductofamilia = null;
            ProductoFamilia idproductfamils = new ProductoFamilia { id_producto_familia = id_producto_familia };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoFamilia/buscar", idproductfamils);
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductofamilia = model.Result.ToList();
            }
            return lproductofamilia;
        }
        private async Task<int> TraeFamiliaNombreAsync(dynamic text)
        {
            try
            {
                List<ProductoFamilia> lproductofamilia = null;
                ProductoFamilia idproductfamils = new ProductoFamilia { descripcion = text };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ProductoFamilia/familia", idproductfamils);
                var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
                if (model.Result.Count > 0)
                {
                    lproductofamilia = model.Result.ToList();
                    return lproductofamilia.FirstOrDefault().id_producto_familia;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        [HttpPost]
        public async Task<JsonResult> GuardaProductoFamilia(string id_producto_familia, string codigo, string descripcion, string codigo_sunat)
        {
            int idexistente = 0;
            ProductoFamilia _producto_familia = new ProductoFamilia();
            if (id_producto_familia == "0")
            {
                _producto_familia.id_producto_familia = 0;
                idexistente = await TraeFamiliaNombreAsync(descripcion);
            }
            else
            {
                _producto_familia.id_producto_familia = Convert.ToInt32(id_producto_familia);
            }
            if (idexistente > 0)
            {
                return Json("99999", JsonRequestBehavior.AllowGet);
            }
            _producto_familia.codigo = codigo;
            _producto_familia.descripcion = descripcion;
            _producto_familia.estado = "1";
            _producto_familia.codigo_sunat = codigo_sunat;

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_producto_familia.id_producto_familia == 0)
                {
                    metodo = "ProductoFamilia/agregar";
                }
                else
                {
                    metodo = "ProductoFamilia/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _producto_familia);
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
        public async Task<JsonResult> GetProductoFamiliaCodigoAsync(string codigoc)
        {
            List<ProductoFamilia> _producto_familia = new List<Models.ProductoFamilia>();
            _producto_familia = await GetProductoFamiliaCodigo(codigoc);
            string finded = "0";
            if (_producto_familia != null)
            {
                try
                {
                    if (_producto_familia.Count > 0)
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
        private async Task<List<ProductoFamilia>> GetProductoFamiliaCodigo(string codigoc)
        {
            List<ProductoFamilia> lproductofamilia = null;
            ProductoFamilia _productfamils = new ProductoFamilia { codigo = codigoc };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoFamilia/buscarcodigo", _productfamils);
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductofamilia = model.Result.ToList();
            }
            return lproductofamilia;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaProductoLinea(string id_producto_linea, string codigo, string descripcion)
        {
            int idexistente = 0;
            ProductoLineaErp _producto_familia = new ProductoLineaErp();
            if (id_producto_linea == "0")
            {
                _producto_familia.id_producto_linea = 0;
                //idexistente = await TraeFamiliaNombreAsync(descripcion);
            }
            else
            {
                _producto_familia.id_producto_linea = Convert.ToInt32(id_producto_linea);
            }
            if (idexistente > 0)
            {
                return Json("99999", JsonRequestBehavior.AllowGet);
            }
            _producto_familia.codigo = codigo;
            _producto_familia.descripcion = descripcion;
            _producto_familia.estado = "1";

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_producto_familia.id_producto_linea == 0)
                {
                    metodo = "ProductoErp/t_producto_lineaInsert";
                }
                else
                {
                    metodo = "ProductoErp/t_producto_lineaModificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _producto_familia);
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