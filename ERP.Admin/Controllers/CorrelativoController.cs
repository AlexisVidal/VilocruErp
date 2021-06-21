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
    public class CorrelativoController : Controller
    {
        public async Task<Correlativo> GetCorrelativoByComprobanteTipo(int FkCompTipo)
        {
            Correlativo entidad = null;
            try
            {
                List<Correlativo> lstEntidad = null;

                var client = new HttpClient();
                Correlativo parametro = new Correlativo { fk_comprobante_tipo = FkCompTipo };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Correlativo/ByComprobanteTipo", parametro);

                var model = response.Content.ReadAsAsync<List<Correlativo>>();
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
        public async Task<List<Correlativo>> GetCorrelativoByComprobanteTipoList(int FkCompTipo)
        {
            //Correlativo entidad = null;
            List<Correlativo> lstEntidad = null;
            try
            {
                

                var client = new HttpClient();
                Correlativo parametro = new Correlativo { fk_comprobante_tipo = FkCompTipo };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Correlativo/ByComprobanteTipo", parametro);

                var model = response.Content.ReadAsAsync<List<Correlativo>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                   // entidad = lstEntidad[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        // GET: Correlativo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetJson_Correlativo_ByComprobanteTipo(int FkCompTipo)
        {
            Correlativo entidad = new Correlativo();
            int IntCorr = 0;
            string strCorr = "";
            string msgReturn = "";
            try
            {
                entidad = await GetCorrelativoByComprobanteTipo(FkCompTipo);
                //strCorr = entidad.nro_correlativo;
                //int pox = strCorr.IndexOf('-');
                //int intSub = strCorr.Length - pox -1;
                //string strSerie = strCorr.Substring(0, pox); ;
                //strCorr = strCorr.Substring(pox + 1, intSub);
                //IntCorr = Convert.ToInt32(strCorr) + 1;
                //entidad.new_correlativo = strSerie + "-" + IntCorr.ToString().PadLeft(intSub, '0');
                ////entidad.int_correlativo = IntCorr;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(entidad);
        }
        public async Task<JsonResult> GetJson_Correlativo_ByComprobanteTipo2(int FkCompTipo, string SiglCV)
        {
            List<Correlativo> lentidad = new List<Correlativo>();
            Correlativo entidad = new Correlativo();
            int IntCorr = 0;
            string strCorr = "";
            string msgReturn = "";
            try
            {
                lentidad = await GetCorrelativoByComprobanteTipoList(FkCompTipo);
                if (lentidad.Any())
                {
                    entidad = lentidad.Where(i => i.comprobante_modifica.Equals(SiglCV)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(entidad);
        }
    }
}