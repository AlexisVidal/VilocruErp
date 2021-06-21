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
    [SessionAuthorize]
    public class CuentasPorCobrarController : Controller
    {
        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];

        public async Task<CuentasPorCobrar> InsertCuentasPorCobrar(CuentasPorCobrar cuentas_por_cobrar)
        {
            CuentasPorCobrar entidad = null;
            try
            {
                List<CuentasPorCobrar> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CuentasPorCobrar/agregar", cuentas_por_cobrar);

                var model = response.Content.ReadAsAsync<List<CuentasPorCobrar>>();
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

        public async Task<List<CuentasPorCobrar>> GetCuentasPorCobrarByCompVentCobr(int FkCompVentCobr)
        {
            List<CuentasPorCobrar> lstEntidad = null;
            try
            {
                CuentasPorCobrar parametro = new CuentasPorCobrar { fk_comprobante_venta_cobro = FkCompVentCobr };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CuentasPorCobrar/ByComprobanteVentaCobro", parametro);

                var model = response.Content.ReadAsAsync<List<CuentasPorCobrar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CuentasPorCobrar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<CuentasPorCobrar> GetCuentasPorCobrarById(int IdCuenPorCobr)
        {
            CuentasPorCobrar entidad = null;
            try
            {
                List<CuentasPorCobrar> lstEntidad = null;

                var client = new HttpClient();
                CuentasPorCobrar parametro = new CuentasPorCobrar { id_cuentas_por_cobrar = IdCuenPorCobr };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CuentasPorCobrar/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<CuentasPorCobrar>>();
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

        // GET: CuentasPorCobrar
        public ActionResult IndexCronograma()
        {
            List<string> lEstados = new List<string>();
            lEstados.Add("TODOS");
            lEstados.Add("SIN CRONOGRANA");
            lEstados.Add("CON CRONOGRAMA");

            ViewBag.EstadosFilter = lEstados.ToList();
            return View();
        }

        public ActionResult Index()
        {
            List<string> lEstados = new List<string>();
            lEstados.Add("TODOS");
            lEstados.Add("PENDIENTES");
            lEstados.Add("CANCELADOS");

            ViewBag.EstadosFilter = lEstados.ToList();
            return View();
        }
        public ActionResult IndexCobrar()
        {
            List<string> lEstados = new List<string>();
            lEstados.Add("TODOS");
            lEstados.Add("PENDIENTES");
            lEstados.Add("CANCELADOS");

            ViewBag.EstadosFilter = lEstados.ToList();
            return View();
        }
        public async Task<ActionResult> ListIndex(string EstaFilt, string CallBy)
        {
            ComprobanteVentaCobroController CtrlCompVentCobro = new ComprobanteVentaCobroController();
            List<ComprobanteVentaCobro> lstComprobanteVentaCobro = null;
            int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);

            lstComprobanteVentaCobro = await CtrlCompVentCobro.GetComprobanteVentaCobroByMedioPago(FkMediPagoLineCred);
            if(CallBy== "Cronograna")
            {
                //if (EstaFilt == "1")//SIN CRONOGRANA
                //{
                lstComprobanteVentaCobro = lstComprobanteVentaCobro.Where(i => i.flg_cronograma.Equals("0")).ToList();
                //}
                //else if (EstaFilt == "2")//SIN CRONOGRANA
                //{
                //    lstComprobanteVenta = lstComprobanteVenta.Where(i => i.flg_cronograma.Equals("1")).ToList();
                //}
            }
            else
            {
                lstComprobanteVentaCobro = lstComprobanteVentaCobro.Where(i => i.flg_cronograma.Equals("1")).ToList();
                if (EstaFilt == "1")//Pendientes de pago
                {
                    lstComprobanteVentaCobro = lstComprobanteVentaCobro.Where(i => i.saldo > 0).ToList();
                }
                else if (EstaFilt == "2")//Canceladas
                {
                    lstComprobanteVentaCobro = lstComprobanteVentaCobro.Where(i => i.saldo == 0).ToList();
                }
            }
            
            ViewBag.CallBy = CallBy;
            return PartialView(lstComprobanteVentaCobro);
        }

        public async Task<ActionResult> IndexByCaja()
        {
            string msgReturn = "";
            ComprobanteVentaCobroController CtrlCompVentCobro = new ComprobanteVentaCobroController();
            CajaController CtrlCaja = new CajaController();
            List<ComprobanteVentaCobro> lstComprobanteVentaCobro = null;
            List<Caja> lstCaja = new List<Caja>();
            Caja caja = null;
            int FkMediPagoLineCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MedioPago_LineaCredito"]);

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

            lstComprobanteVentaCobro = await CtrlCompVentCobro.GetComprobanteVentaCobroByMedioPago(FkMediPagoLineCred);
            lstComprobanteVentaCobro = lstComprobanteVentaCobro.Where(i => i.flg_cronograma.Equals("1")).ToList();
            lstComprobanteVentaCobro = lstComprobanteVentaCobro.Where(i => i.saldo > 0).ToList();

            ViewBag.Caja = caja;
            return PartialView(lstComprobanteVentaCobro);
        }

        public async Task<ActionResult> ViewCronograma(int FkCompVentCobr, string CallBy, int? FkCaja)
        {
            string msgReturn = "";
            ComprobanteVentaCobroController CtrlCompVentaCobro = new ComprobanteVentaCobroController();
            List<CuentasPorCobrar> lstCuentasPorCobrar = null;
            ComprobanteVentaCobro comprobante_venta_cobro = null;
            try
            {
                comprobante_venta_cobro = await CtrlCompVentaCobro.GetComprobanteVentaCobroById(FkCompVentCobr);

                lstCuentasPorCobrar = await GetCuentasPorCobrarByCompVentCobr(FkCompVentCobr);
                ViewBag.ComprobanteVentaCobro = comprobante_venta_cobro;
                ViewBag.CallByCtasPorCobrar = CallBy;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            if (FkCaja == null) FkCaja = 0;
            ViewBag.FkCaja = FkCaja;
            return PartialView(lstCuentasPorCobrar);
        }

        public async Task<ActionResult> SaveNewCuentasPorCobrar(int FkCompVentCobr, List<List<string>> arrCtasPorCobr)
        {
            string msgReturn = "";
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            msgReturn = "El Cronograma se registró satisfactoriamente";
            try
            {
                CuentasPorCobrar cuentasPorCobrarReturn = null;
                CuentasPorCobrar cuentas_por_cobrar = null;
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                foreach (var oneCtasPorCobr in arrCtasPorCobr)
                {
                    DateTime FVenc = Convert.ToDateTime(oneCtasPorCobr[1]);
                    Decimal Mont = Convert.ToDecimal(oneCtasPorCobr[2]);
                    Decimal Sald = Convert.ToDecimal(oneCtasPorCobr[3]);
                    cuentas_por_cobrar = new CuentasPorCobrar
                    {
                        fk_comprobante_venta_cobro = FkCompVentCobr,
                        fk_usuario = FkUsua,
                        f_vencimiento = FVenc,
                        monto = Mont,
                        saldo = Sald
                    };
                    cuentasPorCobrarReturn = await InsertCuentasPorCobrar(cuentas_por_cobrar);
                    if (cuentasPorCobrarReturn == null)
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

        public async Task<ActionResult> ViewPagoCuota(int FkCtasPorCobr, int? FkCaja)
        {
            string msgReturn = "";
            MedioPagoController CtrlMediPago = new MedioPagoController();
            BancoController CtrlBanc = new BancoController();
            TarjetaCreditoController CtrlTarjCred = new TarjetaCreditoController();

            CuentasPorCobrar cuenta_por_cobrar = null;
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
                lstMedioPago = lstMedioPago.Where(i => !i.id_medio_pago.Equals(FkMediPagoLineCred)).ToList();
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

                cuenta_por_cobrar = await GetCuentasPorCobrarById(FkCtasPorCobr);
                if (FkCaja == null) FkCaja = 0;
                ViewBag.FkCaja = FkCaja;
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
            return PartialView(cuenta_por_cobrar);
        }

        public async Task<ActionResult> SaveNewPagoCuentasPorCobrar(int FkCuenPorCobr, int FkCaja, int FkMediPago,
            int FkTarjCred, int FkNotaCred, int FkBanc, string NroDocuPago, decimal PagoMonto)
        {
            string msgReturn = "";
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            msgReturn = "El Pago se registró satisfactoriamente";
            try
            {
                PagoCuentasPorCobrarController CtrlPagoCuenPorCobr = new PagoCuentasPorCobrarController();
                PagoCuentasPorCobrar pagoCuentasPorCobrarReturn = null;
                PagoCuentasPorCobrar pago_cuentas_por_cobrar = null;
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                pago_cuentas_por_cobrar = new PagoCuentasPorCobrar
                {
                    fk_cuentas_por_cobrar = FkCuenPorCobr,
                    fk_caja = FkCaja,
                    fk_medio_pago = FkMediPago,
                    fk_tarjeta_credito = FkTarjCred,
                    fk_nota_credito = FkNotaCred,
                    fk_banco = FkBanc,
                    fk_usuario = FkUsua,
                    nro_documento = NroDocuPago,
                    monto = PagoMonto
                };
                pagoCuentasPorCobrarReturn = await CtrlPagoCuenPorCobr.InsertPagoCuentasPorCobrar(pago_cuentas_por_cobrar);
                if (pagoCuentasPorCobrarReturn == null)
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

        public async Task<ActionResult> PrintCronogramaCPC(string IdCompVent, string NombClie)
        {
            try
            {
                int idi = Convert.ToInt32(IdCompVent);
                DataSet data = await Get_DataPagoCronogramaCPC(idi);
                if (data?.Tables.Count > 0)
                {
                    if (data != null)
                    {
                        string codigus = data.Tables[0].Rows[0]["fk_comprobante_venta"].ToString();
                        using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                        {
                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrCronogramaCtasPorCobrar.rpt")));
                            rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                            rd.SetParameterValue("@fk_comprobante_venta", idi);

                            CultureInfo ci = CultureInfo.InvariantCulture;
                            string fechaactual = DateTime.Now.ToString("dd-MM-yyyy", ci);
                            //rd.SetDataSource(data.Tables[0]);
                            rd.SetParameterValue("ParamNombClien", NombClie);
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

        public async Task<List<CuentasPorCobrar>> GetPagoCuentasPorCobrarByComprobanteVenta(int FkCompVenta)
        {
            List<CuentasPorCobrar> lstEntidad = null;
            try
            {
                CuentasPorCobrar parametro = new CuentasPorCobrar { fk_comprobante_venta = FkCompVenta };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CuentasPorCobrar/PagosByComprobanteVenta", parametro);

                var model = response.Content.ReadAsAsync<List<CuentasPorCobrar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CuentasPorCobrar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        private async Task<DataSet> Get_DataPagoCronogramaCPC(int IdCompVent)
        {
            var lCronograma = await GetPagoCuentasPorCobrarByComprobanteVenta(IdCompVent);
            if (lCronograma != null)
            {
                if (lCronograma.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_cuentas_por_cobrar", typeof(int));
                    dt.Columns.Add("fk_comprobante_venta", typeof(int));
                    dt.Columns.Add("f_vencimiento", typeof(string));
                    dt.Columns.Add("monto", typeof(decimal));
                    dt.Columns.Add("total_bruto", typeof(decimal));
                    dt.Columns.Add("saldo", typeof(decimal));
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
                        row["id_cuentas_por_cobrar"] = item.id_cuentas_por_cobrar;
                        row["fk_comprobante_venta"] = item.fk_comprobante_venta;
                        row["f_vencimiento"] = Convert.ToDateTime(item.f_vencimiento).ToString("dd/MM/yyyy");
                        row["monto"] = item.monto;
                        row["total_bruto"] = item.total_bruto;
                        row["saldo"] = item.saldo;
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

        public FileStreamResult GetPDF(string codigo)
        {
            FileStream fs = new FileStream(@"C:\reportesotros\" + codigo, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }
    }
}