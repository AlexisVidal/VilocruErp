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
    public class NotaCreditoDetalleController : Controller
    {
        public async Task<NotaCreditoDetalle> InsertNotaCreditoDetalle(NotaCreditoDetalle nota_credito_detalle)
        {
            NotaCreditoDetalle entidad = null;
            try
            {
                List<NotaCreditoDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaCreditoDetalle/agregar", nota_credito_detalle);

                var model = response.Content.ReadAsAsync<List<NotaCreditoDetalle>>();
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

        public async Task<List<NotaCreditoDetalle>> GetNotaCreditoDetalleByNotaCredito(int FkNotaCred)
        {
            List<NotaCreditoDetalle> lstEntidad = null;
            try
            {
                NotaCreditoDetalle parametro = new NotaCreditoDetalle { fk_nota_credito = FkNotaCred };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("NotaCreditoDetalle/ByNotaCredito", parametro);

                var model = response.Content.ReadAsAsync<List<NotaCreditoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<NotaCreditoDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: NotaCreditoDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}