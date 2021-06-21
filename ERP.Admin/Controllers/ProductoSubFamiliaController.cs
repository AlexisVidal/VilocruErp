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
    public class ProductoSubFamiliaController : Controller
    {
        // GET: ProductoSubFamilia
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> RegistroProductoSubFamilia(string id_producto_subfamilia)
        {
            List<ProductoSubFamilia> _lentidad = new List<Models.ProductoSubFamilia>();
            List<ProductoFamilia> _lsubentidad = new List<Models.ProductoFamilia>();
            _lsubentidad = await GetProductoFamiliaAsync();
            ViewBag.ProductoFamilia = _lsubentidad;

            if (id_producto_subfamilia != "0")
            {
                int idproductosubfamilia = 0;
                try
                {
                    idproductosubfamilia = Convert.ToInt32(id_producto_subfamilia);
                    _lentidad = await GetProductoSubFamiliaIdAsync(idproductosubfamilia);
                    if (_lentidad != null)
                    {
                        ViewBag.ProductoSubFamilia = _lentidad[0];
                    }
                    else
                    {
                        ViewBag.ProductoSubFamilia = null;
                    }
                }
                catch (Exception ex)
                {
                    idproductosubfamilia = 0;
                    ViewBag.ProductoSubFamilia = null;
                }

            }
            else
            {
                ViewBag.ProductoSubFamilia = null;
            }
            return PartialView();
        }
        private async Task<List<ProductoFamilia>> GetProductoFamiliaAsync()
        {
            List<ProductoFamilia> lproductofamilia = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductofamilia = model.Result.ToList();
            }
            else
            {
                lproductofamilia = new List<Models.ProductoFamilia>();
            }

            return lproductofamilia;
        }

        private async Task<List<ProductoSubFamilia>> GetProductoSubFamiliaIdAsync(int id_producto_subfamilia)
        {
            List<ProductoSubFamilia> lproductosubfamilia = null;
            ProductoSubFamilia _productosubfamilia = new ProductoSubFamilia { id_producto_subfamilia = id_producto_subfamilia };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoSubFamilia/buscar", _productosubfamilia);
            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductosubfamilia = model.Result.ToList();
            }
            return lproductosubfamilia;
        }
        [HttpPost]
        public async Task<JsonResult> GuardaProductoSubFamilia(string id_save, string fkfamilia, string codigo, string descripcion)
        {
            ProductoSubFamilia _productosubfamilia = new ProductoSubFamilia();

            if (id_save == "0")
            {
                _productosubfamilia.id_producto_subfamilia = 0;
            }
            else
            {
                _productosubfamilia.id_producto_subfamilia = Convert.ToInt32(id_save);
            }
            if (fkfamilia == "0")
            {
                _productosubfamilia.fk_producto_familia = 0;
            }
            else
            {
                _productosubfamilia.fk_producto_familia = Convert.ToInt32(fkfamilia);
            }
            if (codigo == "")
            {
                string codeone = await GetCodigoAsync(fkfamilia);
                int maxid = await GetMaxIdAsync(fkfamilia);
                maxid = maxid + 1;
                _productosubfamilia.codigo = codeone + maxid.ToString().PadLeft(4, '0');
            }
            else
            {
                _productosubfamilia.codigo = codigo;
            }
            _productosubfamilia.descripcion = descripcion;
            _productosubfamilia.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_productosubfamilia.id_producto_subfamilia == 0)
                {
                    metodo = "ProductoSubFamilia/agregar";
                }
                else
                {
                    metodo = "ProductoSubFamilia/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _productosubfamilia);
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

        private async Task<string> GetCodigoAsync(string fkfamilia)
        {
            int fks = Convert.ToInt32(fkfamilia);
            string codigofamilia = "0";
            List<ProductoFamilia> lproductofa = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductofa = model.Result.ToList();
            }
            if (lproductofa != null)
            {
                var itemmax = lproductofa.Where(x => x.id_producto_familia.Equals(fks)).Select(y => y.codigo).FirstOrDefault();
                try
                {
                    codigofamilia = itemmax.ToString();
                }
                catch (Exception ex)
                {
                    return codigofamilia;
                }
            }
            else
            {
                return codigofamilia;
            }
            return codigofamilia;
        }

        private async Task<int> GetMaxIdAsync(string fkfamilia)
        {
            int fks = Convert.ToInt32(fkfamilia);
            int maxidsubfamilia = 0;
            List<ProductoSubFamilia> lproductosubfa = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoSubFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductosubfa = model.Result.ToList();
            }
            if (lproductosubfa != null)
            {
                try
                {
                    List<ProductoSubFamilia> lproductosubfa2 = new List<ProductoSubFamilia>();
                    lproductosubfa2 = lproductosubfa.Where(x => x.fk_producto_familia.Equals(fks)).ToList();
                    var itemmax = lproductosubfa2.Count;
                    maxidsubfamilia = Convert.ToInt32(itemmax);
                }
                catch (Exception ex)
                {
                    return maxidsubfamilia;
                }
            }
            else
            {
                return maxidsubfamilia;
            }
            return maxidsubfamilia;
        }
        [HttpPost]
        public async Task<JsonResult> Elimina(string id_save)
        {
            List<ProductoSubFamilia> _entidad = new List<ProductoSubFamilia>();
            _entidad = await GetProductoSubFamiliaIdAsync(Convert.ToInt32(id_save));
            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    ProductoSubFamilia idu = new ProductoSubFamilia { id_producto_subfamilia = _entidad[0].id_producto_subfamilia };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "ProductoSubFamilia/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idu);
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
    }
}