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
    public class GuiaRemisionDetalleController : Controller
    {
        public async Task<List<GuiaRemisionDetalle>> GetGuiaRemisionDetalleByFkGuiaRemi(int FkGuiaRemi)
        {
            List<GuiaRemisionDetalle> lstEntidad = null;
            try
            {
                GuiaRemisionDetalle parametro = new GuiaRemisionDetalle { fk_guia_remision = FkGuiaRemi };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("GuiaRemisionDetalle/getByFkGuiaRemision", parametro);

                var model = response.Content.ReadAsAsync<List<GuiaRemisionDetalle>>();
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

        public async Task<GuiaRemisionDetalle> InsertGuiaRemisionDetalle(GuiaRemisionDetalle guia_remision_detalle)
        {
            GuiaRemisionDetalle entidad = null;
            try
            {
                List<GuiaRemisionDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaRemisionDetalle/agregar", guia_remision_detalle);

                var model = response.Content.ReadAsAsync<List<GuiaRemisionDetalle>>();
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

        public async Task<GuiaRemisionDetalle> UpdateGuiaRemisionDetalle(GuiaRemisionDetalle guia_remision_detalle)
        {
            GuiaRemisionDetalle entidad = null;
            try
            {
                List<GuiaRemisionDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaRemisionDetalle/modificar", guia_remision_detalle);

                var model = response.Content.ReadAsAsync<List<GuiaRemisionDetalle>>();
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

        public async Task<GuiaRemisionDetalle> DeleteGuiaRemisionDetalle(int IdGuiaRemiDeta)
        {
            GuiaRemisionDetalle entidad = null;
            try
            {
                List<GuiaRemisionDetalle> lstEntidad = null;

                var client = new HttpClient();
                GuiaRemisionDetalle parametro = new GuiaRemisionDetalle { id_guia_remision_detalle = IdGuiaRemiDeta };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("GuiaRemisionDetalle/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<GuiaRemisionDetalle>>();
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

        // GET: GuiaRemisionDetalle
        public ActionResult Index()
        {
            return View();
        }

        public async Task<List<GuiaRemisionDetalle>> Get_GuiaRemisionDetalleGroup_ListFkGR(List<string> arrIdGuiaRemi)
        {
            List<GuiaRemisionDetalle> lstEntidad = null;
            List<GuiaRemisionDetalle> lstReturn = new List<GuiaRemisionDetalle>();
            int flgExiste = 0;
            try
            {
                foreach (var oneFkGuiaRemi in arrIdGuiaRemi)
                {
                    lstEntidad = await GetGuiaRemisionDetalleByFkGuiaRemi(Convert.ToInt32(oneFkGuiaRemi));
                    if (lstEntidad != null)
                    {
                        foreach (var oneGuieRemiDeta in lstEntidad)
                        {
                            flgExiste = 0;
                            if (!oneGuieRemiDeta.estado.Equals("0"))
                            {
                                if (lstReturn.Count > 0)
                                {
                                    for (int i = 0; i < lstReturn.Count; i++)
                                    {
                                        if (lstReturn[i].fk_producto.Equals(oneGuieRemiDeta.fk_producto))
                                        {
                                            lstReturn[i].cantidad = lstReturn[i].cantidad + oneGuieRemiDeta.cantidad;
                                            flgExiste = 1;
                                            break;
                                        }
                                    }
                                    if (flgExiste == 0)
                                    {
                                        lstReturn.Add(oneGuieRemiDeta);
                                    }
                                }
                                else
                                {
                                    lstReturn.Add(oneGuieRemiDeta);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstReturn = new List<Models.GuiaRemisionDetalle>();
            }
            return lstReturn;
        }

        public async Task<JsonResult> GetJson_GuiaRemisionDetalleGroup_ListFkGR(List<string> arrIdGuiaRemi)
        {
            List<GuiaRemisionDetalle> lstReturn = new List<GuiaRemisionDetalle>();
            string msgReturn = "";
            try
            {
                lstReturn = await Get_GuiaRemisionDetalleGroup_ListFkGR(arrIdGuiaRemi);
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar leer los registros";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(lstReturn);
        }
    }
}