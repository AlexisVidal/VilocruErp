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
    public class NotaCreditoCompraDetalleController : Controller
    {
        public async Task<NotaCreditoCompraDetalle> InsertNotaCreditoCompraDetalle(NotaCreditoCompraDetalle nota_credito_compra_detalle)
        {
            NotaCreditoCompraDetalle entidad = null;
            try
            {
                List<NotaCreditoCompraDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaCreditoCompraDetalle/agregar", nota_credito_compra_detalle);

                var model = response.Content.ReadAsAsync<List<NotaCreditoCompraDetalle>>();
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

        public async Task<List<NotaCreditoCompraDetalle>> GetNotaCreditoCompraDetalleByNotaCredito(int FkNotaCredComp)
        {
            List<NotaCreditoCompraDetalle> lstEntidad = null;
            try
            {
                NotaCreditoCompraDetalle parametro = new NotaCreditoCompraDetalle { fk_nota_credito_compra = FkNotaCredComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("NotaCreditoCompraDetalle/ByNotaCreditoCompra", parametro);

                var model = response.Content.ReadAsAsync<List<NotaCreditoCompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<NotaCreditoCompraDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: NotaCreditoCompraDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}