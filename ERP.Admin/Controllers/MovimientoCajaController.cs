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
    [SessionAuthorize]
    public class MovimientoCajaController : Controller
    {
        public async Task<MovimientoCaja> InsertMovimientoCaja(MovimientoCaja movimiento_caja)
        {
            MovimientoCaja entidad = null;
            try
            {
                List<MovimientoCaja> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("MovimientoCaja/agregar", movimiento_caja);

                var model = response.Content.ReadAsAsync<List<MovimientoCaja>>();
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

        public async Task<ComprobanteFlete> InsertComprobanteFlete(ComprobanteFlete comprobante_flete)
        {
            ComprobanteFlete entidad = null;
            try
            {
                List<ComprobanteFlete> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("MovimientoCaja/InsertComprobanteFlete", comprobante_flete);

                var model = response.Content.ReadAsAsync<List<ComprobanteFlete>>();
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

        public async Task<MovimientoCaja> GetMovimientoCajaById(int IdMoviCaja)
        {
            MovimientoCaja entidad = null;
            try
            {
                List<MovimientoCaja> lstEntidad = null;

                var client = new HttpClient();
                MovimientoCaja parametro = new MovimientoCaja { id_movimiento_caja = IdMoviCaja };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("MovimientoCaja/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<MovimientoCaja>>();
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

        public async Task<List<MovimientoCaja>> GetMovimientoCajaByCaja(int FkCaja)
        {
            List<MovimientoCaja> lstEntidad = null;
            try
            {
                MovimientoCaja parametro = new MovimientoCaja { fk_caja = FkCaja };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("MovimientoCaja/ByCaja", parametro);

                var model = response.Content.ReadAsAsync<List<MovimientoCaja>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<MovimientoCaja>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<List<MovimientoCaja>> GetPendienteVincular()
        {
            List<MovimientoCaja> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("MovimientoCaja/GetPendienteVincular");
                var model = response.Content.ReadAsAsync<List<MovimientoCaja>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.MovimientoCaja>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: MovimientoCaja
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> IndexFletes()
        {
            List<MovimientoCaja> lstMoviCajaPendientesVincular = null;
            try
            {
                lstMoviCajaPendientesVincular = await GetPendienteVincular();
            }
            catch (Exception ex)
            {
                return null;
            }
            return View(lstMoviCajaPendientesVincular);
        }

        public async Task<ActionResult> SaveComprobanteFlete(int FkCompComp, List<string> ArraIdMoviCaja)
        {
            ComprobanteFlete CompFleteReturn = null;
            ComprobanteFlete comprobante_flete = null;
            try
            {
                if (ArraIdMoviCaja != null)
                {
                    foreach(var oneIdMoviCaja in ArraIdMoviCaja)
                    {
                        comprobante_flete = new ComprobanteFlete
                        {
                            fk_comprobante_compra = FkCompComp,
                            fk_movimiento_caja = Convert.ToInt32(oneIdMoviCaja)
                        };
                        CompFleteReturn = await InsertComprobanteFlete(comprobante_flete);
                        if (CompFleteReturn == null)
                        {
                            return Json(-1, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}