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
using System.Data;
using System.Security.Principal;
using System.Globalization;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using System.Web.Configuration;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class CajaController : Controller
    {
        public async Task<Caja> InsertCaja(Caja caja)
        {
            Caja entidad = null;
            try
            {
                List<Caja> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Caja/agregar", caja);

                var model = response.Content.ReadAsAsync<List<Caja>>();
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

        public async Task<Caja> CierreCaja(Caja caja)
        {
            Caja entidad = null;
            try
            {
                List<Caja> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Caja/cierre", caja);

                var model = response.Content.ReadAsAsync<List<Caja>>();
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

        public async Task<Caja> GetCajaId(int IdCaja)
        {
            List<Caja> lstEntidad = null;
            Caja entidad = null;
            try
            {
                Caja parametro = new Caja { id_caja = IdCaja };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Caja/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<Caja>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    entidad = lstEntidad[0];
                }
                else
                {
                    entidad = new Caja();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return entidad;
        }

        public async Task<List<Caja>> GetCajaAll()
        {
            List<Caja> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Caja/all");
                var model = response.Content.ReadAsAsync<List<Caja>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Caja>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: Caja
        public ActionResult Index()
        {
            List<Caja> lEstados = new List<Caja>();
            lEstados.Add(new Caja { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new Caja { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new Caja { estado = "1", NEstado = "ABIERTA" });
            lEstados.Add(new Caja { estado = "1", NEstado = "CERRADA" });

            ViewBag.EstadosFilter = lEstados.ToList();

            return View();
        }

        public async Task<ActionResult> ListIndex(string EstaFilt)
        {
            List<Caja> lstEntidad = null;
            lstEntidad = await GetCajaAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> Create()
        {
            string msgReturn = "";
            string NombUsua = Session["Nombres"].ToString();
            string DniUsua = Session["DNI"].ToString();
            try
            {
                List<Caja> lstCaja = new List<Caja>();
                lstCaja = await GetCajaAll();
                if (lstCaja != null)
                {
                    lstCaja = lstCaja.Where(i => i.estado.Equals("1")).ToList();
                    if (lstCaja.Count > 0)
                    {
                        msgReturn = "Caja ya se encuentra aperturada";
                        Response.StatusCode = 500;
                        return Json(msgReturn, JsonRequestBehavior.AllowGet);
                    }
                }
                ViewBag.UsuarioNombreCompleto = DniUsua + " - " + NombUsua;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public async Task<ActionResult> SaveNewCaja(decimal MontAper)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "Caja se aperturó satisfactoriamente";
            try
            {
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                Caja cajaReturn = null;
                Caja caja = new Caja
                {
                    fk_usuario = FkUsua,
                    monto_apertura = MontAper
                };
                cajaReturn = await InsertCaja(caja);
                if (cajaReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar Aperturar Caja, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Aperturar Caja, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewIngresoEgreso(int IdCaja)
        {
            string msgReturn = "";
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            List<ComprobanteTipo> lstComprobanteTipo = null;
            try
            {
                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
                if (lstComprobanteTipo != null)
                {
                    lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0") && i.id_comprobante_tipo <= 2).ToList();
                }
                lstComprobanteTipo.Add(new ComprobanteTipo { id_comprobante_tipo = 0, descripcion = "" });
                lstComprobanteTipo = lstComprobanteTipo.OrderBy(i => i.id_comprobante_tipo).ToList();

                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.IdCaja = IdCaja;
                return PartialView();
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SaveMovimientoCaja(int FkCaja, int FkCompTipo, string MoviNroDocu,
            string MoviDesc, decimal MoviMont, string MoviTipo)
        {
            string msgReturn = "";
            int flgError = 0;
            int IdMovi = 0;
            msgReturn = "El Movimiento de caja se registró satisfactoriamente";
            int FkMediPagoEfec = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Efectivo"]);
            try
            {
                MovimientoCajaController CtrlMoviCaja = new MovimientoCajaController();

                MovimientoCaja movimientoCajaReturn = null;
                MovimientoCaja movimiento_caja = new MovimientoCaja
                {
                    fk_caja = FkCaja,
                    fk_comprobante_tipo = FkCompTipo,
                    fk_medio_pago = FkMediPagoEfec,
                    nro_documento = MoviNroDocu,
                    descripcion = MoviDesc,
                    monto = MoviMont,
                    ingreso_egreso = MoviTipo
                };
                movimientoCajaReturn = await CtrlMoviCaja.InsertMovimientoCaja(movimiento_caja);
                if (movimientoCajaReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar registrar, Por favor comuniquese con el administrador de sistemas";
                    flgError = 1;
                }
                else
                {
                    IdMovi = movimientoCajaReturn.id_movimiento_caja;
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return Json(msgReturn, JsonRequestBehavior.AllowGet);
            return Json(new { msgRpta = msgReturn, IdMovi = IdMovi }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewCerrarCaja(int IdCaja, string CallBy)
        {
            string msgReturn = "";
            ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
            MovimientoCajaController CtrlMoviCaja = new MovimientoCajaController();
            PagoCuentasPorCobrarController CtrlPagoCtasPorCobr = new PagoCuentasPorCobrarController();

            List<ComprobanteVentaCobro> lstComprobanteVenta = null;
            List<MovimientoCaja> lstMovimientoCaja = null;
            List<PagoCuentasPorCobrar> lstPagoCuentasPorCobrar = null;
            int FkMediPagoEfec = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Efectivo"]);
            int FkMediPagoCheq = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Cheque"]);
            int FkMediPagoDepo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Deposito"]);
            int FkMediPagoTarj = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Tarjeta"]);
            int FkMediPagoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_NotaCredito"]);
            int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);
            Caja caja = null;
            try
            {
                //Seleccionamos la Caja
                caja = await GetCajaId(IdCaja);
                //Seleccionamos los comprobantes de venta
                lstComprobanteVenta = await CtrlCompVent.GetComprobanteVentaByCaja(IdCaja);

                //Seleccionamos los ingreso y eguesos
                lstMovimientoCaja = await CtrlMoviCaja.GetMovimientoCajaByCaja(IdCaja);

                //Seleccionamos los cobros de creditos realizados por esta caja
                lstPagoCuentasPorCobrar = await CtrlPagoCtasPorCobr.GetPagoCuentasPorCobrarByCaja(IdCaja);

                ViewBag.MedioPagoEfectivo = FkMediPagoEfec;
                ViewBag.MedioPagoCheque = FkMediPagoCheq;
                ViewBag.MedioPagoDeposito = FkMediPagoDepo;
                ViewBag.MedioPagoTarjeta = FkMediPagoTarj;
                ViewBag.MedioPagoNotaCredito = FkMediPagoNotaCred;
                ViewBag.MedioPagoLineaCredito = FkMediPagoLineCred;
                ViewBag.Caja = caja;
                ViewBag.MovimientoCaja = lstMovimientoCaja;
                ViewBag.PagoCuentasPorCobrar = lstPagoCuentasPorCobrar;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstComprobanteVenta);
        }

        public async Task<ActionResult> SaveCierreCaja(int IdCaja, decimal MontCier)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "Caja se Cerró satisfactoriamente";
            try
            {
                Caja cajaReturn = null;
                Caja caja = new Caja
                {
                    id_caja = IdCaja,
                    monto_cierre = MontCier
                };
                cajaReturn = await CierreCaja(caja);
                if (cajaReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar cerrar Caja, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar cerrar Caja, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        private async Task<DataSet> Get_DataSalida(int id)
        {
            PedidoDetalleController CtrlPediDeta = new PedidoDetalleController();
            var caja = await GetCajaId(id);
            if (caja != null)
            {
                //if (lpedido.Any())
                //{
                DataTable dt = new DataTable();
                dt.Columns.Add("id_caja", typeof(int));
                dt.Columns.Add("fk_usuario", typeof(int));
                dt.Columns.Add("f_inicio", typeof(string));
                dt.Columns.Add("f_fin", typeof(string));
                dt.Columns.Add("monto_apertura", typeof(decimal));
                dt.Columns.Add("monto_cierre", typeof(decimal));
                dt.Columns.Add("estado", typeof(string));
                dt.Columns.Add("fk_trabajador", typeof(int));
                dt.Columns.Add("fk_persona", typeof(int));
                dt.Columns.Add("nombres", typeof(string));
                dt.Columns.Add("apellido_paterno", typeof(string));
                dt.Columns.Add("apellido_materno", typeof(string));
                dt.Columns.Add("email", typeof(string));
                dt.Columns.Add("dni", typeof(string));

                //foreach (var item in lpedido)
                //{
                DataRow row = dt.NewRow();
                row["id_caja"] = caja.id_caja;
                row["fk_usuario"] = caja.fk_usuario;
                row["f_inicio"] = Convert.ToDateTime(caja.f_inicio).ToString("dd/MM/yyyy");
                row["f_fin"] = caja.f_fin;
                row["monto_apertura"] = caja.monto_apertura;
                row["monto_cierre"] = caja.monto_cierre;
                row["estado"] = caja.estado;
                row["fk_trabajador"] = caja.fk_trabajador;
                row["fk_persona"] = caja.fk_persona;
                row["nombres"] = caja.nombres;
                row["apellido_paterno"] = caja.apellido_paterno;
                row["apellido_materno"] = caja.apellido_materno;
                row["email"] = caja.email;
                row["dni"] = caja.dni;


                dt.Rows.Add(row);
                //}
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                return ds;
                //}
                //return null;
            }
            return null;
        }

        public async Task<ActionResult> PrintCierreCaja(int IdCaja, decimal TotaVentEfec, decimal TotaVentCheq,
            decimal TotaVentDepo, decimal TotaVentTarj, decimal TotaVentNotaCred, decimal TotaVentLineCred,
            decimal TotaVentDia, decimal TotaCobrLineCredEfect, decimal TotaOtroIngr, decimal TotaOtroEgre,
            decimal MontCier)
        {
            try
            {
                //string NroPedi = IdCaja.ToString();
                DataSet data = await Get_DataSalida(IdCaja);
                if (data?.Tables.Count > 0)
                {
                    if (data != null)
                    {
                        string codigus = data.Tables[0].Rows[0]["id_caja"].ToString();
                        using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                        {
                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrCierreCaja.rpt")));

                            string path = Server.MapPath("~/img/reportes/qrguia" + IdCaja.ToString().PadLeft(6, '0') + ".jpg");
                            for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                            {
                                if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                                {
                                    rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                                }
                            }


                            CultureInfo ci = CultureInfo.InvariantCulture;
                            string fechaactual = DateTime.Now.ToString("dd-MM-yyyy", ci);
                            rd.SetDataSource(data.Tables[0]);
                            rd.SetParameterValue("ParamTotaVentEfec", TotaVentEfec.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaVentCheq", TotaVentCheq.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaVentDepo", TotaVentDepo.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaVentTarj", TotaVentTarj.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaVentNotaCred", TotaVentNotaCred.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaVentLineCred", TotaVentLineCred.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaVentDia", TotaVentDia.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaCobrLineCredEfec", TotaCobrLineCredEfect.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaOtroIngr", TotaOtroIngr.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamTotaOtroEgre", TotaOtroEgre.ToString("#,##0.00"));
                            rd.SetParameterValue("ParamMontCier", MontCier.ToString("#,##0.00"));
                            Directory.CreateDirectory(@"C:\reportesotros");
                            rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\guia" + codigus + "-" + fechaactual + ".pdf");

                            ViewBag.Printer = "Guia " + codigus;
                            ViewBag.Codigo = "guia" + codigus + "-" + fechaactual + ".pdf";
                        }
                    }
                    else
                    {
                        ViewBag.Printer = "No se encontro informacion para mostrar!";
                        ViewBag.Codigo = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Printer = ex;
                ViewBag.Codigo = "";
            }
            return View();
        }

        public FileStreamResult GetPDF(string codigo)
        {
            FileStream fs = new FileStream(@"C:\reportesotros\" + codigo, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }

        public async Task<DataSet> Get_DataMovimientoCaja(int IdMoviCaja)
        {
            MovimientoCajaController CtrlMoviCaja = new MovimientoCajaController();
            var entidad = await CtrlMoviCaja.GetMovimientoCajaById(IdMoviCaja);
            if (entidad != null)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("id_movimiento_caja", typeof(int));
                dt.Columns.Add("fk_caja", typeof(int));
                dt.Columns.Add("fk_comprobante_tipo", typeof(int));
                dt.Columns.Add("fk_medio_pago", typeof(int));
                dt.Columns.Add("f_movimiento", typeof(string));
                dt.Columns.Add("nro_documento", typeof(string));
                dt.Columns.Add("descripcion", typeof(string));
                dt.Columns.Add("monto", typeof(decimal));
                dt.Columns.Add("ingreso_egreso", typeof(string));
                dt.Columns.Add("estado", typeof(string));
                dt.Columns.Add("descripcion_comprobante_tipo", typeof(string));


                DataRow row = dt.NewRow();
                row["id_movimiento_caja"] = entidad.id_movimiento_caja;
                row["fk_caja"] = entidad.fk_caja;
                row["fk_comprobante_tipo"] = entidad.fk_comprobante_tipo;
                row["fk_medio_pago"] = entidad.fk_medio_pago;
                row["f_movimiento"] = entidad.f_movimiento;
                row["nro_documento"] = entidad.nro_documento;
                row["descripcion"] = entidad.descripcion;
                row["monto"] = entidad.monto;
                row["ingreso_egreso"] = entidad.ingreso_egreso;
                row["estado"] = entidad.estado;
                row["descripcion_comprobante_tipo"] = entidad.descripcion_comprobante_tipo;
                dt.Rows.Add(row);

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                return null;
            }
        }

        public async Task<ActionResult> PrintMovimientoCaja(int? IdMovi)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            try
            {
                int idi = Convert.ToInt32(IdMovi);
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                //List<VentaDetalle> lstVentaDetalle = await CtrlVentDeta.Get_DataDespacho(FkVent);
                //comprobante_venta = await GetComprobanteVentaById(IdCompVent);
                //string CompNro = DescTipoComp + " - " + NroCompVent;
                //string Clie = DniRuc + " - " + NombClie;
                DataSet data = await Get_DataMovimientoCaja(idi);
                //if (EfecReci == null)
                //{
                //    EfecReci = 0;
                //}
                //if (Vuel == null)
                //{
                //    Vuel = 0;
                //}
                //string papel = WebConfigurationManager.AppSettings["GuiaProancoPaper"];
                if (data?.Tables.Count > 0)
                {
                    
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrEgresoPagoProveedor.rpt")));
                    rd.SetParameterValue("@id_movimiento_caja", IdMovi);
                    //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    //QRCodeData qrCodeData = qrGenerator.CreateQrCode(id.PadLeft(6, '0'), QRCodeGenerator.ECCLevel.Q);
                    //QRCode qrCode = new QRCode(qrCodeData);
                    //Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    //qrCodeImage.Save(Path.Combine(Server.MapPath("~/reportes/qrguia" + id.PadLeft(6, '0') + ".jpg")), System.Drawing.Imaging.ImageFormat.Jpeg);
                    //string path = Path.Combine(Server.MapPath("~/assets/img/cromo_plastic.jpg"));
                    //for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                    //{
                    //    if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                    //    {
                    //        rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                    //    }
                    //}
                    rd.SetDataSource(data.Tables[0]);
                    //rd.SetParameterValue("ParamCliente", Clie);
                    //rd.SetParameterValue("ParamDireccion", DireClie);
                    //rd.SetParameterValue("ParamComprobanteNumero", CompNro);
                    //rd.SetParameterValue("ParamEfectivoRecibido", Convert.ToDecimal(EfecReci).ToString("#,##0.00"));
                    //rd.SetParameterValue("ParamVuelto", Convert.ToDecimal(Vuel).ToString("#,##0.00"));
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
    }
}