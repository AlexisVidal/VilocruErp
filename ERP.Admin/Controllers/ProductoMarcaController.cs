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
    public class ProductoMarcaController : Controller
    {
        // GET: ProductoMarca
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> EliminaProductoMarca(string id_producto_marca)
        {
            List<ProductoMarca> _productmarc = new List<Models.ProductoMarca>();
            _productmarc = await GetProductoMarcaIdAsync(Convert.ToInt32(id_producto_marca));
            string idinserted = "0";
            if (_productmarc != null)
            {
                try
                {
                    ProductoMarca idproductmarcs = new ProductoMarca { id_producto_marca = _productmarc[0].id_producto_marca };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "ProductoMarca/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idproductmarcs);
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

        private async Task<int> TraeMarcaNombreAsync(dynamic text)
        {
            try
            {
                List<ProductoMarca> lproductomarca = null;
                ProductoMarca idproductmarcs = new ProductoMarca { descripcion = text };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ProductoMarca/marca", idproductmarcs);
                var model = response.Content.ReadAsAsync<List<ProductoMarca>>();
                if (model.Result.Count > 0)
                {
                    lproductomarca = model.Result.ToList();
                    return lproductomarca.FirstOrDefault().id_producto_marca;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private async Task<List<ProductoMarca>> GetProductoMarcaIdAsync(int id_producto_marca)
        {
            List<ProductoMarca> lproductomarca = null;
            ProductoMarca idproductmarcs = new ProductoMarca { id_producto_marca = id_producto_marca };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoMarca/buscar", idproductmarcs);
            var model = response.Content.ReadAsAsync<List<ProductoMarca>>();
            if (model.Result.Count > 0)
            {
                lproductomarca = model.Result.ToList();
            }
            return lproductomarca;
        }
        [HttpPost]
        public async Task<JsonResult> GuardaProductoMarca(string id_producto_marca, string descripcion)
        {
            int idexistente = 0;
            ProductoMarca _productmarc = new ProductoMarca();
            if (id_producto_marca == "0")
            {
                _productmarc.id_producto_marca = 0;
                idexistente = await TraeMarcaNombreAsync(descripcion);
            }
            else
            {
                _productmarc.id_producto_marca = Convert.ToInt32(id_producto_marca);
            }
            if (idexistente > 0)
            {
                return Json("99999", JsonRequestBehavior.AllowGet);
            }
            _productmarc.descripcion = descripcion;
            _productmarc.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_productmarc.id_producto_marca == 0)
                {
                    metodo = "ProductoMarca/agregar";
                }
                else
                {
                    metodo = "ProductoMarca/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _productmarc);
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