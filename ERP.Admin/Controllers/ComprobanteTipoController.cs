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
    public class ComprobanteTipoController : Controller
    {
        public async Task<List<ComprobanteTipo>> GetComprobanteTipoAll()
        {
            List<ComprobanteTipo> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ComprobanteTipo/all");
                var model = response.Content.ReadAsAsync<List<ComprobanteTipo>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.ComprobanteTipo>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: ComprobanteTipo
        public ActionResult Index()
        {
            return View();
        }
    }
}