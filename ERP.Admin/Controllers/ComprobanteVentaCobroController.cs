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

namespace ERP.Admin.Controllers
{
    public class ComprobanteVentaCobroController : Controller
    {
        public async Task<ComprobanteVentaCobro> InsertComprobanteVentaCobro(ComprobanteVentaCobro comprobante_venta_cobro)
        {
            ComprobanteVentaCobro entidad = null;
            try
            {
                List<ComprobanteVentaCobro> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteVentaCobro/agregar", comprobante_venta_cobro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVentaCobro>>();
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

        public async Task<ComprobanteVentaCobro> GetComprobanteVentaCobroById(int IdCompVentCobr)
        {
            ComprobanteVentaCobro entidad = null;
            try
            {
                List<ComprobanteVentaCobro> lstEntidad = null;

                ComprobanteVentaCobro parametro = new ComprobanteVentaCobro { id_comprobante_venta_cobro = IdCompVentCobr };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVentaCobro/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVentaCobro>>();
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

        public async Task<List<ComprobanteVentaCobro>> GetComprobanteVentaCobroByMedioPago(int FkMediPago)
        {
            List<ComprobanteVentaCobro> lstEntidad = null;
            try
            {
                ComprobanteVentaCobro parametro = new ComprobanteVentaCobro { fk_medio_pago = FkMediPago };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("ComprobanteVentaCobro/ByMedioPago", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteVentaCobro>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<ComprobanteVentaCobro>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: ComprobanteVentaCobro
        public ActionResult Index()
        {
            return View();
        }
    }
}