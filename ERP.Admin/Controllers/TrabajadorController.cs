using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using ERP.Admin.App_Start;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class TrabajadorController : Controller
    {
        // GET: Trabajador
        public async Task<ActionResult> IndexAsync()
        {


            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Filter_PersonaAsync(string persona)
        {
            List<Persona> _persona = new List<Models.Persona>();
            List<Persona> _personaresult = new List<Models.Persona>();

            _persona = await GetPersonasNoTrabajaAsync();

            if (persona.Length > 0)
            {
                _personaresult = _persona.Where(z => z.apellido_paterno.Contains(persona.ToUpper())).ToList();
            }
            return Json(_personaresult);
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
        [HttpPost]
        public async Task<JsonResult> GuardaTrabajador(string id_trabajador_save, string fkpersona, string fktipotra, string password)
        {
            Trabajador _trabajador = new Trabajador();
            Usuario _usuario = new Usuario();
            if (id_trabajador_save == "0")
            {
                _trabajador.id_trabajador = 0;
            }
            else
            {
                _trabajador.id_trabajador = Convert.ToInt32(id_trabajador_save);
            }
            _trabajador.fk_persona = Convert.ToInt32(fkpersona);
            _trabajador.fk_tipo_trabajador = Convert.ToInt32(fktipotra);
            _trabajador.estado = "1";
            string idinserted = "0";
            string userinserted = "0";
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_trabajador.id_trabajador == 0)
                {
                    metodo = "Trabajador/agregar";
                }
                else
                {
                    metodo = "Trabajador/modificar";
                }
                var response = await client.PostAsJsonAsync(metodo, _trabajador);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                }

                if (password != "" && Convert.ToInt32(idinserted) > 0)
                {
                    if (Convert.ToInt32(idinserted) > 0)
                    {
                        _usuario = await GetUsuarioFkAsync(Convert.ToInt32(idinserted));
                    }

                    try
                    {
                        byte[] hash = EncriptaClave(password);
                        string claveEncrip = Convert.ToBase64String(hash);
                        var client2 = new HttpClient();
                        Usuario _usernew = new Usuario();
                        string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                        client2.BaseAddress = new Uri(connectionInfo);
                        client2.DefaultRequestHeaders.Accept.Clear();
                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string metodo2 = "";
                        if (_usuario == null)
                        {
                            metodo2 = "Usuario/agregar";
                        }
                        else
                        {
                            metodo2 = "Usuario/modificar";
                        }
                        _usernew = new Usuario();
                        if (_usuario != null)
                        {
                            _usernew.id_usuario = _usuario.id_usuario;
                        }
                        else
                        {
                            _usernew.id_usuario = 0;
                        }

                        _usernew.fk_trabajador = Convert.ToInt32(idinserted);
                        _usernew.clave = claveEncrip;
                        _usernew.clave_modificaciones = claveEncrip;
                        _usernew.estado = "1";
                        var response2 = await client2.PostAsJsonAsync(metodo2, _usernew);
                        var respuesta2 = response2.Content.ReadAsAsync<string>();
                        if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                        {
                            userinserted = respuesta.Result.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }
                }
                else if (password == "" && Convert.ToInt32(idinserted) > 0)
                {
                    return Json(idinserted, JsonRequestBehavior.AllowGet);
                }
                else if (password == "" && Convert.ToInt32(idinserted) < 1)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
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

        private async Task<Usuario> GetUsuarioFkAsync(int id)
        {
            List<Usuario> lusuario = null;
            Usuario lusuario2 = null;
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
                lusuario2 = lusuario.Where(z => z.fk_trabajador == id).FirstOrDefault();
            }
            return lusuario2;
        }
    }
}