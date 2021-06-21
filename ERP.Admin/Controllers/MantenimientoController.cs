using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Index()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        #region Trabajador
        // GET: Trabajador
        public async System.Threading.Tasks.Task<ActionResult> Trabajador()
        {
            List<Trabajador> ltrabajador = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Trabajador/all");
            var model = response.Content.ReadAsAsync<List<Trabajador>>();
            if (model.Result.Count > 0)
            {
                ltrabajador = model.Result.ToList();
            }
            else
            {
                ltrabajador = new List<Models.Trabajador>();
            }

            return View(ltrabajador);
        }

        public async Task<ActionResult> RegistroTrabajador(string id_trabajador)
        {
            List<Trabajador> _trabajador = new List<Models.Trabajador>();
            List<TipoTrabajador> _tipotrabajador = new List<Models.TipoTrabajador>();
            List<Usuario> _usuario = new List<Models.Usuario>();
            _tipotrabajador = await GetTipoTrabajadorAsync();
            ViewBag.TipoTrabajador = _tipotrabajador;

            List<Persona> _persona = new List<Models.Persona>();

            _persona = await GetPersonasNoTrabajaAsync();

            ViewBag.PersonasST = _persona;

            if (id_trabajador != "0")
            {
                int idtrabajador = 0;
                try
                {
                    idtrabajador = Convert.ToInt32(id_trabajador);
                    _trabajador = await GetTrabajadorIdAsync(idtrabajador);
                    if (_trabajador != null)
                    {
                        ViewBag.Trabajador = _trabajador[0];
                        _usuario = await GetUsuariosAllAsync();
                        if (_usuario != null)
                        {
                            Usuario _user = new Usuario();
                            int idtrabaj = Convert.ToInt32(_trabajador[0].id_trabajador);
                            _user = _usuario.Where(c => c.fk_trabajador == idtrabaj).FirstOrDefault();
                            if (_user != null)
                            {
                                ViewBag.Usuario = _user;
                            }
                            else
                            {
                                ViewBag.Usuario = null;
                            }
                        }
                        else
                        {
                            ViewBag.Usuario = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    idtrabajador = 0;
                    ViewBag.Trabajador = null;
                }

            }
            else
            {
                ViewBag.Trabajador = null;
            }
            return PartialView();
        }

        private async Task<List<Usuario>> GetUsuariosAllAsync()
        {
            List<Usuario> lusuario = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Usuario/all");
            var model = response.Content.ReadAsAsync<List<Usuario>>();
            if (model.Result.Count > 0)
            {
                lusuario = model.Result.ToList();
            }
            else
            {
                lusuario = new List<Models.Usuario>();
            }

            return lusuario;
        }

        private async Task<List<Trabajador>> GetTrabajadorIdAsync(int trabajador)
        {
            List<Trabajador> ltrabajador = null;
            Trabajador _traba = new Trabajador { id_trabajador = trabajador };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Trabajador/buscar", _traba);
            var model = response.Content.ReadAsAsync<List<Trabajador>>();
            if (model.Result.Count > 0)
            {
                ltrabajador = model.Result.ToList();
            }
            return ltrabajador;
        }

        private async Task<List<TipoTrabajador>> GetTipoTrabajadorAsync()
        {
            List<TipoTrabajador> ltipotrabajador = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("TipoTrabajador/all");
            var model = response.Content.ReadAsAsync<List<TipoTrabajador>>();
            if (model.Result.Count > 0)
            {
                ltipotrabajador = model.Result.ToList();
            }
            else
            {
                ltipotrabajador = new List<Models.TipoTrabajador>();
            }

            return ltipotrabajador;
        }

        private async Task<List<Persona>> GetPersonasNoTrabajaAsync()
        {
            List<Persona> lpersona = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Persona/allnotrabajo");
            var model = response.Content.ReadAsAsync<List<Persona>>();
            if (model.Result.Count > 0)
            {
                lpersona = model.Result.ToList();
            }
            else
            {
                lpersona = new List<Models.Persona>();
            }
            return lpersona;
        }
        #endregion Trabajador

        public async System.Threading.Tasks.Task<ActionResult> Persona()
        {
            List<Persona> lpersona = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Persona/all");
            var model = response.Content.ReadAsAsync<List<Persona>>();
            if (model.Result.Count > 0)
            {
                lpersona = model.Result.ToList();
            }
            else
            {
                lpersona = new List<Models.Persona>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lpersona);
        }

        public async Task<ActionResult> RegistroPersona(string id_persona)
        {
            List<Persona> _persona = new List<Models.Persona>();

            if (id_persona != "0")
            {
                int idperson = 0;
                try
                {
                    idperson = Convert.ToInt32(id_persona);
                    _persona = await GetPersonaIdAsync(idperson);
                    if (_persona != null)
                    {
                        ViewBag.Personas = _persona[0];
                    }
                }
                catch (Exception ex)
                {
                    idperson = 0;
                    ViewBag.Personas = null;
                }

            }
            else
            {
                ViewBag.Personas = null;
            }
            return PartialView();
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

        #region TipoTrabajador
        public async System.Threading.Tasks.Task<ActionResult> TipoTrabajador()
        {
            List<TipoTrabajador> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("TipoTrabajador/all");
            var model = response.Content.ReadAsAsync<List<TipoTrabajador>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<Models.TipoTrabajador>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        #endregion
        #region Usuario
        public async System.Threading.Tasks.Task<ActionResult> Usuario()
        {
            List<UsuarioErp> lentidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Usuario/all");
                var model = response.Content.ReadAsAsync<List<UsuarioErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<UsuarioErp>();
                }
            }
            catch (Exception ex)
            {
                lentidad = new List<UsuarioErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lentidad);
        }
        #endregion

        //Luis
        #region Transporte
        public ActionResult Transporte()
        {
            List<Trabajador> lEstados = new List<Trabajador>();
            lEstados.Add(new Trabajador { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new Trabajador { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new Trabajador { estado = "1", NEstado = "VIGENTE" });

            ViewBag.EstadosFilter = lEstados.ToList();
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        #endregion

        #region Conductor
        public ActionResult Conductor()
        {
            List<Conductor> lEstados = new List<Conductor>();
            lEstados.Add(new Conductor { estado = "-1", NEstado = "TODAS" });
            lEstados.Add(new Conductor { estado = "0", NEstado = "ANULADA" });
            lEstados.Add(new Conductor { estado = "1", NEstado = "VIGENTE" });

            ViewBag.EstadosFilter = lEstados.ToList();
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }
        #endregion
        //Fin Luis
    }
}