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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Globalization;
using CrystalDecisions.Shared;

namespace ERP.Admin.Controllers
{
    public class CuentasPorPagarController : Controller
    {
        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];

        [SessionAuthorize]
        public async Task<List<CuentasPorPagar>> GetCuentasPorPagarPendientes()
        {
            List<CuentasPorPagar> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("CuentasPorPagar/pendientes");
                var model = response.Content.ReadAsAsync<List<CuentasPorPagar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.CuentasPorPagar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<CuentasPorPagar> InsertCuentasPorPagar(CuentasPorPagar cuentas_por_pagar)
        {
            CuentasPorPagar entidad = null;
            try
            {
                List<CuentasPorPagar> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CuentasPorPagar/agregar", cuentas_por_pagar);

                var model = response.Content.ReadAsAsync<List<CuentasPorPagar>>();
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

        public async Task<CuentasPorPagar> GetCuentasPorPagarById(int IdCuenPorPaga)
        {
            CuentasPorPagar entidad = null;
            try
            {
                List<CuentasPorPagar> lstEntidad = null;

                var client = new HttpClient();
                CuentasPorPagar parametro = new CuentasPorPagar { id_cuentas_por_pagar = IdCuenPorPaga };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CuentasPorPagar/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<CuentasPorPagar>>();
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

        public async Task<List<CuentasPorPagar>> GetCuentasPorPagarByComprobanteCompra(int FkCompComp)
        {
            List<CuentasPorPagar> lstEntidad = null;
            try
            {
                CuentasPorPagar parametro = new CuentasPorPagar { fk_comprobante_compra = FkCompComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CuentasPorPagar/ByComprobanteCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CuentasPorPagar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CuentasPorPagar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<CuentasPorPagar>> GetPagoCuentasPorPagarByComprobanteCompra(int FkCompComp)
        {
            List<CuentasPorPagar> lstEntidad = null;
            try
            {
                CuentasPorPagar parametro = new CuentasPorPagar { fk_comprobante_compra = FkCompComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CuentasPorPagar/PagosByComprobanteCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CuentasPorPagar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CuentasPorPagar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: CuentasPorPagar
        public ActionResult Index()
        {
            List<string> lEstados = new List<string>();
            lEstados.Add("TODOS");
            lEstados.Add("PENDIENTES");
            lEstados.Add("CANCELADOS");

            ViewBag.EstadosFilter = lEstados.ToList();
            return View();
        }

        public async Task<ActionResult> ListIndex(string EstaFilt)
        {
            ComprobanteCompraController CtrlCompComp = new ComprobanteCompraController();
            List<ComprobanteCompra> lstComprobanteCompra = null;

            lstComprobanteCompra = await CtrlCompComp.GetComprobanteCompraAll();

            if (EstaFilt == "1")//Pendientes de pago
            {
                lstComprobanteCompra = lstComprobanteCompra.Where(i => i.saldo_bruto > 0).ToList();
            }
            else if (EstaFilt == "2")//Canceladas
            {
                lstComprobanteCompra = lstComprobanteCompra.Where(i => i.saldo_bruto == 0).ToList();
            }
            return PartialView(lstComprobanteCompra);
        }

        public async Task<ActionResult> ViewCuentasPorPagarCalendario()
        {
            List<CuentasPorPagar> lstCuentasPorPagar = null;
            lstCuentasPorPagar = await GetCuentasPorPagarPendientes();
            ViewBag.CuentasPorPagarPendientes = lstCuentasPorPagar;
            return View();
        }

        public async Task<ActionResult> ViewCPPCronogramaPersonalizado(int IdCompComp, string CallBy)
        {
            //List<CuentasPorPagar> lstCuentasPorPagar = null;
            //lstCuentasPorPagar = await GetCuentasPorPagarPendientes();
            //ViewBag.CuentasPorPagarPendientes = lstCuentasPorPagar;
            //return View();
            string msgReturn = "";
            ComprobanteCompraController CtrlCompCompra = new ComprobanteCompraController();
            List<CuentasPorPagar> lstCuentasPorPagar = null;
            ComprobanteCompra comprobante_compra = null;
            try
            {
                comprobante_compra = await CtrlCompCompra.GetComprobanteCompraById(IdCompComp);

                lstCuentasPorPagar = await GetCuentasPorPagarByComprobanteCompra(IdCompComp);
                ViewBag.ComprobanteCompra = comprobante_compra;
                ViewBag.CallByCtasPorPagar = CallBy;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstCuentasPorPagar);
            ViewBag.CuotasCuentasPorPagar = lstCuentasPorPagar;
            return View();
        }

        public async Task<ActionResult> ViewCronograma(int IdCompComp, string CallBy)
        {
            string msgReturn = "";
            ComprobanteCompraController CtrlCompCompra = new ComprobanteCompraController();
            List<CuentasPorPagar> lstCuentasPorPagar = null;
            List<CuentasPorPagar> lstReturnCuentasPorPagar = new List<CuentasPorPagar>();
            ComprobanteCompra comprobante_compra = null;
            CuentasPorPagar cuentas_por_pagar = null;
            try
            {
                comprobante_compra = await CtrlCompCompra.GetComprobanteCompraById(IdCompComp);

                lstCuentasPorPagar = await GetCuentasPorPagarByComprobanteCompra(IdCompComp);

                //var newListGroup = lstCuentasPorPagar.GroupBy(p => p.id_cuentas_por_pagar).Select(g => g).ToList();
                //foreach (var oneItem in newListGroup)
                //for(int i=0; i<newListGroup.Count; i++)
                //{
                //    cuentas_por_pagar = new CuentasPorPagar();
                //    cuentas_por_pagar = newListGroup[i].ElementAtOrDefault;
                //    //CuentasPorPagar cuentas_por_pagar = new CuentasPorPagar
                //    //{
                //    //    id_cuentas_por_pagar = Convert.ToInt32(oneItem.Select(i => i.apellido_materno)),
                //    //    fk_comprobante_compra = Convert.ToInt32(oneItem.Select(i => i.fk_comprobante_compra)),
                //    //    fk_usuario = Convert.ToInt32(oneItem.Select(i => i.fk_usuario)),
                //    //    f_vencimiento = Convert.ToDateTime(oneItem.Select(i => i.f_vencimiento)),
                //    //    monto = Convert.ToDecimal(oneItem.Select(i => i.monto)),
                //    //    saldo = Convert.ToDecimal(oneItem.Select(i => i.saldo)),
                //    //    f_registro = Convert.ToDateTime(oneItem.Select(i => i.f_registro)),
                //    //    estado = oneItem.Select(i => i.estado).ToString(),
                //    //    id_comprobante_compra = 
                //    //};
                //    lstReturnCuentasPorPagar.Add(cuentas_por_pagar);
                //    //									fk_compra	fk_comprobante_tipo	nro_comprobante	f_emision	total_bruto	total_igv	total_isc	total_neto	saldo_bruto	flg_cronograma	estado_comprobante_compra	id_compra	fk_orden_compra	fk_usuario_compra	fk_proveedor	n_compra	f_compra	estado_compra	id_proveedor	fk_empresa	cod_proveedor	estado_proveedor	id_empresa	ruc	razon_social	direccion	estado_empresa	id_usuario	fk_trabajador	clave	clave_modificaciones	estado_usuario	id_trabajador	fk_persona	fk_tipo_trabajador	estado_trabajador	id_persona	nombres	apellido_paterno	apellido_materno	f_nacimiento	email	dni	estado_persona	id_comprobante_tipo	descripcion_comprobante_tipo	estado_comprobante_tipo	id_pago_cuentas_por_pagar	fk_medio_pago	f_pago	monto_pago
                //}
                //lstReturnCuentasPorPagar = newListGroup<List<CuentasPorPagar>>();

                ViewBag.ComprobanteCompra = comprobante_compra;
                ViewBag.CallByCtasPorPagar = CallBy;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstCuentasPorPagar);
        }

        public async Task<ActionResult> SaveNewCuentasPorPagar(int FkCompComp, int FkTipoMone, List<List<string>> arrCtasPorPaga)
        {
            string msgReturn = "";
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            TimeSpan ts = new TimeSpan(11, 00, 0);
            msgReturn = "El Cronograma se registró satisfactoriamente";
            try
            {
                CuentasPorPagar cuentasPorPagarReturn = null;
                CuentasPorPagar cuentas_por_pagar = null;
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                foreach (var oneCtasPorCobr in arrCtasPorPaga)
                {
                    DateTime FVenc = Convert.ToDateTime(oneCtasPorCobr[1].Substring(0, 10));
                    FVenc = FVenc.Date + ts;
                    Decimal Mont = Convert.ToDecimal(oneCtasPorCobr[2]);
                    Decimal Sald = Convert.ToDecimal(oneCtasPorCobr[3]);
                    cuentas_por_pagar = new CuentasPorPagar
                    {
                        fk_comprobante_compra = FkCompComp,
                        fk_usuario = FkUsua,
                        fk_tipo_moneda = FkTipoMone,
                        f_vencimiento = FVenc,
                        monto = Mont,
                        saldo = Sald
                    };
                    cuentasPorPagarReturn = await InsertCuentasPorPagar(cuentas_por_pagar);
                    if (cuentasPorPagarReturn == null)
                    {
                        msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                        break;
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
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewPagoCuota(int FkCtasPorPaga)
        {
            string msgReturn = "";
            MedioPagoController CtrlMediPago = new MedioPagoController();
            BancoController CtrlBanc = new BancoController();
            TarjetaCreditoController CtrlTarjCred = new TarjetaCreditoController();

            CuentasPorPagar cuenta_por_pagar = null;
            List<MedioPago> lstMedioPago = null;
            List<Banco> lstBanco = null;
            List<TarjetaCredito> lsTarjetaCredito = null;
            int FkMediPagoEfec = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Efectivo"]);
            int FkMediPagoCheq = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Cheque"]);
            int FkMediPagoDepo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Deposito"]);
            int FkMediPagoTarj = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_Tarjeta"]);
            int FkMediPagoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_NotaCredito"]);
            int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);
            try
            {
                lstMedioPago = await CtrlMediPago.GetMedioPagoAll();
                lstMedioPago = lstMedioPago.Where(i=> !i.id_medio_pago.Equals(FkMediPagoLineCred)).ToList();
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

                cuenta_por_pagar = await GetCuentasPorPagarById(FkCtasPorPaga);
                ViewBag.MedioPagoEfectivo = FkMediPagoEfec;
                ViewBag.MedioPagoCheque = FkMediPagoCheq;
                ViewBag.MedioPagoDeposito = FkMediPagoDepo;
                ViewBag.MedioPagoTarjeta = FkMediPagoTarj;
                ViewBag.MedioPagoNotaCredito = FkMediPagoNotaCred;
                ViewBag.MedioPagoLineaCredito = FkMediPagoLineCred;
                ViewBag.MedioPago = lstMedioPago;
                ViewBag.Banco = lstBanco;
                ViewBag.TarjetaCredito = lsTarjetaCredito;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(cuenta_por_pagar);
        }

        public async Task<ActionResult> SaveNewPagoCuentasPorPagar(int FkCuenPorPaga, int FkMediPago,
            int FkTarjCred, int FkNotaCred, int FkBanc, string NroDocuPago, decimal PagoMonto)
        {
            string msgReturn = "";
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            msgReturn = "El Pago se registró satisfactoriamente";
            try
            {
                PagoCuentasPorPagarController CtrlPagoCuenPorPaga = new PagoCuentasPorPagarController();
                PagoCuentasPorPagar pagoCuentasPorPagarReturn = null;
                PagoCuentasPorPagar pago_cuentas_por_pagar = null;
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                pago_cuentas_por_pagar = new PagoCuentasPorPagar
                {
                    fk_cuentas_por_pagar = FkCuenPorPaga,
                    fk_medio_pago = FkMediPago,
                    fk_tarjeta_credito = FkTarjCred,
                    fk_nota_credito = FkNotaCred,
                    fk_banco = FkBanc,
                    fk_usuario = FkUsua,
                    nro_documento = NroDocuPago,
                    monto = PagoMonto
                };
                pagoCuentasPorPagarReturn = await CtrlPagoCuenPorPaga.InsertPagoCuentasPorPagar(pago_cuentas_por_pagar);
                if (pagoCuentasPorPagarReturn == null)
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

        private async Task<DataSet> Get_DataPagoCronogramaCPP(int IdCompComp)
        {
            PedidoDetalleController CtrlPediDeta = new PedidoDetalleController();
            var lCronograma = await GetPagoCuentasPorPagarByComprobanteCompra(IdCompComp);
            if (lCronograma != null)
            {
                if (lCronograma.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_cuentas_por_pagar", typeof(int));
                    dt.Columns.Add("fk_comprobante_compra", typeof(int));
                    dt.Columns.Add("f_vencimiento", typeof(string));
                    dt.Columns.Add("monto", typeof(decimal));
                    dt.Columns.Add("saldo", typeof(decimal));
                    dt.Columns.Add("total_bruto", typeof(decimal));
                    dt.Columns.Add("f_emision", typeof(string));
                    dt.Columns.Add("nro_comprobante", typeof(string)); 
                    dt.Columns.Add("descripcion_comprobante_tipo", typeof(string));
                    dt.Columns.Add("razon_social", typeof(string));
                    dt.Columns.Add("f_pago", typeof(string));
                    dt.Columns.Add("monto_pago", typeof(string));
                    dt.Columns.Add("saldo_pago", typeof(string)); 

                    foreach (var item in lCronograma)
                    {
                        DataRow row = dt.NewRow();
                        row["id_cuentas_por_pagar"] = item.id_cuentas_por_pagar;
                        row["fk_comprobante_compra"] = item.fk_comprobante_compra;
                        row["f_vencimiento"] = Convert.ToDateTime(item.f_vencimiento).ToString("dd/MM/yyyy");
                        row["monto"] = item.monto;
                        row["saldo"] = item.saldo;
                        row["total_bruto"] = item.total_bruto;
                        row["f_emision"] = Convert.ToDateTime(item.f_emision).ToString("dd/MM/yyyy");
                        row["nro_comprobante"] = item.nro_comprobante;
                        row["descripcion_comprobante_tipo"] = item.descripcion_comprobante_tipo;
                        row["razon_social"] = item.razon_social;
                        row["f_pago"] = item.f_pago;
                        row["monto_pago"] = item.monto_pago;
                        row["saldo_pago"] = item.saldo_pago;

                        dt.Rows.Add(row);
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    return ds;
                }
                return null;
            }
            return null;
        }

        public async Task<ActionResult> PrintCronogramaCPP(string IdCompComp, string DescMone)
        {
            try
            {
                int idi = Convert.ToInt32(IdCompComp);
                DataSet data = await Get_DataPagoCronogramaCPP(idi);
                if (data?.Tables.Count > 0)
                {
                    if (data != null)
                    {
                        string codigus = data.Tables[0].Rows[0]["fk_comprobante_compra"].ToString();
                        using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                        {
                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrCronogramaCtasPorPagar.rpt")));
                            rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);

                            rd.SetParameterValue("@fk_comprobante_compra", idi);

                            //string path = Server.MapPath("~/img/reportes/qrguia" + IdCompComp.PadLeft(6, '0') + ".jpg");
                            //for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                            //{
                            //    if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                            //    {
                            //        rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                            //    }
                            //}


                            CultureInfo ci = CultureInfo.InvariantCulture;
                            string fechaactual = DateTime.Now.ToString("dd-MM-yyyy", ci);
                            //rd.SetDataSource(data.Tables[0]);
                            rd.SetParameterValue("ParamMoneda", DescMone);
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
    }
}