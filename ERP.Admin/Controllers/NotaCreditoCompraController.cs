using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ERP.Admin.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using ERP.Admin.App_Start;

namespace ERP.Admin.Controllers
{
    public class NotaCreditoCompraController : Controller
    {
        public async Task<NotaCreditoCompra> InsertNotaCreditoCompra(NotaCreditoCompra nota_credito_compra)
        {
            NotaCreditoCompra entidad = null;
            try
            {
                List<NotaCreditoCompra> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaCreditoCompra/agregar", nota_credito_compra);

                var model = response.Content.ReadAsAsync<List<NotaCreditoCompra>>();
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

        public async Task<NotaCreditoCompra> GetNotaCreditoCompraById(int IdNotaCredComp)
        {
            NotaCreditoCompra entidad = null;
            try
            {
                List<NotaCreditoCompra> lstEntidad = null;

                NotaCreditoCompra parametro = new NotaCreditoCompra { id_nota_credito_compra = IdNotaCredComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("NotaCreditoCompra/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<NotaCreditoCompra>>();
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

        public async Task<List<NotaCreditoCompra>> GetNotaCreditoCompraAll()
        {
            List<NotaCreditoCompra> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("NotaCreditoCompra/all");
                var model = response.Content.ReadAsAsync<List<NotaCreditoCompra>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.NotaCreditoCompra>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: NotaCreditoProveedor
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ListIndex(string EstaFilt)
        {
            List<NotaCreditoCompra> lstEntidad = null;
            lstEntidad = await GetNotaCreditoCompraAll();
            //if (EstaFilt != null && EstaFilt != "-1")
            //{
            //    lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            //}
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> Create(int IdCompComp, string CallBy)
        {
            string msgReturn = "";
            ComprobanteCompraController CtrlCompComp = new ComprobanteCompraController();
            GuiaRemisionDetalleController CtrlGuiaRemiDeta = new GuiaRemisionDetalleController();
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            GuiaComprobanteController CtrlGuiaCompComp = new GuiaComprobanteController();
            ComprobanteCompra comprobante_compra = null;
            List<GuiaComprobante> lstGuiaComprobante = null;
            List<GuiaRemisionDetalle> lstGuiaRemisionDetalle = new List<GuiaRemisionDetalle>();
            List<ComprobanteTipo> lstComprobanteTipo = null;
            List<string> lstIdGuiaRemi = new List<string>();
            int FkCompTipoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_NotaCredito"]);
            try
            {
                comprobante_compra = await CtrlCompComp.GetComprobanteCompraById(IdCompComp);
                if (comprobante_compra != null)
                {
                    lstGuiaComprobante = await CtrlGuiaCompComp.Get_GuiaComprobanteByFkCompComp(comprobante_compra.id_comprobante_compra);
                    if (lstGuiaComprobante != null)
                    {
                        lstGuiaComprobante = lstGuiaComprobante.Where(i => !i.estado.Equals("0")).ToList();
                        foreach (var oneGuiaComp in lstGuiaComprobante)
                        {
                            lstIdGuiaRemi.Add(oneGuiaComp.fk_guia_remision.ToString());
                        }
                        lstGuiaRemisionDetalle = await CtrlGuiaRemiDeta.Get_GuiaRemisionDetalleGroup_ListFkGR(lstIdGuiaRemi);
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

                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.IdGuiaRemision = lstIdGuiaRemi;
                ViewBag.GuiaRemisionDetalle = lstGuiaRemisionDetalle;
                ViewBag.CallBy = CallBy;
                ViewBag.FkCompTipoNotaCredito = FkCompTipoNotaCred;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(comprobante_compra);
        }

        public async Task<ActionResult> SaveNewNotaCredito(int FkCompComp, string NroCompNC, string FechEmisNC,
            decimal TotaBrutNC, decimal TotaIgvNC, decimal TotaNetoNC, string MotiNC, string flgAfecStoc,
            List<List<string>> arrNotaCredDeta, List<List<string>> arrAlmaStoc)
        {
            string msgReturn = "";
            int IdNewNotaCredComp = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "La nota de crédito se generó satisfactoriamente";
            int FkCompTipoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_NotaCredito"]);
            try
            {
                VentaController CtrlVent = new VentaController();
                NotaCreditoCompra notaCreditoCompraReturn = null;
                NotaCreditoCompra nota_credito_compra = null;
                NewFechEmis = Convert.ToDateTime(FechEmisNC);
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);

                nota_credito_compra = new NotaCreditoCompra
                {
                    fk_comprobante_compra = FkCompComp,
                    fk_usuario = FkUsua,
                    nro_comprobante = NroCompNC,
                    f_emision = NewFechEmis,
                    total_bruto = TotaBrutNC,
                    total_igv = TotaIgvNC,
                    total_isc = 0,
                    total_neto = TotaNetoNC,
                    motivo = MotiNC,
                    flg_afecta_stock = flgAfecStoc
                };
                notaCreditoCompraReturn = await InsertNotaCreditoCompra(nota_credito_compra);
                if (notaCreditoCompraReturn != null)
                {
                    IdNewNotaCredComp = notaCreditoCompraReturn.id_nota_credito_compra;
                    if (arrNotaCredDeta != null)
                    {
                        flgReturnSave = await SaveListNotaCreditoCompraDetalle(IdNewNotaCredComp, arrNotaCredDeta, flgAfecStoc, arrAlmaStoc);
                        if (flgReturnSave == 0)
                        {
                            msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
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
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> SaveListNotaCreditoCompraDetalle(int FkNotaCred, List<List<string>> arrNotaCredDeta, string flgAfecStoc, List<List<string>> arrAlmaStoc)
        {
            int flgSaveMovi = 0;
            NotaCreditoCompraDetalleController CtrlNotaCredCompDeta = new NotaCreditoCompraDetalleController();
            try
            {
                NotaCreditoCompraDetalle notaCreditoCompraDetalleReturn = null;
                NotaCreditoCompraDetalle nota_credito_compra_detalle = null;
                foreach (var oneDetaVent in arrNotaCredDeta)
                {
                    nota_credito_compra_detalle = new NotaCreditoCompraDetalle
                    {
                        fk_nota_credito_compra = FkNotaCred,
                        fk_producto = Convert.ToInt32(oneDetaVent[0]),
                        cantidad = Convert.ToDecimal(oneDetaVent[1]),
                        precio = Convert.ToDecimal(oneDetaVent[2])
                    };
                    notaCreditoCompraDetalleReturn = await CtrlNotaCredCompDeta.InsertNotaCreditoCompraDetalle(nota_credito_compra_detalle);
                    if (notaCreditoCompraDetalleReturn == null) return 0;
                }
                //Guardamos en t_movimiento
                if (flgAfecStoc.Equals("1"))
                {
                    foreach(var oneAlmaStoc in arrAlmaStoc)
                    {
                        flgSaveMovi = await SaveMovimiento(notaCreditoCompraDetalleReturn.id_nota_credito_compra_detalle, Convert.ToInt32(oneAlmaStoc[0]), Convert.ToInt32(oneAlmaStoc[1]), Convert.ToDecimal(oneAlmaStoc[2]));
                        if (flgSaveMovi == 0) return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public async Task<int> SaveMovimiento(int FkNotaCredDeta, int FkAlma, int FkProd, decimal Cant)
        {
            MovimientoController CtrlMovi = new MovimientoController();
            Movimiento movimiento = null;
            Movimiento movimientoReturn = null;
            try
            {
                int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_SalidaNotaCredito"]);
                //Insertamos en la tabla t_movimiento
                movimiento = new Movimiento
                {
                    fk_movimiento_tipo = FkMoviTipo,
                    fk_guia_remision_detalle = 0,
                    fk_venta_detalle = 0,
                    fk_comprobante_traslado_detalle = 0,
                    fk_nota_credito_detalle = FkNotaCredDeta,
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

        public async Task<ActionResult> Edit(int IdNotaCredComp, string CallBy)
        {
            string msgReturn = "";
            NotaCreditoCompraDetalleController CtrlNotaCredCompDeta = new NotaCreditoCompraDetalleController();
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();

            NotaCreditoCompra nota_credito_compra = null;
            List<NotaCreditoCompraDetalle> lstNotaCreditoCompraDetalle = new List<NotaCreditoCompraDetalle>();
            List<ComprobanteTipo> lstComprobanteTipo = null;

            int FkCompTipoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_NotaCredito"]);

            try
            {
                nota_credito_compra = await GetNotaCreditoCompraById(IdNotaCredComp);
                if (nota_credito_compra != null)
                {
                    lstNotaCreditoCompraDetalle = await CtrlNotaCredCompDeta.GetNotaCreditoCompraDetalleByNotaCredito(IdNotaCredComp);
                    if (lstNotaCreditoCompraDetalle != null)
                    {
                        lstNotaCreditoCompraDetalle = lstNotaCreditoCompraDetalle.Where(i => !i.estado.Equals("0")).ToList();
                    }
                }
                else
                {
                    msgReturn = "Comprobante no encontrado";
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();

                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.NotaCreditoCompraDetalle = lstNotaCreditoCompraDetalle;
                ViewBag.CallBy = CallBy;
                ViewBag.FkCompTipoNotaCredito = FkCompTipoNotaCred;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(nota_credito_compra);
        }

        public async Task<ActionResult> ViewSelecDescargarStock(List<List<string>> arraIdProd)
        {
            string msgReturn = "";
            AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
            List<AlmacenStock> lstReturnAlmacenStock = new List<AlmacenStock>();
            List<AlmacenStock> lstTempAlmacenStock = null;
            try
            {
                foreach(var oneProd in arraIdProd)
                {
                    lstTempAlmacenStock = await CtrlAlmaStoc.GetAlmacenStockProducto(Convert.ToInt32(oneProd[0]));
                    if (lstTempAlmacenStock != null)
                    {
                        foreach(var oneAlmaStoc in lstTempAlmacenStock)
                        {
                            oneAlmaStoc.cantidad_descontar = Convert.ToDecimal(oneProd[1]);
                            lstReturnAlmacenStock.Add(oneAlmaStoc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstReturnAlmacenStock);
        }
    }
}