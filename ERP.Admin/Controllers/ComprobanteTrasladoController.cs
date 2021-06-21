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
    public class ComprobanteTrasladoController : Controller
    {
        public async Task<ComprobanteTraslado> InsertComprobanteTraslado(ComprobanteTraslado comprobante_traslado)
        {
            ComprobanteTraslado entidad = null;
            try
            {
                List<ComprobanteTraslado> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteTraslado/agregar", comprobante_traslado);

                var model = response.Content.ReadAsAsync<List<ComprobanteTraslado>>();
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

        public async Task<ComprobanteTraslado> DeleteComprobanteTraslado(int IdCompTras)
        {
            ComprobanteTraslado entidad = null;
            try
            {
                List<ComprobanteTraslado> lstEntidad = null;

                var client = new HttpClient();
                ComprobanteTraslado parametro = new ComprobanteTraslado { id_comprobante_traslado = IdCompTras };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteTraslado/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteTraslado>>();
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

        public async Task<ComprobanteTraslado> RecepcionarComprobanteTraslado(int IdCompTras)
        {
            ComprobanteTraslado entidad = null;
            try
            {
                List<ComprobanteTraslado> lstEntidad = null;

                var client = new HttpClient();
                ComprobanteTraslado parametro = new ComprobanteTraslado { id_comprobante_traslado = IdCompTras };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteTraslado/recepcionar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteTraslado>>();
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

        public async Task<List<ComprobanteTraslado>> GetComprobanteTrasladoAll()
        {
            List<ComprobanteTraslado> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteTraslado/all");
                var model = response.Content.ReadAsAsync<List<ComprobanteTraslado>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ComprobanteTraslado>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ComprobanteTraslado> GetComprobanteTrasladoById(int IdCompTras)
        {
            ComprobanteTraslado entidad = null;
            try
            {
                List<ComprobanteTraslado> lstEntidad = null;

                ComprobanteTraslado parametro = new ComprobanteTraslado { id_comprobante_traslado = IdCompTras };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteTraslado/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteTraslado>>();
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

        // GET: ComprobanteTraslado
        public ActionResult Index()
        {
            List<ComprobanteTraslado> lEstados = new List<ComprobanteTraslado>();
            lEstados.Add(new ComprobanteTraslado { estado = "-1", NEstado = "TODOS" });
            lEstados.Add(new ComprobanteTraslado { estado = "0", NEstado = "ANULADO" });
            lEstados.Add(new ComprobanteTraslado { estado = "1", NEstado = "POR RECEPCIONAR" });
            lEstados.Add(new ComprobanteTraslado { estado = "2", NEstado = "RECEPCIONADO" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }

        public async Task<ActionResult> ListIndex(string EstaFilt)
        {
            List<ComprobanteTraslado> lstEntidad = null;
            lstEntidad = await GetComprobanteTrasladoAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> Create()
        {
            string msgReturn = "";
            try
            {
                ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
                AlmacenController CtrlAlma = new AlmacenController();
                List<ComprobanteTipo> lstComprobanteTipo = null;
                List<Almacen> lstAlmacen = null;

                int FkCompTipo_Default = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_GuiaRemision"]);

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
                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.AlmacenOrigen = lstAlmacen;
                ViewBag.AlmacenDestino = lstAlmacen;
                ViewBag.FkComprobanteTipoDefault = FkCompTipo_Default;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public async Task<ActionResult> Recepcionar(int IdCompTras, string CallBy)
        {
            string msgReturn = "";
            ComprobanteTraslado comprobante_traslado = null;
            try
            {
                ComprobanteTrasladoDetalleController CtrlCompTrasDeta = new ComprobanteTrasladoDetalleController();

                List<ComprobanteTrasladoDetalle> lstComprobanteTrasladoDetalle = null;

                comprobante_traslado = await GetComprobanteTrasladoById(IdCompTras);
                if (comprobante_traslado != null)
                {
                    lstComprobanteTrasladoDetalle = await CtrlCompTrasDeta.GetComprobanteTrasladoDetalleByComprobanteTraslado(comprobante_traslado.id_comprobante_traslado);
                    if (lstComprobanteTrasladoDetalle != null)
                    {
                        if(CallBy == "ComprobanteTrasladoRecepcionar")
                        {
                            lstComprobanteTrasladoDetalle = lstComprobanteTrasladoDetalle.Where(i => !i.estado.Equals("0")).ToList();
                        }
                    }
                }
                else
                {
                    msgReturn = "Comprobante de traslado no encontrado";
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
                ViewBag.ComprobanteTrasladoDetalle = lstComprobanteTrasladoDetalle;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(comprobante_traslado);
        }

        public async Task<ActionResult> SaveRecepcionarComprobanteTraslado(int IdCompTras, int FkAlma, List<List<string>> arrCompTrasDeta)
        {
            string msgReturn = "";
            int flgError = 0;
            int IdCompTrasDeta = 0;
            int FkProd = 0;
            decimal Cant = 0;
            msgReturn = "La recepción se registró satisfactoriamente";
            try
            {
                MovimientoController CtrlMovi = new MovimientoController();

                ComprobanteTraslado comprobanteTrasladoReturn = null;
                Movimiento movimientoReturn = null;
                Movimiento movimiento = null;
                int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_IngresoTraslado"]);

                comprobanteTrasladoReturn = await RecepcionarComprobanteTraslado(IdCompTras);
                if (comprobanteTrasladoReturn != null)
                {
                    if (arrCompTrasDeta != null)
                    {
                        foreach (var oneCompTrasDeta in arrCompTrasDeta)
                        {
                            IdCompTrasDeta = Convert.ToInt32(oneCompTrasDeta[0]);
                            FkProd = Convert.ToInt32(oneCompTrasDeta[1]);
                            Cant = Convert.ToDecimal(oneCompTrasDeta[2]);
                            //Insertamos en la tabla t_movimiento
                            movimiento = new Movimiento
                            {
                                fk_movimiento_tipo = FkMoviTipo,
                                fk_guia_remision_detalle = 0,
                                fk_venta_detalle = 0,
                                fk_comprobante_traslado_detalle = IdCompTrasDeta,
                                fk_nota_credito_detalle = 0,
                                fk_almacen = FkAlma,
                                fk_producto = FkProd,
                                cantidad = Cant
                            };
                            movimientoReturn = await CtrlMovi.InsertMovimiento(movimiento);
                            if (movimientoReturn == null)
                            {

                            }
                        }
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar recepcionar, Por favor comuniquese con el administrador de sistemas";
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

        public async Task<ActionResult> SaveNewComprobanteTraslado(int FlgConSinSoliTras, int FkCompTipo, int FkSoliTras,
            int FkAlmaDesp, int FkAlmaSoli, string NroComp, List<List<string>> arrCompTrasDeta)
        {
            string msgReturn = "";
            int IdNewCompTras = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "El comprobante de traslado de generó satisfactoriamente";
            try
            {
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                SolicitudTrasladoController CtrlSoliTras = new SolicitudTrasladoController();
                SolicitudTrasladoDetalleController CtrlSoliTrasDeta = new SolicitudTrasladoDetalleController();

                SolicitudTraslado solicitudTrasladoReturn = null;
                SolicitudTrasladoDetalle solicitudTrasladoDetalleReturn = null;
                SolicitudTrasladoDetalle solicitud_traslado_detalle = null;

                ComprobanteTraslado comprobanteTrasladoReturn = null;

                if (FlgConSinSoliTras == 0)
                {
                    //Si no viene de solicitud de traslado, instamos una solicitud automatica
                    SolicitudTraslado solicitud_traslado = new SolicitudTraslado
                    {
                        fk_almacen_solicita = FkAlmaSoli,
                        fk_usuario = FkUsua,
                        tipo = "2" //Generada Automaticamente
                    };
                    solicitudTrasladoReturn = await CtrlSoliTras.InsertSolicitudTraslado(solicitud_traslado);
                    if (solicitudTrasladoReturn != null)
                    {
                        FkSoliTras = solicitudTrasladoReturn.id_solicitud_traslado;
                        if (arrCompTrasDeta != null)
                        {
                            for (int i = 0; i < arrCompTrasDeta.Count; i++)
                            {
                                solicitud_traslado_detalle = new SolicitudTrasladoDetalle
                                {
                                    fk_solicitud_traslado = FkSoliTras,
                                    fk_producto = Convert.ToInt32(arrCompTrasDeta[i][2]),
                                    cantidad_solicitada = Convert.ToDecimal(arrCompTrasDeta[i][3])
                                };
                                solicitudTrasladoDetalleReturn = await CtrlSoliTrasDeta.InsertSolicitudTrasladoDetalle(solicitud_traslado_detalle);
                                if (solicitudTrasladoDetalleReturn != null)
                                {
                                    arrCompTrasDeta[i][1] = solicitudTrasladoDetalleReturn.id_solicitud_traslado_detalle.ToString();
                                }
                                else
                                {
                                    msgReturn = "Ocurrió un error al intentar Registrar los detalles de la solicitud de traslado autómatica, Por favor comuniquese con el administrador de sistemas";
                                    flgError = 1;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        msgReturn = "Ocurrió un error al intentar generar solicitud de traslado automática, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                if (flgError == 0)
                {
                    ComprobanteTraslado comprobante_traslado = new ComprobanteTraslado
                    {
                        fk_comprobante_tipo = FkCompTipo,
                        fk_solicitud_traslado = FkSoliTras,
                        fk_almacen_despacho = FkAlmaDesp,
                        nro_comprobante = ""
                    };
                    comprobanteTrasladoReturn = await InsertComprobanteTraslado(comprobante_traslado);
                    IdNewCompTras = comprobanteTrasladoReturn.id_comprobante_traslado;
                    if (arrCompTrasDeta != null)
                    {
                        flgReturnSave = await SaveListComprobanteTrasladoDetalle(IdNewCompTras, FkAlmaDesp, arrCompTrasDeta);
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

        public async Task<ActionResult> SaveDeleteComprobanteTraslado(int IdCompTras)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Comprobante se anuló satisfactoriamente";
            try
            {
                ComprobanteTraslado comprobante_traslado = null;
                comprobante_traslado = await DeleteComprobanteTraslado(IdCompTras);
                if (comprobante_traslado == null)
                {
                    msgReturn = "Ocurrió un error al intentar Anular, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Anular, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> SaveListComprobanteTrasladoDetalle(int FkCompTras, int FkAlma, List<List<string>> arrCompTrasDeta)
        {
            int IdCompTrasDeta = 0;
            int FkSoliTrasDeta = 0;
            int FkProd = 0;
            decimal Cant = 0;
            ComprobanteTrasladoDetalleController CtrlCompTrasDeta = new ComprobanteTrasladoDetalleController();
            MovimientoController CtrlMovi = new MovimientoController();
            int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_SalidaTraslado"]);
            try
            {
                ComprobanteTrasladoDetalle comprobanteTrasladoDetalleReturn = null;
                ComprobanteTrasladoDetalle comprobante_traslado_detalle = null;
                Movimiento movimiento = null;
                Movimiento movimientoReturn = null;
                foreach (var oneSoliTrasDeta in arrCompTrasDeta)
                {
                    IdCompTrasDeta = Convert.ToInt32(oneSoliTrasDeta[0]);
                    FkSoliTrasDeta = Convert.ToInt32(oneSoliTrasDeta[1]);
                    FkProd = Convert.ToInt32(oneSoliTrasDeta[2]);
                    Cant = Convert.ToDecimal(oneSoliTrasDeta[3]);
                    if (IdCompTrasDeta == 0)
                    {
                        //Insertamos
                        comprobante_traslado_detalle = new ComprobanteTrasladoDetalle
                        {
                            fk_comprobante_traslado = FkCompTras,
                            fk_solicitud_traslado_detalle = FkSoliTrasDeta,
                            cantidad = Cant
                        };

                        comprobanteTrasladoDetalleReturn = await CtrlCompTrasDeta.InsertComprobanteTrasladoDetalle(comprobante_traslado_detalle);
                        if (comprobanteTrasladoDetalleReturn == null) return 0;
                        //Insertamos en la tabla t_movimiento
                        movimiento = new Movimiento
                        {
                            fk_movimiento_tipo = FkMoviTipo,
                            fk_guia_remision_detalle = 0,
                            fk_venta_detalle = 0,
                            fk_comprobante_traslado_detalle = comprobanteTrasladoDetalleReturn.id_comprobante_traslado_detalle,
                            fk_nota_credito_detalle = 0,
                            fk_almacen = FkAlma,
                            fk_producto = FkProd,
                            cantidad = Cant
                        };
                        movimientoReturn = await CtrlMovi.InsertMovimiento(movimiento);
                        if (movimientoReturn == null) return 0;
                    }
                    //else
                    //{
                    //    //Modificamos
                    //    solicitud_traslado_detalle = new SolicitudTrasladoDetalle
                    //    {
                    //        id_solicitud_traslado_detalle = IdSoliTrasDeta,
                    //        cantidad_solicitada = CantSoli
                    //    };
                    //    solicitudTrasladoDetalleReturn = await CtrlSoliTrasDeta.UpdateSolicitudTrasladoDetalle(solicitud_traslado_detalle);
                    //    if (solicitudTrasladoDetalleReturn == null) return 0;
                    //}
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
    }
}