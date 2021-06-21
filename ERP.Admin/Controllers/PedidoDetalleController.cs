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
    public class PedidoDetalleController : Controller
    {
        public async Task<PedidoDetalle> InsertPedidoDetalle(PedidoDetalle pedido_detalle)
        {
            PedidoDetalle entidad = null;
            List<PedidoDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("PedidoDetalle/agregar", pedido_detalle);

                var model = response.Content.ReadAsAsync<List<PedidoDetalle>>();
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

        public async Task<List<PedidoDetalle>> Get_PedidoDetalleByFkPedido(int FkPedi)
        {
            List<PedidoDetalle> lstEntidad = null;
            try
            {
                PedidoDetalle parametro = new PedidoDetalle { fk_pedido = FkPedi };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("venta/PedidoDetalle_Venta", parametro);

                var model = response.Content.ReadAsAsync<List<PedidoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: PedidoDetalle
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetJson_PedidoDetalleByFkPedido(int FkPedi)
        {
            List<PedidoDetalle> lstEntidad = null;
            string msgReturn = "";
            try
            {
                lstEntidad = await Get_PedidoDetalleByFkPedido(FkPedi);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(lstEntidad);
        }
    }
}