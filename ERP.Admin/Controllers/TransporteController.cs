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
    public class TransporteController : Controller
    {
        public async Task<Transporte> InsertTransporte(Transporte transporte)
        {
            Transporte entidad = null;
            try
            {
                List<Transporte> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Transporte/agregar", transporte);

                var model = response.Content.ReadAsAsync<List<Transporte>>();
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

        public async Task<Transporte> UpdateTransporte(Transporte transporte)
        {
            Transporte entidad = null;
            try
            {
                List<Transporte> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Transporte/modificar", transporte);

                var model = response.Content.ReadAsAsync<List<Transporte>>();
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

        public async Task<Transporte> DelteTransporte(int IdTran)
        {
            Transporte entidad = null;
            try
            {
                List<Transporte> lstEntidad = null;

                var client = new HttpClient();
                Transporte parametro = new Transporte { id_transporte = IdTran };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Transporte/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<Transporte>>();
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

        public async Task<List<Transporte>> GetTransporteAll()
        {
            List<Transporte> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Transporte/all");
                var model = response.Content.ReadAsAsync<List<Transporte>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Transporte>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<Transporte> GetTransporteById(int IdTran)
        {
            Transporte entidad = null;
            try
            {
                List<Transporte> lstEntidad = null;

                var client = new HttpClient();
                Transporte parametro = new Transporte { id_transporte = IdTran };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Transporte/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<Transporte>>();
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

        public ActionResult Create()
        {
            return PartialView();
        }

        public async Task<ActionResult> Edit(int IdTran, string CallBy)
        {
            string msgReturn = "";
            Transporte transporte = null;
            try
            {
                transporte = await GetTransporteById(IdTran);
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(transporte);
        }

        // GET: Transporte
        public async Task<ActionResult> Index(string EstaFilt)
        {
            List<Transporte> lstEntidad = null;
            lstEntidad = await GetTransporteAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ListaTransporte(string EstaFilt, string CallBy)
        {
            List<Transporte> lstEntidad = null;
            lstEntidad = await GetTransporteAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> SaveNewTransporte(string NroPlac)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Transporte se registró satisfactoriamente";
            try
            {
                Transporte transporteReturn = null;
                Transporte transporte = null;

                //Verificamos si existe el transporte
                transporteReturn = await ValidaExisteTransporte(0, NroPlac);
                if (transporteReturn == null)
                {
                    transporte = new Transporte
                    {
                        n_placa = NroPlac.ToUpper()
                    };
                    transporteReturn = await InsertTransporte(transporte);
                    if (transporteReturn == null)
                    {
                        msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    msgReturn = "Transporte ya existe";
                    flgError = 1;
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveEditTransporte(int IdTran, string NroPlac, string EstaTran)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Transporte se actualizó satisfactoriamente";
            try
            {
                Transporte transporteReturn = null;
                Transporte transporte = null;

                //Verificamos si existe el transporte
                transporteReturn = await ValidaExisteTransporte(IdTran, NroPlac);
                if (transporteReturn == null)
                {
                    transporte = new Transporte
                    {
                        id_transporte = IdTran,
                        n_placa = NroPlac.ToUpper(),
                        estado = EstaTran
                    };
                    transporteReturn = await UpdateTransporte(transporte);
                    if (transporteReturn == null)
                    {
                        msgReturn = "Ocurrió un error al intentar Actualizar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    msgReturn = "Transporte ya existe";
                    flgError = 1;
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Actualizar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveDeleteTransporte(int IdTran)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Transporte se Eliminó satisfactoriamente";
            try
            {
                Transporte transporteReturn = null;
                transporteReturn = await DelteTransporte(IdTran);
                if (transporteReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar Eliminar, Por favor comuniquese con el administrador de sistemas";
                    flgError = 1;
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar Eliminar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveActivarTransporte(int IdTran)
        {
            string msgReturn = "";
            int flgError = 0;
            msgReturn = "El Transporte se restableció satisfactoriamente";
            try
            {
                Transporte transporteReturn = null;
                Transporte transporteActivado = null;
                transporteReturn = await GetTransporteById(IdTran);
                if (transporteReturn != null)
                {
                    transporteReturn.estado = "1";
                    transporteActivado = await UpdateTransporte(transporteReturn);
                    if (transporteActivado == null)
                    {
                        msgReturn = "Ocurrió un error al intentar activar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar activar, Por favor comuniquese con el administrador de sistemas";
                    flgError = 1;
                }
                if (flgError == 1)
                {
                    Response.StatusCode = 500;
                    return Json(msgReturn, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar activar, Por favor comuniquese con el administrador de sistemas";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return Json(msgReturn, JsonRequestBehavior.AllowGet);
        }

        public async Task<Transporte> ValidaExisteTransporte(int IdTran, string NroPlac)
        {
            Transporte entidad = null;
            List<Transporte> lstTransporte = null;
            try
            {
                lstTransporte = await GetTransporteAll();
                if (lstTransporte != null)
                {
                    lstTransporte = lstTransporte.Where(i => !i.id_transporte.Equals(IdTran) &&
                                                        i.n_placa.ToUpper().Trim().Equals(NroPlac.ToUpper().Trim()) &&
                                                        !i.estado.Equals("0")).ToList();
                    if (lstTransporte != null)
                    {
                        entidad = lstTransporte[0];
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return entidad;
        }
    }
}