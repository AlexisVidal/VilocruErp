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
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class NotaDebitoController : Controller
    {
        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        public async Task<NotaDebito> InsertNotaDebito(NotaDebito nota_debito)
        {
            NotaDebito entidad = null;
            try
            {
                List<NotaDebito> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaDebito/agregar", nota_debito);

                var model = response.Content.ReadAsAsync<List<NotaDebito>>();
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

        public async Task<NotaDebito> GetNotaDebitoById(int IdNotaCred)
        {
            NotaDebito entidad = null;
            try
            {
                List<NotaDebito> lstEntidad = null;

                NotaDebito parametro = new NotaDebito { id_nota_debito = IdNotaCred };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("NotaDebito/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<NotaDebito>>();
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

        public async Task<List<NotaDebito>> GetNotaDebitoAll()
        {
            List<NotaDebito> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("NotaDebito/all");
                var model = response.Content.ReadAsAsync<List<NotaDebito>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.NotaDebito>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: NotaDebito
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ListIndex(string EstaFilt)
        {
            List<NotaDebito> lstEntidad = null;
            lstEntidad = await GetNotaDebitoAll();
            //if (EstaFilt != null && EstaFilt != "-1")
            //{
            //    lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            //}
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> Create(int IdCompVent, string CallBy)
        {
            string msgReturn = "";
            ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();
            VentaController CtrlVent = new VentaController();
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();

            ComprobanteVenta comprobante_venta = null;
            Venta venta = null;
            List<VentaDetalle> lstVentaDetalle = new List<VentaDetalle>();
            List<ComprobanteTipo> lstComprobanteTipo = null;

            int FkCompTipoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_NotaDebito"]);

            try
            {
                comprobante_venta = await CtrlCompVent.GetComprobanteVentaById(IdCompVent);
                if (comprobante_venta != null)
                {
                    venta = await CtrlVent.GetVentaById(comprobante_venta.fk_venta);
                    if (venta != null)
                    {
                        lstVentaDetalle = await CtrlVentDeta.GetVentaDetalleAll();
                        if (lstVentaDetalle != null)
                        {
                            lstVentaDetalle = lstVentaDetalle.Where(i => i.fk_venta.Equals(venta.id_venta) && !i.estado_venta.Equals("0")).ToList();
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

                List<TipoAfectacion> ltipoafecta = new List<TipoAfectacion>();
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 1, descripcion = "Gravado Operación Onerosa", codespecial = "18|1|IGV" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 2, descripcion = "[Gratuito] Gravado Retiro por premio", codespecial = "0|2|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 3, descripcion = "[Gratuito] Gravado Retiro por donación", codespecial = "0|3|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 4, descripcion = "[Gratuito] Gravado Retiro", codespecial = "0|4|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 5, descripcion = "[Gratuito] Gravado Retiro por publicidad", codespecial = "0|5|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 6, descripcion = "[Gratuito] Gravado Bonificaciones", codespecial = "0|6|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 7, descripcion = "[Gratuito] Gravado Retiro por entrega a trabajadores", codespecial = "0|7|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 8, descripcion = "Exonerado Operación Onerosa", codespecial = "0|8|EXO" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 9, descripcion = "Inafecto Operación Onerosa", codespecial = "0|9|INA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 10, descripcion = "[Gratuito] Inafecto Retiro por Bonificación", codespecial = "0|10|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 11, descripcion = "[Gratuito] Inafecto Retiro", codespecial = "0|11|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 12, descripcion = "[Gratuito] Inafecto Retiro por Muestras Médicas", codespecial = "0|12|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 13, descripcion = "[Gratuito] Inafecto Retiro por Convenio Colectivo", codespecial = "0|13|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 14, descripcion = "[Gratuito] Inafecto Retiro por premio", codespecial = "0|14|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 15, descripcion = "[Gratuito] Inafecto Retiro por publicidad", codespecial = "0|15|GRA" });
                ltipoafecta.Add(new TipoAfectacion { id_tipo_afectacion_igv = 16, descripcion = "Exportación", codespecial = "0|16|INA" });
                ViewBag.ListTipoAfectacion = ltipoafecta;
                ViewBag.ListTipoAfectacionTemp = ltipoafecta;

                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.Venta = venta;
                ViewBag.VentaDetalle = lstVentaDetalle;
                ViewBag.CallBy = CallBy;
                ViewBag.FkCompTipoNotaDebito = FkCompTipoNotaCred;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(comprobante_venta);
        }

        public async Task<ActionResult> SaveNewNotaDebito(int FkCompVent, string NroCompNC, string FechEmisNC,
            //decimal TotaBrutNC, decimal TotaIgvNC, decimal TotaNetoNC,
            string DescMotiNC, int MotiNC, string flgAfecStoc,List<List<string>> arrNotaDebDeta, 
            string TotaNetoNC, string TotaIgvNC, string TotaBrutNC, string Sigla,
            decimal TotalExonerado, decimal TotalInafecto, decimal TotalGratuito)
        {
            string msgReturn = "";
            string DataQr = "";
            int IdNewNotaCred = 0;
            DateTime NewFechEmis = DateTime.Now;
            int flgError = 0;
            int flgReturnSave = 1;

            decimal dTotaNetoNC = Convert.ToDecimal(TotaNetoNC);
            decimal dTotaIgvNC = Convert.ToDecimal(TotaIgvNC);
            decimal dTotaBrutNC = Convert.ToDecimal(TotaBrutNC);

            msgReturn = "La nota de débito se generó satisfactoriamente";
            int FkCompTipoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_NotaDebito"]);
            try
            {
                ComprobanteVentaController CtrlComVent = new ComprobanteVentaController();
                VentaController CtrlVent = new VentaController();
                VentaDetalleController CtrlDVent = new VentaDetalleController();
                NotaDebito notaCreditoReturn = null;
                NotaDebito nota_debito = null;
                NewFechEmis = Convert.ToDateTime(FechEmisNC);
                string idusuario = Session["IdUsuario"].ToString();

                var varcomprob = await CtrlComVent.GetComprobanteVentaById(FkCompVent);
                if (varcomprob != null)
                {
                    var ventadetalle = await CtrlDVent.GetVentaDetalle_ByFkVenta(varcomprob.fk_venta);
                    nota_debito = new NotaDebito
                    {
                        fk_comprobante_venta = FkCompVent,
                        fk_usuario = idusuario,
                        nro_comprobante = NroCompNC,
                        f_emision = NewFechEmis,
                        total_bruto = dTotaBrutNC,
                        total_igv = dTotaIgvNC,
                        total_isc = 0,
                        total_neto = dTotaNetoNC,
                        motivo = DescMotiNC,
                        motivo_nc = MotiNC,
                        flg_afecta_stock = flgAfecStoc,
                        fk_comprobante_tipo = FkCompTipoNotaCred,
                        fk_moneda = varcomprob.fk_moneda,
                        f_vencimiento = NewFechEmis,
                        observacion_a = "",
                        observacion_b = "",
                        observacion_full = "",
                        total_bruto_comprobante_venta = varcomprob.total_bruto,
                        total_exonerado = TotalExonerado,
                        total_inafecto = TotalInafecto,
                        total_gratuito = TotalGratuito,
                        referencia = "",
                        codigo_hash = "",
                        cadena_para_codigo_qr = "",
                        tipo_cambio = varcomprob.tipo_cambio,
                        sigla = Sigla
                    };
                    notaCreditoReturn = await InsertNotaDebito(nota_debito);
                    if (notaCreditoReturn != null)
                    {
                        IdNewNotaCred = notaCreditoReturn.id_nota_debito;
                        if (arrNotaDebDeta != null && arrNotaDebDeta.Any())
                        {
                            flgReturnSave = await SaveListNotaDebitoDetalle2(IdNewNotaCred, arrNotaDebDeta, flgAfecStoc);
                            if (flgReturnSave == 0)
                            {
                                //notaCreditoReturn = await SaveDeleteComprobanteVenta(IdNewNotaCred);
                                msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                            }
                            else
                            {
                                Respuesta rptaFactE = await EnviaNCFacturacionElectronica(notaCreditoReturn);
                                if (rptaFactE.errors == null)
                                {
                                    DataQr = rptaFactE.cadena_para_codigo_qr;
                                    try
                                    {
                                        NotaDebito xnota = new NotaDebito()
                                        {
                                            id_nota_debito = notaCreditoReturn.id_nota_debito,
                                            codigo_hash = rptaFactE.codigo_hash,
                                            cadena_para_codigo_qr = rptaFactE.cadena_para_codigo_qr,
                                            enlace = rptaFactE.enlace
                                        };
                                        var xyz = await UpdateNotaDebitoRespuesta(xnota);
                                    }
                                    catch (Exception ex)
                                    {

                                    }


                                }
                                else
                                {
                                    notaCreditoReturn = await SaveDeleteComprobanteVenta(notaCreditoReturn.id_nota_debito);
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
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return Json(msgReturn, JsonRequestBehavior.AllowGet);
            return Json(new { msgRpta = msgReturn, IdNotaDebVent = IdNewNotaCred, DataQr = DataQr }, JsonRequestBehavior.AllowGet);
        }
        public async Task<NotaDebito> SaveDeleteComprobanteVenta(int IdCompVent)
        {
            NotaDebito comprobante_venta = null;
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
        public async Task<NotaDebito> DeleteComprobanteVentaId(int IdCompVent)
        {
            NotaDebito entidad = null;
            try
            {
                List<NotaDebito> lstEntidad = null;

                var client = new HttpClient();
                NotaDebito parametro = new NotaDebito { id_nota_debito = IdCompVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaDebito/eliminarid", parametro);

                var model = response.Content.ReadAsAsync<List<NotaDebito>>();
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


        public static async Task<NotaDebito> UpdateNotaDebitoRespuesta(NotaDebito nota)
        {
            NotaDebito entidad = null;
            try
            {
                List<NotaDebito> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaDebito/t_nota_debitoUpdateRespuesta", nota);

                var model = response.Content.ReadAsAsync<List<NotaDebito>>();
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

        public async Task<Respuesta> EnviaNCFacturacionElectronica(NotaDebito notaCreditoReturn)
        {
            List<NotaDebitoDetalle> lstNotaDebitoDetalle = null;
            FacturacionElectronicaController CtrlFactElec = new FacturacionElectronicaController();
            ComprobanteVentaController CtrlCompVenta = new ComprobanteVentaController();
            NotaDebitoDetalleController CtrlNotaCredDeta = new NotaDebitoDetalleController();
            List<Parametro> lstParametro = null;
            Respuesta rptaFactE = null;
            string EmailEmpr = "";

            try
            {
                string ParamDiasAlertSoliMues = ConfigurationManager.AppSettings["ParamEmailFactElect"].ToString();
                lstParametro = await CtrlCompVenta.GetParametroAll();
                lstParametro = lstParametro.Where(i => i.nombre.Trim().ToUpper().Equals(ParamDiasAlertSoliMues.Trim().ToUpper())).ToList();
                if (lstParametro.Count > 0)
                {
                    EmailEmpr = lstParametro[0].valor_string;
                }
                string SeriComp = notaCreditoReturn.nro_comprobante.Substring(0, 4);
                string NroComp = notaCreditoReturn.nro_comprobante.Substring(5, notaCreditoReturn.nro_comprobante.Length - 5);
                string SeriDocuAfec = notaCreditoReturn.nro_comprobante_comprobante_venta.Substring(0, 4);
                //SeriDocuAfec = SeriComp; // notaCreditoReturn.sigla + SeriComp;
                string NroCompAfec = notaCreditoReturn.nro_comprobante_comprobante_venta.Substring(5, notaCreditoReturn.nro_comprobante_comprobante_venta.Length - 5);

                decimal ValorIgv = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["ValorIgv"]);
                string ClieTipoDocu = "6";
                int clientipo = notaCreditoReturn.fk_cliente_tipo;
                if (clientipo == 1)
                {
                    ClieTipoDocu = "1";
                }
                else if (clientipo == 2)
                {
                    ClieTipoDocu = "6";
                }
                string ClieNuroDocu = notaCreditoReturn.ruc_empresa_cliente_juridico;
                string ClieDeno = notaCreditoReturn.razon_social;
                string ClieEmai = notaCreditoReturn.email;
                string ClieDire = notaCreditoReturn.direccion_empresa;
                double totalgratuita = 0;
                List<Items> lstItems = new List<Items>();
                Items one_item = null;
                lstNotaDebitoDetalle = await CtrlNotaCredDeta.GetNotaDebitoDetalleByNotaDebito(notaCreditoReturn.id_nota_debito);
                if (lstNotaDebitoDetalle != null)
                {
                    lstNotaDebitoDetalle = lstNotaDebitoDetalle.Where(i => !i.estado.Equals("0")).ToList();
                }
                foreach (var oneItem in lstNotaDebitoDetalle)
                {
                    double factIgv = 1.18;
                    if (oneItem.flag_afecto_igv == "0")
                    {
                        factIgv = 1;
                    }
                    //decimal factIgv = (1 + oneItem.porcentaje / 100);
                    one_item = new Items();
                    one_item.unidad_de_medida = oneItem.tipo_bien;
                    one_item.codigo = oneItem.codigo;
                    one_item.descripcion = oneItem.descripcion;
                    one_item.cantidad = Convert.ToDouble(oneItem.cantidad);
                    one_item.valor_unitario = Math.Round(Convert.ToDouble(oneItem.precio), 2); //500;
                    one_item.precio_unitario = Math.Round((Convert.ToDouble(oneItem.precio) * factIgv), 2);//590;
                    one_item.descuento = "";
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
                invoice.tipo_de_comprobante = 4;//
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
                invoice.fecha_de_emision = Convert.ToDateTime(notaCreditoReturn.f_emision);
                invoice.fecha_de_vencimiento = Convert.ToDateTime(notaCreditoReturn.f_vencimiento);
                invoice.moneda = Convert.ToInt32(notaCreditoReturn.fk_moneda);
                invoice.tipo_de_cambio = Convert.ToDecimal(notaCreditoReturn.tipo_cambio);
                invoice.porcentaje_de_igv = Convert.ToDouble(ValorIgv);//18.00;
                invoice.descuento_global = "";
                invoice.total_descuento = "";
                invoice.total_anticipo = "";
                invoice.total_gravada = notaCreditoReturn.total_neto;//500;
                invoice.total_inafecta = notaCreditoReturn.total_inafecto;
                invoice.total_exonerada = notaCreditoReturn.total_exonerado;
                invoice.total_igv = Convert.ToDouble(notaCreditoReturn.total_igv); //90;
                invoice.total_gratuita = notaCreditoReturn.total_gratuito;
                invoice.total_otros_cargos = "";
                invoice.total = Convert.ToDouble(notaCreditoReturn.total_bruto);//590;
                invoice.percepcion_tipo = "";
                invoice.percepcion_base_imponible = "";
                invoice.total_percepcion = "";
                invoice.detraccion = notaCreditoReturn.check_detraccion;
                if (notaCreditoReturn.sigla == "F")
                {
                    invoice.documento_que_se_modifica_tipo = 1;
                }
                else if (notaCreditoReturn.sigla == "B")
                {
                    invoice.documento_que_se_modifica_tipo = 2;
                }
                //invoice.documento_que_se_modifica_serie =
                invoice.observaciones = "";
                invoice.documento_que_se_modifica_tipo = Convert.ToInt32(notaCreditoReturn.codigo);
                invoice.documento_que_se_modifica_serie = SeriDocuAfec;
                invoice.documento_que_se_modifica_numero = NroCompAfec;
                invoice.tipo_de_nota_de_credito = "";
                invoice.tipo_de_nota_de_debito = notaCreditoReturn.motivo_nc;
                invoice.enviar_automaticamente_a_la_sunat = true;
                invoice.enviar_automaticamente_al_cliente = false;
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
                    System.IO.File.WriteAllText(path + "notadebito-" + invoice.cliente_numero_de_documento + "-" + invoice.serie + ".json", jsondata);
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


        private async Task<int> SaveListNotaDebitoDetalle2(int FkNotaCred, List<List<string>> arrNotaCredDeta, string flgAfecStoc)
        {
            int flgSaveMovi = 0;
            NotaDebitoDetalleController CtrlNotaCredDeta = new NotaDebitoDetalleController();
            try
            {
                NotaDebitoDetalle notaCreditoDetalleReturn = null;
                NotaDebitoDetalle nota_debito_detalle = null;
                foreach (var oneDetaVent in arrNotaCredDeta)
                {
                    nota_debito_detalle = new NotaDebitoDetalle
                    {
                        fk_nota_debito = FkNotaCred,
                        fk_venta_detalle = Convert.ToInt32(oneDetaVent[0]),
                        cantidad = Convert.ToDecimal(oneDetaVent[1]),
                        precio = Convert.ToDecimal(oneDetaVent[2]),
                        codigo = oneDetaVent[6],
                        codigo_sunat = oneDetaVent[5],
                        descripcion = oneDetaVent[7],
                        fk_tipo_afectacion_igv = Convert.ToInt32(oneDetaVent[4]),
                        fk_tipo_isc = 0,
                        valor_venta = Convert.ToDecimal(oneDetaVent[8]),
                        igv = Convert.ToDecimal(oneDetaVent[9]),
                        importe = Convert.ToDecimal(oneDetaVent[10]),
                        estado = "1",
                        tipo_bien = oneDetaVent[11]
                    };
                    notaCreditoDetalleReturn = await CtrlNotaCredDeta.InsertNotaDebitoDetalle(nota_debito_detalle);
                    if (notaCreditoDetalleReturn == null) return 0;
                    //Guardamos en t_movimiento
                    //if (flgAfecStoc.Equals("1"))
                    //{
                    //    flgSaveMovi = await SaveMovimiento(notaCreditoDetalleReturn.id_nota_debito_detalle, Convert.ToInt32(oneDetaVent.fk_proyecto), Convert.ToDecimal(oneDetaVent.cantidad));
                    //    if (flgSaveMovi == 0) return 0;
                    //}
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
        public async Task<int> SaveListNotaDebitoDetalle(int FkNotaCred, List<VentaDetalle> arrNotaCredDeta, string flgAfecStoc)
        {
            int flgSaveMovi = 0;
            NotaDebitoDetalleController CtrlNotaCredDeta = new NotaDebitoDetalleController();
            try
            {
                NotaDebitoDetalle notaCreditoDetalleReturn = null;
                NotaDebitoDetalle nota_debito_detalle = null;
                foreach (var oneDetaVent in arrNotaCredDeta)
                {
                    nota_debito_detalle = new NotaDebitoDetalle
                    {
                        fk_nota_debito = FkNotaCred,
                        fk_venta_detalle = Convert.ToInt32(oneDetaVent.id_venta_detalle),
                        cantidad = Convert.ToDecimal(oneDetaVent.cantidad),
                        precio = Convert.ToDecimal(oneDetaVent.precio),
                        codigo = oneDetaVent.codigo,
                        codigo_sunat = oneDetaVent.codigo_sunat,
                        descripcion = oneDetaVent.descripcion,
                        fk_tipo_afectacion_igv = oneDetaVent.fk_tipo_afectacion_igv,
                        fk_tipo_isc = oneDetaVent.fk_tipo_isc,
                        valor_venta = oneDetaVent.valor_venta,
                        igv = oneDetaVent.igv,
                        importe = oneDetaVent.importe,
                        estado = "1",
                        tipo_bien = oneDetaVent.tipo_bien
                    };
                    notaCreditoDetalleReturn = await CtrlNotaCredDeta.InsertNotaDebitoDetalle(nota_debito_detalle);
                    if (notaCreditoDetalleReturn == null) return 0;
                    //Guardamos en t_movimiento
                    //if (flgAfecStoc.Equals("1"))
                    //{
                    //    flgSaveMovi = await SaveMovimiento(notaCreditoDetalleReturn.id_nota_debito_detalle, Convert.ToInt32(oneDetaVent.fk_proyecto), Convert.ToDecimal(oneDetaVent.cantidad));
                    //    if (flgSaveMovi == 0) return 0;
                    //}
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public async Task<int> SaveMovimiento(int FkNotaCredDeta, int FkProd, decimal Cant)
        {
            MovimientoController CtrlMovi = new MovimientoController();
            Movimiento movimiento = null;
            Movimiento movimientoReturn = null;
            try
            {
                int FkAlma = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FkAlmaPrinc"]);
                int FkMoviTipo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MoviTipo_IngresoNotaDebito"]);
                //Insertamos en la tabla t_movimiento
                movimiento = new Movimiento
                {
                    fk_movimiento_tipo = FkMoviTipo,
                    fk_guia_remision_detalle = 0,
                    fk_venta_detalle = 0,
                    fk_comprobante_traslado_detalle = 0,
                    //fk_nota_debito_detalle = FkNotaCredDeta,
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

        public async Task<ActionResult> Edit(int IdNotaCred, string CallBy)
        {
            string msgReturn = "";
            NotaDebitoDetalleController CtrlNotaCredDeta = new NotaDebitoDetalleController();
            ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();

            NotaDebito nota_debito = null;
            List<NotaDebitoDetalle> lstNotaDebitoDetalle = new List<NotaDebitoDetalle>();
            List<ComprobanteTipo> lstComprobanteTipo = null;

            int FkCompTipoNotaCred = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_NotaDebito"]);

            try
            {
                nota_debito = await GetNotaDebitoById(IdNotaCred);
                if (nota_debito != null)
                {
                    lstNotaDebitoDetalle = await CtrlNotaCredDeta.GetNotaDebitoDetalleByNotaDebito(IdNotaCred);
                    if (lstNotaDebitoDetalle != null)
                    {
                        lstNotaDebitoDetalle = lstNotaDebitoDetalle.Where(i => !i.estado.Equals("0")).ToList();
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
                ViewBag.NotaDebitoDetalle = lstNotaDebitoDetalle;
                ViewBag.CallBy = CallBy;
                ViewBag.FkCompTipoNotaDebito = FkCompTipoNotaCred;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(nota_debito);
        }

        public async Task<ActionResult> ViewNotaDebitoPendientes(string CallBy)
        {
            List<NotaDebito> lstEntidad = null;
            lstEntidad = await GetNotaDebitoAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals("1") && i.saldo_disponible > 0).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }
        public async Task<ActionResult> ExportComprobante(int IdNotaDebVent)
        {
            NotaDebitoDetalleController CtrlVentDeta = new NotaDebitoDetalleController();
            List<ComprobanteVentaFullModel> CtrlCompVent = new List<ComprobanteVentaFullModel>();
            try
            {

                string DocuAfecta = "";
                string TotaIgv = "";
                string TotaNeto = "";
                string sigla = "";
                string TotalLetras = "";
                //Obtengo el Comprobante Venta
                //NotaDebito nota_debito_venta = await GetNotaDebitoById(IdNotaCredVent);
                List<NotaDebitoDetalle> nota_debito_venta = await CtrlVentDeta.GetNotaDebitoDetalleByNotaDebito(IdNotaDebVent);
                TotaIgv = nota_debito_venta[0].total_igv.ToString("#,##0.00");
                TotaNeto = nota_debito_venta[0].total_neto.ToString("#,##0.00");
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();

                string SeriCV = nota_debito_venta[0].nro_comprobante_comprobante_venta.Substring(0, 4);
                //SeriCV = nota_debito_venta.sigla + SeriCV;
                DocuAfecta = nota_debito_venta[0].descripcion_comprobante_tipo + " " + SeriCV + "-" + nota_debito_venta[0].nro_comprobante_comprobante_venta.Substring(5, nota_debito_venta[0].nro_comprobante_comprobante_venta.Length - 5);

                string MotivoNC = nota_debito_venta[0].motivo_nc.ToString();
                string Motivo = nota_debito_venta[0].motivo;

                string SeriNC = nota_debito_venta[0].nro_comprobante.Substring(1, 3);
                SeriNC = nota_debito_venta[0].sigla + SeriNC;
                sigla = "NOTA DE DEBITO ELECTRONICA";
                string NroNC = SeriNC + "-" + nota_debito_venta[0].nro_comprobante.Substring(5, nota_debito_venta[0].nro_comprobante.Length - 5);

                TotalLetras = nota_debito_venta[0].total_bruto.NumeroALetras();
                string DniRucClie = "";
                string NombClie = "";
                string DireClie = "";
                if (nota_debito_venta[0].id_cliente_natural > 0)
                {
                    DniRucClie = nota_debito_venta[0].dni_cliente_natural;
                    NombClie = nota_debito_venta[0].nombre_cliente_natural + " " + nota_debito_venta[0].apellido_paterno_cliente_natural + " " + nota_debito_venta[0].apellido_materno_cliente_natural;
                    DireClie = nota_debito_venta[0].direccion_cliente_natural;
                }
                else
                {
                    DniRucClie = nota_debito_venta[0].ruc_empresa_cliente_juridico;
                    NombClie = nota_debito_venta[0].razon_social;
                    DireClie = nota_debito_venta[0].direccion_empresa;
                }
                //DataSet data = await Get_DataNotaDebito(IdNotaCredVent);
                //if (data?.Tables.Count > 0)
                //{
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrNotaDebitoVenta.rpt")));
                rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                rd.SetParameterValue("@fk_nota_debito", IdNotaDebVent);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData =
                    qrGenerator.CreateQrCode(nota_debito_venta[0].cadena_para_codigo_qr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(5);
                qrCodeImage.Save(Server.MapPath("~/img/reportes/qrNCV" + IdNotaDebVent + ".jpg"),
                    ImageFormat.Jpeg);
                string path = Server.MapPath("~/img/reportes/qrNCV" + IdNotaDebVent + ".jpg");
                for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                {
                    if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                    {
                        rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                    }
                }

                // rd.SetDataSource(data.Tables[0]);
                rd.SetParameterValue("ParamSiglaNC", sigla);
                rd.SetParameterValue("ParamLetras", TotalLetras);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();


                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return File(stream, "application/pdf", "NotaDebito-" + NroNC + ".pdf");

                //}
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception ex)
            {
                return null;
            }

            // return View();
        }
        public async Task<ActionResult> PrintNotaDebito(int IdNotaCredVent, string DataQr)
        {
            try
            {
                string DocuAfecta = "";
                string TotaIgv = "";
                string TotaNeto = "";
                //Obtengo el Comprobante Venta
                NotaDebito nota_debito_venta = await GetNotaDebitoById(IdNotaCredVent);
                TotaIgv = nota_debito_venta.total_igv.ToString("#,##0.00");
                TotaNeto = nota_debito_venta.total_neto.ToString("#,##0.00");
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();

                string SeriCV = nota_debito_venta.nro_comprobante_comprobante_venta.Substring(0, 4);
                //SeriCV = nota_debito_venta.sigla + SeriCV;
                DocuAfecta = nota_debito_venta.descripcion_comprobante_tipo + " " + SeriCV + "-" + nota_debito_venta.nro_comprobante_comprobante_venta.Substring(5, nota_debito_venta.nro_comprobante_comprobante_venta.Length - 5);

                string MotivoNC = nota_debito_venta.motivo_nc.ToString();
                string Motivo = nota_debito_venta.motivo;

                string SeriNC = nota_debito_venta.nro_comprobante.Substring(1, 3);
                SeriNC = nota_debito_venta.sigla + SeriNC;
                string NroNC = "NC - " + SeriNC + "-" + nota_debito_venta.nro_comprobante.Substring(5, nota_debito_venta.nro_comprobante.Length - 5);

                string DniRucClie = "";
                string NombClie = "";
                string DireClie = "";
                if (nota_debito_venta.id_cliente_natural > 0)
                {
                    DniRucClie = nota_debito_venta.dni_cliente_natural;
                    NombClie = nota_debito_venta.nombre_cliente_natural + " " + nota_debito_venta.apellido_paterno_cliente_natural + " " + nota_debito_venta.apellido_materno_cliente_natural;
                    DireClie = nota_debito_venta.direccion_cliente_natural;
                }
                else
                {
                    DniRucClie = nota_debito_venta.ruc_empresa_cliente_juridico;
                    NombClie = nota_debito_venta.razon_social;
                    DireClie = nota_debito_venta.direccion_empresa;
                }
                DataSet data = await Get_DataNotaDebito(IdNotaCredVent);
                if (data?.Tables.Count > 0)
                {
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrNotaDebitoVenta.rpt")));
                    rd.SetParameterValue("@fk_nota_debito", IdNotaCredVent);

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData =
                        qrGenerator.CreateQrCode(DataQr, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    qrCodeImage.Save(Server.MapPath("~/img/reportes/qrNCV" + IdNotaCredVent + ".jpg"),
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                    string path = Server.MapPath("~/img/reportes/qrNCV" + IdNotaCredVent + ".jpg");
                    for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                    {
                        if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                        {
                            rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                        }
                    }

                    rd.SetDataSource(data.Tables[0]);
                    rd.SetParameterValue("ParamDniRucCliente", DniRucClie);
                    rd.SetParameterValue("ParamCliente", NombClie);
                    rd.SetParameterValue("ParamDireccion", DireClie);
                    rd.SetParameterValue("ParamComprobanteNumero", NroNC);
                    rd.SetParameterValue("ParamIgv", TotaIgv);
                    rd.SetParameterValue("ParamNeto", TotaNeto);
                    rd.SetParameterValue("ParamDocuAfecta", DocuAfecta);
                    rd.SetParameterValue("ParamMotivo", Motivo);
                    rd.SetParameterValue("ParamMotivoNC", MotivoNC);

                    PrintDocument pDoc = new PrintDocument();
                    PrintLayoutSettings PrintLayout = new PrintLayoutSettings();
                    PrinterSettings printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = WebConfigurationManager.AppSettings["DefaultPrinter"];
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
        public async Task<ActionResult> PrintNotaDebitoView(int IdNotaDebVent, string DataQr)
        {
            
            NotaDebitoDetalleController CtrlVentDeta = new NotaDebitoDetalleController();
            List<ComprobanteVentaFullModel> CtrlCompVent = new List<ComprobanteVentaFullModel>();
            try
            {

                string DocuAfecta = "";
                string TotaIgv = "";
                string TotaNeto = "";
                string sigla = "";
                string TotalLetras = "";
                //Obtengo el Comprobante Venta
                //NotaDebito nota_debito_venta = await GetNotaDebitoById(IdNotaCredVent);
                List<NotaDebitoDetalle> nota_debito_venta = await CtrlVentDeta.GetNotaDebitoDetalleByNotaDebito(IdNotaDebVent);
                TotaIgv = nota_debito_venta[0].total_igv.ToString("#,##0.00");
                TotaNeto = nota_debito_venta[0].total_neto.ToString("#,##0.00");
                string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();

                string SeriCV = nota_debito_venta[0].nro_comprobante_comprobante_venta.Substring(0, 4);
                //SeriCV = nota_debito_venta.sigla + SeriCV;
                DocuAfecta = nota_debito_venta[0].descripcion_comprobante_tipo + " " + SeriCV + "-" + nota_debito_venta[0].nro_comprobante_comprobante_venta.Substring(5, nota_debito_venta[0].nro_comprobante_comprobante_venta.Length - 5);

                string MotivoNC = nota_debito_venta[0].motivo_nc.ToString();
                string Motivo = nota_debito_venta[0].motivo;

                string SeriNC = nota_debito_venta[0].nro_comprobante.Substring(1, 3);
                SeriNC = nota_debito_venta[0].sigla + SeriNC;
                sigla = "NOTA DE DEBITO ELECTRONICA";
                string NroNC = SeriNC + "-" + nota_debito_venta[0].nro_comprobante.Substring(5, nota_debito_venta[0].nro_comprobante.Length - 5);

                TotalLetras = nota_debito_venta[0].total_bruto.NumeroALetras();
                string DniRucClie = "";
                string NombClie = "";
                string DireClie = "";
                if (nota_debito_venta[0].id_cliente_natural > 0)
                {
                    DniRucClie = nota_debito_venta[0].dni_cliente_natural;
                    NombClie = nota_debito_venta[0].nombre_cliente_natural + " " + nota_debito_venta[0].apellido_paterno_cliente_natural + " " + nota_debito_venta[0].apellido_materno_cliente_natural;
                    DireClie = nota_debito_venta[0].direccion_cliente_natural;
                }
                else
                {
                    DniRucClie = nota_debito_venta[0].ruc_empresa_cliente_juridico;
                    NombClie = nota_debito_venta[0].razon_social;
                    DireClie = nota_debito_venta[0].direccion_empresa;
                }
                //DataSet data = await Get_DataNotaDebito(IdNotaCredVent);
                //if (data?.Tables.Count > 0)
                //{
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrNotaDebitoVenta.rpt")));
                rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                rd.SetParameterValue("@fk_nota_debito", IdNotaDebVent);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData =
                    qrGenerator.CreateQrCode(nota_debito_venta[0].cadena_para_codigo_qr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(5);
                qrCodeImage.Save(Server.MapPath("~/img/reportes/qrNCV" + IdNotaDebVent + ".jpg"),
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                string path = Server.MapPath("~/img/reportes/qrNCV" + IdNotaDebVent + ".jpg");
                for (int i = 0; i < rd.DataDefinition.FormulaFields.Count; i++)
                {
                    if (rd.DataDefinition.FormulaFields[i].FormulaName == "{@varimage}")
                    {
                        rd.DataDefinition.FormulaFields[i].Text = "'" + path + "'";
                    }
                }

                // rd.SetDataSource(data.Tables[0]);
                rd.SetParameterValue("ParamSiglaNC", sigla);
                rd.SetParameterValue("ParamLetras", TotalLetras);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                //rd.SetDataSource(data.Tables[0]);
                //rd.SetParameterValue("ParamSiglaNC", sigla);

                Directory.CreateDirectory(@"C:\reportesotros");
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\ND-" + NroNC + ".pdf");

                ViewBag.Printer = "ND-" + NroNC;
                ViewBag.Codigo = "ND-" + NroNC + ".pdf";

                //return null;
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar!";
                ViewBag.Codigo = "";
            }
            return View();
        }
        public async Task<DataSet> Get_DataNotaDebito(int FkNotaCred)
        {
            NotaDebitoDetalleController CtrlNotaCredDeta = new NotaDebitoDetalleController();
            var lstVentaDetalle = await CtrlNotaCredDeta.GetNotaDebitoDetalleByNotaDebito(FkNotaCred);
            if (lstVentaDetalle != null)
            {
                if (lstVentaDetalle.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_venta_detalle", typeof(int));
                    dt.Columns.Add("fk_venta", typeof(int));
                    dt.Columns.Add("fk_producto", typeof(int));
                    dt.Columns.Add("fk_tipo_afectacion_igv", typeof(int));
                    dt.Columns.Add("fk_tipo_isc", typeof(int));
                    dt.Columns.Add("cantidad", typeof(decimal));
                    dt.Columns.Add("precio", typeof(decimal));
                    dt.Columns.Add("estado", typeof(string));
                    dt.Columns.Add("id_producto", typeof(int));
                    dt.Columns.Add("fk_proveedor", typeof(int));
                    dt.Columns.Add("fk_producto_marca", typeof(int));
                    dt.Columns.Add("fk_producto_subfamilia", typeof(int));
                    dt.Columns.Add("cod_producto", typeof(string));
                    dt.Columns.Add("nom_producto", typeof(string));
                    dt.Columns.Add("codigo_sku", typeof(string));
                    dt.Columns.Add("embalaje", typeof(string));
                    dt.Columns.Add("image_url", typeof(string));
                    dt.Columns.Add("estado_producto", typeof(string));
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

                    foreach (var item in lstVentaDetalle)
                    {
                        DataRow row = dt.NewRow();
                        row["id_venta_detalle"] = item.id_venta_detalle;
                        row["fk_venta"] = item.fk_venta;
                        row["fk_producto"] = item.fk_producto;
                        row["fk_tipo_afectacion_igv"] = item.fk_tipo_afectacion_igv;
                        row["fk_tipo_isc"] = item.fk_tipo_isc;
                        row["cantidad"] = item.cantidad;
                        row["precio"] = item.precio;
                        row["estado"] = item.estado;
                        row["id_producto"] = item.id_producto;
                        row["fk_proveedor"] = item.fk_proveedor;
                        row["fk_producto_marca"] = item.fk_producto_marca;
                        row["fk_producto_subfamilia"] = item.fk_producto_subfamilia;
                        row["cod_producto"] = item.cod_producto;
                        row["nom_producto"] = item.nom_producto;
                        row["codigo_sku"] = item.codigo_sku;
                        row["embalaje"] = item.embalaje;
                        row["image_url"] = item.image_url;
                        row["estado_producto"] = item.estado_producto;
                        row["id_producto_marca"] = item.id_producto_marca;
                        row["descripcion_marca"] = item.nombre_almacen;
                        row["estado_producto_marca"] = item.descripcion_marca;
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
    }
}