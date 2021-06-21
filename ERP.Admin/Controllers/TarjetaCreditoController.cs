using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ERP.Admin.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ERP.Admin.Controllers
{
    public class TarjetaCreditoController : Controller
    {
        public async Task<List<TarjetaCredito>> GetTarjetaCreditoAll()
        {
            List<TarjetaCredito> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("TarjetaCredito/all");
                var model = response.Content.ReadAsAsync<List<TarjetaCredito>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.TarjetaCredito>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: TarjetaCredito
        public ActionResult Index()
        {
            return View();
        }
    }
}