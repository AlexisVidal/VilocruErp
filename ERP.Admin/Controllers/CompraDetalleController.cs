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
    public class CompraDetalleController : Controller
    {
        public async Task<CompraDetalle> InsertCompraDetalle(CompraDetalle compra_detalle)
        {
            CompraDetalle entidad = null;
            try
            {
                List<CompraDetalle> lstEntidad = null;

                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CompraDetalle/agregar", compra_detalle);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
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

        public async Task<List<CompraDetalle>> GetCompraDetalleByFkCompra(int FkComp)
        {
            List<CompraDetalle> lstEntidad = null;
            try
            {
                CompraDetalle parametro = new CompraDetalle { fk_compra = FkComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CompraDetalle/getByFkCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.CompraDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<JsonResult> GetJson_CompraDetalleByFkCompra(int FkComp)
        {
            List<CompraDetalle> lstEntidad = null;
            string msgReturn = "";
            try
            {
                lstEntidad = await GetCompraDetalleByFkCompra(FkComp);
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.Where(i => !i.estado.Equals("0")).ToList();
                }
                else
                {
                    msgReturn = "No se han encontrado detalles de la OC";
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los detalles de la OC";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(lstEntidad);
        }

        // GET: CompraDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}