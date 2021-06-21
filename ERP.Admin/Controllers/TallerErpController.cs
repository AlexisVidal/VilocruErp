using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ERP.Admin.App_Start;
using ERP.Admin.Models;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class TallerErpController : Controller
    {
        // GET: TallerErp
        public async Task<ActionResult> Vehiculos()
        {
            List<VehiculoErp> lstEntidad = new List<VehiculoErp>();
            try
            {
                lstEntidad = await GetVehiculoErpAll();
            }
            catch (Exception e)
            {
                lstEntidad = new List<VehiculoErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftpvehiculos"];
            ViewBag.UrlLink = virtualdir;
            return View(lstEntidad);
        }

        private async Task<List<VehiculoErp>> GetVehiculoErpAll()
        {
            List<VehiculoErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Tallererp/t_vehiculoSelectAll");
                var model = response.Content.ReadAsAsync<List<VehiculoErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<VehiculoErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        public async Task<ActionResult> RegistroVehiculo(string id_save)
        {
            string pattt = HttpContext.Server.MapPath(("~/img/vehiculos/") + "defaultimage.jpg").ToString();
            ViewBag.Patth = pattt;
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"];
            string urldir = System.Configuration.ConfigurationManager.AppSettings["UrlProduccion"];

            string UrlLink = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftpvehiculos"];
            ViewBag.UrlLink = UrlLink;


            ViewBag.VirtualDir = virtualdir;
            List<VehiculoErp> _lentidad = new List<VehiculoErp>();

            VehiculoErp _entidad = new VehiculoErp();

            List<EquipoErp> _lentidadequipo = new List<EquipoErp>();
            EquipoErp _entidadequipo = new EquipoErp()
            {
                id_equipo = 0,
                descripcion = "",
                estado = "1"
            };

            _lentidadequipo = await GetEquipos();
            _lentidadequipo.Add(_entidadequipo);
            ViewBag.Equipo = _lentidadequipo.OrderBy(z => z.id_equipo).ToList();

            List<VehiculoMarcaErp> _lentidadmarca = new List<VehiculoMarcaErp>();
            VehiculoMarcaErp _entidadmarca = new VehiculoMarcaErp()
            {
                id_vehiculo_marca = 0,
                descripcion = "",
                estado = "1"
            };

            _lentidadmarca = await GetVehiculoMarcaErp();
            _lentidadmarca.Add(_entidadmarca);
            ViewBag.Marcas = _lentidadmarca.OrderBy(z => z.descripcion).ToList();

            List<CarroceriaErp> _lentidadcarroceria = new List<CarroceriaErp>();
            CarroceriaErp _entidadcarroceria = new CarroceriaErp()
            {
                id_carroceria = 0,
                descripcion = "",
                estado = "1"
            };

            _lentidadcarroceria = await GetCarroceriaErp();
            _lentidadcarroceria.Add(_entidadcarroceria);
            ViewBag.Carrocerias = _lentidadcarroceria.OrderBy(z => z.descripcion).ToList();

            int id_savei = 0;
            if (id_save != "0")
            {

                try
                {
                    id_savei = Convert.ToInt32(id_save);
                    _lentidad = await GetVehiculoErpAll();
                    if (_lentidad != null && _lentidad.Any())
                    {
                        _entidad = _lentidad.FirstOrDefault(x => x.id_vehiculo == id_savei);
                        ViewBag.Vehiculo = _entidad;
                        string foto = _entidad.image_url;
                        if (foto.Contains("~/img/vehiculos/defaultimage.jpg"))
                        {
                            ViewBag.ImagenModel = "/" + urldir + "/img/vehiculos/defaultimage.jpg";
                        }
                        else
                        {
                            string prelink = _entidad.image_url.Replace("~", "").Trim();
                            int indexquitar = prelink.IndexOf("/vehiculos/");
                            prelink = prelink.Substring(indexquitar + 11);
                            ViewBag.ImagenModel = UrlLink + "/" + prelink;
                        }
                        
                    }
                    else
                    {
                        ViewBag.Vehiculo = null;
                        _entidad = new VehiculoErp();
                        ViewBag.ImagenModel = "/"+ urldir+"/img/vehiculos/defaultimage.jpg";
                    }
                }
                catch (Exception ex)
                {
                    id_savei = 0;
                    ViewBag.Vehiculo = null;
                    _entidad = new VehiculoErp();
                    ViewBag.ImagenModel = "/" + urldir + "/img/vehiculos/defaultimage.jpg";
                }
            }
            else
            {
                ViewBag.Vehiculo = null;
                _entidad = new VehiculoErp();
                ViewBag.ImagenModel = "/corcrusac/img/vehiculos/defaultimage.jpg";
            }

            //string imagenmodel = HttpContext.Server.MapPath(("~/img/vehiculos/") + _entidad.image_url.ToString().Replace("~", ""));
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(_entidad);
        }
        [HttpPost]
        public async Task<JsonResult> GuardaVehiculo(int id_vehiculo, int fk_equipo, int fk_tipo_equipo, string codigo, int fk_vehiculo_marca,
            string modelo, string serie, string placa, string nro_motor, string combustible, string potencia, int anio_fabricacion,
        int fk_carroceria, int pasajeros, int ruedas, decimal peso_bruto, decimal peso_neto, decimal longitud, decimal altura,
        decimal ancho, int ejes, int asientos, int cilindros, decimal cilindrada, decimal carga_util, string strfotografia)
        {
            VehiculoErp _entidad = new VehiculoErp();
            int insertar;
            if (id_vehiculo == 0)
            {
                insertar = 0;
            }
            else
            {
                insertar = 1;
            }

            _entidad.id_vehiculo = id_vehiculo;
            _entidad.fk_equipo = fk_equipo;
            _entidad.fk_tipo_equipo = fk_tipo_equipo;
            _entidad.codigo = codigo;
            _entidad.fk_vehiculo_marca = fk_vehiculo_marca;
            _entidad.modelo = modelo;
            _entidad.serie = serie;
            _entidad.placa = placa;
            _entidad.nro_motor = nro_motor;
            _entidad.combustible = combustible;
            _entidad.potencia = potencia;
            _entidad.anio_fabricacion = anio_fabricacion;
            _entidad.fk_carroceria = fk_carroceria;
            _entidad.pasajeros = pasajeros;
            _entidad.ruedas = ruedas;
            _entidad.peso_bruto = peso_bruto;
            _entidad.peso_neto = peso_neto;
            _entidad.longitud = longitud;
            _entidad.altura = altura;
            _entidad.ancho = ancho;
            _entidad.ejes = ejes;
            _entidad.asientos = asientos;
            _entidad.cilindros = cilindros;
            _entidad.cilindrada = cilindrada;
            _entidad.carga_util = carga_util;
            _entidad.image_url = strfotografia;
            _entidad.IDUSUARIO = Session["IdUsuario"].ToString().Trim();
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (insertar == 0)
                {
                    metodo = "Tallererp/t_vehiculoInsert";
                }
                else
                {
                    metodo = "Tallererp/t_vehiculoUpdate";
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
        private async Task<List<VehiculoMarcaErp>> GetVehiculoMarcaErp()
        {
            List<VehiculoMarcaErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Tallererp/t_vehiculo_marcaSelectAll");
                var model = response.Content.ReadAsAsync<List<VehiculoMarcaErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<VehiculoMarcaErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        private async Task<List<CarroceriaErp>> GetCarroceriaErp()
        {
            List<CarroceriaErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Tallererp/t_carroceriaSelectAll");
                var model = response.Content.ReadAsAsync<List<CarroceriaErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CarroceriaErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        private async Task<List<ProductoMarca>> GetProductoMarcas()
        {
            List<ProductoMarca> lproductomarca = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("ProductoMarca/all");
                var model = response.Content.ReadAsAsync<List<ProductoMarca>>();
                if (model.Result.Count > 0)
                {
                    lproductomarca = model.Result.ToList();
                }
                else
                {
                    lproductomarca = new List<ProductoMarca>();
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return lproductomarca;
        }

        private async Task<List<EquipoErp>> GetEquipos()
        {
            List<EquipoErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Tallererp/t_equipoSelectAll");
                var model = response.Content.ReadAsAsync<List<EquipoErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<EquipoErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        [HttpPost]
        public async Task<JsonResult> Filter_TipoEquipo(int idequipo)
        {
            List<EquipoTipoErp> todos = new List<EquipoTipoErp>();
            List<EquipoTipoErp> lstEntidad = new List<EquipoTipoErp>();
            try
            {
                todos = await GetEquipoTiposAll();
                if (todos.Any())
                {
                    lstEntidad = todos.Where(x => x.fk_equipo == idequipo).ToList();
                }
            }
            catch (Exception e)
            {
                lstEntidad = new List<EquipoTipoErp>();
            }
            return Json(lstEntidad, JsonRequestBehavior.AllowGet);
        }

        private async Task<List<EquipoTipoErp>> GetEquipoTiposAll()
        {
            List<EquipoTipoErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Tallererp/t_tipo_equipoSelectAll");
                var model = response.Content.ReadAsAsync<List<EquipoTipoErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<EquipoTipoErp>();
                }
            }
            catch (Exception ex)
            {
                return new List<EquipoTipoErp>();
            }
            return lstEntidad;
        }

        [HttpPost]
        public JsonResult GuardarImagenVehiculo()
        {
            string urlftp = "";
            try
            {
                string fcodigo = "";

                fcodigo = ControllerContext.HttpContext.Request.Form["fcodigo"];
                var foldervehiculos = System.Configuration.ConfigurationManager.AppSettings["Ftpvehiculos"];
                HttpFileCollectionBase files = Request.Files;
                if (files.Count % 2 == 0)
                {
                    int i = 0;
                    i = files.Count / 2;
                    if (i == 1)
                    {
                        i = 0;
                    }
                    HttpPostedFileBase file = files[0];
                    fcodigo = files.AllKeys[1].ToString();
                    urlftp = UploadFtp2(file, fcodigo, foldervehiculos, fcodigo);
                    if (urlftp != "")
                    {
                        //return Json(file.FileName, JsonRequestBehavior.AllowGet);
                        return Json(urlftp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("");
                    }
                    //}




                    //HttpFileCollectionBase files = Request.Files;
                    //if (files.Count % 2 == 0)
                    //{
                    //    int i = 0;
                    //    i = files.Count / 2;
                    //    if (i==1)
                    //    {
                    //        i = 0;
                    //    }
                    //    HttpPostedFileBase file = files[i];

                    //    if (file != null)
                    //    {
                    //        string pattt = HttpContext.Server.MapPath(("~/img/vehiculos/") + file.FileName).ToString();
                    //        file.SaveAs(HttpContext.Server.MapPath(("~/img/vehiculos/") + file.FileName));
                    //    }
                    //    return Json(file.FileName, JsonRequestBehavior.AllowGet);

                    //}

                    
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //return Json(urlftp, JsonRequestBehavior.AllowGet);
        }

        private string UploadFtp(HttpPostedFileBase file, string fcodigo, string folder, string nombrenormal)
        {
            try
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath(("~/img/vehiculos/") + file.FileName));
                }
                //var uploadurl = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
                //string uploadfilename = "";
                //if (nombrenormal == "")
                //{
                //    uploadfilename = fcodigo.ToString().PadLeft(4, '0') + System.IO.Path.GetExtension(file.FileName); // file.FileName;
                //}
                //else
                //{
                //    uploadfilename = file.FileName.ToString()/* + System.IO.Path.GetExtension(file.FileName)*/; // file.FileName;
                //}

                //var username = System.Configuration.ConfigurationManager.AppSettings["Ftpuser"];
                //var password = System.Configuration.ConfigurationManager.AppSettings["Ftppass"];


                //Stream streamObj = file.InputStream;
                //byte[] buffer = new byte[file.ContentLength];
                //streamObj.Read(buffer, 0, buffer.Length);
                //streamObj.Close();
                //streamObj = null;
                //string ftpurl = String.Format("{0}/{1}", uploadurl + folder, uploadfilename);
                //var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                //requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                //requestObj.Credentials = new NetworkCredential(username, password);
                //Stream requestStream = requestObj.GetRequestStream();
                //requestStream.Write(buffer, 0, buffer.Length);
                //requestStream.Flush();
                //requestStream.Close();
                //requestObj = null;
                //return ftpurl;
                return file.FileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string UploadFtp2(HttpPostedFileBase file, string fcodigo, string folder, string nombrenormal)
        {
            try
            {
                var uploadurl = System.Configuration.ConfigurationManager.AppSettings["Ftpstring"];
                string uploadfilename = "";
                if (nombrenormal == "")
                {
                    uploadfilename = fcodigo + System.IO.Path.GetExtension(file.FileName); // file.FileName;
                }
                else
                {
                    uploadfilename = nombrenormal + "-" + file.FileName.ToString(); // file.FileName;
                }

                var username = System.Configuration.ConfigurationManager.AppSettings["Ftpuser"];
                var password = System.Configuration.ConfigurationManager.AppSettings["Ftppass"];
                string ftpurlfolder = String.Format("{0}/{1}", uploadurl + folder + "/" + fcodigo, "");

                bool result = FtpDirectoryExists(ftpurlfolder, username, password);


                Stream streamObj = file.InputStream;
                byte[] buffer = new byte[file.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                string ftpurl = String.Format("{0}/{1}", uploadurl + folder + "/" + fcodigo, uploadfilename);
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

        private bool FtpDirectoryExists(string directoryPath, string ftpUser, string ftpPassword)
        {
            bool IsExists = false;
            try
            {
                if (WebRequestMethods.Ftp.ListDirectoryDetails.Contains(directoryPath))
                {
                    IsExists = true;
                }
                else
                {
                    FtpWebRequest reques = (FtpWebRequest)(WebRequest.Create(directoryPath));
                    reques.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                    reques.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reques.KeepAlive = false;
                    try
                    {
                        FtpWebResponse response = (FtpWebResponse)reques.GetResponse();
                        response.Close();
                    }
                    catch (Exception e)
                    {
                        reques.Abort();
                        IsExists = false;
                    }
                    reques.Abort();
                    IsExists = true;
                }

            }
            catch (Exception ex)
            {
                IsExists = false;
            }
            return IsExists;
        }
    }
}