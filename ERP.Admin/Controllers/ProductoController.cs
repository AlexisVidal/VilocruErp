using ERP.Admin.App_Start;
using ERP.Admin.Models;
using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Excelold = Microsoft.Office.Interop.Excel;


namespace ERP.Admin.Controllers
{
    public class Product
    {
        public int id_producto { get; set; }
        public string nom_producto { get; set; }
    }

    [SessionAuthorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        //public ActionResult Index()
        //{
        //    //return View();
        //}
        [HttpPost]
        public JsonResult GuardarImagenProducto()
        {
            string urlftp = "";
            try
            {
                int idProd = 0;

                idProd = Convert.ToInt32(ControllerContext.HttpContext.Request.Form["hidIdProducto"]);
                HttpFileCollectionBase files = Request.Files;
                if (files.Count == 2)
                {
                    HttpPostedFileBase file = files[0];
                    idProd = Convert.ToInt32(files.AllKeys[1]);
                    urlftp = UploadFtp(file, idProd);
                    if (urlftp != "")
                    {
                        return Json(urlftp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("");
                    }


                    //fileName = "producto-" + idProd.ToString() + System.IO.Path.GetExtension(file.FileName);

                    ////strRutaImagProd = ConfigurationManager.AppSettings["vGlobalRutaImagenProducto"].ToString();
                    //rutaImagProd = "192.168.0.16:8080/";
                    ////rutaImagProd = Server.MapPath("~/img/product/");
                    //if (rutaImagProd.Substring(0, 2).Equals(".."))
                    //{
                    //    rutaImagProd = rutaImagProd.Substring(2, rutaImagProd.Length - 2);
                    //}
                    //onlyFilaName = fileName;
                    ////fileName = Path.Combine(Server.MapPath("~" + rutaImagProd), fileName);// Si se quiere guardar el el servidor iis (Original)
                    //fileName = rutaImagProd + fileName;// Ruta si se quiere guardar en otra direccion con el ip \\192.168.10.190\DESARROLLO\ClienteAsp\ImagesProducto\; si esta publicado en la misma maquina trabajar con la unidad D:\\DESARROLLO\ClienteAsp\ImagesProducto\
                    //                                   //file.SaveAs("D:\\\\ImagesProducto\\" + fileName);
                    //file.SaveAs(fileName);
                    //return Json(fileName, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(urlftp, JsonRequestBehavior.AllowGet);
        }

        private string UploadFtp(HttpPostedFileBase file, int idProd)
        {
            try
            {
                var uploadurl = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
                var uploadfilename = "producto-" + idProd.ToString().PadLeft(4, '0') + System.IO.Path.GetExtension(file.FileName); // file.FileName;
                var username = System.Configuration.ConfigurationManager.AppSettings["Ftpuser"]; ;
                var password = System.Configuration.ConfigurationManager.AppSettings["Ftppass"]; ;

                Stream streamObj = file.InputStream;
                byte[] buffer = new byte[file.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                requestObj.Credentials = new NetworkCredential(username, password);
                Stream requestStream = requestObj.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
                requestObj = null;
                return ftpurl;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpPost]
        public async Task<JsonResult> ActualizaProducto(string id_save, string image_url)
        {
            Producto _entidad = new Producto();
            if (id_save == "0")
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                _entidad.id_producto = Convert.ToInt32(id_save);
            }
            
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "Producto/imagenup";
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
        public async Task<JsonResult> Guarda(string id_save, string fk_proveedor, string fk_producto_marca, string fk_producto_subfamilia, string nom_producto, string codigo_sku, string embalaje,
            string id_precio, string preciocompra, string dscto1, string dscto2, string dscto3, string dscto4, string preciocomprafinal, string fktipomoneda, string codproduct,
            string nomArc, string image_urlsx)
        {
            Producto _entidad = new Producto();
            if (id_save == "0")
            {
                _entidad.id_producto = 0;
            }
            else
            {
                _entidad.id_producto = Convert.ToInt32(id_save);
            }
            //_entidad.fk_proveedor = Convert.ToInt32(fk_proveedor);
            _entidad.fk_producto_marca = Convert.ToInt32(fk_producto_marca);
            //_entidad.fk_producto_subfamilia = Convert.ToInt32(fk_producto_subfamilia);
            //_entidad.fk_tipo_moneda = Convert.ToInt32(fktipomoneda);
            //string partone = await GetProductoSubFamiliaCodigoAsync(Convert.ToInt32(fk_producto_subfamilia));
            //int maxid = await GetProductoxSubFamiliaMaxAsync(Convert.ToInt32(fk_producto_subfamilia));
            //maxid = maxid + 1;
            //if (codproduct != "")
            //{
            //    _entidad.cod_producto = codproduct;
            //}
            //else
            //{
            //    _entidad.cod_producto = partone + "." + maxid.ToString().PadLeft(4, '0');
            //}

            _entidad.nom_producto = nom_producto;
            _entidad.codigo_sku = codigo_sku;
            //if (nomArc == "" && image_urlsx != "")
            //{
            //    _entidad.image_url = image_urlsx;
            //}
            //else
            //{
            //    _entidad.image_url = "ftp://localhost//productodefault.png";
            //}

            //_entidad.embalaje = embalaje;
            try
            {
                decimal precioi = Convert.ToDecimal(preciocompra);
                _entidad.preciocompra = precioi;
            }
            catch (Exception ex)
            {
                _entidad.preciocompra = 0;
            }
            try
            {
                decimal preciomi = Convert.ToDecimal(preciocomprafinal);
                _entidad.preciocomprafinal = preciomi;
            }
            catch (Exception ex)
            {
                _entidad.preciocomprafinal = 0;
            }
           //_entidad.embalaje = embalaje;
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
                    metodo = "Producto/agregar";
                }
                else
                {
                    metodo = "Producto/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                    int precioinserted = await InsertPrecioCompra(idinserted, id_precio, preciocompra, dscto1, dscto2, dscto3, dscto4, preciocomprafinal);
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        private async Task<int> InsertPrecioCompra(string idinserted, string id_precio, string preciocompra, string dscto1, string dscto2, string dscto3, string dscto4, string preciocomprafinal)
        {
            try
            {
                int retorno = 0;
                PrecioCompra _precio = new PrecioCompra();
                int fk_producto = Convert.ToInt32(idinserted);
                int id_precio_i = Convert.ToInt32(id_precio);
                _precio.id_precio_compra = id_precio_i;
                _precio.fk_producto = fk_producto;
                _precio.preciocompra = Convert.ToDecimal(preciocompra);
                _precio.dsctounop = Convert.ToDecimal(dscto1);
                _precio.dsctodosp = Convert.ToDecimal(dscto2);
                _precio.dsctotresp = Convert.ToDecimal(dscto3);
                _precio.dsctocuatrop = Convert.ToDecimal(dscto4);
                _precio.preciocomprafinal = Convert.ToDecimal(preciocomprafinal);
                _precio.estado = "1";
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_precio.id_precio_compra == 0)
                {
                    metodo = "PrecioCompra/agregar";
                }
                else
                {
                    metodo = "PrecioCompra/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _precio);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    retorno = Convert.ToInt32(respuesta.Result.ToString());
                    return retorno;
                }


                return retorno;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private async Task<int> InsertPrecioVenta(string fk_producto, int fk_precio_compra, decimal precio_unidadp,
                            decimal precio_mayorp, decimal precio_especialp, decimal precio_unidad,
                            decimal precio_mayor, decimal precio_especial, decimal tipo_cambio)
        {
            ProductoPrecioVenta _entidad = new ProductoPrecioVenta();
            _entidad.id_producto_precio_venta = 0;

            _entidad.fk_producto = Convert.ToInt32(fk_producto);
            _entidad.fk_precio_compra = Convert.ToInt32(fk_precio_compra);
            _entidad.precio_unidadp = Convert.ToDecimal(precio_unidadp);
            _entidad.precio_unidad = Convert.ToDecimal(precio_unidad);
            _entidad.precio_mayorp = Convert.ToDecimal(precio_mayorp);
            _entidad.precio_mayor = Convert.ToDecimal(precio_mayor);
            _entidad.precio_especialp = Convert.ToDecimal(precio_especialp);
            _entidad.precio_especial = Convert.ToDecimal(precio_especial);
            _entidad.tipo_cambio = Convert.ToDecimal(tipo_cambio);

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
                if (_entidad.id_producto_precio_venta == 0)
                {
                    metodo = "ProductoPrecioVenta/agregar";
                }
                else
                {
                    metodo = "ProductoPrecioVenta/modificar";
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
                return 0;
            }
            return Convert.ToInt32(idinserted);
        }
        private async Task<string> GetProductoSubFamiliaCodigoAsync(int id_save)
        {
            List<ProductoSubFamilia> lentidad = null;
            ProductoSubFamilia _entidad = new ProductoSubFamilia { id_producto_subfamilia = id_save };
            string codigox = "";
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoSubFamilia/buscar", _entidad);
            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
                codigox = lentidad[0].codigo.ToString();
            }
            return codigox;
        }
        //private async Task<int> GetProductoxSubFamiliaMaxAsync(int id_save)
        //{
        //    List<Producto> lentidad = null;
        //    Producto _entidad = new Producto { fk_producto_subfamilia = id_save };
        //    int codigox = 0;
        //    HttpClient client = new HttpClient();
        //    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
        //    client.BaseAddress = new Uri(connectionInfo);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await client.PostAsJsonAsync("Producto/buscarfkproductomarcasf", _entidad);
        //    var model = response.Content.ReadAsAsync<List<Producto>>();
        //    if (model.Result.Count > 0)
        //    {
        //        lentidad = model.Result.ToList();
        //        string codigux = lentidad.Last().cod_producto;
        //        int indexofpoint = codigux.IndexOf('.');
        //        string lastpart = codigux.Substring(indexofpoint + 1);

        //        //codigox = lentidad.Count();
        //        codigox = Convert.ToInt32(lastpart);
        //    }
        //    return codigox;
        //}

        public ActionResult VerFoto(string foto)
        {
            if (foto == "")
            {
                foto = "ftp://localhost//productodefault.png";
            }
            string ftps = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"];
            int couns = ftps.Length + 1;   //para ip .0.230
            //int couns = ftps.Length - 1;
            int indexlast = foto.LastIndexOf('/');
            foto = foto.Substring(indexlast + 1);
            foto = virtualdir + foto;
            ViewBag.Foto = foto;

            return PartialView();
        }


        public async Task<ActionResult> Registro(string id_save)
        {
            List<Producto> _lentidad = new List<Producto>();
            List<ProductoMarca> _lentidadmarca = new List<ProductoMarca>();
           
            
            _lentidadmarca = await GetProductoMarcaAsync();
            ViewBag.ProductoMarca = _lentidadmarca;
            

            if (id_save != "0")
            {
                int id_savei = 0;
                try
                {
                    id_savei = Convert.ToInt32(id_save);
                    _lentidad = await GetProductoIdAsync(id_savei);
                    if (_lentidad != null)
                    {
                        ViewBag.Producto = _lentidad[0];
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
            return PartialView();
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

        private async Task<List<Proveedor>> GetProveedorAsync()
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

        private async Task<List<PrecioCompra>> GetPreciosCompraAsync(string id_save)
        {
            List<PrecioCompra> lPrecioCompra = null;
            int id_savei = Convert.ToInt32(id_save);
            PrecioCompra id_ = new PrecioCompra { fk_producto = id_savei };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("PrecioCompra/buscarproducto", id_);

            var model = response.Content.ReadAsAsync<List<PrecioCompra>>();
            if (model.Result.Count > 0)
            {
                lPrecioCompra = model.Result.ToList();
            }

            return lPrecioCompra;
        }

        [HttpPost]
        public async Task<JsonResult> Filter_ProductoMarcaAsync(string format)
        {
            List<ProductoMarca> lproductomarca = null;
            List<ProductoMarca> lproductomarca2 = null;
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
                if (format.Length > 0)
                {
                    lproductomarca2 = lproductomarca.Where(x => x.descripcion.Contains(format)).ToList();
                }
            }

            return Json(lproductomarca2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Filter_ProductoSubFamiliaAsync(string idfamilia)
        {
            int idfamilia_i = Convert.ToInt32(idfamilia);
            List<ProductoSubFamilia> lproductosubfam = null;
            List<ProductoSubFamilia> lproductosubfam2 = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoSubFamilia/all");
            var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
            if (model.Result.Count > 0)
            {
                lproductosubfam = model.Result.ToList();
                if (idfamilia.Length > 0)
                {
                    lproductosubfam2 = lproductosubfam.Where(x => x.fk_producto_familia == idfamilia_i).ToList();
                }
            }

            return Json(lproductosubfam2, JsonRequestBehavior.AllowGet);
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


        private async Task<List<Producto>> GetProductoIdAsync(int id_savei)
        {
            List<Producto> lproducto = null;

            Producto id_ = new Producto { id_producto = id_savei };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Producto/buscar", id_);

            var model = response.Content.ReadAsAsync<List<Producto>>();
            if (model.Result.Count > 0)
            {
                lproducto = model.Result.ToList();
            }

            return lproducto;
        }
        public async Task<ActionResult> GeneracionPrecioVenta(string id_save, string fkproducto, string nom_producto,
            string precio_compra, string fk_precio_compra, string fk_tipo_moneda, string tipo_cambio, string moneda)
        {
            int fkproductoi = Convert.ToInt32(fkproducto);
            ViewBag.ProductoID = fkproductoi;
            ViewBag.NomProducto = nom_producto;
            ViewBag.PrecioCompra = precio_compra;
            ViewBag.FkPrecioCompra = fk_precio_compra;
            ViewBag.Moneda = moneda;
            ViewBag.TipoMoneda = fk_tipo_moneda;
            ViewBag.TipoCambio = tipo_cambio;
            if (fk_tipo_moneda == "2")
            {
                if (tipo_cambio != "0.00")
                {
                    ViewBag.PrecioFinalSoles = Convert.ToDecimal(tipo_cambio) * Convert.ToDecimal(precio_compra);
                }
                else
                {
                    ViewBag.PrecioFinalSoles = Convert.ToDecimal(1) * Convert.ToDecimal(precio_compra);
                }
            }
            else
            {
                ViewBag.PrecioFinalSoles = precio_compra;
            }
            List<ProductoPrecioVenta> _lentidad = new List<ProductoPrecioVenta>();

            if (id_save != "0")
            {
                int id_savei = 0;
                try
                {
                    id_savei = Convert.ToInt32(id_save);
                    _lentidad = await GetPrecioVentaAsync(id_savei);
                    if (_lentidad != null)
                    {
                        ViewBag.ProductoPrecioVenta = _lentidad[0];
                    }
                    else
                    {
                        ViewBag.ProductoPrecioVenta = null;
                    }
                }
                catch (Exception ex)
                {
                    id_savei = 0;
                    ViewBag.ProductoPrecioVenta = null;
                }

            }
            else
            {
                ViewBag.ProductoPrecioVenta = null;
            }
            return PartialView();
        }

        private async Task<List<ProductoPrecioVenta>> GetPrecioVentaAsync(int id_savei)
        {
            List<ProductoPrecioVenta> lproducto = null;

            ProductoPrecioVenta id_ = new ProductoPrecioVenta { id_producto_precio_venta = id_savei };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoPrecioVenta/buscar", id_);

            var model = response.Content.ReadAsAsync<List<ProductoPrecioVenta>>();
            if (model.Result.Count > 0)
            {
                lproducto = model.Result.ToList();
            }

            return lproducto;
        }
        //Luis
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

        public async Task<ActionResult> ListaProductos(string CallBy, int? FkProv, string EstaProd)
        {
            List<Producto> lstProducto = null;
            try
            {
                lstProducto = await GetProductoAll();
                if (EstaProd != null)
                {
                    lstProducto = lstProducto.Where(i => i.estado.Equals(EstaProd)).ToList();
                }
                if (FkProv != null)
                {
                    //lstProducto = lstProducto.Where(i => i.fk_proveedor.Equals(Convert.ToInt32(FkProv))).ToList();
                }
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                return null;
            }
            return PartialView(lstProducto);
        }
        //Fin Luis

        

        public ActionResult Upload()
        {
            return PartialView();
        }

        private string UploadFtpExcelold(HttpPostedFileBase file)
        {
            try
            {
                Random r = new Random();
                int n = r.Next();
                string adds = n.ToString();

                var uploadurl = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
                //var uploadfilename = adds + "-" + file.FileName; // file.FileName;
                var uploadfilename = file.FileName; // file.FileName;
                var username = System.Configuration.ConfigurationManager.AppSettings["Ftpuser"]; ;
                var password = System.Configuration.ConfigurationManager.AppSettings["Ftppass"]; ;

                Stream streamObj = file.InputStream;
                byte[] buffer = new byte[file.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                requestObj.Credentials = new NetworkCredential(username, password);
                Stream requestStream = requestObj.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
                requestObj = null;
                return ftpurl;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [HttpPost]
        public async Task<ActionResult> Import()
        {
            HttpFileCollectionBase files = Request.Files;
            string path, urlftpn = "";
            string newpath = "http://192.168.10.135/uploads/x.xls";
            if (files.Count == 1)
            {
                HttpPostedFileBase filex = files[0];
                string urlftp = UploadFtpExcelold(filex);
                if (urlftp != "")
                {
                    string ftps = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
                    string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"];
                    int couns = ftps.Length + 1;
                    urlftpn = urlftp.Substring(couns);
                    newpath = virtualdir + urlftpn;
                    Excelold.Application applicatio = new Excelold.Application();
                    Excelold.Workbook workbook = null;
                    Excelold.Worksheet worksheet = null;
                    Excelold.Range range = null;
                    try
                    {

                        workbook = applicatio.Workbooks.Open(newpath);
                        worksheet = (Excelold.Worksheet)workbook.Sheets[1]; // <--- See this line 
                        range = worksheet.UsedRange;

                        List<ProductoImport> lproductoimport = new List<ProductoImport>();
                        for (int row = 2; row <= range.Rows.Count; row++)
                        {
                            ProductoImport p = new ProductoImport();
                            try
                            {
                                if (((Excelold.Range)range.Cells[row, 6]).Value.ToString() == "")
                                {
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                break;
                            }
                            int idproductexist = await ProductoExistAsync(((Excelold.Range)range.Cells[row, 6]).Value.ToString());
                            if (idproductexist <= 0)
                            {

                                p.PROVEEDOR = ((Excelold.Range)range.Cells[row, 1]).ToString();

                                p.IDPROVEEDOR = await TraeProveedorRazonAsync(((Excelold.Range)range.Cells[row, 1]).Text);
                                p.MARCA = ((Excelold.Range)range.Cells[row, 2]).Text;
                                p.IDMARCA = await TraeMarcaNombreAsync(((Excelold.Range)range.Cells[row, 2]).Text);
                                if (p.IDMARCA == 0)
                                {
                                    p.IDMARCA = await GuardaProductoMarcaImport(((Excelold.Range)range.Cells[row, 2]).Text);
                                }
                                p.FAMILIA = ((Excelold.Range)range.Cells[row, 3]).Text;
                                p.IDFAMILIA = await TraeFamiliaNombreAsync(((Excelold.Range)range.Cells[row, 3]).Text);
                                if (p.IDFAMILIA == 0)
                                {
                                    string familiadescr = ((Excelold.Range)range.Cells[row, 3]).Text;
                                    string codigofamili = familiadescr.Substring(0, 2);
                                    p.IDFAMILIA = await GuardaProductoFamiliaImport(codigofamili, familiadescr);
                                }
                                p.SUBFAMILIA = ((Excelold.Range)range.Cells[row, 4]).Text;
                                p.IDSUBFAMILIA =
                                    await TraeSubFamiliaNombreAsync(p.IDFAMILIA, ((Excelold.Range)range.Cells[row, 4]).Text);
                                if (p.IDSUBFAMILIA == 0)
                                {
                                    p.IDSUBFAMILIA = await GuardaProductoSubFamiliaImport(p.IDFAMILIA, "", p.SUBFAMILIA);
                                }
                                p.SKU = ((Excelold.Range)range.Cells[row, 5]).Text;

                                //CHECK IF EXIST WITH THE NAME

                                //CHECK IF EXIST WITH THE NAME
                                p.PRODUCTO = ((Excelold.Range)range.Cells[row, 6]).Text;
                                p.UND = ((Excelold.Range)range.Cells[row, 7]).Text;
                                p.EMPAQUE = ((Excelold.Range)range.Cells[row, 8]).Text;
                                p.IDTIPOMONEDA = Convert.ToInt32(((Excelold.Range)range.Cells[row, 9]).Text);

                                //PRECIO COMPRA
                                if (((Excelold.Range)range.Cells[row, 10]).Text == "")
                                {
                                    p.PRECIOCOMPRA = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        p.PRECIOCOMPRA = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 10]).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        p.PRECIOCOMPRA = 0;
                                    }
                                }

                                if (((Excelold.Range)range.Cells[row, 11]).Text == "")
                                {
                                    p.DSCTOUNO = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        p.DSCTOUNO = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 11]).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        p.DSCTOUNO = 0;
                                    }
                                }

                                if (((Excelold.Range)range.Cells[row, 13]).Text == "")
                                {
                                    p.DSCTODOS = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        p.DSCTODOS = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 13]).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        p.DSCTODOS = 0;
                                    }
                                }

                                if (((Excelold.Range)range.Cells[row, 15]).Text == "")
                                {
                                    p.DSCTOTRES = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        p.DSCTOTRES = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 15]).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        p.DSCTOTRES = 0;
                                    }
                                }

                                if (((Excelold.Range)range.Cells[row, 17]).Text == "")
                                {
                                    p.DSCTOCUATRO = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        p.DSCTOCUATRO = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 17]).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        p.DSCTOCUATRO = 0;
                                    }
                                }

                                if (((Excelold.Range)range.Cells[row, 19]).Text == "")
                                {
                                    p.PRECIOCOMPRAFINAL = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        p.PRECIOCOMPRAFINAL = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 19]).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        p.PRECIOCOMPRAFINAL = 0;
                                    }
                                }



                                if (p.IDPROVEEDOR == 0)
                                {

                                }
                                else
                                {
                                    lproductoimport.Add(p);
                                }
                            }

                        }
                        try
                        {
                            //workbook.Close();
                            //applicatio.Quit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        if (lproductoimport.Count > 0)
                        {
                            int resultado = await RegistraProductosAsync(lproductoimport);
                            return Json(resultado, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("0", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(ex, JsonRequestBehavior.AllowGet);
                    }
                    finally
                    {
                        if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                        if (workbook != null) Marshal.ReleaseComObject(workbook);
                        if (applicatio != null) Marshal.ReleaseComObject(applicatio);
                    }
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<int> GuardaProductoMarcaImport(string descripcion)
        {
            int idexistente = 0;
            ProductoMarca _productmarc = new ProductoMarca();

            _productmarc.descripcion = descripcion;
            _productmarc.estado = "1";
            int idinserted = 0;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "ProductoMarca/agregar";
                var response = await client.PostAsJsonAsync(metodo, _productmarc);
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
        public async Task<int> GuardaProductoFamiliaImport(string codigo, string descripcion)
        {
            int idexistente = 0;
            ProductoFamilia _producto_familia = new ProductoFamilia();
            _producto_familia.codigo = codigo;
            _producto_familia.descripcion = descripcion;
            _producto_familia.estado = "1";
            int idinserted = 0;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "ProductoFamilia/agregar";

                var response = await client.PostAsJsonAsync(metodo, _producto_familia);
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

        public async Task<int> GuardaProductoSubFamiliaImport(int fkfamilia, string codigo, string descripcion)
        {
            ProductoSubFamilia _productosubfamilia = new ProductoSubFamilia();
            _productosubfamilia.id_producto_subfamilia = 0;
            _productosubfamilia.fk_producto_familia = Convert.ToInt32(fkfamilia);
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
            int idinserted = 0;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "ProductoSubFamilia/agregar";
                var response = await client.PostAsJsonAsync(metodo, _productosubfamilia);
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
        private async Task<int> GetMaxIdAsync(int fkfamilia)
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
        private async Task<string> GetCodigoAsync(int fkfamilia)
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
        //[HttpPost]
        //public async Task<ActionResult> Import()
        //{
        //    HttpFileCollectionBase files = Request.Files;
        //    string path, urlftpn = "";
        //    string newpath = "http://192.168.10.135/uploads/x.xls";
        //    if (files.Count == 1)
        //    {
        //        HttpPostedFileBase filex = files[0];
        //        string urlftp = UploadFtpExcelold(filex);
        //        if (urlftp != "")
        //        {
        //            string ftps = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
        //            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"];
        //            int couns = ftps.Length + 1;
        //            urlftpn = urlftp.Substring(couns);
        //            newpath = virtualdir + urlftpn;
        //            Excelold.Application applicatio = new Excelold.Application();
        //            Excelold.Workbook workbook = null;
        //            Excelold.Worksheet worksheet = null;
        //            Excelold.Range range = null;
        //            try
        //            {

        //                workbook = applicatio.Workbooks.Open(newpath);
        //                worksheet = (Excelold.Worksheet)workbook.Sheets[1]; // <--- See this line 
        //                range = worksheet.UsedRange;

        //                List<ProductoImport> lproductoimport = new List<ProductoImport>();
        //                for (int row = 2; row <= range.Rows.Count; row++)
        //                {
        //                    ProductoImport p = new ProductoImport();
        //                    try
        //                    {
        //                        if (((Excelold.Range)range.Cells[row, 6]).Value.ToString() == "")
        //                        {
        //                            break;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        break;
        //                    }
        //                    int idproductexist = await ProductoExistAsync(((Excelold.Range)range.Cells[row, 6]).Value.ToString());
        //                    if (idproductexist <= 0)
        //                    {

        //                        p.PROVEEDOR = ((Excelold.Range)range.Cells[row, 1]).ToString();

        //                        p.IDPROVEEDOR = await TraeProveedorRazonAsync(((Excelold.Range)range.Cells[row, 1]).Text);
        //                        p.MARCA = ((Excelold.Range)range.Cells[row, 2]).Text;
        //                        p.IDMARCA = await TraeMarcaNombreAsync(((Excelold.Range)range.Cells[row, 2]).Text);
        //                        p.FAMILIA = ((Excelold.Range)range.Cells[row, 3]).Text;
        //                        p.IDFAMILIA = await TraeFamiliaNombreAsync(((Excelold.Range)range.Cells[row, 3]).Text);
        //                        p.SUBFAMILIA = ((Excelold.Range)range.Cells[row, 4]).Text;
        //                        p.IDSUBFAMILIA =
        //                            await TraeSubFamiliaNombreAsync(p.IDFAMILIA, ((Excelold.Range)range.Cells[row, 4]).Text);
        //                        p.SKU = ((Excelold.Range)range.Cells[row, 5]).Text;

        //                        //CHECK IF EXIST WITH THE NAME

        //                        //CHECK IF EXIST WITH THE NAME
        //                        p.PRODUCTO = ((Excelold.Range)range.Cells[row, 6]).Text;
        //                        p.UND = ((Excelold.Range)range.Cells[row, 7]).Text;
        //                        p.EMPAQUE = ((Excelold.Range)range.Cells[row, 8]).Text;
        //                        p.IDTIPOMONEDA = Convert.ToInt32(((Excelold.Range)range.Cells[row, 9]).Text);

        //                        //PRECIO COMPRA
        //                        if (((Excelold.Range)range.Cells[row, 10]).Text == "")
        //                        {
        //                            p.PRECIOCOMPRA = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.PRECIOCOMPRA = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 10]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.PRECIOCOMPRA = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 11]).Text == "")
        //                        {
        //                            p.DSCTOUNO = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTOUNO = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 11]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTOUNO = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 13]).Text == "")
        //                        {
        //                            p.DSCTODOS = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTODOS = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 13]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTODOS = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 15]).Text == "")
        //                        {
        //                            p.DSCTOTRES = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTOTRES = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 15]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTOTRES = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 17]).Text == "")
        //                        {
        //                            p.DSCTOCUATRO = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTOCUATRO = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 17]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTOCUATRO = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 19]).Text == "")
        //                        {
        //                            p.PRECIOCOMPRAFINAL = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.PRECIOCOMPRAFINAL = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 19]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.PRECIOCOMPRAFINAL = 0;
        //                            }
        //                        }



        //                        if (p.IDPROVEEDOR == 0)
        //                        {

        //                        }
        //                        else
        //                        {
        //                            lproductoimport.Add(p);
        //                        }
        //                    }

        //                }
        //                //try
        //                //{
        //                //    workbook.Close();
        //                //    applicatio.Quit();
        //                //}
        //                //catch (Exception ex)
        //                //{

        //                //}

        //                if (lproductoimport.Count > 0)
        //                {
        //                    int resultado = await RegistraProductosAsync(lproductoimport);
        //                    return Json(resultado, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    return Json("0", JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return Json(ex, JsonRequestBehavior.AllowGet);
        //            }
        //            finally
        //            {
        //                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
        //                if (workbook != null) Marshal.ReleaseComObject(workbook);
        //                if (applicatio != null) Marshal.ReleaseComObject(applicatio);
        //            }
        //        }
        //        else
        //        {
        //            return Json("0", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json("0", JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> Import(/*HttpPostedFileBase Exceloldfile*/)
        //{
        //    HttpFileCollectionBase files = Request.Files;
        //    string path, urlftpn = "";
        //    string newpath = "http://192.168.10.135/uploads/x.xls";
        //    if (files.Count == 1)
        //    {
        //        HttpPostedFileBase filex = files[0];
        //        string urlftp = UploadFtpExcelold(filex);
        //        if (urlftp != "")
        //        {
        //            string ftps = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
        //            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"];
        //            int couns = ftps.Length + 1;
        //            urlftpn = urlftp.Substring(couns);
        //            newpath = virtualdir + urlftpn;

        //            //var process = System.Diagnostics.Process.GetProcessesByName("Excelold");
        //            ////return Json("process: " + process, JsonRequestBehavior.AllowGet);
        //            //foreach (var p in process)
        //            //{
        //            //    if (!string.IsNullOrEmpty(p.ProcessName))
        //            //    {
        //            //        try
        //            //        {
        //            //            p.Kill();
        //            //        }
        //            //        catch (Exception a)
        //            //        {
        //            //            return Json("Exception killing Excelold: " + a.Message, JsonRequestBehavior.AllowGet);
        //            //        }
        //            //    }
        //            //}



        //            Excelold.Application applicatio = new Excelold.Application();
        //            Excelold.Workbook workbook = null;
        //            Excelold.Worksheet worksheet = null;
        //            Excelold.Range range = null;
        //            try
        //            {

        //                workbook = applicatio.Workbooks.Open(newpath);
        //                worksheet = (Excelold.Worksheet)workbook.Sheets[1]; // <--- See this line 
        //                range = worksheet.UsedRange;

        //                List<ProductoImport> lproductoimport = new List<ProductoImport>();
        //                for (int row = 2; row <= range.Rows.Count; row++)
        //                {
        //                    ProductoImport p = new ProductoImport();
        //                    //int idproductexist = await ProductoExistAsync(((Excelold.Range)range.Cells[row, 6]).Text);
        //                    int idproductexist = await ProductoExistAsync(((Excelold.Range)range.Cells[row, 6]).Value.ToString());
        //                    if (idproductexist <= 0)
        //                    {

        //                        p.PROVEEDOR = ((Excelold.Range)range.Cells[row, 1]).ToString();
        //                        p.IDPROVEEDOR = await TraeProveedorRazonAsync(((Excelold.Range)range.Cells[row, 1]).Text);
        //                        p.MARCA = ((Excelold.Range)range.Cells[row, 2]).Text;
        //                        p.IDMARCA = await TraeMarcaNombreAsync(((Excelold.Range)range.Cells[row, 2]).Text);
        //                        p.FAMILIA = ((Excelold.Range)range.Cells[row, 3]).Text;
        //                        p.IDFAMILIA = await TraeFamiliaNombreAsync(((Excelold.Range)range.Cells[row, 3]).Text);
        //                        p.SUBFAMILIA = ((Excelold.Range)range.Cells[row, 4]).Text;
        //                        p.IDSUBFAMILIA =
        //                            await TraeSubFamiliaNombreAsync(p.IDFAMILIA, ((Excelold.Range)range.Cells[row, 4]).Text);
        //                        p.SKU = ((Excelold.Range)range.Cells[row, 5]).Text;

        //                        //CHECK IF EXIST WITH THE NAME

        //                        //CHECK IF EXIST WITH THE NAME
        //                        p.PRODUCTO = ((Excelold.Range)range.Cells[row, 6]).Text;
        //                        p.UND = ((Excelold.Range)range.Cells[row, 7]).Text;
        //                        p.EMPAQUE = ((Excelold.Range)range.Cells[row, 8]).Text;
        //                        p.IDTIPOMONEDA = Convert.ToInt32(((Excelold.Range)range.Cells[row, 9]).Text);

        //                        //PRECIO COMPRA
        //                        if (((Excelold.Range)range.Cells[row, 10]).Text == "")
        //                        {
        //                            p.PRECIOCOMPRA = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.PRECIOCOMPRA = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 10]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.PRECIOCOMPRA = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 11]).Text == "")
        //                        {
        //                            p.DSCTOUNO = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTOUNO = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 11]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTOUNO = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 13]).Text == "")
        //                        {
        //                            p.DSCTODOS = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTODOS = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 13]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTODOS = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 15]).Text == "")
        //                        {
        //                            p.DSCTOTRES = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTOTRES = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 15]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTOTRES = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 17]).Text == "")
        //                        {
        //                            p.DSCTOCUATRO = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.DSCTOCUATRO = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 17]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.DSCTOCUATRO = 0;
        //                            }
        //                        }

        //                        if (((Excelold.Range)range.Cells[row, 19]).Text == "")
        //                        {
        //                            p.PRECIOCOMPRAFINAL = 0;
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                p.PRECIOCOMPRAFINAL = Convert.ToDecimal(((Excelold.Range)range.Cells[row, 19]).Text);
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                Console.WriteLine(e);
        //                                p.PRECIOCOMPRAFINAL = 0;
        //                            }
        //                        }



        //                        if (p.IDPROVEEDOR == 0)
        //                        {

        //                        }
        //                        else
        //                        {
        //                            lproductoimport.Add(p);
        //                        }
        //                    }

        //                }
        //                workbook.Close();
        //                applicatio.Quit();

        //                if (lproductoimport.Count > 0)
        //                {
        //                    int resultado = await RegistraProductosAsync(lproductoimport);
        //                    return Json(resultado, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    return Json("0", JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return Json(ex, JsonRequestBehavior.AllowGet);
        //            }
        //            finally
        //            {
        //                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
        //                if (workbook != null) Marshal.ReleaseComObject(workbook);
        //                if (applicatio != null) Marshal.ReleaseComObject(applicatio);
        //            }
        //        }
        //        else
        //        {
        //            return Json("0", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json("0", JsonRequestBehavior.AllowGet);
        //    }
        //}

        private async Task<int> RegistraProductosAsync(List<ProductoImport> lproductoimport)
        {
            int contador = 0;
            foreach (var item in lproductoimport)
            {
                Producto _entidad = new Producto();
                _entidad.id_producto = 0;
                //_entidad.fk_proveedor = item.IDPROVEEDOR;
                _entidad.fk_producto_marca = item.IDMARCA;
                //_entidad.fk_producto_subfamilia = item.IDSUBFAMILIA;
                string partone = await GetProductoSubFamiliaCodigoAsync(item.IDSUBFAMILIA);
                int maxid = 0;
                maxid = maxid + 1;
                _entidad.cod_producto = partone + "." + maxid.ToString().PadLeft(4, '0');
                //_entidad.fk_tipo_moneda = item.IDTIPOMONEDA;
                _entidad.nom_producto = item.PRODUCTO;
                _entidad.codigo_sku = item.SKU;
                //_entidad.image_url = "ftp://localhost//productodefault.png";
                //_entidad.embalaje = item.EMPAQUE;
                try
                {
                    decimal precioi = Convert.ToDecimal(item.PRECIOCOMPRA);
                    _entidad.preciocompra = precioi;
                }
                catch (Exception ex)
                {
                    _entidad.preciocompra = 0;
                }
                try
                {
                    decimal preciomi = Convert.ToDecimal(item.PRECIOCOMPRAFINAL);
                    _entidad.preciocomprafinal = preciomi;
                }
                catch (Exception ex)
                {
                    _entidad.preciocomprafinal = 0;
                }
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
                    metodo = "Producto/agregar";
                    var response = await client.PostAsJsonAsync(metodo, _entidad);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = respuesta.Result.ToString();
                        int precioinserted = await InsertPrecioCompra(idinserted, "0", item.PRECIOCOMPRA.ToString(), item.DSCTOUNO.ToString(), item.DSCTODOS.ToString(), item.DSCTOTRES.ToString(), item.DSCTOCUATRO.ToString(), item.PRECIOCOMPRAFINAL.ToString());
                        int precioventainserted = await InsertPrecioVenta(idinserted, precioinserted, item.DPPVUNIDAD,
                            item.DPPVMAYOR, item.DPPVESPECIAL, item.DPVUNIDAD, item.DPVMAYOR, item.DPVESPECIAL, item.DTIPOCAMBIO);
                        int stockinserted = await InsertStockInicial(item.DIDALMACEN, idinserted, item.DSTOCKINICIAL);
                        contador++;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return contador;
        }

        private async Task<int> InsertStockInicial(int dIDALMACEN, string idinserted, decimal dSTOCKINICIAL)
        {
            int entidad = 0;
            AlmacenStock almacen_stock = new AlmacenStock()
            {
                fk_almacen = dIDALMACEN,
                fk_producto = Convert.ToInt32(idinserted),
                existencia = dSTOCKINICIAL,
                pto_limite = 10
            };
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
                    entidad = lstEntidad[0].id_almacen_stock;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return entidad;
        }

        private async Task<int> TraeSubFamiliaNombreAsync(int iDFAMILIA, dynamic text)
        {
            try
            {
                List<ProductoSubFamilia> lproductosubfamilia = null;
                ProductoSubFamilia _productosubfamilia = new ProductoSubFamilia { fk_producto_familia = iDFAMILIA, descripcion = text };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ProductoSubFamilia/subfamilia", _productosubfamilia);
                var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
                if (model.Result.Count > 0)
                {
                    lproductosubfamilia = model.Result.ToList();
                    return lproductosubfamilia.FirstOrDefault().id_producto_subfamilia;
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

        private async Task<int> TraeProveedorRazonAsync(dynamic text)
        {
            try
            {
                string proveedorn = "";
                if (text != "")
                {
                    proveedorn = text;
                }
                List<Proveedor> lentidad = null;
                Proveedor idalmac = new Proveedor { razon_social = proveedorn };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Proveedor/razon", idalmac);
                var model = response.Content.ReadAsAsync<List<Proveedor>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                    return lentidad.FirstOrDefault().id_proveedor;
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

        private async Task<int> ProductoExistAsync(dynamic text)
        {
            try
            {
                List<Product> lentidad = null;
                Product produc = new Product { nom_producto = text };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Producto/exist", produc);
                var model = response.Content.ReadAsAsync<List<Product>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                    return lentidad.FirstOrDefault().id_producto;
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
        public async Task<JsonResult> Elimina(string id_save)
        {
            List<Producto> _entidad = new List<Producto>();
            _entidad = await GetProductoIdAsync(Convert.ToInt32(id_save));
            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    Producto id = new Producto { id_producto = _entidad[0].id_producto };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Producto/eliminar";
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

        public async Task<List<TipoMoneda>> GetTipoMonedaAsync()
        {
            List<TipoMoneda> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/alltipomoneda");
            var model = response.Content.ReadAsAsync<List<TipoMoneda>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<TipoMoneda>();
            }
            return lentidad;
        }
        public async Task<ActionResult> GeneracionPrecioVentaMasiva()
        {
            List<Producto> _lentidad = new List<Producto>();
            List<ProductoFamilia> _lproductofamilia = new List<ProductoFamilia>();
            _lproductofamilia = await GetProductoFamiliaAsync();
            ProductoFamilia newadd = new ProductoFamilia
            {
                id_producto_familia = -1,
                descripcion = ""
            };
            _lproductofamilia.Add(newadd);
            ViewBag.ProductoFamilia = _lproductofamilia.OrderBy(x => x.id_producto_familia).ToList();

            List<ProductoMarca> _lProductoMarca = new List<ProductoMarca>();
            _lProductoMarca = await GetProductoMarcaExistAsync();
            ProductoMarca newaddm = new ProductoMarca
            {
                id_producto_marca = -1,
                descripcion = ""
            };
            _lProductoMarca.Add(newaddm);
            ViewBag.ProductoMarca = _lProductoMarca.OrderBy(x => x.id_producto_marca).ToList();


            List<TipoMoneda> _lentidadmoneda = new List<TipoMoneda>();

            _lentidadmoneda = await GetTipoMonedaAsync();
            ViewBag.TipoMoneda = _lentidadmoneda;
            return PartialView();
        }

        private async Task<List<ProductoMarca>> GetProductoMarcaExistAsync()
        {
            List<ProductoMarca> lproductomarca = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("ProductoMarca/marcasexist");
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

        public ActionResult UploadCustom()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ReadExcel()
        {
            List<CabeceraExcel> lsexcel = new List<CabeceraExcel>();
            if (ModelState.IsValid)
            {

                string filePath = string.Empty;
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["file"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {

                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        filePath = path + Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        file.SaveAs(filePath);
                        Stream stream = file.InputStream;
                        // We return the interface, so that
                        IExcelDataReader reader = null;
                        if (file.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx"))
                        {
                            try
                            {
                                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            }
                            catch (Exception ex)
                            {
                                string xxx = "";
                                xxx = ex.Message;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            return RedirectToAction("Index");
                        }
                        reader.IsFirstRowAsColumnNames = true;
                        DataSet result = reader.AsDataSet();
                        reader.Close();
                        //delete the file from physical path after reading 
                        string filedetails = path + fileName;
                        FileInfo fileinfo = new FileInfo(filedetails);
                        if (fileinfo.Exists)
                        {
                            fileinfo.Delete();
                        }
                        DataTable dt = result.Tables[0];
                        lsexcel = ConvertDataTable<CabeceraExcel>(dt);
                        TempData["Excelproducto"] = lsexcel;
                    }
                }

            }
            return new JsonResult { Data = lsexcel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<ActionResult> InsertExcelData()
        {
            int length = 0;
            try
            {
                if (TempData["Excelproducto"] != null)
                {
                    List<CabeceraExcel> lstProducts = (List<CabeceraExcel>)TempData["Excelproducto"];
                    List<ProductoImport> lproductoimport = new List<ProductoImport>();
                    foreach (var item in lstProducts)
                    {
                        ProductoImport p = new ProductoImport();
                        int idproductexist = await ProductoExistAsync(item.PRODUCTO);
                        if (idproductexist <= 0)
                        {

                            p.PROVEEDOR = item.PROVEEDOR;

                            p.IDPROVEEDOR = await TraeProveedorRazonAsync(item.PROVEEDOR);
                            p.MARCA = item.MARCA;
                            p.IDMARCA = await TraeMarcaNombreAsync(item.MARCA);
                            if (p.IDMARCA == 0)
                            {
                                p.IDMARCA = await GuardaProductoMarcaImport(item.MARCA);
                            }
                            p.FAMILIA = item.FAMILIA;
                            p.IDFAMILIA = await TraeFamiliaNombreAsync(item.FAMILIA);
                            if (p.IDFAMILIA == 0)
                            {
                                string familiadescr = item.FAMILIA;
                                string codigofamili = familiadescr.Substring(0, 2);
                                p.IDFAMILIA = await GuardaProductoFamiliaImport(codigofamili, familiadescr);
                            }
                            p.SUBFAMILIA = item.SUBFAMILIA;
                            p.IDSUBFAMILIA =
                                await TraeSubFamiliaNombreAsync(p.IDFAMILIA, item.SUBFAMILIA);
                            if (p.IDSUBFAMILIA == 0)
                            {
                                p.IDSUBFAMILIA = await GuardaProductoSubFamiliaImport(p.IDFAMILIA, "", p.SUBFAMILIA);
                            }
                            p.SKU = item.SKU;

                            //CHECK IF EXIST WITH THE NAME

                            //CHECK IF EXIST WITH THE NAME
                            p.PRODUCTO = item.PRODUCTO;
                            p.UND = item.UND;
                            p.EMPAQUE = item.EMPAQUE.ToString();
                            p.IDTIPOMONEDA = Convert.ToInt32(item.IDMONEDA);

                            //PRECIO COMPRA
                            if (item.PRECIOCOMPRA.ToString() == "")
                            {
                                p.PRECIOCOMPRA = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.PRECIOCOMPRA = Math.Round(Convert.ToDecimal(item.PRECIOCOMPRA), 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.PRECIOCOMPRA = 0;
                                }
                            }

                            if (item.DPRIMERO.ToString() == "")
                            {
                                p.DSCTOUNO = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DSCTOUNO = Math.Round(Convert.ToDecimal(item.DPRIMERO), 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DSCTOUNO = 0;
                                }
                            }

                            if (item.DSEGUNDO.ToString() == "")
                            {
                                p.DSCTODOS = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DSCTODOS = Math.Round(Convert.ToDecimal(item.DSEGUNDO), 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DSCTODOS = 0;
                                }
                            }

                            if (item.DTERCER.ToString() == "")
                            {
                                p.DSCTOTRES = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DSCTOTRES = Math.Round(Convert.ToDecimal(item.DTERCER), 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DSCTOTRES = 0;
                                }
                            }

                            if (item.DCUARTO.ToString() == "")
                            {
                                p.DSCTOCUATRO = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DSCTOCUATRO = Math.Round(Convert.ToDecimal(item.DCUARTO), 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DSCTOCUATRO = 0;
                                }
                            }

                            if (item.PRECIOFINAL.ToString() == "")
                            {
                                p.PRECIOCOMPRAFINAL = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.PRECIOCOMPRAFINAL = Math.Round(Convert.ToDecimal(item.PRECIOFINAL), 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.PRECIOCOMPRAFINAL = 0;
                                }
                            }

                            #region PRECIOVENTA
                            if (item.TIPOCAMBIO.ToString() == "")
                            {
                                p.DTIPOCAMBIO = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DTIPOCAMBIO = Convert.ToDecimal(item.TIPOCAMBIO);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DTIPOCAMBIO = 0;
                                }
                            }

                            if (item.PPVUNIDAD.ToString() == "")
                            {
                                p.DPPVUNIDAD = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DPPVUNIDAD = Convert.ToDecimal(item.PPVUNIDAD);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DTIPOCAMBIO = 0;
                                }
                            }
                            if (item.PVUNIDAD.ToString() == "")
                            {
                                p.DTIPOCAMBIO = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DPVUNIDAD = Convert.ToDecimal(item.PVUNIDAD);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DPVUNIDAD = 0;
                                }
                            }

                            if (item.PPVMAYOR.ToString() == "")
                            {
                                p.DPPVMAYOR = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DPPVMAYOR = Convert.ToDecimal(item.PPVMAYOR);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DPPVMAYOR = 0;
                                }
                            }

                            if (item.PVMAYOR.ToString() == "")
                            {
                                p.DPVMAYOR = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DPVMAYOR = Convert.ToDecimal(item.PVMAYOR);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DPVMAYOR = 0;
                                }
                            }

                            if (item.PPVESPECIAL.ToString() == "")
                            {
                                p.DPPVESPECIAL = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DPPVESPECIAL = Convert.ToDecimal(item.PPVESPECIAL);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DPPVESPECIAL = 0;
                                }
                            }

                            if (item.PVESPECIAL.ToString() == "")
                            {
                                p.DPVESPECIAL = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DPVESPECIAL = Convert.ToDecimal(item.PVESPECIAL);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DPVESPECIAL = 0;
                                }
                            }

                            if (item.STOCKINICIAL.ToString() == "")
                            {
                                p.DSTOCKINICIAL = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DSTOCKINICIAL = Convert.ToDecimal(item.STOCKINICIAL);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DSTOCKINICIAL = 0;
                                }
                            }

                            if (item.IDALMACEN.ToString() == "")
                            {
                                p.DIDALMACEN = 0;
                            }
                            else
                            {
                                try
                                {
                                    p.DIDALMACEN = Convert.ToInt32(item.IDALMACEN);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    p.DIDALMACEN = 1;
                                }
                            }
                            #endregion

                            if (p.IDPROVEEDOR == 0)
                            {

                            }
                            else
                            {
                                lproductoimport.Add(p);
                            }


                        }
                    }

                    if (lproductoimport.Count > 0)
                    {
                        int resultado = await RegistraProductosAsync(lproductoimport);
                        length = resultado;
                    }
                    else
                    {
                        length = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new JsonResult { Data = length, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        T item = GetItem<T>(row);
                        data.Add(item);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                data = new List<T>();
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}