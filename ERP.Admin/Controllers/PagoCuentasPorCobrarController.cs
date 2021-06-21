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
    public class PagoCuentasPorCobrarController : Controller
    {
        public async Task<PagoCuentasPorCobrar> InsertPagoCuentasPorCobrar(PagoCuentasPorCobrar pago_cuentas_por_cobrar)
        {
            PagoCuentasPorCobrar entidad = null;
            try
            {
                List<PagoCuentasPorCobrar> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("PagoCuentasPorCobrar/agregar", pago_cuentas_por_cobrar);

                var model = response.Content.ReadAsAsync<List<PagoCuentasPorCobrar>>();
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

        public async Task<List<PagoCuentasPorCobrar>> GetPagoCuentasPorCobrarByCaja(int FkCaja)
        {
            List<PagoCuentasPorCobrar> lstEntidad = null;
            try
            {
                PagoCuentasPorCobrar parametro = new PagoCuentasPorCobrar { fk_caja = FkCaja };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("PagoCuentasPorCobrar/ByCaja", parametro);

                var model = response.Content.ReadAsAsync<List<PagoCuentasPorCobrar>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PagoCuentasPorCobrar>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: PagoCuentasPorCobrar
        public ActionResult Index()
        {
            return View();
        }
    }
}