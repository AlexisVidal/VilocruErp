using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Admin.App_Start;
using ERP.Admin.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class PersonalController : Controller
    {
        List<AfpErp> lafp = null;
        List<BancoErp> lbanco = null;
        List<RegimenLaboralErp> lreglaboral = null;
        List<TipoContratoErp> ltipocontrato = null;
        // GET: Personal
        public ActionResult Index()
        {
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        public async Task<ActionResult> Afp()
        {
            lafp = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_afpsSelectAll");
            var model = response.Content.ReadAsAsync<List<AfpErp>>();
            if (model.Result.Count > 0)
            {
                lafp = model.Result.ToList();
            }
            else
            {
                lafp = new List<AfpErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lafp);
        }
        //public ActionResult AfpGridViewPartial()
        //{
        //    return PartialView("AfpGridViewPartial", lpersona);
        //}

        public async Task<ActionResult> RegimenLaboral()
        {
            lreglaboral = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_regimen_laboralSelectAll");
            var model = response.Content.ReadAsAsync<List<RegimenLaboralErp>>();
            if (model.Result.Count > 0)
            {
                lreglaboral = model.Result.ToList();
            }
            else
            {
                lreglaboral = new List<RegimenLaboralErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lreglaboral);
        }

        public async Task<ActionResult> Personal()
        {
            List<PersonalErp> lstEntidad = null;
            lstEntidad = await GetTrabajadorAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.ToList();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lstEntidad);
        }
        public async Task<ActionResult> ListaTrabajador()
        {
            List<PersonalErp> lstEntidad = null;
            lstEntidad = await GetTrabajadorAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.ToList();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            //ViewBag.CallBy = CallBy;
            return PartialView(lstEntidad);
        }

        private async Task<List<PersonalErp>> GetTrabajadorAll()
        {
            List<PersonalErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_generalSelectAll");
                var model = response.Content.ReadAsAsync<List<PersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PersonalErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return lstEntidad;
        }

        public async Task<ActionResult> TipoContrato()
        {
            ltipocontrato = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_tp_contratoSelectAll");
            var model = response.Content.ReadAsAsync<List<TipoContratoErp>>();
            if (model.Result.Count > 0)
            {
                ltipocontrato = model.Result.ToList();
            }
            else
            {
                ltipocontrato = new List<TipoContratoErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(ltipocontrato);
        }

        public async Task<List<TipoContratoErp>> GetTiposContrato()
        {
            ltipocontrato = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_tp_contratoSelectAll");
            var model = response.Content.ReadAsAsync<List<TipoContratoErp>>();
            if (model.Result.Count > 0)
            {
                ltipocontrato = model.Result.ToList();
            }
            else
            {
                ltipocontrato = new List<TipoContratoErp>();
            }
            return ltipocontrato;
        }

        public async Task<ActionResult> GetTrabajadorById(string IdTrabajador)
        {
            List<SexoErp> _lsexos = new List<SexoErp>()
            {
                new SexoErp(){SEXO="",DESCRIPCION=""},
                new SexoErp(){SEXO="M",DESCRIPCION="MASCULINO"},
                new SexoErp(){SEXO="F",DESCRIPCION="FEMENINO"}
            };
            ViewBag.Sexos = _lsexos;

            List<NacionalidadErp> _lnacionalidades = new List<NacionalidadErp>();
            _lnacionalidades = await GetNacionalidadAsync();
            ViewBag.Nacionalidades = _lnacionalidades;

            List<DocIdentidadErp> _ldocidentidad = new List<DocIdentidadErp>();
            DocIdentidadErp docid = new DocIdentidadErp()
            {
                IDDOCIDENTIDAD = "",
                DESCRIPCION = ""
            };
            _ldocidentidad = await GetDocIdentidadAsync();
            _ldocidentidad.Add(docid);
            ViewBag.DocIdentidad = _ldocidentidad.OrderBy(x => x.IDDOCIDENTIDAD).ToList();

            List<EstadoCivilErp> _lestadocivil = new List<EstadoCivilErp>();
            _lestadocivil = await GetEstadoCivilAsync();
            ViewBag.EstadoCivil = _lestadocivil;

            List<AfpErp> _lafps = new List<AfpErp>();
            AfpErp newafp = new AfpErp()
            {
                IDAFP = "",
                DESCRIPCION = ""
            };
            _lafps = await GetAfpsAsync();
            _lafps.Add(newafp);
            ViewBag.Afps = _lafps.OrderBy(x => x.IDAFP).ToList();

            BancoErp newadd = new BancoErp()
            {
                id_banco = 0,
                idbanco = "",
                descripcion = "",
                codigo_sunat = "",
                direccion = "",
                estado = "1",
                telefono = ""
            };
            List<BancoErp> lbancos = new List<BancoErp>();
            lbancos = await GetBancoErp();
            lbancos.Add(newadd);
            ViewBag.Bancos = lbancos.Where(y => y.estado.Equals("1")).OrderBy(x => x.id_banco).ToList();

            PeriodicidadSueldoErp newaddp = new PeriodicidadSueldoErp()
            {
                IDPERIODICIDAD = "",
                DESCRIPCION = ""
            };
            List<PeriodicidadSueldoErp> lperiodios = new List<PeriodicidadSueldoErp>();
            lperiodios = await GetPeriodicidadSueldoErp();
            lperiodios.Add(newaddp);
            ViewBag.Periodos = lperiodios.OrderBy(x => x.IDPERIODICIDAD).ToList();

            TipoRemuneracionErp newaddt = new TipoRemuneracionErp()
            {
                TIPOREMUNERACION = "",
                DESCRIPCION = ""
            };
            List<TipoRemuneracionErp> ltiporem = new List<TipoRemuneracionErp>();
            ltiporem = await GetTipoRemuneracionErp();
            ltiporem.Add(newaddt);
            ViewBag.TiposRem = ltiporem.OrderBy(x => x.TIPOREMUNERACION).ToList();


            List<DepartamentoErp> _ldepartamentos = new List<DepartamentoErp>();
            _ldepartamentos = await GetDepartamentosAsync();
            ViewBag.Departamentos = _ldepartamentos;

            List<NivelEstudioErp> _lniveles = new List<NivelEstudioErp>();
            _lniveles = await GetNivelEstudioAsync();
            ViewBag.Niveles = _lniveles;

            PersonalErp busca = new PersonalErp() { IDCODIGOGENERAL = IdTrabajador };

            PersonalErp entidad = new PersonalErp();

            if (IdTrabajador != "")
            {
                entidad = await GetDatosTrabajador(busca);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(entidad);
        }

        private async Task<List<TipoRemuneracionErp>> GetTipoRemuneracionErp()
        {
            List<TipoRemuneracionErp> xentidad = new List<TipoRemuneracionErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_tipo_remuneracionSelectAll");
                var model = response.Content.ReadAsAsync<List<TipoRemuneracionErp>>();
                if (model.Result.Count > 0)
                {
                    xentidad = model.Result.ToList();
                }
                else
                {
                    xentidad = new List<TipoRemuneracionErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return xentidad;
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




        private async Task<List<DepartamentoErp>> GetDepartamentosAsync()
        {
            List<DepartamentoErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_departamentoSelectAll");
            var model = response.Content.ReadAsAsync<List<DepartamentoErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<DepartamentoErp>();
            }
            return lentidad;
        }
        private async Task<List<AfpErp>> GetAfpsAsync()
        {
            List<AfpErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_afpsSelectAll");
            var model = response.Content.ReadAsAsync<List<AfpErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<AfpErp>();
            }
            return lentidad;
        }
        private async Task<List<NacionalidadErp>> GetNacionalidadAsync()
        {
            List<NacionalidadErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_nacionalidadSelectAll");
            var model = response.Content.ReadAsAsync<List<NacionalidadErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<NacionalidadErp>();
            }
            return lentidad;
        }
        private async Task<List<DocIdentidadErp>> GetDocIdentidadAsync()
        {
            List<DocIdentidadErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_doc_identidadSelectAll");
            var model = response.Content.ReadAsAsync<List<DocIdentidadErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<DocIdentidadErp>();
            }
            return lentidad;
        }
        private async Task<List<EstadoCivilErp>> GetEstadoCivilAsync()
        {
            List<EstadoCivilErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_estado_civilSelectAll");
            var model = response.Content.ReadAsAsync<List<EstadoCivilErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<EstadoCivilErp>();
            }
            return lentidad;
        }
        private async Task<List<NivelEstudioErp>> GetNivelEstudioAsync()
        {
            List<NivelEstudioErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_nivel_estudioSelectAll");
            var model = response.Content.ReadAsAsync<List<NivelEstudioErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<NivelEstudioErp>();
            }
            return lentidad;
        }
        [HttpPost]
        public async Task<JsonResult> Filter_ProvinciaAsync(string IDDEPARTAMENTO)
        {
            ProvinciaErp busca = new ProvinciaErp() { IDDEPARTAMENTO = IDDEPARTAMENTO };
            List<ProvinciaErp> lstEntidad = new List<ProvinciaErp>();

            var client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("Personalerp/t_provinciaByDepartamento", busca);

            var model = response.Content.ReadAsAsync<List<ProvinciaErp>>();
            if (model.Result.Count > 0)
            {
                lstEntidad = model.Result.ToList();
            }
            return Json(lstEntidad, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> Filter_DistritoAsync(string IDPROVINCIA)
        {
            DistritoErp busca = new DistritoErp() { IDPROVINCIA = IDPROVINCIA };
            List<DistritoErp> lstEntidad = new List<DistritoErp>();

            var client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("Personalerp/t_distritoByProvincia", busca);

            var model = response.Content.ReadAsAsync<List<DistritoErp>>();
            if (model.Result.Count > 0)
            {
                lstEntidad = model.Result.ToList();
            }
            return Json(lstEntidad, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ListDataRegistroReferencias(string NRODOCUMENTO)
        {
            string msgReturn = "";
            List<PersonalReferenciaErp> lstEntidad = null;
            PersonalReferenciaErp busca = new PersonalReferenciaErp() { IDCODIGOGENERAL = NRODOCUMENTO.Trim() };

            try
            {
                lstEntidad = await GetPersonalReferenciaErp(busca);
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstEntidad);
        }

        public async Task<List<PersonalReferenciaErp>> GetPersonalReferenciaErp(PersonalReferenciaErp busca)
        {
            List<PersonalReferenciaErp> lstEntidad = null;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_referenciaByCodigo", busca);

                var model = response.Content.ReadAsAsync<List<PersonalReferenciaErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.PersonalReferenciaErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> ListDataRegistroFamiliares(string NRODOCUMENTO)
        {
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftpderechohabiente"];
            ViewBag.UrlLink = virtualdir;
            string msgReturn = "";
            List<PersonalFamiliarErp> lstEntidad = null;
            PersonalFamiliarErp busca = new PersonalFamiliarErp() { IDCODIGOGENERAL = NRODOCUMENTO.Trim() };

            try
            {
                lstEntidad = await GetPersonalFamiliarErp(busca);
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstEntidad);
        }
        public ActionResult VerFoto(string foto)
        {
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + "fotopersonal/";
            int indexlast = foto.LastIndexOf('/');
            foto = foto.Substring(indexlast + 1);
            foto = virtualdir + foto;
            ViewBag.Foto = foto;
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }
        [HttpPost]
        public JsonResult GuardarImagenPersonal()
        {
            string urlftp = "";
            try
            {
                string idCodigogeneral = "";

                idCodigogeneral = ControllerContext.HttpContext.Request.Form["fidcodigogeneral"];
                var folderpersonal = System.Configuration.ConfigurationManager.AppSettings["Ftppersonal"];
                HttpFileCollectionBase files = Request.Files;
                if (files.Count == 2)
                {
                    HttpPostedFileBase file = files[0];
                    idCodigogeneral = files.AllKeys[1].ToString();
                    urlftp = UploadFtp(file, idCodigogeneral, folderpersonal, "");
                    if (urlftp != "")
                    {
                        return Json(urlftp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(urlftp, JsonRequestBehavior.AllowGet);
        }
        private string UploadFtp(HttpPostedFileBase file, string idCodigogeneral, string folder, string nombrenormal)
        {
            try
            {
                var uploadurl = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
                string uploadfilename = "";
                if (nombrenormal == "")
                {
                    uploadfilename = idCodigogeneral.ToString().PadLeft(4, '0') + System.IO.Path.GetExtension(file.FileName); // file.FileName;
                }
                else
                {
                    uploadfilename = file.FileName.ToString()/* + System.IO.Path.GetExtension(file.FileName)*/; // file.FileName;
                }

                var username = System.Configuration.ConfigurationManager.AppSettings["Ftpuser"];
                var password = System.Configuration.ConfigurationManager.AppSettings["Ftppass"];


                Stream streamObj = file.InputStream;
                byte[] buffer = new byte[file.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                string ftpurl = String.Format("{0}/{1}", uploadurl + folder, uploadfilename);
                var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                requestObj.Credentials = new NetworkCredential(username, password);
                Stream requestStream = requestObj.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
                requestObj = null;
                return ftpurl;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<List<PersonalFamiliarErp>> GetPersonalFamiliarErp(PersonalFamiliarErp busca)
        {
            List<PersonalFamiliarErp> lstEntidad = null;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_familiarSelectByCodigo", busca);

                var model = response.Content.ReadAsAsync<List<PersonalFamiliarErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PersonalFamiliarErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public async Task<ActionResult> EditReferencia(string idcodigogeneral, int IDPERSONALREFERENCIA)
        {
            List<PersonalReferenciaErp> _lentidad = new List<PersonalReferenciaErp>();
            PersonalReferenciaErp busca = new PersonalReferenciaErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };

            if (idcodigogeneral != "" && IDPERSONALREFERENCIA != 0)
            {
                try
                {
                    _lentidad = await GetPersonalReferenciaErp(busca);
                    if (_lentidad != null)
                    {
                        ViewBag.ReferenciaExist = _lentidad.Where(x => x.IDPERSONALREFERENCIA == IDPERSONALREFERENCIA).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.ReferenciaExist = null;
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.ReferenciaExist = null;
                }

            }
            else
            {
                ViewBag.ReferenciaExist = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> GuardaReferencia(string id_save_ref, string IDCODIGOGENERAL,
                            string NOMBRESRef, string CARGORef, string TELEFONORef)
        {
            PersonalReferenciaErp _entidad = new PersonalReferenciaErp();
            if (id_save_ref == "0")
            {
                _entidad.IDPERSONALREFERENCIA = 0;
            }
            else
            {


                _entidad.IDPERSONALREFERENCIA = Convert.ToInt32(id_save_ref);
            }
            _entidad.IDCODIGOGENERAL = IDCODIGOGENERAL;
            _entidad.NOMBRES = NOMBRESRef;
            _entidad.CARGO = CARGORef;
            _entidad.TELEFONO = TELEFONORef;
            _entidad.ESTADO = "1";

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.IDPERSONALREFERENCIA == 0)
                {
                    metodo = "Personalerp/t_personal_referenciaInsert";
                }
                else
                {
                    metodo = "Personalerp/t_personal_referenciaUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
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
        public async Task<JsonResult> EliminaReferencia(string idcodigogeneral, int IDPERSONALREFERENCIA)
        {
            List<PersonalReferenciaErp> _lentidad = new List<PersonalReferenciaErp>();
            PersonalReferenciaErp _entidad = new PersonalReferenciaErp();

            PersonalReferenciaErp busca = new PersonalReferenciaErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };

            if (idcodigogeneral != "" && IDPERSONALREFERENCIA != 0)
            {
                try
                {
                    _lentidad = await GetPersonalReferenciaErp(busca);
                    if (_lentidad != null)
                    {
                        _entidad = _lentidad.Where(x => x.IDPERSONALREFERENCIA == IDPERSONALREFERENCIA).FirstOrDefault();
                    }
                    else
                    {
                        _entidad = null;
                    }
                }
                catch (Exception ex)
                {
                    _entidad = null;
                }
            }
            else
            {
                _entidad = null;
            }

            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    PersonalReferenciaErp id = new PersonalReferenciaErp { IDPERSONALREFERENCIA = _entidad.IDPERSONALREFERENCIA };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_personal_referenciaDelete";
                    var response = await client.PostAsJsonAsync(metodo, id);
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

        public async Task<ActionResult> EditDhabientes(string idcodigogeneral, int IDPERSONALFAMILIAR)
        {
            List<VinculoFamiliarErp> _lvinculos = new List<VinculoFamiliarErp>();
            _lvinculos = await GetVinculoFamiliarAsync();
            VinculoFamiliarErp nuevo = new VinculoFamiliarErp()
            {
                IDVINCULO = "",
                DESCRIPCION = "",
                FECHACREACION = DateTime.Now,
                ESTADO = "1"
            };
            _lvinculos.Add(nuevo);
            ViewBag.Vinculos = _lvinculos.OrderBy(x => x.IDVINCULO).ToList();

            List<PersonalFamiliarErp> _lentidad = new List<PersonalFamiliarErp>();
            PersonalFamiliarErp busca = new PersonalFamiliarErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };

            if (idcodigogeneral != "" && IDPERSONALFAMILIAR != 0)
            {
                try
                {
                    _lentidad = await GetPersonalFamiliarErp(busca);
                    if (_lentidad != null)
                    {
                        ViewBag.FamiliarExist = _lentidad.Where(x => x.IDPERSONALFAMILIAR == IDPERSONALFAMILIAR).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.FamiliarExist = null;
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.FamiliarExist = null;
                }

            }
            else
            {
                ViewBag.FamiliarExist = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        private async Task<List<VinculoFamiliarErp>> GetVinculoFamiliarAsync()
        {
            List<VinculoFamiliarErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_vinculo_familiarSelectAll");
            var model = response.Content.ReadAsAsync<List<VinculoFamiliarErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<VinculoFamiliarErp>();
            }
            return lentidad;
        }

        [HttpPost]
        public async Task<JsonResult> Filter_DocVinculoFamiliarAsync(string IDVINCULO)
        {
            DocVinculoFamiliarErp busca = new DocVinculoFamiliarErp() { IDVINCULO = IDVINCULO };
            List<DocVinculoFamiliarErp> lstEntidad = new List<DocVinculoFamiliarErp>();
            DocVinculoFamiliarErp nuevo = new DocVinculoFamiliarErp()
            {
                IDVINCULO = "",
                DESCRIPCION = "",
                VINCULO = "",
                IDDOCVINC = "",
                ESTADO = "1",
                FECHACREACION = DateTime.Now
            };

            var client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("Personalerp/t_doc_vinculo_familiarSelectAll");

            var model = response.Content.ReadAsAsync<List<DocVinculoFamiliarErp>>();
            if (model.Result.Count > 0)
            {
                lstEntidad = model.Result.Where(x => x.IDVINCULO.Equals(IDVINCULO)).ToList();
            }
            lstEntidad.Add(nuevo);
            return Json(lstEntidad.OrderBy(x => x.IDDOCVINC).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAdjuntoDerecho()
        {
            string urlftp = "";
            try
            {
                string idCodigogeneral = "";
                string folder = System.Configuration.ConfigurationManager.AppSettings["Ftpderechohabiente"];
                idCodigogeneral = ControllerContext.HttpContext.Request.Form["fidcodigogeneral"];
                HttpFileCollectionBase files = Request.Files;
                if (files.Count == 2)
                {
                    HttpPostedFileBase file = files[0];
                    idCodigogeneral = files.AllKeys[1].ToString();
                    urlftp = UploadFtp(file, idCodigogeneral, folder, "otros");
                    if (urlftp != "")
                    {
                        return Json(urlftp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(urlftp, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GuardaFamiliar(string id_save_fam, string IDCODIGOGENERAL, string CODIGOGENERALFam, string TELEFONOFam,
                            string NOMBRESFam, string IDVINCULOFam, string IDDOCVINCFam, string txtfecha_nacDerec, string stradjunto)
        {
            PersonalFamiliarErp _entidad = new PersonalFamiliarErp();
            if (id_save_fam == "0")
            {
                _entidad.IDPERSONALFAMILIAR = 0;
            }
            else
            {


                _entidad.IDPERSONALFAMILIAR = Convert.ToInt32(id_save_fam);
            }
            _entidad.IDCODIGOGENERAL = IDCODIGOGENERAL;
            _entidad.CODIGOGENERAL = CODIGOGENERALFam;
            _entidad.TELEFONO = TELEFONOFam;
            _entidad.NOMBRES = NOMBRESFam;
            _entidad.IDVINCULO = IDVINCULOFam;
            if (IDDOCVINCFam == null)
            {
                _entidad.IDDOCVINC = "";
            }
            else
            {
                _entidad.IDDOCVINC = IDDOCVINCFam;
            }


            if (txtfecha_nacDerec != "")
            {
                try
                {
                    _entidad.fecha_nacimiento = Convert.ToDateTime(txtfecha_nacDerec);
                }
                catch (Exception ex)
                {
                    _entidad.fecha_nacimiento = Convert.ToDateTime(DateTime.Now);
                }
            }
            else
            {
                _entidad.fecha_nacimiento = Convert.ToDateTime(DateTime.Now);
            }
            _entidad.documento_adjunto = stradjunto;
            _entidad.ESTADO = "1";

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.IDPERSONALFAMILIAR == 0)
                {
                    metodo = "Personalerp/t_personal_familiarInsert";
                }
                else
                {
                    metodo = "Personalerp/t_personal_familiarUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
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
        public async Task<JsonResult> GetFamiliarDniAsync(string dnic)
        {
            List<PersonalFamiliarErp> _persona = new List<PersonalFamiliarErp>();
            PersonalFamiliarErp _busca = new PersonalFamiliarErp()
            {
                CODIGOGENERAL = dnic
            };
            _persona = await GetPersonalFamiliarCodigoErp(_busca);
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

        public async Task<List<PersonalFamiliarErp>> GetPersonalFamiliarCodigoErp(PersonalFamiliarErp busca)
        {
            List<PersonalFamiliarErp> lstEntidad = null;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_familiarSelectByFamiliar", busca);

                var model = response.Content.ReadAsAsync<List<PersonalFamiliarErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PersonalFamiliarErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        private async Task<List<TipoBajaDerechoHabienteErp>> GetTipoBajaDerechoHabientesAsync()
        {
            List<TipoBajaDerechoHabienteErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_tipo_baja_derecho_habienteSelectAll");
            var model = response.Content.ReadAsAsync<List<TipoBajaDerechoHabienteErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<TipoBajaDerechoHabienteErp>();
            }
            return lentidad;
        }
        public async Task<ActionResult> BajaDhabientes(string idcodigogeneral, int IDPERSONALFAMILIAR)
        {
            List<TipoBajaDerechoHabienteErp> _tipobaja = new List<TipoBajaDerechoHabienteErp>();
            _tipobaja = await GetTipoBajaDerechoHabientesAsync();
            TipoBajaDerechoHabienteErp nuevo = new TipoBajaDerechoHabienteErp()
            {
                IDBAJA = "",
                DESCRIPCION = "",
                ESTADO = "1"
            };
            _tipobaja.Add(nuevo);
            ViewBag.TipoBaja = _tipobaja.OrderBy(x => x.IDBAJA).ToList();

            List<PersonalFamiliarErp> _lentidad = new List<PersonalFamiliarErp>();
            PersonalFamiliarErp busca = new PersonalFamiliarErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };

            if (idcodigogeneral != "" && IDPERSONALFAMILIAR != 0)
            {
                try
                {
                    _lentidad = await GetPersonalFamiliarErp(busca);
                    if (_lentidad != null)
                    {
                        ViewBag.FamiliarExist = _lentidad.Where(x => x.IDPERSONALFAMILIAR == IDPERSONALFAMILIAR).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.FamiliarExist = null;
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.FamiliarExist = null;
                }

            }
            else
            {
                ViewBag.FamiliarExist = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> BajaFamiliar(string id_save_fam, string IDBAJA)
        {
            PersonalFamiliarErp _entidad = new PersonalFamiliarErp();

            _entidad.IDPERSONALFAMILIAR = Convert.ToInt32(id_save_fam);
            _entidad.IDBAJA = IDBAJA;
            _entidad.ESTADO = "0";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "Personalerp/t_personal_familiarBaja";

                var response = await client.PostAsJsonAsync(metodo, _entidad);
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
        public async Task<JsonResult> GuardaTrabajador(string IDDOCIDENTIDAD, string NRODOCUMENTO, string NOMBRES, string A_PATERNO, string A_MATERNO, string FECHANAC,
                    string IDESTADOCIVIL, string ASIGNACION, string AUTOGENERADOIPSS, string SNP, string AFP, string sfecha_afptra,
                    string AUTOGENERADOAFP, string licencia_auto, string licencia_moto, string otros, string RUC, string TELEFONO,
                    string CELULAR, string EMAIL, string direccion_domicilio, string DIRECCION_NUMERO, string DIRECCION_MANZANA,
                    string DIRECCION_INTERIOR, string DIRECCION_LOTE, string DIRECCION_KILOMETRO, string IDDEPARTAMENTO,
                    string IDPROVINCIA, string IDDISTRITO, string IDNIVELESTUDIO, string ESPECIALIDAD, string CENTROESTUDIOS,
                    string ANIO_EGRESO, string IDNACIONALIDAD, string SEXO,
                    string IDBANCO, string CUENTA_BANCO, string IDPERIODICIDAD, string TIPOREMUNERACION,
                    string IDBANCOCTS, string CUENTA_CTS, string PERSONAL_FOTO, string SCTRSALUD)
        {
            PersonalErp _entidad = new PersonalErp();
            int insertar = 1;
            PersonalErp _busca = new PersonalErp()
            {
                IDCODIGOGENERAL = NRODOCUMENTO
            };
            try
            {
                var existe = await GetDatosTrabajador(_busca);
                if (existe != null)
                {
                    insertar = 0;
                }
                else
                {
                    insertar = 1;
                }
            }
            catch (Exception ex)
            {
                insertar = 1;
            }
            _entidad.IDDOCIDENTIDAD = IDDOCIDENTIDAD;
            _entidad.IDCODIGOGENERAL = NRODOCUMENTO;
            _entidad.NOMBRES = NOMBRES;
            _entidad.A_PATERNO = A_PATERNO;
            _entidad.A_MATERNO = A_MATERNO;
            _entidad.FECHA_NACIMIENTO = Convert.ToDateTime(FECHANAC);
            _entidad.IDESTADOCIVIL = IDESTADOCIVIL;
            _entidad.ASIGNACION = ASIGNACION;
            _entidad.AUTOGENERADOIPSS = AUTOGENERADOIPSS;
            _entidad.SNP = SNP;
            _entidad.IDAFP = AFP;
            if (sfecha_afptra == "")
            {
                _entidad.INICIO_AFP = Convert.ToDateTime("01/01/1901");
            }
            else
            {
                _entidad.INICIO_AFP = Convert.ToDateTime(sfecha_afptra);
            }

            _entidad.AUTOGENERADOAFP = AUTOGENERADOAFP;
            _entidad.licencia_auto = licencia_auto;
            _entidad.licencia_moto = licencia_moto;
            _entidad.otros = otros;
            _entidad.RUC = RUC;
            _entidad.TELEFONO = TELEFONO;
            _entidad.CELULAR = CELULAR;
            _entidad.EMAIL = EMAIL;
            _entidad.direccion_domicilio = direccion_domicilio;
            if (DIRECCION_NUMERO == "")
            {
                _entidad.DIRECCION_NUMERO = 0;
            }
            else
            {
                try
                {
                    _entidad.DIRECCION_NUMERO = Convert.ToInt32(DIRECCION_NUMERO);
                }
                catch (Exception ex)
                {
                    _entidad.DIRECCION_NUMERO = 0;
                }
            }
            if (PERSONAL_FOTO == "")
            {
                _entidad.PERSONAL_FOTO = "default.jpg";
            }
            else
            {
                _entidad.PERSONAL_FOTO = PERSONAL_FOTO;
            }



            _entidad.DIRECCION_MANZANA = DIRECCION_MANZANA;
            _entidad.DIRECCION_INTERIOR = DIRECCION_INTERIOR;
            _entidad.DIRECCION_LOTE = DIRECCION_LOTE;
            _entidad.DIRECCION_KILOMETRO = DIRECCION_KILOMETRO;
            _entidad.IDDEPARTAMENTO = IDDEPARTAMENTO;
            _entidad.IDPROVINCIA = IDPROVINCIA;

            _entidad.IDDISTRITO = IDDISTRITO;
            _entidad.IDNIVELESTUDIO = IDNIVELESTUDIO;
            _entidad.ESPECIALIDAD = ESPECIALIDAD;
            _entidad.CENTROESTUDIOS = CENTROESTUDIOS;
            if (ANIO_EGRESO == "")
            {
                _entidad.ANIO_EGRESO = Convert.ToInt32(0);
            }
            else
            {
                _entidad.ANIO_EGRESO = Convert.ToInt32(ANIO_EGRESO);
            }

            _entidad.IDNACIONALIDAD = IDNACIONALIDAD;
            _entidad.SEXO = SEXO;
            _entidad.ESTADO = "1";

            _entidad.IDBANCO = IDBANCO;
            _entidad.CUENTA_BANCO = CUENTA_BANCO;
            _entidad.IDPERIODICIDAD = IDPERIODICIDAD;
            _entidad.TIPOREMUNERACION = TIPOREMUNERACION;
            _entidad.IDBANCOCTS = IDBANCOCTS;
            _entidad.CUENTA_CTS = CUENTA_CTS;
            _entidad.SCTRSALUD = SCTRSALUD;

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (insertar == 1)
                {
                    metodo = "Personalerp/t_personal_generalInsert";
                }
                else
                {
                    metodo = "Personalerp/t_personal_generalUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null)
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
        public async Task<JsonResult> GetTrabajadorDniAsync(string dnic)
        {
            PersonalErp _persona = new PersonalErp();
            PersonalErp _busca = new PersonalErp()
            {
                IDCODIGOGENERAL = dnic
            };
            _persona = await GetDatosTrabajador(_busca);
            string finded = "0";
            if (_persona != null)
            {
                try
                {
                    if (_persona.ESTADO == "1")
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


        public async Task<ActionResult> Banco()
        {
            lbanco = null;
            try
            {
                lbanco = await GetBancoErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lbanco);
        }
        private async Task<List<PeriodicidadSueldoErp>> GetPeriodicidadSueldoErp()
        {
            List<PeriodicidadSueldoErp> xentidad = new List<PeriodicidadSueldoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_periodicidad_sueldoSelectAll");
                var model = response.Content.ReadAsAsync<List<PeriodicidadSueldoErp>>();
                if (model.Result.Count > 0)
                {
                    xentidad = model.Result.ToList();
                }
                else
                {
                    xentidad = new List<PeriodicidadSueldoErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return xentidad;
        }
        public async Task<List<BancoErp>> GetBancoErp()
        {
            List<BancoErp> xlbanco = new List<BancoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_bancoSelectAll");
                var model = response.Content.ReadAsAsync<List<BancoErp>>();
                if (model.Result.Count > 0)
                {
                    xlbanco = model.Result.ToList();
                }
                else
                {
                    xlbanco = new List<BancoErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlbanco;
        }

        public async Task<ActionResult> RegistroBanco(string id_entidad)
        {
            BancoErp _entidad = new BancoErp();

            if (id_entidad != "")
            {
                id_entidad = id_entidad.Trim();
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_entidad);
                    var bancos = await GetBancoErp();
                    if (bancos != null && bancos.Any())
                    {
                        _entidad = bancos.Where(x => x.id_banco == identidad).FirstOrDefault();
                    }

                    if (_entidad != null)
                    {
                        ViewBag.Entidad = _entidad;
                    }
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    ViewBag.Entidad = null;
                }

            }
            else
            {
                ViewBag.Entidad = null;
            }
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> GetBancoExist(string idbanco)
        {
            List<BancoErp> _entidad = new List<BancoErp>();
            BancoErp _busca = null;
            _entidad = await GetBancoErp();
            int finded = 0;
            if (_entidad != null && _entidad.Any())
            {
                _busca = _entidad.Where(x => x.idbanco.Trim().Equals(idbanco)).FirstOrDefault();
                try
                {
                    if (_busca != null)
                    {
                        finded = _busca.id_banco;
                    }
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(finded, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GuardaBanco(string id_banco, string idbanco, string descripcion, string codigo_sunat, string direccion, string telefono)
        {
            int iid_banco = Convert.ToInt32(id_banco);
            BancoErp _entidad = new BancoErp()
            {
                id_banco = iid_banco,
                idbanco = idbanco,
                descripcion = descripcion,
                codigo_sunat = codigo_sunat,
                direccion = direccion,
                telefono = telefono,
                estado = "1"
            };
            string idinserted = "";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (iid_banco == 0)
                {
                    metodo = "Personalerp/t_bancoInsert";
                }
                else
                {
                    metodo = "Personalerp/t_bancoUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
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
        public async Task<JsonResult> EliminaBanco(int idbanco)
        {
            List<BancoErp> _lentidad = new List<BancoErp>();
            BancoErp _entidad = new BancoErp();

            BancoErp busca = new BancoErp()
            {
                id_banco = idbanco
            };

            try
            {
                _lentidad = await GetBancoErp();
                if (_lentidad != null)
                {
                    _entidad = _lentidad.Where(x => x.id_banco == idbanco).FirstOrDefault();
                }
                else
                {
                    _entidad = null;
                }
            }
            catch (Exception ex)
            {
                _entidad = null;
            }


            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_bancoDelete";
                    var response = await client.PostAsJsonAsync(metodo, busca);
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

        public async Task<ActionResult> ListDataRegistroCuentas(string NRODOCUMENTO)
        {
            //string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftpderechohabiente"];
            // ViewBag.UrlLink = virtualdir;
            string msgReturn = "";
            List<PersonalCuentaErp> lstEntidad = null;
            PersonalCuentaErp busca = new PersonalCuentaErp() { IDCODIGOGENERAL = NRODOCUMENTO.Trim() };

            try
            {
                lstEntidad = await GetPersonalCuentasErp(busca);
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstEntidad);
        }
        public async Task<List<PersonalCuentaErp>> GetPersonalCuentasErp(PersonalCuentaErp busca)
        {
            List<PersonalCuentaErp> lstEntidad = null;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_cuentaSelectByCodigo", busca);

                var model = response.Content.ReadAsAsync<List<PersonalCuentaErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PersonalCuentaErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public async Task<ActionResult> ListDataRegistroOtosDoc(string NRODOCUMENTO)
        {
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftpotrosdocs"];
            ViewBag.UrlLink = virtualdir;
            string msgReturn = "";
            List<PersonalOtrosDocumentosErp> lstEntidad = null;
            PersonalOtrosDocumentosErp busca = new PersonalOtrosDocumentosErp() { IDCODIGOGENERAL = NRODOCUMENTO.Trim() };

            try
            {
                lstEntidad = await GetPersonalOtrosDocErp(busca);
                if (lstEntidad != null)
                {
                    lstEntidad = lstEntidad.ToList();
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrio un error al intentar abrir la ventana";
                Response.StatusCode = 500;
                return Json(msgReturn, JsonRequestBehavior.AllowGet);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstEntidad);
        }
        public async Task<List<PersonalOtrosDocumentosErp>> GetPersonalOtrosDocErp(PersonalOtrosDocumentosErp busca)
        {
            List<PersonalOtrosDocumentosErp> lstEntidad = null;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_otrosdocSelectByCodigo", busca);

                var model = response.Content.ReadAsAsync<List<PersonalOtrosDocumentosErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PersonalOtrosDocumentosErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> EditOtrosDocs(string idcodigogeneral, int IDPERSONALOTROSDOC)
        {
            List<OtrosDocumentosErp> _lotrosdocs = new List<OtrosDocumentosErp>();
            _lotrosdocs = await GetOtrodDocsAsync();
            OtrosDocumentosErp nuevo = new OtrosDocumentosErp()
            {
                IDOTROSDOC = 0,
                DESCRIPCION = ""
            };
            _lotrosdocs.Add(nuevo);
            ViewBag.OtrosDocs = _lotrosdocs.OrderBy(x => x.IDOTROSDOC).ToList();

            List<PersonalOtrosDocumentosErp> _lentidad = new List<PersonalOtrosDocumentosErp>();
            PersonalOtrosDocumentosErp busca = new PersonalOtrosDocumentosErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };

            if (idcodigogeneral != "" && IDPERSONALOTROSDOC != 0)
            {
                try
                {
                    _lentidad = await GetPersonalOtrosDocErp(busca);
                    if (_lentidad != null)
                    {
                        ViewBag.OtrosDocExist = _lentidad.Where(x => x.IDPERSONALOTROSDOC == IDPERSONALOTROSDOC).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.OtrosDocExist = null;
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.OtrosDocExist = null;
                }

            }
            else
            {
                ViewBag.OtrosDocExist = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        private async Task<List<OtrosDocumentosErp>> GetOtrodDocsAsync()
        {
            List<OtrosDocumentosErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_otros_documentosSelectAll");
            var model = response.Content.ReadAsAsync<List<OtrosDocumentosErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<OtrosDocumentosErp>();
            }
            return lentidad;
        }

        [HttpPost]
        public JsonResult GuardarAdjuntoOtros()
        {
            string urlftp = "";
            try
            {
                string idCodigogeneral = "";
                string folder = System.Configuration.ConfigurationManager.AppSettings["Ftpotrosdocs"];
                idCodigogeneral = ControllerContext.HttpContext.Request.Form["fidcodigogeneral"];
                HttpFileCollectionBase files = Request.Files;
                if (files.Count == 2)
                {
                    HttpPostedFileBase file = files[0];
                    idCodigogeneral = files.AllKeys[1].ToString();
                    urlftp = UploadFtp(file, idCodigogeneral, folder, "otros");
                    if (urlftp != "")
                    {
                        return Json(urlftp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(urlftp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GuardaOtrosDocs(string id_save_otro, string IDCODIGOGENERAL, string IDOTROSDOCP, string DOCUMENTO_ADJUNTO)
        {
            PersonalOtrosDocumentosErp _entidad = new PersonalOtrosDocumentosErp();
            if (id_save_otro == "0")
            {
                _entidad.IDPERSONALOTROSDOC = 0;
            }
            else
            {


                _entidad.IDPERSONALOTROSDOC = Convert.ToInt32(id_save_otro);
            }
            _entidad.IDCODIGOGENERAL = IDCODIGOGENERAL;
            _entidad.IDOTROSDOC = Convert.ToInt32(IDOTROSDOCP);
            _entidad.DOCUMENTO_ADJUNTO = DOCUMENTO_ADJUNTO;
            _entidad.ESTADO = "1";

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.IDPERSONALOTROSDOC == 0)
                {
                    metodo = "Personalerp/t_personal_otrosdocInsert";
                }
                else
                {
                    metodo = "Personalerp/t_personal_otrosdocUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
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
        public async Task<JsonResult> EliminaOtrosDocs(string idcodigogeneral, int IDPERSONALOTROSDOC)
        {
            List<PersonalOtrosDocumentosErp> _lentidad = new List<PersonalOtrosDocumentosErp>();
            PersonalOtrosDocumentosErp _entidad = new PersonalOtrosDocumentosErp();

            PersonalOtrosDocumentosErp busca = new PersonalOtrosDocumentosErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };

            if (idcodigogeneral != "" && IDPERSONALOTROSDOC != 0)
            {
                try
                {
                    _lentidad = await GetPersonalOtrosDocErp(busca);
                    if (_lentidad != null)
                    {
                        _entidad = _lentidad.Where(x => x.IDPERSONALOTROSDOC == IDPERSONALOTROSDOC).FirstOrDefault();
                    }
                    else
                    {
                        _entidad = null;
                    }
                }
                catch (Exception ex)
                {
                    _entidad = null;
                }
            }
            else
            {
                _entidad = null;
            }

            string idinserted = "0";
            if (_entidad != null)
            {
                try
                {
                    PersonalOtrosDocumentosErp id = new PersonalOtrosDocumentosErp { IDPERSONALOTROSDOC = _entidad.IDPERSONALOTROSDOC };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_personal_otrosdocDelete";
                    var response = await client.PostAsJsonAsync(metodo, id);
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
        public ActionResult Download()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "Personal.xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }
        public async Task<ActionResult> ExcelExportPersonal()
        {
            List<PersonalErp> FileData = null;
            FileData = await GetTrabajadorAll();
            if (FileData != null)
            {
                FileData = FileData.ToList();
            }

            if (FileData == null || !FileData.Any())
            {
                return null;
            }
            else
            {
                try
                {
                    DataTable Dt = new DataTable();
                    //Dt = new DataTable();
                    Dt.Columns.Add("NRO", typeof(Int32));
                    Dt.Columns.Add("TIPO DOCUMENTO", typeof(string));
                    Dt.Columns.Add("DOCUMENTO", typeof(string));
                    Dt.Columns.Add("A. PATERNO", typeof(string));
                    Dt.Columns.Add("A. MATERNO", typeof(string));
                    Dt.Columns.Add("NOMBRES", typeof(string));
                    Dt.Columns.Add("SEXO", typeof(string));
                    Dt.Columns.Add("AFP", typeof(string));
                    Dt.Columns.Add("ESTADO CIVIL", typeof(string));
                    Dt.Columns.Add("NIVEL ESTUDIOS", typeof(string));
                    Dt.Columns.Add("NACIONALIDAD", typeof(string));
                    Dt.Columns.Add("ESTADO", typeof(string));
                    foreach (var data in FileData)
                    {
                        DataRow row = Dt.NewRow();
                        row[0] = data.ITEM;
                        row[1] = data.DOCIDENTIDAD;
                        row[2] = data.NRODOCUMENTO;
                        row[3] = data.A_PATERNO;
                        row[4] = data.A_MATERNO;
                        row[5] = data.NOMBRES;
                        row[6] = data.SEXO;
                        row[7] = data.AFP;
                        row[8] = data.ESTADOCIVIL;
                        row[9] = data.NIVELESTUDIO;
                        row[10] = data.NACIONALIDAD;
                        row[11] = data.NEstado;
                        Dt.Rows.Add(row);

                    }


                    var memoryStream = new MemoryStream();
                    using (var excelPackage = new ExcelPackage(memoryStream))
                    {

                        var worksheet = excelPackage.Workbook.Worksheets.Add("Personal ");
                        using (ExcelRange Rng = worksheet.Cells["A2:L2"])
                        {
                            Rng.Value = "REPORTE DE PERSONAL ";
                            Rng.Merge = true;
                            Rng.Style.Font.Size = 16;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Italic = true;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        using (ExcelRange Rng = worksheet.Cells["A3:AN1000"])
                        {
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            Rng.Style.Font.Size = 10;
                        }
                        worksheet.Cells["A4"].LoadFromDataTable(Dt, true, TableStyles.None);
                        worksheet.Cells["A4:L4"].Style.Font.Bold = true;
                        worksheet.DefaultRowHeight = 15;



                        worksheet.DefaultColWidth = 30;
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                        worksheet.Column(1).Width = 9;
                        worksheet.Column(2).Width = 16;
                        worksheet.Column(3).Width = 30;

                        Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }

        }

        //public async Task<ActionResult> RegistroTipoContrato(string id_entidad)
        //{
        //    BancoErp _entidad = new BancoErp();

        //    if (id_entidad != "")
        //    {
        //        id_entidad = id_entidad.Trim();
        //        int identidad = 0;
        //        try
        //        {
        //            identidad = Convert.ToInt32(id_entidad);
        //            var bancos = await GetBancoErp();
        //            if (bancos != null && bancos.Any())
        //            {
        //                _entidad = bancos.Where(x => x.id_banco == identidad).FirstOrDefault();
        //            }

        //            if (_entidad != null)
        //            {
        //                ViewBag.Entidad = _entidad;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            identidad = 0;
        //            ViewBag.Entidad = null;
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.Entidad = null;
        //    }
        //    return PartialView();
        //}

        public async Task<ActionResult> Concepto()
        {
            List<ConceptoErp> _lista = new List<ConceptoErp>();
            try
            {
                _lista = await GetConceptosErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }


        public async Task<List<ConceptoErp>> GetConceptosErp()
        {
            List<ConceptoErp> xlbanco = new List<ConceptoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_conceptoSelectAll");
                var model = response.Content.ReadAsAsync<List<ConceptoErp>>();
                if (model.Result.Count > 0)
                {
                    xlbanco = model.Result.ToList();
                }
                else
                {
                    xlbanco = new List<ConceptoErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlbanco;
        }

        public async Task<ActionResult> TipoConcepto()
        {
            List<TipoConceptoErp> _lista = new List<TipoConceptoErp>();
            try
            {
                _lista = await GetTipoConceptoErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }


        public async Task<List<TipoConceptoErp>> GetTipoConceptoErp()
        {
            List<TipoConceptoErp> xlbanco = new List<TipoConceptoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_tipo_conceptoSelectAll");
                var model = response.Content.ReadAsAsync<List<TipoConceptoErp>>();
                if (model.Result.Count > 0)
                {
                    xlbanco = model.Result.ToList();
                }
                else
                {
                    xlbanco = new List<TipoConceptoErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlbanco;
        }

        public async Task<ActionResult> PlanillaErp()
        {
            List<PlanillaErp> _lista = new List<PlanillaErp>();
            try
            {
                _lista = await GetPlanillaErp();
            }
            catch (Exception)
            {
                _lista = new List<PlanillaErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }

        private async Task<List<PlanillaErp>> GetPlanillaErp()
        {
            List<PlanillaErp> xentidad = new List<PlanillaErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_planillaSelectAll");
                var model = response.Content.ReadAsAsync<List<PlanillaErp>>();
                if (model.Result.Count > 0)
                {
                    xentidad = model.Result.ToList();
                }
                else
                {
                    xentidad = new List<PlanillaErp>();
                }
            }
            catch (Exception ex)
            {
                return new List<PlanillaErp>();
            }
            return xentidad;
        }

        public async Task<ActionResult> Variables()
        {
            List<VariableErp> _lista = new List<VariableErp>();
            try
            {
                _lista = await GetVariableErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }

        private async Task<List<VariableErp>> GetVariableErp()
        {
            List<VariableErp> xlvariable = new List<VariableErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_variable_detalleSelectAll");
                var model = response.Content.ReadAsAsync<List<VariableErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<VariableErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }

        public async Task<ActionResult> RegistroVariable(string id_save, string idplanilla)
        {
            List<VariableErp> _lentidad = new List<VariableErp>();
            VariableErp _entidad = new VariableErp();
            List<PlanillaErp> _lplanilla = new List<PlanillaErp>();
            PlanillaErp _entidadplanilla = new PlanillaErp()
            {
                IDPLANILLA = "",
                DESCRIPCION = "",
                ESTADO = "1"
            };

            _lplanilla = await GetPlanillaErp();
            _lplanilla.Add(_entidadplanilla);
            ViewBag.Planillas = _lplanilla.OrderBy(z => z.IDPLANILLA).ToList();


            VariableErp _entidadvar = new VariableErp()
            {
                IDVARIABLE = "",
                DESCRIPCION = "",
                ESTADO = "1"
            };

            _lentidad = await GetVariablesErpAll();
            _lentidad.Add(_entidadvar);
            ViewBag.Variables = _lentidad.OrderBy(z => z.IDVARIABLE).ToList();

            if (id_save != "" && idplanilla != "")
            {
                try
                {
                    _entidad = await GetVariableDetalle(id_save, idplanilla);
                    if (_entidad != null)
                    {
                        ViewBag.VariableDetalle = _entidad;
                    }
                    else
                    {
                        ViewBag.VariableDetalle = null;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.VariableDetalle = null;
                }

            }
            else
            {
                ViewBag.VariableDetalle = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }

        private async Task<VariableErp> GetVariableDetalle(string id_save, string idplanilla)
        {
            VariableErp entidad = null;
            VariableErp busca = new VariableErp()
            {
                IDVARIABLE = id_save,
                IDPLANILLA = idplanilla
            };

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_variable_detalleSelectId", busca);

                var model = response.Content.ReadAsAsync<List<VariableErp>>();
                if (model.Result.Count > 0)
                {
                    var lista = model.Result.ToList();
                    entidad = lista[0];
                }
                else
                {
                    entidad = new VariableErp();
                }
            }
            catch (Exception ex)
            {
                return new VariableErp();
            }
            return entidad;
        }

        private async Task<List<VariableErp>> GetVariablesErpAll()
        {
            List<VariableErp> xlvariable = new List<VariableErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_variableSelectAll");
                var model = response.Content.ReadAsAsync<List<VariableErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<VariableErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaDetalleVariable(string IDPLANILLA, string IDVARIABLE, decimal VALOR,
            string FECHAINI, string FECHAFIN, string IDPLANILLAOLD, string IDVARIABLEOLD)
        {
            VariableErp _entidad = new VariableErp();

            _entidad.IDVARIABLE = IDVARIABLE;
            _entidad.IDPLANILLA = IDPLANILLA;
            _entidad.VALOR = Convert.ToDecimal(VALOR);
            _entidad.FECHA_INICIO = Convert.ToDateTime(FECHAINI);
            _entidad.FECHA_FINAL = Convert.ToDateTime(FECHAFIN);
            _entidad.IDPLANILLAOLD = IDPLANILLAOLD;
            _entidad.IDVARIABLEOLD = IDVARIABLEOLD;
            _entidad.ESTADO = "1";
            string metodo = "";
            int idinserted = 0;

            if (IDPLANILLAOLD.Trim() == "" && IDVARIABLEOLD.Trim() == "")
            {
                metodo = "Personalerp/t_variable_detalleInsert";
            }
            else
            {
                metodo = "Personalerp/t_variable_detalleUpdate";
            }

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var model = response.Content.ReadAsAsync<List<VariableErp>>();
                if (model.Result.Count > 0)
                {
                    var lista = model.Result.ToList();
                    var valorx = lista[0].VALOR;
                    if (valorx == VALOR)
                    {
                        idinserted = 1;
                    }
                    else
                    {
                        idinserted = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TipoTrabajador()
        {
            List<TipoTrabajadorErp> _lista = new List<TipoTrabajadorErp>();
            try
            {
                _lista = await GetTipoTrabajador();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }

        private async Task<List<TipoTrabajadorErp>> GetTipoTrabajador()
        {
            List<TipoTrabajadorErp> xlvariable = new List<TipoTrabajadorErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_tipo_trabajadorSelectAll");
                var model = response.Content.ReadAsAsync<List<TipoTrabajadorErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<TipoTrabajadorErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }

        public async Task<ActionResult> PersonalCargo()
        {
            List<CargosErp> _lista = new List<CargosErp>();
            try
            {
                _lista = await GetCargosErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }
        public async Task<ActionResult> Cargos()
        {
            List<CargosErp> _lista = new List<CargosErp>();
            try
            {
                _lista = await GetCargosErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }
        private async Task<List<CargosErp>> GetCargosErp()
        {
            List<CargosErp> xlvariable = new List<CargosErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_cargosSelectAll");
                var model = response.Content.ReadAsAsync<List<CargosErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<CargosErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }
        private async Task<List<PersonalCargoErp>> GetPersonalCargo()
        {
            List<PersonalCargoErp> xlvariable = new List<PersonalCargoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_cargoSelectAll");
                var model = response.Content.ReadAsAsync<List<PersonalCargoErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<PersonalCargoErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }

        public async Task<ActionResult> RegistroContrato(string id_save)
        {
            PersonalContratoErp _entidad = new PersonalContratoErp();

            if (id_save != "")
            {
                try
                {
                    var lista = await GetPersonalContratoErpId(Convert.ToInt32(id_save));
                    if (lista != null)
                    {
                        _entidad = lista;
                        ViewBag.PersonalContrato = _entidad;
                    }
                    else
                    {
                        ViewBag.PersonalContrato = null;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.PersonalContrato = null;
                }

            }
            else
            {
                ViewBag.PersonalContrato = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }
        public async Task<ActionResult> RegistroCargo(string id_save)
        {
            CargosErp _entidad = new CargosErp();

            if (id_save != "")
            {
                try
                {
                    var lista = await GetCargosErp();
                    if (lista.Any())
                    {
                        _entidad = lista.FirstOrDefault(z => z.IDCARGO.Equals(id_save));
                        ViewBag.PersonalCargo = _entidad;
                    }
                    else
                    {
                        ViewBag.PersonalCargo = null;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.PersonalCargo = null;
                }

            }
            else
            {
                ViewBag.PersonalCargo = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }



        [HttpPost]
        public async Task<JsonResult> GuardaCargo(string IDCARGO, string DESCRIPCION)
        {
            CargosErp _entidad = new CargosErp();

            _entidad.IDCARGO = IDCARGO.Trim();
            _entidad.DESCRIPCION = DESCRIPCION;
            _entidad.ESTADO = "1";
            string metodo = "";
            int idinserted = 0;

            if (IDCARGO.Trim() == "")
            {
                metodo = "Personalerp/t_cargosInsert";
            }
            else
            {
                metodo = "Personalerp/t_cargosUpdate";
            }

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var model = response.Content.ReadAsAsync<List<CargosErp>>();
                if (model.Result.Count > 0)
                {
                    var lista = model.Result.ToList();
                    var valorx = lista[0].IDCARGO;
                    if (valorx != "")
                    {
                        idinserted = 1;
                    }
                    else
                    {
                        idinserted = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EliminaCargo(string idpersonalcargo)
        {
            List<CargosErp> _lentidad = new List<CargosErp>();
            CargosErp _entidad = new CargosErp();

            CargosErp busca = new CargosErp()
            {
                IDCARGO = idpersonalcargo
            };




            int idinserted = 0;
            if (idpersonalcargo != "")
            {
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_cargosDelete";
                    var response = await client.PostAsJsonAsync(metodo, busca);
                    var model = response.Content.ReadAsAsync<List<CargosErp>>();
                    if (model.Result.Count > 0)
                    {
                        var lista = model.Result.ToList();
                        var valorx = lista[0].IDCARGO;
                        if (valorx != "")
                        {
                            idinserted = 1;
                        }
                        else
                        {
                            idinserted = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Contratos()
        {
            List<PersonalContratoErp> _lista = new List<PersonalContratoErp>();
            try
            {
                _lista = await GetPersonalContratoErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista);
        }

        private async Task<List<PersonalContratoErp>> GetPersonalContratoErp()
        {
            List<PersonalContratoErp> xlvariable = new List<PersonalContratoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_contratoSelectAll");
                var model = response.Content.ReadAsAsync<List<PersonalContratoErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<PersonalContratoErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }



        private async Task<List<TipoContratoErp>> GetTipoContrato()
        {
            List<TipoContratoErp> xlvariable = new List<TipoContratoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_tipo_contratoSelectAll");
                var model = response.Content.ReadAsAsync<List<TipoContratoErp>>();
                if (model.Result.Count > 0)
                {
                    xlvariable = model.Result.ToList();
                }
                else
                {
                    xlvariable = new List<TipoContratoErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlvariable;
        }

        private async Task<PersonalContratoErp> GetPersonalContratoErpId(int id_save)
        {
            PersonalContratoErp busca = new PersonalContratoErp()
            {
                id_contrato = id_save
            };
            List<PersonalContratoErp> lstEntidad = new List<PersonalContratoErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_contratoSelect", busca);

                var model = response.Content.ReadAsAsync<List<PersonalContratoErp>>();
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
        private async Task<List<PersonalContratoErp>> GetPersonalContratoErpCodigo(string id_save)
        {
            PersonalContratoErp busca = new PersonalContratoErp()
            {
                IDCODIGOGENERAL = id_save
            };
            List<PersonalContratoErp> lstEntidad = new List<PersonalContratoErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_contratoSelectIDCODIGO", busca);

                var model = response.Content.ReadAsAsync<List<PersonalContratoErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad;
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

        [HttpPost]
        public async Task<JsonResult> GuardaContrato(string id_save,string idx, string sfecha_inicio, string sfecha_fin, string sfecha_firma, string cmbfk_tipocontrato,
                            string cmbfk_planilla, string cmbfk_servicio, string txtbasico, string txtbono, string txtmovilidad)
        {
            string IDCODIGOX = Session["DNI"].ToString().Trim();
            int idcontrato = Convert.ToInt32(id_save.Trim());
            int proyectoid  = Convert.ToInt32(cmbfk_servicio);
            decimal basico = Convert.ToDecimal(txtbasico);
            decimal bono = 0;
            decimal movilidad = 0;
            if (!txtbono.Trim().Equals(""))
            {
                bono = Convert.ToDecimal(txtbono);
            }
            if (!txtmovilidad.Trim().Equals(""))
            {
                movilidad = Convert.ToDecimal(txtmovilidad);
            }

            DateTime fecha_inicio = Convert.ToDateTime(sfecha_inicio);
            DateTime fecha_fin = Convert.ToDateTime(sfecha_fin);
            DateTime fecha_firma = Convert.ToDateTime(sfecha_firma);
            int days = Convert.ToInt32(((fecha_fin - fecha_inicio).TotalDays));
            string idtipocontrato = cmbfk_tipocontrato.Trim();
            string idplanilla = cmbfk_planilla;

            PersonalContratoErp _entidad = new PersonalContratoErp();
            _entidad.fk_proyecto = proyectoid;
            _entidad.IDCODIGOGENERAL = idx.Trim();
            _entidad.ESTADO = "1";
            _entidad.BASICO = basico;
            _entidad.BONIFICACION = bono;
            _entidad.MOVILIDAD = movilidad;
            _entidad.OTROS = 0;
            _entidad.DURACION = days;
            _entidad.PERIODO_PRUEBA = 0;
            _entidad.INICIO_CONTRATO = fecha_inicio;
            _entidad.FINAL_CONTRATO = fecha_fin;
            _entidad.IDTIPOCONTRATO = idtipocontrato;
            _entidad.FIRMA_CONTRATO = fecha_firma;
            _entidad.FIRMA_FISICA = "0";
            _entidad.IDPLANILLA = idplanilla;
            _entidad.CODIGOGENERALREGISTRO = IDCODIGOX;
            string metodo = "";
            int idinserted = 0;

            if (idcontrato == 0)
            {
                metodo = "Personalerp/t_personal_contratoInsert";
            }
            else
            {
                metodo = "Personalerp/t_personal_contratoUpdate";
            }

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }

            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> NuevosContratos()
        {
            return View();
        }

        public async Task<ActionResult> PersonalHorarios()
        {
            List<PersonalHorarioErp> _lista = new List<PersonalHorarioErp>();
            try
            {
                _lista = await GetPersonalHorarioErp();
            }
            catch (Exception)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_lista.OrderBy(x => x.lugar).ToList());
        }

        private async Task<List<PersonalHorarioErp>> GetPersonalHorarioErp()
        {
            List<PersonalHorarioErp> lentidad = new List<PersonalHorarioErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_horarioSelectAll");
                var model = response.Content.ReadAsAsync<List<PersonalHorarioErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.ToList();
                }
                else
                {
                    lentidad = new List<PersonalHorarioErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return lentidad;
        }
        CompraErpController compraCtrl = new CompraErpController();
        public async Task<ActionResult> RegistroPersonalHorario(string id_entidad)
        {
            List<PersonalErp> _lentidadpersonal = new List<PersonalErp>();
            PersonalErp _entidadpersonal = new PersonalErp()
            {
                IDCODIGOGENERAL = "",
                NOMBRES = "",
                A_PATERNO = "",
                A_MATERNO = "",
                NOMBRES_FULL = "",
                ESTADO = "1"
            };

            _lentidadpersonal = await GetPersonalErpSinHorario();
            _lentidadpersonal.Add(_entidadpersonal);
            ViewBag.Personal = _lentidadpersonal.OrderBy(z => z.NOMBRES_FULL).ToList();

            List<ProyectoErp> _lentidadproyecto = new List<ProyectoErp>();
            ProyectoErp _entidadproyecto = new ProyectoErp()
            {
                id_proyecto = 0,
                codigo = "",
                fk_empresa = 0,
                descripcion = "",
                observacion = "",
                estado = "1"
            };

            _lentidadproyecto = await compraCtrl.GetProyectoErp();
            _lentidadproyecto.Add(_entidadproyecto);
            ViewBag.Proyectos = _lentidadproyecto.OrderBy(z => z.descripcion).ToList();


            PersonalHorarioErp _entidad = new PersonalHorarioErp();

            if (id_entidad != "0")
            {
                id_entidad = id_entidad.Trim();
                int identidad = 0;
                try
                {
                    identidad = Convert.ToInt32(id_entidad);
                    var existe = await GetHorarioPersonalId(identidad);
                    if (existe != null)
                    {
                        ViewBag.Entidad = existe;
                    }
                    _lentidadpersonal = await compraCtrl.GetPersonalErp();
                }
                catch (Exception ex)
                {
                    identidad = 0;
                    _lentidadpersonal = await GetPersonalErpSinHorario();
                    ViewBag.Entidad = null;
                }

            }
            else
            {
                _lentidadpersonal = await GetPersonalErpSinHorario();
                ViewBag.Entidad = null;
            }


            _lentidadpersonal.Add(_entidadpersonal);
            ViewBag.Personal = _lentidadpersonal.OrderBy(z => z.NOMBRES_FULL).ToList();
            return PartialView();
        }
        public async Task<List<PersonalErp>> GetPersonalErpSinHorario()
        {
            List<PersonalErp> lempresa = null;
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_generalSelectNoHorario");
                var model = response.Content.ReadAsAsync<List<PersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lempresa = model.Result.Where(x => x.ESTADO.Equals("1")).ToList();
                }
                else
                {
                    lempresa = new List<PersonalErp>();
                }
            }
            catch (Exception e)
            {
                lempresa = new List<PersonalErp>();
            }

            return lempresa;
        }
        private async Task<PersonalHorarioErp> GetHorarioPersonalId(int identidad)
        {
            PersonalHorarioErp busca = new PersonalHorarioErp()
            {
                id_personal_horario = identidad
            };
            List<PersonalHorarioErp> lstEntidad = new List<PersonalHorarioErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_horarioSelect", busca);

                var model = response.Content.ReadAsAsync<List<PersonalHorarioErp>>();
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

        [HttpPost]
        public async Task<JsonResult> GuardaHorario(string id_save_horario, string fk_proyecto, string lugar,
            string IDPERSONAL, string nombres, string lunes, string martes, string miercoles, string jueves, string viernes,
            string sabado, string horas_mes)
        {
            PersonalHorarioErp _entidad = new PersonalHorarioErp();
            if (id_save_horario == "0")
            {
                _entidad.id_personal_horario = 0;
            }
            else
            {


                _entidad.id_personal_horario = Convert.ToInt32(id_save_horario);
            }
            _entidad.fk_proyecto = Convert.ToInt32(fk_proyecto);
            _entidad.lugar = lugar;
            _entidad.IDPERSONAL = IDPERSONAL;
            _entidad.nombres = nombres;
            _entidad.lunes = Convert.ToDecimal(lunes);
            _entidad.martes = Convert.ToDecimal(martes);
            _entidad.miercoles = Convert.ToDecimal(miercoles);
            _entidad.jueves = Convert.ToDecimal(jueves);
            _entidad.viernes = Convert.ToDecimal(viernes);
            _entidad.sabado = Convert.ToDecimal(sabado);
            _entidad.horas_mes = Convert.ToDecimal(horas_mes);

            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.id_personal_horario == 0)
                {
                    metodo = "Personalerp/t_personal_horarioInsert";
                }
                else
                {
                    metodo = "Personalerp/t_personal_horarioUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _entidad);
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

        #region SedeLaboral
        public async Task<ActionResult> SedeLaboral()
        {
            List<SedeLaboralErp> lstEntidad = null;
            lstEntidad = await GetSedeLaboralErpAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.ToList();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lstEntidad);
        }
        private async Task<List<SedeLaboralErp>> GetSedeLaboralErpAll()
        {
            List<SedeLaboralErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_sede_laboralSelectAll");
                var model = response.Content.ReadAsAsync<List<SedeLaboralErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.OrderBy(x => x.id_sede_laboral).ToList();
                }
                else
                {
                    lstEntidad = new List<SedeLaboralErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return lstEntidad;
        }
        public async Task<ActionResult> RegistroSede(string id_sede_laboral)
        {
            SedeLaboralErp _entidad = new SedeLaboralErp();
            int idbusca = 0;
            if (id_sede_laboral != "0")
            {
                int idempre = 0;
                try
                {
                    idbusca = Convert.ToInt32(id_sede_laboral);
                    var lentidad = await GetSedeLaboralErpAll();
                    if (lentidad.Any())
                    {
                        _entidad = lentidad.Where(x => x.id_sede_laboral == idbusca).FirstOrDefault();
                        if (_entidad != null)
                        {
                            idempre = idbusca;
                            ViewBag.Sedes = _entidad;
                        }
                        else
                        {
                            idempre = 0;
                            ViewBag.Sedes = null;
                        }

                    }
                    else
                    {
                        idempre = 0;
                        ViewBag.Sedes = null;
                    }
                }
                catch (Exception ex)
                {
                    idempre = 0;
                    ViewBag.Sedes = null;
                }

            }
            else
            {
                ViewBag.Sedes = null;
            }

            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> GuardaSede(string id_sede_laboral, string abreviatura, string descripcion, string direccion)
        {
            SedeLaboralErp _sede = new SedeLaboralErp();
            if (id_sede_laboral == "0")
            {
                _sede.id_sede_laboral = 0;
            }
            else
            {
                _sede.id_sede_laboral = Convert.ToInt32(id_sede_laboral);
            }
            _sede.abreviatura = abreviatura;
            _sede.descripcion = descripcion;
            _sede.direccion = direccion;
            _sede.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_sede.id_sede_laboral == 0)
                {
                    metodo = "Personalerp/t_sede_laboralInsert";
                }
                else
                {
                    metodo = "Personalerp/t_sede_laboralUpdate";
                }
                var response = await client.PostAsJsonAsync(metodo, _sede);
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
        #endregion


        #region Prestamos
        public async Task<ActionResult> GetTrabajadorPrestamos(string IdTrabajador)
        {
            PersonalPrestamo busca = new PersonalPrestamo() { IDCODIGOGENERAL = IdTrabajador };
            List<PersonalPrestamo> entidad = new List<PersonalPrestamo>();

            if (IdTrabajador != "")
            {
                entidad = await GetPrestamosTrabajador(busca);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.IdTrabajador = IdTrabajador;
            return View(entidad);
        }
        public async Task<ActionResult> GetTrabajadorContratos(string IdTrabajador)
        {
            //PersonalContratoErp busca = new PersonalContratoErp() { IDCODIGOGENERAL = IdTrabajador };
            List<PersonalContratoErp> entidad = new List<PersonalContratoErp>();
            int tieneactivo = 0;
            if (IdTrabajador != "")
            {
                entidad = await GetPersonalContratoErpCodigo(IdTrabajador);
            }
            if (entidad == null)
            {
                entidad = new List<PersonalContratoErp>();
            }
            else
            {
                tieneactivo = entidad.Where(x => x.ESTADO.Equals("1")).Count();
            }
            PersonalErp fpersona = new PersonalErp()
            {
                IDCODIGOGENERAL = IdTrabajador.Trim()
            };
            var persona = await GetDatosTrabajador(fpersona);
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.IdTrabajador = IdTrabajador;
            ViewBag.TieneActivo = tieneactivo.ToString();
            ViewBag.Personal = IdTrabajador + " - " + persona.NOMBRES_FULL;
            return View(entidad);
        }
        //public async Task<ActionResult> RegistroPersonalContrato(int id_save)
        //{

        //    PersonalContratoErp _entidad = new PersonalContratoErp();

        //    List<PersonalErp> _lpersonal = new List<PersonalErp>();
        //    _lpersonal = await GetTrabajadorAll();
        //    PersonalErp newadd = new PersonalErp()
        //    {
        //        IDCODIGOGENERAL = "",
        //        NOMBRES = "",
        //        A_PATERNO = "",
        //        A_MATERNO = ""
        //    };
        //    _lpersonal.Add(newadd);
        //    ViewBag.Personal = _lpersonal.OrderBy(x => x.IDCODIGOGENERAL).ToList();

        //    List<TipoContratoErp> _ltipocontrato = new List<TipoContratoErp>();
        //    _ltipocontrato = await GetTipoContrato();
        //    TipoContratoErp newtipocontrato = new TipoContratoErp()
        //    {
        //        IDTIPOCONTRATO = "",
        //        IDTPCONTRATO = "",
        //        DESCRIPCION = "",
        //        ESTADO = "1"
        //    };
        //    _ltipocontrato.Add(newtipocontrato);
        //    ViewBag.TipoContrato = _ltipocontrato.OrderBy(x => x.IDTIPOCONTRATO).ToList();

        //    if (id_save != 0)
        //    {
        //        try
        //        {
        //            _entidad = await GetPersonalContratoErpId(id_save);
        //            if (_entidad != null)
        //            {
        //                ViewBag.PersonalContrato = _entidad;
        //            }
        //            else
        //            {
        //                ViewBag.PersonalContrato = null;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.PersonalContrato = null;
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.PersonalContrato = null;
        //    }
        //    return PartialView();
        //}

        public async Task<List<ProyectoErp>> GetProyectoErp()
        {
            List<ProyectoErp> lproyecto = null;
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Servicioerp/t_proyectoSelectAll");
                var model = response.Content.ReadAsAsync<List<ProyectoErp>>();
                if (model.Result.Count > 0)
                {
                    lproyecto = model.Result.Where(x => x.estado.Equals("1")).ToList();
                }
                else
                {
                    lproyecto = new List<ProyectoErp>();
                }
            }
            catch (Exception e)
            {
                lproyecto = new List<ProyectoErp>();
            }

            return lproyecto;
        }

        public async Task<ActionResult> RegistroPersonalContrato(string IDCodigoGeneral)
        {
            List<PersonalContratoErp> _percontrato = new List<PersonalContratoErp>();
            ViewBag.DNI = IDCodigoGeneral;
            PersonalErp fpersona = new PersonalErp()
            {
                IDCODIGOGENERAL = IDCodigoGeneral.Trim()
            };
            PersonalErp persona = new PersonalErp();

            List<TipoContratoErp> _ltipocontrato = new List<TipoContratoErp>();
            _ltipocontrato = await GetTiposContrato();
            TipoContratoErp newtipocontrato = new TipoContratoErp()
            {
                IDTIPOCONTRATO = "",
                IDTPCONTRATO = "",
                DESCRIPCION = "",
                ESTADO = "1"
            };
            _ltipocontrato.Add(newtipocontrato);
            ViewBag.TipoContrato = _ltipocontrato.OrderBy(x => x.IDTPCONTRATO).ToList();

            List<PlanillaErp> _lplanillas = new List<PlanillaErp>();
            _lplanillas = await GetPlanillaErp();
            PlanillaErp planilladd = new PlanillaErp()
            {
                IDPLANILLA = "",
                DESCRIPCION = ""
            };
            _lplanillas.Add(planilladd);
            ViewBag.Planillas = _lplanillas.OrderBy(x => x.IDPLANILLA).ToList();

            List<ProyectoErp> _lproyectos = new List<ProyectoErp>();
            _lproyectos = await GetProyectoErp();
            ProyectoErp proyectoadd = new ProyectoErp()
            {
                id_proyecto = 0,
                descripcion=""
            };
            _lproyectos.Add(proyectoadd);
            ViewBag.Servicios = _lproyectos.OrderBy(x => x.id_proyecto).ToList();

            try
            {
                persona = await GetDatosTrabajador(fpersona);
            }
            catch (Exception ex)
            {
                persona = null;
            }
            ViewBag.Persona = persona;
            return PartialView();
        }
        private async Task<PersonalPrestamoDetalle> SetRegistroPrestamoDetalle(PersonalPrestamoDetalle busca)
        {
            List<PersonalPrestamoDetalle> lstEntidad = new List<PersonalPrestamoDetalle>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_prestamo_detalleUpdateEstado", busca);

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad[0];
                }
                else
                {
                    return new PersonalPrestamoDetalle();
                }
            }
            catch (Exception ex)
            {
                return new PersonalPrestamoDetalle();
            }
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarDescuentoPrestamo(int id_personal_prestamo_detalle)
        {
            List<PersonalPrestamoDetalle> _lentidad = new List<PersonalPrestamoDetalle>();
            PersonalPrestamoDetalle _entidad = null;
            int idinserted = 0;
            PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
            {
                id_personal_prestamo_detalle = id_personal_prestamo_detalle,
                estado = "2"
            };
            var registrado = await SetRegistroPrestamoDetalle(busca);
            if (registrado != null)
            {
                if (registrado.estado.Equals("2"))
                {
                    idinserted = registrado.id_personal_prestamo_detalle;
                }
            }
            else
            {
                return Json(idinserted, JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        private async Task<List<PersonalPrestamo>> GetPrestamosTrabajador(PersonalPrestamo busca)
        {
            List<PersonalPrestamo> lstEntidad = new List<PersonalPrestamo>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_prestamoSelectIDCODIGOGENERAL", busca);

                var model = response.Content.ReadAsAsync<List<PersonalPrestamo>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad;
                }
                else
                {
                    return lstEntidad;
                }
            }
            catch (Exception ex)
            {
                return lstEntidad;
            }
        }
        //ComercialController comercon = new ComercialController();

        public async Task<ActionResult> RegistroPrestamoById(string id_personal_prestamo)
        {
            string shortname = System.Configuration.ConfigurationManager.AppSettings["ShortName"];
            int id_personal_prestamox = Convert.ToInt32(id_personal_prestamo);
            PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
            {
                id_personal_prestamo = id_personal_prestamox
            };
            var datos = await GetDetallePrestamo(busca);
            ViewBag.Datos = datos;

            List<TipoCheckErp> _ltipos = new List<TipoCheckErp>();
            TipoCheckErp tiponew = new TipoCheckErp()
            {
                id_tipo_check = 0,
                descripcion = ""
            };
            _ltipos = await GetTipoCheckErp();
            _ltipos.Add(tiponew);
            ViewBag.Tipos = _ltipos.Where(x => !x.descripcion.Equals("CUOTAS DIA HABIL")).OrderBy(x => x.id_tipo_check).ToList();


            return PartialView();
        }

        public async Task<ActionResult> RegistroPrestamo(string idcodigogeneral)
        {
            string shortname = System.Configuration.ConfigurationManager.AppSettings["ShortName"];
            PersonalErp busca = new PersonalErp()
            {
                IDCODIGOGENERAL = idcodigogeneral
            };
            var datos = await GetDatosTrabajador(busca);
            ViewBag.Datos = datos;

            List<TipoCheckErp> _ltipos = new List<TipoCheckErp>();
            TipoCheckErp tiponew = new TipoCheckErp()
            {
                id_tipo_check = 0,
                descripcion = ""
            };
            _ltipos = await GetTipoCheckErp();
            _ltipos.Add(tiponew);
            ViewBag.Tipos = _ltipos.Where(x => !x.descripcion.Equals("CUOTAS DIA HABIL")).OrderBy(x => x.id_tipo_check).ToList();


            return PartialView();
        }
        public async Task<ActionResult> GetPrestamoById(string id_personal_prestamo)
        {
            string shortname = System.Configuration.ConfigurationManager.AppSettings["ShortName"];
            int xid_personal_prestamo = Convert.ToInt32(id_personal_prestamo);
            PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
            {
                id_personal_prestamo = xid_personal_prestamo
            };
            var datos = await GetDetallePrestamo(busca);
            ViewBag.Datos = datos;

            return PartialView();
        }
        private async Task<List<PersonalPrestamoDetalle>> GetAllDetallePrestamo(string busca)
        {
            List<PersonalPrestamoDetalle> lstEntidad = new List<PersonalPrestamoDetalle>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_prestamo_detalleSelectAll");

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.Where(x => x.IDCODIGOGENERAL.Equals(busca)).ToList();
                    if (lstEntidad.Any())
                    {
                        return lstEntidad;
                    }
                    else
                    {
                        return null;
                    }
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

        private async Task<List<PersonalPrestamoDetalle>> GetAllDetallePrestamoPeriodo(string busca)
        {
            List<PersonalPrestamoDetalle> lstEntidad = new List<PersonalPrestamoDetalle>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_prestamo_detalleSelectAll");

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.Where(x => x.PERIODO.Equals(busca)).ToList();
                    if (lstEntidad.Any())
                    {
                        return lstEntidad;
                    }
                    else
                    {
                        return new List<PersonalPrestamoDetalle>();
                    }
                }
                else
                {
                    return new List<PersonalPrestamoDetalle>();
                }
            }
            catch (Exception ex)
            {
                return new List<PersonalPrestamoDetalle>();
            }
        }
        private async Task<List<PersonalPrestamoDetalle>> GetAllDetallePrestamoPeriodoID(string buscas)
        {
            List<PersonalPrestamoDetalle> lstEntidad = new List<PersonalPrestamoDetalle>();
            PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
            {
                PERIODO = buscas
            };
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_prestamo_detalleSelectPeriodo", busca);

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad;
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

        private async Task<List<PersonalPrestamoDetalle>> GetDetallePrestamoPersona(PersonalPrestamoDetalle busca)
        {
            List<PersonalPrestamoDetalle> lstEntidad = new List<PersonalPrestamoDetalle>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_prestamo_detalleSelectPersona", busca);

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad;
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

        [HttpPost]
        public async Task<JsonResult> EliminaDetallePrestamo(string id_personal_prestamo_detalle)
        {
            int xid_personal_prestamo_detalle = Convert.ToInt32(id_personal_prestamo_detalle);
            List<PersonalPrestamoDetalle> _lentidad = new List<PersonalPrestamoDetalle>();
            PersonalPrestamoDetalle _entidad = new PersonalPrestamoDetalle();
            PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
            {
                id_personal_prestamo_detalle = xid_personal_prestamo_detalle
            };

            try
            {
                _entidad = await GetDetallePrestamoID(busca);
            }
            catch (Exception ex)
            {
                _entidad = null;
            }


            string idinserted = "0";
            if (_entidad != null)
            {
                PersonalPrestamoDetalle updated = new PersonalPrestamoDetalle()
                {
                    id_personal_prestamo_detalle = xid_personal_prestamo_detalle,
                    estado = "0"
                };
                var actualizado = await SetRegistroPrestamoDetalle(updated);
                if (actualizado != null)
                {
                    string estadonew = actualizado.estado;
                    if (estadonew == "0")
                    {
                        idinserted = "1";
                    }
                    else
                    {
                        idinserted = "0";
                    }
                }
                else
                {
                    idinserted = "0";
                }
            }
            else
            {
                idinserted = "0";
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }
        private async Task<PersonalPrestamoDetalle> GetDetallePrestamoID(PersonalPrestamoDetalle busca)
        {
            PersonalPrestamoDetalle lstEntidad = new PersonalPrestamoDetalle();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_prestamo_detalleSelectID", busca);

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.FirstOrDefault();
                    return lstEntidad;
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
        private async Task<List<PersonalPrestamoDetalle>> GetDetallePrestamo(PersonalPrestamoDetalle busca)
        {
            List<PersonalPrestamoDetalle> lstEntidad = new List<PersonalPrestamoDetalle>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_personal_prestamo_detalleSelectFk", busca);

                var model = response.Content.ReadAsAsync<List<PersonalPrestamoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                    return lstEntidad;
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
        string databases = System.Configuration.ConfigurationManager.AppSettings["BaseDatos"];
        string datauser = System.Configuration.ConfigurationManager.AppSettings["BaseUser"];
        string dataclave = System.Configuration.ConfigurationManager.AppSettings["BaseClave"];
        string dataurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];

        public async Task<ActionResult> ExportResumen(string IdTrabajador)
        {
            List<PersonalPrestamoDetalle> listdetalles = new List<PersonalPrestamoDetalle>();
            try
            {
                string dni = "";
                int idi = 0;
                string xIdTrabajador = IdTrabajador.PadLeft(8, '0');
                PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
                {
                    IDCODIGOGENERAL = xIdTrabajador.Trim()
                };
                listdetalles = await GetDetallePrestamoPersona(busca);
                if (listdetalles.Any())
                {
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;
                        //idi = Convert.ToInt32(listdetalles[0].id_personal_prestamo);
                        dni = listdetalles[0].IDCODIGOGENERAL;
                        string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrResumenPrestamosDni.rpt")));
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetParameterValue("@IDCODIGOGENERAL", xIdTrabajador.Trim());
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\ResumenPrestamo-" + dni + ".pdf");
                        rd.Close();
                        rd.Dispose();
                    }
                    ViewBag.Printer = "ResumenPrestamo-" + dni;
                    ViewBag.Codigo = "ResumenPrestamo-" + dni + ".pdf";
                }
                else
                {
                    ViewBag.Printer = "No se encontro informacion para mostrar!";
                    ViewBag.Codigo = "";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar!";
                ViewBag.Codigo = "";
            }
            return View();
        }


        public async Task<ActionResult> ExportCompromiso(string id_personal_prestamo)
        {
            List<PersonalPrestamoDetalle> listdetalles = new List<PersonalPrestamoDetalle>();
            try
            {
                int xid_personal_prestamo = Convert.ToInt32(id_personal_prestamo);
                string dni = "";
                int idi = 0;
                PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
                {
                    id_personal_prestamo = xid_personal_prestamo
                };
                listdetalles = await GetDetallePrestamo(busca);
                if (listdetalles.Any())
                {
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;
                        idi = Convert.ToInt32(listdetalles[0].id_personal_prestamo);
                        dni = listdetalles[0].IDCODIGOGENERAL;
                        string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrCompromisoPrestamo.rpt")));
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetParameterValue("@id_personal_prestamo", idi);
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\Compromiso-" + dni + "-" + idi.ToString() + ".pdf");
                        rd.Close();
                        rd.Dispose();
                    }
                    ViewBag.Printer = "Compromiso-" + dni + "-" + idi.ToString();
                    ViewBag.Codigo = "Compromiso-" + dni + "-" + idi.ToString() + ".pdf";
                }
                else
                {
                    ViewBag.Printer = "No se encontro informacion para mostrar!";
                    ViewBag.Codigo = "";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar!";
                ViewBag.Codigo = "";
            }
            return View();
        }

        public async Task<List<TipoCheckErp>> GetTipoCheckErp()
        {
            List<TipoCheckErp> xlTipoCheckErp = new List<TipoCheckErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Obligacioneserp/t_tipo_checkSelectAll");
                var model = response.Content.ReadAsAsync<List<TipoCheckErp>>();
                if (model.Result.Count > 0)
                {
                    xlTipoCheckErp = model.Result.ToList();
                }
                else
                {
                    xlTipoCheckErp = new List<TipoCheckErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlTipoCheckErp;
        }

        [HttpPost]
        public async Task<JsonResult> GuardaPrestamo(string id_save, string monto, string nro_letras,
                                                        string fecha_inicio, List<List<string>> arrLetras,
                                                        string fk_tipo_check, string idpersonalprestamo)
        {
            string f_vencimiento = "";
            decimal xmonto = Convert.ToDecimal(monto);
            int xnro_letras = Convert.ToInt32(nro_letras);
            DateTime xfecha_inicio = Convert.ToDateTime(fecha_inicio);
            if (arrLetras != null)
            {
                foreach (var oneLetrasArr in arrLetras)
                {
                    if (Convert.ToInt32(oneLetrasArr[0]) == xnro_letras)
                    {
                        f_vencimiento = oneLetrasArr[3];
                    }
                }
            }


            string FkUsua = Session["IdUsuario"].ToString().Trim();
            PersonalPrestamo _entidad = new PersonalPrestamo();
            _entidad.id_personal_prestamo = Convert.ToInt32(idpersonalprestamo);
            _entidad.IDCODIGOGENERAL = id_save.PadLeft(8, '0');
            _entidad.FECHAINICIO = xfecha_inicio;
            _entidad.FECHAFIN = Convert.ToDateTime(f_vencimiento);
            _entidad.ESTADO = "1";
            _entidad.MONTOTOTAL = xmonto;
            _entidad.CUOTAS = xnro_letras;
            _entidad.fk_tipo_check = Convert.ToInt32(fk_tipo_check);
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (Convert.ToInt32(idpersonalprestamo) > 0)
                {
                    metodo = "Personalerp/t_personal_prestamoUpdate";
                }
                else
                {
                    metodo = "Personalerp/t_personal_prestamoInsert";
                }

                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = respuesta.Result.ToString();
                    if (Convert.ToInt32(idinserted) > 0)
                    {
                        if (arrLetras != null)
                        {
                            if (Convert.ToInt32(idpersonalprestamo) > 0)
                            {
                                PersonalPrestamoDetalle busca = new PersonalPrestamoDetalle()
                                {
                                    id_personal_prestamo = Convert.ToInt32(idpersonalprestamo)
                                };
                                var listado = await GetDetallePrestamo(busca);
                                foreach (var itemxx in listado)
                                {
                                    PersonalPrestamoDetalle _dentidad = new PersonalPrestamoDetalle()
                                    {
                                        id_personal_prestamo_detalle = itemxx.id_personal_prestamo_detalle,
                                        estado = "0"
                                    };
                                    var eliminado = await SetRegistroPrestamoDetalle(_dentidad);
                                }
                            }
                            foreach (var oneLetrasArr in arrLetras)
                            {
                                int idinserted2 = 0;

                                try
                                {
                                    PersonalPrestamoDetalle _dentidad = new PersonalPrestamoDetalle()
                                    {
                                        id_personal_prestamo = Convert.ToInt32(idinserted),
                                        nrocuota = Convert.ToInt32(oneLetrasArr[0]),
                                        montodescuento = Convert.ToDecimal(oneLetrasArr[2]),
                                        FECHADESCUENTO = Convert.ToDateTime(oneLetrasArr[3]),
                                        estado = "1"
                                    };
                                    var client2 = new HttpClient();
                                    string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                                    client2.BaseAddress = new Uri(connectionInfo2);
                                    client2.DefaultRequestHeaders.Accept.Clear();
                                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    string metodo2 = "";
                                    metodo2 = "Personalerp/t_personal_prestamo_detalleInsert";
                                    var response2 = await client2.PostAsJsonAsync(metodo2, _dentidad);
                                    var respuesta2 = response2.Content.ReadAsAsync<string>();
                                    if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                                    {
                                        idinserted2 = Convert.ToInt32(respuesta2.Result.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(idinserted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ExportDescuentosPeriodo()
        {
            List<PersonalPrestamoDetalle> listdetalles = new List<PersonalPrestamoDetalle>();
            try
            {
                string dni = "";
                int idi = 0;
                string xperiodo = DateTime.Now.ToString("yyyyMM");
                int diax = DateTime.Now.Day;
                if (diax <= 15)
                {
                    xperiodo = xperiodo + "I";
                }
                else
                {
                    xperiodo = xperiodo + "II";
                }
                listdetalles = await GetAllDetallePrestamoPeriodoID(xperiodo);
                if (listdetalles.Any())
                {
                    using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
                    {
                        ConnectionInfo cnnInfo = new ConnectionInfo();
                        cnnInfo.ServerName = dataurl;
                        cnnInfo.DatabaseName = databases;
                        cnnInfo.UserID = datauser;
                        cnnInfo.Password = dataclave;
                        cnnInfo.Type = ConnectionInfoType.SQL;
                        cnnInfo.IntegratedSecurity = false;
                        //idi = Convert.ToInt32(listdetalles[0].id_personal_prestamo);
                        //dni = listdetalles[0].IDCODIGOGENERAL;
                        string appPath = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/img/Reportes/CrPrestamosPeriodo.rpt")));
                        rd.DataSourceConnections[0].SetConnection(dataurl, databases, datauser, dataclave);
                        rd.SetParameterValue("@PERIODO", xperiodo);
                        Directory.CreateDirectory(@"C:\reportesotros");
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\reportesotros\PrestamosPeriodo-" + xperiodo + ".pdf");
                        rd.Close();
                        rd.Dispose();
                    }
                    ViewBag.Printer = "PrestamosPeriodo-" + xperiodo;
                    ViewBag.Codigo = "PrestamosPeriodo-" + xperiodo + ".pdf";
                }
                else
                {
                    ViewBag.Printer = "No se encontro informacion para mostrar!";
                    ViewBag.Codigo = "";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Printer = "No se encontro informacion para mostrar!";
                ViewBag.Codigo = "";
            }
            return View();
        }

        #endregion

        #region Covid
        public async Task<ActionResult> Seguimiento()
        {
            List<PersonalErp> lstEntidad = null;
            lstEntidad = await GetTrabajadorSeguimientoAll();
            if (lstEntidad != null)
            {
                lstEntidad = lstEntidad.ToList();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lstEntidad.OrderByDescending(x=>x.seguimientos).ToList());
        }
        private async Task<List<PersonalErp>> GetTrabajadorSeguimientoAll()
        {
            List<PersonalErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("seguimiento/t_personal_generalSelectSeguimiento");
                var model = response.Content.ReadAsAsync<List<PersonalErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<PersonalErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return lstEntidad;
        }


        public async Task<ActionResult> DetallesSeguimiento(string IDCODIGOGENERAL)
        {
            SeguimientoErp _seguimientos = new SeguimientoErp();
            List<SeguimientoErp> _lseguimientos = new List<SeguimientoErp>();
            if (IDCODIGOGENERAL != "")
            {
                try
                {
                    ViewBag.IDCODIGOGENERAL = IDCODIGOGENERAL;
                    var xentidad = await GetSeguimientoId(IDCODIGOGENERAL);
                    if (xentidad != null && xentidad.Any())
                    {
                        _lseguimientos = xentidad.OrderByDescending(x => x.fecha_registro).ToList();
                    }
                }
                catch (Exception ex)
                {
                    IDCODIGOGENERAL = "";
                    ViewBag.IDCODIGOGENERAL = IDCODIGOGENERAL;
                }
            }
            else
            {
                IDCODIGOGENERAL = "";
                ViewBag.IDCODIGOGENERAL = IDCODIGOGENERAL;
            }
            return PartialView(_lseguimientos);
        }

        private async Task<List<SeguimientoErp>> GetSeguimientoId(string iDCODIGOGENERAL)
        {
            List<SeguimientoErp> _lseguimientos = null;
            SeguimientoErp busca = new SeguimientoErp { IDCODIGOGENERAL = iDCODIGOGENERAL };
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Seguimiento/t_seguimientoSelectWeb", busca);
                var model = response.Content.ReadAsAsync<List<SeguimientoErp>>();
                if (model.Result.Count > 0)
                {
                    _lseguimientos = model.Result.ToList();
                }
                else
                {
                    _lseguimientos = new List<SeguimientoErp>();
                }
            }
            catch (Exception e)
            {
                _lseguimientos = new List<SeguimientoErp>();
            }
            return _lseguimientos;
        }

        //t_seguimientoSelect

        #endregion
    }
}


