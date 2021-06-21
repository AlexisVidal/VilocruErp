using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Admin.App_Helper;
using ERP.Admin.App_Start;
using ERP.Admin.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class ComprobanteVentaController : Controller
    {

        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        public async Task<ComprobanteVenta> InsertComprobanteVentaTemp(ComprobanteVenta venta_detalle)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/agregartemp", venta_detalle);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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
        public async Task<ComprobanteVenta> InsertComprobanteVenta(ComprobanteVenta venta_detalle)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/agregar", venta_detalle);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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

        public async Task<ComprobanteVenta> DeleteComprobanteVentaId(int IdCompVent)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                var client = new HttpClient();
                ComprobanteVenta parametro = new ComprobanteVenta { id_comprobante_venta = IdCompVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/eliminarid", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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
        
        public async Task<ComprobanteVenta> DeleteComprobanteVenta(int IdCompVent)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                var client = new HttpClient();
                ComprobanteVenta parametro = new ComprobanteVenta { id_comprobante_venta = IdCompVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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

        public async Task<ComprobanteVenta> UpdateEstadoComprobanteVenta(ComprobanteVenta comprobante_venta)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/UpdateEstado", comprobante_venta);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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
        public async Task<ComprobanteVenta> GetComprobanteVentaTempById(int IdCompVent)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                ComprobanteVenta parametro = new ComprobanteVenta { id_comprobante_venta = IdCompVent };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVenta/buscartemp", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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
        public async Task<ComprobanteVenta> GetComprobanteVentaById(int IdCompVent)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                ComprobanteVenta parametro = new ComprobanteVenta { id_comprobante_venta = IdCompVent };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVenta/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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

        public async Task<List<ComprobanteVenta>> GetComprobanteVentaByMedioPago(int FkMediPago)
        {
            List<ComprobanteVenta> lstEntidad = null;
            try
            {
                ComprobanteVenta parametro = new ComprobanteVenta { fk_medio_pago = FkMediPago };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVenta/ByMedioPago", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<ComprobanteVenta>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<ComprobanteVentaCobro>> GetComprobanteVentaByCaja(int FkCaja)
        {
            List<ComprobanteVentaCobro> lstEntidad = null;
            try
            {
                ComprobanteVentaCobro parametro = new ComprobanteVentaCobro { fk_caja = FkCaja };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVenta/ByCaja", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVentaCobro>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<ComprobanteVentaCobro>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<ComprobanteVenta>> GetComprobanteVentaAll()
        {
            List<ComprobanteVenta> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteVenta/all");
                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ComprobanteVenta>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<Parametro>> GetParametroAll()
        {
            List<Parametro> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Parametro/all");
                var model = response.Content.ReadAsAsync<List<Parametro>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Parametro>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public async Task<List<Cliente>> GetClientesByVenta()
        {
            List<Cliente> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteVenta/ClienteByVenta");
                var model = response.Content.ReadAsAsync<List<Cliente>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Cliente>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> ListIndex(string EstaFilt, string CallBy)
        {
            List<Caja> lstCaja = new List<Caja>();
            List<ComprobanteVenta> lstEntidad = null;
            lstEntidad = await GetComprobanteVentaAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            CajaController CtrlCaja = new CajaController();
            //Verificamos si existe caja aperturada
            lstCaja = await CtrlCaja.GetCajaAll();
            if (lstCaja != null)
            {
                lstCaja = lstCaja.Where(i => i.estado.Equals("1")).ToList();
                if (lstCaja.Count == 0)
                {
                    ViewBag.Caja = 0;
                }
                else
                {
                    int idcaja = lstCaja.Where(z => z.estado.Equals("1")).Select(x => x.id_caja).FirstOrDefault();
                    if (idcaja > 0 && lstEntidad != null)
                    {
                        lstEntidad = lstEntidad.Where(i => i.fk_caja == idcaja).ToList();
                    }

                    ViewBag.Caja = 1;
                }
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        // GET: ComprobanteVenta
        public async Task<ActionResult> Index()
        {
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            List<ComprobanteTipo> lstComprobanteTipo = null;
            lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
            if (lstComprobanteTipo != null)
            {
                lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0")).OrderBy(x => x.descripcion).ToList();
            }
            lstComprobanteTipo.Add(new ComprobanteTipo { id_comprobante_tipo = 0, descripcion = "", codigo="00" });
            lstComprobanteTipo = lstComprobanteTipo.Where(j=> j.codigo.Equals("00") || j.codigo.Equals("01") || j.codigo.Equals("02") || j.codigo.Equals("03") || j.codigo.Equals("04")).OrderBy(i => i.descripcion).ToList();
            ViewBag.TiposFilter = lstComprobanteTipo.ToList();


            List<ComprobanteVenta> lEstados = new List<ComprobanteVenta>();
            lEstados.Add(new ComprobanteVenta { estado = "-1", NEstado = "TODOS" });
            lEstados.Add(new ComprobanteVenta { estado = "0", NEstado = "ANULADO" });
            lEstados.Add(new ComprobanteVenta { estado = "1", NEstado = "VIGENTE" });

            ViewBag.EstadosFilter = lEstados.ToList();
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        public ActionResult IndexPorRegularizar()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        public async Task<JsonResult> GetJson_Porcentaje_ByDetraxionTipo(int FkDetraxTipo)
        {
            DetraccionTipo entidad = new DetraccionTipo();
            int IntCorr = 0;
            string strCorr = "";
            string msgReturn = "";
            try
            {
                entidad = await GetPorcentajeByDetrax(FkDetraxTipo);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(entidad);
        }

        public async Task<DetraccionTipo> GetPorcentajeByDetrax(int FkDetraxTipo)
        {
            DetraccionTipo entidad = null;
            try
            {
                List<DetraccionTipo> lstEntidad = null;

                var client = new HttpClient();
                DetraccionTipo parametro = new DetraccionTipo { id_detraccion_tipo = FkDetraxTipo };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/t_detraccion_tipoById", parametro);

                var model = response.Content.ReadAsAsync<List<DetraccionTipo>>();
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

        public async Task<ActionResult> Create()
        {
            string msgReturn = "";
            int FkClieGene = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FkClieGenerico"]);
            try
            {
                ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
                AlmacenController CtrlAlma = new AlmacenController();
                CajaController CtrlCaja = new CajaController();

                ComercialController CtrlComer = new ComercialController();
                List<DetraccionTipo> lsDetraccionTipo = null;
                lsDetraccionTipo = await CtrlComer.GetDetraccionTipos();
                if (lsDetraccionTipo != null)
                {
                    lsDetraccionTipo = lsDetraccionTipo.Where(i => i.estado_detraccion.Equals("1")).OrderBy(j=>j.detraccion_tipo).ToList();
                }



                MedioPagoController CtrlMediPago = new MedioPagoController();
                BancoController CtrlBanc = new BancoController();
                TarjetaCreditoController CtrlTarjCred = new TarjetaCreditoController();
                CorrelativoController CtrlCorr = new CorrelativoController();

                List<ComprobanteTipo> lstComprobanteTipo = null;
                List<Almacen> lstAlmacen = null;
                List<Caja> lstCaja = new List<Caja>();
                Caja caja = null;
                List<MedioPago> lstMedioPago = null;
                List<Banco> lstBanco = null;
                List<TarjetaCredito> lsTarjetaCredito = null;

                int FkAlmaPrinc = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FkAlmaPrinc"]);

                int FkMediPagoEfec = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Efectivo"]);
                int FkMediPagoCheq = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Cheque"]);
                int FkMediPagoDepo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Deposito"]);
                int FkMediPagoTarj = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Tarjeta"]);
                int FkMediPagoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_NotaCredito"]);
                int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);

                //Verificamos si existe caja aperturada
                lstCaja = await CtrlCaja.GetCajaAll();
                if (lstCaja != null)
                {
                    lstCaja = lstCaja.Where(i => i.estado.Equals("1")).ToList();
                    if (lstCaja.Count == 0)
                    {
                        msgReturn = "No existe Caja apertura, Seleccione la Opción Caja, luego aperturar";
                        //Response.StatusCode = 500;
                        return Json(msgReturn, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        caja = lstCaja[0];
                    }
                }
                List<MonedaErp> _lmoneda = new List<MonedaErp>();
                _lmoneda = await CtrlComer.GetMonedaErp();
                ViewBag.Monedas = _lmoneda.Where(y => y.ESTADO.Equals("1")).OrderBy(x => x.IDMONEDA).ToList();


                lstMedioPago = await CtrlMediPago.GetMedioPagoAll();

                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
                if (lstComprobanteTipo != null)
                {
                    lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0") && (i.codigo.Equals("01") || i.codigo.Equals("02"))).OrderBy(x => x.descripcion).ToList();
                }
                lstComprobanteTipo.Add(new ComprobanteTipo { id_comprobante_tipo = 0, descripcion = "" });
                lstComprobanteTipo = lstComprobanteTipo.OrderBy(i => i.descripcion).ToList();
                lstAlmacen = await CtrlAlma.GetAlmacenAll();
                if (lstAlmacen != null)
                {
                    lstAlmacen = lstAlmacen.Where(i => !i.estado.Equals("0")).ToList();
                    Almacen almacen = new Almacen
                    {
                        id_almacen = -1,
                        nombre = "",
                    };
                    lstAlmacen.Add(almacen);
                    lstAlmacen = lstAlmacen.OrderBy(i => i.id_almacen).ToList();
                }
                //Banco
                lstBanco = await CtrlBanc.GetBancoAll();
                if (lstBanco == null)
                {
                    lstBanco = new List<Banco>();
                }
                lstBanco.Add(new Banco { id_banco = 0, descripcion = " ", estado = "1" });
                lstBanco = lstBanco.OrderBy(i => i.id_banco).ToList();
                //Tarjeta
                lsTarjetaCredito = await CtrlTarjCred.GetTarjetaCreditoAll();
                if (lsTarjetaCredito == null)
                {
                    lsTarjetaCredito = new List<TarjetaCredito>();
                }
                lsTarjetaCredito.Add(new TarjetaCredito { id_tarjeta_credito = 0, descripcion = " ", estado = "1" });
                lsTarjetaCredito = lsTarjetaCredito.OrderBy(i => i.id_tarjeta_credito).ToList();


                List<TipoAfectacion> _ligv = new List<TipoAfectacion>();
                _ligv = await CtrlComer.GetTipoAfectacionErp();
                ViewBag.TipoIgv = _ligv.Where(y => y.estado.Equals("1")).OrderBy(x => x.id_tipo_afectacion_igv).ToList();


                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.Almacen = lstAlmacen;
                ViewBag.FkClienteGenerico = FkClieGene;
                ViewBag.MedioPago = lstMedioPago;
                ViewBag.MedioPagoEfectivo = FkMediPagoEfec;
                ViewBag.MedioPagoCheque = FkMediPagoCheq;
                ViewBag.MedioPagoDeposito = FkMediPagoDepo;
                ViewBag.MedioPagoTarjeta = FkMediPagoTarj;
                ViewBag.MedioPagoNotaCredito = FkMediPagoNotaCred;
                ViewBag.MedioPagoLineaCredito = FkMediPagoLineCred;
                ViewBag.Caja = caja;
                ViewBag.Banco = lstBanco;
                ViewBag.TarjetaCredito = lsTarjetaCredito;
                ViewBag.FkAlmacenPrincipal = FkAlmaPrinc;
                ViewBag.DetraccionesTipo = lsDetraccionTipo;


            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        public async Task<ActionResult> ViewDistribuirCantidad(int FkProd, string CallBy)
        {
            string msgReturn = "";
            try
            {
                AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
                List<AlmacenStock> lstAlmacenStock = null;

                lstAlmacenStock = await CtrlAlmaStoc.GetAlmacenStockProducto(FkProd);
                ViewBag.AlmacenStock = lstAlmacenStock;
                ViewBag.CallByDistCant = CallBy;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        public async Task<ActionResult> Edit(int IdCompVent, string CallBy)
        {
            string msgReturn = "";
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            VentaController CtrlVent = new VentaController();
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            AlmacenController CtrlAlma = new AlmacenController();
            MovimientoController CtrlMovi = new MovimientoController();
            CajaController CtrlCaja = new CajaController();
            MedioPagoController CtrlMediPago = new MedioPagoController();
            BancoController CtrlBanc = new BancoController();
            TarjetaCreditoController CtrlTarjCred = new TarjetaCreditoController();

            List<Almacen> lstAlmacen = null;
            ComprobanteVenta comprobante_venta = null;
            Venta venta = null;
            List<VentaDetalle> lstVentaDetalle = new List<VentaDetalle>();
            List<ComprobanteTipo> lstComprobanteTipo = null;
            List<Movimiento> lstMovimiento = null;
            List<Movimiento> lstTempMovimiento = null;
            List<Caja> lstCaja = new List<Caja>();
            Caja caja = null;
            List<MedioPago> lstMedioPago = null;
            List<Banco> lstBanco = null;
            List<TarjetaCredito> lsTarjetaCredito = null;

            int FkAlmaBusq = 0;
            int flgUnicoAlma = 1;
            string strDist = "";
            int FkAlmaPrinc = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FkAlmaPrinc"]);

            int FkClieGene = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FkClieGenerico"]);
            int FkMediPagoEfec = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Efectivo"]);
            int FkMediPagoCheq = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Cheque"]);
            int FkMediPagoDepo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Deposito"]);
            int FkMediPagoTarj = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Tarjeta"]);
            int FkMediPagoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_NotaCredito"]);
            int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);
            try
            {
                //Verificamos si existe caja aperturada
                lstCaja = await CtrlCaja.GetCajaAll();
                if (lstCaja != null)
                {
                    lstCaja = lstCaja.Where(i => i.estado.Equals("1")).ToList();
                    if (lstCaja.Count == 0)
                    {
                        msgReturn = "No existe Caja apertura, Seleccione la Opción Caja, luego aperturar";
                        Response.StatusCode = 500;
                        return Json(msgReturn, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        caja = lstCaja[0]; 
                    }
                }

                lstMedioPago = await CtrlMediPago.GetMedioPagoAll();

                comprobante_venta = await GetComprobanteVentaById(IdCompVent);
                if (comprobante_venta != null)
                {
                    venta = await CtrlVent.GetVentaById(comprobante_venta.fk_venta);
                    if (venta != null)
                    {
                        lstVentaDetalle = await CtrlVentDeta.GetVentaDetalleAll();
                        if (lstVentaDetalle != null)
                        {
                            lstVentaDetalle = lstVentaDetalle.Where(i => i.fk_venta.Equals(venta.id_venta) && !i.estado_venta.Equals("0")).ToList();
                            if (comprobante_venta.estado.Equals("1"))
                            {
                                //Extraemos los almacenes y las cantidades de donde se despacho
                                lstMovimiento = await CtrlMovi.Get_MovimientoByVenta(comprobante_venta.fk_venta);
                                if (lstMovimiento != null)
                                {
                                    for (int i = 0; i < lstVentaDetalle.Count; i++)
                                    {
                                        strDist = "";
                                        lstTempMovimiento = lstMovimiento.Where(j => j.fk_venta_detalle.Equals(lstVentaDetalle[i].id_venta_detalle)).ToList();
                                        foreach (var oneMovi in lstTempMovimiento)
                                        {
                                            if (FkAlmaBusq.Equals(0))
                                            {
                                                FkAlmaBusq = oneMovi.fk_almacen;
                                            }
                                            else
                                            {
                                                if (FkAlmaBusq != oneMovi.fk_almacen)
                                                {
                                                    flgUnicoAlma = 0;
                                                }
                                            }
                                            strDist = strDist + oneMovi.fk_almacen + "[" + oneMovi.cantidad + ",";
                                        }
                                        lstVentaDetalle[i].str_distribuir = strDist;
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    msgReturn = "Comprobante no encontrado";
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
                if (lstComprobanteTipo != null)
                {
                    lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0")).ToList();
                }
                lstAlmacen = await CtrlAlma.GetAlmacenAll();
                if (lstAlmacen != null)
                {
                    lstAlmacen = lstAlmacen.Where(i => !i.estado.Equals("0")).ToList();
                    Almacen almacen = new Almacen
                    {
                        id_almacen = -1,
                        nombre = "",
                    };
                    lstAlmacen.Add(almacen);
                    lstAlmacen = lstAlmacen.OrderBy(i => i.id_almacen).ToList();
                }

                if (flgUnicoAlma == 0)
                {
                    FkAlmaBusq = 0;
                }

                //Banco
                lstBanco = await CtrlBanc.GetBancoAll();
                if (lstBanco == null)
                {
                    lstBanco = new List<Banco>();
                }
                lstBanco.Add(new Banco { id_banco = 0, descripcion = " ", estado = "1" });
                lstBanco = lstBanco.OrderBy(i => i.id_banco).ToList();
                //Tarjeta
                lsTarjetaCredito = await CtrlTarjCred.GetTarjetaCreditoAll();
                if (lsTarjetaCredito == null)
                {
                    lsTarjetaCredito = new List<TarjetaCredito>();
                }
                lsTarjetaCredito.Add(new TarjetaCredito { id_tarjeta_credito = 0, descripcion = " ", estado = "1" });
                lsTarjetaCredito = lsTarjetaCredito.OrderBy(i => i.id_tarjeta_credito).ToList();

                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.Venta = venta;
                ViewBag.VentaDetalle = lstVentaDetalle;
                ViewBag.Almacen = lstAlmacen;
                ViewBag.FkAlmacenDespacho = FkAlmaPrinc;
                ViewBag.CallBy = CallBy;
                ViewBag.FkClienteGenerico = FkClieGene;
                ViewBag.MedioPago = lstMedioPago;
                ViewBag.MedioPagoEfectivo = FkMediPagoEfec;
                ViewBag.MedioPagoCheque = FkMediPagoCheq;
                ViewBag.MedioPagoDeposito = FkMediPagoDepo;
                ViewBag.MedioPagoTarjeta = FkMediPagoTarj;
                ViewBag.MedioPagoNotaCredito = FkMediPagoNotaCred;
                ViewBag.MedioPagoLineaCredito = FkMediPagoLineCred;
                ViewBag.Caja = caja;
                ViewBag.Banco = lstBanco;
                ViewBag.TarjetaCredito = lsTarjetaCredito;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(comprobante_venta);
        }
        public async Task<ActionResult> SaveNewComprobanteVentaTemp(int FkUsuaVent, int FkClie, int FkPedi,
            int FkCompTipo, int FkCaja, string FechEmis, string NroCompVent, decimal CVTotaNeto,
            decimal CVTotaIgv, decimal CVTotaBrut, string EstaVent, int FkAlma, int FlgSaveNewCliente,
            string ClieDni, string ClieNomb, string ClieDire, string ClieEmail, List<List<string>> arrVentDeta,
            List<List<string>> arrCompVentMediPago, List<string> arraCVIdPedi, string EfecReci, string Vuel,
            decimal detraccion, int FkDetraccion, decimal PorcDetrax, string FkMoneda, string FechVenc, string TxtReferencia, string Observacion,
            decimal TotalExonerado, decimal TotalInafecto, decimal TotalGratuito, decimal TipoCambio)
        {
            bool check_detrax = false;
            if (FkDetraccion > 0)
            {
                check_detrax = true;
            }
            decimal efectivo = 0;
            decimal vuelto = 0;
            try
            {
                if (EfecReci == "")
                {

                }
                else
                {
                    efectivo = Convert.ToDecimal(EfecReci);
                }
            }
            catch (Exception ex)
            {
                efectivo = 0;
            }

            try
            {
                if (Vuel == "")
                {

                }
                else
                {
                    vuelto = Convert.ToDecimal(Vuel);
                }
            }
            catch (Exception ex)
            {
                vuelto = 0;
            }

            string msgReturn = "";
            string DataQr = "";
            int IdNewVent = 0;
            int IdCompVent = 0;
            DateTime NewFechEmis = DateTime.Now;
            DateTime NewFechVenc = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "La vista previa se registró satisfactoriamente";
            try
            {
                VentaController CtrlVent = new VentaController();
                //VentaDetalleController CtrlVentDeta = new VentaDetalleController();
                ComprobanteVentaCobroController CtrlCompVentCobr = new ComprobanteVentaCobroController();
                VentaPedidoController CtrlVentPedi = new VentaPedidoController();
                Venta ventaReturn = null;
                VentaPedido VentaPedidoReturn = null;
                ComprobanteVenta comprobanteVentaReturn = null;
                ComprobanteVenta comprobante_venta = null;
                ComprobanteVentaCobro comprobante_venta_cobro = null;
                ComprobanteVentaCobro compVentCobrReturn = null;

                NewFechEmis = Convert.ToDateTime(FechEmis);
                NewFechVenc = Convert.ToDateTime(FechVenc);
                // int FkUsuaPago = Convert.ToInt32(Session["IdUsuario"]);
                string FkUsuaPago = Session["IdUsuario"].ToString();
                //if (FlgSaveNewCliente.Equals(1))
                //{
                //    FkClie = await SaveNewClienteNatural(ClieDni, ClieNomb, ClieDire, ClieEmail);
                //}
                Venta venta = new Venta
                {
                    fk_empresa = FkClie,
                    f_venta = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss")
                };
                ventaReturn = await CtrlVent.InsertVentaTemp(venta);
                if (ventaReturn != null)
                {
                    IdNewVent = ventaReturn.id_venta;
                    

                    if (arrVentDeta != null)
                    {
                        flgReturnSave = await SaveListVentaDetalleTemp(IdNewVent, FkAlma, EstaVent, arrVentDeta);
                        if (flgReturnSave == 0)
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                    if (flgReturnSave == 1)
                    {
                        //Guardamos el comprobante
                        comprobante_venta = new ComprobanteVenta
                        {
                            fk_comprobante_tipo = FkCompTipo,
                            fk_venta = IdNewVent,
                            fk_caja = FkCaja,
                            //fk_medio_pago = FkMediPago,
                            nro_comprobante = NroCompVent,
                            total_bruto = CVTotaBrut,
                            total_igv = CVTotaIgv,
                            total_isc = 0,
                            total_neto = CVTotaNeto,
                            //saldo_bruto = CVSaldBrut,
                            estado = EstaVent,
                            //fk_tarjeta_credito = FkTarjCred,
                            //fk_nota_credito = FkNotaCred,
                            //fk_banco = FkBanc,
                            //nro_documento = NroDocuPago
                            total_impuestos_bolsas = 0,
                            efectivo_recibido = efectivo,
                            vuelto = vuelto,
                            check_detraccion = check_detrax,
                            detraccion = detraccion,
                            fk_detraccion_tipo = FkDetraccion,
                            direccion = ClieDire,
                            fk_moneda = FkMoneda,
                            f_vencimiento = NewFechVenc.ToString("yyyy/MM/dd H:mm:ss"),
                            f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                            referencia = TxtReferencia,
                            observacion_full = Observacion,
                            total_exonerado = TotalExonerado,
                            total_gratuito = TotalGratuito,
                            total_inafecto = TotalInafecto,
                            tipo_cambio = TipoCambio
                        };
                        comprobanteVentaReturn = await InsertComprobanteVentaTemp(comprobante_venta);
                        if (comprobanteVentaReturn == null)
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                        else
                        {
                            IdCompVent = comprobanteVentaReturn.id_comprobante_venta;
                        }
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return Json(msgReturn, JsonRequestBehavior.AllowGet);
            return Json(new { msgRpta = msgReturn, IdNewVent = IdNewVent, IdCompVent = IdCompVent, DataQr = DataQr }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SaveNewComprobanteVenta(int FkUsuaVent, int FkClie, int FkPedi,
            int FkCompTipo, int FkCaja, string FechEmis, string NroCompVent, decimal CVTotaNeto,
            decimal CVTotaIgv, decimal CVTotaBrut, string EstaVent, int FkAlma, int FlgSaveNewCliente,
            string ClieDni, string ClieNomb, string ClieDire, string ClieEmail, List<List<string>> arrVentDeta,
            List<List<string>> arrCompVentMediPago, List<string> arraCVIdPedi, string EfecReci, string Vuel, 
            decimal detraccion, int FkDetraccion, decimal PorcDetrax, string FkMoneda, string FechVenc, string TxtReferencia, string Observacion,
            decimal TotalExonerado, decimal TotalInafecto, decimal TotalGratuito, decimal TipoCambio)
        {
            bool check_detrax = false;
            if (FkDetraccion > 0)
            {
                check_detrax = true;
            }
            decimal efectivo = 0;
            decimal vuelto = 0;
            try
            {
                if (EfecReci == "")
                {

                }
                else
                {
                    efectivo = Convert.ToDecimal(EfecReci);
                }
            }
            catch (Exception ex)
            {
                efectivo = 0;
            }

            try
            {
                if (Vuel == "")
                {

                }
                else
                {
                    vuelto = Convert.ToDecimal(Vuel);
                }
            }
            catch (Exception ex)
            {
                vuelto = 0;
            }

            string msgReturn = "";
            string DataQr = "";
            int IdNewVent = 0;
            int IdCompVent = 0;
            DateTime NewFechEmis = DateTime.Now;
            DateTime NewFechVenc = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "El Comprobante se registró satisfactoriamente";
            try
            {
                VentaController CtrlVent = new VentaController();
                //VentaDetalleController CtrlVentDeta = new VentaDetalleController();
                ComprobanteVentaCobroController CtrlCompVentCobr = new ComprobanteVentaCobroController();
                VentaPedidoController CtrlVentPedi = new VentaPedidoController();
                Venta ventaReturn = null;
                VentaPedido VentaPedidoReturn = null;
                ComprobanteVenta comprobanteVentaReturn = null;
                ComprobanteVenta comprobante_venta = null;
                ComprobanteVentaCobro comprobante_venta_cobro = null;
                ComprobanteVentaCobro compVentCobrReturn = null;

                NewFechEmis = Convert.ToDateTime(FechEmis);
                NewFechVenc = Convert.ToDateTime(FechVenc);
                // int FkUsuaPago = Convert.ToInt32(Session["IdUsuario"]);
                string FkUsuaPago = Session["IdUsuario"].ToString();
                if (FlgSaveNewCliente.Equals(1))
                {
                    FkClie = await SaveNewClienteNatural(ClieDni, ClieNomb, ClieDire, ClieEmail);
                }
                Venta venta = new Venta
                {
                    fk_empresa = FkClie,
                    f_venta = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss")
                };
                ventaReturn = await CtrlVent.InsertVenta(venta);
                if (ventaReturn != null)
                {
                    IdNewVent = ventaReturn.id_venta;
                    //Guardamos en la table t_venta_pedido
                    if (arraCVIdPedi != null)
                    {
                        foreach (var oneIdPedi in arraCVIdPedi)
                        {
                            VentaPedido venta_pedido = new VentaPedido
                            {
                                fk_venta = IdNewVent,
                                fk_pedido = Convert.ToInt32(oneIdPedi)
                            };
                            VentaPedidoReturn = await CtrlVentPedi.InsertVentaPedido(venta_pedido);
                        }
                    }

                    if (arrVentDeta != null)
                    {
                        flgReturnSave = await SaveListVentaDetalle(IdNewVent, FkAlma, EstaVent, arrVentDeta);
                        if (flgReturnSave == 0)
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                    if (flgReturnSave == 1)
                    {
                        //Guardamos el comprobante
                        comprobante_venta = new ComprobanteVenta
                        {
                            fk_comprobante_tipo = FkCompTipo,
                            fk_venta = IdNewVent,
                            fk_caja = FkCaja,
                            //fk_medio_pago = FkMediPago,
                            nro_comprobante = NroCompVent,
                            total_bruto = CVTotaBrut,
                            total_igv = CVTotaIgv,
                            total_isc = 0,
                            total_neto = CVTotaNeto,
                            //saldo_bruto = CVSaldBrut,
                            estado = EstaVent,
                            //fk_tarjeta_credito = FkTarjCred,
                            //fk_nota_credito = FkNotaCred,
                            //fk_banco = FkBanc,
                            //nro_documento = NroDocuPago
                            total_impuestos_bolsas = 0,
                            efectivo_recibido = efectivo,
                            vuelto = vuelto,
                            check_detraccion = check_detrax,
                            detraccion = detraccion,
                            fk_detraccion_tipo = FkDetraccion,
                            direccion = ClieDire,
                            fk_moneda = FkMoneda,
                            f_vencimiento = NewFechVenc.ToString("yyyy/MM/dd H:mm:ss"),
                            f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                            referencia = TxtReferencia,
                            observacion_full = Observacion,
                            total_exonerado = TotalExonerado,
                            total_gratuito = TotalGratuito,
                            total_inafecto = TotalInafecto,
                            tipo_cambio = TipoCambio
                        };
                        comprobanteVentaReturn = await InsertComprobanteVenta(comprobante_venta);
                        if (comprobanteVentaReturn == null)
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                        else
                        {
                            Respuesta rptaFactE = await EnviaFacturacionElectronica(comprobanteVentaReturn);
                            if (rptaFactE.errors == null)
                            {
                                DataQr = rptaFactE.cadena_para_codigo_qr;
                                IdCompVent = comprobanteVentaReturn.id_comprobante_venta;
                                //Guardamos medio de pago
                                if (arrCompVentMediPago[0] != null)
                                {
                                    try
                                    {
                                        foreach (var oneCompVentCobro in arrCompVentMediPago)
                                        {
                                            comprobante_venta_cobro = new ComprobanteVentaCobro
                                            {
                                                fk_comprobante_venta = comprobanteVentaReturn.id_comprobante_venta,
                                                fk_medio_pago = Convert.ToInt32(oneCompVentCobro[0]),
                                                fk_tarjeta_credito = Convert.ToInt32(oneCompVentCobro[1]),
                                                fk_nota_credito = Convert.ToInt32(oneCompVentCobro[2]),
                                                fk_banco = Convert.ToInt32(oneCompVentCobro[3]),
                                                nro_documento = oneCompVentCobro[4],
                                                monto = Convert.ToDecimal(oneCompVentCobro[5]),
                                                saldo = Convert.ToDecimal(oneCompVentCobro[6])
                                            };
                                            compVentCobrReturn = await CtrlCompVentCobr.InsertComprobanteVentaCobro(comprobante_venta_cobro);
                                            try
                                            {
                                                ComprobanteVenta xcompro = new ComprobanteVenta()
                                                {
                                                    id_comprobante_venta = comprobanteVentaReturn.id_comprobante_venta,
                                                    cadena_para_codigo_qr = rptaFactE.cadena_para_codigo_qr,
                                                    enlace = rptaFactE.enlace,
                                                    codigo_hash = rptaFactE.codigo_hash
                                                };
                                                var xyz = await UpdateComprobanteVentaRespuesta(xcompro);
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                            else
                            {
                                comprobanteVentaReturn = await SaveDeleteComprobanteVenta2(comprobanteVentaReturn.id_comprobante_venta);
                                return Json(new { msgRpta = rptaFactE.errors, IdNewVent = -1 }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return Json(msgReturn, JsonRequestBehavior.AllowGet);
            return Json(new { msgRpta = msgReturn, IdNewVent = IdNewVent, IdCompVent = IdCompVent, DataQr = DataQr }, JsonRequestBehavior.AllowGet);
        }

        public async Task<Respuesta> EnviaFacturacionElectronica(ComprobanteVenta comprobanteVentaReturn)
        {
            List<VentaDetalle> lstVentaDetalle = null;
            FacturacionElectronicaController CtrlFactElec = new FacturacionElectronicaController();
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            List<Parametro> lstParametro = null;
            Respuesta rptaFactE = null;
            string EmailEmpr = "";

            try
            {
                string ParamDiasAlertSoliMues = ConfigurationManager.AppSettings["ParamEmailFactElect"].ToString();
                lstParametro = await GetParametroAll();
                lstParametro = lstParametro.Where(i => i.nombre.Trim().ToUpper().Equals(ParamDiasAlertSoliMues.Trim().ToUpper())).ToList();
                if (lstParametro.Count > 0)
                {
                    EmailEmpr = lstParametro[0].valor_string;
                }

                string SeriComp = comprobanteVentaReturn.nro_comprobante.Substring(0, 4);
                //SeriComp = comprobanteVentaReturn.sigla + SeriComp;
                string NroComp = comprobanteVentaReturn.nro_comprobante.Substring(5, comprobanteVentaReturn.nro_comprobante.Length - 5);
                //int ClieTipoDocu = 1;
                string ClieTipoDocu = "6";
                int clientipo = comprobanteVentaReturn.fk_cliente_tipo;
                if (clientipo == 1)
                {
                    ClieTipoDocu = "1";
                }
                else if (clientipo == 2)
                {
                    ClieTipoDocu = "6";
                }
                
                string ClieNuroDocu = comprobanteVentaReturn.ruc;
                string ClieDeno = comprobanteVentaReturn.razon_social;
                string ClieEmai = comprobanteVentaReturn.email;
                string ClieDire = comprobanteVentaReturn.direccion;
                decimal ValorIgv = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["ValorIgv"]);
                //if (comprobanteVentaReturn.id_cliente_tipo.Equals(2) && comprobanteVentaReturn.generico == "0")
                //{
                //    ClieTipoDocu = "6";
                //    ClieNuroDocu = comprobanteVentaReturn.ruc_empresa_cliente_juridico;
                //    ClieDeno = comprobanteVentaReturn.razon_social;
                //    ClieEmai = comprobanteVentaReturn.email_cliente_juridico;
                //    ClieDire = comprobanteVentaReturn.direccion_empresa;
                //}
                //if (comprobanteVentaReturn.generico == "1")
                //{
                //    ClieTipoDocu = "-";
                //}
                double totalgratuita = 0;

                List<Items> lstItems = new List<Items>();
                Items one_item = null;
                lstVentaDetalle = await CtrlVentDeta.GetVentaDetalle_ByFkVenta(comprobanteVentaReturn.fk_venta);
                foreach (var oneItem in lstVentaDetalle)
                {
                    double factIgv = 1.18;
                    if (oneItem.flag_afecto_igv == "0")
                    {
                        factIgv = 1;
                    }
                    
                    one_item = new Items();
                    one_item.unidad_de_medida = oneItem.tipo_bien;
                    one_item.codigo = oneItem.codigo;
                    one_item.descripcion = oneItem.descripcion;
                    one_item.cantidad = Convert.ToDouble(oneItem.cantidad);
                    //one_item.valor_unitario = Math.Round((Convert.ToDouble(oneItem.precio) / factIgv), 2); //500;
                    one_item.valor_unitario = Math.Round((Convert.ToDouble(oneItem.precio) ),2); //500;
                    one_item.precio_unitario = Math.Round((Convert.ToDouble(oneItem.precio) * factIgv), 2);//590;

                    one_item.descuento = "";
                    //one_item.subtotal = Math.Round(((Convert.ToDouble(oneItem.cantidad) * Convert.ToDouble(oneItem.precio)) / factIgv), 2);//500;
                    one_item.subtotal = Math.Round(Convert.ToDouble(Convert.ToDouble(oneItem.cantidad) * Convert.ToDouble(one_item.valor_unitario)), 2);//500;
                    one_item.tipo_de_igv = oneItem.id_tipo_afectacion_igv;
                    one_item.igv = Math.Round(Convert.ToDouble(oneItem.igv), 2);//90;
                    one_item.total = Math.Round(Convert.ToDouble(one_item.subtotal + one_item.igv), 2);//590;
                    if (oneItem.flag_afecto_igv == "0")
                    {
                        totalgratuita = totalgratuita + one_item.total;
                    }
                    one_item.anticipo_regularizacion = false;
                    one_item.anticipo_comprobante_serie = "";
                    one_item.anticipo_comprobante_numero = "";
                    one_item.codigo_producto_sunat = oneItem.codigo_sunat;
                    lstItems.Add(one_item);
                }

                Invoice invoice = new Invoice();
                invoice.operacion = "generar_comprobante";
                invoice.tipo_de_comprobante = Convert.ToInt32(comprobanteVentaReturn.codigo);
                invoice.serie = SeriComp;
                invoice.numero = Convert.ToInt32(NroComp);
                invoice.sunat_transaction = 1;
                invoice.cliente_tipo_de_documento = ClieTipoDocu;
                invoice.cliente_numero_de_documento = ClieNuroDocu;
                invoice.cliente_denominacion = ClieDeno;
                invoice.cliente_direccion = ClieDire;
                invoice.cliente_email = EmailEmpr;
                invoice.cliente_email_1 = ClieEmai;
                invoice.cliente_email_2 = "";
                invoice.fecha_de_emision = Convert.ToDateTime(comprobanteVentaReturn.f_emision);
                invoice.fecha_de_vencimiento = Convert.ToDateTime(comprobanteVentaReturn.f_vencimiento);
                invoice.moneda = Convert.ToInt32(comprobanteVentaReturn.fk_moneda);
                invoice.tipo_de_cambio = comprobanteVentaReturn.tipo_cambio;
                invoice.porcentaje_de_igv = Convert.ToDouble(ValorIgv);//18.00;
                invoice.descuento_global = "";
                invoice.total_descuento = "";
                invoice.total_anticipo = "";
                invoice.total_gravada = comprobanteVentaReturn.total_neto;//500;
                invoice.total_inafecta = comprobanteVentaReturn.total_inafecto;
                invoice.total_exonerada = comprobanteVentaReturn.total_exonerado;
                invoice.total_igv = Convert.ToDouble(comprobanteVentaReturn.total_igv); //90;
                invoice.total_gratuita = comprobanteVentaReturn.total_gratuito;
                invoice.total_otros_cargos = "";
                invoice.total = Convert.ToDouble(comprobanteVentaReturn.total_bruto);//590;
                invoice.percepcion_tipo = "";
                invoice.percepcion_base_imponible = "";
                invoice.total_percepcion = "";
                invoice.detraccion = comprobanteVentaReturn.check_detraccion;
                invoice.observaciones = "";
                invoice.documento_que_se_modifica_tipo = "";
                invoice.documento_que_se_modifica_serie = "";
                invoice.documento_que_se_modifica_numero = "";
                invoice.tipo_de_nota_de_credito = "";
                invoice.tipo_de_nota_de_debito = "";
                invoice.enviar_automaticamente_a_la_sunat = true;
                invoice.enviar_automaticamente_al_cliente = true;
                invoice.codigo_unico = "";
                invoice.condiciones_de_pago = "";
                invoice.medio_de_pago = "";
                invoice.placa_vehiculo = "";
                invoice.orden_compra_servicio = "";
                invoice.tabla_personalizada_codigo = "";
                invoice.formato_de_pdf = "";
                invoice.items = lstItems;

                try
                {
                    var jsondata = new JavaScriptSerializer().Serialize(invoice);
                    string path = Server.MapPath("~/Json/");
                    System.IO.File.WriteAllText(path + "invoice-" + invoice.cliente_numero_de_documento + "-"+ invoice.serie + ".json", jsondata);
                }
                catch (Exception ex)
                {

                }

                return rptaFactE = CtrlFactElec.enviarFacturacion(invoice);

                

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public Respuesta EnviaAnularFacturacionElectronica(ComprobanteVenta comprobanteVentaReturn)
        {
            FacturacionElectronicaController CtrlFactElec = new FacturacionElectronicaController();
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            Respuesta rptaFactE = null;

            string SeriComp = comprobanteVentaReturn.nro_comprobante.Substring(0, 4);
            //SeriComp = comprobanteVentaReturn.sigla + SeriComp;
            string NroComp = comprobanteVentaReturn.nro_comprobante.Substring(5, comprobanteVentaReturn.nro_comprobante.Length - 5);

            Invoice invoice = new Invoice();
            invoice.operacion = "generar_anulacion";
            invoice.tipo_de_comprobante = Convert.ToInt32(comprobanteVentaReturn.codigo);
            invoice.serie = SeriComp;
            invoice.numero = Convert.ToInt32(NroComp);
            invoice.motivo = "ERROR DEL SISTEMA";
            invoice.codigo_unico = "";

            return rptaFactE = CtrlFactElec.enviarFacturacion(invoice);
        }

        public async Task<int> SaveNewClienteNatural(string ClieDni, string ClieNomb, string ClieDire, string ClieEmail)
        {
            int IdClieNew = 0;
            int IdPersNew = 0;
            int IdClieNatuNew = 0;
            try
            {
                ClienteController CtrlClie = new ClienteController();
                PersonaController CtrlPerso = new PersonaController();

                Cliente cliente = new Cliente
                {
                    fk_cliente_tipo = 1
                };
                IdClieNew = await CtrlClie.InsertCliente(cliente);
                if (IdClieNew > 0)
                {
                    Persona persona = new Persona
                    {
                        nombres = ClieNomb.ToUpper(),
                        apellido_paterno = "",
                        apellido_materno = "",
                        f_nacimiento = DateTime.Now,
                        email = ClieEmail,
                        dni = ClieDni,
                        direccion = ClieDire
                    };
                    IdPersNew = await CtrlPerso.InsertPersona(persona);
                    if (IdPersNew != 0)
                    {
                        //Guardamos Cliente Natural
                        ClienteNatural cliente_natural = new ClienteNatural
                        {
                            fk_persona = IdPersNew,
                            fk_cliente = IdClieNew,
                            ruc = ""
                        };
                        IdClieNatuNew = await CtrlClie.InsertClienteNatural(cliente_natural);
                        if (IdClieNatuNew == 0) return 0;
                    }
                    else
                    {
                        return 0;
                    }
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
            return IdClieNew;
        }
        public async Task<int> SaveListVentaDetalleTemp(int FkVent, int FkAlma, string EstaVent, List<List<string>> arrVentDeta)
        {
            int flgSaveMovi = 0;
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                VentaDetalle ventaDetalleReturn = null;
                VentaDetalle venta_detalle = null;
                foreach (var oneDetaVent in arrVentDeta)
                {
                    venta_detalle = new VentaDetalle
                    {
                        fk_venta = FkVent,
                        fk_proyecto = Convert.ToInt32(oneDetaVent[0]),
                        fk_tipo_afectacion_igv = Convert.ToInt32(oneDetaVent[3]),
                        fk_tipo_isc = 0,
                        cantidad = Convert.ToDecimal(oneDetaVent[1]),
                        precio = Convert.ToDecimal(oneDetaVent[2]),
                        estado = oneDetaVent[5].Trim(),
                        codigo_sunat = oneDetaVent[6].Trim(),
                        codigo = oneDetaVent[7].Trim(),
                        descripcion = oneDetaVent[8].Trim(),
                        valor_venta = Convert.ToDecimal(oneDetaVent[9].Trim()),
                        igv = Convert.ToDecimal(oneDetaVent[10].Trim()),
                        importe = Convert.ToDecimal(oneDetaVent[11].Trim()),
                        tipo_bien = oneDetaVent[12].Trim().ToUpper()
                    };

                    ventaDetalleReturn = await CtrlVentDeta.InsertVentaDetalleTemp(venta_detalle);
                    if (ventaDetalleReturn == null) return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
        public async Task<int> SaveListVentaDetalle(int FkVent, int FkAlma, string EstaVent, List<List<string>> arrVentDeta)
        {
            int flgSaveMovi = 0;
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                VentaDetalle ventaDetalleReturn = null;
                VentaDetalle venta_detalle = null;
                foreach (var oneDetaVent in arrVentDeta)
                {
                    venta_detalle = new VentaDetalle
                    {
                        fk_venta = FkVent,
                        fk_proyecto = Convert.ToInt32(oneDetaVent[0]),
                        fk_tipo_afectacion_igv = Convert.ToInt32(oneDetaVent[3]),
                        fk_tipo_isc = 0,
                        cantidad = Convert.ToDecimal(oneDetaVent[1]),
                        precio = Convert.ToDecimal(oneDetaVent[2]),
                        estado = oneDetaVent[5].Trim(),
                        codigo_sunat = oneDetaVent[6].Trim(),
                        codigo = oneDetaVent[7].Trim(),
                        descripcion = oneDetaVent[8].Trim(),
                        valor_venta= Convert.ToDecimal(oneDetaVent[9].Trim()),
                        igv = Convert.ToDecimal(oneDetaVent[10].Trim()),
                        importe = Convert.ToDecimal(oneDetaVent[11].Trim()),
                        tipo_bien = oneDetaVent[12].Trim().ToUpper()
                    };

                    ventaDetalleReturn = await CtrlVentDeta.InsertVentaDetalle(venta_detalle);
                    if (ventaDetalleReturn == null) return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        //public async Task<int> SaveMovimiento(int FkVentDeta, int FkProd, string strDist)
        //{
        //    int FkAlma = 0;
        //    decimal Cant = 0;
        //    MovimientoController CtrlMovi = new MovimientoController();
        //    Movimiento movimiento = null;
        //    Movimiento movimientoReturn = null;
        //    try
        //    {
        //        int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_Venta"]);
        //        string[] arraDist = strDist.Split(',');
        //        if (arraDist.Length > 0)
        //        {
        //            foreach (var oneElem in arraDist)
        //            {
        //                string[] oneMovi = oneElem.Split('[');
        //                FkAlma = Convert.ToInt32(oneMovi[0]);
        //                Cant = Convert.ToDecimal(oneMovi[1]);
        //                //Insertamos en la tabla t_movimiento
        //                movimiento = new Movimiento
        //                {
        //                    fk_movimiento_tipo = FkMoviTipo,
        //                    fk_guia_remision_detalle = 0,
        //                    fk_venta_detalle = FkVentDeta,
        //                    fk_comprobante_traslado_detalle = 0,
        //                    fk_nota_credito_detalle = 0,
        //                    fk_almacen = FkAlma,
        //                    fk_producto = FkProd,
        //                    cantidad = Cant
        //                };
        //                movimientoReturn = await CtrlMovi.InsertMovimiento(movimiento);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //    return 1;
        //}

        public async Task<int> SaveMovimiento(int FkVentDeta, int FkProd, int FkAlma, decimal Cant)
        {
            MovimientoController CtrlMovi = new MovimientoController();
            Movimiento movimiento = null;
            Movimiento movimientoReturn = null;
            try
            {
                int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_Venta"]);
                movimiento = new Movimiento
                {
                    fk_movimiento_tipo = FkMoviTipo,
                    fk_guia_remision_detalle = 0,
                    fk_venta_detalle = FkVentDeta,
                    fk_comprobante_traslado_detalle = 0,
                    fk_nota_credito_detalle = 0,
                    fk_almacen = FkAlma,
                    fk_producto = FkProd,
                    cantidad = Cant
                };
                movimientoReturn = await CtrlMovi.InsertMovimiento(movimiento);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public async Task<ActionResult> FnSaveAnularComprobanteVenta(int IdCompVent, string FEmis)
        {
            ComprobanteVenta comprobante_venta = null;
            ComprobanteVenta comprobanteVentaReturn = null;
            try
            {
                DateTime FechEmisCV = Convert.ToDateTime(FEmis);
                DateTime FechActual = DateTime.Now;
                DateTime FechComparar = FechEmisCV.AddDays(3);
                if (FechActual < FechComparar)
                {
                    comprobante_venta = await SaveDeleteComprobanteVenta(IdCompVent);
                    if (comprobante_venta == null)
                    {
                        return Json(new { flgRpta = -1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Respuesta rptaFactE = EnviaAnularFacturacionElectronica(comprobante_venta);
                        if (rptaFactE.errors == null)
                        {

                        }
                        else
                        {
                            //Actualizamos el estado del comprobante
                            comprobante_venta = new ComprobanteVenta
                            {
                                id_comprobante_venta = IdCompVent,
                                estado = "1"
                            };
                            comprobanteVentaReturn = await UpdateEstadoComprobanteVenta(comprobante_venta);
                            return Json(new { flgRpta = -3, msgRpta = rptaFactE.errors }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return Json(new { flgRpta = -2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { flgRpta = 0 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { flgRpta = 1 }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ComprobanteVenta> SaveDeleteComprobanteVenta(int IdCompVent)
        {
            ComprobanteVenta comprobante_venta = null;
            try
            {
                comprobante_venta = await DeleteComprobanteVenta(IdCompVent);
                if (comprobante_venta == null)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return null;
            }
            return comprobante_venta;
        }
        public async Task<ComprobanteVenta> SaveDeleteComprobanteVenta2(int IdCompVent)
        {
            ComprobanteVenta comprobante_venta = null;
            try
            {
                comprobante_venta = await DeleteComprobanteVentaId(IdCompVent);
                if (comprobante_venta == null)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return null;
            }
            return comprobante_venta;
        }
        public async Task<JsonResult> GetJson_ValidaStock(int FkAlma, List<List<string>> arraVali)
        {
            AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
            AlmacenStock almacen_stock = null;
            List<AlmacenStock> lstAlmacenStockReturn = new List<AlmacenStock>();
            int FkProd = 0;
            decimal CantPedi = 0;
            string msgReturn = "";
            try
            {
                if (arraVali != null)
                {
                    foreach (var oneVali in arraVali)
                    {
                        FkProd = Convert.ToInt32(oneVali[0]);
                        CantPedi = Convert.ToDecimal(oneVali[1]);
                        almacen_stock = await CtrlAlmaStoc.GetAlmacenStock_AlmacenProducto(FkAlma, FkProd);
                        if (almacen_stock != null)
                        {
                            if (almacen_stock.existencia < CantPedi)
                            {
                                lstAlmacenStockReturn.Add(almacen_stock);
                            }
                        }
                        else
                        {
                            almacen_stock = new AlmacenStock
                            {
                                fk_producto = FkProd,
                            };
                            lstAlmacenStockReturn.Add(almacen_stock);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(lstAlmacenStockReturn);
        }

        public async Task<List<Producto>> SelectProductoVenta()
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
                lentidad = new List<Models.Producto>();
            }
            return lentidad;
        }

        public async Task<ActionResult> ListaProductoVenta2(string CallBy)
        {
            ProductoErpController CtrlProducto = new ProductoErpController();
            List<ProductoErp> lentidad = await CtrlProducto.GetProductos();
            if (lentidad == null)
            {
                lentidad = new List<Models.ProductoErp>();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lentidad);
        }
        //public async Task<ActionResult> ListaProductoVenta(string CallBy)
        //{
        //    List<Producto> lentidad = await SelectProductoVenta();
        //    if (lentidad == null)
        //    {
        //        lentidad = new List<Models.Producto>();
        //    }
        //    ViewBag.CallBy = CallBy;
        //    return PartialView(lentidad);
        //}
        
        public async Task<ActionResult> ListaServicioVenta(string CallBy)
        {
            List<ProyectoErp> lentidad = await SelectProyectoVenta();
            if (lentidad == null)
            {
                lentidad = new List<ProyectoErp>();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lentidad);
        }

        private async Task<List<ProyectoErp>> SelectProyectoVenta()
        {
            List<ProyectoErp> lproyectos = new List<ProyectoErp>();
            try
            {
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
            }
            catch (Exception ex)
            {
                lproyectos = new List<ProyectoErp>();
            }
            return lproyectos;
        }

        public async Task<ActionResult> SaveRevertirComprobanteVenta(int IdCompVent, int FkUsuaVent, int FkClie, int FkPedi,
            int FkCompTipo, int FkCaja, string FechEmis, string NroCompVent, decimal CVTotaNeto, decimal CVTotaIgv,
            decimal CVTotaBrut, string EstaVent, int FkAlma, List<List<string>> arrVentDeta, List<List<string>> arrCompVentMediPago)
        {
            string msgReturn = "";
            string DataQr = "";
            int IdNewVent = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "El Proceso de revertir se concreto satisfactoriamente";
            try
            {
                VentaController CtrlVent = new VentaController();
                ComprobanteVentaCobroController CtrlCompVentCobr = new ComprobanteVentaCobroController();
                Venta ventaReturn = null;
                ComprobanteVenta comprobanteVentaAnular = null;
                ComprobanteVenta comprobanteVentaReturn = null;
                ComprobanteVenta comprobante_venta = null;
                ComprobanteVentaCobro comprobante_venta_cobro = null;
                ComprobanteVentaCobro compVentCobrReturn = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                string FkUsuaPago = Session["IdUsuario"].ToString();
                comprobanteVentaAnular = await SaveDeleteComprobanteVenta(IdCompVent);
                //Enviamos la anlacion al servicio de facturacion electronica
                Respuesta rptaFactE = EnviaAnularFacturacionElectronica(comprobanteVentaAnular);
                if (rptaFactE.errors == null)
                {
                    Venta venta = new Venta
                    {
                        fk_empresa = FkClie,
                        f_venta = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss")
                    };
                    ventaReturn = await CtrlVent.InsertVenta(venta);
                    if (ventaReturn != null)
                    {
                        IdNewVent = ventaReturn.id_venta;
                        if (arrVentDeta != null)
                        {
                            flgReturnSave = await SaveListVentaDetalle(IdNewVent, FkAlma, EstaVent, arrVentDeta);
                            if (flgReturnSave == 0)
                            {
                                msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                            }
                        }
                        if (flgReturnSave == 1)
                        {
                            //Guardamos el comprobante
                            comprobante_venta = new ComprobanteVenta
                            {
                                fk_comprobante_tipo = FkCompTipo,
                                fk_venta = IdNewVent,
                                fk_caja = FkCaja,
                                nro_comprobante = NroCompVent,
                                total_bruto = CVTotaBrut,
                                total_igv = CVTotaIgv,
                                total_isc = 0,
                                total_neto = CVTotaNeto,
                                estado = EstaVent
                            };
                            comprobanteVentaReturn = await InsertComprobanteVenta(comprobante_venta);
                            if (comprobanteVentaReturn == null)
                            {
                                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                            }
                            else
                            {
                                rptaFactE = await EnviaFacturacionElectronica(comprobanteVentaReturn);
                                if (rptaFactE.errors == null)
                                {
                                    DataQr = rptaFactE.cadena_para_codigo_qr;
                                    IdCompVent = comprobanteVentaReturn.id_comprobante_venta;
                                    //Guardamos medio de pago
                                    foreach (var oneCompVentCobro in arrCompVentMediPago)
                                    {
                                        comprobante_venta_cobro = new ComprobanteVentaCobro
                                        {
                                            fk_comprobante_venta = comprobanteVentaReturn.id_comprobante_venta,
                                            fk_medio_pago = Convert.ToInt32(oneCompVentCobro[0]),
                                            fk_tarjeta_credito = Convert.ToInt32(oneCompVentCobro[1]),
                                            fk_nota_credito = Convert.ToInt32(oneCompVentCobro[2]),
                                            fk_banco = Convert.ToInt32(oneCompVentCobro[3]),
                                            nro_documento = oneCompVentCobro[4],
                                            monto = Convert.ToDecimal(oneCompVentCobro[5]),
                                            saldo = Convert.ToDecimal(oneCompVentCobro[6])
                                        };
                                        compVentCobrReturn = await CtrlCompVentCobr.InsertComprobanteVentaCobro(comprobante_venta_cobro);
                                    }
                                }
                                else
                                {
                                    return Json(new { msgRpta = rptaFactE.errors, IdNewVent = -1 }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                    if (flgError == 1)
                    {
                        Response.StatusCode = 500;
                        return Json(msgReturn, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { msgRpta = rptaFactE.errors, IdNewVent = -1 }, JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return Json(msgReturn, JsonRequestBehavior.AllowGet);
            return Json(new { msgRpta = msgReturn, IdNewVent = IdNewVent, IdCompVent = IdCompVent, DataQr = DataQr }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveRegularizarComprobanteVenta(int IdCompVent, List<List<string>> arrVentDeta)
        {
            string msgReturn = "";
            int FkVentDeta = 0;
            int FkVentProd = 0;
            int flgError = 0;
            decimal Cant = 0;
            int flgReturnSave = 1;
            msgReturn = "La regularización se concreto satisfactoriamente";
            try
            {
                VentaController CtrlVent = new VentaController();
                ComprobanteVentaCobroController CtrlCompVentCobr = new ComprobanteVentaCobroController();
                ComprobanteVenta comprobanteVentaReturn = null;
                ComprobanteVenta comprobante_venta = null;
                int FkAlmaPrinc = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FkAlmaPrinc"]);
                await SaveDeleteComprobanteVenta(IdCompVent);

                if (arrVentDeta != null)
                {
                    foreach (var oneVentDeta in arrVentDeta)
                    {
                        FkVentDeta = Convert.ToInt32(oneVentDeta[6]);
                        FkVentProd = Convert.ToInt32(oneVentDeta[0]);
                        Cant = Convert.ToDecimal(oneVentDeta[1]);
                        flgReturnSave = await SaveMovimiento(FkVentDeta, FkVentProd, FkAlmaPrinc, Cant);
                        if (flgReturnSave == 0)
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                            break;
                        }
                    }

                }
                if (flgReturnSave == 1)
                {
                    //Actualizamos el estado del comprobante
                    comprobante_venta = new ComprobanteVenta
                    {
                        id_comprobante_venta = IdCompVent,
                        estado = "1"
                    };
                    comprobanteVentaReturn = await UpdateEstadoComprobanteVenta(comprobante_venta);
                    if (comprobanteVentaReturn == null)
                    {
                        msgReturn = "Ocurrió un error al intentar Regularizar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Regularizar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }
        ComercialController comer = new ComercialController();
        public async Task<ActionResult> ListaClientes(string CallBy)
        {
            //var empresas = await comer.GetEmpresasErp();
            List<Empresa> lstEntidad = null;
            //List<Cliente> lstEntidad = null;
            lstEntidad = await comer.GetEmpresasErp();

            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }
        public async Task<ActionResult> ListaClientes2(string CallBy, string tipocliente)
        {
            int tipo_cliente = Convert.ToInt32(tipocliente);
            List<Empresa> lstEntidad = null;
            var xlstEntidad = await comer.GetEmpresasErp();
            if (xlstEntidad.Any())
            {
                try
                {
                   // lstEntidad = xlstEntidad.Where(j => j.fk_cliente_tipo == tipo_cliente).ToList();
                    lstEntidad = xlstEntidad.ToList();
                }
                catch (Exception ex)
                {
                    lstEntidad = new List<Empresa>(); 
                }
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ViewNotaEntrega(int FkVent)
        {
            string msgReturn = "";
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            List<VentaDetalle> lstVentaDetalle = null;
            try
            {
                lstVentaDetalle = await CtrlVentDeta.GetVentaDetalle_Despacho(FkVent);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstVentaDetalle);
        }

        public async Task<ActionResult> SaveNewCliente(int TipoClie, string ClieRuc, string ClieRazoSoci,
            string ClieDni, string ClieNomb, string ClieApelPate, string ClieApelMate, string ClieDire, string ClieEmai)
        {
            string msgReturn = "";
            int IdClieNew = 0;
            int IdEmprNew = 0;
            int IdClieJuriNew = 0;
            int IdPersNew = 0;
            int IdClieNatuNew = 0;
            string DniRuc = "";
            string NombRazoSoci = "";
            int flgError = 0;
            //msgReturn = "El Comprobante se registró satisfactoriamente";
            try
            {
                ClienteController CtrlClie = new ClienteController();

                ComprobanteVentaCobroController CtrlCompVentCobr = new ComprobanteVentaCobroController();
                EmpresaController CtrlEmpresa = new EmpresaController();
                PersonaController CtrlPerso = new PersonaController();

                Cliente cliente = new Cliente
                {
                    fk_cliente_tipo = TipoClie
                };
                IdClieNew = await CtrlClie.InsertCliente(cliente);
                if (IdClieNew > 0)
                {
                    if (TipoClie == 2) //Cliente Juridico
                    {
                        DniRuc = ClieRuc;
                        NombRazoSoci = ClieRazoSoci.ToUpper();
                        //Guardamos empresa
                        Empresa empresa = new Empresa
                        {
                            ruc = ClieRuc,
                            razon_social = ClieRazoSoci.ToUpper(),
                            direccion = ClieDire.ToUpper(),
                            email = ClieEmai
                        };
                        IdEmprNew = await CtrlEmpresa.InsertEmpresa(empresa);
                        if (IdEmprNew > 0)
                        {
                            //Guardamos en Cliente_Juridico
                            ClienteJuridico cliente_juridico = new ClienteJuridico
                            {
                                fk_empresa = IdEmprNew,
                                fk_cliente = IdClieNew
                            };
                            IdClieJuriNew = await CtrlClie.InsertClienteJuridico(cliente_juridico);
                            if (IdClieJuriNew == 0)
                            {
                                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                            }
                        }
                        else
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                    else //Cliente Natural
                    {
                        DniRuc = ClieDni.ToUpper();
                        NombRazoSoci = ClieNomb.ToUpper() + " " + ClieApelPate.ToUpper() + " " + ClieApelMate.ToUpper();
                        //Guardamos Persona
                        Persona persona = new Persona
                        {
                            nombres = ClieNomb.ToUpper(),
                            apellido_paterno = ClieApelPate.ToUpper(),
                            apellido_materno = ClieApelMate.ToUpper(),
                            f_nacimiento = DateTime.Now,
                            email = ClieEmai,
                            dni = ClieDni,
                            direccion = ClieDire
                        };
                        IdPersNew = await CtrlPerso.InsertPersona(persona);
                        if (IdPersNew != 0)
                        {
                            //Guardamos Cliente Natural
                            ClienteNatural cliente_natural = new ClienteNatural
                            {
                                fk_persona = IdPersNew,
                                fk_cliente = IdClieNew,
                                ruc = ""
                            };
                            IdClieNatuNew = await CtrlClie.InsertClienteNatural(cliente_natural);
                            if (IdClieNatuNew == 0)
                            {
                                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                            }
                        }
                        else
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return Json(msgReturn, JsonRequestBehavior.AllowGet);
            return Json(new { msgRpta = msgReturn, IdClieNew = IdClieNew, DniRuc = DniRuc, NombRazoSoci = NombRazoSoci, ClieEmai = ClieEmai }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PrintNotaEntrega(int? FkVent)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                int idi = Convert.ToInt32(FkVent);
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                //List<VentaDetalle> lstVentaDetalle = await CtrlVentDeta.Get_DataDespacho(FkVent);
                DataSet data = await CtrlVentDeta.Get_DataDespacho(idi);
                //string papel = WebConfigurationManager.AppSettings["GuiaProancoPaper"];
                if (data?.Tables.Count > 0)
                {
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrNotaEntrega.rpt")));
                    rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                    rd.SetParameterValue("@fk_venta", FkVent);
                    string path = Path.Combine(Server.MapPath("~/assets/img/cromo_plastic.jpg"));
                    for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                    {
                        if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                        {
                            rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                        }
                    }
                    //rd.SetDataSource(data.Tables[0]);
                    PrintDocument pDoc = new PrintDocument();
                    PrintLayoutSettings PrintLayout = new PrintLayoutSettings();
                    PrinterSettings printerSettings = new PrinterSettings();
                    //if (WebConfigurationManager.AppSettings["GuiaProancoPrinter"] != "")
                    //{
                    //    printerSettings.PrinterName = WebConfigurationManager.AppSettings["GuiaProancoPrinter"];
                    //}
                    //else
                    //{
                    printerSettings.PrinterName = WebConfigurationManager.AppSettings["DefaultPrinter"];
                    //}
                    PageSettings pSettings = new PageSettings(printerSettings);
                    pSettings.Landscape = true;
                    pSettings.Margins.Top = 0;
                    pSettings.Margins.Bottom = 0;
                    pSettings.Margins.Left = 0;
                    pSettings.Margins.Right = 0;
                    pSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);
                    pSettings.PaperSize.RawKind = (int)PaperKind.A4;
                    rd.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                    rd.PrintOptions.PrinterDuplex = PrinterDuplex.Simplex;
                    rd.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    rd.PrintToPrinter(printerSettings, pSettings, false, PrintLayout);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<VentaDetalle>> Get_DataComprobanteVentaTempList(int FkVent)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            var lstVentaDetalle = await CtrlVentDeta.GetVentaDetalle_ByFkVentaTemp(FkVent);
            if (lstVentaDetalle != null)
            {
                if (lstVentaDetalle.Any())
                {

                    return lstVentaDetalle;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public async Task<List<VentaDetalle>> Get_DataComprobanteVentaList(int FkVent)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            var lstVentaDetalle = await CtrlVentDeta.GetVentaDetalle_ByFkVenta(FkVent);
            if (lstVentaDetalle != null)
            {
                if (lstVentaDetalle.Any())
                {

                    return lstVentaDetalle;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public async Task<DataSet> Get_DataComprobanteVenta(int FkVent)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            var lstVentaDetalle = await CtrlVentDeta.GetVentaDetalle_ByFkVenta(FkVent);
            if (lstVentaDetalle != null)
            {
                if (lstVentaDetalle.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_venta_detalle", typeof(int));
                    dt.Columns.Add("fk_venta", typeof(int));
                    dt.Columns.Add("fk_proyecto", typeof(int));
                    dt.Columns.Add("fk_tipo_afectacion_igv", typeof(int));
                    dt.Columns.Add("fk_tipo_isc", typeof(int));
                    dt.Columns.Add("cantidad", typeof(decimal));
                    dt.Columns.Add("precio", typeof(decimal));
                    dt.Columns.Add("estado", typeof(string));
                    //dt.Columns.Add("id_producto", typeof(int));
                    //dt.Columns.Add("fk_proveedor", typeof(int));
                    //dt.Columns.Add("fk_producto_marca", typeof(int));
                    //dt.Columns.Add("fk_producto_subfamilia", typeof(int));
                    //dt.Columns.Add("cod_producto", typeof(string));
                    dt.Columns.Add("descripcion", typeof(string));
                    //dt.Columns.Add("codigo_sku", typeof(string));
                    //dt.Columns.Add("embalaje", typeof(string));
                    //dt.Columns.Add("image_url", typeof(string));
                    //dt.Columns.Add("estado_producto", typeof(string));
                    dt.Columns.Add("id_producto_marca", typeof(int));
                    dt.Columns.Add("descripcion_marca", typeof(string));
                    dt.Columns.Add("estado_producto_marca", typeof(string));
                    dt.Columns.Add("id_producto_subfamilia", typeof(int));
                    dt.Columns.Add("fk_producto_familia", typeof(int));
                    dt.Columns.Add("codigo", typeof(string));
                    dt.Columns.Add("descripcion_producto_subfamilia", typeof(string));
                    dt.Columns.Add("estado_producto_subfamilia", typeof(string));
                    dt.Columns.Add("id_tipo_afectacion_igv", typeof(int));
                    dt.Columns.Add("fk_igv", typeof(int));
                    dt.Columns.Add("codigo_tipo_igv", typeof(string));
                    dt.Columns.Add("descripcion_tipo_afectacion", typeof(string));
                    dt.Columns.Add("flag_afecto_igv", typeof(string));
                    dt.Columns.Add("flag_tipo_afecto", typeof(string));
                    dt.Columns.Add("id_igv", typeof(int));
                    dt.Columns.Add("codigo_igv", typeof(string));
                    dt.Columns.Add("descripcion_igv", typeof(string));
                    dt.Columns.Add("un_ece", typeof(string));
                    dt.Columns.Add("porcentaje", typeof(decimal));
                    dt.Columns.Add("codigo_sunat", typeof(string));


                    foreach (var item in lstVentaDetalle)
                    {
                        DataRow row = dt.NewRow();
                        row["id_venta_detalle"] = item.id_venta_detalle;
                        row["fk_venta"] = item.fk_venta;
                        row["fk_proyecto"] = item.fk_proyecto;
                        row["fk_tipo_afectacion_igv"] = item.fk_tipo_afectacion_igv;
                        row["fk_tipo_isc"] = item.fk_tipo_isc;
                        row["cantidad"] = item.cantidad;
                        row["precio"] = item.precio;
                        row["estado"] = item.estado;
                        //row["id_producto"] = item.id_producto;
                        //row["fk_proveedor"] = item.fk_proveedor;
                        //row["fk_producto_marca"] = item.fk_producto_marca;
                        //row["fk_producto_subfamilia"] = item.fk_producto_subfamilia;
                        //row["cod_producto"] = item.cod_producto;
                        row["descripcion"] = item.descripcion;
                        //row["codigo_sku"] = item.codigo_sku;
                        //row["embalaje"] = item.embalaje;
                        //row["image_url"] = item.image_url;
                        row["estado_producto"] = item.estado_producto;
                        row["id_producto_marca"] = item.id_producto_marca;
                        row["descripcion_marca"] = item.descripcion_marca;
                        row["estado_producto_marca"] = item.estado_producto_marca;
                        row["id_producto_subfamilia"] = item.id_producto_subfamilia;
                        row["fk_producto_familia"] = item.fk_producto_familia;
                        row["codigo"] = item.codigo;
                        row["descripcion_producto_subfamilia"] = item.descripcion_producto_subfamilia;
                        row["estado_producto_subfamilia"] = item.estado_producto_subfamilia;
                        row["id_tipo_afectacion_igv"] = item.id_tipo_afectacion_igv;
                        row["fk_igv"] = item.fk_igv;
                        row["codigo_tipo_igv"] = item.codigo_tipo_igv;
                        row["descripcion_tipo_afectacion"] = item.descripcion_tipo_afectacion;
                        row["flag_afecto_igv"] = item.flag_afecto_igv;
                        row["flag_tipo_afecto"] = item.flag_tipo_afecto;
                        row["id_igv"] = item.id_igv;
                        row["codigo_igv"] = item.codigo_igv;
                        row["descripcion_igv"] = item.descripcion_igv;
                        row["un_ece"] = item.un_ece;
                        row["porcentaje"] = item.porcentaje;
                        row["codigo_sunat"] = item.codigo_sunat;
                        dt.Rows.Add(row);
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public async Task<ActionResult> PrintComprobanteVenta(int? FkVent, int FkCompVent, string DescTipoComp, string NroCompVent,
            string DniRuc, string NombClie, string DireClie, decimal? EfecReci, decimal? Vuel, string DataQr)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
                string SiglComp = "";
                string NombCaja = "";
                string NombVend = "";
                string TotaIgv = "";
                string TotaNeto = "";
                string Medios = "";
                string Tarjeta = "";
                int idi = Convert.ToInt32(FkVent);
                //Obtengo el Comprobante Venta
                ComprobanteVenta comprobante_venta = await CtrlCompVent.GetComprobanteVentaById(FkCompVent);
                NombCaja = comprobante_venta.nombre_usuario_caja + " " + comprobante_venta.apellido_paterno_usuario_caja + " " + comprobante_venta.apellido_materno_usuario_caja;
                NombVend = comprobante_venta.nombre_usuario_venta + " " + comprobante_venta.apellido_paterno_usuario_venta + " " + comprobante_venta.apellido_materno_usuario_venta;
                TotaIgv = comprobante_venta.total_igv.ToString("#,##0.00");
                TotaNeto = comprobante_venta.total_neto.ToString("#,##0.00");

                Medios = comprobante_venta.medio_pago;
                Tarjeta = comprobante_venta.tarjeta;
                string paramrucdni = "RUC";
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                //List<VentaDetalle> lstVentaDetalle = await CtrlVentDeta.Get_DataDespacho(FkVent);
                //comprobante_venta = await GetComprobanteVentaById(IdCompVent);
                if (DescTipoComp.Equals("BOLETA"))
                {
                    SiglComp = "BOLETA ELECTRONICA";
                    paramrucdni = "DNI";
                }
                else if (DescTipoComp.Equals("FACTURA"))
                {
                    SiglComp = "FACTURA ELECTRONICA";
                }
                string CompNro = NroCompVent;
                string DniRucClie = DniRuc;
                DataSet data = await Get_DataComprobanteVenta(idi);
                if (EfecReci == null)
                {
                    EfecReci = 0;
                }
                if (Vuel == null)
                {
                    Vuel = 0;
                }
                if (data?.Tables.Count > 0)
                {
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrComprobanteVenta.rpt")));
                    rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                    rd.SetParameterValue("@fk_venta", FkVent);

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(DataQr, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    qrCodeImage.Save(Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg"), ImageFormat.Jpeg);
                    string path = Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg");
                    for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                    {
                        if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                        {
                            rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                        }
                    }

                    //rd.SetDataSource(data.Tables[0]);
                    rd.SetParameterValue("ParamRucDni", paramrucdni);
                    rd.SetParameterValue("ParamDniRucCliente", DniRucClie);
                    rd.SetParameterValue("ParamCliente", NombClie);
                    rd.SetParameterValue("ParamDireccion", DireClie);
                    rd.SetParameterValue("ParamCajero", NombCaja);
                    rd.SetParameterValue("ParamVendedor", NombVend);
                    rd.SetParameterValue("ParamComprobanteNumero", CompNro);
                    rd.SetParameterValue("ParamSigla", SiglComp);
                    rd.SetParameterValue("ParamEfectivoRecibido", Convert.ToDecimal(EfecReci).ToString("#,##0.00"));
                    rd.SetParameterValue("ParamVuelto", Convert.ToDecimal(Vuel).ToString("#,##0.00"));
                    rd.SetParameterValue("ParamIgv", TotaIgv);
                    rd.SetParameterValue("ParamNeto", TotaNeto);

                    rd.SetParameterValue("Medios", Medios);
                    rd.SetParameterValue("Tarjeta", Tarjeta);

                    PrintDocument pDoc = new PrintDocument();
                    PrintLayoutSettings PrintLayout = new PrintLayoutSettings();
                    PrinterSettings printerSettings = new PrinterSettings();
                    //if (WebConfigurationManager.AppSettings["GuiaProancoPrinter"] != "")
                    //{
                    //    printerSettings.PrinterName = WebConfigurationManager.AppSettings["GuiaProancoPrinter"];
                    //}
                    //else
                    //{
                    printerSettings.PrinterName = WebConfigurationManager.AppSettings["DefaultPrinter"];
                    PrintingPermission perm = new PrintingPermission(System.Security.Permissions.PermissionState.Unrestricted);
                    perm.Level = PrintingPermissionLevel.AllPrinting;
                    //}
                    PageSettings pSettings = new PageSettings(printerSettings);
                    pSettings.Landscape = true;
                    pSettings.Margins.Top = 0;
                    pSettings.Margins.Bottom = 0;
                    pSettings.Margins.Left = 0;
                    pSettings.Margins.Right = 0;
                    //pSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);
                    //pSettings.PaperSize.RawKind = (int)PaperKind.A4;
                    rd.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                    rd.PrintOptions.PrinterDuplex = PrinterDuplex.Simplex;
                    rd.PrintOptions.PaperOrientation = PaperOrientation.Landscape;

                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        rd.PrintToPrinter(printerSettings, pSettings, false, PrintLayout);
                    }


                }
                return null;
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public async Task<ActionResult> PrintComprobanteVentaViewTemp(int? FkVent, int FkCompVent, string DescTipoComp, string NroCompVent,
            string DniRuc, string NombClie, string DireClie, decimal? EfecReci, decimal? Vuel, string DataQr)
        {
            string codigo = "";
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
                string SiglComp = "";
                string TotaIgv = "";
                string TotalLetras = "";
                string Moneda = "";
                string TotaNeto = "";
                string paramrucdni = "RUC";
                int idi = Convert.ToInt32(FkVent);
                ComprobanteVenta comprobante_venta = await CtrlCompVent.GetComprobanteVentaTempById(FkCompVent);
                TotaIgv = comprobante_venta.total_igv.ToString("#,##0.00");
                TotaNeto = comprobante_venta.total_neto.ToString("#,##0.00");
                TotalLetras = comprobante_venta.total_bruto.NumeroALetras();
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                if (DescTipoComp.Equals("BOLETA"))
                {
                    SiglComp = "BOLETA ELECTRONICA";
                    paramrucdni = "DNI";
                }
                else if (DescTipoComp.Equals("FACTURA"))
                {
                    SiglComp = "FACTURA ELECTRONICA";
                    paramrucdni = "RUC";
                }
                string CompNro = NroCompVent;
                string DniRucClie = DniRuc;
                var data = await Get_DataComprobanteVentaTempList(idi);
                codigo = SiglComp + " " + NroCompVent;
                if (EfecReci == null)
                {
                    EfecReci = 0;
                }
                if (Vuel == null)
                {
                    Vuel = 0;
                }
                if (data.Count > 0)
                {
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;


                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrComprobanteVentaPreview.rpt")));
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetParameterValue("@fk_venta", FkVent);

                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData =
                            qrGenerator.CreateQrCode(DataQr, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(5);
                        qrCodeImage.Save(Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg"),
                            ImageFormat.Jpeg);
                        string path = Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg");
                        for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                        {
                            if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                            {
                                rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                            }
                        }

                        //rd.SetDataSource(data.Tables[0]);
                        rd.SetParameterValue("ParamDniRucCliente", DniRucClie);
                        rd.SetParameterValue("ParamCliente", NombClie);
                        rd.SetParameterValue("ParamDireccion", DireClie);
                        rd.SetParameterValue("ParamComprobanteNumero", CompNro);
                        rd.SetParameterValue("ParamSigla", SiglComp);
                        rd.SetParameterValue("ParamIgv", TotaIgv);
                        rd.SetParameterValue("ParamNeto", TotaNeto);
                        rd.SetParameterValue("ParamMoneda", Moneda);
                        rd.SetParameterValue("ParamLetras", TotalLetras);
                        rd.SetParameterValue("ParamRucDni", paramrucdni);
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\TempDoc-" + codigo + ".pdf");
                    }
                    ViewBag.Printer = "TempDoc-" + codigo;
                    ViewBag.Codigo = "TempDoc-" + codigo + ".pdf";

                }
                //return null;
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar!";
                ViewBag.Codigo = "";
            }
            return View();
        }
        public async Task<ActionResult> PrintComprobanteVentaView(int? FkVent, int FkCompVent, string DescTipoComp, string NroCompVent,
            string DniRuc, string NombClie, string DireClie, decimal? EfecReci, decimal? Vuel, string DataQr)
        {
            string codigo = "";
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
                string SiglComp = "";
                //string NombCaja = "";
                //string NombVend = "";
                string TotaIgv = "";
                string TotalLetras = "";
                string Moneda = "";
                string TotaNeto = "";
                string paramrucdni = "RUC";
                //string Medios = "";
                //string Tarjeta = "";
                int idi = Convert.ToInt32(FkVent);
                //Obtengo el Comprobante Venta
                ComprobanteVenta comprobante_venta = await CtrlCompVent.GetComprobanteVentaById(FkCompVent);
                //NombCaja = comprobante_venta.nombre_usuario_caja + " " + comprobante_venta.apellido_paterno_usuario_caja + " " + comprobante_venta.apellido_materno_usuario_caja;
                //NombVend = comprobante_venta.nombre_usuario_venta + " " + comprobante_venta.apellido_paterno_usuario_venta + " " + comprobante_venta.apellido_materno_usuario_venta;
                TotaIgv = comprobante_venta.total_igv.ToString("#,##0.00");
                TotaNeto = comprobante_venta.total_neto.ToString("#,##0.00");
                TotalLetras = comprobante_venta.total_bruto.NumeroALetras();
                //Medios = comprobante_venta.medio_pago;
                //Tarjeta = comprobante_venta.tarjeta;

                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                //List<VentaDetalle> lstVentaDetalle = await CtrlVentDeta.Get_DataDespacho(FkVent);
                //comprobante_venta = await GetComprobanteVentaById(IdCompVent);
                if (DescTipoComp.Equals("BOLETA"))
                {
                    SiglComp = "BOLETA ELECTRONICA";
                    paramrucdni = "DNI";
                }
                else if (DescTipoComp.Equals("FACTURA"))
                {
                    SiglComp = "FACTURA ELECTRONICA";
                    paramrucdni = "RUC";
                }
                string CompNro = NroCompVent;
                string DniRucClie = DniRuc;
                //DataSet data = await Get_DataComprobanteVenta(idi);
                var data = await Get_DataComprobanteVentaList(idi);
                codigo = SiglComp + " " + NroCompVent;
                if (EfecReci == null)
                {
                    EfecReci = 0;
                }
                if (Vuel == null)
                {
                    Vuel = 0;
                }
                if (data.Count > 0)
                {
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;


                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrComprobanteVenta.rpt")));
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetParameterValue("@fk_venta", FkVent);

                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData =
                            qrGenerator.CreateQrCode(DataQr, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(5);
                        qrCodeImage.Save(Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg"),
                            ImageFormat.Jpeg);
                        string path = Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg");
                        for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                        {
                            if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                            {
                                rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                            }
                        }

                        //rd.SetDataSource(data.Tables[0]);
                        rd.SetParameterValue("ParamDniRucCliente", DniRucClie);
                        rd.SetParameterValue("ParamCliente", NombClie);
                        rd.SetParameterValue("ParamDireccion", DireClie);
                        rd.SetParameterValue("ParamComprobanteNumero", CompNro);
                        rd.SetParameterValue("ParamSigla", SiglComp);
                        rd.SetParameterValue("ParamIgv", TotaIgv);
                        rd.SetParameterValue("ParamNeto", TotaNeto);
                        rd.SetParameterValue("ParamMoneda", Moneda);
                        rd.SetParameterValue("ParamLetras", TotalLetras);
                        rd.SetParameterValue("ParamRucDni", paramrucdni);
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\Doc-" + codigo + ".pdf");
                    }
                    ViewBag.Printer = "Doc-" + codigo;
                    ViewBag.Codigo = "Doc-" + codigo + ".pdf";

                }
                //return null;
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar!";
                ViewBag.Codigo = "";
            }
            return View();
        }
        public async Task<ActionResult> ExportComprobante(int FkCompVent)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            List<ComprobanteVentaFullModel> CtrlCompVent = new List<ComprobanteVentaFullModel>();
            try
            {

                string paramrucdni = "RUC";
                //Obtengo el Comprobante Venta
                CtrlCompVent = await GetComprobanteVentaByIdForPrint(FkCompVent);
                if (CtrlCompVent.Any())
                {
                    string DataQr = "";
                    string SiglComp = "";
                    string NombCaja = "";
                    string NombVend = "";
                    string TotaIgv = "";
                    string TotaNeto = "";
                    string Medios = "";
                    string Tarjeta = "";
                    string Moneda = "";
                    string TotalLetras = "";
                    int idi = Convert.ToInt32(CtrlCompVent[0].fk_venta);
                    int FkVent = idi;
                    NombCaja = CtrlCompVent[0].nombre_usuario_caja + " " + CtrlCompVent[0].apellido_paterno_usuario_caja + " " + CtrlCompVent[0].apellido_materno_usuario_caja;
                    NombVend = CtrlCompVent[0].nombre_usuario_venta + " " + CtrlCompVent[0].apellido_paterno_usuario_venta + " " + CtrlCompVent[0].apellido_materno_usuario_venta;
                    TotaIgv = CtrlCompVent[0].total_igv.ToString("#,##0.00");
                    TotaNeto = CtrlCompVent[0].total_neto.ToString("#,##0.00");
                    TotalLetras = CtrlCompVent[0].total_bruto.NumeroALetras();

                    Medios = CtrlCompVent[0].medio_pago;
                    Tarjeta = CtrlCompVent[0].tarjeta;
                    Moneda = CtrlCompVent[0].descripcion_moneda;
                    string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                    string DniRucClie = "";
                    string NombClie = "";
                    string DireClie = "";
                    string NroCompVent = CtrlCompVent[0].nro_comprobante;
                    if (CtrlCompVent[0].descripcion_comprobante_tipo.Equals("BOLETA"))
                    {
                        SiglComp = "BOLETA ELECTRONICA";
                        DniRucClie = CtrlCompVent[0].ruc_empresa_cliente_juridico;
                        NombClie = CtrlCompVent[0].razon_social;
                        DireClie = CtrlCompVent[0].direccion_empresa;
                        paramrucdni = "DNI";
                    }
                    else if (CtrlCompVent[0].descripcion_comprobante_tipo.Equals("FACTURA"))
                    {
                        SiglComp = "FACTURA ELECTRONICA";
                        DniRucClie = CtrlCompVent[0].ruc_empresa_cliente_juridico;
                        NombClie = CtrlCompVent[0].razon_social;
                        DireClie = CtrlCompVent[0].direccion_empresa;
                        paramrucdni = "RUC";
                    }
                    string CompNro = CtrlCompVent[0].nro_comprobante;
                    DataQr = CtrlCompVent[0].cadena_para_codigo_qr;

                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrComprobanteVenta.rpt")));
                    rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                    rd.SetParameterValue("@fk_venta", idi);

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData =
                        qrGenerator.CreateQrCode(DataQr, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    qrCodeImage.Save(Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg"),
                        ImageFormat.Jpeg);
                    string path = Server.MapPath("~/img/reportes/qrCV" + FkVent + ".jpg");
                    for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                    {
                        if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                        {
                            rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                        }
                    }

                    //rd.SetDataSource(CtrlCompVent);
                    rd.SetParameterValue("ParamDniRucCliente", DniRucClie);
                    rd.SetParameterValue("ParamCliente", NombClie);
                    rd.SetParameterValue("ParamDireccion", DireClie);
                    //rd.SetParameterValue("ParamCajero", NombCaja);
                    //rd.SetParameterValue("ParamVendedor", NombVend);
                    rd.SetParameterValue("ParamComprobanteNumero", CompNro);
                    rd.SetParameterValue("ParamSigla", SiglComp);
                    //rd.SetParameterValue("ParamEfectivoRecibido", Convert.ToDecimal(CtrlCompVent[0].efectivo_recibido).ToString("#,##0.00"));
                    //rd.SetParameterValue("ParamVuelto", Convert.ToDecimal(CtrlCompVent[0].vuelto).ToString("#,##0.00"));
                    rd.SetParameterValue("ParamIgv", TotaIgv);
                    rd.SetParameterValue("ParamNeto", TotaNeto);
                    rd.SetParameterValue("ParamMoneda", Moneda);
                    rd.SetParameterValue("ParamLetras", TotalLetras);
                    rd.SetParameterValue("ParamRucDni", paramrucdni);

                    //rd.SetParameterValue("Medios", Medios);
                    //rd.SetParameterValue("Tarjeta", Tarjeta);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();


                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    rd.Close();
                    rd.Dispose();
                    return File(stream, "application/pdf", "Comprobante-" + CompNro + ".pdf");

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

            // return View();
        }

        public FileStreamResult GetPDF(string codigo)
        {
            FileStream fs = new FileStream(@"C:\reportesotros\" + codigo, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }
        public ActionResult RegistroVentas()
        {
            string msgReturn = "";
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstMes.Add(new Meses { id_mes = 1, nombre_mes = "ENERO" });
                lstMes.Add(new Meses { id_mes = 2, nombre_mes = "FEBRERO" });
                lstMes.Add(new Meses { id_mes = 3, nombre_mes = "MARZO" });
                lstMes.Add(new Meses { id_mes = 4, nombre_mes = "ABRIL" });
                lstMes.Add(new Meses { id_mes = 5, nombre_mes = "MAYO" });
                lstMes.Add(new Meses { id_mes = 6, nombre_mes = "JUNIO" });
                lstMes.Add(new Meses { id_mes = 7, nombre_mes = "JULIO" });
                lstMes.Add(new Meses { id_mes = 8, nombre_mes = "AGOSTO" });
                lstMes.Add(new Meses { id_mes = 9, nombre_mes = "SETIEMBRE" });
                lstMes.Add(new Meses { id_mes = 10, nombre_mes = "OCYUBRE" });
                lstMes.Add(new Meses { id_mes = 11, nombre_mes = "NOVIEMBRE" });
                lstMes.Add(new Meses { id_mes = 12, nombre_mes = "DICIEMBRE" });

                for (int i = 6; i >= 0; i--)
                {
                    lstAnio.Add(new Anio { id_anio = anioActual, nombre_anio = anioActual });
                    anioActual--;
                }
                lstAnio = lstAnio.OrderByDescending(p => p.id_anio).ToList();

                ViewBag.Meses = lstMes.ToList();
                ViewBag.Anios = lstAnio.ToList();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
            return View();
        }

        public async Task<ActionResult> ListDataRegistroVenta(string RegiVentAnio, string RegiVentMes)
        {
            string msgReturn = "";
            List<ComprobanteVenta> lstEntidad = null;
            List<ComprobanteVenta> lstReturn = new List<ComprobanteVenta>();
            ComprobanteVenta comprobante_venta = null;
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstEntidad = await GetComprobanteVentaAll();
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.Where(p => Convert.ToInt32(Convert.ToDateTime(p.f_emision).Year).Equals(Convert.ToInt32(RegiVentAnio)) &&
                                                        Convert.ToInt32(Convert.ToDateTime(p.f_emision).Month).Equals(Convert.ToInt32(RegiVentMes))).ToList();
                    lstEntidad = lstEntidad.OrderByDescending(p => p.estado).ToList();
                    var newGroup = lstEntidad.GroupBy(i => new { i.fk_comprobante_tipo, i.nro_comprobante }).Select(i =>
                     new
                     {
                         FEmis = i.First().f_emision,
                         TotaNeto = i.First().total_neto,
                         TotaIgv = i.First().total_igv,
                         TotaBrut = i.First().total_bruto,
                         IdClieNatu = i.First().id_cliente_natural,
                         DniClieNatu = i.First().dni_cliente_natural,
                         NombClieNatu = i.First().nombre_cliente_natural,
                         ApelPateClieNatu = i.First().apellido_paterno_cliente_natural,
                         ApelMateClieNatu = i.First().apellido_materno_cliente_natural,
                         IdClieJuri = i.First().id_cliente_juridico,
                         RucClieJuri = i.First().ruc_empresa_cliente_juridico,
                         RazoSoci = i.First().razon_social,
                         Codi = i.First().codigo,
                         NroComp = i.First().nro_comprobante,
                         EstaComp = i.First().estado
                     });
                    foreach (var oneCompVent in newGroup)
                    {
                        comprobante_venta = new ComprobanteVenta
                        {
                            f_emision = oneCompVent.FEmis,
                            total_neto = oneCompVent.TotaNeto,
                            total_igv = oneCompVent.TotaIgv,
                            total_bruto = oneCompVent.TotaBrut,
                            id_cliente_natural = oneCompVent.IdClieNatu,
                            dni_cliente_natural = oneCompVent.DniClieNatu,
                            nombre_cliente_natural = oneCompVent.NombClieNatu,
                            apellido_paterno_cliente_natural = oneCompVent.ApelPateClieNatu,
                            apellido_materno_cliente_natural = oneCompVent.ApelMateClieNatu,
                            id_cliente_juridico = oneCompVent.IdClieJuri,
                            ruc_empresa_cliente_juridico = oneCompVent.RucClieJuri,
                            razon_social = oneCompVent.RazoSoci,
                            codigo = oneCompVent.Codi,
                            nro_comprobante = oneCompVent.NroComp,
                            estado = oneCompVent.EstaComp
                        };
                        lstReturn.Add(comprobante_venta);
                    }
                    lstReturn = lstReturn.OrderBy(p => p.f_emision).ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstReturn);
        }

        public async Task<List<ComprobanteVentaFullModel>> GetComprobanteVentaByIdForPrint(int IdCompVent)
        {
            List<ComprobanteVentaFullModel> entidad = null;
            try
            {
                List<ComprobanteVentaFullModel> lstEntidad = null;

                ComprobanteVentaFullModel parametro = new ComprobanteVentaFullModel { id_comprobante_venta = IdCompVent };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVenta/forprint", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVentaFullModel>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    entidad = lstEntidad;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return entidad;
        }

        public static async Task<ComprobanteVenta> UpdateComprobanteVentaRespuesta(ComprobanteVenta venta)
        {
            ComprobanteVenta entidad = null;
            try
            {
                List<ComprobanteVenta> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVenta/t_comprobante_ventaUpdateRespuesta", venta);

                var model = response.Content.ReadAsAsync<List<ComprobanteVenta>>();
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
    }
}