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
using System.Data;
using System.Security.Principal;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Globalization;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class PedidoController : Controller
    {
        public async Task<Pedido> InsertPedido(Pedido pedido)
        {
            Pedido entidad = null;
            List<Pedido> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Pedido/agregar", pedido);

                var model = response.Content.ReadAsAsync<List<Pedido>>();
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

        public async Task<List<Pedido>> GetPedidoAll_Venta()
        {
            List<Pedido> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("venta/PedidoAll");
                var model = response.Content.ReadAsAsync<List<Pedido>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Pedido>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        //public async Task<Pedido> GetPedidoId(int IdPedi)
        //{
        //    List<Pedido> lstEntidad = null;
        //    Pedido pedido = null;
        //    HttpClient client = new HttpClient();
        //    try
        //    {
        //        lstEntidad = await GetPedidoAll_Venta();
        //        if (lstEntidad !=null)
        //        {
        //            lstEntidad = lstEntidad.Where(i=> i.id_pedido.Equals(IdPedi)).ToList();
        //            if(lstEntidad.Count > 0)
        //            {
        //                pedido = lstEntidad[0];
        //            }
        //        }
        //        else
        //        {
        //            pedido = new Pedido();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return pedido;
        //}

        private async Task<DataSet> Get_DataSalida(int id)
        {
            PedidoDetalleController CtrlPediDeta = new PedidoDetalleController();
            var lpedido = await CtrlPediDeta.Get_PedidoDetalleByFkPedido(id);
            if (lpedido != null)
            {
                if (lpedido.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_detalle_pedido", typeof(int));
                    dt.Columns.Add("fk_pedido", typeof(int));
                    dt.Columns.Add("cod_producto", typeof(string));
                    dt.Columns.Add("nom_producto", typeof(string));
                    dt.Columns.Add("descripcion_producto_marca", typeof(string));
                    dt.Columns.Add("descripcion_producto_subfamilia", typeof(string));
                    dt.Columns.Add("cantidad", typeof(decimal));
                    dt.Columns.Add("precio", typeof(decimal));
                    
                    foreach (var item in lpedido)
                    {
                        DataRow row = dt.NewRow();
                        row["id_detalle_pedido"] = item.id_detalle_pedido;
                        row["fk_pedido"] = item.fk_pedido;
                        row["cod_producto"] = item.cod_producto;
                        row["nom_producto"] = item.nom_producto;
                        row["descripcion_producto_marca"] = item.descripcion_producto_marca;
                        row["descripcion_producto_subfamilia"] = item.descripcion_producto_subfamilia;
                        row["cantidad"] = item.cantidad;
                        row["precio"] = item.precio;
                        
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

        // GET: Pedido
        public ActionResult IndexCotizacion()
        {
            return View();
        }

        public async Task<ActionResult> ListIndex(string CharPC)
        {
            List<Pedido> lstEntidad = null;
            lstEntidad = await GetPedidoAll_Venta();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => i.n_pedido.Substring(0, 1).ToUpper().Equals(CharPC) && i.estado.Equals("1")).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ViewPedidoFacturar(string CharPC, string CallBy)
        {
            List<Pedido> lstEntidad = null;
            lstEntidad = await GetPedidoAll_Venta();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals("1")).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ListaPedidoFacturar(string CharPC, string CallBy)
        {
            List<Pedido> lstEntidad = null;
            lstEntidad = await GetPedidoAll_Venta();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => i.n_pedido.Substring(0,1).ToUpper().Equals(CharPC) && i.estado.Equals("1")).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> Create()
        {
            string msgReturn = "";
            try
            {
                ComprobanteTipoController CtrlCompTipo = new ComprobanteTipoController();

                List<ComprobanteTipo> lstComprobanteTipo = null;

                int FkCompTipoCoti = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompTipo_Cotizacion"]);

                lstComprobanteTipo = await CtrlCompTipo.GetComprobanteTipoAll();
                if (lstComprobanteTipo != null)
                {
                    lstComprobanteTipo = lstComprobanteTipo.Where(i => !i.estado.Equals("0")).ToList();
                }
                
                ViewBag.ComprobanteTipo = lstComprobanteTipo;
                ViewBag.FkTipoComprobanteCotizacion = FkCompTipoCoti;
            }
            catch (Exception)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public async Task<ActionResult> SaveNewPedido(int FkClie, List<List<string>> arrVentDeta)
        {
            string msgReturn = "";
            int IdNewPedi = 0;
            int FkProd = 0;
            decimal Cant = 0;
            decimal Prec = 0;
            int flgError = 0;
            msgReturn = "La Cotización se registró satisfactoriamente";
            try
            {
                PedidoDetalleController CtrlPediDeta = new PedidoDetalleController();
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                Pedido pedidoReturn = null;
                PedidoDetalle pedidoDetalleReturn = null;
                Pedido pedido = new Pedido
                {
                    fk_cliente = FkClie,
                    fk_userventa = FkUsua,
                    n_pedido = "",
                    tipo_pago = "1"
                };
                pedidoReturn = await InsertPedido(pedido);
                if (pedidoReturn != null)
                {
                    IdNewPedi = pedidoReturn.id_pedido;
                    if (arrVentDeta != null)
                    {
                        foreach(var onePediDeta in arrVentDeta)
                        {
                            FkProd = Convert.ToInt32(onePediDeta[0]);
                            Cant = Convert.ToDecimal(onePediDeta[1]);
                            Prec = Convert.ToDecimal(onePediDeta[2]);
                            PedidoDetalle pedido_detalle = new PedidoDetalle
                            {
                                fk_pedido = IdNewPedi,
                                fk_producto = FkProd,
                                cantidad = Cant,
                                precio = Prec
                            };
                            pedidoDetalleReturn = await CtrlPediDeta.InsertPedidoDetalle(pedido_detalle);
                            if (pedidoDetalleReturn == null) {
                                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                                break;
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
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PrintIndex(string id, string NroPedi, string FechPedi, string DniRuc, 
            string NombClie, string UsuaVent)
        {
            try
            {
                int idi = Convert.ToInt32(id);
                DataSet data = await Get_DataSalida(idi);
                if (data?.Tables.Count > 0)
                {
                    if (data != null)
                    {
                        string codigus = data.Tables[0].Rows[0]["id_detalle_pedido"].ToString();
                        using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                        {
                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrCotizacion.rpt")));
                            rd.SetParameterValue("@fk_pedido", idi);
                            
                            string path = Server.MapPath("~/img/reportes/qrguia" + id.PadLeft(6, '0') + ".jpg");
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
                            rd.SetParameterValue("ParamNroPedido", NroPedi);
                            rd.SetParameterValue("ParamFechPedido", FechPedi);
                            rd.SetParameterValue("ParamDniRuc", DniRuc);
                            rd.SetParameterValue("ParamNombCliente", NombClie);
                            rd.SetParameterValue("ParamNombUsuaVenta", UsuaVent);
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

        public async Task<Pedido> UpdatePedidoVenta(Pedido pedido)
        {
            Pedido entidad = null;
            List<Pedido> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Pedido/updateventa", pedido);

                var model = response.Content.ReadAsAsync<List<Pedido>>();
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