using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Admin.App_Start;
using ERP.Admin.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using X.PagedList;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class ComercialController : Controller
    {
        // GET: Comercial
        string logoreport = System.Configuration.ConfigurationManager.AppSettings["logoreport"];
        string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        #region Empresa
        public async Task<ActionResult> Empresa()
        {
            List<Empresa> lempresa = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Empresa/all");
            var model = response.Content.ReadAsAsync<List<Empresa>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.Where(x => x.tipo.Equals("P")).ToList();
            }
            else
            {
                lempresa = new List<Empresa>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lempresa);
        }

        public async Task<ActionResult> Cliente()
        {
            List<Empresa> lempresa = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Empresa/all");
            var model = response.Content.ReadAsAsync<List<Empresa>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.Where(x => x.tipo.Equals("C")).ToList();
            }
            else
            {
                lempresa = new List<Empresa>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lempresa);
        }

        public async Task<List<ClienteTipoErp>> ClienteTipo()
        {
            List<ClienteTipoErp> lclientestipo = null;
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ClienteTipo/all");
                var model = response.Content.ReadAsAsync<List<ClienteTipoErp>>();
                if (model.Result.Count > 0)
                {
                    lclientestipo = model.Result.ToList();
                }
                else
                {
                    lclientestipo = new List<ClienteTipoErp>();
                }
            }
            catch (Exception ex)
            {
                lclientestipo = null;
            }
            return lclientestipo;
        }
        public async Task<ActionResult> RegistroEmpresa(string id_empresa, string tipo)
        {
            List<ClienteTipoErp> ltipos = null;
            ltipos = await ClienteTipo();
            ltipos.Add(new ClienteTipoErp { id_cliente_tipo = 0, descripcion = "", estado = "" });

            ViewBag.Tipos = ltipos.OrderBy(i => i.id_cliente_tipo).ToList();

            List<Empresa> _empresa = new List<Empresa>();

            if (id_empresa != "0")
            {
                int idempre = 0;
                try
                {
                    idempre = Convert.ToInt32(id_empresa);
                    _empresa = await GetEmpresaIdAsync(idempre);
                    if (_empresa != null)
                    {
                        ViewBag.Empresas = _empresa[0];
                    }
                }
                catch (Exception ex)
                {
                    idempre = 0;
                    ViewBag.Empresas = null;
                }

            }
            else
            {
                ViewBag.Empresas = null;
            }

            ViewBag.Tipo = tipo;
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        private async Task<List<Empresa>> GetEmpresaIdAsync(int idempre)
        {
            List<Empresa> lempresa = null;
            Empresa idempres = new Empresa { id_empresa = idempre };
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
        #endregion
        #region Almacen
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
                lalmacen = new List<Almacen>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lalmacen);
        }
        public async Task<ActionResult> RegistroAlmacen(string id_almacen)
        {
            List<Almacen> _almacen = new List<Almacen>();

            if (id_almacen != "0")
            {
                int idalmac = 0;
                try
                {
                    idalmac = Convert.ToInt32(id_almacen);
                    _almacen = await GetAlmacenIdAsync(idalmac);
                    if (_almacen != null)
                    {
                        ViewBag.Almacen = _almacen[0];
                    }
                }
                catch (Exception ex)
                {
                    idalmac = 0;
                    ViewBag.Almacen = null;
                }

            }
            else
            {
                ViewBag.Almacen = null;
            }
            return PartialView();
        }
        public async Task<ActionResult> RegistroMovimientoIngreso(string id_almacen_movimiento)
        {
            List<AlmacenMovimientoErp> _almacenmovimiento = new List<AlmacenMovimientoErp>();

            if (id_almacen_movimiento != "0")
            {
                int idalmacenmovimiento = 0;
                try
                {
                    idalmacenmovimiento = Convert.ToInt32(id_almacen_movimiento);
                    _almacenmovimiento = await GetAlmacenMovimientoIdAsync(idalmacenmovimiento);
                    if (_almacenmovimiento != null)
                    {
                        ViewBag.AlmacenMovimientoErp = _almacenmovimiento[0];
                    }
                }
                catch (Exception ex)
                {
                    idalmacenmovimiento = 0;
                    ViewBag.AlmacenMovimientoErp = null;
                }

            }
            else
            {
                ViewBag.AlmacenMovimientoErp = null;
            }
            return PartialView();
        }

        private async Task<List<AlmacenMovimientoErp>> GetAlmacenMovimientoIdAsync(int identidad)
        {
            List<AlmacenMovimientoErp> lentidad = null;
            AlmacenMovimientoErp idalmacs = new AlmacenMovimientoErp { id_almacen_movimiento = identidad };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Movimiento/t_almacen_movimientoSelect", idalmacs);
            var model = response.Content.ReadAsAsync<List<AlmacenMovimientoErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }

        private async Task<List<Almacen>> GetAlmacenIdAsync(int idalmac)
        {
            List<Almacen> lalmacen = null;
            Almacen idalmacs = new Almacen { id_almacen = idalmac };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Almacen/buscar", idalmacs);
            var model = response.Content.ReadAsAsync<List<Almacen>>();
            if (model.Result.Count > 0)
            {
                lalmacen = model.Result.ToList();
            }
            return lalmacen;
        }
        #endregion
        #region ProductoMarca
        public async Task<ActionResult> ProductoMarca()
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
                lproductomarca = new List<ProductoMarca>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lproductomarca);
        }
        public async Task<ActionResult> RegistroProductoMarca(string id_producto_marca)
        {
            List<ProductoMarca> _producto_marca = new List<ProductoMarca>();

            if (id_producto_marca != "0")
            {
                int idproductmarc = 0;
                try
                {
                    idproductmarc = Convert.ToInt32(id_producto_marca);
                    _producto_marca = await GetProductoMarcaIdAsync(idproductmarc);
                    if (_producto_marca != null)
                    {
                        ViewBag.ProductoMarca = _producto_marca[0];
                    }
                }
                catch (Exception ex)
                {
                    idproductmarc = 0;
                    ViewBag.ProductoMarca = null;
                }

            }
            else
            {
                ViewBag.ProductoMarca = null;
            }
            return PartialView();
        }

        private async Task<List<ProductoMarca>> GetProductoMarcaIdAsync(int idproductmarc)
        {
            List<ProductoMarca> lproductomarca = null;
            ProductoMarca idproductmarcs = new ProductoMarca { id_producto_marca = idproductmarc };
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
        #endregion
        #region ProductoFamilia
        public async Task<ActionResult> ProductoFamilia()
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
                lentidad = new List<ProductoFamilia>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        public async Task<ActionResult> RegistroProductoFamilia(string id_producto_familia)
        {
            List<ProductoFamilia> _entidad = new List<ProductoFamilia>();

            if (id_producto_familia != "0")
            {
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_producto_familia);
                    _entidad = await GetProductoFamiliaIdAsync(identidad);
                    if (_entidad != null)
                    {
                        ViewBag.ProductoFamilia = _entidad[0];
                    }
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    ViewBag.ProductoFamilia = null;
                }

            }
            else
            {
                ViewBag.ProductoFamilia = null;
            }
            return PartialView();
        }

        private async Task<List<ProductoFamilia>> GetProductoFamiliaIdAsync(int identidad)
        {
            List<ProductoFamilia> lentidad = null;
            ProductoFamilia identidads = new ProductoFamilia { id_producto_familia = identidad };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoFamilia/buscar", identidads);
            var model = response.Content.ReadAsAsync<List<ProductoFamilia>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        #endregion
        #region ProductoSubFamilia
        // GET: ProductoSubFamilia
        public async Task<ActionResult> ProductoSubFamilia()
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
                lentidad = new List<ProductoSubFamilia>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }




        #endregion ProductoSubFamilia

        #region Proveedor
        public async Task<ActionResult> Proveedor()
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
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;

            return View(lentidad);
        }
        #endregion Proveedor


        #region TipoPrecioVenta
        public async Task<ActionResult> TipoPrecioVenta()
        {
            List<TipoPrecioVenta> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("TipoPrecioVenta/all");
            var model = response.Content.ReadAsAsync<List<TipoPrecioVenta>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<TipoPrecioVenta>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        #endregion
        #region Producto
        public async Task<ActionResult> Producto()
        {
            List<Producto> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/all");
            var model = response.Content.ReadAsAsync<List<Producto>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Producto>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        #endregion

        #region PrecioVenta
        public async Task<ActionResult> PreciosVentaList(int? page, string descripcion = "")
        {
            int total = 0;
            descripcion = descripcion.Trim();
            string buscars = descripcion.Replace(" ", "| OR /");
            buscars = "/" + buscars + "|";
            var entidad = new Busqueda { descripcion = buscars };

            List<Producto> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Producto/t_productoSelectConPrecioCompraText", entidad);
            var model = response.Content.ReadAsAsync<List<Producto>>();

            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Producto>();
            }
            total = lentidad.Count();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = lentidad.ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize

            ViewBag.onePageOfProducts = onePageOfProducts;
            if (descripcion == null)
            {
                descripcion = "";
            }
            ViewBag.Descripcion = descripcion;
            ViewBag.Total = total;
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        public async Task<ActionResult> PrecioVenta()
        {
            List<Producto> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/allforventa");
            var model = response.Content.ReadAsAsync<List<Producto>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Producto>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        #endregion

        #region OrdenCompra
        public ActionResult OrdenCompra()
        {
            HttpClient client = new HttpClient();

            List<OrdenCompra> lEstados = new List<OrdenCompra>();
            lEstados.Add(new OrdenCompra { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new OrdenCompra { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new OrdenCompra { estado = "1", NEstado = "REGISTRADA" });
            lEstados.Add(new OrdenCompra { estado = "2", NEstado = "APROBADA" });
            lEstados.Add(new OrdenCompra { estado = "3", NEstado = "ENT. PARCIALMETE" });
            lEstados.Add(new OrdenCompra { estado = "4", NEstado = "ENT. TOTALMENTE" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }
        #endregion

        //Luis
        #region Compra
        public ActionResult Compra()
        {
            List<Compra> lEstados = new List<Compra>();
            lEstados.Add(new Compra { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new Compra { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new Compra { estado = "1", NEstado = "ACEPTADA" });
            lEstados.Add(new Compra { estado = "2", NEstado = "ENT. PARCIALMETE" });
            lEstados.Add(new Compra { estado = "3", NEstado = "ENT. TOTALMENTE" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }
        #endregion

        #region GuiaRemision
        public ActionResult GuiaRemision()
        {
            List<GuiaRemision> lEstados = new List<GuiaRemision>();
            lEstados.Add(new GuiaRemision { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new GuiaRemision { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new GuiaRemision { estado = "1", NEstado = "REGISTRADA" });
            lEstados.Add(new GuiaRemision { estado = "2", NEstado = "FINALIZADA" });
            lEstados.Add(new GuiaRemision { estado = "3", NEstado = "FACTURADA" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }
        #endregion

        #region ComprobanteCompra
        public ActionResult ComprobanteCompra()
        {
            List<GuiaRemision> lEstados = new List<GuiaRemision>();
            lEstados.Add(new GuiaRemision { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new GuiaRemision { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new GuiaRemision { estado = "1", NEstado = "REGISTRADA" });
            lEstados.Add(new GuiaRemision { estado = "2", NEstado = "FINALIZADA" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }
        #endregion
        //Fin Luis
        public class Busqueda
        {
            public string descripcion { get; set; }
        }
        #region Producto
        public async Task<ActionResult> ProductoList(int? page, string descripcion = "")
        {
            int total = 0;
            descripcion = descripcion.Trim();
            string buscars = descripcion.Replace(" ", "| AND /");
            buscars = "/" + buscars + "|";
            var entidad = new Busqueda { descripcion = buscars };

            List<Producto> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Producto/t_productoSelectTextAll", entidad);
            var model = response.Content.ReadAsAsync<List<Producto>>();

            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Producto>();
            }
            total = lentidad.Count();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = lentidad.ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize

            ViewBag.onePageOfProducts = onePageOfProducts;
            if (descripcion == null)
            {
                descripcion = "";
            }
            ViewBag.Descripcion = descripcion;


            ViewBag.Total = total;
            return View();
        }
        #endregion


        #region PrecioVenta
        public async Task<ActionResult> PrecioVentaList(int? page, string descripcion = "")
        {
            descripcion = descripcion.Trim();
            string buscars = descripcion.Replace(" ", "| OR /");
            buscars = "/" + buscars + "|";
            var entidad = new Busqueda { descripcion = buscars };

            List<Producto> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/allforventa");
            var model = response.Content.ReadAsAsync<List<Producto>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Producto>();
            }
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = lentidad.ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize

            ViewBag.onePageOfProducts = onePageOfProducts;
            return View(lentidad);
        }
        #endregion

        public async Task<ActionResult> Servicio()
        {
            List<ServicioErp> lservicios = null;
            try
            {
                lservicios = await GetServicios();
            }
            catch (Exception)
            {
                lservicios = new List<ServicioErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lservicios);
        }

        private async Task<List<ServicioErp>> GetServicios()
        {
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Servicioerp/t_servicioSelectAll");
                var model = response.Content.ReadAsAsync<List<ServicioErp>>();
                if (model.Result.Count > 0)
                {
                    return model.Result.ToList();
                }
                else
                {
                    return new List<ServicioErp>();
                }
            }
            catch (Exception ex)
            {
                return new List<ServicioErp>();
            }
        }

        public async Task<ActionResult> RegistroServicio(string id_servicio)
        {
            List<ServicioErp> _empresa = new List<ServicioErp>();

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
        private async Task<List<ServicioErp>> GetServicioIdAsync(int idservicio)
        {
            List<ServicioErp> lempresa = null;
            ServicioErp idempres = new ServicioErp { id_servicio = idservicio };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ServicioErp/t_servicioSelect", idempres);
            var model = response.Content.ReadAsAsync<List<ServicioErp>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaServicio(string id_servicio_save, string codigo_sunat, string descripcion, string observacion)
        {
            ServicioErp _entidad = new ServicioErp();
            if (id_servicio_save == "0")
            {
                _entidad.id_servicio = 0;
            }
            else
            {


                _entidad.id_servicio = Convert.ToInt32(id_servicio_save);
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
                if (_entidad.id_servicio == 0)
                {
                    metodo = "Servicioerp/t_servicioInsert";
                }
                else
                {
                    metodo = "Servicioerp/t_servicioUpdate";
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
        PersonalController CtrlPersonal = new PersonalController();
        public async Task<ActionResult> RegistroEmpresaCuenta(string id_empresa)
        {
            List<Empresa> _empresa = new List<Empresa>();
            List<BancoErp> _lbanco = new List<BancoErp>();
            List<MonedaErp> _lmoneda = new List<MonedaErp>();

            if (id_empresa != "0")
            {
                int idempre = 0;
                try
                {
                    idempre = Convert.ToInt32(id_empresa);
                    ViewBag.FkEmpresa = idempre;

                    BancoErp newadd = new BancoErp()
                    {
                        id_banco = 0,
                        idbanco = "",
                        descripcion = "",
                        codigo_sunat = "",
                        direccion = "",
                        estado = "1",
                        telefono = ""
                    };

                    _lbanco = await CtrlPersonal.GetBancoErp();
                    _lbanco.Add(newadd);
                    ViewBag.Bancos = _lbanco.Where(y => y.estado.Equals("1")).OrderBy(x => x.id_banco).ToList();

                    MonedaErp newaddm = new MonedaErp()
                    {
                        IDMONEDA = "",
                        NOMBRE_CORTO = "",
                        DESCRIPCION = "",
                        TIPO_MONEDA = "",
                        FECHACREACION = DateTime.Now,
                        IDREGISTRO_SUNAT = "",
                        ESTADO = "1"
                    };
                    _lmoneda = await GetMonedaErp();
                    _lmoneda.Add(newaddm);
                    ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();

                }
                catch (Exception ex)
                {
                    idempre = 0;
                    ViewBag.Bancos = _lbanco;
                    ViewBag.Monedas = _lmoneda;
                }

            }
            else
            {
                ViewBag.Bancos = _lbanco;
                ViewBag.Monedas = _lmoneda;
            }
            return PartialView();
        }

        public async Task<List<MonedaErp>> GetMonedaErp()
        {
            List<MonedaErp> xlMonedaErp = new List<MonedaErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Empresa/t_monedaSelectAll");
                var model = response.Content.ReadAsAsync<List<MonedaErp>>();
                if (model.Result.Count > 0)
                {
                    xlMonedaErp = model.Result.ToList();
                }
                else
                {
                    xlMonedaErp = new List<MonedaErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlMonedaErp;
        }
        public async Task<List<TipoCheckErp>> GetTipoCheckErp()
        {
            List<TipoCheckErp> xlTipoCheckErp = new List<TipoCheckErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Obligacioneserp/t_tipo_checkSelectAll");
                var model = response.Content.ReadAsAsync<List<TipoCheckErp>>();
                if (model.Result.Count > 0)
                {
                    xlTipoCheckErp = model.Result.ToList();
                }
                else
                {
                    xlTipoCheckErp = new List<TipoCheckErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlTipoCheckErp;
        }
        public async Task<ActionResult> GetCuentasEmpresa(string fk_empresa)
        {
            List<EmpresaCuentaErp> lstEntidad = null;
            int fkempresa = Convert.ToInt32(fk_empresa);
            lstEntidad = await GetCuentasFkAsync(fkempresa);
            if (lstEntidad != null && lstEntidad.Any())
            {
                lstEntidad = lstEntidad.ToList();
            }
            else
            {
                lstEntidad = new List<EmpresaCuentaErp>();
            }
            return PartialView(lstEntidad);
        }

        private async Task<List<EmpresaCuentaErp>> GetCuentasFkAsync(int fkempresa)
        {
            List<EmpresaCuentaErp> lempresa = null;
            EmpresaCuentaErp idempres = new EmpresaCuentaErp { fk_empresa = fkempresa };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Empresa/t_empresa_cuentaSelectFk", idempres);
            var model = response.Content.ReadAsAsync<List<EmpresaCuentaErp>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaCuentaEmpresa(int idempresacuenta, string fkempresax, string cmbfk_banco, string cmbidmoneda,
            string txtNroCuenta, string txtNroCci)
        {
            EmpresaCuentaErp _entidad = new EmpresaCuentaErp();
            if (idempresacuenta == 0)
            {
                _entidad.id_empresa_cuenta = 0;
            }
            else
            {


                _entidad.id_empresa_cuenta = Convert.ToInt32(idempresacuenta);
            }
            _entidad.fk_empresa = Convert.ToInt32(fkempresax);
            _entidad.fk_banco = Convert.ToInt32(cmbfk_banco);
            _entidad.IDMONEDA = cmbidmoneda;
            _entidad.nro_cuenta = txtNroCuenta;
            _entidad.nro_cuenta_interbancario = txtNroCci;
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
                if (_entidad.id_empresa_cuenta == 0)
                {
                    metodo = "Empresa/t_empresa_cuentaInsert";
                }
                else
                {
                    metodo = "Empresa/t_empresa_cuentaUpdate";
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

        public async Task<ActionResult> Proyecto()
        {
            List<ProyectoErp> lproyectos = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Servicioerp/t_proyectoSelectAll");
            var model = response.Content.ReadAsAsync<List<ProyectoErp>>();
            if (model.Result.Count > 0)
            {
                lproyectos = model.Result.ToList();
            }
            else
            {
                lproyectos = new List<ProyectoErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lproyectos);
        }
        public async Task<ActionResult> RegistroProyecto(int id_proyecto)
        {
            List<Empresa> _empresa = new List<Empresa>();
            List<ProyectoErp> _lproyecto = new List<ProyectoErp>();
            List<MonedaErp> _lmoneda = new List<MonedaErp>();
            List<ServicioErp> _lservicio = new List<ServicioErp>();
            ProyectoErp _proyecto = new ProyectoErp();
            if (id_proyecto != 0)
            {
                try
                {
                    ViewBag.IdProyecto = id_proyecto;
                    var xproyecto = await GetProyectoIdAsync(id_proyecto);
                    if (xproyecto != null && xproyecto.Any())
                    {
                        _proyecto = xproyecto[0];
                    }
                }
                catch (Exception ex)
                {
                    id_proyecto = 0;
                    ViewBag.IdProyecto = id_proyecto;
                }
            }
            else
            {
                id_proyecto = 0;
                ViewBag.IdProyecto = id_proyecto;
            }

            Empresa newadd = new Empresa()
            {
                id_empresa = 0,
                ruc = "",
                razon_social = "",
                contacto = "",
                email = "",
                estado = "1",
                telefono = "",
                direccion = "",
                tipo = "C"
            };

            _empresa = await GetEmpresasErp();
            _empresa.Add(newadd);
            try
            {
                ViewBag.Empresas = _empresa.Where(y => y.estado.Equals("1") && y.tipo.Equals("C")).OrderBy(x => x.id_empresa).ToList();
            }
            catch (Exception e)
            {
                ViewBag.Empresas = _empresa.OrderBy(x => x.id_empresa).ToList();
            }

            MonedaErp newaddm = new MonedaErp()
            {
                IDMONEDA = "",
                NOMBRE_CORTO = "",
                DESCRIPCION = "",
                TIPO_MONEDA = "",
                FECHACREACION = DateTime.Now,
                IDREGISTRO_SUNAT = "",
                ESTADO = "1"
            };
            _lmoneda = await GetMonedaErp();
            _lmoneda.Add(newaddm);
            ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();

            ServicioErp newservi = new ServicioErp()
            {
                id_servicio = 0,
                descripcion = "",
                codigo_sunat = "",
                cod_servicio = "",
                observacion = "",
                estado = "1"
            };
            _lservicio = await GetServicios();
            _lservicio.Add(newservi);
            ViewBag.Servicios = _lservicio.Where(y => y.estado.Equals("1")).OrderBy(x => x.id_servicio).ToList();

            ViewBag.Proyecto = _proyecto;

            List<ProyectoServicioErp> _ldetalle = new List<ProyectoServicioErp>();
            try
            {
                _ldetalle = await GetServicioProyecto(id_proyecto);
            }
            catch (Exception e)
            {
                _ldetalle = new List<ProyectoServicioErp>();
            }
            ViewBag.ServicioProyecto = _ldetalle;
            return PartialView();
        }

        private async Task<List<ProyectoServicioErp>> GetServicioProyecto(int id_proyecto)
        {
            List<ProyectoServicioErp> lproyecto = null;
            ProyectoServicioErp busca = new ProyectoServicioErp { fk_proyecto = id_proyecto };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Servicioerp/t_proyecto_servicioSelectFk", busca);
            var model = response.Content.ReadAsAsync<List<ProyectoServicioErp>>();
            if (model.Result.Count > 0)
            {
                lproyecto = model.Result.ToList();
            }
            return lproyecto;

        }

        [HttpPost]
        public async Task<JsonResult> GetServicioProyJson(int id_proyecto)
        {
            List<ProyectoServicioErp> lproyecto = null;
            ProyectoServicioErp busca = new ProyectoServicioErp { fk_proyecto = id_proyecto };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Servicioerp/t_proyecto_servicioSelectFk", busca);
            var model = response.Content.ReadAsAsync<List<ProyectoServicioErp>>();
            if (model.Result.Count > 0)
            {
                lproyecto = model.Result.ToList();
            }
            else
            {
                lproyecto = new List<ProyectoServicioErp>();
            }
            return Json(lproyecto, JsonRequestBehavior.AllowGet);
        }
        private async Task<List<ProyectoErp>> GetProyectoIdAsync(int id_proyecto)
        {
            List<ProyectoErp> lproyecto = null;
            ProyectoErp busca = new ProyectoErp { id_proyecto = id_proyecto };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Servicioerp/t_proyectoSelectId", busca);
            var model = response.Content.ReadAsAsync<List<ProyectoErp>>();
            if (model.Result.Count > 0)
            {
                lproyecto = model.Result.ToList();
            }
            return lproyecto;
        }

        public async Task<List<Empresa>> GetEmpresasErp()
        {
            List<Empresa> lempresa = new List<Empresa>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Empresa/all");
                var model = response.Content.ReadAsAsync<List<Empresa>>();
                if (model.Result.Count > 0)
                {
                    lempresa = model.Result.ToList();
                }
                else
                {
                    lempresa = new List<Empresa>();
                }
            }
            catch (Exception e)
            {
                lempresa = new List<Empresa>();
            }

            return lempresa;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaProyecto(string id_save, int fk_empresa,
            string descripcion, string idmoneda, decimal monto, string fecha_ini, string fecha_fin, string observacion,
            List<List<string>> arrServicios)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            ProyectoErp _entidad = new ProyectoErp();
            if (id_save == "0")
            {
                _entidad.id_proyecto = 0;
            }
            else
            {


                _entidad.id_proyecto = Convert.ToInt32(id_save);
            }
            _entidad.fk_empresa = Convert.ToInt32(fk_empresa);
            //_entidad.fk_servicio = Convert.ToInt32(fk_servicio);
            _entidad.descripcion = descripcion;
            _entidad.IDMONEDA = idmoneda;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.inicio = Convert.ToDateTime(fecha_ini);
            _entidad.fin = Convert.ToDateTime(fecha_fin);
            _entidad.IDUSUARIO = FkUsua;
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
                if (_entidad.id_proyecto == 0)
                {
                    metodo = "Servicioerp/t_proyectoInsert";
                }
                else
                {
                    metodo = "Servicioerp/t_proyectoUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                    foreach (var item in arrServicios)
                    {
                        ProyectoServicioErp inserta = new ProyectoServicioErp()
                        {
                            fk_proyecto = Convert.ToInt32(idinserted),
                            fk_servicio = Convert.ToInt32(item[0]),
                            IDUSUARIO = FkUsua
                        };
                        var registrado = await InsertaServicioProyecto(inserta);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        private async Task<int> InsertaServicioProyecto(ProyectoServicioErp inserta)
        {
            int insertado = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "Servicioerp/t_proyecto_servicioInsert";

                var response = await client.PostAsJsonAsync(metodo, inserta);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    insertado = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return insertado;
        }

        public async Task<ActionResult> CuentaPagar()
        {
            List<CuentaPorPagarErp> lcuentas = new List<CuentaPorPagarErp>();
            ViewBag.DeudaSoles = 0;
            ViewBag.DeudaDolar = 0;
            ViewBag.DeudaEuros = 0;
            try
            {
                lcuentas = await GetCuentasPagar();
                if (lcuentas != null && lcuentas.Any())
                {
                    ViewBag.DeudaSoles = lcuentas.Where(y => y.IDMONEDA.Equals("01")).Sum(x => x.saldo);
                    ViewBag.DeudaDolar = lcuentas.Where(y => y.IDMONEDA.Equals("02")).Sum(x => x.saldo);
                    ViewBag.DeudaEuros = lcuentas.Where(y => y.IDMONEDA.Equals("03")).Sum(x => x.saldo);
                }
            }
            catch (Exception e)
            {
                lcuentas = new List<CuentaPorPagarErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lcuentas);
        }

        private async Task<List<CuentaPorPagarErp>> GetCuentasPagar()
        {
            List<CuentaPorPagarErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_cuentas_por_pagar_bancosSelectAll");
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<CuentaPorPagarErp>();
                }
            }
            catch (Exception e)
            {
                return new List<CuentaPorPagarErp>();
            }

            return lcuentas;
        }

        public async Task<ActionResult> RegistroPrestamo(int id_cuentas_por_pagar)
        {
            List<Empresa> _empresa = new List<Empresa>();
            List<BancoErp> _banco = new List<BancoErp>();
            List<CuentaPorPagarErp> _lentidad = new List<CuentaPorPagarErp>();
            List<MonedaErp> _lmoneda = new List<MonedaErp>();
            List<MotivoPrestamoErp> _lmotivo = new List<MotivoPrestamoErp>();
            List<TipoPrestamo> _ltipoprestamo = new List<TipoPrestamo>();
            CuentaPorPagarErp _cuenta = new CuentaPorPagarErp();
            if (id_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                    var xentidad = await GetCuentasPagar();
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.FirstOrDefault(x => x.id_cuentas_por_pagar == id_cuentas_por_pagar);
                    }
                }
                catch (Exception ex)
                {
                    id_cuentas_por_pagar = 0;
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                }
            }
            else
            {
                id_cuentas_por_pagar = 0;
                ViewBag.IdCuenta = id_cuentas_por_pagar;
            }

            BancoErp newadd = new BancoErp()
            {
                id_banco = 0,
                codigo_sunat = "",
                descripcion = "",
                idbanco = "",
                NEstado = "1"
            };
            PersonalController person = new PersonalController();
            _banco = await person.GetBancoErp();
            _banco.Add(newadd);
            try
            {
                ViewBag.Empresas = _banco.Where(y => y.estado.Equals("1")).OrderBy(x => x.id_banco).ToList();
            }
            catch (Exception e)
            {
                ViewBag.Empresas = _banco.OrderBy(x => x.id_banco).ToList();
            }


            MonedaErp newaddm = new MonedaErp()
            {
                IDMONEDA = "",
                NOMBRE_CORTO = "",
                DESCRIPCION = "",
                TIPO_MONEDA = "",
                FECHACREACION = DateTime.Now,
                IDREGISTRO_SUNAT = "",
                ESTADO = "1"
            };
            _lmoneda = await GetMonedaErp();
            _lmoneda.Add(newaddm);
            ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();

            MotivoPrestamoErp newmotivo = new MotivoPrestamoErp()
            {
                id_motivo_prestamo = 0,
                descripcion = "",
                estado = "1"
            };
            _lmotivo = await GetMotivosPrestamo();
            _lmotivo.Add(newmotivo);
            ViewBag.Motivos = _lmotivo.Where(y => y.estado.Equals("1")).OrderBy(x => x.id_motivo_prestamo).ToList();

            ViewBag.CuentaPagar = _cuenta;


            _ltipoprestamo.Add(new TipoPrestamo() { idtipoprestamo = "", tipoprestamo = "" });
            _ltipoprestamo.Add(new TipoPrestamo() { idtipoprestamo = "LEASING", tipoprestamo = "LEASING" });
            _ltipoprestamo.Add(new TipoPrestamo() { idtipoprestamo = "PAGARE", tipoprestamo = "PAGARE" });
            _ltipoprestamo.Add(new TipoPrestamo() { idtipoprestamo = "PRESTAMO", tipoprestamo = "PRESTAMO" });
            ViewBag.TipoPrestamo = _ltipoprestamo.OrderBy(x => x.idtipoprestamo).ToList();
            return PartialView();
        }

        private async Task<List<MotivoPrestamoErp>> GetMotivosPrestamo()
        {
            List<MotivoPrestamoErp> lista = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_motivo_prestamosSelectAll");
                var model = response.Content.ReadAsAsync<List<MotivoPrestamoErp>>();
                if (model.Result.Count > 0)
                {
                    lista = model.Result.ToList();
                }
                else
                {
                    lista = new List<MotivoPrestamoErp>();
                }
            }
            catch (Exception e)
            {
                return new List<MotivoPrestamoErp>();
            }

            return lista;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPrestamo(string id_save, int fk_entidad, string tipo,
            string fk_motivo_prestamo, string nro_operacion, string IDMONEDA, decimal monto, string f_operacion,
            string observacion, string cuotas, string dias, string monto_cuota, string interes, List<List<string>> arrCuotas)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            CuentaPorPagarErp _entidad = new CuentaPorPagarErp();

            string f_vencimiento = f_operacion;
            int nro_cuotas = Convert.ToInt32(cuotas);
            foreach (var oneLetrasArr in arrCuotas)
            {
                if (Convert.ToInt32(oneLetrasArr[0]) == nro_cuotas)
                {
                    f_vencimiento = oneLetrasArr[2];
                }
            }


            if (id_save == "0")
            {
                _entidad.id_cuentas_por_pagar = 0;
            }
            else
            {
                _entidad.id_cuentas_por_pagar = Convert.ToInt32(id_save);
            }
            _entidad.fk_entidad = Convert.ToInt32(fk_entidad);
            _entidad.tipo = tipo;
            _entidad.fk_motivo_prestamo = Convert.ToInt32(fk_motivo_prestamo);
            _entidad.nro_operacion = nro_operacion;
            _entidad.IDMONEDA = IDMONEDA;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.f_operacion = Convert.ToDateTime(f_operacion);
            _entidad.f_vencimiento = Convert.ToDateTime(f_vencimiento);
            _entidad.IDUSUARIO = FkUsua;
            _entidad.observacion = observacion;
            //_entidad.estado = "1";
            _entidad.nro_cuotas = nro_cuotas;
            _entidad.dias = Convert.ToInt32(dias);
            _entidad.interes = Convert.ToDecimal(interes);
            _entidad.monto_cuota = Convert.ToDecimal(monto_cuota);

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.id_cuentas_por_pagar == 0)
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_bancosInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_bancosUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                    if (Convert.ToInt32(idinserted) > 0)
                    {
                        foreach (var oneCuotasArr in arrCuotas)
                        {
                            int idinserted2 = 0;

                            try
                            {
                                CuentaPorPagarErp _dentidad = new CuentaPorPagarErp()
                                {
                                    fk_cuentas_por_pagar = Convert.ToInt32(idinserted),
                                    nro_cuota = Convert.ToInt32(oneCuotasArr[0]),
                                    monto_detalle = Convert.ToDecimal(oneCuotasArr[1]),
                                    fecha = Convert.ToDateTime(oneCuotasArr[2])
                                };
                                var client2 = new HttpClient();
                                string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                                client2.BaseAddress = new Uri(connectionInfo2);
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                string metodo2 = "";
                                metodo2 = "Cuentaserp/t_cuentas_por_pagar_bancos_detalleInsert";
                                var response2 = await client2.PostAsJsonAsync(metodo2, _dentidad);
                                var respuesta2 = response2.Content.ReadAsAsync<string>();
                                if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                                {
                                    idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> EliminaPrestamo(int id_cuentas_por_pagar)
        {
            List<CuentaPorPagarErp> _lentidad = new List<CuentaPorPagarErp>();
            CuentaPorPagarErp _entidad = new CuentaPorPagarErp();
            CuentaPorPagarErp busca = new CuentaPorPagarErp()
            {
                id_cuentas_por_pagar = id_cuentas_por_pagar,
                estado = "0"
            };

            try
            {
                _lentidad = await GetCuentasPagar();
                if (_lentidad != null)
                {
                    _entidad = _lentidad.FirstOrDefault(x => x.id_cuentas_por_pagar == id_cuentas_por_pagar);
                }
                else
                {
                    _entidad = null;
                }
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
                    metodo = "Cuentaserp/t_cuentas_por_pagar_bancosUpdateEstado";
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

        public async Task<ActionResult> RegistroPagoPrestamo(int id_cuentas_por_pagar)
        {
            CuentaPorPagarErp _cuenta = new CuentaPorPagarErp();
            if (id_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                    var xentidad = await GetCuentasPagar();
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.FirstOrDefault(x => x.id_cuentas_por_pagar == id_cuentas_por_pagar);
                    }
                }
                catch (Exception ex)
                {
                    id_cuentas_por_pagar = 0;
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                }
            }
            else
            {
                id_cuentas_por_pagar = 0;
                ViewBag.IdCuenta = id_cuentas_por_pagar;
            }

            ViewBag.Cuenta = _cuenta;
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPagoPrestamo(string id_save, int fk_cuentas_por_pagar,
            string f_pago, string nro_operacion, decimal monto)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            CuentaPorPagarPagoErp _entidad = new CuentaPorPagarPagoErp();
            if (id_save == "0")
            {
                _entidad.id_cuentas_por_pagar_pago = 0;
            }
            else
            {


                _entidad.id_cuentas_por_pagar_pago = Convert.ToInt32(id_save);
            }
            _entidad.fk_cuentas_por_pagar = Convert.ToInt32(fk_cuentas_por_pagar);
            _entidad.f_pago = Convert.ToDateTime(f_pago);
            _entidad.nro_operacion = nro_operacion;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.IDUSUARIO = FkUsua;
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
                if (_entidad.id_cuentas_por_pagar_pago == 0)
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_pago_bancosInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_pago_bancosUpdate";
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

        public async Task<ActionResult> DetallePrestamo(int id_cuentas_por_pagar)
        {
            CuentaPorPagarPagoErp _cuenta_pagos = new CuentaPorPagarPagoErp();
            List<CuentaPorPagarPagoErp> _lcuenta_pagos = new List<CuentaPorPagarPagoErp>();
            if (id_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                    var xentidad = await GetCuentaPorPagarPagoErpFk(id_cuentas_por_pagar);
                    if (xentidad != null && xentidad.Any())
                    {
                        _lcuenta_pagos = xentidad.OrderBy(x => x.f_pago).ToList();
                    }
                }
                catch (Exception ex)
                {
                    id_cuentas_por_pagar = 0;
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                }
            }
            else
            {
                id_cuentas_por_pagar = 0;
                ViewBag.IdCuenta = id_cuentas_por_pagar;
            }
            return PartialView(_lcuenta_pagos);
        }
        private async Task<List<CuentaPorPagarPagoErp>> GetCuentaPorPagarPagoErpFk(int id_cuentas_por_pagar)
        {
            List<CuentaPorPagarPagoErp> lcuentas = null;
            CuentaPorPagarPagoErp busca = new CuentaPorPagarPagoErp { fk_cuentas_por_pagar = id_cuentas_por_pagar };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_pago_bancosSelectById", busca);
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarPagoErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<CuentaPorPagarPagoErp>();
                }
            }
            catch (Exception e)
            {
                lcuentas = new List<CuentaPorPagarPagoErp>();
            }
            return lcuentas;
        }
        [HttpPost]
        public async Task<JsonResult> EliminaPagoPrestamo(int id_cuentas_por_pagar_pago)
        {
            List<CuentaPorPagarPagoErp> _lentidad = new List<CuentaPorPagarPagoErp>();
            CuentaPorPagarPagoErp _entidad = new CuentaPorPagarPagoErp();
            CuentaPorPagarPagoErp busca = new CuentaPorPagarPagoErp()
            {
                id_cuentas_por_pagar_pago = id_cuentas_por_pagar_pago,
                estado = "0"
            };

            try
            {
                _entidad = await GetCuentaPorPagarPagoErpId(id_cuentas_por_pagar_pago);
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
                    metodo = "Cuentaserp/t_cuentas_por_pagar_pago_bancosUpdateEstado";
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

        private async Task<CuentaPorPagarPagoErp> GetCuentaPorPagarPagoErpId(int id_cuentas_por_pagar_pago)
        {
            List<CuentaPorPagarPagoErp> lcuentas = null;
            CuentaPorPagarPagoErp cuenta = null;
            CuentaPorPagarPagoErp busca = new CuentaPorPagarPagoErp { id_cuentas_por_pagar_pago = id_cuentas_por_pagar_pago };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_pago_bancosSelect", busca);
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarPagoErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                    cuenta = lcuentas[0];
                }
                else
                {
                    cuenta = new CuentaPorPagarPagoErp();
                }
            }
            catch (Exception e)
            {
                cuenta = new CuentaPorPagarPagoErp();
            }
            return cuenta;
        }

        public async Task<ActionResult> CuentaPagarProveedor()
        {
            List<CuentaPagarProveedorErp> lcuentas = new List<CuentaPagarProveedorErp>();
            ViewBag.DeudaSoles = 0;
            ViewBag.DeudaDolar = 0;
            ViewBag.DeudaEuros = 0;
            try
            {
                lcuentas = await GetCuentasPagarProveedor();
                if (lcuentas != null && lcuentas.Any())
                {
                    ViewBag.DeudaSoles = lcuentas.Where(y => y.IDMONEDA.Equals("01")).Sum(x => x.saldo);
                    ViewBag.DeudaDolar = lcuentas.Where(y => y.IDMONEDA.Equals("02")).Sum(x => x.saldo);
                    ViewBag.DeudaEuros = lcuentas.Where(y => y.IDMONEDA.Equals("03")).Sum(x => x.saldo);
                }
            }
            catch (Exception e)
            {
                lcuentas = new List<CuentaPagarProveedorErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lcuentas);
        }

        public async Task<ActionResult> ListRegistros(string EstaFilt)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftptareos"];
            ViewBag.UrlLink = virtualdir;
            List<CuentaPagarProveedorErp> lcuentas = new List<CuentaPagarProveedorErp>();
            List<CuentaPagarProveedorErp> _lcontrol = new List<CuentaPagarProveedorErp>();
            try
            {
                lcuentas = await GetCuentasPagarProveedor();
                if (lcuentas == null || !lcuentas.Any())
                {
                    _lcontrol = new List<CuentaPagarProveedorErp>();
                }
                else
                {
                    if (EstaFilt.Equals("1"))
                    {
                        _lcontrol = lcuentas.Where(y => y.saldo == 0).ToList();
                    }
                    else if (EstaFilt.Equals("2"))
                    {
                        _lcontrol = lcuentas.Where(y => y.saldo > 0).ToList();
                    }
                    else if (EstaFilt.Equals("3"))
                    {
                        _lcontrol = lcuentas.ToList();
                    }
                    else if (EstaFilt.Equals("4"))
                    {
                        _lcontrol = new List<CuentaPagarProveedorErp>();
                    }
                }
            }
            catch (Exception)
            {
                _lcontrol = new List<CuentaPagarProveedorErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.IDUSUARIOTIPO = IDUSUARIOTIPO;
            return PartialView(_lcontrol);
        }

        private async Task<List<CuentaPagarProveedorErp>> GetCuentasPagarProveedor()
        {
            List<CuentaPagarProveedorErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_cuentas_por_pagar_proveedorSelectAll");
                var model = response.Content.ReadAsAsync<List<CuentaPagarProveedorErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<CuentaPagarProveedorErp>();
                }
            }
            catch (Exception e)
            {
                return new List<CuentaPagarProveedorErp>();
            }

            return lcuentas;
        }

        public async Task<ActionResult> RegistroCuentaPagarProveedor(int id_cuentas_por_pagar)
        {
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            List<ComprobanteTipo> lstComprobanteTipo = new List<ComprobanteTipo>();
            lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
            lstComprobanteTipo.Add(new ComprobanteTipo { id_comprobante_tipo = 0, descripcion = "" });
            lstComprobanteTipo = lstComprobanteTipo.OrderBy(i => i.id_comprobante_tipo).ToList();
            ViewBag.ComprobanteTipo = lstComprobanteTipo;

            List<Empresa> _empresa = new List<Empresa>();
            Empresa newadd = new Empresa()
            {
                id_empresa = 0,
                ruc = "",
                razon_social = "",
                contacto = "",
                email = "",
                estado = "1",
                telefono = "",
                direccion = "",
                tipo = "C"
            };

            _empresa = await GetEmpresasErp();
            _empresa.Add(newadd);
            try
            {
                ViewBag.Empresas = _empresa.Where(y => y.estado.Equals("1") && y.tipo.Equals("P")).OrderBy(x => x.id_empresa).ToList();
            }
            catch (Exception e)
            {
                ViewBag.Empresas = _empresa.OrderBy(x => x.id_empresa).ToList();
            }

            List<MonedaErp> _lmoneda = new List<MonedaErp>();
            MonedaErp newaddm = new MonedaErp()
            {
                IDMONEDA = "",
                NOMBRE_CORTO = "",
                DESCRIPCION = "",
                TIPO_MONEDA = "",
                FECHACREACION = DateTime.Now,
                IDREGISTRO_SUNAT = "",
                ESTADO = "1"
            };
            _lmoneda = await GetMonedaErp();
            _lmoneda.Add(newaddm);
            ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();
            CuentaPagarProveedorErp _cuenta = new CuentaPagarProveedorErp();
            if (id_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                    var xentidad = await GetCuentasPagarProveedor();
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.FirstOrDefault(x => x.id_cuentas_por_pagar_proveedor == id_cuentas_por_pagar);
                    }
                }
                catch (Exception ex)
                {
                    id_cuentas_por_pagar = 0;
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                }
            }
            else
            {
                id_cuentas_por_pagar = 0;
                ViewBag.IdCuenta = id_cuentas_por_pagar;
            }

            ViewBag.Cuenta = _cuenta;
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> GuardaCtaPagarProveedor(string id_save, int fk_empresa, int fk_comprobantetipo,
            string documento, string IDMONEDA, decimal monto, string f_emision, string f_vencimiento,
            string afectaDetraccion, decimal montoDetraccion, string observacion, string detraccion_estados)
        {
            //string cafectadetraccion = "1";
            //if (afectaDetraccion == "NO")
            //{
            //    cafectadetraccion = "0";
            //}
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            CuentaPagarProveedorErp _entidad = new CuentaPagarProveedorErp();
            if (id_save == "0")
            {
                _entidad.id_cuentas_por_pagar_proveedor = 0;
            }
            else
            {


                _entidad.id_cuentas_por_pagar_proveedor = Convert.ToInt32(id_save);
            }
            _entidad.fk_proveedor = Convert.ToInt32(fk_empresa);
            _entidad.fk_comprobante_tipo = Convert.ToInt32(fk_comprobantetipo);
            _entidad.documento = documento;
            _entidad.IDMONEDA = IDMONEDA;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.f_emision = Convert.ToDateTime(f_emision);
            _entidad.f_vencimiento = Convert.ToDateTime(f_vencimiento);
            _entidad.afecta_detraccion = afectaDetraccion;
            _entidad.monto_detraccion = Convert.ToDecimal(montoDetraccion);
            _entidad.observacion = observacion;
            _entidad.IDUSUARIO = FkUsua;
            _entidad.detraccion_estado = detraccion_estados;
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
                if (_entidad.id_cuentas_por_pagar_proveedor == 0)
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_proveedorInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_proveedorUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                    //if (detraccion_estados == "1")
                    //{
                    //    var existe = await GetCuentaPorPagarProveedorPagoErpDetra(Convert.ToInt32(idinserted));
                    //    int idinserted2 = 0;
                    //    CuentaPorPagarProveedorPagoErp busca2 = new CuentaPorPagarProveedorPagoErp()
                    //    {
                    //        fk_cuentas_por_pagar_proveedor = Convert.ToInt32(idinserted),
                    //        IDUSUARIO = FkUsua,
                    //        f_pago = DateTime.Now,
                    //        nro
                    //        monto = Convert.ToDecimal(montoDetraccion)
                    //    };
                    //    if (existe != null)
                    //    {
                    //        try
                    //        {
                    //            var client2 = new HttpClient();
                    //            string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    //            client2.BaseAddress = new Uri(connectionInfo2);
                    //            client2.DefaultRequestHeaders.Accept.Clear();
                    //            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //            string metodo2 = "";
                    //            metodo2 = "Cuentaserp/t_cuentas_por_pagar_pago_proveedorUpdateDetra";
                    //            var response2 = await client2.PostAsJsonAsync(metodo, busca2);
                    //            var respuesta2 = response2.Content.ReadAsAsync<string>();
                    //            if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                    //            {
                    //                idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            return Json("0", JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EliminaCtaProveedor(int id_cuentas_por_pagar)
        {
            List<CuentaPagarProveedorErp> _lentidad = new List<CuentaPagarProveedorErp>();
            CuentaPagarProveedorErp _entidad = new CuentaPagarProveedorErp();
            CuentaPagarProveedorErp busca = new CuentaPagarProveedorErp()
            {
                id_cuentas_por_pagar_proveedor = id_cuentas_por_pagar,
                estado = "0"
            };

            try
            {
                _lentidad = await GetCuentasPagarProveedor();
                if (_lentidad != null)
                {
                    _entidad = _lentidad.FirstOrDefault(x => x.id_cuentas_por_pagar_proveedor == id_cuentas_por_pagar);
                }
                else
                {
                    _entidad = null;
                }
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
                    metodo = "Cuentaserp/t_cuentas_por_pagar_proveedorUpdateEstado";
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

        public async Task<ActionResult> RegistroPagoProveedor(int id_cuentas_por_pagar)
        {
            CuentaPagarProveedorErp _cuenta = new CuentaPagarProveedorErp();
            if (id_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                    var xentidad = await GetCuentasPagarProveedor();
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.FirstOrDefault(x => x.id_cuentas_por_pagar_proveedor == id_cuentas_por_pagar);
                    }
                }
                catch (Exception ex)
                {
                    id_cuentas_por_pagar = 0;
                    ViewBag.IdCuenta = id_cuentas_por_pagar;
                }
            }
            else
            {
                id_cuentas_por_pagar = 0;
                ViewBag.IdCuenta = id_cuentas_por_pagar;
            }

            ViewBag.Cuenta = _cuenta;
            return PartialView();
        }
        private async Task<CuentaPorPagarProveedorPagoErp> GetCuentaPorPagarProveedorPagoErpId(int id_cuentas_por_pagar_pago)
        {
            List<CuentaPorPagarProveedorPagoErp> lcuentas = null;
            CuentaPorPagarProveedorPagoErp cuenta = null;
            CuentaPorPagarProveedorPagoErp busca = new CuentaPorPagarProveedorPagoErp { id_cuentas_por_pagar_proveedor_pago = id_cuentas_por_pagar_pago };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_pago_proveedorSelect", busca);
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarProveedorPagoErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                    cuenta = lcuentas[0];
                }
                else
                {
                    cuenta = new CuentaPorPagarProveedorPagoErp();
                }
            }
            catch (Exception e)
            {
                cuenta = new CuentaPorPagarProveedorPagoErp();
            }
            return cuenta;
        }

        private async Task<CuentaPorPagarProveedorPagoErp> GetCuentaPorPagarProveedorPagoErpDetra(int id_cuentas_por_pagar_pago)
        {
            List<CuentaPorPagarProveedorPagoErp> lcuentas = null;
            CuentaPorPagarProveedorPagoErp cuenta = null;
            CuentaPorPagarProveedorPagoErp busca = new CuentaPorPagarProveedorPagoErp { fk_cuentas_por_pagar_proveedor = id_cuentas_por_pagar_pago };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_pago_proveedorSelectDetr", busca);
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarProveedorPagoErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                    cuenta = lcuentas[0];
                }
                else
                {
                    cuenta = null;
                }
            }
            catch (Exception e)
            {
                cuenta = null;
            }
            return cuenta;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPagoProveedor(string id_save, int fk_cuentas_por_pagar,
            string f_pago, string nro_operacion, string monto, string tipo)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            CuentaPorPagarProveedorPagoErp _entidad = new CuentaPorPagarProveedorPagoErp();
            if (id_save == "0")
            {
                _entidad.id_cuentas_por_pagar_proveedor_pago = 0;
            }
            else
            {
                _entidad.id_cuentas_por_pagar_proveedor_pago = Convert.ToInt32(id_save);
            }
            _entidad.fk_cuentas_por_pagar_proveedor = Convert.ToInt32(fk_cuentas_por_pagar);
            _entidad.f_pago = Convert.ToDateTime(f_pago);
            _entidad.nro_operacion = nro_operacion;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.IDUSUARIO = FkUsua;
            _entidad.tipo = tipo;
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
                if (_entidad.id_cuentas_por_pagar_proveedor_pago == 0)
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_pago_proveedorInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_pago_proveedorUpdateDetra";
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

        public async Task<ActionResult> DetalleCtaProveedor(int id_cuentas_por_pagar_proveedor)
        {
            CuentaPorPagarProveedorPagoErp _cuenta_pagos = new CuentaPorPagarProveedorPagoErp();
            List<CuentaPorPagarProveedorPagoErp> _lcuenta_pagos = new List<CuentaPorPagarProveedorPagoErp>();
            if (id_cuentas_por_pagar_proveedor != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_cuentas_por_pagar_proveedor;
                    var xentidad = await GetCuentaPorPagarPagoErpProvFk(id_cuentas_por_pagar_proveedor);
                    if (xentidad != null && xentidad.Any())
                    {
                        _lcuenta_pagos = xentidad.OrderBy(x => x.f_pago).ToList();
                    }
                }
                catch (Exception ex)
                {
                    id_cuentas_por_pagar_proveedor = 0;
                    ViewBag.IdCuenta = id_cuentas_por_pagar_proveedor;
                }
            }
            else
            {
                id_cuentas_por_pagar_proveedor = 0;
                ViewBag.IdCuenta = id_cuentas_por_pagar_proveedor;
            }
            return PartialView(_lcuenta_pagos);
        }
        private async Task<List<CuentaPorPagarProveedorPagoErp>> GetCuentaPorPagarPagoErpProvFk(int id_cuentas_por_pagar_proveedor)
        {
            List<CuentaPorPagarProveedorPagoErp> lcuentas = null;
            CuentaPorPagarProveedorPagoErp busca = new CuentaPorPagarProveedorPagoErp { fk_cuentas_por_pagar_proveedor = id_cuentas_por_pagar_proveedor };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_pago_proveedorSelectById", busca);
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarProveedorPagoErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<CuentaPorPagarProveedorPagoErp>();
                }
            }
            catch (Exception e)
            {
                lcuentas = new List<CuentaPorPagarProveedorPagoErp>();
            }
            return lcuentas;
        }

        [HttpPost]
        public async Task<JsonResult> EliminaPagoCtaProveedor(int id_cuentas_por_pagar_proveedor_pago)
        {
            List<CuentaPorPagarProveedorPagoErp> _lentidad = new List<CuentaPorPagarProveedorPagoErp>();
            CuentaPorPagarProveedorPagoErp _entidad = new CuentaPorPagarProveedorPagoErp();
            CuentaPorPagarProveedorPagoErp busca = new CuentaPorPagarProveedorPagoErp()
            {
                id_cuentas_por_pagar_proveedor_pago = id_cuentas_por_pagar_proveedor_pago,
                estado = "0"
            };

            try
            {
                _entidad = await GetCuentaPorPagarProveedorPagoErpId(id_cuentas_por_pagar_proveedor_pago);
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
                    metodo = "Cuentaserp/t_cuentas_por_pagar_pago_proveedorUpdateEstado";
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

        private async Task<List<GastoPersonalErp>> GetGastoPersonalErp()
        {
            List<GastoPersonalErp> lista = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_gasto_personalSelectAll");
                var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lista = model.Result.ToList();
                }
                else
                {
                    lista = new List<GastoPersonalErp>();
                }
            }
            catch (Exception e)
            {
                return new List<GastoPersonalErp>();
            }

            return lista;
        }
        public async Task<ActionResult> CuentasGastosPersonal()
        {
            List<GastoPersonalErp> lcuentas = new List<GastoPersonalErp>();

            try
            {
                lcuentas = await GetGastoPersonalErp();
                if (lcuentas != null && lcuentas.Any())
                {

                }
            }
            catch (Exception e)
            {
                lcuentas = new List<GastoPersonalErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lcuentas);
        }
        CompraErpController compraCtrl = new CompraErpController();
        public async Task<ActionResult> RegistroCuentaGasto(int id_gasto_personal)
        {
            string nuevocode = await GetCodigoGastoPersonalErp();
            ViewBag.NuevoCodigo = nuevocode;

            List<PersonalErp> _lentidadpersonal = new List<PersonalErp>();
            PersonalErp _entidadpersonal = new PersonalErp()
            {
                IDCODIGOGENERAL = "",
                NOMBRES = "",
                A_PATERNO = "",
                A_MATERNO = "",
                NOMBRES_FULL = "",
                ESTADO = "1"
            };

            _lentidadpersonal = await compraCtrl.GetPersonalErp();
            _lentidadpersonal.Add(_entidadpersonal);
            ViewBag.Personal = _lentidadpersonal.OrderBy(z => z.NOMBRES_FULL).ToList();


            List<GastoPersonalErp> _cuenta = new List<GastoPersonalErp>();
            if (id_gasto_personal != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_gasto_personal;
                    var xentidad = await GetGastoPersonalErpId(id_gasto_personal);
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.ToList();
                    }
                }
                catch (Exception ex)
                {
                    id_gasto_personal = 0;
                    ViewBag.IdCuenta = id_gasto_personal;
                }
            }
            else
            {
                id_gasto_personal = 0;
                ViewBag.IdCuenta = id_gasto_personal;
            }

            ViewBag.Cuenta = _cuenta;
            return PartialView();
        }

        private async Task<List<GastoPersonalErp>> GetGastoPersonalErpId(int id_gasto_personal)
        {
            List<GastoPersonalErp> lempresa = null;
            GastoPersonalErp busca = new GastoPersonalErp { id_gasto_personal = id_gasto_personal };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personalSelect", busca);
            var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }
        private async Task<string> GetCodigoGastoPersonalErp()
        {
            List<GastoPersonalErp> lista = null;
            string nuevocodigo = "";
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_gasto_personalNewCodigo");
                var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lista = model.Result.ToList();
                    nuevocodigo = lista[0].codigo;
                }
                else
                {
                    lista = new List<GastoPersonalErp>();
                    nuevocodigo = "00001";
                }
            }
            catch (Exception e)
            {
                return nuevocodigo = "00001";
            }

            return nuevocodigo;
        }

        public async Task<ActionResult> RegistroDetalleCuentaGasto(int id_gasto_personal, string codigoo, string fechaa, string responsable)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            int idinserted = 0;
            if (id_gasto_personal == 0)
            {
                GastoPersonalErp inserter = new GastoPersonalErp()
                {
                    codigo = codigoo.Trim(),
                    IDUSUARIO = FkUsua,
                    IDCODIGOGENERAL = responsable,
                    f_apertura = Convert.ToDateTime(fechaa),
                    estado = "1"
                };

                string metodo = "";
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    metodo = "Cuentaserp/t_gasto_personalInsert";

                    var response = await client.PostAsJsonAsync(metodo, inserter);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = Convert.ToInt32(respuesta.Result.ToString());
                    }
                }
                catch (Exception ex)
                {
                    idinserted = 0;
                }
            }
            else
            {
                idinserted = id_gasto_personal;
            }
            ViewBag.IdCuenta = idinserted;


            return PartialView();
        }
        private async Task<List<GastoPersonalErp>> GetGastoPersonalDetalleErpId(int id_gasto_personal)
        {
            List<GastoPersonalErp> lempresa = null;
            GastoPersonalErp busca = new GastoPersonalErp { id_gasto_personal = id_gasto_personal };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personalDetalleSelect", busca);
                var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lempresa = model.Result.ToList();
                }
                else
                {
                    lempresa = new List<GastoPersonalErp>();
                }
            }
            catch (Exception e)
            {
                lempresa = new List<GastoPersonalErp>();
            }
            return lempresa;
        }
        public async Task<ActionResult> ListadoDetalleCuentaGasto(int id_gasto_personal)
        {

            List<GastoPersonalErp> _cuenta = new List<GastoPersonalErp>();
            if (id_gasto_personal != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_gasto_personal;
                    //var xentidad = await GetGastoPersonalDetalleErpId(id_gasto_personal);
                    var xentidad = await GetGastoPersonalDetalleErpFk(id_gasto_personal);
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.ToList();
                    }
                }
                catch (Exception ex)
                {
                    id_gasto_personal = 0;
                    ViewBag.IdCuenta = id_gasto_personal;
                }
            }
            else
            {
                id_gasto_personal = 0;
                ViewBag.IdCuenta = id_gasto_personal;
            }

            ViewBag.CuentaDetalle = _cuenta;
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> GuardaDetalleGasto(int id_save, int id_gasto_personal, string nro_operacion,
            string fecha_operacion, decimal monto, string observacion)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            GastoPersonalDetalleErp _entidad = new GastoPersonalDetalleErp();
            if (id_save == 0)
            {
                _entidad.id_gasto_personal_detalle = 0;
            }
            else
            {
                _entidad.id_gasto_personal_detalle = Convert.ToInt32(id_save);
            }
            _entidad.fk_gasto_personal = Convert.ToInt32(id_gasto_personal);
            _entidad.nro_operacion = nro_operacion;
            _entidad.f_operacion = Convert.ToDateTime(fecha_operacion);
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.observacion = observacion;
            _entidad.IDUSUARIO = FkUsua;
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
                if (_entidad.id_gasto_personal_detalle == 0)
                {
                    metodo = "Cuentaserp/t_gasto_personal_detalleInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_gasto_personal_detalleUpdate";
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

        [HttpPost]
        public async Task<JsonResult> EliminaCtaGasto(int id_gasto_personal)
        {
            List<GastoPersonalErp> _lentidad = new List<GastoPersonalErp>();
            GastoPersonalErp _entidad = new GastoPersonalErp();
            GastoPersonalErp busca = new GastoPersonalErp()
            {
                id_gasto_personal = id_gasto_personal,
                estado = "0"
            };

            try
            {
                _lentidad = await GetGastoPersonalErpId(id_gasto_personal);
                if (_lentidad != null && _lentidad.Any())
                {
                    _entidad = _lentidad.FirstOrDefault(x => x.id_gasto_personal == id_gasto_personal);
                }
                else
                {
                    _entidad = null;
                }
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
                    metodo = "Cuentaserp/t_gasto_personalDelete";
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
        [HttpPost]
        public async Task<JsonResult> EliminaDetalleCtaGasto(int id_gasto_personal_detalle)
        {
            List<GastoPersonalErp> _lentidad = new List<GastoPersonalErp>();
            GastoPersonalErp _entidad = new GastoPersonalErp();
            GastoPersonalErp busca = new GastoPersonalErp()
            {
                id_gasto_personal_detalle = id_gasto_personal_detalle,
                estado = "0"
            };

            try
            {
                _entidad = await GetGastoPersonalDetalleErpIDet(id_gasto_personal_detalle);

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
                    metodo = "Cuentaserp/t_gasto_personal_detalleDelete";
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

        private async Task<GastoPersonalErp> GetGastoPersonalDetalleErpIDet(int id_gasto_personal_detalle)
        {
            List<GastoPersonalErp> lentidad = null;
            GastoPersonalErp entidad = null;
            GastoPersonalErp busca = new GastoPersonalErp { id_gasto_personal_detalle = id_gasto_personal_detalle };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personalDetalleSelectId", busca);
                var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                    entidad = lentidad.FirstOrDefault();
                }
                else
                {
                    entidad = new GastoPersonalErp();
                }
            }
            catch (Exception e)
            {
                entidad = new GastoPersonalErp();
            }
            return entidad;
        }
        public async Task<ActionResult> GastosLiquidacion()
        {
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftptareos"];
            ViewBag.UrlLink = virtualdir;
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();
            string IDCODIGOGENERAL = Session["DNI"].ToString().Trim();

            var fechahoy = DateTime.Now;


            List<GastoPersonalErp> _lcontrol = new List<GastoPersonalErp>();
            try
            {
                var lista = await GetGastoPersonalErp();
                if (lista != null && lista.Any())
                {
                    if (IDUSUARIOTIPO.Trim().Equals("ADM"))
                    {
                        _lcontrol = lista.ToList();
                    }
                    else
                    {
                        _lcontrol = lista.Where(x => x.IDCODIGOGENERAL.Equals(IDCODIGOGENERAL) && x.estado.Equals("1") && x.MONTO > 0).ToList();
                    }

                }
            }
            catch (Exception e)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.IDUSUARIOTIPO = IDUSUARIOTIPO;
            return View(_lcontrol);
        }
        public async Task<ActionResult> RegistroLiquidacion(int id_gasto_personal)
        {
            string shortname = System.Configuration.ConfigurationManager.AppSettings["ShortName"];
            string nuevocode = await GetCodigoGastoPersonalErp();
            ViewBag.NuevoCodigo = nuevocode;

            List<GastoPersonalErp> _cuenta = new List<GastoPersonalErp>();
            List<GastoPersonalErp> _ingresos = new List<GastoPersonalErp>();
            if (id_gasto_personal != 0)
            {
                try
                {
                    ViewBag.IdCuenta = id_gasto_personal;
                    var xentidad = await GetGastoPersonalDetalleErpId(id_gasto_personal);
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.ToList();
                        var xingresos = await GetGastoPersonalIngresoId(id_gasto_personal);
                        if (xingresos != null && xingresos.Any())
                        {
                            _ingresos = xingresos.ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    id_gasto_personal = 0;
                    ViewBag.IdCuenta = id_gasto_personal;
                }
            }
            else
            {
                id_gasto_personal = 0;
                ViewBag.IdCuenta = id_gasto_personal;
            }

            ViewBag.Cuenta = _cuenta;
            ViewBag.Ingresos = _ingresos;
            ViewBag.ShortName = shortname.ToUpper().ToString();
            return PartialView();
        }

        private async Task<List<GastoPersonalErp>> GetGastoPersonalIngresoId(int id_gasto_personal)
        {
            List<GastoPersonalErp> lempresa = null;

            GastoPersonalErp busca = new GastoPersonalErp { id_gasto_personal = id_gasto_personal };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personalIngresoSelect", busca);
                var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lempresa = model.Result.ToList();
                }
                else
                {
                    lempresa = new List<GastoPersonalErp>();
                }
            }
            catch (Exception e)
            {
                lempresa = new List<GastoPersonalErp>();
            }
            return lempresa;
        }

        public async Task<ActionResult> ListadoDetalleLiquidacion(int fk_gasto_personal)
        {
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();
            ViewBag.IDUSUARIOTIPO = IDUSUARIOTIPO;
            var ingresodetalle = await GetGastoPersonalIngresoId(fk_gasto_personal);
            decimal saldo = 0;
            if (ingresodetalle.Any())
            {
                saldo = ingresodetalle.Sum(x => x.monto);
            }
            List<GastoPersonalLiquidacionErp> _cuenta = new List<GastoPersonalLiquidacionErp>();
            List<GastoPersonalErp> _ingresos = new List<GastoPersonalErp>();
            GastoPersonalErp xnew = new GastoPersonalErp()
            {
                id_gasto_personal_detalle = 0,
                nro_operacion = ""
            };
            if (fk_gasto_personal != 0)
            {
                try
                {
                    ViewBag.FkGasto = fk_gasto_personal;
                    var xentidad = await GetGastoPersonalLiquidacionErpFk(fk_gasto_personal);
                    if (xentidad != null && xentidad.Any())
                    {
                        _cuenta = xentidad.ToList();
                    }

                    var xingresos = await GetGastoPersonalIngresoId(fk_gasto_personal);
                    if (xingresos != null && xingresos.Any())
                    {
                        var found = xingresos.Where(x => x.estado_detalle.Equals("1")).ToList();
                        _ingresos.Add(xnew);
                        foreach (var item in found)
                        {
                            int idd = item.id_gasto_personal_detalle;
                            string nroperacion = item.sf_operacion + " " + item.nro_operacion;
                            _ingresos.Add(new GastoPersonalErp { id_gasto_personal_detalle = idd, nro_operacion = nroperacion });
                        }
                    }
                }
                catch (Exception ex)
                {
                    fk_gasto_personal = 0;
                    ViewBag.FkGasto = fk_gasto_personal;
                }
            }
            else
            {
                fk_gasto_personal = 0;
                ViewBag.FkGasto = fk_gasto_personal;
            }

            ViewBag.Liquidaciones = _cuenta;
            ViewBag.Saldox = saldo;
            ViewBag.Ingresos = _ingresos.OrderBy(y => y.id_gasto_personal_detalle).ToList();
            return PartialView();
        }

        private async Task<List<GastoPersonalLiquidacionErp>> GetGastoPersonalLiquidacionErpFk(int fk_gasto_personal)
        {
            List<GastoPersonalLiquidacionErp> lentidad = null;
            GastoPersonalLiquidacionErp busca = new GastoPersonalLiquidacionErp { fk_gasto_personal = fk_gasto_personal };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personal_liquidacionSelect", busca);
                var model = response.Content.ReadAsAsync<List<GastoPersonalLiquidacionErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<GastoPersonalLiquidacionErp>();
                }
            }
            catch (Exception e)
            {
                lentidad = new List<GastoPersonalLiquidacionErp>();
            }
            return lentidad;
        }

        public async Task<ActionResult> RegistroLiquidacionGasto(int id_gasto_personal)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            int idinserted = 0;
            if (id_gasto_personal == 0)
            {
                idinserted = 0;
            }
            else
            {
                idinserted = id_gasto_personal;
            }
            ViewBag.FkGasto = idinserted;
            List<ProyectoErp> _lentidadproyecto = new List<ProyectoErp>();
            ProyectoErp _entidadproyecto = new ProyectoErp()
            {
                id_proyecto = 0,
                codigo = "",
                fk_empresa = 0,
                descripcion = "",
                observacion = "",
                estado = "1"
            };

            _lentidadproyecto = await compraCtrl.GetProyectoErp();
            _lentidadproyecto.Add(_entidadproyecto);
            ViewBag.Proyectos = _lentidadproyecto.OrderBy(z => z.descripcion).ToList();
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> GuardaDetalleLiquidacion(int id_save, int id_gasto_personal, string documento,
            string concepto, string destino, string fecha_operacion, decimal monto, string ruc, string razon_social, string fk_proyecto)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            GastoPersonalLiquidacionErp _entidad = new GastoPersonalLiquidacionErp();
            if (id_save == 0)
            {
                _entidad.id_gasto_personal_liquidacion = 0;
            }
            else
            {
                _entidad.id_gasto_personal_liquidacion = Convert.ToInt32(id_save);
            }
            _entidad.fk_gasto_personal = Convert.ToInt32(id_gasto_personal);
            _entidad.documento = documento;
            _entidad.concepto = concepto;
            _entidad.f_emision = Convert.ToDateTime(fecha_operacion);
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.estado = "1";
            _entidad.destino = destino;
            _entidad.ruc = ruc;
            _entidad.razon_social = razon_social;
            _entidad.fk_proyecto = Convert.ToInt32(fk_proyecto);
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.id_gasto_personal_liquidacion == 0)
                {
                    metodo = "Cuentaserp/t_gasto_personal_liquidacionInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_gasto_personal_liquidacionUpdate";
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

        [HttpPost]
        public async Task<ActionResult> EliminaDetalleLiquidacion(int id_gasto_personal_liquidacion)
        {
            List<GastoPersonalLiquidacionErp> _lentidad = new List<GastoPersonalLiquidacionErp>();
            GastoPersonalLiquidacionErp _entidad = new GastoPersonalLiquidacionErp();
            GastoPersonalLiquidacionErp busca = new GastoPersonalLiquidacionErp()
            {
                id_gasto_personal_liquidacion = id_gasto_personal_liquidacion,
                estado = "0"
            };
            string idinserted = "0";
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "Cuentaserp/t_gasto_personal_liquidacionDelete";
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
            //}
            //else
            //{

            //}
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> CheckDetalleLiquidacion(string id_gasto_personal_liquidacion, string revisado)
        {
            List<GastoPersonalLiquidacionErp> _lentidad = new List<GastoPersonalLiquidacionErp>();
            GastoPersonalLiquidacionErp _entidad = new GastoPersonalLiquidacionErp();
            GastoPersonalLiquidacionErp busca = new GastoPersonalLiquidacionErp()
            {
                id_gasto_personal_liquidacion = Convert.ToInt32(id_gasto_personal_liquidacion),
                revisado = revisado
            };
            string idinserted = "0";
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "Cuentaserp/t_gasto_personal_liquidacionCheck";
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
            //}
            //else
            //{

            //}
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> PrintLiquidacionView(int id_gasto_personal)
        {
            string codigo = "";

            List<GastoPersonalLiquidacionErp> lgastoliq = new List<GastoPersonalLiquidacionErp>();
            List<GastoPersonalErp> lgastodetalle = new List<GastoPersonalErp>();
            try
            {
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();

                var data = await GetGastoRpt(id_gasto_personal);
                lgastoliq = await GetGastoPersonalLiquidacionErpFk(id_gasto_personal);
                lgastodetalle = await GetGastoPersonalDetalleErpFk(id_gasto_personal);
                if (data.Any())
                {
                    codigo = data[0].codigo_full;
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        //string path = Server.MapPath(logoreport);
                        string path = Path.Combine(Server.MapPath(logoreport));

                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;
                        ReportDocument rd = new ReportDocument();
                        //rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/ReporteLiquidacionErp.rpt")));
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/ReportLiquidacionErp.rpt")));
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetDatabaseLogon(datauser, dataclave, dataurl, databases);
                        rd.SetParameterValue("@id_gasto_personal", id_gasto_personal);

                        //rd.SetDataSource(data);
                        //rd.Subreports[0].DataSourceConnections.Clear();
                        //rd.Subreports[1].SetDataSource(lgastoliq);
                        //rd.Subreports[0].SetDataSource(lgastodetalle);

                        for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                        {
                            if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                            {
                                rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                            }
                        }
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\Liquidacion-" + codigo + ".pdf");
                    }
                    ViewBag.Printer = "Liquidacion-" + codigo;
                    ViewBag.Codigo = "Liquidacion-" + codigo + ".pdf";

                }
                //return null;
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar! " + ex.Message;
                ViewBag.Codigo = "";
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        private async Task<List<GastoPersonalErp>> GetGastoPersonalDetalleErpFk(int id_gasto_personal)
        {
            List<GastoPersonalErp> lgastos = new List<GastoPersonalErp>();
            GastoPersonalErp busca = new GastoPersonalErp { id_gasto_personal = id_gasto_personal };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personalIngresoSelect", busca);
            var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
            if (model.Result.Count > 0)
            {
                lgastos = model.Result.ToList();
            }
            return lgastos;
        }

        private async Task<List<GastoPersonalErp>> GetGastoRpt(int id_gasto_personal)
        {
            List<GastoPersonalErp> lgastos = null;
            GastoPersonalErp busca = new GastoPersonalErp { id_gasto_personal = id_gasto_personal };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personalDetalleSelectRp", busca);
            var model = response.Content.ReadAsAsync<List<GastoPersonalErp>>();
            if (model.Result.Count > 0)
            {
                lgastos = model.Result.ToList();
            }
            return lgastos;
        }
        private async Task<List<GastoPersonalLiquidacionErp>> GetGastoLiqRpt(int id_gasto_personal)
        {
            List<GastoPersonalLiquidacionErp> lgastos = null;
            GastoPersonalLiquidacionErp busca = new GastoPersonalLiquidacionErp { id_gasto_personal = id_gasto_personal };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Cuentaserp/t_gasto_personal_liquidacionSelectRP", busca);
            var model = response.Content.ReadAsAsync<List<GastoPersonalLiquidacionErp>>();
            if (model.Result.Count > 0)
            {
                lgastos = model.Result.ToList();
            }
            return lgastos;
        }
        public FileStreamResult GetPDF(string codigo)
        {
            FileStream fs = new FileStream(@"C:\reportesotros\" + codigo, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }

        public async Task<ActionResult> PagoBloqueCuentaPagarProveedor(List<List<string>> arrCuentas)
        {
            ViewBag.Cantidad = arrCuentas.Count;
            List<CuentaPagarProveedorErp> lcuentas = new List<CuentaPagarProveedorErp>();
            ViewBag.DeudaSoles = 0;
            ViewBag.DeudaDolar = 0;
            ViewBag.DeudaEuros = 0;
            List<int> idcuentas = new List<int>();
            List<int> idcuentasfound = new List<int>();
            try
            {
                foreach (var item in arrCuentas)
                {
                    int id = Convert.ToInt32(item[0].ToString());
                    idcuentas.Add(id);
                }
                var todos = await GetCuentasPagarProveedor();
                if (todos.Any())
                {
                    int[] array = new int[idcuentas.Count];
                    idcuentas.CopyTo(array);
                    lcuentas = todos.Where((n) => (array.Contains(n.id_cuentas_por_pagar_proveedor))).ToList(); //from elements in todos where (array.Contains(elements.id_cuentas_por_pagar_proveedor)) select elements; 
                    if (lcuentas != null && lcuentas.Any())
                    {
                        foreach (var item in lcuentas)
                        {
                            idcuentasfound.Add(item.id_cuentas_por_pagar_proveedor);
                        }
                        ViewBag.DeudaSoles = lcuentas.Where(y => y.IDMONEDA.Equals("01")).Sum(x => x.saldo);
                        ViewBag.DeudaDolar = lcuentas.Where(y => y.IDMONEDA.Equals("02")).Sum(x => x.saldo);
                        ViewBag.DeudaEuros = lcuentas.Where(y => y.IDMONEDA.Equals("03")).Sum(x => x.saldo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            int[] arrayfound = new int[idcuentasfound.Count];
            idcuentasfound.CopyTo(arrayfound);
            ViewBag.CuentasForPay = idcuentasfound;
            return PartialView(lcuentas);
        }
        [HttpPost]
        public async Task<JsonResult> GuardaPagoProveedorBloque(string id_save, string nro_operacion, List<List<string>> cuentasporpagar)
        {
            int contador = 0;
            int total = cuentasporpagar.Count;
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            try
            {
                foreach (var item in cuentasporpagar)
                {
                    var datos = await GetCuentaPorPagarProveedorErpId(Convert.ToInt32(item[0].ToString()));
                    if (datos != null && datos.id_cuentas_por_pagar_proveedor > 0)
                    {
                        CuentaPorPagarProveedorPagoErp _entidadi = new CuentaPorPagarProveedorPagoErp();
                        _entidadi.id_cuentas_por_pagar_proveedor_pago = 0;
                        _entidadi.fk_cuentas_por_pagar_proveedor = Convert.ToInt32(datos.id_cuentas_por_pagar_proveedor);
                        _entidadi.f_pago = Convert.ToDateTime(DateTime.Now);
                        _entidadi.nro_operacion = nro_operacion;
                        _entidadi.monto = Convert.ToDecimal(datos.saldo);
                        _entidadi.IDUSUARIO = FkUsua;
                        _entidadi.tipo = "2";
                        _entidadi.estado = "1";

                        string idinsertedi = "0";
                        var client = new HttpClient();
                        string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                        client.BaseAddress = new Uri(connectionInfo);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string metodo = "";
                        metodo = "Cuentaserp/t_cuentas_por_pagar_pago_proveedorInsert";
                        var response = await client.PostAsJsonAsync(metodo, _entidadi);
                        var respuesta = response.Content.ReadAsAsync<string>();
                        if (respuesta != null && respuesta.Result.ToString() != "0")
                        {
                            idinsertedi = respuesta.Result.ToString();
                            contador++;
                        }
                    }
                }

            }
            catch (Exception)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(contador, JsonRequestBehavior.AllowGet);
        }

        private async Task<CuentaPagarProveedorErp> GetCuentaPorPagarProveedorErpId(int id_cuentas_por_pagar_proveedor)
        {
            List<CuentaPagarProveedorErp> lcuentas = null;
            CuentaPagarProveedorErp cuenta = null;
            CuentaPagarProveedorErp busca = new CuentaPagarProveedorErp { id_cuentas_por_pagar_proveedor = id_cuentas_por_pagar_proveedor };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_proveedorSelectId", busca);
                var model = response.Content.ReadAsAsync<List<CuentaPagarProveedorErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                    cuenta = lcuentas[0];
                }
                else
                {
                    cuenta = new CuentaPagarProveedorErp();
                }
            }
            catch (Exception e)
            {
                cuenta = new CuentaPagarProveedorErp();
            }
            return cuenta;
        }


        [HttpPost]
        public async Task<JsonResult> GetFacturaExist(string doc, string fk_comprobantetipo, string fk_empresa)
        {
            string documento = doc.Trim();
            int fkcomprobante = Convert.ToInt32(fk_comprobantetipo);
            int fkempresa = Convert.ToInt32(fk_empresa);
            string finded = "0";
            var lentidad = await GetDocsCuentasPagarProveedor();
            if (lentidad != null && lentidad.Any())
            {
                var xy = lentidad.Where(y => y.documento.Equals(documento) && y.fk_comprobante_tipo == fkcomprobante && y.fk_proveedor == fkempresa).Select(x => x.id_cuentas_por_pagar_proveedor).FirstOrDefault();
                finded = xy.ToString();
                return Json(xy, JsonRequestBehavior.AllowGet);
            }
            return Json(finded, JsonRequestBehavior.AllowGet);
        }

        private async Task<List<CuentaPagarProveedorErp>> GetDocsCuentasPagarProveedor()
        {
            List<CuentaPagarProveedorErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_cuentas_por_pagar_proveedorSelectForDoc");
                var model = response.Content.ReadAsAsync<List<CuentaPagarProveedorErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<CuentaPagarProveedorErp>();
                }
            }
            catch (Exception e)
            {
                return new List<CuentaPagarProveedorErp>();
            }

            return lcuentas;
        }

        public async Task<ActionResult> LetraPagarProveedor()
        {
            List<LetrasPorPagarErp> lcuentas = new List<LetrasPorPagarErp>();
            ViewBag.DeudaSoles = 0;
            ViewBag.DeudaDolar = 0;
            ViewBag.DeudaEuros = 0;
            try
            {
                lcuentas = await GetLetrasPorPagarErp();
                if (lcuentas != null && lcuentas.Any())
                {
                    ViewBag.DeudaSoles = lcuentas.Where(y => y.IDMONEDA.Equals("01")).Sum(x => x.saldo);
                    ViewBag.DeudaDolar = lcuentas.Where(y => y.IDMONEDA.Equals("02")).Sum(x => x.saldo);
                    ViewBag.DeudaEuros = lcuentas.Where(y => y.IDMONEDA.Equals("03")).Sum(x => x.saldo);
                }
            }
            catch (Exception e)
            {
                lcuentas = new List<LetrasPorPagarErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lcuentas);
        }

        private async Task<List<LetrasPorPagarErp>> GetLetrasPorPagarErp()
        {
            List<LetrasPorPagarErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Letraserp/t_letras_por_pagar_proveedorSelectAll");
                var model = response.Content.ReadAsAsync<List<LetrasPorPagarErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<LetrasPorPagarErp>();
                }
            }
            catch (Exception e)
            {
                return new List<LetrasPorPagarErp>();
            }

            return lcuentas;
        }

        public async Task<ActionResult> RegistroLetrasPagarProveedor(int id_letras_por_pagar_proveedor)
        {
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            List<ComprobanteTipo> lstComprobanteTipo = new List<ComprobanteTipo>();
            lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
            lstComprobanteTipo.Add(new ComprobanteTipo { id_comprobante_tipo = 0, descripcion = "" });
            lstComprobanteTipo = lstComprobanteTipo.OrderBy(i => i.id_comprobante_tipo).ToList();
            ViewBag.ComprobanteTipo = lstComprobanteTipo;

            List<Empresa> _empresa = new List<Empresa>();
            Empresa newadd = new Empresa()
            {
                id_empresa = 0,
                ruc = "",
                razon_social = "",
                contacto = "",
                email = "",
                estado = "1",
                telefono = "",
                direccion = "",
                tipo = "P"
            };

            _empresa = await GetEmpresasErp();
            _empresa.Add(newadd);
            try
            {
                ViewBag.Empresas = _empresa.Where(y => y.estado.Equals("1") && y.tipo.Equals("P")).OrderBy(x => x.razon_social).ToList();
            }
            catch (Exception e)
            {
                ViewBag.Empresas = _empresa.OrderBy(x => x.id_empresa).ToList();
            }

            List<MonedaErp> _lmoneda = new List<MonedaErp>();
            MonedaErp newaddm = new MonedaErp()
            {
                IDMONEDA = "",
                NOMBRE_CORTO = "",
                DESCRIPCION = "",
                TIPO_MONEDA = "",
                FECHACREACION = DateTime.Now,
                IDREGISTRO_SUNAT = "",
                ESTADO = "1"
            };
            _lmoneda = await GetMonedaErp();
            _lmoneda.Add(newaddm);
            ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();
            LetrasPorPagarErp _letra = new LetrasPorPagarErp();
            if (id_letras_por_pagar_proveedor != 0)
            {
                try
                {
                    ViewBag.IdLetra = id_letras_por_pagar_proveedor;
                    var xentidad = await GetLetrasPorPagarErp();
                    if (xentidad != null && xentidad.Any())
                    {
                        _letra = xentidad.FirstOrDefault(x => x.id_letras_por_pagar_proveedor == id_letras_por_pagar_proveedor);
                    }
                }
                catch (Exception ex)
                {
                    id_letras_por_pagar_proveedor = 0;
                    ViewBag.IdLetra = id_letras_por_pagar_proveedor;
                }
            }
            else
            {
                id_letras_por_pagar_proveedor = 0;
                ViewBag.IdLetra = id_letras_por_pagar_proveedor;
            }

            ViewBag.Letra = _letra;
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> GuardaLetraPagarProveedor(string id_save, int fk_empresa,
            int fk_comprobantetipo, string documento, string IDMONEDA, decimal monto,
            int nro_letras, int dias, string f_emision, string observacion, List<List<string>> arrLetras)
        {
            string f_vencimiento = f_emision;
            foreach (var oneLetrasArr in arrLetras)
            {
                if (Convert.ToInt32(oneLetrasArr[0]) == nro_letras)
                {
                    f_vencimiento = oneLetrasArr[3];
                }
            }

            string FkUsua = Session["IdUsuario"].ToString().Trim();
            LetrasPorPagarErp _entidad = new LetrasPorPagarErp();
            if (id_save == "0")
            {
                _entidad.id_letras_por_pagar_proveedor = 0;
            }
            else
            {
                _entidad.id_letras_por_pagar_proveedor = Convert.ToInt32(id_save);
            }
            _entidad.fk_proveedor = Convert.ToInt32(fk_empresa);
            _entidad.fk_comprobante_tipo = Convert.ToInt32(fk_comprobantetipo);
            _entidad.documento_referencia = documento;
            _entidad.IDMONEDA = IDMONEDA;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.saldo = Convert.ToDecimal(monto);
            _entidad.f_emision = Convert.ToDateTime(f_emision);
            _entidad.f_vencimiento = Convert.ToDateTime(f_vencimiento);
            _entidad.nro_letras = nro_letras;
            _entidad.dias = dias;
            _entidad.observacion = observacion;
            _entidad.IDUSUARIO = FkUsua;
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
                if (_entidad.id_letras_por_pagar_proveedor == 0)
                {
                    metodo = "Letraserp/t_letras_por_pagar_proveedorInsert";
                }
                else
                {
                    metodo = "Letraserp/t_letras_por_pagar_proveedorUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                    if (Convert.ToInt32(idinserted) > 0)
                    {
                        foreach (var oneLetrasArr in arrLetras)
                        {
                            int idinserted2 = 0;

                            try
                            {
                                LetrasPorPagarErp _dentidad = new LetrasPorPagarErp()
                                {
                                    fk_letras_por_pagar_proveedor = Convert.ToInt32(idinserted),
                                    nro_letra = Convert.ToInt32(oneLetrasArr[0]),
                                    letra_numero = oneLetrasArr[1].ToUpper(),
                                    monto = Convert.ToDecimal(oneLetrasArr[2]),
                                    fecha = Convert.ToDateTime(oneLetrasArr[3])
                                };
                                var client2 = new HttpClient();
                                string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                                client2.BaseAddress = new Uri(connectionInfo2);
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                string metodo2 = "";
                                metodo2 = "Letraserp/t_letras_por_pagar_proveedor_detalleInsert";
                                var response2 = await client2.PostAsJsonAsync(metodo2, _dentidad);
                                var respuesta2 = response2.Content.ReadAsAsync<string>();
                                if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                                {
                                    idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> LetrasCalendario()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        private async Task<List<LetrasPorPagarErp>> GetLetrasPorPagarDetalleErp()
        {
            List<LetrasPorPagarErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Letraserp/t_letras_por_pagar_proveedor_detalleSelectAll");
                var model = response.Content.ReadAsAsync<List<LetrasPorPagarErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<LetrasPorPagarErp>();
                }
            }
            catch (Exception e)
            {
                return new List<LetrasPorPagarErp>();
            }

            return lcuentas;
        }
        public async Task<JsonResult> GetEvents()
        {
            List<LetrasPorPagarErp> entidad = new List<LetrasPorPagarErp>();
            entidad = await GetLetrasPorPagarDetalleErp();
            return new JsonResult { Data = entidad, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public class NombreProveedores
        {
            public string id { get; set; }
            public string title { get; set; }
        }
        public async Task<JsonResult> GetProveedoresEvents()
        {
            var entidad = await GetLetrasPorPagarDetalleErp();
            List<LetrasPorPagarErp> lhabitacion = new List<LetrasPorPagarErp>();

            List<LetrasPorPagarErp> listObjects = (from obj in entidad
                                                   select obj).GroupBy(n => new { n.fk_proveedor })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();

            List<NombreProveedores> lnombres = new List<NombreProveedores>();
            foreach (var item in listObjects)
            {
                NombreProveedores nentidad = new NombreProveedores()
                {
                    id = item.fk_proveedor.ToString(),
                    title = item.razon_social
                };
                lnombres.Add(nentidad);
            }
            return new JsonResult { Data = lnombres, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<ActionResult> IndexDetalleLetra(string fk_letras_por_pagar_proveedor, string id_letras_por_pagar_proveedor_detalle)
        {
            List<LetrasPorPagarErp> xlentidad = new List<LetrasPorPagarErp>();
            int ifk_letras_por_pagar_proveedor = Convert.ToInt32(fk_letras_por_pagar_proveedor);
            int iid_letras_por_pagar_proveedor_detalle = Convert.ToInt32(id_letras_por_pagar_proveedor_detalle);

            LetrasPorPagarErp _letra = new LetrasPorPagarErp();
            if (ifk_letras_por_pagar_proveedor != 0)
            {
                try
                {
                    ViewBag.IdLetra = ifk_letras_por_pagar_proveedor;
                    var xentidad = await GetLetrasPorPagarFkAsync(ifk_letras_por_pagar_proveedor);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                    }
                }
                catch (Exception ex)
                {
                    ifk_letras_por_pagar_proveedor = 0;
                    ViewBag.IdLetra = ifk_letras_por_pagar_proveedor;
                }
            }
            else
            {
                ifk_letras_por_pagar_proveedor = 0;
                ViewBag.IdLetra = ifk_letras_por_pagar_proveedor;
            }

            ViewBag.IdLetraDetalle = iid_letras_por_pagar_proveedor_detalle;
            return PartialView(xlentidad);
        }

        private async Task<List<LetrasPorPagarErp>> GetLetrasPorPagarFkAsync(int fk_letras_por_pagar_proveedor)
        {
            List<LetrasPorPagarErp> lletras = null;
            LetrasPorPagarErp fkletra = new LetrasPorPagarErp { fk_letras_por_pagar_proveedor = fk_letras_por_pagar_proveedor };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Letraserp/t_letras_por_pagar_proveedor_detalleSelectFk", fkletra);
            var model = response.Content.ReadAsAsync<List<LetrasPorPagarErp>>();
            if (model.Result.Count > 0)
            {
                lletras = model.Result.ToList();
            }
            return lletras;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPagoLetra(string id_letras_por_pagar_proveedor_detalle, string fecha_pago)
        {
            LetrasPorPagarErp _entidad = new LetrasPorPagarErp();
            _entidad.id_letras_por_pagar_proveedor_detalle = Convert.ToInt32(id_letras_por_pagar_proveedor_detalle);
            _entidad.estado = "2";
            _entidad.fecha_pago = Convert.ToDateTime(fecha_pago);
            string idinserted = "0";
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "Letraserp/t_letras_por_pagar_proveedor_detalleUpdate";
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

        public async Task<ActionResult> CuotasCalendario()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        public async Task<JsonResult> GetEventsCuotas()
        {
            List<CuentaPorPagarErp> entidad = new List<CuentaPorPagarErp>();
            entidad = await GetCuotasPorPagarDetalleErp();
            return new JsonResult { Data = entidad, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private async Task<List<CuentaPorPagarErp>> GetCuotasPorPagarDetalleErp()
        {
            List<CuentaPorPagarErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Cuentaserp/t_cuentas_por_pagar_bancos_detalleSelectAll");
                var model = response.Content.ReadAsAsync<List<CuentaPorPagarErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<CuentaPorPagarErp>();
                }
            }
            catch (Exception e)
            {
                return new List<CuentaPorPagarErp>();
            }

            return lcuentas;
        }

        public async Task<JsonResult> GetEntidadesEvents()
        {
            var entidad = await GetCuotasPorPagarDetalleErp();
            List<CuentaPorPagarErp> lcuentas = new List<CuentaPorPagarErp>();

            List<CuentaPorPagarErp> listObjects = (from obj in entidad
                                                   select obj).GroupBy(n => new { n.fk_entidad })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();

            List<NombreProveedores> lnombres = new List<NombreProveedores>();
            foreach (var item in listObjects)
            {
                NombreProveedores nentidad = new NombreProveedores()
                {
                    id = item.fk_entidad.ToString(),
                    title = item.razon_social
                };
                lnombres.Add(nentidad);
            }
            return new JsonResult { Data = lnombres, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<ActionResult> IndexDetalleCuota(string fk_cuentas_por_pagar, string id_cuentas_por_pagar_bancos_detalle)
        {
            List<CuentaPorPagarErp> xlentidad = new List<CuentaPorPagarErp>();
            int ifk_cuentas_por_pagar = Convert.ToInt32(fk_cuentas_por_pagar);
            int iid_cuentas_por_pagar_bancos_detalle = Convert.ToInt32(id_cuentas_por_pagar_bancos_detalle);

            CuentaPorPagarErp _cuota = new CuentaPorPagarErp();
            if (ifk_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuota = ifk_cuentas_por_pagar;
                    var xentidad = await GetCuentaPorPagarFkAsync(ifk_cuentas_por_pagar);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                    }
                }
                catch (Exception ex)
                {
                    ifk_cuentas_por_pagar = 0;
                    ViewBag.IdCuota = ifk_cuentas_por_pagar;
                }
            }
            else
            {
                ifk_cuentas_por_pagar = 0;
                ViewBag.IdCuota = ifk_cuentas_por_pagar;
            }

            ViewBag.IdCuotaDetalle = iid_cuentas_por_pagar_bancos_detalle;
            return PartialView(xlentidad);
        }

        private async Task<List<CuentaPorPagarErp>> GetCuentaPorPagarFkAsync(int fk_cuentas_por_pagar)
        {
            List<CuentaPorPagarErp> lletras = null;
            CuentaPorPagarErp fkletra = new CuentaPorPagarErp { fk_cuentas_por_pagar = fk_cuentas_por_pagar };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_bancos_detalleSelectFk", fkletra);
            var model = response.Content.ReadAsAsync<List<CuentaPorPagarErp>>();
            if (model.Result.Count > 0)
            {
                lletras = model.Result.ToList();
            }
            return lletras;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPagoCuota(string id_cuentas_por_pagar_bancos_detalle, string fecha_pago)
        {
            CuentaPorPagarErp _entidad = new CuentaPorPagarErp();
            _entidad.id_cuentas_por_pagar_bancos_detalle = Convert.ToInt32(id_cuentas_por_pagar_bancos_detalle);
            _entidad.estado = "2";
            _entidad.fecha_pago = Convert.ToDateTime(fecha_pago);
            string idinserted = "0";
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "Cuentaserp/t_cuentas_por_pagar_bancos_detalleUpdate";
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

        public async Task<ActionResult> IndexDetallesCuota(string id_cuentas_por_pagar)
        {
            List<CuentaPorPagarErp> xlentidad = new List<CuentaPorPagarErp>();
            int ifk_cuentas_por_pagar = Convert.ToInt32(id_cuentas_por_pagar);

            CuentaPorPagarErp _cuota = new CuentaPorPagarErp();
            if (ifk_cuentas_por_pagar != 0)
            {
                try
                {
                    ViewBag.IdCuota = ifk_cuentas_por_pagar;
                    var xentidad = await GetCuentaPorPagarIdAsync(ifk_cuentas_por_pagar);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                    }
                }
                catch (Exception ex)
                {
                    ifk_cuentas_por_pagar = 0;
                    ViewBag.IdCuota = ifk_cuentas_por_pagar;
                }
            }
            else
            {
                ifk_cuentas_por_pagar = 0;
                ViewBag.IdCuota = ifk_cuentas_por_pagar;
            }

            return PartialView(xlentidad);
        }
        private async Task<List<CuentaPorPagarErp>> GetCuentaPorPagarIdAsync(int fk_cuentas_por_pagar)
        {
            List<CuentaPorPagarErp> lletras = null;
            CuentaPorPagarErp fkletra = new CuentaPorPagarErp { id_cuentas_por_pagar = fk_cuentas_por_pagar };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Cuentaserp/t_cuentas_por_pagar_bancos_detalleSelectId", fkletra);
            var model = response.Content.ReadAsAsync<List<CuentaPorPagarErp>>();
            if (model.Result.Count > 0)
            {
                lletras = model.Result.ToList();
            }
            return lletras;
        }

        public async Task<ActionResult> IndexDetallesLetra(string id_letras_por_pagar_proveedor)
        {
            List<LetrasPorPagarErp> xlentidad = new List<LetrasPorPagarErp>();
            int ifk_letras_por_pagar_proveedor = Convert.ToInt32(id_letras_por_pagar_proveedor);

            LetrasPorPagarErp _letra = new LetrasPorPagarErp();
            if (ifk_letras_por_pagar_proveedor != 0)
            {
                try
                {
                    ViewBag.IdLetra = ifk_letras_por_pagar_proveedor;
                    var xentidad = await GetLetrasPorPagarIdAsync(ifk_letras_por_pagar_proveedor);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                    }
                }
                catch (Exception ex)
                {
                    ifk_letras_por_pagar_proveedor = 0;
                    ViewBag.IdLetra = ifk_letras_por_pagar_proveedor;
                }
            }
            else
            {
                ifk_letras_por_pagar_proveedor = 0;
                ViewBag.IdLetra = ifk_letras_por_pagar_proveedor;
            }
            return PartialView(xlentidad);
        }
        public async Task<ActionResult> IndexDetallesObligacion(string id_obligacion_pagar)
        {
            List<ObligacionPagarErp> xlentidad = new List<ObligacionPagarErp>();
            int iid_obligacion_pagar = Convert.ToInt32(id_obligacion_pagar);

            ObligacionPagarErp _letra = new ObligacionPagarErp();
            if (iid_obligacion_pagar != 0)
            {
                try
                {
                    ViewBag.IdObligacion = iid_obligacion_pagar;
                    var xentidad = await GetObligacionPagarErpById(iid_obligacion_pagar);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                    }
                }
                catch (Exception ex)
                {
                    iid_obligacion_pagar = 0;
                    ViewBag.IdObligacion = iid_obligacion_pagar;
                }
            }
            else
            {
                iid_obligacion_pagar = 0;
                ViewBag.IdObligacion = iid_obligacion_pagar;
            }
            return PartialView(xlentidad);
        }
        private async Task<List<LetrasPorPagarErp>> GetLetrasPorPagarIdAsync(int id_letras_por_pagar_proveedor)
        {
            List<LetrasPorPagarErp> lletras = null;
            LetrasPorPagarErp fkletra = new LetrasPorPagarErp { id_letras_por_pagar_proveedor = id_letras_por_pagar_proveedor };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Letraserp/t_letras_por_pagar_proveedor_detalleSelectId", fkletra);
            var model = response.Content.ReadAsAsync<List<LetrasPorPagarErp>>();
            if (model.Result.Count > 0)
            {
                lletras = model.Result.ToList();
            }
            return lletras;
        }

        public async Task<ActionResult> ExcelExportLiquidacion(string id_gasto_personal)
        {
            string codigo = "";
            int iid_gasto_personal = Convert.ToInt32(id_gasto_personal);
            List<GastoPersonalLiquidacionErp> lgastoliq = new List<GastoPersonalLiquidacionErp>();
            List<GastoPersonalErp> lgastodetalle = new List<GastoPersonalErp>();
            try
            {
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();

                var data = await GetGastoRpt(iid_gasto_personal);
                lgastoliq = await GetGastoPersonalLiquidacionErpFk(iid_gasto_personal);
                lgastodetalle = await GetGastoPersonalDetalleErpFk(iid_gasto_personal);
                if (data.Any())
                {
                    codigo = data[0].codigo_full;
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        //string path = Server.MapPath(logoreport);
                        string path = Path.Combine(Server.MapPath(logoreport));
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;
                        ReportDocument rd = new ReportDocument();
                        //rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/ReporteLiquidacionErp.rpt")));
                        //rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/ReporteLiquidacionErp2.rpt")));
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/ReportLiquidacionErp.rpt")));

                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetParameterValue("@id_gasto_personal", iid_gasto_personal);

                        //rd.SetDataSource(data);
                        ////rd.Subreports[0].DataSourceConnections.Clear();
                        //rd.Subreports[1].SetDataSource(lgastoliq);
                        //rd.Subreports[0].SetDataSource(lgastodetalle);

                        for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                        {
                            if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                            {
                                rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                            }
                        }
                        Directory.CreateDirectory(@"C:\reportesotros");

                        // Declare variables and get the export options.
                        ExportOptions exportOpts = new ExportOptions();
                        ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
                        DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                        exportOpts = rd.ExportOptions;

                        // Set the excel format options.
                        excelFormatOpts.ExcelUseConstantColumnWidth = true;
                        excelFormatOpts.ExcelTabHasColumnHeadings = true;
                        exportOpts.ExportFormatType = ExportFormatType.Excel;
                        exportOpts.FormatOptions = excelFormatOpts;

                        // Set the disk file options and export.
                        exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                        diskOpts.DiskFileName = @"C:\reportesotros\Liquidacion-" + codigo + ".xls";
                        exportOpts.DestinationOptions = diskOpts;

                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        //rd.Export();
                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                        stream.Seek(0, SeekOrigin.Begin);
                        return File(stream, "application/xls", @"C:\reportesotros\Liquidacion-" + codigo + ".xls");

                    }

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
        }

        #region Obligaciones
        public async Task<ActionResult> ObligacionesPagar()
        {
            List<ObligacionPagarErp> lobligaciones = new List<ObligacionPagarErp>();
            ViewBag.DeudaSoles = 0;
            ViewBag.DeudaDolar = 0;
            ViewBag.DeudaEuros = 0;
            try
            {
                lobligaciones = await GetObligacionPagarErp();
                if (lobligaciones != null && lobligaciones.Any())
                {
                    ViewBag.DeudaSoles = lobligaciones.Where(y => y.IDMONEDA.Equals("01") && y.estado.Equals("1")).Sum(x => x.saldo);
                    ViewBag.DeudaDolar = lobligaciones.Where(y => y.IDMONEDA.Equals("02") && y.estado.Equals("1")).Sum(x => x.saldo);
                    ViewBag.DeudaEuros = lobligaciones.Where(y => y.IDMONEDA.Equals("03") && y.estado.Equals("1")).Sum(x => x.saldo);
                }
            }
            catch (Exception e)
            {
                lobligaciones = new List<ObligacionPagarErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lobligaciones);
        }

        private async Task<List<ObligacionPagarErp>> GetObligacionPagarErpById(int idobligacion)
        {
            List<ObligacionPagarErp> lempresa = null;
            ObligacionPagarErp idempres = new ObligacionPagarErp { id_obligacion_pagar = idobligacion };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Obligacioneserp/t_obligacion_pagar_detalleSelectId", idempres);
            var model = response.Content.ReadAsAsync<List<ObligacionPagarErp>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.ToList();
            }
            return lempresa;
        }
        private async Task<List<ObligacionPagarErp>> GetObligacionPagarErp()
        {
            List<ObligacionPagarErp> lentidad = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Obligacioneserp/t_obligacion_pagarSelectAll");
                var model = response.Content.ReadAsAsync<List<ObligacionPagarErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<ObligacionPagarErp>();
                }
            }
            catch (Exception e)
            {
                return new List<ObligacionPagarErp>();
            }

            return lentidad;
        }

        private async Task<List<ObligacionTipoErp>> GetObligacionTipoErp()
        {
            List<ObligacionTipoErp> lentidad = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Obligacioneserp/t_obligacion_tipoSelectAll");
                var model = response.Content.ReadAsAsync<List<ObligacionTipoErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<ObligacionTipoErp>();
                }
            }
            catch (Exception e)
            {
                return new List<ObligacionTipoErp>();
            }

            return lentidad;
        }

        public async Task<ActionResult> RegistroObligacionPagarErp(int id_obligacion_pagar)
        {
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            List<ObligacionTipoErp> lstObligacionTipo = new List<ObligacionTipoErp>();
            List<ComprobanteTipo> lstComprobanteTipo = new List<ComprobanteTipo>();
            lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
            lstComprobanteTipo.Add(new ComprobanteTipo { id_comprobante_tipo = 0, descripcion = "" });
            lstComprobanteTipo = lstComprobanteTipo.OrderBy(i => i.id_comprobante_tipo).ToList();
            ViewBag.ComprobanteTipo = lstComprobanteTipo;

            List<ObligacionTipoErp> _oblitipos = new List<ObligacionTipoErp>();
            ObligacionTipoErp newtipo = new ObligacionTipoErp()
            {
                id_obligacion_tipo = 0,
                descripcion = "",
                tipo = "",
                estado = "1"
            };

            _oblitipos = await GetObligacionTipoErp();
            _oblitipos.Add(newtipo);
            try
            {
                ViewBag.ObligacionTipo = _oblitipos.Where(y => y.estado.Equals("1")).OrderBy(x => x.descripcion).ToList();
            }
            catch (Exception e)
            {
                ViewBag.ObligacionTipo = _oblitipos.OrderBy(x => x.id_obligacion_tipo).ToList();
            }

            List<Empresa> _empresa = new List<Empresa>();
            Empresa newadd = new Empresa()
            {
                id_empresa = 0,
                ruc = "",
                razon_social = "",
                contacto = "",
                email = "",
                estado = "1",
                telefono = "",
                direccion = "",
                tipo = "P"
            };

            _empresa = await GetEmpresasErp();
            _empresa.Add(newadd);
            try
            {
                ViewBag.Empresas = _empresa.Where(y => y.estado.Equals("1") && y.tipo.Equals("P")).OrderBy(x => x.razon_social).ToList();
            }
            catch (Exception e)
            {
                ViewBag.Empresas = _empresa.OrderBy(x => x.id_empresa).ToList();
            }

            List<TipoCheckErp> _ltipos = new List<TipoCheckErp>();
            TipoCheckErp tiponew = new TipoCheckErp()
            {
                id_tipo_check = 0,
                descripcion = ""
            };
            _ltipos = await GetTipoCheckErp();
            _ltipos.Add(tiponew);
            ViewBag.Tipos = _ltipos.OrderBy(x => x.id_tipo_check).ToList();


            List<MonedaErp> _lmoneda = new List<MonedaErp>();
            MonedaErp newaddm = new MonedaErp()
            {
                IDMONEDA = "",
                NOMBRE_CORTO = "",
                DESCRIPCION = "",
                TIPO_MONEDA = "",
                FECHACREACION = DateTime.Now,
                IDREGISTRO_SUNAT = "",
                ESTADO = "1"
            };
            _lmoneda = await GetMonedaErp();
            _lmoneda.Add(newaddm);
            ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();

            List<ObligacionPagarErp> _obligacion = new List<ObligacionPagarErp>();
            if (id_obligacion_pagar != 0)
            {
                try
                {
                    ViewBag.IdObliga = id_obligacion_pagar;
                    var xentidad = await GetObligacionPagarErp();
                    if (xentidad != null && xentidad.Any())
                    {
                        _obligacion = xentidad.Where(x => x.id_obligacion_pagar == id_obligacion_pagar).ToList();
                    }
                }
                catch (Exception ex)
                {
                    id_obligacion_pagar = 0;
                    ViewBag.IdObliga = id_obligacion_pagar;
                }
            }
            else
            {
                id_obligacion_pagar = 0;
                ViewBag.IdObliga = id_obligacion_pagar;
            }

            ViewBag.Obliga = _obligacion;
            return PartialView();
        }
        #endregion
        [HttpPost]
        public async Task<JsonResult> GetObligacionPagarErpId(string id_obligacion_pagar)
        {
            int iid_obligacion_pagar = 0;
            List<ObligacionPagarErp> _obligacion = new List<ObligacionPagarErp>();
            if (id_obligacion_pagar != "")
            {
                iid_obligacion_pagar = Convert.ToInt32(id_obligacion_pagar);
            }
            try
            {
                var xentidad = await GetObligacionPagarErpFkAsync(iid_obligacion_pagar);
                if (xentidad != null && xentidad.Any())
                {
                    _obligacion = xentidad.ToList();
                }
            }
            catch (Exception)
            {

            }
            return Json(_obligacion, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetTipoObligacion(string fk_obligacion_tipo)
        {
            string sfk_obligacion_tipo = fk_obligacion_tipo.Trim();
            int fk_obligaciontipo = Convert.ToInt32(fk_obligacion_tipo);

            string finded = "0";
            var lentidad = await GetObligacionTipoErp();
            if (lentidad != null && lentidad.Any())
            {
                var xy = lentidad.Where(y => y.id_obligacion_tipo == fk_obligaciontipo).Select(x => x.tipo).FirstOrDefault();
                finded = xy.ToString();
                return Json(xy, JsonRequestBehavior.AllowGet);
            }
            return Json(finded, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GuardaObligacionPagar(string id_save, int fk_empresa, string fk_obligacion_tipo,
            int fk_comprobantetipo, string documento, string IDMONEDA, decimal monto,
            int nro_letras, string interes, int dias, string f_emision, string montocuotainicial, string montocuota, string observacion, List<List<string>> arrLetras)
        {
            string f_vencimiento = f_emision;
            if (arrLetras != null)
            {
                foreach (var oneLetrasArr in arrLetras)
                {
                    if (Convert.ToInt32(oneLetrasArr[0]) == nro_letras)
                    {
                        f_vencimiento = oneLetrasArr[3];
                    }
                }
            }


            string FkUsua = Session["IdUsuario"].ToString().Trim();
            ObligacionPagarErp _entidad = new ObligacionPagarErp();
            if (id_save == "0")
            {
                _entidad.id_obligacion_pagar = 0;
            }
            else
            {
                _entidad.id_obligacion_pagar = Convert.ToInt32(id_save);
            }
            _entidad.fk_proveedor = Convert.ToInt32(fk_empresa);
            _entidad.fk_obligacion_tipo = Convert.ToInt32(fk_obligacion_tipo);
            _entidad.fk_comprobante_tipo = Convert.ToInt32(fk_comprobantetipo);
            _entidad.documento_referencia = documento;
            _entidad.IDMONEDA = IDMONEDA;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.saldo = Convert.ToDecimal(monto);
            _entidad.fecha_emision = Convert.ToDateTime(f_emision);
            _entidad.fecha_vencimiento = Convert.ToDateTime(f_vencimiento);
            _entidad.nro_letras = nro_letras;
            _entidad.dias = dias;
            _entidad.interes = Convert.ToDecimal(interes);
            _entidad.cuota_inicial = Convert.ToDecimal(montocuotainicial);
            _entidad.interes = Convert.ToDecimal(interes);
            _entidad.observacion = observacion;
            _entidad.IDUSUARIO = FkUsua;
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
                if (_entidad.id_obligacion_pagar == 0)
                {
                    metodo = "Obligacioneserp/t_obligacion_pagarInsert";
                    var response = await client.PostAsJsonAsync(metodo, _entidad);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = respuesta.Result.ToString();
                        if (Convert.ToInt32(idinserted) > 0)
                        {
                            if (arrLetras != null)
                            {

                                foreach (var oneLetrasArr in arrLetras)
                                {
                                    int idinserted2 = 0;

                                    try
                                    {
                                        ObligacionPagarErp _dentidad = new ObligacionPagarErp()
                                        {
                                            fk_obligacion_pagar = Convert.ToInt32(idinserted),
                                            nro_letra = Convert.ToInt32(oneLetrasArr[0]),
                                            letra_numero = oneLetrasArr[1].ToUpper(),
                                            monto = Convert.ToDecimal(oneLetrasArr[2]),
                                            fecha = Convert.ToDateTime(oneLetrasArr[3])
                                        };
                                        var client2 = new HttpClient();
                                        string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                                        client2.BaseAddress = new Uri(connectionInfo2);
                                        client2.DefaultRequestHeaders.Accept.Clear();
                                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        string metodo2 = "";
                                        metodo2 = "Obligacioneserp/t_obligacion_pagar_detalleInsert";
                                        var response2 = await client2.PostAsJsonAsync(metodo2, _dentidad);
                                        var respuesta2 = response2.Content.ReadAsAsync<string>();
                                        if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                                        {
                                            idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                            else
                            {
                                int idinserted2 = 0;
                                try
                                {
                                    ObligacionPagarErp _dentidad = new ObligacionPagarErp()
                                    {
                                        fk_obligacion_pagar = Convert.ToInt32(idinserted),
                                        nro_letra = 1,
                                        letra_numero = "",
                                        monto = Convert.ToDecimal(monto),
                                        fecha = Convert.ToDateTime(f_emision)
                                    };
                                    var client2 = new HttpClient();
                                    string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                                    client2.BaseAddress = new Uri(connectionInfo2);
                                    client2.DefaultRequestHeaders.Accept.Clear();
                                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    string metodo2 = "";
                                    metodo2 = "Obligacioneserp/t_obligacion_pagar_detalleInsert";
                                    var response2 = await client2.PostAsJsonAsync(metodo2, _dentidad);
                                    var respuesta2 = response2.Content.ReadAsAsync<string>();
                                    if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                                    {
                                        idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }
                else
                {
                    string metodo2 = "Obligacioneserp/t_obligacion_pagar_detalleUpdateDetail";
                    if (arrLetras != null)
                    {

                        foreach (var oneLetrasArr in arrLetras)
                        {
                            int idinserted2 = 0;
                            idinserted = id_save;
                            try
                            {
                                ObligacionPagarErp _dentidad = new ObligacionPagarErp()
                                {
                                    id_obligacion_pagar_detalle = Convert.ToInt32(oneLetrasArr[4]),
                                    fk_obligacion_pagar = Convert.ToInt32(idinserted),
                                    nro_letra = Convert.ToInt32(oneLetrasArr[0]),
                                    letra_numero = oneLetrasArr[1].ToUpper(),
                                    monto = Convert.ToDecimal(oneLetrasArr[2]),
                                    fecha = Convert.ToDateTime(oneLetrasArr[3])
                                };
                                var client2 = new HttpClient();
                                string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                                client2.BaseAddress = new Uri(connectionInfo2);
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var response2 = await client2.PostAsJsonAsync(metodo2, _dentidad);
                                var respuesta2 = response2.Content.ReadAsAsync<string>();
                                if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                                {
                                    idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ObligacionesCalendario()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        public async Task<ActionResult> ReportesMensuales()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GuardaPagoObligacion(string id_obligacion_pagar_detalle, string fecha_pago)
        {
            ObligacionPagarErp _entidad = new ObligacionPagarErp();
            _entidad.id_obligacion_pagar_detalle = Convert.ToInt32(id_obligacion_pagar_detalle);
            _entidad.estado = "2";
            _entidad.fecha_pago = Convert.ToDateTime(fecha_pago);
            string idinserted = "0";
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "Obligacioneserp/t_obligacion_pagar_detalleUpdate";
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

        public async Task<JsonResult> GetObligaciones()
        {
            List<ObligacionPagarErp> entidad = new List<ObligacionPagarErp>();
            entidad = await GetObligacionPagarDetallesErp();
            return new JsonResult { Data = entidad, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private async Task<List<ObligacionPagarErp>> GetObligacionPagarDetallesErp()
        {
            List<ObligacionPagarErp> lcuentas = null;
            try
            {

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Obligacioneserp/t_obligacion_pagar_detalleSelectAll");
                var model = response.Content.ReadAsAsync<List<ObligacionPagarErp>>();
                if (model.Result.Count > 0)
                {
                    lcuentas = model.Result.ToList();
                }
                else
                {
                    lcuentas = new List<ObligacionPagarErp>();
                }
            }
            catch (Exception e)
            {
                return new List<ObligacionPagarErp>();
            }

            return lcuentas;
        }

        public async Task<JsonResult> GetProveedoresObligaciones()
        {
            var entidad = await GetObligacionPagarDetallesErp();
            List<ObligacionPagarErp> lhabitacion = new List<ObligacionPagarErp>();

            List<ObligacionPagarErp> listObjects = (from obj in entidad
                                                    select obj).GroupBy(n => new { n.fk_proveedor })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();

            List<NombreProveedores> lnombres = new List<NombreProveedores>();
            foreach (var item in listObjects)
            {
                NombreProveedores nentidad = new NombreProveedores()
                {
                    id = item.fk_proveedor.ToString(),
                    title = item.razon_social
                };
                lnombres.Add(nentidad);
            }
            return new JsonResult { Data = lnombres, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<ActionResult> IndexDetalleObligacion(string fk_obligacion_pagar, string id_obligacion_pagar_detalle)
        {
            List<ObligacionPagarErp> xlentidad = new List<ObligacionPagarErp>();
            int ifk_obligacion_pagar = Convert.ToInt32(fk_obligacion_pagar);
            int iid_obligacion_pagar_detalle = Convert.ToInt32(id_obligacion_pagar_detalle);

            ObligacionPagarErp _letra = new ObligacionPagarErp();
            if (ifk_obligacion_pagar != 0)
            {
                try
                {
                    ViewBag.IdObligacion = ifk_obligacion_pagar;
                    var xentidad = await GetObligacionPagarErpFkAsync(ifk_obligacion_pagar);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                    }
                }
                catch (Exception ex)
                {
                    ifk_obligacion_pagar = 0;
                    ViewBag.IdObligacion = ifk_obligacion_pagar;
                }
            }
            else
            {
                ifk_obligacion_pagar = 0;
                ViewBag.IdObligacion = ifk_obligacion_pagar;
            }

            ViewBag.IdObligacionDetalle = iid_obligacion_pagar_detalle;
            return PartialView(xlentidad);
        }
        private async Task<List<ObligacionPagarErp>> GetObligacionPagarErpFkAsync(int ifk_obligacion_pagar)
        {
            List<ObligacionPagarErp> lletras = null;
            ObligacionPagarErp fkletra = new ObligacionPagarErp { fk_obligacion_pagar = ifk_obligacion_pagar };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Obligacioneserp/t_obligacion_pagar_detalleSelectFk", fkletra);
            var model = response.Content.ReadAsAsync<List<ObligacionPagarErp>>();
            if (model.Result.Count > 0)
            {
                lletras = model.Result.ToList();
            }
            return lletras;
        }

        public async Task<JsonResult> GetPrimerDiaHabilErp(string cuotas, string diab, string opcion)
        {
            string metodo = "";
            switch (opcion)
            {
                case "primer":
                    metodo = "Obligacioneserp/t_primer_dia_habilSelectCuotas";
                    break;
                case "quince":
                    metodo = "Obligacioneserp/t_quince_dia_habilSelectCuotas";
                    break;
                case "fin":
                    metodo = "Obligacioneserp/t_fin_dia_habilSelectCuotas";
                    break;
                case "quincefin":
                    metodo = "Obligacioneserp/t_quince_fin_dia_habilSelectCuotas";
                    break;
            }
            var entidad = await GetPrimerDiaHabilErpCuota(cuotas, diab, metodo);
            List<PrimerDiaHabilErp> lprimer = new List<PrimerDiaHabilErp>();
            List<string> dias = new List<string>();
            foreach (var item in entidad)
            {
                dias.Add(item.df_dia);
            }
            return new JsonResult { Data = dias, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<JsonResult> GetPrimerDiaHabilErpAgre(string cuotas, string diab, string opcion)
        {
            string metodo = "";
            switch (opcion)
            {
                case "primer":
                    metodo = "Obligacioneserp/t_primer_dia_habilSelectCuotasAgre";
                    break;
                case "quince":
                    metodo = "Obligacioneserp/t_quince_dia_habilSelectCuotasAgre";
                    break;
                case "fin":
                    metodo = "Obligacioneserp/t_fin_dia_habilSelectCuotasAgre";
                    break;
            }
            var entidad = await GetPrimerDiaHabilErpCuota(cuotas, diab, metodo);
            List<PrimerDiaHabilErp> lprimer = new List<PrimerDiaHabilErp>();
            List<string> dias = new List<string>();
            foreach (var item in entidad)
            {
                dias.Add(item.df_dia);
            }
            return new JsonResult { Data = dias, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private async Task<List<PrimerDiaHabilErp>> GetPrimerDiaHabilErpCuota(string cuotas, string diab, string metodo)
        {
            int icuotas = Convert.ToInt32(cuotas);
            DateTime ddiab = Convert.ToDateTime(diab);
            List<PrimerDiaHabilErp> lentidad = null;
            PrimerDiaHabilErp fkletra = new PrimerDiaHabilErp { cuotas = icuotas, diab = ddiab };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(metodo, fkletra);
            var model = response.Content.ReadAsAsync<List<PrimerDiaHabilErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }

        public async Task<ActionResult> PrintPresupuestoView(string cmb_anio, string cmb_mes)
        {
            int icmb_anio = Convert.ToInt32(cmb_anio);
            int icmb_mes = Convert.ToInt32(cmb_mes);



            List<ObligacionPagarErp> lobligacion = new List<ObligacionPagarErp>();
            try
            {

                var data = await GetObligacionPagarErpReport(icmb_anio, icmb_mes);
                if (data.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_obligacion_pagar", typeof(int));
                    dt.Columns.Add("fk_obligacion_tipo", typeof(int));
                    dt.Columns.Add("fk_comprobante_tipo", typeof(int));
                    dt.Columns.Add("fk_proveedor", typeof(int));
                    dt.Columns.Add("IDMONEDA", typeof(string));
                    dt.Columns.Add("IDUSUARIO", typeof(string));
                    dt.Columns.Add("documento_referencia", typeof(string));
                    dt.Columns.Add("fecha_emision", typeof(DateTime));
                    dt.Columns.Add("monto", typeof(decimal));
                    dt.Columns.Add("nro_letras", typeof(int));
                    dt.Columns.Add("interes", typeof(decimal));
                    dt.Columns.Add("cuota_inicial", typeof(decimal));
                    dt.Columns.Add("saldo", typeof(decimal));
                    dt.Columns.Add("fecha_registro", typeof(DateTime));
                    dt.Columns.Add("estado", typeof(string));
                    dt.Columns.Add("observacion", typeof(string));
                    dt.Columns.Add("fecha_vencimiento", typeof(DateTime));
                    dt.Columns.Add("dias", typeof(int));
                    dt.Columns.Add("comprobante_tipo", typeof(string));
                    dt.Columns.Add("ruc", typeof(string));
                    dt.Columns.Add("razon_social", typeof(string));
                    dt.Columns.Add("moneda", typeof(string));
                    dt.Columns.Add("NOMBRE_CORTO", typeof(string));
                    dt.Columns.Add("id_obligacion_pagar_detalle", typeof(int));
                    dt.Columns.Add("fk_obligacion_pagar", typeof(int));
                    dt.Columns.Add("nro_letra", typeof(int));
                    dt.Columns.Add("letra_numero", typeof(string));
                    dt.Columns.Add("monto_detalle", typeof(decimal));
                    dt.Columns.Add("fecha", typeof(DateTime));
                    dt.Columns.Add("estado_detalle", typeof(string));
                    dt.Columns.Add("fecha_pago", typeof(DateTime));
                    dt.Columns.Add("obligacion_tipo", typeof(string));
                    dt.Columns.Add("mes_vence", typeof(string));
                    dt.Columns.Add("num_cuota", typeof(string));
                    dt.Columns.Add("monto_soles", typeof(decimal));
                    dt.Columns.Add("monto_dolares", typeof(decimal));
                    dt.Columns.Add("num_week", typeof(int));
                    dt.Columns.Add("detalle", typeof(string));
                    dt.Columns.Add("NombreEmpresa", typeof(string));
                    dt.Columns.Add("RucEmpresa", typeof(string));
                    dt.Columns.Add("DireccionEmpresa", typeof(string));

                    foreach (var item in data)
                    {
                        DataRow row = dt.NewRow();
                        row["id_obligacion_pagar"] = Convert.ToInt32(item.id_obligacion_pagar);
                        row["fk_obligacion_tipo"] = Convert.ToInt32(item.fk_obligacion_tipo);
                        row["fk_comprobante_tipo"] = Convert.ToInt32(item.fk_comprobante_tipo);
                        row["fk_proveedor"] = Convert.ToInt32(item.fk_proveedor);
                        row["IDMONEDA"] = item.IDMONEDA;
                        row["IDUSUARIO"] = item.IDUSUARIO;
                        row["documento_referencia"] = item.documento_referencia;
                        row["fecha_emision"] = Convert.ToDateTime(item.fecha_emision);
                        row["monto"] = Convert.ToDecimal(item.monto);
                        row["nro_letras"] = Convert.ToInt32(item.nro_letras);
                        row["interes"] = Convert.ToDecimal(item.interes);
                        row["cuota_inicial"] = item.cuota_inicial;
                        row["saldo"] = Convert.ToDecimal(item.saldo);
                        row["fecha_registro"] = Convert.ToDateTime(item.fecha_registro);
                        row["estado"] = item.estado;
                        row["observacion"] = item.observacion;
                        row["fecha_vencimiento"] = Convert.ToDateTime(item.fecha_vencimiento);
                        row["dias"] = Convert.ToInt32(item.dias);
                        row["comprobante_tipo"] = item.comprobante_tipo;
                        row["ruc"] = item.ruc;
                        row["razon_social"] = item.razon_social;
                        row["moneda"] = item.moneda;
                        row["NOMBRE_CORTO"] = item.NOMBRE_CORTO;
                        row["id_obligacion_pagar_detalle"] = Convert.ToInt32(item.id_obligacion_pagar_detalle);
                        row["fk_obligacion_pagar"] = Convert.ToInt32(item.fk_obligacion_pagar);
                        row["nro_letra"] = Convert.ToInt32(item.nro_letra);
                        row["letra_numero"] = item.letra_numero;
                        row["monto_detalle"] = Convert.ToDecimal(item.monto_detalle);
                        row["fecha"] = Convert.ToDateTime(item.fecha);
                        row["estado_detalle"] = item.estado_detalle;
                        row["fecha_pago"] = Convert.ToDateTime(item.fecha_pago);
                        row["obligacion_tipo"] = item.obligacion_tipo;
                        row["mes_vence"] = item.mes_vence;
                        row["num_cuota"] = item.num_cuota;
                        row["monto_soles"] = Convert.ToDecimal(item.monto_soles);
                        row["monto_dolares"] = Convert.ToDecimal(item.monto_dolares);
                        row["num_week"] = Convert.ToInt32(item.num_week);
                        row["detalle"] = item.detalle;
                        row["NombreEmpresa"] = item.NombreEmpresa;
                        row["RucEmpresa"] = item.RucEmpresa;
                        row["DireccionEmpresa"] = item.DireccionEmpresa;
                        dt.Rows.Add(row);
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);



                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        string path = Path.Combine(Server.MapPath(logoreport));

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrPresupuestoErp.rpt")));
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetDatabaseLogon(datauser, dataclave, dataurl, databases);
                        //*rd.SetDatabaseLogon(datauser, dataclave, dataurl, databases);
                        rd.VerifyDatabase();
                        //rd.SetDataSource(ds);
                        rd.SetParameterValue("@aniobusca", icmb_anio);
                        rd.SetParameterValue("@mesbusca", icmb_mes);
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\Presupuesto-" + cmb_anio + "-" + cmb_mes + ".pdf");
                        rd.Dispose();
                        rd.Close();

                    }
                    ViewBag.Printer = "Presupuesto-" + cmb_anio + "-" + cmb_mes;
                    ViewBag.Codigo = "Presupuesto-" + cmb_anio + "-" + cmb_mes + ".pdf";

                }
                //return null;
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar! " + ex.Message;
                ViewBag.Codigo = "";
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        private async Task<List<ObligacionPagarErp>> GetObligacionPagarErpReport(int cmb_anio, int icmb_mes)
        {
            List<ObligacionPagarErp> lentidad = null;
            ObligacionPagarErp fkletra = new ObligacionPagarErp { aniobusca = cmb_anio, mesbusca = icmb_mes };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Obligacioneserp/t_obligacion_pagar_detalleSelectReport", fkletra);
            var model = response.Content.ReadAsAsync<List<ObligacionPagarErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }


        public async Task<ActionResult> ProductoLinea()
        {
            List<ProductoLineaErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Productoerp/t_producto_lineaSelectAll");
            var model = response.Content.ReadAsAsync<List<ProductoLineaErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<ProductoLineaErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }

        public async Task<ActionResult> RegistroProductoLinea(string id_producto_linea)
        {
            List<ProductoLineaErp> _entidad = new List<ProductoLineaErp>();

            if (id_producto_linea != "0")
            {
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_producto_linea);
                    _entidad = await GetProductoLineaErpIdAsync(identidad);
                    if (_entidad != null)
                    {
                        ViewBag.ProductoLinea = _entidad[0];
                    }
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    ViewBag.ProductoLinea = null;
                }

            }
            else
            {
                ViewBag.ProductoLinea = null;
            }
            return PartialView();
        }

        private async Task<List<ProductoLineaErp>> GetProductoLineaErpIdAsync(int identidad)
        {
            List<ProductoLineaErp> lentidad = null;
            ProductoLineaErp identidads = new ProductoLineaErp { id_producto_linea = identidad };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Productoerp/t_producto_lineaSelect", identidads);
            var model = response.Content.ReadAsAsync<List<ProductoLineaErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }

        public async Task<ActionResult> ProductoTipo()
        {
            List<ProductoTipoErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
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
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        public async Task<ActionResult> RegistroProductoTipo(string id_producto_tipo)
        {
            List<ProductoTipoErp> _entidad = new List<ProductoTipoErp>();
            List<ProductoLineaErp> _llineas = new List<ProductoLineaErp>();
            if (id_producto_tipo != "0")
            {
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_producto_tipo);
                    _entidad = await GetProductoTipoErpIdAsync(identidad);
                    if (_entidad != null)
                    {
                        ViewBag.ProductoLinea = _entidad[0];
                    }
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    ViewBag.ProductoLinea = null;
                }

            }
            else
            {
                ViewBag.ProductoLinea = null;
            }
            var _lineas = await ProductoLineasAll();
            ProductoLineaErp nuevalinea = new ProductoLineaErp()
            {
                id_producto_linea = 0,
                codigo = "",
                descripcion = "",
                estado = "1"
            };
            _lineas.Add(nuevalinea);
            ViewBag.ProductoLinea = _lineas.OrderBy(x => x.descripcion).ToList();
            return PartialView();
        }
        private async Task<List<ProductoTipoErp>> GetProductoTipoErpIdAsync(int identidad)
        {
            List<ProductoTipoErp> lentidad = null;
            ProductoTipoErp identidads = new ProductoTipoErp { id_producto_tipo = identidad };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Productoerp/txtValBus_linea", identidads);
            var model = response.Content.ReadAsAsync<List<ProductoTipoErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        public static async Task<List<ProductoLineaErp>> ProductoLineasAll()
        {
            List<ProductoLineaErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Productoerp/t_producto_lineaSelectAll");
            var model = response.Content.ReadAsAsync<List<ProductoLineaErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<ProductoLineaErp>();
            }

            return lentidad;
        }
        public static async Task<List<ProductoTipoErp>> ProductoTipoAll()
        {
            List<ProductoTipoErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
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

            return lentidad;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaProductoTipo(string id_producto_tipo, string fk_producto_linea, string codigo_tipo, string producto_tipo, string abreviatura_tipo)
        {
            ProductoTipoErp _producto_tipo = new ProductoTipoErp();
            if (id_producto_tipo == "0")
            {
                _producto_tipo.id_producto_tipo = 0;
            }
            else
            {
                _producto_tipo.id_producto_tipo = Convert.ToInt32(id_producto_tipo);
            }
            _producto_tipo.fk_producto_linea = Convert.ToInt32(fk_producto_linea);
            _producto_tipo.codigo_tipo = codigo_tipo;
            _producto_tipo.producto_tipo = producto_tipo;
            _producto_tipo.abreviatura_tipo = abreviatura_tipo;

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_producto_tipo.id_producto_tipo == 0)
                {
                    metodo = "Productoerp/t_producto_tipoInsert";
                }
                else
                {
                    metodo = "Productoerp/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _producto_tipo);
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

        public async Task<ActionResult> UnidadMedida()
        {
            List<UnidadMedidaErp> lentidad = null;
            try
            {
                lentidad = await GetUnidades();
            }
            catch (Exception)
            {
                lentidad = new List<UnidadMedidaErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }

        public static async Task<List<UnidadMedidaErp>> GetUnidades()
        {
            List<UnidadMedidaErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Productoerp/t_unidad_medidaSelectAll");
            var model = response.Content.ReadAsAsync<List<UnidadMedidaErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<UnidadMedidaErp>();
            }

            return lentidad;
        }

        [HttpPost]
        public async Task<JsonResult> EliminaCtaObligacion(int id_obligacion_pagar)
        {
            List<ObligacionPagarErp> _lentidad = new List<ObligacionPagarErp>();
            ObligacionPagarErp _entidad = new ObligacionPagarErp();
            ObligacionPagarErp busca = new ObligacionPagarErp()
            {
                id_obligacion_pagar = id_obligacion_pagar
            };

            try
            {
                _lentidad = await GetObligacionPagarErp();
                if (_lentidad != null)
                {
                    _entidad = _lentidad.FirstOrDefault(x => x.id_obligacion_pagar == id_obligacion_pagar);
                }
                else
                {
                    _entidad = null;
                }
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
                    metodo = "Obligacioneserp/t_obligacion_pagarDelete";
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

        #region AlmacenMovimiento
        public async Task<ActionResult> AlmacenMovimientoIngreso()
        {
            List<AlmacenMovimientoErp> lentidad = null;
            AlmacenMovimientoErp identidads = new AlmacenMovimientoErp { codigo_movimiento_tipo = "I" };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Almacen/GetMovimientoAlmacenCodigo", identidads);
            var model = response.Content.ReadAsAsync<List<AlmacenMovimientoErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<AlmacenMovimientoErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        public async Task<ActionResult> AlmacenMovimientoSalida()
        {
            List<AlmacenMovimientoErp> lentidad = null;
            AlmacenMovimientoErp identidads = new AlmacenMovimientoErp { codigo_movimiento_tipo = "S" };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Almacen/GetMovimientoAlmacenCodigo", identidads);
            var model = response.Content.ReadAsAsync<List<AlmacenMovimientoErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<AlmacenMovimientoErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        public async Task<ActionResult> IndexDetallesMovimiento(string id_almacen_movimiento, string opcion)
        {
            List<AlmacenMovimientoErp> xlentidad = new List<AlmacenMovimientoErp>();
            List<AlmacenMovimientoErp> xlentidad2 = new List<AlmacenMovimientoErp>();
            int ifd_almacen_movimiento = Convert.ToInt32(id_almacen_movimiento);

            AlmacenMovimientoErp _cuota = new AlmacenMovimientoErp();
            if (ifd_almacen_movimiento != 0)
            {
                try
                {
                    ViewBag.IdCuota = ifd_almacen_movimiento;
                    var xentidad = await GetAlmacenMovimientoErpId(ifd_almacen_movimiento);
                    if (xentidad.Any())
                    {
                        xlentidad = xentidad;
                        xlentidad2 = await GetAlmacenMovimientosErpId(ifd_almacen_movimiento);
                    }
                }
                catch (Exception ex)
                {
                    ifd_almacen_movimiento = 0;
                }
            }
            else
            {
                ifd_almacen_movimiento = 0;
                
            }
            ViewBag.IdAlmaMovimiento = ifd_almacen_movimiento;
            ViewBag.Movimientos = xlentidad2;
            ViewBag.Opcion = opcion;
            return PartialView();
        }
        private async Task<List<AlmacenMovimientoErp>> GetAlmacenMovimientoErpId(int id_almacen_movimiento)
        {
            List<AlmacenMovimientoErp> lentidades = null;
            AlmacenMovimientoErp fkletra = new AlmacenMovimientoErp { id_almacen_movimiento = id_almacen_movimiento };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Movimiento/t_almacen_movimientoSelect", fkletra);
            var model = response.Content.ReadAsAsync<List<AlmacenMovimientoErp>>();
            if (model.Result.Count > 0)
            {
                lentidades = model.Result.ToList();
            }
            return lentidades;
        }

        private async Task<List<AlmacenMovimientoErp>> GetAlmacenMovimientosErpId(int id_almacen_movimiento)
        {
            List<AlmacenMovimientoErp> lentidades = null;
            AlmacenMovimientoErp fkletra = new AlmacenMovimientoErp { id_almacen_movimiento = id_almacen_movimiento };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Movimiento/t_almacen_movimientosSelect", fkletra);
            var model = response.Content.ReadAsAsync<List<AlmacenMovimientoErp>>();
            if (model.Result.Count > 0)
            {
                lentidades = model.Result.ToList();
            }
            return lentidades;
        }
        #endregion

        public async Task<ActionResult> ListaProductoAlmacen(string idproducto,string pCallBy, string fk_almacen)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            int idinserted = 0;
            int id_producto = Convert.ToInt32(idproducto);
            int fkalmacen = Convert.ToInt32(fk_almacen);
            if (id_producto == 0)
            {
                idinserted = 0;
            }
            else
            {
                idinserted = id_producto;
            }
            ViewBag.FkProducto = idinserted;
            var products = await GetProductoAllAlmacen(fkalmacen);

            var objPersonallist = await compraCtrl.GetPersonalErp();
            PersonalErp personerp = new PersonalErp()
            {
                IDCODIGOGENERAL = "",
                NOMBRES_FULL = "",
                A_PATERNO = ""
            };
            objPersonallist.Add(personerp);
            ViewBag.Personal = objPersonallist.OrderBy(z => z.A_PATERNO).ToList();
            ViewBag.Productos = products.OrderBy(z => z.nom_producto).ToList();
            ViewBag.CallBy = pCallBy;
            return PartialView();
        }
        public async Task<List<Producto>> GetProductoAll()
        {
            List<Producto> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/all");
            var model = response.Content.ReadAsAsync<List<Producto>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Producto>();
            }
            return lentidad;
        }
        public async Task<List<Producto>> GetProductoAllAlmacen(int fkalmacen)
        {
            List<Producto> lproductos = null;
            Producto busca = new Producto()
            {
                fk_almacen = fkalmacen
            };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("producto/t_productoSelectAlmacenAll", busca);
                var model = response.Content.ReadAsAsync<List<Producto>>();
                lproductos = new List<Producto>();
                if (model.Result.Count > 0)
                {
                    lproductos = model.Result.ToList();
                }
            }
            catch (Exception e)
            {
                lproductos = new List<Producto>();
            }

            return lproductos;
        }
        public async Task<JsonResult> GetVisitCustomerAsync(string term = "")
        {
            var objCustomerlist = await compraCtrl.GetPersonalErp();
            if (objCustomerlist.Any())
            {
                objCustomerlist = objCustomerlist
                    .Where(x => x.NOMBRES_FULL.ToUpper()
                    .Contains(term.ToUpper()))
                    .Select(c => new PersonalErp() { IDCODIGOGENERAL = c.IDCODIGOGENERAL, NOMBRES_FULL = c.NOMBRES_FULL })
                    .Distinct().ToList();
            }
            return Json(objCustomerlist, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GuiaRemisionSalida()
        {
            List<GuiaRemisionSalida> lentidad = null;
            try
            {
                lentidad = await GetGuias();
            }
            catch (Exception)
            {
                lentidad = new List<GuiaRemisionSalida>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }

        public static async Task<List<GuiaRemisionSalida>> GetGuias()
        {
            List<GuiaRemisionSalida> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("GuiaRemisionSalida/all");
            var model = response.Content.ReadAsAsync<List<GuiaRemisionSalida>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<GuiaRemisionSalida>();
            }

            return lentidad;
        }

        public async Task<ActionResult> NewGuiaRemisionSalida(string idalmacenmovimiento)
        {
            List<AlmacenMovimientoErp> lalmacenmov = null;
            int id_almacen_movimiento = 0;
            if (idalmacenmovimiento != "")
            {
                id_almacen_movimiento = Convert.ToInt32(idalmacenmovimiento);
                if (id_almacen_movimiento > 0)
                {
                    lalmacenmov = await GetAlmacenMovimientosErpId(id_almacen_movimiento);
                }
            }
            List<GuiaRemisionSalida> lentidad = null;
            ViewBag.IdAlmacenMovimiento = id_almacen_movimiento;
            ViewBag.ListAlmaMov = lalmacenmov;

            
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }


        public async Task<List<DetraccionTipo>> GetDetraccionTipos()
        {
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteVenta/t_detraccion_tipoSelectAll");
                var model = response.Content.ReadAsAsync<List<DetraccionTipo>>();
                if (model.Result.Count > 0)
                {
                    return model.Result.ToList();
                }
                else
                {
                    return new List<DetraccionTipo>();
                }
            }
            catch (Exception ex)
            {
                return new List<DetraccionTipo>();
            }
        }

        public async Task<List<TipoAfectacion>> GetTipoAfectacionErp()
        {
            List<TipoAfectacion> xlMonedaErp = new List<TipoAfectacion>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteVenta/t_sunat_07_tipo_afectacion_igvSelec");
                var model = response.Content.ReadAsAsync<List<TipoAfectacion>>();
                if (model.Result.Count > 0)
                {
                    xlMonedaErp = model.Result.ToList();
                }
                else
                {
                    xlMonedaErp = new List<TipoAfectacion>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlMonedaErp;
        }
    }
}
