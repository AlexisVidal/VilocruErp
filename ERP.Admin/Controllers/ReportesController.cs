using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing.Printing;
using CrystalDecisions.Shared;
using System.Web.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReporteVentas()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }
        public ActionResult ReporteCompras()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }

        public async Task<ActionResult> ListDataReporteVenta(string FechInic, string FechFin, int FkUsua, int FkClieTipo, int FkClie, int FkSubFami)
        {
            ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
            List<ComprobanteVenta> lstEntidad = null;
            List<VentaDetalle> lstVentaDetalle = null;
            List<ComprobanteVenta> lstReturn = new List<ComprobanteVenta>();
            ComprobanteVenta comprobante_venta = null;
            try
            {
                lstEntidad = await CtrlCompVent.GetComprobanteVentaAll();
                if (lstEntidad != null)
                {
                    if (Convert.ToDateTime(FechInic) <= Convert.ToDateTime(FechFin))
                    {
                        lstEntidad = lstEntidad.Where(p => Convert.ToDateTime(p.f_emision).Date >= Convert.ToDateTime(FechInic).Date && Convert.ToDateTime(p.f_emision).Date <= Convert.ToDateTime(FechFin).Date).ToList();
                        lstEntidad = lstEntidad.OrderByDescending(p => p.estado).ToList();
                        var newGroup = lstEntidad.GroupBy(i => new { i.fk_comprobante_tipo, i.nro_comprobante }).Select(i =>
                         new
                         {
                             FEmis = i.First().f_emision,
                             TotaNeto = i.First().total_neto,
                             TotaIgv = i.First().total_igv,
                             TotaBrut = i.First().total_bruto,
                             IdClieNatu = i.First().id_cliente_natural,
                             DniClieNatu = i.First().dni_cliente_natural,
                             NombClieNatu = i.First().nombre_cliente_natural,
                             ApelPateClieNatu = i.First().apellido_paterno_cliente_natural,
                             ApelMateClieNatu = i.First().apellido_materno_cliente_natural,
                             IdClieJuri = i.First().id_cliente_juridico,
                             RucClieJuri = i.First().ruc_empresa_cliente_juridico,
                             RazoSoci = i.First().razon_social,
                             DescCompTipo = i.First().descripcion_comprobante_tipo,
                             Codi = i.First().codigo,
                             NroComp = i.First().nro_comprobante,
                             EstaComp = i.First().estado,
                             FKClieClieNatural = i.First().fk_cliente_cliente_natural,
                             FKClieClieJuridico = i.First().fk_cliente_cliente_juridico
                         });
                        foreach (var oneCompVent in newGroup)
                        {
                            comprobante_venta = new ComprobanteVenta
                            {
                                f_emision = oneCompVent.FEmis,
                                total_neto = oneCompVent.TotaNeto,
                                total_igv = oneCompVent.TotaIgv,
                                total_bruto = oneCompVent.TotaBrut,
                                id_cliente_natural = oneCompVent.IdClieNatu,
                                dni_cliente_natural = oneCompVent.DniClieNatu,
                                nombre_cliente_natural = oneCompVent.NombClieNatu,
                                apellido_paterno_cliente_natural = oneCompVent.ApelPateClieNatu,
                                apellido_materno_cliente_natural = oneCompVent.ApelMateClieNatu,
                                id_cliente_juridico = oneCompVent.IdClieJuri,
                                ruc_empresa_cliente_juridico = oneCompVent.RucClieJuri,
                                razon_social = oneCompVent.RazoSoci,
                                descripcion_comprobante_tipo = oneCompVent.DescCompTipo,
                                codigo = oneCompVent.Codi,
                                nro_comprobante = oneCompVent.NroComp,
                                estado = oneCompVent.EstaComp,
                                fk_cliente_cliente_natural = oneCompVent.FKClieClieNatural,
                                fk_cliente_cliente_juridico = oneCompVent.FKClieClieJuridico
                            };
                            lstReturn.Add(comprobante_venta);
                        }
                        lstReturn = lstReturn.OrderBy(p => p.f_emision).ToList();
                    }
                    else
                    {
                        return Json(-1, JsonRequestBehavior.AllowGet);
                    }
                    if (FkUsua > 0)
                    {
                        lstReturn = lstReturn.ToList();
                    }
                    if (FkClieTipo > 0)
                    {
                        if (FkClieTipo == 1)
                        {
                            lstReturn = lstReturn.Where(p => p.fk_cliente_cliente_natural.Equals(FkClie)).ToList();
                        }
                        else
                        {
                            lstReturn = lstReturn.Where(p => p.fk_cliente_cliente_juridico.Equals(FkClie)).ToList();
                        }
                    }
                    if (FkSubFami > 0)
                    {
                        lstVentaDetalle = await GetVentaDetalleByProdSubFalimia(FkSubFami);
                        if (lstVentaDetalle != null)
                        {
                            for (int i = 0; i < lstVentaDetalle.Count; i++)
                            {
                                foreach (var OneCompVent in lstReturn)
                                {
                                    if (lstVentaDetalle[i].fk_venta == OneCompVent.fk_venta)
                                    {
                                        lstVentaDetalle[i].str_f_emision = OneCompVent.f_emision;
                                        lstVentaDetalle[i].descripcion_comprobante_tipo = OneCompVent.descripcion_comprobante_tipo;
                                        lstVentaDetalle[i].nro_comprobante = OneCompVent.nro_comprobante;
                                    }
                                }
                            }
                        }
                    }
                }
                ViewBag.FkSubFamilia = FkSubFami;
                ViewBag.DetalleVenta = lstVentaDetalle;
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstReturn);
        }

        public async Task<ActionResult> ListDataReporteCompra(string FechInic, string FechFin, int FkProv, int FkMarc, int FkSubFami)
        {
            ComprobanteCompraController CtrlCompComp = new ComprobanteCompraController();
            List<ComprobanteCompra> lstEntidad = null;
            List<CompraDetalle> lstCompraDetalle = null;
            try
            {
                lstEntidad = await CtrlCompComp.GetComprobanteCompraAll();
                if (lstEntidad != null)
                {
                    if (Convert.ToDateTime(FechInic) <= Convert.ToDateTime(FechFin))
                    {
                        lstEntidad = lstEntidad.Where(p => Convert.ToDateTime(p.f_emision).Date >= Convert.ToDateTime(FechInic).Date && Convert.ToDateTime(p.f_emision).Date <= Convert.ToDateTime(FechFin).Date).ToList();
                    }
                    else
                    {
                        return Json(-1, JsonRequestBehavior.AllowGet);
                    }
                    if (FkProv > 0)
                    {
                        lstEntidad = lstEntidad.Where(p => p.fk_proveedor.Equals(FkProv)).ToList();
                    }
                    if (FkSubFami > 0 || FkMarc > 0)
                    {
                        if (FkSubFami > 0 && FkMarc > 0)
                        {
                            lstCompraDetalle = await GetCompraDetalleByProdSubFalimia(FkSubFami);
                            if (lstCompraDetalle != null)
                            {
                                lstCompraDetalle = lstCompraDetalle.Where(P => P.fk_producto_marca.Equals(FkMarc)).ToList();
                                if (lstCompraDetalle.Count > 0)
                                {
                                    for (int i = 0; i < lstCompraDetalle.Count; i++)
                                    {
                                        foreach (var OneCompVent in lstEntidad)
                                        {
                                            if (lstCompraDetalle[i].fk_compra == OneCompVent.fk_compra)
                                            {
                                                lstCompraDetalle[i].f_compra = OneCompVent.f_emision;
                                                lstCompraDetalle[i].descripcion_comprobante_tipo = OneCompVent.descripcion_comprobante_tipo;
                                                lstCompraDetalle[i].nro_comprobante = OneCompVent.nro_comprobante;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (FkSubFami > 0)
                        {
                            lstCompraDetalle = await GetCompraDetalleByProdSubFalimia(FkSubFami);
                            if (lstCompraDetalle != null)
                            {
                                for (int i = 0; i < lstCompraDetalle.Count; i++)
                                {
                                    foreach (var OneCompVent in lstEntidad)
                                    {
                                        if (lstCompraDetalle[i].fk_compra == OneCompVent.fk_compra)
                                        {
                                            lstCompraDetalle[i].f_compra = OneCompVent.f_emision;
                                            lstCompraDetalle[i].descripcion_comprobante_tipo = OneCompVent.descripcion_comprobante_tipo;
                                            lstCompraDetalle[i].nro_comprobante = OneCompVent.nro_comprobante;
                                        }
                                    }
                                }
                            }
                        }
                        else if (FkMarc > 0)
                        {
                            lstCompraDetalle = await GetCompraDetalleByProdMarca(FkMarc);
                            if (lstCompraDetalle != null)
                            {
                                for (int i = 0; i < lstCompraDetalle.Count; i++)
                                {
                                    foreach (var OneCompVent in lstEntidad)
                                    {
                                        if (lstCompraDetalle[i].fk_compra == OneCompVent.fk_compra)
                                        {
                                            lstCompraDetalle[i].f_compra = OneCompVent.f_emision;
                                            lstCompraDetalle[i].descripcion_comprobante_tipo = OneCompVent.descripcion_comprobante_tipo;
                                            lstCompraDetalle[i].nro_comprobante = OneCompVent.nro_comprobante;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ViewBag.FkSubFamilia = FkSubFami;
                ViewBag.FkMarca = FkMarc;
                ViewBag.DetalleCompra = lstCompraDetalle;
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ListaSubFamilia(string CallBy)
        {
            List<ProductoSubFamilia> lstEntidad = null;
            lstEntidad = await GetSubFamilias();

            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<List<ProductoSubFamilia>> GetSubFamilias()
        {
            List<ProductoSubFamilia> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ProductoSubFamilia/all");
                var model = response.Content.ReadAsAsync<List<ProductoSubFamilia>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ProductoSubFamilia>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> ListaMarca(string CallBy)
        {
            List<ProductoMarca> lstEntidad = null;
            lstEntidad = await GetMarcas();

            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<List<ProductoMarca>> GetMarcas()
        {
            List<ProductoMarca> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ProductoMarca/all");
                var model = response.Content.ReadAsAsync<List<ProductoMarca>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ProductoMarca>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<VentaDetalle>> GetVentaDetalleByProdSubFalimia(int FkSubFami)
        {
            List<VentaDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                VentaDetalle parametro = new VentaDetalle { fk_producto_subfamilia = FkSubFami };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaDetalle/buscarByFkProdSubFamilia", parametro);

                var model = response.Content.ReadAsAsync<List<VentaDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<VentaDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<CompraDetalle>> GetCompraDetalleByProdSubFalimia(int FkSubFami)
        {
            List<CompraDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                CompraDetalle parametro = new CompraDetalle { fk_producto_subfamilia = FkSubFami };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CompraDetalle/getByFkSubFamilia", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CompraDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<CompraDetalle>> GetCompraDetalleByProdMarca(int FkMarc)
        {
            List<CompraDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                CompraDetalle parametro = new CompraDetalle { fk_producto_marca = FkMarc };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CompraDetalle/getByFkMarca", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CompraDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> ReporteExistencias()
        {
            string msgReturn = "";
            AlmacenController CtrlAlma = new AlmacenController();
            List<Almacen> lstAlmacen = null;
            try
            {
                lstAlmacen = await CtrlAlma.GetAlmacenAll();
                if (lstAlmacen != null)
                {
                    Almacen oneAlmacen = new Almacen
                    {
                        id_almacen = 0,
                        nombre = ""
                    };
                    lstAlmacen.Add(oneAlmacen);
                    lstAlmacen = lstAlmacen.OrderBy(p => p.nombre).ToList();
                }
                ViewBag.Almacen = lstAlmacen;
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> ListDataReporteExistencia(int IdAlma)
        {
            string msgReturn = "";
            AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
            List<AlmacenStock> lstAlmacenStock = null;
            Almacen almacen = null;
            try
            {
                almacen = await CtrlAlmaStoc.GetAlmacenId(IdAlma);
                lstAlmacenStock = await CtrlAlmaStoc.GetAlmacenStockAll();
                if (lstAlmacenStock != null)
                {
                    lstAlmacenStock = lstAlmacenStock.Where(i => i.fk_almacen.Equals(IdAlma) && !i.estado.Equals("0")).ToList();
                }
                ViewBag.Almacen = almacen;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstAlmacenStock);
        }

        public ActionResult ReporteKardex()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }

        public async Task<ActionResult> ListDataReporteKardex(int FkProd)
        {
            string msgReturn = "";
            AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
            List<Movimiento> lstMovimiento = null;
            List<Movimiento> lstReturnMoviInventario = null;
            List<Movimiento> lstMoviInventario = null;
            Movimiento oneMovimiento = null;
            ProductoErp producto = null;

            try
            {
                lstMovimiento = await CtrlAlmaStoc.GetAlmacenStock_KardexByProducto(FkProd);
                producto = await CtrlAlmaStoc.GetProductoId(FkProd);
                if (lstMovimiento != null)
                {
                    lstReturnMoviInventario = lstMovimiento.Where(i => !i.descripcion_movimiento_tipo.Equals("INVENTARIO")).ToList();
                    lstMoviInventario = lstMovimiento.Where(i => i.descripcion_movimiento_tipo.Equals("INVENTARIO")).ToList();
                    if (lstMoviInventario.Count > 0)
                    {
                        var groupMoviInventario = lstMoviInventario.GroupBy(i => new { i.fk_producto, i.nro_inventario }).Select(i =>
                          new
                          {
                              FechMovi = i.First().f_movimiento,
                              FechEmis = i.First().f_movimiento,
                              TipoCompIngr = "",
                              NumeCompIngr = "",
                              CantIngr = i.Sum(b => b.cantidad),
                              PrecIngr = i.First().precio_ingreso,
                              IngrSali = i.First().ingreso_salida,
                              DescMoviTipo = i.First().descripcion_movimiento_tipo
                          }).ToList();
                        foreach (var oneMoviInve in groupMoviInventario)
                        {
                            oneMovimiento = new Movimiento
                            {
                                f_movimiento = oneMoviInve.FechMovi,
                                f_emision_ingreso = Convert.ToDateTime(oneMoviInve.FechEmis),
                                tipo_comprobante_ingreso = oneMoviInve.TipoCompIngr,
                                nro_comprobante_ingreso = oneMoviInve.NumeCompIngr,
                                cantidad = oneMoviInve.CantIngr,
                                precio_ingreso = oneMoviInve.PrecIngr,
                                ingreso_salida = oneMoviInve.IngrSali,
                                descripcion_movimiento_tipo = oneMoviInve.DescMoviTipo
                            };
                            lstReturnMoviInventario.Add(oneMovimiento);
                        }
                    }
                    lstReturnMoviInventario = lstReturnMoviInventario.OrderBy(p => Convert.ToDateTime(p.f_movimiento)).ToList();
                }
                ViewBag.Producto = producto;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return PartialView(lstMovimiento);
            return PartialView(lstReturnMoviInventario);
        }

        public ActionResult ReporteProdExistencia()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }

        public async Task<ActionResult> ListDataReporteProdExistencia(int FkProd)
        {
            string msgReturn = "";
            AlmacenController CtrlAlma = new AlmacenController();
            AlmacenStockController CtrlAlmaStoc = new AlmacenStockController();
            List<AlmacenStock> lstAlmacenStock = null;
            ProductoErp producto = null;
            List<Almacen> lstAlmacen = null;

            try
            {
                lstAlmacen = await CtrlAlma.GetAlmacenAll();
                lstAlmacenStock = await CtrlAlmaStoc.GetAlmacenStockProducto(FkProd);
                producto = await CtrlAlmaStoc.GetProductoId(FkProd);

                ViewBag.Almacen = lstAlmacen;
                ViewBag.Producto = producto;
                ViewBag.AlmacenStock = lstAlmacenStock;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public ActionResult ReporteTop10()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }

        public async Task<ActionResult> ListDataReporteTop10(string FechInic, string FechFin, int optTop10)
        {
            VentaDetalleController CtrlVentDeta = new VentaDetalleController();
            //List<ComprobanteVenta> lstEntidad = null;
            List<VentaDetalle> lstVentaDetalle = null;
            VentaDetalle venta_detalle = null;
            List<VentaDetalle> lstReturnVentaDetalle = new List<VentaDetalle>();
            ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
            List<ComprobanteVenta> lstComprobanteVenta = null;
            List<ComprobanteVenta> lstReturnComprobanteVenta = new List<ComprobanteVenta>();
            ComprobanteVenta comprobante_venta = null;
            ComprobanteCompraController CtrlCompComp = new ComprobanteCompraController();
            List<ComprobanteCompra> lstComprobanteCompra = null;
            ComprobanteCompra comprobante_compra = null;
            List<ComprobanteCompra> lstReturnComprobanteCompra = new List<ComprobanteCompra>();
            try
            {
                if (optTop10 == 1)
                {
                    lstVentaDetalle = await CtrlVentDeta.GetVentaDetalleAll();
                    if (lstVentaDetalle != null)
                    {
                        if (Convert.ToDateTime(FechInic) <= Convert.ToDateTime(FechFin))
                        {
                            lstVentaDetalle = lstVentaDetalle.Where(p => Convert.ToDateTime(p.f_emision).Date >= Convert.ToDateTime(FechInic).Date && Convert.ToDateTime(p.f_emision).Date <= Convert.ToDateTime(FechFin).Date).ToList();
                            var groupVentDeta = lstVentaDetalle.GroupBy(i => i.fk_proyecto).Select(i =>
                              new
                              {
                                  CodiSku = i.First().codigo_sku,
                                  NombProd = i.First().nom_producto,
                                  DescSubFami = i.First().descripcion_producto_subfamilia,
                                  Cant = i.Sum(b => b.cantidad),
                                  TotaBrut = i.Sum(b => b.cantidad * b.precio)
                              }).ToList();
                            for (int i = 0; i < groupVentDeta.Count; i++)
                            {
                                venta_detalle = new VentaDetalle
                                {
                                    codigo_sku = groupVentDeta[i].CodiSku,
                                    nom_producto = groupVentDeta[i].NombProd,
                                    descripcion_producto_subfamilia = groupVentDeta[i].DescSubFami,
                                    cantidad = groupVentDeta[i].Cant,
                                    total_bruto = groupVentDeta[i].TotaBrut,
                                };
                                lstReturnVentaDetalle.Add(venta_detalle);
                            }
                            lstReturnVentaDetalle = lstReturnVentaDetalle.OrderByDescending(p => p.total_bruto).Take(10).ToList();
                        }
                        else
                        {
                            return Json(-1, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (optTop10 == 2)
                {
                    lstComprobanteVenta = await CtrlCompVent.GetComprobanteVentaAll();
                    if (lstComprobanteVenta != null)
                    {
                        if (Convert.ToDateTime(FechInic) <= Convert.ToDateTime(FechFin))
                        {
                            lstComprobanteVenta = lstComprobanteVenta.Where(p => Convert.ToDateTime(p.f_emision).Date >= Convert.ToDateTime(FechInic).Date && Convert.ToDateTime(p.f_emision).Date <= Convert.ToDateTime(FechFin).Date).ToList();
                            var groupCompVenta = lstComprobanteVenta.GroupBy(i => i.fk_empresa).Select(i =>
                              new
                              {
                                  FkClie = i.First().id_cliente,
                                  FkClieNatu = i.First().id_cliente_natural,
                                  FkClieJuri = i.First().id_cliente_juridico,
                                  DniClie = i.First().dni_cliente_natural,
                                  NombClie = i.First().nombre_cliente_natural,
                                  ApelPateClie = i.First().apellido_paterno_cliente_natural,
                                  ApelMateClie = i.First().apellido_materno_cliente_natural,
                                  RucClie = i.First().ruc_cliente_natural,
                                  RazoSoci = i.First().razon_social,
                                  TotaNeto = i.Sum(b => b.total_neto),
                                  TotaIgv = i.Sum(b => b.total_igv),
                                  TotaBrut = i.Sum(b => b.total_bruto)
                              }).ToList();
                            for (int i = 0; i < groupCompVenta.Count; i++)
                            {
                                comprobante_venta = new ComprobanteVenta
                                {
                                    id_cliente = groupCompVenta[i].FkClie,
                                    id_cliente_natural = groupCompVenta[i].FkClieNatu,
                                    id_cliente_juridico = groupCompVenta[i].FkClieJuri,
                                    dni_cliente_natural = groupCompVenta[i].DniClie,
                                    nombre_cliente_natural = groupCompVenta[i].NombClie,
                                    apellido_paterno_cliente_natural = groupCompVenta[i].ApelPateClie,
                                    apellido_materno_cliente_natural = groupCompVenta[i].ApelMateClie,
                                    ruc_cliente_natural = groupCompVenta[i].RucClie,
                                    razon_social = groupCompVenta[i].RazoSoci,
                                    total_neto = groupCompVenta[i].TotaNeto,
                                    total_igv = groupCompVenta[i].TotaIgv,
                                    total_bruto = groupCompVenta[i].TotaBrut,
                                };
                                lstReturnComprobanteVenta.Add(comprobante_venta);
                            }
                            lstReturnComprobanteVenta = lstReturnComprobanteVenta.OrderByDescending(p => p.total_bruto).Take(10).ToList();
                        }
                        else
                        {
                            return Json(-1, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (optTop10 == 3)
                {
                    lstComprobanteCompra = await CtrlCompComp.GetComprobanteCompraAll();
                    if (lstComprobanteCompra != null)
                    {
                        if (Convert.ToDateTime(FechInic) <= Convert.ToDateTime(FechFin))
                        {
                            lstComprobanteCompra = lstComprobanteCompra.Where(p => Convert.ToDateTime(p.f_emision).Date >= Convert.ToDateTime(FechInic).Date && Convert.ToDateTime(p.f_emision).Date <= Convert.ToDateTime(FechFin).Date).ToList();
                            var groupCompVenta = lstComprobanteCompra.GroupBy(i => i.fk_proveedor).Select(i =>
                              new
                              {
                                  RucClie = i.First().ruc,
                                  RazoSoci = i.First().razon_social,
                                  TotaNeto = i.Sum(b => b.total_neto),
                                  TotaIgv = i.Sum(b => b.total_igv),
                                  TotaBrut = i.Sum(b => b.total_bruto)
                              }).ToList();
                            for (int i = 0; i < groupCompVenta.Count; i++)
                            {
                                comprobante_compra = new ComprobanteCompra
                                {
                                    ruc = groupCompVenta[i].RucClie,
                                    razon_social = groupCompVenta[i].RazoSoci,
                                    total_neto = groupCompVenta[i].TotaNeto,
                                    total_igv = groupCompVenta[i].TotaIgv,
                                    total_bruto = groupCompVenta[i].TotaBrut,
                                };
                                lstReturnComprobanteCompra.Add(comprobante_compra);
                            }
                            lstReturnComprobanteCompra = lstReturnComprobanteCompra.OrderByDescending(p => p.total_bruto).Take(10).ToList();
                        }
                        else
                        {
                            return Json(-1, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                ViewBag.OpcionTop10 = optTop10;
                ViewBag.VentaDetalle = lstReturnVentaDetalle;
                ViewBag.ComprobanteVenta = lstReturnComprobanteVenta;
                ViewBag.ComprobanteCompra = lstReturnComprobanteCompra;
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        public ActionResult ReporteCuentasPorPagar()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }

        public async Task<ActionResult> ListDataReporteCuentasPorPagar(int FkProv)
        {
            List<CuentasPorPagar> lstEntidad = null;
            List<CuentasPorPagar> lstReturnCuentasPorPagar = new List<CuentasPorPagar>();
            CuentasPorPagar cuentas_por_pagar = null;
            try
            {
                lstEntidad = await GetCuentasPorPagarPagos();
                if (lstEntidad != null)
                {
                    var newListGroup = lstEntidad.GroupBy(p => p.fk_comprobante_compra).Select(i =>
                     new
                     {
                         IdProv = i.First().id_proveedor,
                         FechEmis = i.First().f_emision,
                         DescTipoDocu = i.First().descripcion_comprobante_tipo,
                         NroComp = i.First().nro_comprobante,
                         ruc = i.First().ruc,
                         RazoSoci = i.First().razon_social,
                         TotaBrut = i.First().total_bruto,
                         MontPago = i.Sum(b => b.monto_pago)
                     }).ToList();
                    for (int i = 0; i < newListGroup.Count; i++)
                    {
                        cuentas_por_pagar = new CuentasPorPagar
                        {
                            id_proveedor = newListGroup[i].IdProv,
                            f_emision = newListGroup[i].FechEmis,
                            descripcion_comprobante_tipo = newListGroup[i].DescTipoDocu,
                            nro_comprobante = newListGroup[i].NroComp,
                            ruc = newListGroup[i].ruc,
                            razon_social = newListGroup[i].RazoSoci,
                            total_bruto = newListGroup[i].TotaBrut,
                            monto_pago = newListGroup[i].MontPago
                        };
                        lstReturnCuentasPorPagar.Add(cuentas_por_pagar);
                    }
                    if (FkProv > 0)
                    {
                        lstReturnCuentasPorPagar = lstReturnCuentasPorPagar.Where(p => p.id_proveedor.Equals(FkProv)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstReturnCuentasPorPagar);
        }

        public ActionResult ReporteCuentasPorCobrar()
        {
            string msgReturn = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            //return View(lstEntidad);
        }

        public async Task<ActionResult> ListDataReporteCuentasPorCobrar(int FkClieTipo, int FkClie)
        {
            List<CuentasPorCobrar> lstEntidad = null;
            List<CuentasPorCobrar> lstReturnCuentasPorCobrar = new List<CuentasPorCobrar>();
            CuentasPorCobrar cuentas_por_cobrar = null;
            try
            {
                lstEntidad = await GetCuentasPorCobrarPagos();
                if (lstEntidad != null)
                {
                    var newListGroup = lstEntidad.GroupBy(p => p.id_comprobante_venta).Select(i =>
                     new
                     {
                         IdClieTipo = i.First().id_cliente_tipo,
                         FkClieNatu = i.First().fk_cliente_cliente_natural,
                         FkClieJuri = i.First().fk_cliente_cliente_juridico,
                         FechEmis = i.First().f_emision,
                         DescTipoDocu = i.First().descripcion_comprobante_tipo,
                         NroComp = i.First().nro_comprobante,
                         DniClie = i.First().dni_cliente_natural,
                         NombClie = i.First().nombre_cliente_natural,
                         ApelPateClie = i.First().apellido_paterno_cliente_natural,
                         ApelMateClie = i.First().apellido_materno_cliente_natural,
                         RucClie = i.First().ruc_empresa_cliente_juridico,
                         RazoSoci = i.First().razon_social,
                         TotaBrut = i.First().monto,
                         MontPago = i.Sum(b => b.monto_pago)
                     }).ToList();
                    for (int i = 0; i < newListGroup.Count; i++)
                    {
                        cuentas_por_cobrar = new CuentasPorCobrar
                        {
                            id_cliente_tipo = newListGroup[i].IdClieTipo,
                            fk_cliente_natural = newListGroup[i].FkClieNatu,
                            fk_cliente_juridico = newListGroup[i].FkClieJuri,
                            f_emision = newListGroup[i].FechEmis,
                            descripcion_comprobante_tipo = newListGroup[i].DescTipoDocu,
                            nro_comprobante = newListGroup[i].NroComp,
                            dni_cliente_natural = newListGroup[i].DniClie,
                            nombre_cliente_natural = newListGroup[i].NombClie,
                            apellido_paterno_cliente_natural = newListGroup[i].ApelPateClie,
                            apellido_materno_cliente_natural = newListGroup[i].ApelMateClie,
                            ruc_empresa_cliente_juridico = newListGroup[i].RucClie,
                            razon_social = newListGroup[i].RazoSoci,
                            monto = newListGroup[i].TotaBrut,
                            monto_pago = newListGroup[i].MontPago
                        };
                        lstReturnCuentasPorCobrar.Add(cuentas_por_cobrar);
                    }
                    if (FkClieTipo > 0)
                    {
                        if (FkClieTipo == 1)
                        {
                            lstReturnCuentasPorCobrar = lstReturnCuentasPorCobrar.Where(p => p.fk_cliente_natural.Equals(FkClie)).ToList();
                        }
                        else
                        {
                            lstReturnCuentasPorCobrar = lstReturnCuentasPorCobrar.Where(p => p.fk_cliente_juridico.Equals(FkClie)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstReturnCuentasPorCobrar);
        }

        public async Task<List<CuentasPorPagar>> GetCuentasPorPagarPagos()
        {
            List<CuentasPorPagar> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("CuentasPorPagar/CuentasPorPagarPagos");
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
        public async Task<List<CuentasPorCobrar>> GetCuentasPorCobrarPagos()
        {
            List<CuentasPorCobrar> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("CuentasPorCobrar/CuentasPorCobrarPagos");
                var model = response.Content.ReadAsAsync<List<CuentasPorCobrar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.CuentasPorCobrar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> ReporteInventario()
        {
            string msgReturn = "";
            List<Movimiento> lstMovimiento = null;
            Movimiento one_inventario = null;
            List<Movimiento> lstReturnMovimiento = new List<Movimiento>();
            try
            {
                lstMovimiento = await GetMovimientoInventario();
                if (lstMovimiento != null)
                {
                    var newLstGroup = lstMovimiento.GroupBy(p => p.nro_inventario).Select(i =>
                     new
                     {
                         NroInve = i.First().nro_inventario,
                         FechInic = i.Min(b => Convert.ToDateTime(b.f_movimiento)),
                         FechFin = i.Max(b => Convert.ToDateTime(b.f_movimiento))
                     }).ToList();
                    for (int i = 0; i < newLstGroup.Count; i++)
                    {
                        one_inventario = new Movimiento
                        {
                            nro_inventario = Convert.ToInt32(newLstGroup[i].NroInve),
                            f_inicio = newLstGroup[i].FechInic.ToString("dd/MM/yyyy"),
                            f_fin = newLstGroup[i].FechFin.ToString("dd/MM/yyyy")
                        };
                        lstReturnMovimiento.Add(one_inventario);
                    }
                    lstReturnMovimiento = lstReturnMovimiento.OrderByDescending(p => p.f_inicio).ToList();
                }
                return View(lstReturnMovimiento);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<List<Movimiento>> GetMovimientoInventario()
        {
            List<Movimiento> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Movimiento/GetInventarios");
                var model = response.Content.ReadAsAsync<List<Movimiento>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Movimiento>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        //[HttpPost]
        public async Task<ActionResult> ViewDetalleInventario(int NroInve)
        {
            List<Movimiento> lstMovimiento = null;
            Movimiento one_inventario = null;
            List<Movimiento> lstReturnMovimiento = new List<Movimiento>();
            try
            {
                lstMovimiento = await GetMovimientoInventario();
                if (lstMovimiento != null)
                {
                    //lstMovimiento = lstMovimiento.Where(p => p.nro_inventario.Equals(NroInve)).ToList();
                    var newLstGroup = lstMovimiento.GroupBy(p => p.fk_producto).Select(i =>
                     new
                     {
                         CodiProd = i.First().cod_producto,
                         NombProd = i.First().nom_producto,
                         MarcProd = i.First().descripcion_marca,
                         SubFamiProd = i.First().descripcion_producto_subfamilia,
                         Cant = i.Sum(b => b.cantidad),
                         PrecCost = i.First().precio_costo
                     }).ToList();
                    for (int i = 0; i < newLstGroup.Count; i++)
                    {
                        one_inventario = new Movimiento
                        {
                            cod_producto = newLstGroup[i].CodiProd,
                            nom_producto = newLstGroup[i].NombProd,
                            descripcion_marca = newLstGroup[i].MarcProd,
                            descripcion_producto_subfamilia = newLstGroup[i].SubFamiProd,
                            cantidad = Convert.ToDecimal(newLstGroup[i].Cant),
                            precio_costo = Convert.ToDecimal(newLstGroup[i].PrecCost)
                        };
                        lstReturnMovimiento.Add(one_inventario);
                    }
                    lstReturnMovimiento = lstReturnMovimiento.OrderBy(p => p.nom_producto).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return PartialView(lstReturnMovimiento);
        }
    }
}