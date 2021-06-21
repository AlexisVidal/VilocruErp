using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Registro(string id)
        {
            List<UsuarioErp> _entidadp = new List<UsuarioErp>();
            List<TipoUsuarioErp> _tipousuario = new List<TipoUsuarioErp>();
            _tipousuario = await GetTipoUsuarioAsync();
            ViewBag.TipoUsuario = _tipousuario;
            if (id != "")
            {
                try
                {
                    _entidadp = await GetEntidadPIdAsync(id);
                    if (_entidadp != null)
                    {
                        ViewBag.Usuario = _entidadp[0];
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Usuario = null;
                }

            }
            else
            {
                ViewBag.Usuario = null;
            }
            ViewBag.Nuevo = id;
            return PartialView();
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
        private async Task<List<TipoUsuarioErp>> GetTipoUsuarioAsync()
        {
            List<TipoUsuarioErp> ltipotrabajador = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Usuario/t_tipo_usuarioSelectAll");
            var model = response.Content.ReadAsAsync<List<TipoUsuarioErp>>();
            if (model.Result.Count > 0)
            {
                ltipotrabajador = model.Result.ToList();
            }
            else
            {
                ltipotrabajador = new List<TipoUsuarioErp>();
            }

            return ltipotrabajador;
        }
        private async Task<List<UsuarioErp>> GetEntidadPIdAsync(string id_)
        {
            List<UsuarioErp> lentidad = null;
            UsuarioErp identidad = new UsuarioErp { IDUSUARIO = id_ };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("Usuario/buscar", identidad);
            var model = response.Content.ReadAsAsync<List<UsuarioErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Guarda(string IDUSUARIO, string IDCODIGOGENERAL, string EMAIL, string IDTIPOUSUARIO, 
            string PASSWORD, string id_save, string bloqueo)
        {
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            UsuarioErp _usuario = new UsuarioErp();
            TipoTrabajador _tipotrabajador = new TipoTrabajador();
            Trabajador _trabajador = new Trabajador();
            Persona _persona = new Persona();
            if (IDUSUARIO == "")
            {
                _usuario.IDUSUARIO = "";
            }
            else
            {
                _usuario.IDUSUARIO = IDUSUARIO;
            }
            _usuario.IDCODIGOGENERAL = IDCODIGOGENERAL;
            _usuario.EMAIL = EMAIL;
            _usuario.IDUSUARIOTIPO = IDTIPOUSUARIO;
            if (bloqueo.Trim() =="")
            {
                _usuario.BLOQUEO = "NO";
            }
            else
            {
                _usuario.BLOQUEO = bloqueo.Trim();
            }
            

            string userinserted = "1";

            string claveEncrip = "";
            try
            {
                if (PASSWORD == "")
                {
                    _usuario.PASSWORD = PASSWORD;
                }
                else
                {
                    byte[] hash = EncriptaClave(PASSWORD);
                    claveEncrip = Convert.ToBase64String(hash);
                    _usuario.PASSWORD = claveEncrip;
                }

                var client2 = new HttpClient();
                string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client2.BaseAddress = new Uri(connectionInfo);
                client2.DefaultRequestHeaders.Accept.Clear();
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo2 = "";
                if (id_save == "")
                {
                    metodo2 = "Personalerp/t_usuarioInsert";
                }
                else
                {
                    metodo2 = "Personalerp/t_usuarioUpdate";
                }

                var response2 = await client2.PostAsJsonAsync(metodo2, _usuario);
                var respuesta2 = response2.Content.ReadAsAsync<string>();
                if (respuesta2 != null)
                {
                    userinserted = respuesta2.Result.ToString();
                }

            }
            catch (Exception ex)
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json(userinserted, JsonRequestBehavior.AllowGet);
        }
        private byte[] EncriptaClave(string clave)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new System.Text.UnicodeEncoding()).GetBytes(clave);
            byte[] hash2 = sha1.ComputeHash(inputBytes);
            return hash2;
        }




        [HttpPost]
        public async Task<JsonResult> GetUsuarioExistAsync(string IDUSUARIO)
        {
            List<UsuarioErp> _usuarios = new List<UsuarioErp>();
            _usuarios = await GetUsuariosErp();
            IDUSUARIO = IDUSUARIO.Trim();
            string finded = "0";
            if (_usuarios != null && _usuarios.Any())
            {
                try
                {
                    var existe = _usuarios.Where(x => x.IDUSUARIO.Equals(IDUSUARIO)).FirstOrDefault();
                    if (existe != null)
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
        [HttpPost]
        public async Task<JsonResult> GetPersonalExistAsync(string IDCODIGOGENERAL)
        {
            List<UsuarioErp> _usuarios = new List<UsuarioErp>();
            _usuarios = await GetUsuariosErp();
            IDCODIGOGENERAL = IDCODIGOGENERAL.Trim();
            string finded = "0";
            string nombres = "";
            if (_usuarios != null && _usuarios.Any())
            {
                try
                {
                    var existe = _usuarios.Where(x => x.IDCODIGOGENERAL.Equals(IDCODIGOGENERAL)).FirstOrDefault();
                    if (existe != null)
                    {
                        finded = "1";
                    }
                    else
                    {
                        PersonalErp busca = new PersonalErp()
                        {
                            IDCODIGOGENERAL = IDCODIGOGENERAL
                        };
                        var existetrabajador = await GetDatosTrabajador(busca);
                        if (existetrabajador != null)
                        {
                            nombres = existetrabajador.NOMBRES + " " + existetrabajador.A_PATERNO + " " + existetrabajador.A_MATERNO;
                        }
                        else
                        {
                            finded = "2";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new object[] { "0", nombres }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(new object[] { finded, nombres }, JsonRequestBehavior.AllowGet);
        }

        private async Task<PersonalErp> GetDatosTrabajador(PersonalErp busca)
        {
            List<PersonalErp> lstEntidad = new List<PersonalErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_generalSelectId", busca);

                var model = response.Content.ReadAsAsync<List<PersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private async Task<List<UsuarioErp>> GetUsuariosErp()
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
            return lentidad;
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
                    if (_persona.Count > 0)
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
        private async Task<List<Persona>> GetPersonaDni(string dni)
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
        [HttpPost]
        public async Task<JsonResult> Elimina(string id_usuario)
        {
            List<UsuarioErp> _entidad = new List<UsuarioErp>();
            _entidad = await GetEntidadPIdAsync(id_usuario);
            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    UsuarioErp idu = new UsuarioErp { IDUSUARIO = _entidad[0].IDUSUARIO };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Usuario/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, idu);
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

        public async Task<ActionResult> ListaUsuario(string CallBy)
        {
            List<Usuario> lstEntidad = null;
            lstEntidad = await GetUsuarios();

            ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            List<Usuario> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Usuario/all");
                var model = response.Content.ReadAsAsync<List<Usuario>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Usuario>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public static async Task<List<UsuarioModulo>> GetPermisosUsuarioIdAsync(string IdUsua)
        {
            List<UsuarioModulo> lstEntidad = null;
            try
            {
                UsuarioErp parametro = new UsuarioErp { IDUSUARIO = IdUsua };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Usuario/getUsuarioPermisos", parametro);

                var model = response.Content.ReadAsAsync<List<UsuarioModulo>>();
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
        public async Task<ActionResult> Permisos(string id)
        {
            ModuloController CtrlModu = new ModuloController();
            List<UsuarioErp> _entidadp = new List<UsuarioErp>();
            List<TipoTrabajador> _tipotrabajador = new List<TipoTrabajador>();
            List<UsuarioModulo> lstModulo = null;
            //_tipotrabajador = await GetTipoTrabajadorAsync();
            ViewBag.TipoTrabajador = _tipotrabajador;
            if (id != "")
            {
                string id_ = "";
                try
                {
                    id_ = id.Trim();
                    lstModulo = await GetPermisosUsuarioAllModulos(id_);
                    _entidadp = await GetEntidadPIdAsync(id);
                    if (_entidadp != null)
                    {
                        ViewBag.Modulo = lstModulo;
                        ViewBag.Usuario = _entidadp[0];
                    }
                }
                catch (Exception ex)
                {
                    id_ = "";
                    ViewBag.Usuario = null;
                }

            }
            else
            {
                ViewBag.Usuario = null;
            }
            return PartialView(lstModulo.OrderBy(x=>x.descripcion).ToList());
        }
        public async Task<ActionResult> Permisos2(string id)
        {
            ModuloController CtrlModu = new ModuloController();
            List<UsuarioErp> _entidadp = new List<UsuarioErp>();
            List<TipoTrabajador> _tipotrabajador = new List<TipoTrabajador>();
            List<ModuloMenusErp> lstModulo = null;
            //_tipotrabajador = await GetTipoTrabajadorAsync();
            ViewBag.TipoTrabajador = _tipotrabajador;
            if (id != "")
            {
                string id_ = "";
                try
                {
                    id_ = id.Trim();
                    lstModulo = await GetPermisosUsuarioAllModulos2(id_);
                    _entidadp = await GetEntidadPIdAsync(id);
                    if (_entidadp != null)
                    {
                        ViewBag.Modulo = lstModulo;
                        ViewBag.Usuario = _entidadp[0];
                    }
                }
                catch (Exception ex)
                {
                    id_ = "";
                    ViewBag.Usuario = null;
                }

            }
            else
            {
                ViewBag.Usuario = null;
            }
            return PartialView(lstModulo);
        }
        public static async Task<List<ModuloMenusErp>> GetPermisosUsuarioAllModulos2(string IdUsua)
        {
            List<ModuloMenusErp> lstEntidad = null;
            IdUsua = IdUsua.Trim();
            try
            {
                UsuarioErp parametro = new UsuarioErp { IDUSUARIO = IdUsua };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Usuario/getPermisosAllModulos", parametro);

                var model = response.Content.ReadAsAsync<List<ModuloMenusErp>>();
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
        public static async Task<List<UsuarioModulo>> GetPermisosUsuarioAllModulos(string IdUsua)
        {
            List<UsuarioModulo> lstEntidad = null;
            IdUsua = IdUsua.Trim();
            try
            {
                UsuarioErp parametro = new UsuarioErp { IDUSUARIO = IdUsua };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Usuario/getPermisosAllModulos", parametro);

                var model = response.Content.ReadAsAsync<List<UsuarioModulo>>();
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

        [HttpPost]
        public async Task<JsonResult> SaveAsignarPermisos(string FkUsua, List<List<string>> ArraUsuaModu)
        {
            int FkModu = 0;
            int FkMenu = 0;
            string EstaUsuaModu = "";
            UsuarioModulo usuario_modulo = null;
            UsuarioModulo UsuaModuloReturn = null;
            try
            {
                if (ArraUsuaModu != null)
                {
                    foreach (var OneUsuaModu in ArraUsuaModu)
                    {
                        FkModu = Convert.ToInt32(OneUsuaModu[0]);
                        FkMenu = Convert.ToInt32(OneUsuaModu[2]);
                        EstaUsuaModu = OneUsuaModu[1].Trim();
                        usuario_modulo = new UsuarioModulo
                        {
                            IDUSUARIO = FkUsua,
                            fk_modulo = FkModu,
                            fk_menu_modulo = FkMenu,
                            estado = EstaUsuaModu
                        };
                        UsuaModuloReturn = await InsertUsuarioModulo(usuario_modulo);
                        if (UsuaModuloReturn == null)
                        {
                            return Json("-1", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public static async Task<UsuarioModulo> InsertUsuarioModulo(UsuarioModulo usuario_modulo)
        {
            List<UsuarioModulo> lstEntidad = null;
            UsuarioModulo entidad = null;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Usuario/InsertUsuarioModulo", usuario_modulo);

                var model = response.Content.ReadAsAsync<List<UsuarioModulo>>();
                if (model != null && model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    entidad = lstEntidad[0];
                }
                else
                {
                    return null;
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