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

namespace ERP.Admin.Controllers
{
    public class VentaDetalleController : Controller
    {
        public async Task<VentaDetalle> InsertVentaDetalle(VentaDetalle venta_detalle)
        {
            VentaDetalle entidad = null;
            try
            {
                List<VentaDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaDetalle/agregar", venta_detalle);

                var model = response.Content.ReadAsAsync<List<VentaDetalle>>();
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
        public async Task<VentaDetalle> InsertVentaDetalleTemp(VentaDetalle venta_detalle)
        {
            VentaDetalle entidad = null;
            try
            {
                List<VentaDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaDetalle/agregartemp", venta_detalle);

                var model = response.Content.ReadAsAsync<List<VentaDetalle>>();
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
        public async Task<List<VentaDetalle>> GetVentaDetalleAll()
        {
            List<VentaDetalle> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("VentaDetalle/all");
                var model = response.Content.ReadAsAsync<List<VentaDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.VentaDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public async Task<List<VentaDetalle>> GetVentaDetalle_ByFkVentaTemp(int FkVent)
        {
            List<VentaDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                VentaDetalle parametro = new VentaDetalle { fk_venta = FkVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaDetalle/buscarByFkVentaTemp", parametro);

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
        public async Task<List<VentaDetalle>> GetVentaDetalle_ByFkVenta(int FkVent)
        {
            List<VentaDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                VentaDetalle parametro = new VentaDetalle { fk_venta = FkVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaDetalle/buscarByFkVenta", parametro);

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

        public async Task<List<VentaDetalle>> GetVentaDetalle_Despacho(int FkVent)
        {
            List<VentaDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                VentaDetalle parametro = new VentaDetalle { fk_venta = FkVent };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaDetalle/GetDespacho", parametro);

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


        public async Task<DataSet> Get_DataDespacho(int FkVent)
        {
            var lstVentaDetalle = await GetVentaDetalle_Despacho(FkVent);
            if (lstVentaDetalle != null)
            {
                if (lstVentaDetalle.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("descripcion_comprobante", typeof(string));
                    dt.Columns.Add("nro_comprobante", typeof(string));
                    dt.Columns.Add("f_emision", typeof(DateTime));
                    dt.Columns.Add("nombre_cliente", typeof(string));
                    dt.Columns.Add("fk_venta", typeof(int));
                    dt.Columns.Add("fk_producto", typeof(int));
                    dt.Columns.Add("cantidad_venta_detalle", typeof(decimal));
                    dt.Columns.Add("cod_producto", typeof(string));
                    dt.Columns.Add("nom_producto", typeof(string));
                    dt.Columns.Add("codigo_sku", typeof(string));
                    dt.Columns.Add("descripcion_marca", typeof(string));
                    dt.Columns.Add("descripcion_producto_subfamilia", typeof(string));
                    dt.Columns.Add("cantidad_movimiento", typeof(decimal));
                    dt.Columns.Add("nombre_almacen", typeof(string));

                    foreach (var item in lstVentaDetalle)
                    {
                        DataRow row = dt.NewRow();
                        row["descripcion_comprobante"] = item.descripcion_comprobante;
                        row["nro_comprobante"] = item.nro_comprobante;
                        row["f_emision"] = item.f_emision;
                        row["nombre_cliente"] = item.nombre_cliente;
                        row["fk_venta"] = item.fk_venta;
                        row["fk_producto"] = item.fk_proyecto;
                        row["cantidad_venta_detalle"] = item.cantidad_venta_detalle;
                        row["cod_producto"] = item.cod_producto;
                        row["nom_producto"] = item.nom_producto;
                        row["codigo_sku"] = item.codigo_sku;
                        row["descripcion_marca"] = item.descripcion_marca;
                        row["descripcion_producto_subfamilia"] = item.descripcion_producto_subfamilia;
                        row["cantidad_movimiento"] = item.cantidad_movimiento;
                        row["nombre_almacen"] = item.nombre_almacen;
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

        // GET: VentaDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}