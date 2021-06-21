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
    public class SolicitudTrasladoDetalleController : Controller
    {
        public async Task<SolicitudTrasladoDetalle> InsertSolicitudTrasladoDetalle(SolicitudTrasladoDetalle solicitud_traslado_detalle)
        {
            SolicitudTrasladoDetalle entidad = null;
            try
            {
                List<SolicitudTrasladoDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTrasladoDetalle/agregar", solicitud_traslado_detalle);

                var model = response.Content.ReadAsAsync<List<SolicitudTrasladoDetalle>>();
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

        public async Task<SolicitudTrasladoDetalle> UpdateSolicitudTrasladoDetalle(SolicitudTrasladoDetalle solicitud_traslado_detalle)
        {
            SolicitudTrasladoDetalle entidad = null;
            try
            {
                List<SolicitudTrasladoDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTrasladoDetalle/modificar", solicitud_traslado_detalle);

                var model = response.Content.ReadAsAsync<List<SolicitudTrasladoDetalle>>();
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

        public async Task<SolicitudTrasladoDetalle> DeleteSolicitudTrasladoDetalle(int IdSoliTrasDeta)
        {
            SolicitudTrasladoDetalle entidad = null;
            try
            {
                List<SolicitudTrasladoDetalle> lstEntidad = null;

                var client = new HttpClient();
                SolicitudTrasladoDetalle parametro = new SolicitudTrasladoDetalle { id_solicitud_traslado_detalle = IdSoliTrasDeta };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTrasladoDetalle/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<SolicitudTrasladoDetalle>>();
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

        public async Task<List<SolicitudTrasladoDetalle>> GetSolicitudTrasladoDetalleBySolicitudTraslado(int FkSoliTras)
        {
            List<SolicitudTrasladoDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                SolicitudTrasladoDetalle parametro = new SolicitudTrasladoDetalle { fk_solicitud_traslado = FkSoliTras };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("SolicitudTrasladoDetalle/buscarBySolicitudTraslado", parametro);

                var model = response.Content.ReadAsAsync<List<SolicitudTrasladoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.SolicitudTrasladoDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<JsonResult> GetJson_SolicitudTrasladoDetalle(int IdSoliTras)
        {
            List<SolicitudTrasladoDetalle> lstEntidad = null;
            string msgReturn = "";
            try
            {
                lstEntidad = await GetSolicitudTrasladoDetalleBySolicitudTraslado(IdSoliTras);
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