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
    public class OrdenCompraController : Controller
    {
        public async Task<int> InsertOrderCompra(OrdenCompra orden_compra)
        {
            int NewIdOrdeComp = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("OrdenCompra/agregar", orden_compra);

                var rptaReturn = response.Content.ReadAsAsync<string>();
                if (rptaReturn != null && rptaReturn.Result.ToString() != "0")
                {
                    NewIdOrdeComp = Convert.ToInt32(rptaReturn.Result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return NewIdOrdeComp;
        }

        public async Task<int> UpdateOrderCompra(OrdenCompra orden_compra)
        {
            int rptaReturn = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("OrdenCompra/modificar", orden_compra);

                var rptaUpdate = response.Content.ReadAsAsync<string>();
                if (rptaUpdate != null && rptaUpdate.Result.ToString() != "0")
                {
                    rptaReturn = Convert.ToInt32(rptaUpdate.Result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return rptaReturn;
        }

        public async Task<int> UpdateEstadoOrderCompra(int IdOrdeComp, string EstaOrdeComp)
        {
            int rptaReturn = 0;
            try
            {
                var client = new HttpClient();
                OrdenCompra orden_compra = new OrdenCompra { id_orden_compra = IdOrdeComp, estado = EstaOrdeComp };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("OrdenCompra/cambiarEstado", orden_compra);

                var rptaUpdate = response.Content.ReadAsAsync<string>();
                if (rptaUpdate != null && rptaUpdate.Result.ToString() != "0")
                {
                    rptaReturn = Convert.ToInt32(rptaUpdate.Result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return rptaReturn;
        }

        public async Task<OrdenCompra> GetOrderCompraById(int IdOrdeComp)
        {
            OrdenCompra entidad = null;
            try
            {
                List<OrdenCompra> lstEntidad = null;

                OrdenCompra parametro = new OrdenCompra { id_orden_compra = IdOrdeComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("OrdenCompra/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<OrdenCompra>>();
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

        public async Task<List<OrdenCompra>> GetOrderCompraAll()
        {
            List<OrdenCompra> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("OrdenCompra/all");
                var model = response.Content.ReadAsAsync<List<OrdenCompra>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.OrdenCompra>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: OrdenCompra
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ListaOrdenCompra(string EstaFilt)
        {
            List<OrdenCompra> lstOrdenCompra = null;
            lstOrdenCompra = await GetOrderCompraAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstOrdenCompra = lstOrdenCompra.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstOrdenCompra);
        }

        public async Task<ActionResult> Create(int? FkProd)
        {
            AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
            ProductoErp producto = null;
            if (FkProd != null)
            {
                producto = await CtrlAlmaStoc.GetProductoIdErp(Convert.ToInt32(FkProd));
            }
            ViewBag.Producto = producto;
            return PartialView();
        }

        public async Task<ActionResult> Edit(int IdOrdeComp, string CallBy)
        {
            string msgReturn = "";
            OrdenCompraDetalleController CtrlOCDeta = new OrdenCompraDetalleController();
            OrdenCompra orden_compra = null;
            List<OrdenCompraDetalle> lstOrdenCompraDetalle = null;
            try
            {
                orden_compra = await GetOrderCompraById(IdOrdeComp);
                lstOrdenCompraDetalle = await CtrlOCDeta.GetOrderCompraDetalleByFkOrdenCompra(IdOrdeComp);
                lstOrdenCompraDetalle = lstOrdenCompraDetalle.Where(i => !i.estado.Equals("0")).ToList();
                ViewBag.OrdenCompraDetalle = lstOrdenCompraDetalle;
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(orden_compra);
        }

        public async Task<ActionResult> SaveNewOrdenCompra(int FkProv, string FechOrdeComp, List<List<string>> arrOCDeta)
        {
            string msgReturn = "";
            string NroOCReturn = "";
            int NewIdOrdComp = 0;
            int NewIdOCDeta = 0;
            DateTime NewFechOC = DateTime.Now;
            int flgError = 0;
            msgReturn = "La Orden de Compra se registró satisfactoriamente";
            try
            {
                OrdenCompraDetalleController CtrlOrdeCompDeta = new OrdenCompraDetalleController();
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                OrdenCompraDetalle orden_compra_detalle = null;
                NewFechOC = Convert.ToDateTime(FechOrdeComp);
                OrdenCompra orden_compra = new OrdenCompra
                {
                    fk_usuario = FkUsua,
                    fk_proveedor = FkProv,
                    n_orden_compra = "",
                    f_emision = NewFechOC.ToString("yyyy/MM/dd H:mm:ss")
                };
                NewIdOrdComp = await InsertOrderCompra(orden_compra);
                if (NewIdOrdComp > 0)
                {
                    //Guardamos los detalles de la OC
                    if (arrOCDeta != null)
                    {
                        foreach (var oneOCDeta in arrOCDeta)
                        {
                            orden_compra_detalle = new OrdenCompraDetalle
                            {
                                fk_orden_compra = NewIdOrdComp,
                                fk_producto = Convert.ToInt32(oneOCDeta[0]),
                                cantidad = Convert.ToDecimal(oneOCDeta[1]),
                                precio = Convert.ToDecimal(oneOCDeta[2])
                            };
                            NewIdOCDeta = await CtrlOrdeCompDeta.InsertOrderCompraDetalle(orden_compra_detalle);
                            if (NewIdOCDeta <= 0)
                            {
                                msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                                flgError = 1;
                                break;
                            }
                        }
                    }
                    NroOCReturn = orden_compra.n_orden_compra;
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
            return Json(new { msgRpta = msgReturn, NroOCReturn = NroOCReturn }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveEditOrdenCompra(int IdOrdeComp, int FkProv, string NroOrdeComp,
            string FechOrdeComp, string EstaOrdeComp, List<List<string>> arrOCDeta,
            List<string> arrDeleOCDeta)
        {
            string msgReturn = "";
            int rptaUpdateOC = 0;
            int rptaDelete = 0;
            int rptaUpdate = 0;
            DateTime NewFechOC = DateTime.Now;
            int flgError = 0;
            msgReturn = "La Orden de Compra se actualizó satisfactoriamente";
            try
            {
                OrdenCompraDetalleController CtrlOrdeCompDeta = new OrdenCompraDetalleController();
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                NewFechOC = Convert.ToDateTime(FechOrdeComp);
                OrdenCompra orden_compra = new OrdenCompra
                {
                    id_orden_compra = IdOrdeComp,
                    fk_usuario = FkUsua,
                    fk_proveedor = FkProv,
                    n_orden_compra = NroOrdeComp,
                    f_emision = NewFechOC.ToString("yyyy/MM/dd H:mm:ss"),
                    estado = EstaOrdeComp
                };
                rptaUpdateOC = await UpdateOrderCompra(orden_compra);
                if (rptaUpdateOC > 0)
                {
                    //Eliminamos los detalles de la OC
                    if (arrDeleOCDeta != null)
                    {
                        rptaDelete = await DeleteListOrdenCompraDetalle(arrDeleOCDeta);
                        if (rptaDelete == 0)
                        {
                            msgReturn = "Ocurrió un error al intentar Actualizar los detalles, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                    if (flgError == 0)
                    {
                        //Actualizamos los detalle de la OC
                        if (arrOCDeta != null)
                        {
                            rptaUpdate = await UpdateListOrdenCompraDetalle(IdOrdeComp, arrOCDeta);
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

        public async Task<int> DeleteListOrdenCompraDetalle(List<string> arrDeleOCDeta)
        {
            OrdenCompraDetalleController CtrlOCDeta = new OrdenCompraDetalleController();
            int rptaDeleOCDeta = 0;
            try
            {
                foreach (var oneIdOrdeCompDeta in arrDeleOCDeta)
                {
                    rptaDeleOCDeta = await CtrlOCDeta.DeleteOrderCompraDetalle(Convert.ToInt32(oneIdOrdeCompDeta));
                    if (rptaDeleOCDeta <= 0)
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

        public async Task<int> UpdateListOrdenCompraDetalle(int FkOrdeComp, List<List<string>> arrOCDeta)
        {
            int rptaUpdaOCDeta = 0;
            try
            {
                OrdenCompraDetalleController CtrlOCDeta = new OrdenCompraDetalleController();
                OrdenCompraDetalle orden_compra_detalle = null;
                foreach (var oneOCDeta in arrOCDeta)
                {
                    if (Convert.ToInt32(oneOCDeta[0]) == 0)
                    {
                        //Insertamos nuevo detalle
                        orden_compra_detalle = new OrdenCompraDetalle
                        {
                            fk_orden_compra = FkOrdeComp,
                            fk_producto = Convert.ToInt32(oneOCDeta[1]),
                            cantidad = Convert.ToDecimal(oneOCDeta[2]),
                            precio = Convert.ToDecimal(oneOCDeta[3])
                        };
                        rptaUpdaOCDeta = await CtrlOCDeta.InsertOrderCompraDetalle(orden_compra_detalle);
                        if (rptaUpdaOCDeta <= 0)
                        {
                            rptaUpdaOCDeta = 0;
                            break;
                        }
                    }
                    else
                    {
                        //Actualizamos el detalle
                        orden_compra_detalle = new OrdenCompraDetalle
                        {
                            id_orden_compra_detalle = Convert.ToInt32(oneOCDeta[0]),
                            fk_orden_compra = FkOrdeComp,
                            fk_producto = Convert.ToInt32(oneOCDeta[1]),
                            cantidad = Convert.ToDecimal(oneOCDeta[2]),
                            precio = Convert.ToDecimal(oneOCDeta[3]),
                            estado = oneOCDeta[4]
                        };
                        rptaUpdaOCDeta = await CtrlOCDeta.UpdateOrderCompraDetalle(orden_compra_detalle);
                        if (rptaUpdaOCDeta <= 0)
                        {
                            rptaUpdaOCDeta = 0;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return rptaUpdaOCDeta;
        }

        public async Task<ActionResult> SaveChangeEstadoOrdenCompra(int IdOrdeComp, string EstaOrdeComp)
        {
            string msgReturn = "";
            int rptaReturn = 0;
            DateTime NewFechOC = DateTime.Now;
            int flgError = 0;
            msgReturn = "El proceso se concluyó satisfactoriamente";
            try
            {
                OrdenCompraDetalleController CtrlOrdeCompDeta = new OrdenCompraDetalleController();
                rptaReturn = await UpdateEstadoOrderCompra(IdOrdeComp, EstaOrdeComp);
                if (rptaReturn <= 0)
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

        public async Task<ActionResult> SaveAprobarOrdenCompra(int IdOrdeComp, string AgenTrans, string ObseOrdeComp)
        {
            string msgReturn = "";
            DateTime NewFechOC = DateTime.Now;
            int flgError = 0;
            int rptaChangeEstado = 0;
            msgReturn = "Orden de compra aprobada  satisfactoriamente";
            OrdenCompra orden_compra = null;
            Compra compra = null;
            Compra compraReturn = null;
            List<OrdenCompraDetalle> lst_orden_compra_detalle = null;
            CompraDetalle compra_detalle = null;
            CompraDetalle compraDetalleReturn = null;
            try
            {
                CompraController CtrlComp = new CompraController();
                CompraDetalleController CtrlCompDeta = new CompraDetalleController();
                OrdenCompraDetalleController CtrlOrdeCompDeta = new OrdenCompraDetalleController();
                orden_compra = await GetOrderCompraById(IdOrdeComp);
                int FkUsua = Convert.ToInt32(Session["IdUsuario"]);
                if (orden_compra != null)
                {
                    compra = new Compra
                    {
                        fk_orden_compra = orden_compra.id_orden_compra,
                        fk_usuario = FkUsua,
                        fk_proveedor = orden_compra.fk_proveedor,
                        n_compra = "",
                        f_compra = DateTime.Now.ToString("yyyy/MM/dd"),
                        agencia_transporte = AgenTrans.ToUpper(),
                        observacion = ObseOrdeComp
                    };
                    compraReturn = await CtrlComp.InsertCompra(compra);
                    if (compraReturn != null)
                    {
                        lst_orden_compra_detalle = await CtrlOrdeCompDeta.GetOrderCompraDetalleByFkOrdenCompra(orden_compra.id_orden_compra);
                        if (lst_orden_compra_detalle != null)
                        {
                            foreach (var oneOCDeta in lst_orden_compra_detalle)
                            {
                                compra_detalle = new CompraDetalle
                                {
                                    fk_compra = compraReturn.id_compra,
                                    fk_producto = oneOCDeta.fk_producto,
                                    cantidad = oneOCDeta.cantidad,
                                    precio = oneOCDeta.precio
                                };
                                compraDetalleReturn = await CtrlCompDeta.InsertCompraDetalle(compra_detalle);
                                if (compraDetalleReturn == null)
                                {
                                    msgReturn = "Ocurrió un error al intentar clonar los detalles de la OC, Por favor comuniquese con el administrador de sistemas";
                                    flgError = 1;
                                    break;
                                }
                            }
                            if(flgError == 0)
                            {
                                rptaChangeEstado = await UpdateEstadoOrderCompra(orden_compra.id_orden_compra, "2");
                            }
                        }
                        else
                        {
                            msgReturn = "Ocurrió un error al intentar leer los detalles de la OC, Por favor comuniquese con el administrador de sistemas";
                            flgError = 1;
                        }
                    }
                    else
                    {
                        msgReturn = "Ocurrió un error al intentar Aprobar la OC, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al leer la OC, Por favor comuniquese con el administrador de sistemas";
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

        public async Task<ActionResult> ViewAprobar(int IdOrdeComp)
        {
            ViewBag.IdOrdenCompra = IdOrdeComp;
            return PartialView();
        }
    }

}