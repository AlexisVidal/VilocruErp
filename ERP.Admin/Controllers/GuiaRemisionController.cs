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
    public class GuiaRemisionController : Controller
    {
        public async Task<GuiaRemision> InsertGuiaRemision(GuiaRemision guia_remision)
        {
            GuiaRemision entidad = null;
            try
            {
                List<GuiaRemision> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaRemision/agregar", guia_remision);

                var model = response.Content.ReadAsAsync<List<GuiaRemision>>();
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

        public async Task<GuiaRemision> UpdateGuiaRemision(GuiaRemision guia_remision)
        {
            GuiaRemision entidad = null;
            try
            {
                List<GuiaRemision> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaRemision/modificar", guia_remision);

                var model = response.Content.ReadAsAsync<List<GuiaRemision>>();
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

        public async Task<GuiaRemision> UpdateEstadoGuiaRemision(int IdGuiaRemi, string EstaGuiaRemi)
        {
            GuiaRemision entidad = null;
            try
            {
                List<GuiaRemision> lstEntidad = null;

                var client = new HttpClient();
                GuiaRemision parametro = new GuiaRemision { id_guia_remision = IdGuiaRemi, estado = EstaGuiaRemi };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaRemision/cambiarEstado", parametro);

                var model = response.Content.ReadAsAsync<List<GuiaRemision>>();
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

        public async Task<List<GuiaRemision>> GetGuiaRemisionAll()
        {
            List<GuiaRemision> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("GuiaRemision/all");
                var model = response.Content.ReadAsAsync<List<GuiaRemision>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.GuiaRemision>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<GuiaRemision> GetGuiaRemisionById(int IdGuiaRemi)
        {
            GuiaRemision entidad = null;
            try
            {
                List<GuiaRemision> lstEntidad = null;

                GuiaRemision parametro = new GuiaRemision { id_guia_remision = IdGuiaRemi };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("GuiaRemision/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<GuiaRemision>>();
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

        // GET: GuiaRemision
        public async Task<ActionResult> Index(string EstaFilt)
        {
            List<GuiaRemision> lstEntidad = null;
            lstEntidad = await GetGuiaRemisionAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public async Task<ActionResult> Edit(int IdGuiaRemi, string CallBy)
        {
            string msgReturn = "";
            GuiaRemisionDetalleController CtrlGuiaRemiDeta = new GuiaRemisionDetalleController();
            GuiaRemision guia_remision = null;
            List<GuiaRemisionDetalle> lstGuiaRemisionDetalle = null;
            try
            {
                guia_remision = await GetGuiaRemisionById(IdGuiaRemi);
                lstGuiaRemisionDetalle = await CtrlGuiaRemiDeta.GetGuiaRemisionDetalleByFkGuiaRemi(IdGuiaRemi);
                if (lstGuiaRemisionDetalle != null)
                {
                    lstGuiaRemisionDetalle = lstGuiaRemisionDetalle.Where(i => !i.estado.Equals("0")).ToList();
                }
                ViewBag.GuiaRemisionDetalle = lstGuiaRemisionDetalle;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(guia_remision);
        }

        public async Task<ActionResult> SaveNewGuiaRemision(int FkComp, int FkCond, int FkTran, string NroGuia,
            string FechEmis, string FechTras, List<List<string>> arrGuiaRemiDeta)
        {
            string msgReturn = "";
            int rptaSaveGRD = 0;
            DateTime NewFechEmis = DateTime.Now;
            DateTime NewFechTras = DateTime.Now;
            int flgError = 0;
            msgReturn = "La Guía de Remisión se registró satisfactoriamente";
            try
            {
                GuiaRemision guiaRemisionReturn = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                NewFechTras = Convert.ToDateTime(FechTras);
                GuiaRemision guia_remision = new GuiaRemision
                {
                    fk_compra = FkComp,
                    fk_conductor = FkCond,
                    fk_transporte = FkTran,
                    nro_guia = NroGuia,
                    f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                    f_traslado = NewFechTras.ToString("yyyy/MM/dd H:mm:ss")
                };
                guiaRemisionReturn = await InsertGuiaRemision(guia_remision);
                if (guiaRemisionReturn != null)
                {
                    //Guardamos los detalles de la GR
                    if (arrGuiaRemiDeta != null)
                    {
                        rptaSaveGRD = await SaveListGuiaRemisionDetalle(guiaRemisionReturn.id_guia_remision, arrGuiaRemiDeta);
                        if (rptaSaveGRD == 0)
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
                msgReturn = "Ocurrió un error al intentar actualizar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveEditGuiaRemision(int IdGuiaRemi, int FkComp, int FkCond, int FkTran,
            string NroGuia, string FechEmis, string FechTras, string EstaGuiaRemi, List<List<string>> arrGuiaRemiDeta,
            List<string> arrGRDDelete)
        {
            string msgReturn = "";
            int rptaDelete = 0;
            int rptaUpdate = 0;
            DateTime NewFechEmis = DateTime.Now;
            DateTime NewFechTras = DateTime.Now;
            int flgError = 0;
            msgReturn = "La guia de Remisión se actualizó satisfactoriamente";
            try
            {
                GuiaRemision guiaRemiReturnUpdate = null;
                GuiaRemision guia_remision = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                NewFechTras = Convert.ToDateTime(FechTras);
                guia_remision = new GuiaRemision
                {
                    id_guia_remision = IdGuiaRemi,
                    fk_compra = FkComp,
                    fk_conductor = 0,
                    fk_transporte = 0,
                    nro_guia = NroGuia,
                    f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                    f_traslado = NewFechTras.ToString("yyyy/MM/dd H:mm:ss"),
                    estado = EstaGuiaRemi
                };
                guiaRemiReturnUpdate = await UpdateGuiaRemision(guia_remision);
                if (guiaRemiReturnUpdate != null)
                {
                    //Eliminamos los detalles
                    if (arrGRDDelete != null)
                    {
                        rptaDelete = await DeleteListGuiaRemisionDetalle(arrGRDDelete);
                        if (rptaDelete == 0)
                        {
                            msgReturn = "Ocurrió un error al intentar Actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                    if (flgError == 0)
                    {
                        //Actualizamos los detalle
                        if (arrGuiaRemiDeta != null)
                        {
                            rptaUpdate = await SaveListGuiaRemisionDetalle(IdGuiaRemi, arrGuiaRemiDeta);
                            if (rptaUpdate == 0)
                            {
                                msgReturn = "Ocurrió un error al intentar Actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
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
                msgReturn = "Ocurrió un error al intentar actualizar la OC, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveChangeEstadoGuiaRemision(int IdGuiaRemi, string EstaGuiaRemi)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El proceso se concluyó satisfactoriamente";
            try
            {
                GuiaRemision guiaRemiReturn = null;
                guiaRemiReturn = await UpdateEstadoGuiaRemision(IdGuiaRemi, EstaGuiaRemi);
                if (guiaRemiReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar Actualizar la Guía de Remisión, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Actualizar la Guía de Remisión, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveFinalizarGuiaRemision(int IdGuiaRemi, int FkAlma, string EstaGuiaRemi)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El proceso se concluyó satisfactoriamente";
            try
            {
                GuiaRemisionDetalleController CtrlGuiaRemiDeta = new GuiaRemisionDetalleController();
                MovimientoController CtrlMovi = new MovimientoController();
                List<GuiaRemisionDetalle> lstGuiaRemisionDetalle = null;
                GuiaRemision guiaRemiReturn = null;
                Movimiento movimiento = null;
                Movimiento movimientoReturn = null;

                int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_Compra"]);
                lstGuiaRemisionDetalle = await CtrlGuiaRemiDeta.GetGuiaRemisionDetalleByFkGuiaRemi(IdGuiaRemi);
                if (lstGuiaRemisionDetalle != null)
                {
                    foreach (var oneGuiaRemiDeta in lstGuiaRemisionDetalle)
                    {
                        //almacen_stock = new AlmacenStock
                        //{
                        //    fk_almacen = FkAlma,
                        //    fk_producto = oneGuiaRemiDeta.fk_producto,
                        //    existencia = oneGuiaRemiDeta.cantidad,
                        //    pto_limite = 0
                        //};
                        //AlmaStocReturn = await CtrlAlmaStoc.InsertAlmacenStock(almacen_stock);
                        //if (AlmaStocReturn == null)
                        //{
                        //    msgReturn = "Ocurrió un error al intentar finalizar los stocks, Por favor comuniquese con el administrador de sistemas";
                        //    flgError = 1;
                        //    break;
                        //}
                        //Insertamos en la tabla t_movimiento
                        movimiento = new Movimiento
                        {
                            fk_movimiento_tipo = FkMoviTipo,
                            fk_guia_remision_detalle = oneGuiaRemiDeta.id_guia_remision_detalle,
                            fk_venta_detalle = 0,
                            fk_comprobante_traslado_detalle = 0,
                            fk_nota_credito_detalle = 0,
                            fk_almacen = FkAlma,
                            fk_producto = oneGuiaRemiDeta.fk_producto,
                            cantidad = oneGuiaRemiDeta.cantidad
                        };
                        movimientoReturn = await CtrlMovi.InsertMovimiento(movimiento);
                    }
                    if (flgError == 0)
                    {
                        guiaRemiReturn = await UpdateEstadoGuiaRemision(IdGuiaRemi, EstaGuiaRemi);
                        if (guiaRemiReturn == null)
                        {
                            msgReturn = "Los stocks fueron actualizados pero ocurrió un error al intentar cambiar de estado a la Guía de Remisión, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                }
                else
                {
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
                msgReturn = "Ocurrió un error al intentar finalizar la Guía de Remisión, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> DeleteListGuiaRemisionDetalle(List<string> arrGRDDelete)
        {
            GuiaRemisionDetalleController CtrlGRDeta = new GuiaRemisionDetalleController();
            GuiaRemisionDetalle guiaRemiDetaReturnDelete = null;
            int rptaDeleOCDeta = 1;
            try
            {
                foreach (var oneIdGuiaRemiDeta in arrGRDDelete)
                {
                    guiaRemiDetaReturnDelete = await CtrlGRDeta.DeleteGuiaRemisionDetalle(Convert.ToInt32(oneIdGuiaRemiDeta));
                    if (guiaRemiDetaReturnDelete == null)
                    {
                        rptaDeleOCDeta = 0;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return rptaDeleOCDeta;
        }

        public async Task<int> SaveListGuiaRemisionDetalle(int FkGuiaRemi, List<List<string>> arrGuiaRemiDeta)
        {
            try
            {
                GuiaRemisionDetalleController CtrlGuiaRemiDeta = new GuiaRemisionDetalleController();
                GuiaRemisionDetalle guia_remision_detalle = null;
                GuiaRemisionDetalle guiaRemiDetaReturn = null;
                foreach (var oneGuiaRemiDeta in arrGuiaRemiDeta)
                {
                    if (Convert.ToInt32(oneGuiaRemiDeta[0]) == 0)
                    {
                        //Si es nuevo lo insertamos
                        guia_remision_detalle = new GuiaRemisionDetalle
                        {
                            fk_guia_remision = FkGuiaRemi,
                            fk_compra_detalle = Convert.ToInt32(oneGuiaRemiDeta[1]),
                            cantidad = Convert.ToDecimal(oneGuiaRemiDeta[2])
                        };
                        guiaRemiDetaReturn = await CtrlGuiaRemiDeta.InsertGuiaRemisionDetalle(guia_remision_detalle);
                        if (guiaRemiDetaReturn == null)
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        //Modificamos el registro
                        guia_remision_detalle = new GuiaRemisionDetalle
                        {
                            id_guia_remision_detalle = Convert.ToInt32(oneGuiaRemiDeta[0]),
                            fk_guia_remision = FkGuiaRemi,
                            fk_compra_detalle = Convert.ToInt32(oneGuiaRemiDeta[1]),
                            cantidad = Convert.ToDecimal(oneGuiaRemiDeta[2]),
                            estado = oneGuiaRemiDeta[3]
                        };
                        guiaRemiDetaReturn = await CtrlGuiaRemiDeta.UpdateGuiaRemisionDetalle(guia_remision_detalle);
                        if (guiaRemiDetaReturn == null)
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public async Task<List<GuiaRemision>> Get_GuiaRemisionByFkCompra(int FkComp)
        {
            List<GuiaRemision> lstEntidad = null;
            try
            {
                lstEntidad = await GetGuiaRemisionAll();
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.Where(i => i.fk_compra.Equals(FkComp) &&
                                                    !i.estado.Equals("0")).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<JsonResult> GetJson_GuiaRemisionByFkCompraPorFacturar(int FkComp, int? FkCompComp)
        {
            GuiaComprobanteController CtrlGuiaComp = new GuiaComprobanteController();
            GuiaRemisionController CtrlGuiaRemi = new GuiaRemisionController();
            List<GuiaRemision> lstEntidad = null;
            List<GuiaComprobante> lstGuiaComp = null;
            GuiaRemision guia_remision = null;
            string msgReturn = "";
            try
            {
                lstEntidad = await Get_GuiaRemisionByFkCompra(FkComp);
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.Where(i => i.estado.Equals("2")).ToList();
                }
                if (FkCompComp != null)
                {
                    lstGuiaComp = await CtrlGuiaComp.Get_GuiaComprobanteByFkCompComp(Convert.ToInt32(FkCompComp));
                    if (lstGuiaComp != null)
                    {
                        lstGuiaComp = lstGuiaComp.Where(i => !i.estado.Equals("0")).ToList();
                        foreach (var oneGuiaComp in lstGuiaComp)
                        {
                            guia_remision = await CtrlGuiaRemi.GetGuiaRemisionById(oneGuiaComp.fk_guia_remision);
                            if (guia_remision != null)
                            {
                                lstEntidad.Add(guia_remision);
                            }
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
            return Json(lstEntidad);
        }
    }
}