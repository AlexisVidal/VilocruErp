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
    [SessionAuthorize]
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPersona(string id_persona, string nombres, string apellido_paterno, string apellido_materno,
            string f_nacimiento, string email, string dni)
        {
            Persona _persona = new Persona();
            if (id_persona == "0")
            {
                _persona.id_persona = 0;
            }
            else
            {
                _persona.id_persona = Convert.ToInt32(id_persona);
            }
            _persona.nombres = nombres;
            _persona.apellido_paterno = apellido_paterno;
            _persona.apellido_materno = apellido_materno;
            if (f_nacimiento != "")
            {
                try
                {
                    _persona.f_nacimiento = Convert.ToDateTime(f_nacimiento);
                }
                catch (Exception ex)
                {
                    _persona.f_nacimiento = Convert.ToDateTime(DateTime.Now);
                }
            }
            else
            {
                _persona.f_nacimiento = Convert.ToDateTime(DateTime.Now);
            }
            _persona.email = email;
            _persona.dni = dni;
            _persona.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_persona.id_persona == 0)
                {
                    metodo = "Persona/agregar";
                }
                else
                {
                    metodo = "Persona/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _persona);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> EliminaPersona(string id_persona)
        {
            List<Persona> _persona = new List<Models.Persona>();
            _persona = await GetPersonaIdAsync(Convert.ToInt32(id_persona));
            string idinserted = "0";
            if (_persona != null)
            {
                try
                {
                    IdPersonaModel idperson = new IdPersonaModel { id_persona = _persona[0].id_persona };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Persona/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idperson);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = respuesta.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }
        private async Task<List<Persona>> GetPersonaIdAsync(int id_persona)
        {
            List<Persona> lpersona = null;
            IdPersonaModel idperson = new IdPersonaModel { id_persona = id_persona };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Persona/buscar", idperson);
            var model = response.Content.ReadAsAsync<List<Persona>>();
            if (model.Result.Count > 0)
            {
                lpersona = model.Result.ToList();
            }
            return lpersona;
        }
        [HttpPost]
        public async Task<JsonResult> GetPersonaDniAsync(string dnic)
        {
            List<Persona> _persona = new List<Models.Persona>();
            _persona = await GetPersonaDni(dnic);
            string finded = "0";
            if (_persona != null)
            {
                try
                {
                    if (_persona.Count > 0 )
                    {
                        finded = "1";
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(finded, JsonRequestBehavior.AllowGet);
        }
        public async Task<List<Persona>> GetPersonaDni(string dni)
        {
            List<Persona> lpersona = null;
            Persona _person = new Persona { dni = dni };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Persona/buscardni", _person);
            var model = response.Content.ReadAsAsync<List<Persona>>();
            if (model.Result.Count > 0)
            {
                lpersona = model.Result.ToList();
            }
            return lpersona;
        }

        //Luis
        public async Task<int> InsertPersona(Persona persona)
        {
            int idinserted = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Persona/agregar", persona);

                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return idinserted;
        }
        //Fin Luis

        
    }
}