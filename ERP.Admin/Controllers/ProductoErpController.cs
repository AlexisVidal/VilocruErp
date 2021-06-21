using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ERP.Admin.App_Start;
using ERP.Admin.Models;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class ProductoErpController : Controller
    {
        // GET: ProductoErp
        public async Task<ActionResult> Index()
        {
            ProductoTipoErp _entidad = new ProductoTipoErp()
            {

            };
            List<ProductoErp> lentidad = new List<ProductoErp>();
            lentidad = await GetProductos();
            List<ProductoTipoErp> lentidadb = new List<ProductoTipoErp>();
            lentidadb = await GetProductoTipos();
            lentidadb.Add(_entidad);
            ViewBag.ProductoMarca = lentidadb.OrderBy(z => z.producto_tipo).ToList();
            
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);

        }
        [HttpPost]
        public async Task<JsonResult> EliminaProducto(int id_producto)
        {
            List<ProductoErp> _lentidad = new List<ProductoErp>();
            ProductoErp _entidad = null;
            ProductoErp busca = new ProductoErp()
            {
                id_producto = id_producto,
                estado = "0"
            };

            try
            {
                _entidad = await GetProductoIdAsync(id_producto);
                
            }
            catch (Exception ex)
            {
                _entidad = null;
            }


            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Productoerp/t_productoUpdateEstado";
                    var response = await client.PostAsJsonAsync(metodo, busca);
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
        public async Task<ActionResult> Registro(string id_save)
        {
            List<ProductoErp> _lentidad = new List<ProductoErp>();
            ProductoErp _entidad = new ProductoErp();
            List<ProductoMarca> _lentidadmarca = new List<ProductoMarca>();
            ProductoMarca _entidadmarca = new ProductoMarca()
            {
                id_producto_marca = 0,
                descripcion = "",
                estado = "1"
            };

            _lentidadmarca = await GetProductoMarcaAsync();
            _lentidadmarca.Add(_entidadmarca);
            ViewBag.ProductoMarca = _lentidadmarca.OrderBy(z=>z.id_producto_marca).ToList();

            List<ProductoLineaErp> _lentidadlinea = new List<ProductoLineaErp>();
            ProductoLineaErp _entidadlinea = new ProductoLineaErp()
            {
                id_producto_linea = 0,
                descripcion = "",
                estado = "1"
            };

            _lentidadlinea = await ComercialController.ProductoLineasAll();
            _lentidadlinea.Add(_entidadlinea);
            ViewBag.ProductoLinea = _lentidadlinea.OrderBy(z => z.id_producto_linea).ToList();


            List<UnidadMedidaErp> _lentidadunidad = new List<UnidadMedidaErp>();
            UnidadMedidaErp _entidadunidad = new UnidadMedidaErp()
            {
                id_unidad_medida = 0,
                codigo = "",
                abreviatura = "",
                descripcion = ""
            };

            _lentidadunidad = await ComercialController.GetUnidades();
            _lentidadunidad.Add(_entidadunidad);
            ViewBag.UnidadMedida = _lentidadunidad.OrderBy(z => z.id_unidad_medida).ToList();


            List<ProductoTipoErp> _lentidadtipo = new List<ProductoTipoErp>();
            ProductoTipoErp _entidadtipo = new ProductoTipoErp()
            {
                id_producto_tipo = 0,
                producto_tipo = "",
                estado_tipo = "1"
            };

            _lentidadtipo = await ComercialController.ProductoTipoAll();
            _lentidadtipo.Add(_entidadtipo);
            ViewBag.ProductoTipo = _lentidadtipo.OrderBy(z => z.id_producto_tipo).ToList();

            if (id_save != "0")
            {
                int id_savei = 0;
                try
                {
                    id_savei = Convert.ToInt32(id_save);
                    _entidad = await GetProductoIdAsync(id_savei);
                    if (_entidad != null)
                    {
                        ViewBag.Producto = _entidad;
                    }
                    else
                    {
                        ViewBag.Producto = null;
                    }
                }
                catch (Exception ex)
                {
                    id_savei = 0;
                    ViewBag.Producto = null;
                }

            }
            else
            {
                ViewBag.Producto = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        private async Task<List<ProductoSubFamilia>> GetProductoSubFamilia()
        {
            List<ProductoSubFamilia> lproductofamil = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoSubFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductofamil = model.Result.ToList();
            }
            else
            {
                lproductofamil = new List<ProductoSubFamilia>();
            }

            return lproductofamil;
        }

        private async Task<List<ProductoFamilia>> GetProductoFamilia()
        {
            List<ProductoFamilia> lproductofamil = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductofamil = model.Result.ToList();
            }
            else
            {
                lproductofamil = new List<ProductoFamilia>();
            }

            return lproductofamil;
        }

        private async Task<ProductoErp> GetProductoIdAsync(int id_savei)
        {
            List<ProductoErp> lproducto = null;
            ProductoErp _entidad = new ProductoErp();

            ProductoErp id_ = new ProductoErp { id_producto = id_savei };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Productoerp/t_productoSelect", id_);

            var model = response.Content.ReadAsAsync<List<ProductoErp>>();
            if (model.Result.Count > 0)
            {
                lproducto = model.Result.ToList();
                _entidad = lproducto[0];
            }

            return _entidad;
        }
        private async Task<List<ProductoMarca>> GetProductoMarcaAsync()
        {
            List<ProductoMarca> lproductomarca = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoMarca/all");
            var model = response.Content.ReadAsAsync<List<ProductoMarca>>();
            if (model.Result.Count > 0)
            {
                lproductomarca = model.Result.ToList();
            }
            else
            {
                lproductomarca = new List<Models.ProductoMarca>();
            }

            return lproductomarca;
        }
        public async Task<List<ProductoErp>> GetProductos()
        {
            List<ProductoErp> lentidad = null;
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Productoerp/t_productoSelectAll");
                var model = response.Content.ReadAsAsync<List<ProductoErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<ProductoErp>();
                }
            }
            catch (Exception e)
            {
                lentidad = new List<ProductoErp>();
            }

            return lentidad;
        }

        private async Task<List<ProductoTipoErp>> GetProductoTipos()
        {
            List<ProductoTipoErp> lentidad = null;
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Productoerp/t_producto_tipoSelectAll");
                var model = response.Content.ReadAsAsync<List<ProductoTipoErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<ProductoTipoErp>();
                }
            }
            catch (Exception e)
            {
                lentidad = new List<ProductoTipoErp>();
            }

            return lentidad;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaProducto(string id_save, string fk_producto_marca, 
            string fk_producto_tipo, string fk_unidad_medida, string nom_producto, string codigo_sku)
        {
            ProductoErp _entidad = new ProductoErp();
            if (id_save == "0")
            {
                _entidad.id_producto = 0;
            }
            else
            {
                _entidad.id_producto = Convert.ToInt32(id_save);
            }

            _entidad.fk_producto_marca = Convert.ToInt32(fk_producto_marca);
            //_entidad.fk_producto_familia = Convert.ToInt32(fk_producto_tipo);
            _entidad.fk_producto_tipo = Convert.ToInt32(fk_producto_tipo);
            _entidad.fk_unidad_medida = Convert.ToInt32(fk_unidad_medida);
            _entidad.nom_producto = nom_producto;
            _entidad.codigo_sku = codigo_sku;
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
                if (_entidad.id_producto == 0)
                {
                    metodo = "Productoerp/agregar";
                }
                else
                {
                    metodo = "Productoerp/modificar";
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

        public async Task<ActionResult> ListaProductos(string CallBy, string EstaProd)
        {
            List<ProductoErp> lstProducto = null;
            try
            {
                lstProducto = await GetProductos();
                if (EstaProd != null)
                {
                    lstProducto = lstProducto.Where(i => i.estado.Equals(EstaProd)).ToList();
                }
               
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                return null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstProducto);
        }
    }
}