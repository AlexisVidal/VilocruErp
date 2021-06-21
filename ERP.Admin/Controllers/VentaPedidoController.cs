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
    public class VentaPedidoController : Controller
    {

        public async Task<VentaPedido> InsertVentaPedido(VentaPedido venta_pedido)
        {
            VentaPedido entidad = null;
            try
            {
                List<VentaPedido> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("VentaPedido/agregar", venta_pedido);

                var model = response.Content.ReadAsAsync<List<VentaPedido>>();
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

        // GET: VentaPedido
        public ActionResult Index()
        {
            return View();
        }
    }
}