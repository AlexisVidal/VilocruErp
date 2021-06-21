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
    public class AlmacenStockController : Controller
    {
        public async Task<AlmacenStock> InsertAlmacenStock(AlmacenStock almacen_stock)
        {
            AlmacenStock entidad = null;
            try
            {
                List<AlmacenStock> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("AlmacenStock/agregar", almacen_stock);

                var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    entidad = lstEntidad[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return entidad;
        }

        public async Task<AlmacenStock> UpdatePtoLimiteAlmacenStock(AlmacenStock almacen_stock)
        {
            AlmacenStock entidad = null;
            try
            {
                List<AlmacenStock> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("AlmacenStock/UpdatePtoLimite", almacen_stock);

                var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    entidad = lstEntidad[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return entidad;
        }

        public async Task<List<AlmacenStock>> GetAlmacenStockAll()
        {
            List<AlmacenStock> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("AlmacenStock/all");
                var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.AlmacenStock>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<AlmacenStock>> GetAlmacenStockMinimo()
        {
            List<AlmacenStock> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("AlmacenStock/ProductosStockMinimo");
                var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.AlmacenStock>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<AlmacenStock> GetAlmacenStockId(int IdAlmaStoc)
        {
            List<AlmacenStock> lstAlmacenStock = null;
            AlmacenStock entidad = null;
            AlmacenStock parametro = new AlmacenStock { id_almacen_stock = IdAlmaStoc };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("AlmacenStock/buscar", parametro);
            var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
            if (model.Result.Count > 0)
            {
                lstAlmacenStock = model.Result.ToList();
                entidad = lstAlmacenStock[0];
            }
            return entidad;
        }

        public async Task<Almacen> GetAlmacenId(int IdAlma)
        {
            List<Almacen> lstAlmacen = null;
            Almacen almacen = null;
            Almacen idalmac = new Almacen { id_almacen = IdAlma };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Almacen/buscar", idalmac);
            var model = response.Content.ReadAsAsync<List<Almacen>>();
            if (model.Result.Count > 0)
            {
                lstAlmacen = model.Result.ToList();
                almacen = lstAlmacen[0];
            }
            return almacen;
        }

        public async Task<ProductoErp> GetProductoId(int IdProd)
        {
            List<ProductoErp> lproducto = null;
            ProductoErp entidad = null;

            ProductoErp id_ = new ProductoErp { id_producto = IdProd };
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
                entidad = lproducto[0];
            }
            else
            {
                entidad = new ProductoErp();
            }

            return entidad;
        }
        public async Task<ProductoErp> GetProductoIdErp(int IdProd)
        {
            List<ProductoErp> lproducto = null;
            ProductoErp entidad = null;

            ProductoErp id_ = new ProductoErp { id_producto = IdProd };
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
                entidad = lproducto[0];
            }
            else
            {
                entidad = new ProductoErp();
            }

            return entidad;
        }
        public async Task<JsonResult> GetJson_ProductoId(int FkProd)
        {
            ProductoErp producto = null;
            string msgReturn = "";
            try
            {
                producto = await GetProductoId(FkProd);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(producto);
        }

        public async Task<List<AlmacenStock>> GetAlmacenStockProducto(int FkProd)
        {
            List<AlmacenStock> lstAlmacenStock = null;
            AlmacenStock parametro = new AlmacenStock { fk_producto = FkProd };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("AlmacenStock/existenciaProducto", parametro);
            var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
            if (model.Result.Count > 0)
            {
                lstAlmacenStock = model.Result.ToList();
            }
            return lstAlmacenStock;
        }

        public async Task<JsonResult> GetJson_TotalExistenciaProducto(int FkProd)
        {
            List<AlmacenStock> lstAlmacenStock = null;
            string msgReturn = "";
            try
            {
                lstAlmacenStock = await GetAlmacenStockProducto(FkProd);
                if (lstAlmacenStock != null)
                {
                    var groupReturn = lstAlmacenStock.GroupBy(i => i.fk_producto).Select(i =>
                      new
                      {
                          ProdTotaExis = i.Sum(b => b.existencia)
                      }).ToList();
                    return Json(groupReturn);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<AlmacenStock> GetAlmacenStock_AlmacenProducto(int FkAlma, int FkProd)
        {
            List<AlmacenStock> lstAlmacenStock = null;
            AlmacenStock entidad = null;
            AlmacenStock parametro = new AlmacenStock { fk_almacen = FkAlma, fk_producto = FkProd };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("AlmacenStock/existencia_Almacen_producto", parametro);
            var model = response.Content.ReadAsAsync<List<AlmacenStock>>();
            if (model.Result.Count > 0)
            {
                lstAlmacenStock = model.Result.ToList();
                entidad = lstAlmacenStock[0];
            }
            return entidad;
        }

        public async Task<List<Movimiento>> GetAlmacenStock_Kardex(int FkAlma, int FkProd)
        {
            List<Movimiento> lstEntidad = null;
            try
            {
                Movimiento parametro = new Movimiento { fk_almacen = FkAlma, fk_producto = FkProd };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("AlmacenStock/kardex_Almacen_producto", parametro);
                var model = response.Content.ReadAsAsync<List<Movimiento>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Movimiento>();
                }
            }
            catch (Exception ex)
            {
                return new List<Movimiento>(); 
            }
            return lstEntidad;
        }

        public async Task<List<Movimiento>> GetAlmacenStock_KardexByProducto(int FkProd)
        {
            List<Movimiento> lstEntidad = null;
            try
            {
                Movimiento parametro = new Movimiento { fk_producto = FkProd };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("AlmacenStock/kardex_producto", parametro);
                var model = response.Content.ReadAsAsync<List<Movimiento>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Movimiento>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public async Task<ActionResult> Existencias(int FkAlma)
        {
            string msgReturn = "";
            List<AlmacenStock> lstAlmacenStock = null;
            Almacen almacen = null;
            try
            {
                almacen = await GetAlmacenId(FkAlma);
                lstAlmacenStock = await GetAlmacenStockAll();
                if (lstAlmacenStock != null)
                {
                    lstAlmacenStock = lstAlmacenStock.Where(i => i.fk_almacen.Equals(FkAlma) && !i.estado.Equals("0")).ToList();
                }
                ViewBag.Almacen = almacen;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstAlmacenStock);
        }

        // GET: AlmacenStock
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Almacen()
        {
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
            else
            {
                lalmacen = new List<Models.Almacen>();
            }
            return View(lalmacen);
        }

        public async Task<ActionResult> ListaAlmacenStockDisponibleAlmacen(int FkAlma, string NombAlma, string CallBy)
        {
            string msgReturn = "";
            List<AlmacenStock> lstAlmacenStock = null;
            Almacen almacen = null;
            try
            {
                almacen = await GetAlmacenId(FkAlma);
                lstAlmacenStock = await GetAlmacenStockAll();
                if (lstAlmacenStock != null)
                {
                    lstAlmacenStock = lstAlmacenStock.Where(i => i.fk_almacen.Equals(FkAlma) && i.existencia > 0 && !i.estado.Equals("0")).ToList();
                }
                ViewBag.Almacen = almacen;
                ViewBag.NombreAlmacen = NombAlma;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstAlmacenStock);
        }

        public async Task<ActionResult> ViewKardex(int FkAlma, int FkProd)
        {
            ViewBag.Razonsocial = System.Configuration.ConfigurationManager.AppSettings["EmpreName"];
            ViewBag.Rucempresa = System.Configuration.ConfigurationManager.AppSettings["EmpreRuc"];
            ViewBag.Direccion = System.Configuration.ConfigurationManager.AppSettings["EmpreDire"];
            string msgReturn = "";
            List<Movimiento> lstMovimiento = null;
            List<Movimiento> lstReturnMoviInventario = null;
            List<Movimiento> lstMoviInventario = null;
            Movimiento oneMovimiento = null;
            ProductoErp producto = null;
            Almacen almacen = null;

            try
            {
                almacen = await GetAlmacenId(FkAlma);
                lstMovimiento = await GetAlmacenStock_Kardex(FkAlma, FkProd);
                producto = await GetProductoId(FkProd);
                if (lstMovimiento != null)
                {
                    lstReturnMoviInventario = lstMovimiento.Where(i => !i.descripcion_movimiento_tipo.Equals("INVENTARIO")).ToList();
                    lstMoviInventario = lstMovimiento.Where(i => i.descripcion_movimiento_tipo.Equals("INVENTARIO")).ToList();
                    if (lstMoviInventario.Count > 0)
                    {
                        var groupMoviInventario = lstMoviInventario.GroupBy(i => new { i.fk_producto, i.nro_inventario }).Select(i =>
                          new
                          {
                              FechMovi = i.First().f_movimiento,
                              FechEmis = i.First().f_movimiento,
                              TipoCompIngr = "",
                              NumeCompIngr = "",
                              CantIngr = i.Sum(b => b.cantidad),
                              PrecIngr = i.First().precio_ingreso,
                              IngrSali = i.First().ingreso_salida,
                              DescMoviTipo = i.First().descripcion_movimiento_tipo
                          }).ToList();
                        foreach(var oneMoviInve in groupMoviInventario)
                        {
                            oneMovimiento = new Movimiento
                            {
                                f_movimiento = oneMoviInve.FechMovi,
                                f_emision_ingreso = Convert.ToDateTime(oneMoviInve.FechEmis),
                                tipo_comprobante_ingreso = oneMoviInve.TipoCompIngr,
                                nro_comprobante_ingreso = oneMoviInve.NumeCompIngr,
                                cantidad = oneMoviInve.CantIngr,
                                precio_ingreso = oneMoviInve.PrecIngr,
                                ingreso_salida = oneMoviInve.IngrSali,
                                descripcion_movimiento_tipo = oneMoviInve.DescMoviTipo
                            };
                            lstReturnMoviInventario.Add(oneMovimiento);
                        }
                    }
                    lstReturnMoviInventario = lstReturnMoviInventario.OrderBy(p => Convert.ToDateTime(p.f_movimiento)).ToList();
                }
                ViewBag.Almacen = almacen;
                ViewBag.Producto = producto;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                //Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return PartialView(lstReturnMoviInventario);
            return PartialView(lstReturnMoviInventario);
        }

        public async Task<ActionResult> ViewStockMinimo(int IdAlmaStoc)
        {
            string msgReturn = "";
            AlmacenStock almacen_stock = null;
            try
            {
                almacen_stock = await GetAlmacenStockId(IdAlmaStoc);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(almacen_stock);
        }

        public async Task<ActionResult> SaveStockMinimo(int IdAlmaStoc, decimal PtoLimi)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Registro se actualizó satisfactoriamente";
            try
            {
                AlmacenStock almacen_stock = null;
                AlmacenStock almacenStockReturn = null;
                almacen_stock = new AlmacenStock
                {
                    id_almacen_stock = IdAlmaStoc,
                    pto_limite = PtoLimi
                };
                almacenStockReturn = await UpdatePtoLimiteAlmacenStock(almacen_stock);
                if (almacenStockReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar Actualizar, Por favor comuniquese con el administrador de sistemas";
                    flgError = 1;
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Actualizar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewProductoStockMinimo()
        {
            string msgReturn = "";
            List<AlmacenStock> lstAlmacenStock = null;
            try
            {
                lstAlmacenStock = await GetAlmacenStockMinimo();
                if (lstAlmacenStock != null)
                {
                    lstAlmacenStock = lstAlmacenStock.Where(i => !i.estado.Equals("0")).ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstAlmacenStock);
        }

        public async Task<JsonResult> GetJson_AlmacenStockMinimo()
        {
            List<AlmacenStock> lstAlmacenStock = null;
            string msgReturn = "";
            try
            {
                lstAlmacenStock = await GetAlmacenStockMinimo();
                //lstAlmacenStock = await GetAlmacenStockMinimo();
                if (lstAlmacenStock != null)
                {
                    lstAlmacenStock = lstAlmacenStock.Where(i => !i.estado.Equals("0")).ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(lstAlmacenStock);
        }
        public async Task<ActionResult> AddExistencias(int FkAlma)
        {
            string msgReturn = "";
            List<AlmacenStock> lstAlmacenStock = null;
            Almacen almacen = null;
            try
            {
                almacen = await GetAlmacenId(FkAlma);
                
                ViewBag.Almacen = almacen;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(almacen);
        }
    }
}