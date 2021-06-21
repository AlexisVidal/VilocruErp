using ERP.Admin.App_Start;
using ERP.Admin.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    public class CompraController : Controller
    {
        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        public async Task<Compra> InsertCompra(Compra Compra)
        {
            Compra entidad = null;
            try
            {
                List<Compra> lstEntidad = null;

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Compra/agregar", Compra);

                var model = response.Content.ReadAsAsync<List<Compra>>();
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

        public async Task<Compra> UpdateEstadoCompra(int IdComp, string EstaComp, string MotiCier)
        {
            Compra entidad = null;
            try
            {
                List<Compra> lstEntidad = null;

                var client = new HttpClient();
                Compra parametro = new Compra { id_compra = IdComp, motivo_cierre= MotiCier, estado = EstaComp };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Compra/cambiarEstado", parametro);

                var model = response.Content.ReadAsAsync<List<Compra>>();
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

        public async Task<List<Compra>> GetCompraAll()
        {
            List<Compra> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Compra/all");
                var model = response.Content.ReadAsAsync<List<Compra>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Compra>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<Compra> GetCompraById(int IdComp)
        {
            Compra entidad = null;
            try
            {
                List<Compra> lstEntidad = null;

                Compra parametro = new Compra { id_compra = IdComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Compra/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<Compra>>();
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

        // GET: Compra
        public async Task<ActionResult> Index(string EstaFilt)
        {
            List<Compra> lstEntidad = null;
            lstEntidad = await GetCompraAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> Edit(int IdComp, string CallBy)
        {
            string msgReturn = "";
            CompraDetalleController CtrlCompDeta = new CompraDetalleController();
            GuiaRemisionController CtrlGuiaRemi = new GuiaRemisionController();
            Compra compra = null;
            List<CompraDetalle> lstCompraDetalle = null;
            List<GuiaRemision> lstGuiaRemision = null;
            try
            {
                compra = await GetCompraById(IdComp);
                lstCompraDetalle = await CtrlCompDeta.GetCompraDetalleByFkCompra(IdComp);
                if (lstCompraDetalle != null)
                {
                    lstCompraDetalle = lstCompraDetalle.Where(i => !i.estado.Equals("0")).ToList();
                }
                //Guia con las que ha siddo despachada
                lstGuiaRemision = await CtrlGuiaRemi.Get_GuiaRemisionByFkCompra(IdComp);
                ViewBag.CompraDetalle = lstCompraDetalle;
                ViewBag.GuiaRemision = lstGuiaRemision;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(compra);
        }

        public async Task<ActionResult> ListaCompra(string CallBy)
        {
            List<Compra> lstEntidad = null;
            lstEntidad = await GetCompraAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals("1") || i.estado.Equals("2")).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ListaCompraFacturar(string CallBy)
        {
            List<Compra> lstEntidad = null;
            lstEntidad = await GetCompraAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals("2") || i.estado.Equals("3")).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> SaveChangeEstadoCompra(int IdComp, string EstaComp, string MotiCier)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El proceso se concluyó satisfactoriamente";
            try
            {
                Compra compraReturn = null;
                compraReturn = await UpdateEstadoCompra(IdComp, EstaComp, MotiCier);
                if (compraReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar Actualizar la OC, Por favor comuniquese con el administrador de sistemas";
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
                msgReturn = "Ocurrió un error al intentar Actualizar la OC, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        private async Task<DataSet> Get_DataCompraDetalle(int IdComp)
        {
            CompraDetalleController CtrlCompDeta = new CompraDetalleController();
            var lCompraDetalle = await CtrlCompDeta.GetCompraDetalleByFkCompra(IdComp);
            if (lCompraDetalle != null)
            {
                if (lCompraDetalle.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("fk_compra", typeof(int));
                    dt.Columns.Add("n_compra", typeof(string));
                    dt.Columns.Add("f_compra", typeof(string));
                    dt.Columns.Add("agencia_transporte", typeof(string));
                    dt.Columns.Add("observacion", typeof(string));
                    dt.Columns.Add("ruc", typeof(string));
                    dt.Columns.Add("razon_social", typeof(string));
                    dt.Columns.Add("direccion", typeof(string));
                    dt.Columns.Add("cod_producto", typeof(string));
                    dt.Columns.Add("nom_producto", typeof(string));
                    dt.Columns.Add("cantidad", typeof(decimal));
                    dt.Columns.Add("precio", typeof(decimal));

                    foreach (var item in lCompraDetalle)
                    {
                        DataRow row = dt.NewRow();
                        row["fk_compra"] = item.fk_compra;
                        row["n_compra"] = item.n_compra;
                        row["f_compra"] = Convert.ToDateTime(item.f_compra).ToString("dd/MM/yyyy");
                        row["agencia_transporte"] = item.agencia_transporte;
                        row["observacion"] = item.observacion;
                        row["ruc"] = item.ruc;
                        row["razon_social"] = item.razon_social;
                        row["direccion"] = item.direccion;
                        row["cod_producto"] = item.cod_producto;
                        row["nom_producto"] = item.nom_producto;
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

        public async Task<ActionResult> PrintOrdenCompra(string IdComp)
        {
            try
            {
                int idi = Convert.ToInt32(IdComp);
                DataSet data = await Get_DataCompraDetalle(idi);
                if (data?.Tables.Count > 0)
                {
                    if (data != null)
                    {
                        string codigus = data.Tables[0].Rows[0]["fk_compra"].ToString();
                        using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                        {
                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/img/reportes/CrOrdenCompra.rpt")));
                            rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                            rd.SetParameterValue("@fk_compra", idi);

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
                            //rd.SetParameterValue("ParamMoneda", DescMone);
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