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
    public class ConductorController : Controller
    {
        public async Task<Conductor> InsertConductor(Conductor conductor)
        {
            Conductor entidad = null;
            try
            {
                List<Conductor> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Conductor/agregar", conductor);

                var model = response.Content.ReadAsAsync<List<Conductor>>();
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

        public async Task<Conductor> UpdateConductor(Conductor conductor)
        {
            Conductor entidad = null;
            try
            {
                List<Conductor> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Conductor/modificar", conductor);

                var model = response.Content.ReadAsAsync<List<Conductor>>();
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

        public async Task<Conductor> DeleteConductor(int IdCond)
        {
            Conductor entidad = null;
            try
            {
                List<Conductor> lstEntidad = null;

                var client = new HttpClient();
                Conductor parametro = new Conductor { id_conductor = IdCond };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Conductor/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<Conductor>>();
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

        public async Task<List<Conductor>> GetConductorAll()
        {
            List<Conductor> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Conductor/all");
                var model = response.Content.ReadAsAsync<List<Conductor>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Conductor>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<Conductor> GetConductorById(int IdCond)
        {
            Conductor entidad = null;
            try
            {
                List<Conductor> lstEntidad = null;

                var client = new HttpClient();
                Conductor parametro = new Conductor { id_conductor = IdCond };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Conductor/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<Conductor>>();
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

        public async Task<ActionResult> Edit(int IdCond, string CallBy)
        {
            string msgReturn = "";
            Conductor conductor = null;
            try
            {
                conductor = await GetConductorById(IdCond);
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            return PartialView(conductor);
        }

        // GET: Conductor
        public async Task<ActionResult> Index(string EstaFilt)
        {
            List<Conductor> lstEntidad = null;
            lstEntidad = await GetConductorAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            return PartialView(lstEntidad);
        }

        public async Task<ActionResult> ListaConductor(string EstaFilt, string CallBy)
        {
            List<Conductor> lstEntidad = null;
            lstEntidad = await GetConductorAll();
            if (EstaFilt != null && EstaFilt != "-1")
            {
                lstEntidad = lstEntidad.Where(i => i.estado.Equals(EstaFilt)).ToList();
            }
            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<Conductor> BuscaConductorByNroLicencia(int IdCond, string NroLice)
        {
            Conductor entidad = null;
            List<Conductor> lstEntidad = null;
            try
            {
                lstEntidad = await GetConductorAll();
                if (lstEntidad != null)
                {
                    entidad = lstEntidad.Where(i => !i.id_conductor.Equals(IdCond) && i.n_licencia.ToUpper().Equals(NroLice.ToUpper().Trim())
                                                && i.estado.Equals("1")).ToList()[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return entidad;
        }

        public async Task<ActionResult> SaveNewConductor(string NombPers, string ApelPatePers, string ApelMatePers,
            string FechNaciPers, string EmaiPers, string DniPers, string NroLiceCond)
        {
            string msgReturn = "";
            DateTime NewFechNaci = DateTime.Now;
            int FkPers = 0;
            int flgError = 0;
            msgReturn = "El Conductor se registró satisfactoriamente";
            try
            {
                PersonaController CtrlPers = new PersonaController();
                Conductor conductorReturn = null;
                List<Persona> lstPersonaReturn = null;
                Persona personaReturn = null;
                Persona persona = null;
                Conductor conductor = null;
                NewFechNaci = Convert.ToDateTime(FechNaciPers);

                //Verificamos si existe el conductor
                conductorReturn = await BuscaConductorByNroLicencia(0, NroLiceCond);
                if (conductorReturn == null)
                {
                    //Verificamos si existe la persona
                    lstPersonaReturn = await CtrlPers.GetPersonaDni(DniPers);
                    if (lstPersonaReturn != null)
                    {
                        lstPersonaReturn = lstPersonaReturn.Where(i => i.estado.Equals(1)).ToList();
                        if (personaReturn != null)
                        {
                            FkPers = lstPersonaReturn[0].id_persona;
                        }
                        else
                        {
                            //Si no existe registramos la persona
                            persona = new Persona
                            {
                                nombres = NombPers.ToUpper(),
                                apellido_paterno = ApelPatePers.ToUpper(),
                                apellido_materno = ApelMatePers.ToUpper(),
                                f_nacimiento = NewFechNaci,
                                email = EmaiPers.ToUpper(),
                                dni = DniPers,
                                direccion = ""
                        };
                        FkPers = await CtrlPers.InsertPersona(persona);
                    }
                }
                else
                {
                    //Si no existe registramos la persona
                    persona = new Persona
                    {
                        nombres = NombPers.ToUpper(),
                        apellido_paterno = ApelPatePers.ToUpper(),
                        apellido_materno = ApelMatePers.ToUpper(),
                        f_nacimiento = NewFechNaci,
                        email = EmaiPers.ToUpper(),
                        dni = DniPers,
                        direccion = ""
                    };
                    FkPers = await CtrlPers.InsertPersona(persona);
                }
                if (FkPers > 0)
                {
                    conductor = new Conductor
                    {
                        fk_persona = FkPers,
                        n_licencia = NroLiceCond.ToUpper()
                    };
                    conductorReturn = await InsertConductor(conductor);
                    if (conductorReturn == null)
                    {
                        msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                    flgError = 1;
                }
            }
                else
                {
                msgReturn = "Conductor ya existe";
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

public async Task<ActionResult> SaveEditConductor(int IdCond, int FkPers, string NombPers,
    string ApelPatePers, string ApelMatePers, string FechNaciPers, string EmaiPers, string DniPers,
    string NroLiceCond, string EstaCond)
{
    string msgReturn = "";
    DateTime NewFechNaci = DateTime.Now;
    int flgError = 0;
    msgReturn = "El Conductor se Actualizó satisfactoriamente";
    try
    {
        PersonaController CtrlPers = new PersonaController();
        Conductor conductorReturn = null;
        List<Persona> lstPersonaReturn = null;
        Persona personaReturn = null;
        Conductor conductor = null;
        NewFechNaci = Convert.ToDateTime(FechNaciPers);

        //Verificamos si existe el conductor
        conductorReturn = await BuscaConductorByNroLicencia(IdCond, NroLiceCond);
        if (conductorReturn == null)
        {
            //Verificamos si existe la persona
            lstPersonaReturn = await CtrlPers.GetPersonaDni(DniPers);
            if (lstPersonaReturn != null)
            {
                lstPersonaReturn = lstPersonaReturn.Where(i => !i.id_persona.Equals(FkPers) && i.estado.Equals(1)).ToList();
                if (personaReturn != null)
                {
                    FkPers = lstPersonaReturn[0].id_persona;
                }
            }
            if (FkPers > 0)
            {
                conductor = new Conductor
                {
                    id_conductor = IdCond,
                    fk_persona = FkPers,
                    n_licencia = NroLiceCond.ToUpper(),
                    estado = EstaCond
                };
                conductorReturn = await UpdateConductor(conductor);
                if (conductorReturn == null)
                {
                    msgReturn = "Ocurrió un error al intentar actualizar, Por favor comuniquese con el administrador de sistemas";
                    flgError = 1;
                }
            }
            else
            {
                msgReturn = "Ocurrió un error al intentar actualizar, Por favor comuniquese con el administrador de sistemas";
                flgError = 1;
            }
        }
        else
        {
            msgReturn = "Ya existe un conductor con ese nro de licencia";
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
        msgReturn = "Ocurrió un error al intentar actualizar, Por favor comuniquese con el administrador de sistemas";
        Response.StatusCode = 500;
        return Json(msgReturn, JsonRequestBehavior.AllowGet);
    }
    return Json(msgReturn, JsonRequestBehavior.AllowGet);
}

public async Task<ActionResult> SaveDeleteConductor(int IdCond)
{
    string msgReturn = "";
    DateTime NewFechNaci = DateTime.Now;
    int flgError = 0;
    msgReturn = "El Conductor se Eliminó satisfactoriamente";
    try
    {
        Conductor conductorReturn = null;
        conductorReturn = await DeleteConductor(IdCond);
        if (conductorReturn == null)
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

public async Task<ActionResult> SaveActivarConductor(int IdCond)
{
    string msgReturn = "";
    DateTime NewFechNaci = DateTime.Now;
    int flgError = 0;
    msgReturn = "El Conductor se restableció satisfactoriamente";
    try
    {
        Conductor conductorReturn = null;
        Conductor conductorActivado = null;
        conductorReturn = await GetConductorById(IdCond);
        if (conductorReturn != null)
        {
            conductorReturn.estado = "1";
            conductorActivado = await UpdateConductor(conductorReturn);
            if (conductorActivado == null)
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
    }
}