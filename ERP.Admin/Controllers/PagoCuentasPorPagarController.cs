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
    public class PagoCuentasPorPagarController : Controller
    {
        public async Task<PagoCuentasPorPagar> InsertPagoCuentasPorPagar(PagoCuentasPorPagar pago_cuentas_por_pagar)
        {
            PagoCuentasPorPagar entidad = null;
            try
            {
                List<PagoCuentasPorPagar> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("PagoCuentasPorPagar/agregar", pago_cuentas_por_pagar);

                var model = response.Content.ReadAsAsync<List<PagoCuentasPorPagar>>();
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

        // GET: PagoCuentasPorPagar
        public ActionResult Index()
        {
            return View();
        }
    }
}