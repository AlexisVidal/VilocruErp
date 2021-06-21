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
    public class SolicitudTrasladoController : Controller
    {
        public async Task<SolicitudTraslado> InsertSolicitudTraslado(SolicitudTraslado solicitud_traslado)
        {
            SolicitudTraslado entidad = null;
            try
            {
                List<SolicitudTraslado> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTraslado/agregar", solicitud_traslado);

                var model = response.Content.ReadAsAsync<List<SolicitudTraslado>>();
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

        public async Task<SolicitudTraslado> UpdateSolicitudTraslado(SolicitudTraslado solicitud_traslado)
        {
            SolicitudTraslado entidad = null;
            try
            {
                List<SolicitudTraslado> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTraslado/modificar", solicitud_traslado);

                var model = response.Content.ReadAsAsync<List<SolicitudTraslado>>();
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

        public async Task<SolicitudTraslado> DeleteSolicitudTraslado(int IdSoliTras)
        {
            SolicitudTraslado entidad = null;
            try
            {
                List<SolicitudTraslado> lstEntidad = null;

                var client = new HttpClient();
                SolicitudTraslado parametro = new SolicitudTraslado { id_solicitud_traslado = IdSoliTras };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTraslado/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<SolicitudTraslado>>();
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

        public async Task<List<SolicitudTraslado>> GetSolicitudTrasladoAll()
        {
            List<SolicitudTraslado> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("SolicitudTraslado/all");
                var model = response.Content.ReadAsAsync<List<SolicitudTraslado>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.SolicitudTraslado>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<SolicitudTraslado> GetSolicitudTrasladoById(int IdSoliTras)
        {
            SolicitudTraslado entidad = null;
            try
            {
                List<SolicitudTraslado> lstEntidad = null;

                SolicitudTraslado parametro = new SolicitudTraslado { id_solicitud_traslado = IdSoliTras };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("SolicitudTraslado/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<SolicitudTraslado>>();
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

        public async Task<SolicitudTraslado> UpdateEstadoSolicitudTraslado(int IdSoliTras, string EstaSoliTras, string MotiCier)
        {
            SolicitudTraslado entidad = null;
            try
            {
                List<SolicitudTraslado> lstEntidad = null;

                var client = new HttpClient();
                SolicitudTraslado parametro = new SolicitudTraslado { id_solicitud_traslado = IdSoliTras, motivo_cierre = MotiCier, estado = EstaSoliTras };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTraslado/cambiarEstado", parametro);

                var model = response.Content.ReadAsAsync<List<SolicitudTraslado>>();
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

        // GET: SolicitudTraslado
        public ActionResult Index()
        {
            List<SolicitudTraslado> lEstados = new List<SolicitudTraslado>();
            lEstados.Add(new SolicitudTraslado { estado = "-1", NEstado = "TODOS" });
            lEstados.Add(new SolicitudTraslado { estado = "0", NEstado = "ANULADO" });
            lEstados.Add(new SolicitudTraslado { estado = "1", NEstado = "REGISTRADA" });
            lEstados.Add(new SolicitudTraslado { estado = "2", NEstado = "ATENDIDA PARCIALMENTE" });
            lEstados.Add(new SolicitudTraslado { estado = "3", NEstado = "ATENDIDA TOTALMENTE" });
            lEstados.Add(new SolicitudTraslado { estado = "4", NEstado = "FINALIZADA POR EL USUARIO" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }

        public async Task<ActionResult> ListIndex(string EstaFilt)
        {
            List<SolicitudTraslado> lstEntidad = null;
            lstEntidad = await GetSolicitudTrasladoAll();
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
                AlmacenController CtrlAlma = new AlmacenController();
                List<Almacen> lstAlmacen = null;

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
                ViewBag.Almacen = lstAlmacen;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public async Task<ActionResult> Edit(int IdSoliTras, string CallBy)
        {
            string msgReturn = "";
            SolicitudTrasladoDetalleController CtrlSoliTrasDeta = new SolicitudTrasladoDetalleController();
            AlmacenController CtrlAlma = new AlmacenController();

            List<Almacen> lstAlmacen = null;
            SolicitudTraslado solicitud_traslado = null;
            List<SolicitudTrasladoDetalle> lstSoliTrasDetalle = new List<SolicitudTrasladoDetalle>();
            try
            {
                solicitud_traslado = await GetSolicitudTrasladoById(IdSoliTras);
                if (solicitud_traslado != null)
                {
                    lstSoliTrasDetalle = await CtrlSoliTrasDeta.GetSolicitudTrasladoDetalleBySolicitudTraslado(solicitud_traslado.id_solicitud_traslado);
                    if (lstSoliTrasDetalle != null)
                    {
                        lstSoliTrasDetalle = lstSoliTrasDetalle.Where(i => !i.estado.Equals("0")).ToList();
                    }
                }
                else
                {
                    msgReturn = "Solicitud de traslado no encontrada";
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
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
                ViewBag.SolicitudTrasladoDetalle = lstSoliTrasDetalle;
                ViewBag.Almacen = lstAlmacen;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(solicitud_traslado);
        }

        public async Task<ActionResult> SaveEditSolicitudTraslado(int IdSoliTras, int FkAlmaSoli, List<List<string>> arrSoliTrasDeta, List<string> arraDeleteSoliTrasDeta)
        {
            string msgReturn = "";
            int IdNewSoliTras = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "La Solicitud de Traslado se Actualizó satisfactoriamente";
            try
            {
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);

                SolicitudTrasladoDetalleController CtrlSoliTrasDeta = new SolicitudTrasladoDetalleController();

                SolicitudTraslado solicitudTrasladoReturn = null;
                SolicitudTrasladoDetalle solicitudTrasladoDetalleReturn = null;
                SolicitudTraslado solicitud_traslado = new SolicitudTraslado
                {
                    id_solicitud_traslado = IdSoliTras,
                    fk_almacen_solicita = FkAlmaSoli,
                    fk_usuario = FkUsua
                };
                solicitudTrasladoReturn = await UpdateSolicitudTraslado(solicitud_traslado);
                if (solicitudTrasladoReturn != null)
                {
                    IdNewSoliTras = solicitudTrasladoReturn.id_solicitud_traslado;
                    //eliminamos detalles
                    if (arraDeleteSoliTrasDeta != null)
                    {
                        foreach(var oneSTDDelete in arraDeleteSoliTrasDeta)
                        {
                            solicitudTrasladoDetalleReturn = await CtrlSoliTrasDeta.DeleteSolicitudTrasladoDetalle(Convert.ToInt32(oneSTDDelete));
                            if (solicitudTrasladoDetalleReturn == null)
                            {
                                msgReturn = "Ocurrió un error al intentar Actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                                break;
                            }
                        }
                    }
                    if (flgError == 0)
                    {
                        //Agregamos o modificamos detalles
                        if (arrSoliTrasDeta != null)
                        {
                            flgReturnSave = await SaveListSolicitudTrasladoDetalle(IdNewSoliTras, arrSoliTrasDeta);
                            if (flgReturnSave == 0)
                            {
                                msgReturn = "Ocurrió un error al intentar Actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                            }
                        }
                    }
                }
                else
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
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveNewSolicitudTraslado(int FkAlmaSoli, List<List<string>> arrSoliTrasDeta)
        {
            string msgReturn = "";
            int IdNewSoliTras = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;
            msgReturn = "La Solicitud de Traslado se registró satisfactoriamente";
            try
            {
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                SolicitudTraslado solicitudTrasladoReturn = null;
                SolicitudTraslado solicitud_traslado = new SolicitudTraslado
                {
                    fk_almacen_solicita = FkAlmaSoli,
                    fk_usuario = FkUsua,
                    tipo = "1" //Generada por el usuario
                };
                solicitudTrasladoReturn = await InsertSolicitudTraslado(solicitud_traslado);
                if (solicitudTrasladoReturn != null)
                {
                    IdNewSoliTras = solicitudTrasladoReturn.id_solicitud_traslado;
                    if (arrSoliTrasDeta != null)
                    {
                        flgReturnSave = await SaveListSolicitudTrasladoDetalle(IdNewSoliTras, arrSoliTrasDeta);
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

        public async Task<int> SaveListSolicitudTrasladoDetalle(int FkSoliTras, List<List<string>> arrSoliTrasDeta)
        {
            int IdSoliTrasDeta = 0;
            int FkProd = 0;
            decimal CantSoli = 0;
            SolicitudTrasladoDetalleController CtrlSoliTrasDeta = new SolicitudTrasladoDetalleController();
            try
            {
                SolicitudTrasladoDetalle solicitudTrasladoDetalleReturn = null;
                SolicitudTrasladoDetalle solicitud_traslado_detalle = null;
                foreach (var oneSoliTrasDeta in arrSoliTrasDeta)
                {
                    IdSoliTrasDeta = Convert.ToInt32(oneSoliTrasDeta[0]);
                    FkProd = Convert.ToInt32(oneSoliTrasDeta[1]);
                    CantSoli = Convert.ToDecimal(oneSoliTrasDeta[2]);
                    if (IdSoliTrasDeta == 0)
                    {
                        //Insertamos
                        solicitud_traslado_detalle = new SolicitudTrasladoDetalle
                        {
                            fk_solicitud_traslado = FkSoliTras,
                            fk_producto = FkProd,
                            cantidad_solicitada = CantSoli
                        };

                        solicitudTrasladoDetalleReturn = await CtrlSoliTrasDeta.InsertSolicitudTrasladoDetalle(solicitud_traslado_detalle);
                        if (solicitudTrasladoDetalleReturn == null) return 0;
                    }
                    else
                    {
                        //Modificamos
                        solicitud_traslado_detalle = new SolicitudTrasladoDetalle
                        {
                            id_solicitud_traslado_detalle = IdSoliTrasDeta,
                            cantidad_solicitada = CantSoli
                        };
                        solicitudTrasladoDetalleReturn = await CtrlSoliTrasDeta.UpdateSolicitudTrasladoDetalle(solicitud_traslado_detalle);
                        if (solicitudTrasladoDetalleReturn == null) return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public async Task<ActionResult> SaveDeleteSolicitudTraslado(int IdSoliTras)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "La Solicitud de Traslado se anuló satisfactoriamente";
            try
            {
                SolicitudTraslado comprobante_venta = null;
                comprobante_venta = await DeleteSolicitudTraslado(IdSoliTras);
                if (comprobante_venta == null)
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

        public async Task<ActionResult> ListaSolicitudTraslado(string CallBy)
        {
            List<SolicitudTraslado> lstEntidad = null;
            lstEntidad = await GetSolicitudTrasladoAll();
            if (lstEntidad != null)
            {
                if (CallBy.Trim() == "ComprobanteTrasladoCreate")
                {
                    lstEntidad = lstEntidad.Where(i => i.estado.Equals("1") || i.estado.Equals("2")).ToList();
                }
                
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> SaveChangeEstadoSolicitudTraslado(int IdSoliTras, string EstaSoliTras, string MotiCier)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El proceso se concluyó satisfactoriamente";
            try
            {
                SolicitudTraslado solicitudTrasladoReturn = null;
                solicitudTrasladoReturn = await UpdateEstadoSolicitudTraslado(IdSoliTras, EstaSoliTras, MotiCier);
                if (solicitudTrasladoReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar Actualizar la Solicitud de traslado, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Actualizar la Solicitud de traslado, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }
    }
}