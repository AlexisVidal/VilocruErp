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
    public class GuiaComprobanteController : Controller
    {
        public async Task<GuiaComprobante> InsertGuiaComprobante(GuiaComprobante guia_comprobante)
        {
            GuiaComprobante entidad = null;
            try
            {
                List<GuiaComprobante> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaComprobante/agregar", guia_comprobante);

                var model = response.Content.ReadAsAsync<List<GuiaComprobante>>();
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

        public async Task<GuiaComprobante> UpdateGuiaComprobante(GuiaComprobante guia_comprobante)
        {
            GuiaComprobante entidad = null;
            try
            {
                List<GuiaComprobante> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaComprobante/modificar", guia_comprobante);

                var model = response.Content.ReadAsAsync<List<GuiaComprobante>>();
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

        public async Task<GuiaComprobante> DeleteGuiaComprobante(int IdGuiaComp)
        {
            GuiaComprobante entidad = null;
            try
            {
                List<GuiaComprobante> lstEntidad = null;

                var client = new HttpClient();
                GuiaComprobante parametro = new GuiaComprobante { id_guia_comprobante = IdGuiaComp };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaComprobante/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<GuiaComprobante>>();
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

        public async Task<List<GuiaComprobante>> GetGuiaComprobanteAll()
        {
            List<GuiaComprobante> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("GuiaComprobante/all");
                var model = response.Content.ReadAsAsync<List<GuiaComprobante>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.GuiaComprobante>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: GuiaComprobante
        public ActionResult Index()
        {
            return View();
        }

        public async Task<List<GuiaComprobante>> Get_GuiaComprobanteByFkCompComp(int FkCompComp)
        {
            List<GuiaComprobante> lstEntidad = null;
            try
            {
                lstEntidad = await GetGuiaComprobanteAll();
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.Where(i=> i.fk_comprobante_compra.Equals(FkCompComp)).ToList();
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