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
    public class OrdenCompraDetalleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<int> InsertOrderCompraDetalle(OrdenCompraDetalle orden_compra_detalle)
        {
            int NewIdOrdeComp = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("OrdenCompraDetalle/agregar", orden_compra_detalle);

                var rptaReturn = response.Content.ReadAsAsync<string>();
                if (rptaReturn != null && rptaReturn.Result.ToString() != "0")
                {
                    NewIdOrdeComp = Convert.ToInt32(rptaReturn.Result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return NewIdOrdeComp;
        }

        public async Task<int> UpdateOrderCompraDetalle(OrdenCompraDetalle orden_compra_detalle)
        {
            int rptaReturn = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("OrdenCompraDetalle/modificar", orden_compra_detalle);

                var rptaUpdate = response.Content.ReadAsAsync<string>();
                if (rptaUpdate != null && rptaUpdate.Result.ToString() != "0")
                {
                    rptaReturn = Convert.ToInt32(rptaUpdate.Result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return rptaReturn;
        }

        public async Task<int> DeleteOrderCompraDetalle(int IdOrdeCompDeta)
        {
            int RptaReturn = 0;
            try
            {
                var client = new HttpClient();
                OrdenCompraDetalle parametro = new OrdenCompraDetalle { id_orden_compra_detalle = IdOrdeCompDeta };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("OrdenCompraDetalle/eliminar", parametro);

                var rptaReturn = response.Content.ReadAsAsync<string>();
                if (rptaReturn != null && rptaReturn.Result.ToString() != "0")
                {
                    RptaReturn = Convert.ToInt32(rptaReturn.Result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return RptaReturn;
        }

        public async Task<List<OrdenCompraDetalle>> GetOrderCompraDetalleByFkOrdenCompra(int FkOrdeComp)
        {
            List<OrdenCompraDetalle> lstEntidad = null;
            try
            {
                OrdenCompraDetalle parametro = new OrdenCompraDetalle { fk_orden_compra = FkOrdeComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("OrdenCompraDetalle/getByFkOrdenCompra", parametro);

                var model = response.Content.ReadAsAsync<List<OrdenCompraDetalle>>();
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
    }
}