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
    public class ComprobanteCompraController : Controller
    {
        public async Task<ComprobanteCompra> InsertComprobanteCompra(ComprobanteCompra comprobante_compra)
        {
            ComprobanteCompra entidad = null;
            try
            {
                List<ComprobanteCompra> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteCompra/agregar", comprobante_compra);

                var model = response.Content.ReadAsAsync<List<ComprobanteCompra>>();
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

        public async Task<ComprobanteCompra> UpdateComprobanteCompra(ComprobanteCompra comprobante_compra)
        {
            ComprobanteCompra entidad = null;
            try
            {
                List<ComprobanteCompra> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteCompra/modificar", comprobante_compra);

                var model = response.Content.ReadAsAsync<List<ComprobanteCompra>>();
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

        public async Task<ComprobanteCompra> DeleteComprobanteCompra(int IdCompComp)
        {
            ComprobanteCompra entidad = null;
            try
            {
                List<ComprobanteCompra> lstEntidad = null;

                var client = new HttpClient();
                ComprobanteCompra parametro = new ComprobanteCompra { id_comprobante_compra = IdCompComp };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteCompra/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteCompra>>();
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

        public async Task<List<ComprobanteCompra>> GetComprobanteCompraAll()
        {
            List<ComprobanteCompra> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteCompra/all");
                var model = response.Content.ReadAsAsync<List<ComprobanteCompra>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ComprobanteCompra>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ComprobanteCompra> GetComprobanteCompraById(int IdCompComp)
        {
            ComprobanteCompra entidad = null;
            try
            {
                List<ComprobanteCompra> lstEntidad = null;

                ComprobanteCompra parametro = new ComprobanteCompra { id_comprobante_compra = IdCompComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteCompra/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteCompra>>();
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
            try
            {
                ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
                List<ComprobanteTipo> lstComprobanteTipo = null;
                int FkCompTipoFact = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_Factura"]);

                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
                if (lstComprobanteTipo != null)
                {
                    lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0")).ToList();
                }
                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.FkTipoComprobanteFactura = FkCompTipoFact;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public async Task<ActionResult> Edit(int IdGuiaRemi, string CallBy)
        {
            string msgReturn = "";
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            GuiaRemisionDetalleController CtrlGuiaRemiDeta = new GuiaRemisionDetalleController();
            GuiaComprobanteController CtrlGuiaCompComp = new GuiaComprobanteController();
            ComprobanteCompra comprobante_compra = null;
            List<GuiaComprobante> lstGuiaComprobante = null;
            List<GuiaRemisionDetalle> lstGuiaRemisionDetalle = new List<GuiaRemisionDetalle>();
            List<ComprobanteTipo> lstComprobanteTipo = null;
            List<string> lstIdGuiaRemi = new List<string>();
            try
            {
                comprobante_compra = await GetComprobanteCompraById(IdGuiaRemi);
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
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(comprobante_compra);
        }

        // GET: ComprobanteCompra
        public async Task<ActionResult> Index(string EstaFilt)
        {
            List<ComprobanteCompra> lstEntidad = null;
            lstEntidad = await GetComprobanteCompraAll();
            lstEntidad = lstEntidad.Where(p => p.tipo_compra.Equals("1")).ToList();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> SaveNewComprobanteCompra(int FkComp, int FkProv, int FkCompTipo, int FkTipoMone,
            string NroCompComp, string FechEmis, decimal totaBrut, decimal? TipoCamb, List<string> arrFkGuiaRemi)
        {
            string msgReturn = "";
            int IdNewCompComp = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            msgReturn = "El Comprobante se registró satisfactoriamente";
            try
            {
                GuiaComprobanteController CtrlGuiaComp = new GuiaComprobanteController();
                ComprobanteCompra comprobanteCompraReturn = null;
                GuiaComprobante guia_comprobante = null;
                GuiaComprobante guiaComprobanteReturn = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);

                ComprobanteCompra comprobante_compra = new ComprobanteCompra
                {
                    fk_compra = FkComp,
                    fk_proveedor = FkProv,
                    fk_comprobante_tipo = FkCompTipo,
                    fk_medio_pago = FkMediPagoLineCred,
                    fk_tipo_moneda = FkTipoMone,
                    nro_comprobante = NroCompComp,
                    f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                    total_bruto = totaBrut,
                    tipo_cambio = Convert.ToDecimal(TipoCamb),
                    tipo_compra = "1"
                };
                comprobanteCompraReturn = await InsertComprobanteCompra(comprobante_compra);
                if (comprobanteCompraReturn != null)
                {
                    IdNewCompComp = comprobanteCompraReturn.id_comprobante_compra;
                    foreach (var oneIdGuiaRemi in arrFkGuiaRemi)
                    {
                        guia_comprobante = new GuiaComprobante
                        {
                            fk_guia_remision = Convert.ToInt32(oneIdGuiaRemi),
                            fk_comprobante_compra = IdNewCompComp
                        };
                        guiaComprobanteReturn = await CtrlGuiaComp.InsertGuiaComprobante(guia_comprobante);
                        if (guiaComprobanteReturn == null)
                        {
                            msgReturn = "Ocurrió un error al intentar realizar la relación, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                            break;
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

        public async Task<ActionResult> SaveEditComprobanteCompra(int IdCompComp, int FkComp, int FkProv, int FkCompTipo, 
            string NroCompComp, string FechEmis, decimal totaBrut, string EstaCompComp, List<string> arrFkGuiaRemi)
        {
            string msgReturn = "";
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int returnSaveGC = 1;
            msgReturn = "El Comprobante se actualizó satisfactoriamente";
            try
            {
                GuiaComprobanteController CtrlGuiaComp = new GuiaComprobanteController();
                ComprobanteCompra comprobanteCompraReturn = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                ComprobanteCompra comprobante_compra = new ComprobanteCompra
                {
                    id_comprobante_compra = IdCompComp,
                    fk_compra = FkComp,
                    fk_proveedor = FkProv,
                    fk_comprobante_tipo = FkCompTipo,
                    nro_comprobante = NroCompComp,
                    f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                    total_bruto = totaBrut,
                    estado = EstaCompComp
                };
                comprobanteCompraReturn = await UpdateComprobanteCompra(comprobante_compra);
                if (comprobanteCompraReturn != null)
                {
                    returnSaveGC = await FnSaveListGuiaComprobante(IdCompComp, arrFkGuiaRemi);
                    if (returnSaveGC == 0)
                    {
                        msgReturn = "Ocurrió un error al intentar Actualizar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
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
                msgReturn = "Ocurrió un error al intentar Actualizar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveDeleteComprobanteCompra(int IdCompComp)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Comprobante se anuló satisfactoriamente";
            try
            {
                GuiaComprobanteController CtrlGuiaComp = new GuiaComprobanteController();
                ComprobanteCompra comprobante_compra = null;
                List<GuiaComprobante> lstGuiaComprobante = null;
                GuiaComprobante guia_comprobante = null;
                comprobante_compra = await DeleteComprobanteCompra(IdCompComp);
                if (comprobante_compra != null)
                {
                    //Anulamos guiaComprobante
                    lstGuiaComprobante = await CtrlGuiaComp.Get_GuiaComprobanteByFkCompComp(IdCompComp);
                    if (lstGuiaComprobante != null)
                    {
                        foreach (var oneGuiaComp in lstGuiaComprobante)
                        {
                            guia_comprobante = await CtrlGuiaComp.DeleteGuiaComprobante(oneGuiaComp.id_guia_comprobante);
                            if (guia_comprobante == null)
                            {
                                msgReturn = "Ocurrió un error al intentar Liberar las Guias de Remisión, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                                break;
                            }
                        }
                    }
                }
                else
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

        public async Task<ActionResult> SaveFilnalizarComprobanteCompra(int IdCompComp)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Comprobante se finalizó satisfactoriamente";
            try
            {
                ComprobanteCompra comprobante_compra = null;
                ComprobanteCompra compCompReturn = null;
                comprobante_compra = await GetComprobanteCompraById(IdCompComp);
                if (comprobante_compra != null)
                {
                    comprobante_compra.estado = "2";
                    compCompReturn = await UpdateComprobanteCompra(comprobante_compra);
                    if (compCompReturn == null)
                    {
                        msgReturn = "Ocurrió un error al intentar Finalizar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar Finalizar, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Finalizar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> FnSaveListGuiaComprobante(int FkCompComp, List<string> arrFkGuiaRemi)
        {
            int flgExiste = 0;
            try
            {
                GuiaComprobanteController CtrlGuiaComp = new GuiaComprobanteController();
                List<GuiaComprobante> lstGuiaComprobante = null;
                GuiaComprobante tempGuiaComprobante = null;
                GuiaComprobante guia_comprobante = null;
                //Anulamos las existentes
                lstGuiaComprobante = await CtrlGuiaComp.Get_GuiaComprobanteByFkCompComp(FkCompComp);
                if (lstGuiaComprobante != null)
                {
                    foreach (var oneGuiaComp in lstGuiaComprobante)
                    {
                        tempGuiaComprobante = await CtrlGuiaComp.DeleteGuiaComprobante(oneGuiaComp.id_guia_comprobante);
                        if (tempGuiaComprobante == null) return 0;
                    }
                }
                foreach (var oneFkGuiaRem in arrFkGuiaRemi)
                {
                    flgExiste = 0;
                    //Vericamos si existe, si es asi la recuperamos
                    foreach (var oneGuiaComp in lstGuiaComprobante)
                    {
                        if (Convert.ToInt32(oneFkGuiaRem).Equals(oneGuiaComp.fk_guia_remision))
                        {
                            guia_comprobante = oneGuiaComp;
                            flgExiste = 1;
                            break;
                        }
                    }
                    if (flgExiste == 1)
                    {
                        //Recuperamos
                        guia_comprobante.estado = "1";
                        tempGuiaComprobante = await CtrlGuiaComp.UpdateGuiaComprobante(guia_comprobante);
                        if (tempGuiaComprobante == null) return 0;
                    }
                    else
                    {
                        //Insertamos
                        guia_comprobante = new GuiaComprobante
                        {
                            fk_guia_remision = Convert.ToInt32(oneFkGuiaRem),
                            fk_comprobante_compra = FkCompComp
                        };
                        tempGuiaComprobante = await CtrlGuiaComp.InsertGuiaComprobante(guia_comprobante);
                        if (tempGuiaComprobante == null) return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public ActionResult ComprasDiversas()
        {
            List<GuiaRemision> lEstados = new List<GuiaRemision>();
            lEstados.Add(new GuiaRemision { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new GuiaRemision { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new GuiaRemision { estado = "1", NEstado = "REGISTRADA" });
            lEstados.Add(new GuiaRemision { estado = "2", NEstado = "FINALIZADA" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }

        public async Task<ActionResult> IndexCompraDiversas(string EstaFilt)
        {
            List<ComprobanteCompra> lstEntidad = null;
            lstEntidad = await GetComprobanteCompraAll();
            lstEntidad = lstEntidad.Where(p => p.tipo_compra.Equals("2")).ToList();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> CreateCompraDiversa()
        {
            ProductoController CtrlProd = new ProductoController();
            List<TipoMoneda> lstTipoMoneda = null;
            string msgReturn = "";
            try
            {
                ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
                List<ComprobanteTipo> lstComprobanteTipo = null;
                int FkCompTipoFact = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_Factura"]);

                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
                if (lstComprobanteTipo != null)
                {
                    lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0")).ToList();
                }
                lstTipoMoneda = await CtrlProd.GetTipoMonedaAsync();
                ViewBag.TipoMoneda = lstTipoMoneda;
                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.FkTipoComprobanteFactura = FkCompTipoFact;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public async Task<ActionResult> ViewAddDetalleCompraDiversa()
        {
            return PartialView();
        }

        public async Task<ActionResult> SaveNewComprobanteCompraDiversa(int FkProv, int FkCompTipo, int FkTipoMone,
            string NroCompComp, string FechEmis, decimal totaBrut, decimal? TipoCamb, List<List<string>> arrCompCompDive)
        {
            string msgReturn = "";
            int IdNewCompComp = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            msgReturn = "El Comprobante se registró satisfactoriamente";
            try
            {
                CompraDiversasDetalleController CtrlCompDiveDeta = new CompraDiversasDetalleController();
                ComprobanteCompra comprobanteCompraReturn = null;
                CompraDiversasDetalle compra_diversas_detalle = null;
                CompraDiversasDetalle compraDiversasDetalleReturn = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);

                ComprobanteCompra comprobante_compra = new ComprobanteCompra
                {
                    fk_compra = 0,
                    fk_proveedor = FkProv,
                    fk_comprobante_tipo = FkCompTipo,
                    fk_medio_pago = FkMediPagoLineCred,
                    fk_tipo_moneda = FkTipoMone,
                    nro_comprobante = NroCompComp,
                    f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                    total_bruto = totaBrut,
                    tipo_cambio = Convert.ToDecimal(TipoCamb),
                    tipo_compra = "2"
                };
                comprobanteCompraReturn = await InsertComprobanteCompra(comprobante_compra);
                if (comprobanteCompraReturn != null)
                {
                    IdNewCompComp = comprobanteCompraReturn.id_comprobante_compra;
                    foreach (var oneCompCompDiveDeta in arrCompCompDive)
                    {
                        compra_diversas_detalle = new CompraDiversasDetalle
                        {
                            fk_comprobante_compra = IdNewCompComp,
                            descripcion = oneCompCompDiveDeta[0].ToUpper(),
                            cantidad = Convert.ToInt32(oneCompCompDiveDeta[1]),
                            precio =  Convert.ToDecimal(oneCompCompDiveDeta[2]),
                        };
                        compraDiversasDetalleReturn = await CtrlCompDiveDeta.InsertCompraDiversasDetalle(compra_diversas_detalle);
                        if (compraDiversasDetalleReturn == null)
                        {
                            msgReturn = "Ocurrió un error al intentar registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                            break;
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

        public async Task<ActionResult> EditCompraDiversa(int IdCompComp, string CallBy)
        {
            string msgReturn = "";
            ProductoController CtrlProd = new ProductoController();
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            GuiaRemisionDetalleController CtrlGuiaRemiDeta = new GuiaRemisionDetalleController();
            CompraDiversasDetalleController CtrlCompDiveDeta = new CompraDiversasDetalleController();
            ComprobanteCompra comprobante_compra = null;
            List<CompraDiversasDetalle> lstCompraDiversasDetalle = null;
            List<ComprobanteTipo> lstComprobanteTipo = null;
            List<TipoMoneda> lstTipoMoneda = null;
            try
            {
                comprobante_compra = await GetComprobanteCompraById(IdCompComp);
                if (comprobante_compra != null)
                {
                    lstCompraDiversasDetalle = await CtrlCompDiveDeta.GetCompraDiversasDetalle_ByComprobanteCompra(comprobante_compra.id_comprobante_compra);
                    if (lstCompraDiversasDetalle == null)
                    {
                        msgReturn = "Error al cargar detalles";
                        Response.StatusCode = 500;
                        return Json(msgReturn, JsonRequestBehavior.AllowGet);
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
                lstTipoMoneda = await CtrlProd.GetTipoMonedaAsync();
                ViewBag.TipoMoneda = lstTipoMoneda;
                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.CompraDiversasDetalle = lstCompraDiversasDetalle;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(comprobante_compra);
        }

        public async Task<ActionResult> SaveEditComprobanteCompraDiversa(int IdCompComp, int FkProv, int FkCompTipo, 
            int FkTipoMone, string NroCompComp, string FechEmis, decimal totaBrut, string EstaCompComp, decimal? TipoCamb, 
            List<string> arrIdCDDDelete, List<List<string>> arrCompCompDive)
        {
            string msgReturn = "";
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            msgReturn = "El Comprobante se actualizo satisfactoriamente";
            try
            {
                CompraDiversasDetalleController CtrlCompDiveDeta = new CompraDiversasDetalleController();
                ComprobanteCompra comprobanteCompraReturn = null;
                CompraDiversasDetalle compra_diversas_detalle = null;
                CompraDiversasDetalle compraDiversasDetalleReturn = null;
                NewFechEmis = Convert.ToDateTime(FechEmis);
                
                ComprobanteCompra comprobante_compra = new ComprobanteCompra
                {
                    id_comprobante_compra = IdCompComp,
                    fk_compra = 0,
                    fk_proveedor = FkProv,
                    fk_comprobante_tipo = FkCompTipo,
                    nro_comprobante = NroCompComp,
                    f_emision = NewFechEmis.ToString("yyyy/MM/dd H:mm:ss"),
                    total_bruto = totaBrut,
                    estado = EstaCompComp
                };
                comprobanteCompraReturn = await UpdateComprobanteCompra(comprobante_compra);
                if (comprobanteCompraReturn != null)
                {
                    //Eliminamos de la BD los que han sido eliminados en la vista
                    if (arrIdCDDDelete != null)
                    {
                        foreach (var oneCDDDele in arrIdCDDDelete)
                        {
                            compraDiversasDetalleReturn = await CtrlCompDiveDeta.DeleteCompraDiversasDetalle(Convert.ToInt32(oneCDDDele));
                            if (compraDiversasDetalleReturn == null)
                            {
                                msgReturn = "Ocurrió un error al intentar actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                                break;
                            }
                        }
                    }
                    if (flgError == 0)
                    {
                        //Registramos los nuevos detalles
                        if (arrCompCompDive != null)
                        {
                            foreach (var oneCompCompDiveDeta in arrCompCompDive)
                            {
                                compra_diversas_detalle = new CompraDiversasDetalle
                                {
                                    fk_comprobante_compra = IdCompComp,
                                    descripcion = oneCompCompDiveDeta[1].ToUpper(),
                                    cantidad = Convert.ToInt32(oneCompCompDiveDeta[2]),
                                    precio = Convert.ToDecimal(oneCompCompDiveDeta[3]),
                                };
                                compraDiversasDetalleReturn = await CtrlCompDiveDeta.InsertCompraDiversasDetalle(compra_diversas_detalle);
                                if (compraDiversasDetalleReturn == null)
                                {
                                    msgReturn = "Ocurrió un error al intentar actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                                    flgError = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar actualizar, Por favor comuniquese con el administrador de sistemas";
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

        public async Task<ActionResult> SaveDeleteComprobanteCompraDiversa(int IdCompComp)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Comprobante se anuló satisfactoriamente";
            try
            {
                ComprobanteCompra comprobante_compra = null;
                comprobante_compra = await DeleteComprobanteCompra(IdCompComp);
                if (comprobante_compra == null)
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

        public ActionResult RegistroCompras()
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
            return View();
        }

        public async Task<ActionResult> ListDataRegistroCompra(string RegiCompAnio, string RegiCompMes)
        {
            string msgReturn = "";
            List<ComprobanteCompra> lstEntidad = null;
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstEntidad = await GetComprobanteCompraAll();
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.Where(p => Convert.ToInt32(Convert.ToDateTime(p.f_emision).Year).Equals(Convert.ToInt32(RegiCompAnio)) &&
                                                        Convert.ToInt32(Convert.ToDateTime(p.f_emision).Month).Equals(Convert.ToInt32(RegiCompMes))).ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ViewListaComprobanteCompra(string CallBy)
        {
            List<ComprobanteCompra> lstEntidad = null;
            lstEntidad = await GetComprobanteCompraAll();
            if (lstEntidad != null)
            {
                if (CallBy.Equals("VincularFlete"))
                {
                    lstEntidad = lstEntidad.Where(i => i.estado.Equals("2")).ToList();
                }
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }
    }
}