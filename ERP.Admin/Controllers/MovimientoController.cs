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
    public class MovimientoController : Controller
    {
        public async Task<Movimiento> InsertMovimiento(Movimiento movimiento)
        {
            Movimiento entidad = null;
            try
            {
                List<Movimiento> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Movimiento/agregar", movimiento);

                var model = response.Content.ReadAsAsync<List<Movimiento>>();
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

        public async Task<List<Movimiento>> Get_MovimientoByVenta(int FkVent)
        {
            List<Movimiento> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                Movimiento parametro = new Movimiento { fk_venta = FkVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Movimiento/GetByVenta", parametro);

                var model = response.Content.ReadAsAsync<List<Movimiento>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<Movimiento>> Get_MovimientoByProducto(int FkProd, string FechInve)
        {
            List<Movimiento> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                Movimiento parametro = new Movimiento { fk_producto = FkProd, f_movimiento = FechInve };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Movimiento/GetByProductoInventario", parametro);

                var model = response.Content.ReadAsAsync<List<Movimiento>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        // GET: Movimiento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InventarioFinal()
        {
            return View();
        }

        public async Task<ActionResult> ListDataInventarioFinal(string FechInveFinal)
        {
            string msgReturn = "";
            ComprobanteVentaController CtrlCompVent = new ComprobanteVentaController();
            Movimiento movimiento = null;
            Producto ProductoTemp = null;
            List<Producto> lstProducto = null;
            List<Movimiento> lstMovimiento = null;
            List<Producto> lstReturnProducto = new List<Producto>();
            decimal TotaIngr = 0;
            decimal TotaSali = 0;
            decimal StocActu = 0;
            decimal UltiPrec = 0;
            decimal PorcIgv = 0;
            decimal PrecCost = 0;
            string newDate = "";
            try
            {
                newDate = Convert.ToDateTime(FechInveFinal).ToString("yyyy-MM-dd");
                lstProducto = await CtrlCompVent.SelectProductoVenta();
                if (lstProducto != null)
                {
                    lstProducto = lstProducto.OrderBy(p=> p.id_producto).ToList();
                    foreach (var oneProd in lstProducto)
                    {
                        lstMovimiento = await Get_MovimientoByProducto(oneProd.id_producto, newDate);
                        
                        if (lstMovimiento != null)
                        {
                            //lstMovimiento = lstMovimiento.Where(i => Convert.ToDateTime(i.f_movimiento) < Convert.ToDateTime(FechInveFinal)).ToList();
                            movimiento = lstMovimiento.Where(p => p.ingreso_salida.Equals("INGRESO")).OrderByDescending(p => p.f_emision_salida).ToList()[0];

                            UltiPrec = movimiento.precio_ingreso;
                            PorcIgv = movimiento.porcentaje_igv_ingreso;
                            PrecCost = UltiPrec / (1 + PorcIgv / 100);
                            if (movimiento.fk_tipo_moneda == 2) {
                                PrecCost = PrecCost * movimiento.tipo_cambio;
                            }
                            TotaIngr = lstMovimiento.Where(i => i.ingreso_salida.Equals("INGRESO")).Sum(i => i.cantidad);
                            TotaSali = lstMovimiento.Where(i => i.ingreso_salida.Equals("SALIDA")).Sum(i => i.cantidad);

                            ProductoTemp = oneProd;
                            ProductoTemp.total_ingreso = TotaIngr;
                            ProductoTemp.total_salida = TotaSali;
                            ProductoTemp.precio_costo = PrecCost;
                            lstReturnProducto.Add(ProductoTemp);
                        }
                    }
                }
                ViewBag.FechaInventarioFinal = FechInveFinal;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstReturnProducto);
        }
    }
}