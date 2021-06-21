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
    public class ComprobanteTrasladoDetalleController : Controller
    {
        public async Task<ComprobanteTrasladoDetalle> InsertComprobanteTrasladoDetalle(ComprobanteTrasladoDetalle comprobante_traslado_detalle)
        {
            ComprobanteTrasladoDetalle entidad = null;
            try
            {
                List<ComprobanteTrasladoDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteTrasladoDetalle/agregar", comprobante_traslado_detalle);

                var model = response.Content.ReadAsAsync<List<ComprobanteTrasladoDetalle>>();
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

        public async Task<List<ComprobanteTrasladoDetalle>> GetComprobanteTrasladoDetalleByComprobanteTraslado(int FkCompTras)
        {
            List<ComprobanteTrasladoDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                ComprobanteTrasladoDetalle parametro = new ComprobanteTrasladoDetalle { fk_comprobante_traslado = FkCompTras };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ComprobanteTrasladoDetalle/buscarByComprobanteTraslado", parametro);

                var model = response.Content.ReadAsAsync<List<ComprobanteTrasladoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ComprobanteTrasladoDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: ComprobanteTrasladoDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}