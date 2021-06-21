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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using X.PagedList;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class TareoController : Controller
    {
        // GET: Tareo
        public async Task<ActionResult> PlanillaAsistencia2(int? page, string cmbCompVent_FilterPeriodo = "")
        {
            string quincex = "";
            try
            {
                quincex = cmbCompVent_FilterPeriodo.Substring(6);
            }
            catch (Exception e)
            {

            }
            ViewBag.QUINCEX = quincex;
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();

            string periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
            int dia = DateTime.Now.Day;
            if (dia <= 15)
            {
                periodo = periodo + "I";
            }
            else
            {
                periodo = periodo + "II";
            }
            ViewBag.Periodo = periodo;

            List<PeriodoErp> _lperiodos = new List<PeriodoErp>();
            PeriodoErp docid = new PeriodoErp()
            {
                IDPERIODO = "",
                PERIODO = "",
                ANIO = ""
            };
            _lperiodos = await GePeriodos();
            _lperiodos.Add(docid);
            ViewBag.Periodos = _lperiodos.OrderBy(x => x.IDPERIODO).ToList();
            List<AsistenciaTempErp> lstEntidad = null;
            if (cmbCompVent_FilterPeriodo != "")
            {

                AsistenciaTempErp busca = new AsistenciaTempErp()
                {
                    PERIODO = cmbCompVent_FilterPeriodo
                };

                lstEntidad = await GetPlanillon(busca);
                if (lstEntidad == null || !lstEntidad.Any())
                {
                    lstEntidad = new List<AsistenciaTempErp>();
                }
            }
            else
            {
                lstEntidad = new List<AsistenciaTempErp>();
            }

            int contador = lstEntidad.Count;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfList = lstEntidad.ToPagedList(pageNumber, contador); // will only contain 25 products max because of the pageSize

            ViewBag.Listado = onePageOfList;
            if (cmbCompVent_FilterPeriodo == null)
            {
                cmbCompVent_FilterPeriodo = "";
            }
            ViewBag.cmbCompVent_FilterPeriodo = cmbCompVent_FilterPeriodo;
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;

            return View();
        }
        public async Task<ActionResult> ListPlanillon(string EstaFilt)
        {
            List<AsistenciaTempErp> lstEntidad = null;
            AsistenciaTempErp busca = new AsistenciaTempErp()
            {
                PERIODO = EstaFilt
            };

            lstEntidad = await GetPlanillon(busca);
            if (lstEntidad == null || !lstEntidad.Any())
            {
                lstEntidad = new List<AsistenciaTempErp>();
            }

            string quincex = "";
            try
            {
                quincex = EstaFilt.Substring(6);
            }
            catch (Exception e)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.QUINCEX = quincex;
            ViewBag.PeriodoSelected = EstaFilt;
            return PartialView(lstEntidad);
        }
        public async Task<ActionResult> ListPlanillonMensual(string EstaFilt)
        {
            List<AsistenciaTempErp> lstEntidad = null;
            AsistenciaTempErp busca = new AsistenciaTempErp()
            {
                PERIODO = EstaFilt
            };

            lstEntidad = await GetPlanillonTotal(busca);
            if (lstEntidad == null || !lstEntidad.Any())
            {
                lstEntidad = new List<AsistenciaTempErp>();
            }

            string quincex = "";
            try
            {
                quincex = EstaFilt.Substring(6);
            }
            catch (Exception e)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.QUINCEX = quincex;
            ViewBag.PeriodoSelected = EstaFilt;
            return PartialView(lstEntidad);
        }
        private async Task<List<AsistenciaTempErp>> GetPlanillon(AsistenciaTempErp busca)
        {
            List<AsistenciaTempErp> lstEntidad = new List<AsistenciaTempErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_asistencia_tempSelectByPeriodo", busca);

                var model = response.Content.ReadAsAsync<List<AsistenciaTempErp>>();
                if (model.Result.Count > 0)
                {
                    return lstEntidad = model.Result.ToList();
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
        private async Task<List<AsistenciaTempErp>> GetPlanillonTotal(AsistenciaTempErp busca)
        {
            List<AsistenciaTempErp> lstEntidad = new List<AsistenciaTempErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_asistencia_tempSelectByPeriodoTotal", busca);

                var model = response.Content.ReadAsAsync<List<AsistenciaTempErp>>();
                if (model.Result.Count > 0)
                {
                    return lstEntidad = model.Result.ToList();
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
        public async Task<ActionResult> PlanillaAsistencia()
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();

            string periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
            int dia = DateTime.Now.Day;
            if (dia <= 15)
            {
                periodo = periodo + "I";
            }
            else
            {
                periodo = periodo + "II";
            }
            ViewBag.Periodo = periodo;

            List<PeriodoErp> _lperiodos = new List<PeriodoErp>();
            PeriodoErp docid = new PeriodoErp()
            {
                IDPERIODO = "",
                PERIODO = "",
                ANIO = ""
            };
            _lperiodos = await GePeriodos();
            _lperiodos.Add(docid);
            ViewBag.Periodos = _lperiodos.OrderBy(x => x.IDPERIODO).ToList();

            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View();
        }

        private async Task<List<PeriodoErp>> GePeriodos()
        {
            List<PeriodoErp> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Personalerp/t_periodoSelectAll");
            var model = response.Content.ReadAsAsync<List<PeriodoErp>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = new List<PeriodoErp>();
            }
            return lentidad;
        }
        public async Task<ActionResult> ListRegistros(string EstaFilt)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftptareos"];
            ViewBag.UrlLink = virtualdir;
            List<ControlIngresoErp> lstEntidad = null;
            List<ControlIngresoErp> _lcontrol = new List<ControlIngresoErp>();
            ControlIngresoErp busca = new ControlIngresoErp()
            {
                PERIODO = EstaFilt
            };

            lstEntidad = await GetControlIngresoPeriodo(busca);
            if (lstEntidad == null || !lstEntidad.Any())
            {
                lstEntidad = new List<ControlIngresoErp>();
            }
            else
            {
                if (IDUSUARIOTIPO.Trim().Equals("ADM"))
                {
                    _lcontrol = lstEntidad.ToList();
                }
                else
                {
                    _lcontrol = lstEntidad.Where(x => x.IDUSUARIO.Equals(FkUsua)).ToList();
                }

            }


            string quincex = "";
            try
            {
                quincex = EstaFilt.Substring(6);
            }
            catch (Exception e)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.IDUSUARIOTIPO = IDUSUARIOTIPO;
            //ViewBag.QUINCEX = quincex;
            return PartialView(_lcontrol);
        }

        private async Task<List<ControlIngresoErp>> GetControlIngresoPeriodo(ControlIngresoErp busca)
        {
            List<ControlIngresoErp> lstEntidad = new List<ControlIngresoErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_control_ingresoSelectByPeriodo", busca);

                var model = response.Content.ReadAsAsync<List<ControlIngresoErp>>();
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

        public async Task<ActionResult> Index()
        {
            string virtualdir = System.Configuration.ConfigurationManager.AppSettings["VirtualFolderstring"] + System.Configuration.ConfigurationManager.AppSettings["Ftptareos"];
            ViewBag.UrlLink = virtualdir;
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();
            var fechahoy = DateTime.Now;

            string periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
            int dia = DateTime.Now.Day;
            ViewBag.Periodo = periodo;

            List<PeriodoErp> _lperiodos = new List<PeriodoErp>();
            PeriodoErp docid = new PeriodoErp()
            {
                IDPERIODO = "",
                PERIODO = "",
                ANIO = ""
            };
            _lperiodos = await GePeriodos();
            _lperiodos.Add(docid);
            ViewBag.Periodos = _lperiodos.GroupBy(x => x.PERIODO).Select(g => g.First()).OrderBy(p => p.PERIODO).ToList();



            List<ControlIngresoErp> _lcontrol = new List<ControlIngresoErp>();
            try
            {
                var existe = await GetControlIngresoErpAll();
                if (existe != null && existe.Any())
                {
                    if (IDUSUARIOTIPO.Trim().Equals("ADM"))
                    {
                        _lcontrol = existe.Where(x => x.FECHA.Year == fechahoy.Year && x.FECHA.Month == fechahoy.Month).ToList();
                    }
                    else
                    {
                        _lcontrol = existe.Where(x => x.FECHA.Year == fechahoy.Year && x.FECHA.Month == fechahoy.Month && x.IDUSUARIO.Equals(FkUsua)).ToList();
                    }

                }
            }
            catch (Exception e)
            {

            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.IDUSUARIOTIPO = IDUSUARIOTIPO;
            return View(_lcontrol);
        }

        public async Task<List<ControlIngresoErp>> GetControlIngresoErpAll()
        {
            List<ControlIngresoErp> lstEntidad = null;
            var client = new HttpClient();
            try
            {
                var connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("Personalerp/t_control_ingresoSelectAll");
                var model = response.Content.ReadAsAsync<List<ControlIngresoErp>>();
                lstEntidad = model.Result.Count > 0 ? model.Result.ToList() : new List<ControlIngresoErp>();
            }
            catch (Exception ex)
            {
                return null;
            }

            return lstEntidad;
        }

        public async Task<ActionResult> GetDetalleControl(string IDCONTROLINGRESO)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string Nombres = Session["Nombres"].ToString().Trim();
            string IDUSUARIOTIPO = Session["IDUSUARIOTIPO"].ToString().Trim();
            string periodo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
            ViewBag.Periodo = periodo;
            var codigo = await GetCodigoErpAll();
            ViewBag.Codigo = codigo.Codigo;
            ViewBag.IDUSUARIOTIPO = IDUSUARIOTIPO.Trim();
            List<TipoAsistenciaErp> _ltipos = new List<TipoAsistenciaErp>(){
                new TipoAsistenciaErp(){tipo = "I",descripcion = "INGRESO"},
                new TipoAsistenciaErp(){tipo = "S", descripcion = "SALIDA"},
                new TipoAsistenciaErp(){tipo = "R", descripcion = "RE-INGRESO"},
                new TipoAsistenciaErp(){tipo = "Z", descripcion = "RE-SALIDA"},
                new TipoAsistenciaErp(){tipo = "A", descripcion = "REFRIGERIO"},
                new TipoAsistenciaErp(){tipo = "B", descripcion = "FIN REFRIGERIO"}
                };
            ControlIngresoErp entidad = new ControlIngresoErp();
            ViewBag.TiposAsistencia = _ltipos;
            ControlIngresoErp busca = new ControlIngresoErp()
            {
                IDCONTROLINGRESO = IDCONTROLINGRESO
            };
            if (IDCONTROLINGRESO == null)
            {
                entidad = new ControlIngresoErp()
                {
                    IDCONTROLINGRESO = codigo.Codigo,
                    FECHA = DateTime.Now,
                    PERIODO = periodo,
                    IDPLANILLA = "",
                    IDUSUARIO = FkUsua,
                    NOMBRES = Nombres,
                    FECHACREACION = DateTime.Now,
                    IDESTADO = "PE",
                    generado_zktime = "0",
                    procesado = "0",
                    id_tabla = 0
                };

            }
            else
            {
                entidad = await GetDatosCabecera(busca);
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(entidad);
        }

        private async Task<ControlIngresoErp> GetDatosCabecera(ControlIngresoErp busca)
        {
            List<ControlIngresoErp> lstEntidad = new List<ControlIngresoErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_control_ingresoSelectById", busca);

                var model = response.Content.ReadAsAsync<List<ControlIngresoErp>>();
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

        public async Task<CodigoErp> GetCodigoErpAll()
        {
            CodigoErp lstEntidad = null;
            var client = new HttpClient();
            try
            {
                var connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("Personalerp/obtener_id");
                var model = response.Content.ReadAsAsync<List<CodigoErp>>();
                lstEntidad = model.Result.Count > 0 ? model.Result[0] : new CodigoErp();
            }
            catch (Exception ex)
            {
                return null;
            }

            return lstEntidad;
        }

        public async Task<ActionResult> ListDataDetalleAsistencia(string IDCONTROLINGRESO)
        {
            string msgReturn = "";
            List<ControlIngresoDetalleErp> lstEntidad = null;
            ControlIngresoDetalleErp busca = new ControlIngresoDetalleErp() { IDCONTROLINGRESO = IDCONTROLINGRESO.Trim() };

            try
            {
                lstEntidad = await GetIDCONTROLINGRESO(busca);
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

        private async Task<List<ControlIngresoDetalleErp>> GetIDCONTROLINGRESO(ControlIngresoDetalleErp busca)
        {
            List<ControlIngresoDetalleErp> lstEntidad = null;

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_control_ingreso_detalleSelectByFk", busca);
                //CAST JSON A LIST<OBJECT>
                var model = response.Content.ReadAsAsync<List<ControlIngresoDetalleErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<ControlIngresoDetalleErp>();
                }
                //CAST JSON A LIST<OBJECT>
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        [HttpPost]
        public async Task<JsonResult> InsertaCabecera(int id_tabla, string IDCONTROLINGRESO, string FECHA, string PERIODO)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            ControlIngresoErp _entidad = new ControlIngresoErp()
            {
                id_tabla = id_tabla,
                IDCONTROLINGRESO = IDCONTROLINGRESO,
                FECHA = Convert.ToDateTime(FECHA),
                PERIODO = PERIODO,
                IDUSUARIO = FkUsua,
                generado_zktime = "0"
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
                if (id_tabla == 0)
                {
                    metodo = "Personalerp/t_control_ingresoInsert";
                }
                else
                {
                    metodo = "Personalerp/t_control_ingresoUpdate";
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
        public async Task<JsonResult> InsertaDetalle(string IDCONTROLINGRESO,
            string IDPERSONAL,
            string IDPLANILLA,
            string MARCACION,
            string auto,
            string FECHA,
            string TIPO)
        {
            int idinserted = 0;
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            string hora = DateTime.Now.ToString("HH:mm:ss");
            if (auto == "1")
            {
                MARCACION = hora;
            }
            else if (MARCACION == "__:__:__")
            {
                MARCACION = "00:00:00";
            }

            MARCACION = MARCACION.Replace("_", "0");

            ControlIngresoDetalleErp _entidad = new ControlIngresoDetalleErp()
            {
                IDCONTROLINGRESO = IDCONTROLINGRESO,
                IDPERSONAL = IDPERSONAL,
                IDPLANILLA = IDPLANILLA,
                MARCACION = MARCACION,
                FECHA = Convert.ToDateTime(FECHA),
                TIPO = TIPO,
                IDUSUARIO = FkUsua
            };

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "Personalerp/t_control_ingreso_detalleInsert";

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

        public ActionResult Download()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "Planilla.xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }

        public async Task<ActionResult> ExcelExport(string Periodo)
        {
            string varperiodo = Periodo.Substring(6);
            List<AsistenciaTempErp> lstEntidad = new List<AsistenciaTempErp>();
            List<AsistenciaTempErp> FileData = new List<AsistenciaTempErp>();
            AsistenciaTempErp busca = new AsistenciaTempErp()
            {
                PERIODO = Periodo
            };

            FileData = await GetPlanillon(busca);
            if (FileData == null || !FileData.Any())
            {
                return null;
            }
            else
            {
                try
                {
                    DataTable Dt = new DataTable();

                    if (varperiodo == "I")
                    {
                        Dt.Columns.Add("NRO", typeof(Int32));
                        Dt.Columns.Add("DOCUMENTO", typeof(string));
                        Dt.Columns.Add("NOMBRES", typeof(string));
                        Dt.Columns.Add("IN 01", typeof(string));
                        Dt.Columns.Add("SA 01", typeof(string));
                        Dt.Columns.Add("HN 01", typeof(decimal));
                        Dt.Columns.Add("IN REFR 01", typeof(string));
                        Dt.Columns.Add("FIN REFR 01", typeof(string));
                        Dt.Columns.Add("HRF 01", typeof(decimal));
                        Dt.Columns.Add("REIN 01", typeof(string));
                        Dt.Columns.Add("RESA 01", typeof(string));
                        Dt.Columns.Add("HR 01", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 01", typeof(decimal));
                        Dt.Columns.Add("IN 02", typeof(string));
                        Dt.Columns.Add("SA 02", typeof(string));
                        Dt.Columns.Add("HN 02", typeof(decimal));
                        Dt.Columns.Add("IN REFR 02", typeof(string));
                        Dt.Columns.Add("FIN REFR 02", typeof(string));
                        Dt.Columns.Add("HRF 02", typeof(decimal));
                        Dt.Columns.Add("REIN 02", typeof(string));
                        Dt.Columns.Add("RESA 02", typeof(string));
                        Dt.Columns.Add("HR 02", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 02", typeof(decimal));
                        Dt.Columns.Add("IN 03", typeof(string));
                        Dt.Columns.Add("SA 03", typeof(string));
                        Dt.Columns.Add("HN 03", typeof(decimal));
                        Dt.Columns.Add("IN REFR 03", typeof(string));
                        Dt.Columns.Add("FIN REFR 03", typeof(string));
                        Dt.Columns.Add("HRF 03", typeof(decimal));
                        Dt.Columns.Add("REIN 03", typeof(string));
                        Dt.Columns.Add("RESA 03", typeof(string));
                        Dt.Columns.Add("HR 03", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 03", typeof(decimal));
                        Dt.Columns.Add("IN 04", typeof(string));
                        Dt.Columns.Add("SA 04", typeof(string));
                        Dt.Columns.Add("HN 04", typeof(decimal));
                        Dt.Columns.Add("IN REFR 04", typeof(string));
                        Dt.Columns.Add("FIN REFR 04", typeof(string));
                        Dt.Columns.Add("HRF 04", typeof(decimal));
                        Dt.Columns.Add("REIN 04", typeof(string));
                        Dt.Columns.Add("RESA 04", typeof(string));
                        Dt.Columns.Add("HR 04", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 04", typeof(decimal));
                        Dt.Columns.Add("IN 05", typeof(string));
                        Dt.Columns.Add("SA 05", typeof(string));
                        Dt.Columns.Add("HN 05", typeof(decimal));
                        Dt.Columns.Add("IN REFR 05", typeof(string));
                        Dt.Columns.Add("FIN REFR 05", typeof(string));
                        Dt.Columns.Add("HRF 05", typeof(decimal));
                        Dt.Columns.Add("REIN 05", typeof(string));
                        Dt.Columns.Add("RESA 05", typeof(string));
                        Dt.Columns.Add("HR 05", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 05", typeof(decimal));
                        Dt.Columns.Add("IN 06", typeof(string));
                        Dt.Columns.Add("SA 06", typeof(string));
                        Dt.Columns.Add("HN 06", typeof(decimal));
                        Dt.Columns.Add("IN REFR 06", typeof(string));
                        Dt.Columns.Add("FIN REFR 06", typeof(string));
                        Dt.Columns.Add("HRF 06", typeof(decimal));
                        Dt.Columns.Add("REIN 06", typeof(string));
                        Dt.Columns.Add("RESA 06", typeof(string));
                        Dt.Columns.Add("HR 06", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 06", typeof(decimal));
                        Dt.Columns.Add("IN 07", typeof(string));
                        Dt.Columns.Add("SA 07", typeof(string));
                        Dt.Columns.Add("HN 07", typeof(decimal));
                        Dt.Columns.Add("IN REFR 07", typeof(string));
                        Dt.Columns.Add("FIN REFR 07", typeof(string));
                        Dt.Columns.Add("HRF 07", typeof(decimal));
                        Dt.Columns.Add("REIN 07", typeof(string));
                        Dt.Columns.Add("RESA 07", typeof(string));
                        Dt.Columns.Add("HR 07", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 07", typeof(decimal));
                        Dt.Columns.Add("IN 08", typeof(string));
                        Dt.Columns.Add("SA 08", typeof(string));
                        Dt.Columns.Add("HN 08", typeof(decimal));
                        Dt.Columns.Add("IN REFR 08", typeof(string));
                        Dt.Columns.Add("FIN REFR 08", typeof(string));
                        Dt.Columns.Add("HRF 08", typeof(decimal));
                        Dt.Columns.Add("REIN 08", typeof(string));
                        Dt.Columns.Add("RESA 08", typeof(string));
                        Dt.Columns.Add("HR 08", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 08", typeof(decimal));
                        Dt.Columns.Add("IN 09", typeof(string));
                        Dt.Columns.Add("SA 09", typeof(string));
                        Dt.Columns.Add("HN 09", typeof(decimal));
                        Dt.Columns.Add("IN REFR 09", typeof(string));
                        Dt.Columns.Add("FIN REFR 09", typeof(string));
                        Dt.Columns.Add("HRF 09", typeof(decimal));
                        Dt.Columns.Add("REIN 09", typeof(string));
                        Dt.Columns.Add("RESA 09", typeof(string));
                        Dt.Columns.Add("HR 09", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 09", typeof(decimal));
                        Dt.Columns.Add("IN 10", typeof(string));
                        Dt.Columns.Add("SA 10", typeof(string));
                        Dt.Columns.Add("HN 10", typeof(decimal));
                        Dt.Columns.Add("IN REFR 10", typeof(string));
                        Dt.Columns.Add("FIN REFR 10", typeof(string));
                        Dt.Columns.Add("HRF 10", typeof(decimal));
                        Dt.Columns.Add("REIN 10", typeof(string));
                        Dt.Columns.Add("RESA 10", typeof(string));
                        Dt.Columns.Add("HR 10", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 10", typeof(decimal));
                        Dt.Columns.Add("IN 11", typeof(string));
                        Dt.Columns.Add("SA 11", typeof(string));
                        Dt.Columns.Add("HN 11", typeof(decimal));
                        Dt.Columns.Add("IN REFR 11", typeof(string));
                        Dt.Columns.Add("FIN REFR 11", typeof(string));
                        Dt.Columns.Add("HRF 11", typeof(decimal));
                        Dt.Columns.Add("REIN 11", typeof(string));
                        Dt.Columns.Add("RESA 11", typeof(string));
                        Dt.Columns.Add("HR 11", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 11", typeof(decimal));
                        Dt.Columns.Add("IN 12", typeof(string));
                        Dt.Columns.Add("SA 12", typeof(string));
                        Dt.Columns.Add("HN 12", typeof(decimal));
                        Dt.Columns.Add("IN REFR 12", typeof(string));
                        Dt.Columns.Add("FIN REFR 12", typeof(string));
                        Dt.Columns.Add("HRF 12", typeof(decimal));
                        Dt.Columns.Add("REIN 12", typeof(string));
                        Dt.Columns.Add("RESA 12", typeof(string));
                        Dt.Columns.Add("HR 12", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 12", typeof(decimal));
                        Dt.Columns.Add("IN 13", typeof(string));
                        Dt.Columns.Add("SA 13", typeof(string));
                        Dt.Columns.Add("HN 13", typeof(decimal));
                        Dt.Columns.Add("IN REFR 13", typeof(string));
                        Dt.Columns.Add("FIN REFR 13", typeof(string));
                        Dt.Columns.Add("HRF 13", typeof(decimal));
                        Dt.Columns.Add("REIN 13", typeof(string));
                        Dt.Columns.Add("RESA 13", typeof(string));
                        Dt.Columns.Add("HR 13", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 13", typeof(decimal));
                        Dt.Columns.Add("IN 14", typeof(string));
                        Dt.Columns.Add("SA 14", typeof(string));
                        Dt.Columns.Add("HN 14", typeof(decimal));
                        Dt.Columns.Add("IN REFR 14", typeof(string));
                        Dt.Columns.Add("FIN REFR 14", typeof(string));
                        Dt.Columns.Add("HRF 14", typeof(decimal));
                        Dt.Columns.Add("REIN 14", typeof(string));
                        Dt.Columns.Add("RESA 14", typeof(string));
                        Dt.Columns.Add("HR 14", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 14", typeof(decimal));
                        Dt.Columns.Add("IN 15", typeof(string));
                        Dt.Columns.Add("SA 15", typeof(string));
                        Dt.Columns.Add("HN 15", typeof(decimal));
                        Dt.Columns.Add("IN REFR 15", typeof(string));
                        Dt.Columns.Add("FIN REFR 15", typeof(string));
                        Dt.Columns.Add("HRF 15", typeof(decimal));
                        Dt.Columns.Add("REIN 15", typeof(string));
                        Dt.Columns.Add("RESA 15", typeof(string));
                        Dt.Columns.Add("HR 15", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 15", typeof(decimal));
                        Dt.Columns.Add("HORAS QUINCENA", typeof(decimal));
                        Dt.Columns.Add("TOTAL JORNALES", typeof(decimal));
                        foreach (var data in FileData)
                        {
                            DataRow row = Dt.NewRow();
                            row[0] = data.itemrecord;
                            row[1] = data.IDCODIGOGENERAL;
                            row[2] = data.NOMBRES;
                            row[3] = data.s01_ingreso;
                            row[4] = data.s01_salida;
                            row[5] = data.s01_horas;
                            row[6] = data.s01_irefrigerio;
                            row[7] = data.s01_frefrigerio;
                            row[8] = data.s01_refh;
                            row[9] = data.s01_ingresor;
                            row[10] = data.s01_salidar;
                            row[11] = data.s01_horasr;
                            row[12] = data.s01_ht;
                            row[13] = data.s02_ingreso;
                            row[14] = data.s02_salida;
                            row[15] = data.s02_horas;
                            row[16] = data.s02_irefrigerio;
                            row[17] = data.s02_frefrigerio;
                            row[18] = data.s02_refh;
                            row[19] = data.s02_ingresor;
                            row[20] = data.s02_salidar;
                            row[21] = data.s02_horasr;
                            row[22] = data.s02_ht;

                            row[23] = data.s03_ingreso;
                            row[24] = data.s03_salida;
                            row[25] = data.s03_horas;
                            row[26] = data.s03_irefrigerio;
                            row[27] = data.s03_frefrigerio;
                            row[28] = data.s03_refh;
                            row[29] = data.s03_ingresor;
                            row[30] = data.s03_salidar;
                            row[31] = data.s03_horasr;
                            row[32] = data.s03_ht;
                            row[33] = data.s04_ingreso;
                            row[34] = data.s04_salida;
                            row[35] = data.s04_horas;
                            row[36] = data.s04_irefrigerio;
                            row[37] = data.s04_frefrigerio;
                            row[38] = data.s04_refh;
                            row[39] = data.s04_ingresor;
                            row[40] = data.s04_salidar;
                            row[41] = data.s04_horasr;
                            row[42] = data.s04_ht;
                            row[43] = data.s05_ingreso;
                            row[44] = data.s05_salida;
                            row[45] = data.s05_horas;
                            row[46] = data.s05_irefrigerio;
                            row[47] = data.s05_frefrigerio;
                            row[48] = data.s05_refh;
                            row[49] = data.s05_ingresor;
                            row[50] = data.s05_salidar;
                            row[51] = data.s05_horasr;
                            row[52] = data.s05_ht;
                            row[53] = data.s06_ingreso;
                            row[54] = data.s06_salida;
                            row[55] = data.s06_horas;
                            row[56] = data.s06_irefrigerio;
                            row[57] = data.s06_frefrigerio;
                            row[58] = data.s06_refh;
                            row[59] = data.s06_ingresor;
                            row[60] = data.s06_salidar;
                            row[61] = data.s06_horasr;
                            row[62] = data.s06_ht;
                            row[63] = data.s07_ingreso;
                            row[64] = data.s07_salida;
                            row[65] = data.s07_horas;
                            row[66] = data.s07_irefrigerio;
                            row[67] = data.s07_frefrigerio;
                            row[68] = data.s07_refh;
                            row[69] = data.s07_ingresor;
                            row[70] = data.s07_salidar;
                            row[71] = data.s07_horasr;
                            row[72] = data.s07_ht;
                            row[73] = data.s08_ingreso;
                            row[74] = data.s08_salida;
                            row[75] = data.s08_horas;
                            row[76] = data.s08_irefrigerio;
                            row[77] = data.s08_frefrigerio;
                            row[78] = data.s08_refh;
                            row[79] = data.s08_ingresor;
                            row[80] = data.s08_salidar;
                            row[81] = data.s08_horasr;
                            row[82] = data.s08_ht;
                            row[83] = data.s09_ingreso;
                            row[84] = data.s09_salida;
                            row[85] = data.s09_horas;
                            row[86] = data.s09_irefrigerio;
                            row[87] = data.s09_frefrigerio;
                            row[88] = data.s09_refh;
                            row[89] = data.s09_ingresor;
                            row[90] = data.s09_salidar;
                            row[91] = data.s09_horasr;
                            row[92] = data.s09_ht;
                            row[93] = data.s10_ingreso;
                            row[94] = data.s10_salida;
                            row[95] = data.s10_horas;
                            row[96] = data.s10_irefrigerio;
                            row[97] = data.s10_frefrigerio;
                            row[98] = data.s10_refh;
                            row[99] = data.s10_ingresor;
                            row[100] = data.s10_salidar;
                            row[101] = data.s10_horasr;
                            row[102] = data.s10_ht;
                            row[103] = data.s11_ingreso;
                            row[104] = data.s11_salida;
                            row[105] = data.s11_horas;
                            row[106] = data.s11_irefrigerio;
                            row[107] = data.s11_frefrigerio;
                            row[108] = data.s11_refh;
                            row[109] = data.s11_ingresor;
                            row[110] = data.s11_salidar;
                            row[111] = data.s11_horasr;
                            row[112] = data.s11_ht;
                            row[113] = data.s12_ingreso;
                            row[114] = data.s12_salida;
                            row[115] = data.s12_horas;
                            row[116] = data.s12_irefrigerio;
                            row[117] = data.s12_frefrigerio;
                            row[118] = data.s12_refh;
                            row[119] = data.s12_ingresor;
                            row[120] = data.s12_salidar;
                            row[121] = data.s12_horasr;
                            row[122] = data.s12_ht;
                            row[123] = data.s13_ingreso;
                            row[124] = data.s13_salida;
                            row[125] = data.s13_horas;
                            row[126] = data.s13_irefrigerio;
                            row[127] = data.s13_frefrigerio;
                            row[128] = data.s13_refh;
                            row[129] = data.s13_ingresor;
                            row[130] = data.s13_salidar;
                            row[131] = data.s13_horasr;
                            row[132] = data.s13_ht;
                            row[133] = data.s14_ingreso;
                            row[134] = data.s14_salida;
                            row[135] = data.s14_horas;
                            row[136] = data.s14_irefrigerio;
                            row[137] = data.s14_frefrigerio;
                            row[138] = data.s14_refh;
                            row[139] = data.s14_ingresor;
                            row[140] = data.s14_salidar;
                            row[141] = data.s14_horasr;
                            row[142] = data.s14_ht;
                            row[143] = data.s15_ingreso;
                            row[144] = data.s15_salida;
                            row[145] = data.s15_horas;
                            row[146] = data.s15_irefrigerio;
                            row[147] = data.s15_frefrigerio;
                            row[148] = data.s15_refh;
                            row[149] = data.s15_ingresor;
                            row[150] = data.s15_salidar;
                            row[151] = data.s15_horasr;
                            row[152] = data.s15_ht;
                            row[153] = data.s01_dia_horas;
                            row[154] = data.s01_jornales;
                            Dt.Rows.Add(row);

                        }
                    }
                    else
                    {
                        Dt = new DataTable();
                        Dt.Columns.Add("NRO", typeof(Int32));
                        Dt.Columns.Add("DOCUMENTO", typeof(string));
                        Dt.Columns.Add("NOMBRES", typeof(string));
                        Dt.Columns.Add("IN 16", typeof(string));
                        Dt.Columns.Add("SA 16", typeof(string));
                        Dt.Columns.Add("HN 16", typeof(decimal));
                        Dt.Columns.Add("IN REFR 16", typeof(string));
                        Dt.Columns.Add("FIN REFR 16", typeof(string));
                        Dt.Columns.Add("HRF 16", typeof(decimal));
                        Dt.Columns.Add("REIN 16", typeof(string));
                        Dt.Columns.Add("RESA 16", typeof(string));
                        Dt.Columns.Add("HR 16", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 16", typeof(decimal));
                        Dt.Columns.Add("IN 17", typeof(string));
                        Dt.Columns.Add("SA 17", typeof(string));
                        Dt.Columns.Add("HN 17", typeof(decimal));
                        Dt.Columns.Add("IN REFR 17", typeof(string));
                        Dt.Columns.Add("FIN REFR 17", typeof(string));
                        Dt.Columns.Add("HRF 17", typeof(decimal));
                        Dt.Columns.Add("REIN 17", typeof(string));
                        Dt.Columns.Add("RESA 17", typeof(string));
                        Dt.Columns.Add("HR 17", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 17", typeof(decimal));
                        Dt.Columns.Add("IN 18", typeof(string));
                        Dt.Columns.Add("SA 18", typeof(string));
                        Dt.Columns.Add("HN 18", typeof(decimal));
                        Dt.Columns.Add("IN REFR 18", typeof(string));
                        Dt.Columns.Add("FIN REFR 18", typeof(string));
                        Dt.Columns.Add("HRF 18", typeof(decimal));
                        Dt.Columns.Add("REIN 18", typeof(string));
                        Dt.Columns.Add("RESA 18", typeof(string));
                        Dt.Columns.Add("HR 18", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 18", typeof(decimal));
                        Dt.Columns.Add("IN 19", typeof(string));
                        Dt.Columns.Add("SA 19", typeof(string));
                        Dt.Columns.Add("HN 19", typeof(decimal));
                        Dt.Columns.Add("IN REFR 19", typeof(string));
                        Dt.Columns.Add("FIN REFR 19", typeof(string));
                        Dt.Columns.Add("HRF 19", typeof(decimal));
                        Dt.Columns.Add("REIN 19", typeof(string));
                        Dt.Columns.Add("RESA 19", typeof(string));
                        Dt.Columns.Add("HR 19", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 19", typeof(decimal));
                        Dt.Columns.Add("IN 20", typeof(string));
                        Dt.Columns.Add("SA 20", typeof(string));
                        Dt.Columns.Add("HN 20", typeof(decimal));
                        Dt.Columns.Add("IN REFR 20", typeof(string));
                        Dt.Columns.Add("FIN REFR 20", typeof(string));
                        Dt.Columns.Add("HRF 20", typeof(decimal));
                        Dt.Columns.Add("REIN 20", typeof(string));
                        Dt.Columns.Add("RESA 20", typeof(string));
                        Dt.Columns.Add("HR 20", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 20", typeof(decimal));
                        Dt.Columns.Add("IN 21", typeof(string));
                        Dt.Columns.Add("SA 21", typeof(string));
                        Dt.Columns.Add("HN 21", typeof(decimal));
                        Dt.Columns.Add("IN REFR 21", typeof(string));
                        Dt.Columns.Add("FIN REFR 21", typeof(string));
                        Dt.Columns.Add("HRF 21", typeof(decimal));
                        Dt.Columns.Add("REIN 21", typeof(string));
                        Dt.Columns.Add("RESA 21", typeof(string));
                        Dt.Columns.Add("HR 21", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 21", typeof(decimal));
                        Dt.Columns.Add("IN 22", typeof(string));
                        Dt.Columns.Add("SA 22", typeof(string));
                        Dt.Columns.Add("HN 22", typeof(decimal));
                        Dt.Columns.Add("IN REFR 22", typeof(string));
                        Dt.Columns.Add("FIN REFR 22", typeof(string));
                        Dt.Columns.Add("HRF 22", typeof(decimal));
                        Dt.Columns.Add("REIN 22", typeof(string));
                        Dt.Columns.Add("RESA 22", typeof(string));
                        Dt.Columns.Add("HR 22", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 22", typeof(decimal));
                        Dt.Columns.Add("IN 23", typeof(string));
                        Dt.Columns.Add("SA 23", typeof(string));
                        Dt.Columns.Add("HN 23", typeof(decimal));
                        Dt.Columns.Add("IN REFR 23", typeof(string));
                        Dt.Columns.Add("FIN REFR 23", typeof(string));
                        Dt.Columns.Add("HRF 23", typeof(decimal));
                        Dt.Columns.Add("REIN 23", typeof(string));
                        Dt.Columns.Add("RESA 23", typeof(string));
                        Dt.Columns.Add("HR 23", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 23", typeof(decimal));
                        Dt.Columns.Add("IN 24", typeof(string));
                        Dt.Columns.Add("SA 24", typeof(string));
                        Dt.Columns.Add("HN 24", typeof(decimal));
                        Dt.Columns.Add("IN REFR 24", typeof(string));
                        Dt.Columns.Add("FIN REFR 24", typeof(string));
                        Dt.Columns.Add("HRF 24", typeof(decimal));
                        Dt.Columns.Add("REIN 24", typeof(string));
                        Dt.Columns.Add("RESA 24", typeof(string));
                        Dt.Columns.Add("HR 24", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 24", typeof(decimal));
                        Dt.Columns.Add("IN 25", typeof(string));
                        Dt.Columns.Add("SA 25", typeof(string));
                        Dt.Columns.Add("HN 25", typeof(decimal));
                        Dt.Columns.Add("IN REFR 25", typeof(string));
                        Dt.Columns.Add("FIN REFR 25", typeof(string));
                        Dt.Columns.Add("HRF 25", typeof(decimal));
                        Dt.Columns.Add("REIN 25", typeof(string));
                        Dt.Columns.Add("RESA 25", typeof(string));
                        Dt.Columns.Add("HR 25", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 25", typeof(decimal));
                        Dt.Columns.Add("IN 26", typeof(string));
                        Dt.Columns.Add("SA 26", typeof(string));
                        Dt.Columns.Add("HN 26", typeof(decimal));
                        Dt.Columns.Add("IN REFR 26", typeof(string));
                        Dt.Columns.Add("FIN REFR 26", typeof(string));
                        Dt.Columns.Add("HRF 26", typeof(decimal));
                        Dt.Columns.Add("REIN 26", typeof(string));
                        Dt.Columns.Add("RESA 26", typeof(string));
                        Dt.Columns.Add("HR 26", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 26", typeof(decimal));
                        Dt.Columns.Add("IN 27", typeof(string));
                        Dt.Columns.Add("SA 27", typeof(string));
                        Dt.Columns.Add("HN 27", typeof(decimal));
                        Dt.Columns.Add("IN REFR 27", typeof(string));
                        Dt.Columns.Add("FIN REFR 27", typeof(string));
                        Dt.Columns.Add("HRF 27", typeof(decimal));
                        Dt.Columns.Add("REIN 27", typeof(string));
                        Dt.Columns.Add("RESA 27", typeof(string));
                        Dt.Columns.Add("HR 27", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 27", typeof(decimal));
                        Dt.Columns.Add("IN 28", typeof(string));
                        Dt.Columns.Add("SA 28", typeof(string));
                        Dt.Columns.Add("HN 28", typeof(decimal));
                        Dt.Columns.Add("IN REFR 28", typeof(string));
                        Dt.Columns.Add("FIN REFR 28", typeof(string));
                        Dt.Columns.Add("HRF 28", typeof(decimal));
                        Dt.Columns.Add("REIN 28", typeof(string));
                        Dt.Columns.Add("RESA 28", typeof(string));
                        Dt.Columns.Add("HR 28", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 28", typeof(decimal));
                        Dt.Columns.Add("IN 29", typeof(string));
                        Dt.Columns.Add("SA 29", typeof(string));
                        Dt.Columns.Add("HN 29", typeof(decimal));
                        Dt.Columns.Add("IN REFR 29", typeof(string));
                        Dt.Columns.Add("FIN REFR 29", typeof(string));
                        Dt.Columns.Add("HRF 29", typeof(decimal));
                        Dt.Columns.Add("REIN 29", typeof(string));
                        Dt.Columns.Add("RESA 29", typeof(string));
                        Dt.Columns.Add("HR 29", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 29", typeof(decimal));
                        Dt.Columns.Add("IN 30", typeof(string));
                        Dt.Columns.Add("SA 30", typeof(string));
                        Dt.Columns.Add("HN 30", typeof(decimal));
                        Dt.Columns.Add("IN REFR 30", typeof(string));
                        Dt.Columns.Add("FIN REFR 30", typeof(string));
                        Dt.Columns.Add("HRF 30", typeof(decimal));
                        Dt.Columns.Add("REIN 30", typeof(string));
                        Dt.Columns.Add("RESA 30", typeof(string));
                        Dt.Columns.Add("HR 30", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 30", typeof(decimal));
                        Dt.Columns.Add("IN 31", typeof(string));
                        Dt.Columns.Add("SA 31", typeof(string));
                        Dt.Columns.Add("HN 31", typeof(decimal));
                        Dt.Columns.Add("IN REFR 31", typeof(string));
                        Dt.Columns.Add("FIN REFR 31", typeof(string));
                        Dt.Columns.Add("HRF 31", typeof(decimal));
                        Dt.Columns.Add("REIN 31", typeof(string));
                        Dt.Columns.Add("RESA 31", typeof(string));
                        Dt.Columns.Add("HR 31", typeof(decimal));
                        Dt.Columns.Add("HOR TOT 31", typeof(decimal));
                        Dt.Columns.Add("HORAS QUINCENA", typeof(decimal));
                        Dt.Columns.Add("TOTAL JORNALES", typeof(decimal));

                        foreach (var data in FileData)
                        {
                            DataRow row = Dt.NewRow();
                            row[0] = data.itemrecord;
                            row[1] = data.IDCODIGOGENERAL;
                            row[2] = data.NOMBRES;
                            row[3] = data.s16_ingreso;
                            row[4] = data.s16_salida;
                            row[5] = data.s16_horas;
                            row[6] = data.s16_irefrigerio;
                            row[7] = data.s16_frefrigerio;
                            row[8] = data.s16_refh;
                            row[9] = data.s16_ingresor;
                            row[10] = data.s16_salidar;
                            row[11] = data.s16_horasr;
                            row[12] = data.s16_ht;
                            row[13] = data.s17_ingreso;
                            row[14] = data.s17_salida;
                            row[15] = data.s17_horas;
                            row[16] = data.s17_irefrigerio;
                            row[17] = data.s17_frefrigerio;
                            row[18] = data.s17_refh;
                            row[19] = data.s17_ingresor;
                            row[20] = data.s17_salidar;
                            row[21] = data.s17_horasr;
                            row[22] = data.s17_ht;

                            row[23] = data.s18_ingreso;
                            row[24] = data.s18_salida;
                            row[25] = data.s18_horas;
                            row[26] = data.s18_irefrigerio;
                            row[27] = data.s18_frefrigerio;
                            row[28] = data.s18_refh;
                            row[29] = data.s18_ingresor;
                            row[30] = data.s18_salidar;
                            row[31] = data.s18_horasr;
                            row[32] = data.s18_ht;
                            row[33] = data.s19_ingreso;
                            row[34] = data.s19_salida;
                            row[35] = data.s19_horas;
                            row[36] = data.s19_irefrigerio;
                            row[37] = data.s19_frefrigerio;
                            row[38] = data.s19_refh;
                            row[39] = data.s19_ingresor;
                            row[40] = data.s19_salidar;
                            row[41] = data.s19_horasr;
                            row[42] = data.s19_ht;
                            row[43] = data.s20_ingreso;
                            row[44] = data.s20_salida;
                            row[45] = data.s20_horas;
                            row[46] = data.s20_irefrigerio;
                            row[47] = data.s20_frefrigerio;
                            row[48] = data.s20_refh;
                            row[49] = data.s20_ingresor;
                            row[50] = data.s20_salidar;
                            row[51] = data.s20_horasr;
                            row[52] = data.s20_ht;
                            row[53] = data.s21_ingreso;
                            row[54] = data.s21_salida;
                            row[55] = data.s21_horas;
                            row[56] = data.s21_irefrigerio;
                            row[57] = data.s21_frefrigerio;
                            row[58] = data.s21_refh;
                            row[59] = data.s21_ingresor;
                            row[60] = data.s21_salidar;
                            row[61] = data.s21_horasr;
                            row[62] = data.s21_ht;
                            row[63] = data.s22_ingreso;
                            row[64] = data.s22_salida;
                            row[65] = data.s22_horas;
                            row[66] = data.s22_irefrigerio;
                            row[67] = data.s22_frefrigerio;
                            row[68] = data.s22_refh;
                            row[69] = data.s22_ingresor;
                            row[70] = data.s22_salidar;
                            row[71] = data.s22_horasr;
                            row[72] = data.s22_ht;
                            row[73] = data.s23_ingreso;
                            row[74] = data.s23_salida;
                            row[75] = data.s23_horas;
                            row[76] = data.s23_irefrigerio;
                            row[77] = data.s23_frefrigerio;
                            row[78] = data.s23_refh;
                            row[79] = data.s23_ingresor;
                            row[80] = data.s23_salidar;
                            row[81] = data.s23_horasr;
                            row[82] = data.s23_ht;
                            row[83] = data.s24_ingreso;
                            row[84] = data.s24_salida;
                            row[85] = data.s24_horas;
                            row[86] = data.s24_irefrigerio;
                            row[87] = data.s24_frefrigerio;
                            row[88] = data.s24_refh;
                            row[89] = data.s24_ingresor;
                            row[90] = data.s24_salidar;
                            row[91] = data.s24_horasr;
                            row[92] = data.s24_ht;
                            row[93] = data.s25_ingreso;
                            row[94] = data.s25_salida;
                            row[95] = data.s25_horas;
                            row[96] = data.s25_irefrigerio;
                            row[97] = data.s25_frefrigerio;
                            row[98] = data.s25_refh;
                            row[99] = data.s25_ingresor;
                            row[100] = data.s25_salidar;
                            row[101] = data.s25_horasr;
                            row[102] = data.s25_ht;
                            row[103] = data.s26_ingreso;
                            row[104] = data.s26_salida;
                            row[105] = data.s26_horas;
                            row[106] = data.s26_irefrigerio;
                            row[107] = data.s26_frefrigerio;
                            row[108] = data.s26_refh;
                            row[109] = data.s26_ingresor;
                            row[110] = data.s26_salidar;
                            row[111] = data.s26_horasr;
                            row[112] = data.s26_ht;
                            row[113] = data.s27_ingreso;
                            row[114] = data.s27_salida;
                            row[115] = data.s27_horas;
                            row[116] = data.s27_irefrigerio;
                            row[117] = data.s27_frefrigerio;
                            row[118] = data.s27_refh;
                            row[119] = data.s27_ingresor;
                            row[120] = data.s27_salidar;
                            row[121] = data.s27_horasr;
                            row[122] = data.s27_ht;
                            row[123] = data.s28_ingreso;
                            row[124] = data.s28_salida;
                            row[125] = data.s28_horas;
                            row[126] = data.s28_irefrigerio;
                            row[127] = data.s28_frefrigerio;
                            row[128] = data.s28_refh;
                            row[129] = data.s28_ingresor;
                            row[130] = data.s28_salidar;
                            row[131] = data.s28_horasr;
                            row[132] = data.s28_ht;
                            row[133] = data.s29_ingreso;
                            row[134] = data.s29_salida;
                            row[135] = data.s29_horas;
                            row[136] = data.s29_irefrigerio;
                            row[137] = data.s29_frefrigerio;
                            row[138] = data.s29_refh;
                            row[139] = data.s29_ingresor;
                            row[140] = data.s29_salidar;
                            row[141] = data.s29_horasr;
                            row[142] = data.s29_ht;
                            row[143] = data.s30_ingreso;
                            row[144] = data.s30_salida;
                            row[145] = data.s30_horas;
                            row[146] = data.s30_irefrigerio;
                            row[147] = data.s30_frefrigerio;
                            row[148] = data.s30_refh;
                            row[149] = data.s30_ingresor;
                            row[150] = data.s30_salidar;
                            row[151] = data.s30_horasr;
                            row[152] = data.s30_ht;
                            row[153] = data.s31_ingreso;
                            row[154] = data.s31_salida;
                            row[155] = data.s31_horas;
                            row[156] = data.s31_irefrigerio;
                            row[157] = data.s31_frefrigerio;
                            row[158] = data.s31_refh;
                            row[159] = data.s31_ingresor;
                            row[160] = data.s31_salidar;
                            row[161] = data.s31_horasr;
                            row[162] = data.s31_ht;
                            row[163] = data.s02_dia_horas;
                            row[164] = data.s02_jornales;

                            Dt.Rows.Add(row);

                        }
                    }




                    var memoryStream = new MemoryStream();
                    using (var excelPackage = new ExcelPackage(memoryStream))
                    {

                        var worksheet = excelPackage.Workbook.Worksheets.Add("Planilla " + Periodo);
                        using (ExcelRange Rng = worksheet.Cells["A2:N2"])
                        {
                            Rng.Value = "REPORTE DE ASISTENCIA DEL PERIODO " + Periodo;
                            Rng.Merge = true;
                            Rng.Style.Font.Size = 16;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Italic = true;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        using (ExcelRange Rng = worksheet.Cells["D3:M3"])
                        {
                            Rng.Value = "16";
                            Rng.Merge = true;
                            Rng.Style.Font.Size = 10;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Italic = false;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        using (ExcelRange Rng = worksheet.Cells["A3:AN1000"])
                        {
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            Rng.Style.Font.Size = 10;
                        }
                        worksheet.Cells["A4"].LoadFromDataTable(Dt, true, TableStyles.None);
                        worksheet.Cells["A4:AN4"].Style.Font.Bold = true;
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
        public async Task<ActionResult> ExcelExportMensual(string Periodo)
        {
            string varperiodo = Periodo.Substring(6);
            List<AsistenciaTempErp> lstEntidad = new List<AsistenciaTempErp>();
            List<AsistenciaTempErp> FileData = new List<AsistenciaTempErp>();
            AsistenciaTempErp busca = new AsistenciaTempErp()
            {
                PERIODO = Periodo
            };

            FileData = await GetPlanillonTotal(busca);
            if (FileData == null || !FileData.Any())
            {
                return null;
            }
            else
            {
                try
                {
                    DataTable Dt = new DataTable();
                    Dt.Columns.Add("NRO", typeof(Int32));
                    Dt.Columns.Add("DOCUMENTO", typeof(string));
                    Dt.Columns.Add("NOMBRES", typeof(string));
                    Dt.Columns.Add("IN 01", typeof(string));
                    Dt.Columns.Add("SA 01", typeof(string));
                    Dt.Columns.Add("HN 01", typeof(decimal));
                    Dt.Columns.Add("IN REFR 01", typeof(string));
                    Dt.Columns.Add("FIN REFR 01", typeof(string));
                    Dt.Columns.Add("HRF 01", typeof(decimal));
                    Dt.Columns.Add("REIN 01", typeof(string));
                    Dt.Columns.Add("RESA 01", typeof(string));
                    Dt.Columns.Add("HR 01", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 01", typeof(decimal));
                    Dt.Columns.Add("IN 02", typeof(string));
                    Dt.Columns.Add("SA 02", typeof(string));
                    Dt.Columns.Add("HN 02", typeof(decimal));
                    Dt.Columns.Add("IN REFR 02", typeof(string));
                    Dt.Columns.Add("FIN REFR 02", typeof(string));
                    Dt.Columns.Add("HRF 02", typeof(decimal));
                    Dt.Columns.Add("REIN 02", typeof(string));
                    Dt.Columns.Add("RESA 02", typeof(string));
                    Dt.Columns.Add("HR 02", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 02", typeof(decimal));
                    Dt.Columns.Add("IN 03", typeof(string));
                    Dt.Columns.Add("SA 03", typeof(string));
                    Dt.Columns.Add("HN 03", typeof(decimal));
                    Dt.Columns.Add("IN REFR 03", typeof(string));
                    Dt.Columns.Add("FIN REFR 03", typeof(string));
                    Dt.Columns.Add("HRF 03", typeof(decimal));
                    Dt.Columns.Add("REIN 03", typeof(string));
                    Dt.Columns.Add("RESA 03", typeof(string));
                    Dt.Columns.Add("HR 03", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 03", typeof(decimal));
                    Dt.Columns.Add("IN 04", typeof(string));
                    Dt.Columns.Add("SA 04", typeof(string));
                    Dt.Columns.Add("HN 04", typeof(decimal));
                    Dt.Columns.Add("IN REFR 04", typeof(string));
                    Dt.Columns.Add("FIN REFR 04", typeof(string));
                    Dt.Columns.Add("HRF 04", typeof(decimal));
                    Dt.Columns.Add("REIN 04", typeof(string));
                    Dt.Columns.Add("RESA 04", typeof(string));
                    Dt.Columns.Add("HR 04", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 04", typeof(decimal));
                    Dt.Columns.Add("IN 05", typeof(string));
                    Dt.Columns.Add("SA 05", typeof(string));
                    Dt.Columns.Add("HN 05", typeof(decimal));
                    Dt.Columns.Add("IN REFR 05", typeof(string));
                    Dt.Columns.Add("FIN REFR 05", typeof(string));
                    Dt.Columns.Add("HRF 05", typeof(decimal));
                    Dt.Columns.Add("REIN 05", typeof(string));
                    Dt.Columns.Add("RESA 05", typeof(string));
                    Dt.Columns.Add("HR 05", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 05", typeof(decimal));
                    Dt.Columns.Add("IN 06", typeof(string));
                    Dt.Columns.Add("SA 06", typeof(string));
                    Dt.Columns.Add("HN 06", typeof(decimal));
                    Dt.Columns.Add("IN REFR 06", typeof(string));
                    Dt.Columns.Add("FIN REFR 06", typeof(string));
                    Dt.Columns.Add("HRF 06", typeof(decimal));
                    Dt.Columns.Add("REIN 06", typeof(string));
                    Dt.Columns.Add("RESA 06", typeof(string));
                    Dt.Columns.Add("HR 06", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 06", typeof(decimal));
                    Dt.Columns.Add("IN 07", typeof(string));
                    Dt.Columns.Add("SA 07", typeof(string));
                    Dt.Columns.Add("HN 07", typeof(decimal));
                    Dt.Columns.Add("IN REFR 07", typeof(string));
                    Dt.Columns.Add("FIN REFR 07", typeof(string));
                    Dt.Columns.Add("HRF 07", typeof(decimal));
                    Dt.Columns.Add("REIN 07", typeof(string));
                    Dt.Columns.Add("RESA 07", typeof(string));
                    Dt.Columns.Add("HR 07", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 07", typeof(decimal));
                    Dt.Columns.Add("IN 08", typeof(string));
                    Dt.Columns.Add("SA 08", typeof(string));
                    Dt.Columns.Add("HN 08", typeof(decimal));
                    Dt.Columns.Add("IN REFR 08", typeof(string));
                    Dt.Columns.Add("FIN REFR 08", typeof(string));
                    Dt.Columns.Add("HRF 08", typeof(decimal));
                    Dt.Columns.Add("REIN 08", typeof(string));
                    Dt.Columns.Add("RESA 08", typeof(string));
                    Dt.Columns.Add("HR 08", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 08", typeof(decimal));
                    Dt.Columns.Add("IN 09", typeof(string));
                    Dt.Columns.Add("SA 09", typeof(string));
                    Dt.Columns.Add("HN 09", typeof(decimal));
                    Dt.Columns.Add("IN REFR 09", typeof(string));
                    Dt.Columns.Add("FIN REFR 09", typeof(string));
                    Dt.Columns.Add("HRF 09", typeof(decimal));
                    Dt.Columns.Add("REIN 09", typeof(string));
                    Dt.Columns.Add("RESA 09", typeof(string));
                    Dt.Columns.Add("HR 09", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 09", typeof(decimal));
                    Dt.Columns.Add("IN 10", typeof(string));
                    Dt.Columns.Add("SA 10", typeof(string));
                    Dt.Columns.Add("HN 10", typeof(decimal));
                    Dt.Columns.Add("IN REFR 10", typeof(string));
                    Dt.Columns.Add("FIN REFR 10", typeof(string));
                    Dt.Columns.Add("HRF 10", typeof(decimal));
                    Dt.Columns.Add("REIN 10", typeof(string));
                    Dt.Columns.Add("RESA 10", typeof(string));
                    Dt.Columns.Add("HR 10", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 10", typeof(decimal));
                    Dt.Columns.Add("IN 11", typeof(string));
                    Dt.Columns.Add("SA 11", typeof(string));
                    Dt.Columns.Add("HN 11", typeof(decimal));
                    Dt.Columns.Add("IN REFR 11", typeof(string));
                    Dt.Columns.Add("FIN REFR 11", typeof(string));
                    Dt.Columns.Add("HRF 11", typeof(decimal));
                    Dt.Columns.Add("REIN 11", typeof(string));
                    Dt.Columns.Add("RESA 11", typeof(string));
                    Dt.Columns.Add("HR 11", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 11", typeof(decimal));
                    Dt.Columns.Add("IN 12", typeof(string));
                    Dt.Columns.Add("SA 12", typeof(string));
                    Dt.Columns.Add("HN 12", typeof(decimal));
                    Dt.Columns.Add("IN REFR 12", typeof(string));
                    Dt.Columns.Add("FIN REFR 12", typeof(string));
                    Dt.Columns.Add("HRF 12", typeof(decimal));
                    Dt.Columns.Add("REIN 12", typeof(string));
                    Dt.Columns.Add("RESA 12", typeof(string));
                    Dt.Columns.Add("HR 12", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 12", typeof(decimal));
                    Dt.Columns.Add("IN 13", typeof(string));
                    Dt.Columns.Add("SA 13", typeof(string));
                    Dt.Columns.Add("HN 13", typeof(decimal));
                    Dt.Columns.Add("IN REFR 13", typeof(string));
                    Dt.Columns.Add("FIN REFR 13", typeof(string));
                    Dt.Columns.Add("HRF 13", typeof(decimal));
                    Dt.Columns.Add("REIN 13", typeof(string));
                    Dt.Columns.Add("RESA 13", typeof(string));
                    Dt.Columns.Add("HR 13", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 13", typeof(decimal));
                    Dt.Columns.Add("IN 14", typeof(string));
                    Dt.Columns.Add("SA 14", typeof(string));
                    Dt.Columns.Add("HN 14", typeof(decimal));
                    Dt.Columns.Add("IN REFR 14", typeof(string));
                    Dt.Columns.Add("FIN REFR 14", typeof(string));
                    Dt.Columns.Add("HRF 14", typeof(decimal));
                    Dt.Columns.Add("REIN 14", typeof(string));
                    Dt.Columns.Add("RESA 14", typeof(string));
                    Dt.Columns.Add("HR 14", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 14", typeof(decimal));
                    Dt.Columns.Add("IN 15", typeof(string));
                    Dt.Columns.Add("SA 15", typeof(string));
                    Dt.Columns.Add("HN 15", typeof(decimal));
                    Dt.Columns.Add("IN REFR 15", typeof(string));
                    Dt.Columns.Add("FIN REFR 15", typeof(string));
                    Dt.Columns.Add("HRF 15", typeof(decimal));
                    Dt.Columns.Add("REIN 15", typeof(string));
                    Dt.Columns.Add("RESA 15", typeof(string));
                    Dt.Columns.Add("HR 15", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 15", typeof(decimal));

                    Dt.Columns.Add("IN 16", typeof(string));
                    Dt.Columns.Add("SA 16", typeof(string));
                    Dt.Columns.Add("HN 16", typeof(decimal));
                    Dt.Columns.Add("IN REFR 16", typeof(string));
                    Dt.Columns.Add("FIN REFR 16", typeof(string));
                    Dt.Columns.Add("HRF 16", typeof(decimal));
                    Dt.Columns.Add("REIN 16", typeof(string));
                    Dt.Columns.Add("RESA 16", typeof(string));
                    Dt.Columns.Add("HR 16", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 16", typeof(decimal));
                    Dt.Columns.Add("IN 17", typeof(string));
                    Dt.Columns.Add("SA 17", typeof(string));
                    Dt.Columns.Add("HN 17", typeof(decimal));
                    Dt.Columns.Add("IN REFR 17", typeof(string));
                    Dt.Columns.Add("FIN REFR 17", typeof(string));
                    Dt.Columns.Add("HRF 17", typeof(decimal));
                    Dt.Columns.Add("REIN 17", typeof(string));
                    Dt.Columns.Add("RESA 17", typeof(string));
                    Dt.Columns.Add("HR 17", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 17", typeof(decimal));
                    Dt.Columns.Add("IN 18", typeof(string));
                    Dt.Columns.Add("SA 18", typeof(string));
                    Dt.Columns.Add("HN 18", typeof(decimal));
                    Dt.Columns.Add("IN REFR 18", typeof(string));
                    Dt.Columns.Add("FIN REFR 18", typeof(string));
                    Dt.Columns.Add("HRF 18", typeof(decimal));
                    Dt.Columns.Add("REIN 18", typeof(string));
                    Dt.Columns.Add("RESA 18", typeof(string));
                    Dt.Columns.Add("HR 18", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 18", typeof(decimal));
                    Dt.Columns.Add("IN 19", typeof(string));
                    Dt.Columns.Add("SA 19", typeof(string));
                    Dt.Columns.Add("HN 19", typeof(decimal));
                    Dt.Columns.Add("IN REFR 19", typeof(string));
                    Dt.Columns.Add("FIN REFR 19", typeof(string));
                    Dt.Columns.Add("HRF 19", typeof(decimal));
                    Dt.Columns.Add("REIN 19", typeof(string));
                    Dt.Columns.Add("RESA 19", typeof(string));
                    Dt.Columns.Add("HR 19", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 19", typeof(decimal));
                    Dt.Columns.Add("IN 20", typeof(string));
                    Dt.Columns.Add("SA 20", typeof(string));
                    Dt.Columns.Add("HN 20", typeof(decimal));
                    Dt.Columns.Add("IN REFR 20", typeof(string));
                    Dt.Columns.Add("FIN REFR 20", typeof(string));
                    Dt.Columns.Add("HRF 20", typeof(decimal));
                    Dt.Columns.Add("REIN 20", typeof(string));
                    Dt.Columns.Add("RESA 20", typeof(string));
                    Dt.Columns.Add("HR 20", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 20", typeof(decimal));
                    Dt.Columns.Add("IN 21", typeof(string));
                    Dt.Columns.Add("SA 21", typeof(string));
                    Dt.Columns.Add("HN 21", typeof(decimal));
                    Dt.Columns.Add("IN REFR 21", typeof(string));
                    Dt.Columns.Add("FIN REFR 21", typeof(string));
                    Dt.Columns.Add("HRF 21", typeof(decimal));
                    Dt.Columns.Add("REIN 21", typeof(string));
                    Dt.Columns.Add("RESA 21", typeof(string));
                    Dt.Columns.Add("HR 21", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 21", typeof(decimal));
                    Dt.Columns.Add("IN 22", typeof(string));
                    Dt.Columns.Add("SA 22", typeof(string));
                    Dt.Columns.Add("HN 22", typeof(decimal));
                    Dt.Columns.Add("IN REFR 22", typeof(string));
                    Dt.Columns.Add("FIN REFR 22", typeof(string));
                    Dt.Columns.Add("HRF 22", typeof(decimal));
                    Dt.Columns.Add("REIN 22", typeof(string));
                    Dt.Columns.Add("RESA 22", typeof(string));
                    Dt.Columns.Add("HR 22", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 22", typeof(decimal));
                    Dt.Columns.Add("IN 23", typeof(string));
                    Dt.Columns.Add("SA 23", typeof(string));
                    Dt.Columns.Add("HN 23", typeof(decimal));
                    Dt.Columns.Add("IN REFR 23", typeof(string));
                    Dt.Columns.Add("FIN REFR 23", typeof(string));
                    Dt.Columns.Add("HRF 23", typeof(decimal));
                    Dt.Columns.Add("REIN 23", typeof(string));
                    Dt.Columns.Add("RESA 23", typeof(string));
                    Dt.Columns.Add("HR 23", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 23", typeof(decimal));
                    Dt.Columns.Add("IN 24", typeof(string));
                    Dt.Columns.Add("SA 24", typeof(string));
                    Dt.Columns.Add("HN 24", typeof(decimal));
                    Dt.Columns.Add("IN REFR 24", typeof(string));
                    Dt.Columns.Add("FIN REFR 24", typeof(string));
                    Dt.Columns.Add("HRF 24", typeof(decimal));
                    Dt.Columns.Add("REIN 24", typeof(string));
                    Dt.Columns.Add("RESA 24", typeof(string));
                    Dt.Columns.Add("HR 24", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 24", typeof(decimal));
                    Dt.Columns.Add("IN 25", typeof(string));
                    Dt.Columns.Add("SA 25", typeof(string));
                    Dt.Columns.Add("HN 25", typeof(decimal));
                    Dt.Columns.Add("IN REFR 25", typeof(string));
                    Dt.Columns.Add("FIN REFR 25", typeof(string));
                    Dt.Columns.Add("HRF 25", typeof(decimal));
                    Dt.Columns.Add("REIN 25", typeof(string));
                    Dt.Columns.Add("RESA 25", typeof(string));
                    Dt.Columns.Add("HR 25", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 25", typeof(decimal));
                    Dt.Columns.Add("IN 26", typeof(string));
                    Dt.Columns.Add("SA 26", typeof(string));
                    Dt.Columns.Add("HN 26", typeof(decimal));
                    Dt.Columns.Add("IN REFR 26", typeof(string));
                    Dt.Columns.Add("FIN REFR 26", typeof(string));
                    Dt.Columns.Add("HRF 26", typeof(decimal));
                    Dt.Columns.Add("REIN 26", typeof(string));
                    Dt.Columns.Add("RESA 26", typeof(string));
                    Dt.Columns.Add("HR 26", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 26", typeof(decimal));
                    Dt.Columns.Add("IN 27", typeof(string));
                    Dt.Columns.Add("SA 27", typeof(string));
                    Dt.Columns.Add("HN 27", typeof(decimal));
                    Dt.Columns.Add("IN REFR 27", typeof(string));
                    Dt.Columns.Add("FIN REFR 27", typeof(string));
                    Dt.Columns.Add("HRF 27", typeof(decimal));
                    Dt.Columns.Add("REIN 27", typeof(string));
                    Dt.Columns.Add("RESA 27", typeof(string));
                    Dt.Columns.Add("HR 27", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 27", typeof(decimal));
                    Dt.Columns.Add("IN 28", typeof(string));
                    Dt.Columns.Add("SA 28", typeof(string));
                    Dt.Columns.Add("HN 28", typeof(decimal));
                    Dt.Columns.Add("IN REFR 28", typeof(string));
                    Dt.Columns.Add("FIN REFR 28", typeof(string));
                    Dt.Columns.Add("HRF 28", typeof(decimal));
                    Dt.Columns.Add("REIN 28", typeof(string));
                    Dt.Columns.Add("RESA 28", typeof(string));
                    Dt.Columns.Add("HR 28", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 28", typeof(decimal));
                    Dt.Columns.Add("IN 29", typeof(string));
                    Dt.Columns.Add("SA 29", typeof(string));
                    Dt.Columns.Add("HN 29", typeof(decimal));
                    Dt.Columns.Add("IN REFR 29", typeof(string));
                    Dt.Columns.Add("FIN REFR 29", typeof(string));
                    Dt.Columns.Add("HRF 29", typeof(decimal));
                    Dt.Columns.Add("REIN 29", typeof(string));
                    Dt.Columns.Add("RESA 29", typeof(string));
                    Dt.Columns.Add("HR 29", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 29", typeof(decimal));
                    Dt.Columns.Add("IN 30", typeof(string));
                    Dt.Columns.Add("SA 30", typeof(string));
                    Dt.Columns.Add("HN 30", typeof(decimal));
                    Dt.Columns.Add("IN REFR 30", typeof(string));
                    Dt.Columns.Add("FIN REFR 30", typeof(string));
                    Dt.Columns.Add("HRF 30", typeof(decimal));
                    Dt.Columns.Add("REIN 30", typeof(string));
                    Dt.Columns.Add("RESA 30", typeof(string));
                    Dt.Columns.Add("HR 30", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 30", typeof(decimal));
                    Dt.Columns.Add("IN 31", typeof(string));
                    Dt.Columns.Add("SA 31", typeof(string));
                    Dt.Columns.Add("HN 31", typeof(decimal));
                    Dt.Columns.Add("IN REFR 31", typeof(string));
                    Dt.Columns.Add("FIN REFR 31", typeof(string));
                    Dt.Columns.Add("HRF 31", typeof(decimal));
                    Dt.Columns.Add("REIN 31", typeof(string));
                    Dt.Columns.Add("RESA 31", typeof(string));
                    Dt.Columns.Add("HR 31", typeof(decimal));
                    Dt.Columns.Add("HOR TOT 31", typeof(decimal));

                    Dt.Columns.Add("HORAS", typeof(decimal));
                    Dt.Columns.Add("TOTAL JORNALES", typeof(decimal));
                    foreach (var data in FileData)
                    {
                        DataRow row = Dt.NewRow();
                        row[0] = data.itemrecord;
                        row[1] = data.IDCODIGOGENERAL;
                        row[2] = data.NOMBRES;
                        row[3] = data.s01_ingreso;
                        row[4] = data.s01_salida;
                        row[5] = data.s01_horas;
                        row[6] = data.s01_irefrigerio;
                        row[7] = data.s01_frefrigerio;
                        row[8] = data.s01_refh;
                        row[9] = data.s01_ingresor;
                        row[10] = data.s01_salidar;
                        row[11] = data.s01_horasr;
                        row[12] = data.s01_ht;
                        row[13] = data.s02_ingreso;
                        row[14] = data.s02_salida;
                        row[15] = data.s02_horas;
                        row[16] = data.s02_irefrigerio;
                        row[17] = data.s02_frefrigerio;
                        row[18] = data.s02_refh;
                        row[19] = data.s02_ingresor;
                        row[20] = data.s02_salidar;
                        row[21] = data.s02_horasr;
                        row[22] = data.s02_ht;

                        row[23] = data.s03_ingreso;
                        row[24] = data.s03_salida;
                        row[25] = data.s03_horas;
                        row[26] = data.s03_irefrigerio;
                        row[27] = data.s03_frefrigerio;
                        row[28] = data.s03_refh;
                        row[29] = data.s03_ingresor;
                        row[30] = data.s03_salidar;
                        row[31] = data.s03_horasr;
                        row[32] = data.s03_ht;
                        row[33] = data.s04_ingreso;
                        row[34] = data.s04_salida;
                        row[35] = data.s04_horas;
                        row[36] = data.s04_irefrigerio;
                        row[37] = data.s04_frefrigerio;
                        row[38] = data.s04_refh;
                        row[39] = data.s04_ingresor;
                        row[40] = data.s04_salidar;
                        row[41] = data.s04_horasr;
                        row[42] = data.s04_ht;
                        row[43] = data.s05_ingreso;
                        row[44] = data.s05_salida;
                        row[45] = data.s05_horas;
                        row[46] = data.s05_irefrigerio;
                        row[47] = data.s05_frefrigerio;
                        row[48] = data.s05_refh;
                        row[49] = data.s05_ingresor;
                        row[50] = data.s05_salidar;
                        row[51] = data.s05_horasr;
                        row[52] = data.s05_ht;
                        row[53] = data.s06_ingreso;
                        row[54] = data.s06_salida;
                        row[55] = data.s06_horas;
                        row[56] = data.s06_irefrigerio;
                        row[57] = data.s06_frefrigerio;
                        row[58] = data.s06_refh;
                        row[59] = data.s06_ingresor;
                        row[60] = data.s06_salidar;
                        row[61] = data.s06_horasr;
                        row[62] = data.s06_ht;
                        row[63] = data.s07_ingreso;
                        row[64] = data.s07_salida;
                        row[65] = data.s07_horas;
                        row[66] = data.s07_irefrigerio;
                        row[67] = data.s07_frefrigerio;
                        row[68] = data.s07_refh;
                        row[69] = data.s07_ingresor;
                        row[70] = data.s07_salidar;
                        row[71] = data.s07_horasr;
                        row[72] = data.s07_ht;
                        row[73] = data.s08_ingreso;
                        row[74] = data.s08_salida;
                        row[75] = data.s08_horas;
                        row[76] = data.s08_irefrigerio;
                        row[77] = data.s08_frefrigerio;
                        row[78] = data.s08_refh;
                        row[79] = data.s08_ingresor;
                        row[80] = data.s08_salidar;
                        row[81] = data.s08_horasr;
                        row[82] = data.s08_ht;
                        row[83] = data.s09_ingreso;
                        row[84] = data.s09_salida;
                        row[85] = data.s09_horas;
                        row[86] = data.s09_irefrigerio;
                        row[87] = data.s09_frefrigerio;
                        row[88] = data.s09_refh;
                        row[89] = data.s09_ingresor;
                        row[90] = data.s09_salidar;
                        row[91] = data.s09_horasr;
                        row[92] = data.s09_ht;
                        row[93] = data.s10_ingreso;
                        row[94] = data.s10_salida;
                        row[95] = data.s10_horas;
                        row[96] = data.s10_irefrigerio;
                        row[97] = data.s10_frefrigerio;
                        row[98] = data.s10_refh;
                        row[99] = data.s10_ingresor;
                        row[100] = data.s10_salidar;
                        row[101] = data.s10_horasr;
                        row[102] = data.s10_ht;
                        row[103] = data.s11_ingreso;
                        row[104] = data.s11_salida;
                        row[105] = data.s11_horas;
                        row[106] = data.s11_irefrigerio;
                        row[107] = data.s11_frefrigerio;
                        row[108] = data.s11_refh;
                        row[109] = data.s11_ingresor;
                        row[110] = data.s11_salidar;
                        row[111] = data.s11_horasr;
                        row[112] = data.s11_ht;
                        row[113] = data.s12_ingreso;
                        row[114] = data.s12_salida;
                        row[115] = data.s12_horas;
                        row[116] = data.s12_irefrigerio;
                        row[117] = data.s12_frefrigerio;
                        row[118] = data.s12_refh;
                        row[119] = data.s12_ingresor;
                        row[120] = data.s12_salidar;
                        row[121] = data.s12_horasr;
                        row[122] = data.s12_ht;
                        row[123] = data.s13_ingreso;
                        row[124] = data.s13_salida;
                        row[125] = data.s13_horas;
                        row[126] = data.s13_irefrigerio;
                        row[127] = data.s13_frefrigerio;
                        row[128] = data.s13_refh;
                        row[129] = data.s13_ingresor;
                        row[130] = data.s13_salidar;
                        row[131] = data.s13_horasr;
                        row[132] = data.s13_ht;
                        row[133] = data.s14_ingreso;
                        row[134] = data.s14_salida;
                        row[135] = data.s14_horas;
                        row[136] = data.s14_irefrigerio;
                        row[137] = data.s14_frefrigerio;
                        row[138] = data.s14_refh;
                        row[139] = data.s14_ingresor;
                        row[140] = data.s14_salidar;
                        row[141] = data.s14_horasr;
                        row[142] = data.s14_ht;
                        row[143] = data.s15_ingreso;
                        row[144] = data.s15_salida;
                        row[145] = data.s15_horas;
                        row[146] = data.s15_irefrigerio;
                        row[147] = data.s15_frefrigerio;
                        row[148] = data.s15_refh;
                        row[149] = data.s15_ingresor;
                        row[150] = data.s15_salidar;
                        row[151] = data.s15_horasr;
                        row[152] = data.s15_ht;
                        row[153] = data.s16_ingreso;
                        row[154] = data.s16_salida;
                        row[155] = data.s16_horas;
                        row[156] = data.s16_irefrigerio;
                        row[157] = data.s16_frefrigerio;
                        row[158] = data.s16_refh;
                        row[159] = data.s16_ingresor;
                        row[160] = data.s16_salidar;
                        row[161] = data.s16_horasr;
                        row[162] = data.s16_ht;
                        row[163] = data.s17_ingreso;
                        row[164] = data.s17_salida;
                        row[165] = data.s17_horas;
                        row[166] = data.s17_irefrigerio;
                        row[167] = data.s17_frefrigerio;
                        row[168] = data.s17_refh;
                        row[169] = data.s17_ingresor;
                        row[170] = data.s17_salidar;
                        row[171] = data.s17_horasr;
                        row[172] = data.s17_ht;
                        row[173] = data.s18_ingreso;
                        row[174] = data.s18_salida;
                        row[175] = data.s18_horas;
                        row[176] = data.s18_irefrigerio;
                        row[177] = data.s18_frefrigerio;
                        row[178] = data.s18_refh;
                        row[179] = data.s18_ingresor;
                        row[180] = data.s18_salidar;
                        row[181] = data.s18_horasr;
                        row[182] = data.s18_ht;
                        row[183] = data.s19_ingreso;
                        row[184] = data.s19_salida;
                        row[185] = data.s19_horas;
                        row[186] = data.s19_irefrigerio;
                        row[187] = data.s19_frefrigerio;
                        row[188] = data.s19_refh;
                        row[189] = data.s19_ingresor;
                        row[190] = data.s19_salidar;
                        row[191] = data.s19_horasr;
                        row[192] = data.s19_ht;
                        row[193] = data.s20_ingreso;
                        row[194] = data.s20_salida;
                        row[195] = data.s20_horas;
                        row[196] = data.s20_irefrigerio;
                        row[197] = data.s20_frefrigerio;
                        row[198] = data.s20_refh;
                        row[199] = data.s20_ingresor;
                        row[200] = data.s20_salidar;
                        row[201] = data.s20_horasr;
                        row[202] = data.s20_ht;
                        row[203] = data.s21_ingreso;
                        row[204] = data.s21_salida;
                        row[205] = data.s21_horas;
                        row[206] = data.s21_irefrigerio;
                        row[207] = data.s21_frefrigerio;
                        row[208] = data.s21_refh;
                        row[209] = data.s21_ingresor;
                        row[210] = data.s21_salidar;
                        row[211] = data.s21_horasr;
                        row[212] = data.s21_ht;
                        row[213] = data.s22_ingreso;
                        row[214] = data.s22_salida;
                        row[215] = data.s22_horas;
                        row[216] = data.s22_irefrigerio;
                        row[217] = data.s22_frefrigerio;
                        row[218] = data.s22_refh;
                        row[219] = data.s22_ingresor;
                        row[220] = data.s22_salidar;
                        row[221] = data.s22_horasr;
                        row[222] = data.s22_ht;
                        row[223] = data.s23_ingreso;
                        row[224] = data.s23_salida;
                        row[225] = data.s23_horas;
                        row[226] = data.s23_irefrigerio;
                        row[227] = data.s23_frefrigerio;
                        row[228] = data.s23_refh;
                        row[229] = data.s23_ingresor;
                        row[230] = data.s23_salidar;
                        row[231] = data.s23_horasr;
                        row[232] = data.s23_ht;
                        row[233] = data.s24_ingreso;
                        row[234] = data.s24_salida;
                        row[235] = data.s24_horas;
                        row[236] = data.s24_irefrigerio;
                        row[237] = data.s24_frefrigerio;
                        row[238] = data.s24_refh;
                        row[239] = data.s24_ingresor;
                        row[240] = data.s24_salidar;
                        row[241] = data.s24_horasr;
                        row[242] = data.s24_ht;
                        row[243] = data.s25_ingreso;
                        row[244] = data.s25_salida;
                        row[245] = data.s25_horas;
                        row[246] = data.s25_irefrigerio;
                        row[247] = data.s25_frefrigerio;
                        row[248] = data.s25_refh;
                        row[249] = data.s25_ingresor;
                        row[250] = data.s25_salidar;
                        row[251] = data.s25_horasr;
                        row[252] = data.s25_ht;
                        row[253] = data.s26_ingreso;
                        row[254] = data.s26_salida;
                        row[255] = data.s26_horas;
                        row[256] = data.s26_irefrigerio;
                        row[257] = data.s26_frefrigerio;
                        row[258] = data.s26_refh;
                        row[259] = data.s26_ingresor;
                        row[260] = data.s26_salidar;
                        row[261] = data.s26_horasr;
                        row[262] = data.s26_ht;
                        row[263] = data.s27_ingreso;
                        row[264] = data.s27_salida;
                        row[265] = data.s27_horas;
                        row[266] = data.s27_irefrigerio;
                        row[267] = data.s27_frefrigerio;
                        row[268] = data.s27_refh;
                        row[269] = data.s27_ingresor;
                        row[270] = data.s27_salidar;
                        row[271] = data.s27_horasr;
                        row[272] = data.s27_ht;
                        row[273] = data.s28_ingreso;
                        row[274] = data.s28_salida;
                        row[275] = data.s28_horas;
                        row[276] = data.s28_irefrigerio;
                        row[277] = data.s28_frefrigerio;
                        row[278] = data.s28_refh;
                        row[279] = data.s28_ingresor;
                        row[280] = data.s28_salidar;
                        row[281] = data.s28_horasr;
                        row[282] = data.s28_ht;
                        row[283] = data.s29_ingreso;
                        row[284] = data.s29_salida;
                        row[285] = data.s29_horas;
                        row[286] = data.s29_irefrigerio;
                        row[287] = data.s29_frefrigerio;
                        row[288] = data.s29_refh;
                        row[289] = data.s29_ingresor;
                        row[290] = data.s29_salidar;
                        row[291] = data.s29_horasr;
                        row[292] = data.s29_ht;
                        row[293] = data.s30_ingreso;
                        row[294] = data.s30_salida;
                        row[295] = data.s30_horas;
                        row[296] = data.s30_irefrigerio;
                        row[297] = data.s30_frefrigerio;
                        row[298] = data.s30_refh;
                        row[299] = data.s30_ingresor;
                        row[300] = data.s30_salidar;
                        row[301] = data.s30_horasr;
                        row[302] = data.s30_ht;
                        row[303] = data.s31_ingreso;
                        row[304] = data.s31_salida;
                        row[305] = data.s31_horas;
                        row[306] = data.s31_irefrigerio;
                        row[307] = data.s31_frefrigerio;
                        row[308] = data.s31_refh;
                        row[309] = data.s31_ingresor;
                        row[310] = data.s31_salidar;
                        row[311] = data.s31_horasr;
                        row[312] = data.s31_ht;
                        row[313] = data.s01_dia_horas + data.s02_dia_horas;
                        row[314] = data.s01_jornales + data.s02_jornales;


                        Dt.Rows.Add(row);

                    }


                    var memoryStream = new MemoryStream();
                    using (var excelPackage = new ExcelPackage(memoryStream))
                    {

                        var worksheet = excelPackage.Workbook.Worksheets.Add("Planilla " + Periodo);
                        using (ExcelRange Rng = worksheet.Cells["A2:N2"])
                        {
                            Rng.Value = "REPORTE DE ASISTENCIA DEL PERIODO " + Periodo;
                            Rng.Merge = true;
                            Rng.Style.Font.Size = 16;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Italic = true;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        using (ExcelRange Rng = worksheet.Cells["D3:M3"])
                        {
                            Rng.Value = "16";
                            Rng.Merge = true;
                            Rng.Style.Font.Size = 10;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Italic = false;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        using (ExcelRange Rng = worksheet.Cells["A3:LC4000"])
                        {
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            Rng.Style.Font.Size = 10;
                        }
                        worksheet.Cells["A4"].LoadFromDataTable(Dt, true, TableStyles.None);
                        worksheet.Cells["A4:LC4"].Style.Font.Bold = true;
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

        [HttpPost]
        public async Task<JsonResult> EliminaControlIngreso(string IDCONTROLINGRESO)
        {
            List<ControlIngresoErp> _lentidad = new List<ControlIngresoErp>();
            ControlIngresoErp _entidad = new ControlIngresoErp();

            ControlIngresoErp busca = new ControlIngresoErp()
            {
                IDCONTROLINGRESO = IDCONTROLINGRESO.Trim()
            };

            if (IDCONTROLINGRESO.Trim() != "" && IDCONTROLINGRESO.Trim().Length != 0)
            {
                try
                {
                    _lentidad = await GetControlIngresoErpAll();
                    if (_lentidad != null)
                    {
                        _entidad = _lentidad.FirstOrDefault(x => x.IDCONTROLINGRESO.Trim() == IDCONTROLINGRESO.Trim());
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

            string iddeleted = "";
            if (_entidad != null)
            {
                try
                {
                    ControlIngresoErp id = new ControlIngresoErp { IDCONTROLINGRESO = _entidad.IDCONTROLINGRESO };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_control_ingresoDelete";
                    var response = await client.PostAsJsonAsync(metodo, id);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "")
                    {
                        iddeleted = respuesta.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(iddeleted, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EliminaControlIngresodetalle(int id_tabla)
        {
            List<ControlIngresoDetalleErp> _lentidad = new List<ControlIngresoDetalleErp>();
            ControlIngresoDetalleErp _entidad = new ControlIngresoDetalleErp();

            ControlIngresoDetalleErp busca = new ControlIngresoDetalleErp()
            {
                id_tabla = id_tabla
            };
            string iddeleted = "";
            if (id_tabla > 0)
            {
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_control_ingreso_detalleDelete";
                    var response = await client.PostAsJsonAsync(metodo, busca);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "")
                    {
                        iddeleted = respuesta.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

            return Json(iddeleted, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GuardarAdjuntoTareo()
        {
            string urlftp = "";
            string periodo = "";
            try
            {
                string iIDCONTROLINGRESO = "";
                string folder = System.Configuration.ConfigurationManager.AppSettings["Ftptareos"];
                iIDCONTROLINGRESO = ControllerContext.HttpContext.Request.Files.Keys[1].ToString();
                var existe = await GetDatosCabecera(new ControlIngresoErp()
                { IDCONTROLINGRESO = iIDCONTROLINGRESO.Trim() });
                if (existe != null)
                {
                    periodo = existe.PERIODO.ToString();
                }
                HttpFileCollectionBase files = Request.Files;
                if (files.Count == 2)
                {
                    HttpPostedFileBase file = files[0];
                    iIDCONTROLINGRESO = files.AllKeys[1].ToString();
                    urlftp = UploadFtp(file, iIDCONTROLINGRESO, folder, iIDCONTROLINGRESO, periodo);
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

        private string UploadFtp(HttpPostedFileBase file, string idCodigogeneral, string folder, string nombrenormal, string periodo)
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
                    uploadfilename = nombrenormal + "-" + file.FileName.ToString(); // file.FileName;
                }

                var username = System.Configuration.ConfigurationManager.AppSettings["Ftpuser"];
                var password = System.Configuration.ConfigurationManager.AppSettings["Ftppass"];
                string ftpurlfolder = String.Format("{0}/{1}", uploadurl + folder + "/" + periodo, "");

                bool result = FtpDirectoryExists(ftpurlfolder, username, password);


                Stream streamObj = file.InputStream;
                byte[] buffer = new byte[file.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                streamObj = null;
                string ftpurl = String.Format("{0}/{1}", uploadurl + folder + "/" + periodo, uploadfilename);
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

        public bool FtpDirectoryExists(string directoryPath, string ftpUser, string ftpPassword)
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

        [HttpPost]
        public async Task<JsonResult> UpdateEstadoControlIngreso(string IDCONTROLINGRESO, string IDESTADO, string ficha_tareo)
        {
            List<ControlIngresoErp> _lentidad = new List<ControlIngresoErp>();
            ControlIngresoErp _entidad = new ControlIngresoErp();

            ControlIngresoErp busca = new ControlIngresoErp()
            {
                IDCONTROLINGRESO = IDCONTROLINGRESO.Trim()
            };

            if (IDCONTROLINGRESO.Trim() != "" && IDCONTROLINGRESO.Trim().Length != 0)
            {
                try
                {
                    _lentidad = await GetControlIngresoErpAll();
                    if (_lentidad != null)
                    {
                        _entidad = _lentidad.FirstOrDefault(x => x.IDCONTROLINGRESO.Trim() == IDCONTROLINGRESO.Trim());
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

            string idupdated = "";
            if (_entidad != null)
            {
                try
                {
                    int indexo = ficha_tareo.IndexOf("/tareos/");
                    string sadjunto = "";
                    try
                    {
                        var found = ficha_tareo.Substring(0, 10).Contains("ocalhost//");
                        if (found)
                        {
                            sadjunto = ficha_tareo;
                        }
                        else
                        {
                            sadjunto = ficha_tareo.Substring(indexo + 8);
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    ControlIngresoErp id = new ControlIngresoErp { IDCONTROLINGRESO = _entidad.IDCONTROLINGRESO, IDESTADO = IDESTADO, ficha_tareo = sadjunto };
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_control_ingresoUpdateEstado";
                    var response = await client.PostAsJsonAsync(metodo, id);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "")
                    {
                        idupdated = respuesta.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

            }
            return Json(idupdated, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> EditMarcacion(string IDCONTROLINGRESO, int IDTABLA, int ITEM)
        {

            ViewBag.IDCONTROLINGRESO = IDCONTROLINGRESO;
            ViewBag.IDTABLA = IDTABLA;
            ViewBag.ITEM = ITEM;
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> GuardaMarcacion(int id_tabla, string MARCACION)
        {
            List<ControlIngresoDetalleErp> _lentidad = new List<ControlIngresoDetalleErp>();
            ControlIngresoDetalleErp _entidad = new ControlIngresoDetalleErp();

            ControlIngresoDetalleErp busca = new ControlIngresoDetalleErp()
            {
                id_tabla = id_tabla,
                MARCACION = MARCACION
            };
            string iddeleted = "";
            if (id_tabla > 0)
            {
                try
                {
                    var client = new HttpClient();
                    string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                    client.BaseAddress = new Uri(connectionInfo);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string metodo = "";
                    metodo = "Personalerp/t_control_ingreso_detalleUpdateMarcacion";
                    var response = await client.PostAsJsonAsync(metodo, busca);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "")
                    {
                        iddeleted = respuesta.Result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

            return Json(iddeleted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ListHoras(string EstaFilt)
        {
            List<AsistenciaHorasErp> lstEntidad = null;
            string sEstaFilt = EstaFilt.Substring(0, 6);
            AsistenciaHorasErp busca = new AsistenciaHorasErp()
            {
                PERIODO = sEstaFilt
            };

            lstEntidad = await GetPlanillonHoras(busca);
            if (lstEntidad == null || !lstEntidad.Any())
            {
                lstEntidad = new List<AsistenciaHorasErp>();
            }

            //string quincex = "";
            //try
            //{
            //    quincex = EstaFilt.Substring(6);
            //}
            //catch (Exception e)
            //{

            //}
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.QUINCEX = "";
            ViewBag.PeriodoSelected = EstaFilt;
            return PartialView(lstEntidad.OrderBy(X => X.itemrecord).ToList());
        }

        private async Task<List<AsistenciaHorasErp>> GetPlanillonHoras(AsistenciaHorasErp busca)
        {
            List<AsistenciaHorasErp> lstEntidad = new List<AsistenciaHorasErp>();
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Personalerp/t_asistencia_tempSelectByPeriodoExtras", busca);

                var model = response.Content.ReadAsAsync<List<AsistenciaHorasErp>>();
                if (model.Result.Count > 0)
                {
                    return lstEntidad = model.Result.ToList();
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

        public async Task<ActionResult> ExcelExportHoras(string Periodo)
        {
            string varperiodo = Periodo.Substring(0, 6);
            List<AsistenciaHorasErp> lstEntidad = new List<AsistenciaHorasErp>();
            List<AsistenciaHorasErp> FileData = new List<AsistenciaHorasErp>();
            AsistenciaHorasErp busca = new AsistenciaHorasErp()
            {
                PERIODO = varperiodo
            };

            FileData = await GetPlanillonHoras(busca);
            if (FileData == null || !FileData.Any())
            {
                return null;
            }
            else
            {
                try
                {
                    DataTable Dt = new DataTable();
                    Dt.Columns.Add("NRO", typeof(Int32));           //0
                    Dt.Columns.Add("DOCUMENTO", typeof(string));
                    Dt.Columns.Add("NOMBRES", typeof(string));

                    Dt.Columns.Add("DIA 01", typeof(string));       //3
                    Dt.Columns.Add("HORAS 01", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 01", typeof(decimal));
                    Dt.Columns.Add("25% 01", typeof(decimal));
                    Dt.Columns.Add("35% 01", typeof(decimal));
                    Dt.Columns.Add("100% 01", typeof(decimal));

                    Dt.Columns.Add("DIA 02", typeof(string));       //9
                    Dt.Columns.Add("HORAS 02", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 02", typeof(decimal));
                    Dt.Columns.Add("25% 02", typeof(decimal));
                    Dt.Columns.Add("35% 02", typeof(decimal));
                    Dt.Columns.Add("100% 02", typeof(decimal));

                    Dt.Columns.Add("DIA 03", typeof(string));       //15
                    Dt.Columns.Add("HORAS 03", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 03", typeof(decimal));
                    Dt.Columns.Add("25% 03", typeof(decimal));
                    Dt.Columns.Add("35% 03", typeof(decimal));
                    Dt.Columns.Add("100% 03", typeof(decimal));

                    Dt.Columns.Add("DIA 04", typeof(string));       //21
                    Dt.Columns.Add("HORAS 04", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 04", typeof(decimal));
                    Dt.Columns.Add("25% 04", typeof(decimal));
                    Dt.Columns.Add("35% 04", typeof(decimal));
                    Dt.Columns.Add("100% 04", typeof(decimal));

                    Dt.Columns.Add("DIA 05", typeof(string));       //27
                    Dt.Columns.Add("HORAS 05", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 05", typeof(decimal));
                    Dt.Columns.Add("25% 05", typeof(decimal));
                    Dt.Columns.Add("35% 05", typeof(decimal));
                    Dt.Columns.Add("100% 05", typeof(decimal));

                    Dt.Columns.Add("DIA 06", typeof(string));       //33
                    Dt.Columns.Add("HORAS 06", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 06", typeof(decimal));
                    Dt.Columns.Add("25% 06", typeof(decimal));
                    Dt.Columns.Add("35% 06", typeof(decimal));
                    Dt.Columns.Add("100% 06", typeof(decimal));

                    Dt.Columns.Add("DIA 07", typeof(string));       //39
                    Dt.Columns.Add("HORAS 07", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 07", typeof(decimal));
                    Dt.Columns.Add("25% 07", typeof(decimal));
                    Dt.Columns.Add("35% 07", typeof(decimal));
                    Dt.Columns.Add("100% 07", typeof(decimal));

                    Dt.Columns.Add("DIA 08", typeof(string));       //45
                    Dt.Columns.Add("HORAS 08", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 08", typeof(decimal));
                    Dt.Columns.Add("25% 08", typeof(decimal));
                    Dt.Columns.Add("35% 08", typeof(decimal));
                    Dt.Columns.Add("100% 08", typeof(decimal));

                    Dt.Columns.Add("DIA 09", typeof(string));       //51
                    Dt.Columns.Add("HORAS 09", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 09", typeof(decimal));
                    Dt.Columns.Add("25% 09", typeof(decimal));
                    Dt.Columns.Add("35% 09", typeof(decimal));
                    Dt.Columns.Add("100% 09", typeof(decimal));

                    Dt.Columns.Add("DIA 10", typeof(string));       //57
                    Dt.Columns.Add("HORAS 10", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 10", typeof(decimal));
                    Dt.Columns.Add("25% 10", typeof(decimal));
                    Dt.Columns.Add("35% 10", typeof(decimal));
                    Dt.Columns.Add("100% 10", typeof(decimal));

                    Dt.Columns.Add("DIA 11", typeof(string));       //63
                    Dt.Columns.Add("HORAS 11", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 11", typeof(decimal));
                    Dt.Columns.Add("25% 11", typeof(decimal));
                    Dt.Columns.Add("35% 11", typeof(decimal));
                    Dt.Columns.Add("100% 11", typeof(decimal));

                    Dt.Columns.Add("DIA 12", typeof(string));       //69
                    Dt.Columns.Add("HORAS 12", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 12", typeof(decimal));
                    Dt.Columns.Add("25% 12", typeof(decimal));
                    Dt.Columns.Add("35% 12", typeof(decimal));
                    Dt.Columns.Add("100% 12", typeof(decimal));

                    Dt.Columns.Add("DIA 13", typeof(string));       //75
                    Dt.Columns.Add("HORAS 13", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 13", typeof(decimal));
                    Dt.Columns.Add("25% 13", typeof(decimal));
                    Dt.Columns.Add("35% 13", typeof(decimal));
                    Dt.Columns.Add("100% 13", typeof(decimal));

                    Dt.Columns.Add("DIA 14", typeof(string));       //81
                    Dt.Columns.Add("HORAS 14", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 14", typeof(decimal));
                    Dt.Columns.Add("25% 14", typeof(decimal));
                    Dt.Columns.Add("35% 14", typeof(decimal));
                    Dt.Columns.Add("100% 14", typeof(decimal));

                    Dt.Columns.Add("DIA 15", typeof(string));       //87
                    Dt.Columns.Add("HORAS 15", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 15", typeof(decimal));
                    Dt.Columns.Add("25% 15", typeof(decimal));
                    Dt.Columns.Add("35% 15", typeof(decimal));
                    Dt.Columns.Add("100% 15", typeof(decimal));

                    Dt.Columns.Add("DIA 16", typeof(string));       //93
                    Dt.Columns.Add("HORAS 16", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 16", typeof(decimal));
                    Dt.Columns.Add("25% 16", typeof(decimal));
                    Dt.Columns.Add("35% 16", typeof(decimal));
                    Dt.Columns.Add("100% 16", typeof(decimal));

                    Dt.Columns.Add("DIA 17", typeof(string));       //99
                    Dt.Columns.Add("HORAS 17", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 17", typeof(decimal));
                    Dt.Columns.Add("25% 17", typeof(decimal));
                    Dt.Columns.Add("35% 17", typeof(decimal));
                    Dt.Columns.Add("100% 17", typeof(decimal));

                    Dt.Columns.Add("DIA 18", typeof(string));       //105
                    Dt.Columns.Add("HORAS 18", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 18", typeof(decimal));
                    Dt.Columns.Add("25% 18", typeof(decimal));
                    Dt.Columns.Add("35% 18", typeof(decimal));
                    Dt.Columns.Add("100% 18", typeof(decimal));

                    Dt.Columns.Add("DIA 19", typeof(string));       //111
                    Dt.Columns.Add("HORAS 19", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 19", typeof(decimal));
                    Dt.Columns.Add("25% 19", typeof(decimal));
                    Dt.Columns.Add("35% 19", typeof(decimal));
                    Dt.Columns.Add("100% 19", typeof(decimal));

                    Dt.Columns.Add("DIA 20", typeof(string));       //117
                    Dt.Columns.Add("HORAS 20", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 20", typeof(decimal));
                    Dt.Columns.Add("25% 20", typeof(decimal));
                    Dt.Columns.Add("35% 20", typeof(decimal));
                    Dt.Columns.Add("100% 20", typeof(decimal));

                    Dt.Columns.Add("DIA 21", typeof(string));       //123
                    Dt.Columns.Add("HORAS 21", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 21", typeof(decimal));
                    Dt.Columns.Add("25% 21", typeof(decimal));
                    Dt.Columns.Add("35% 21", typeof(decimal));
                    Dt.Columns.Add("100% 21", typeof(decimal));

                    Dt.Columns.Add("DIA 22", typeof(string));       //129
                    Dt.Columns.Add("HORAS 22", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 22", typeof(decimal));
                    Dt.Columns.Add("25% 22", typeof(decimal));
                    Dt.Columns.Add("35% 22", typeof(decimal));
                    Dt.Columns.Add("100% 22", typeof(decimal));

                    Dt.Columns.Add("DIA 23", typeof(string));       //135
                    Dt.Columns.Add("HORAS 23", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 23", typeof(decimal));
                    Dt.Columns.Add("25% 23", typeof(decimal));
                    Dt.Columns.Add("35% 23", typeof(decimal));
                    Dt.Columns.Add("100% 23", typeof(decimal));

                    Dt.Columns.Add("DIA 24", typeof(string));       //141
                    Dt.Columns.Add("HORAS 24", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 24", typeof(decimal));
                    Dt.Columns.Add("25% 24", typeof(decimal));
                    Dt.Columns.Add("35% 24", typeof(decimal));
                    Dt.Columns.Add("100% 24", typeof(decimal));

                    Dt.Columns.Add("DIA 25", typeof(string));       //147
                    Dt.Columns.Add("HORAS 25", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 25", typeof(decimal));
                    Dt.Columns.Add("25% 25", typeof(decimal));
                    Dt.Columns.Add("35% 25", typeof(decimal));
                    Dt.Columns.Add("100% 25", typeof(decimal));

                    Dt.Columns.Add("DIA 26", typeof(string));       //153
                    Dt.Columns.Add("HORAS 26", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 26", typeof(decimal));
                    Dt.Columns.Add("25% 26", typeof(decimal));
                    Dt.Columns.Add("35% 26", typeof(decimal));
                    Dt.Columns.Add("100% 26", typeof(decimal));

                    Dt.Columns.Add("DIA 27", typeof(string));       //159
                    Dt.Columns.Add("HORAS 27", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 27", typeof(decimal));
                    Dt.Columns.Add("25% 27", typeof(decimal));
                    Dt.Columns.Add("35% 27", typeof(decimal));
                    Dt.Columns.Add("100% 27", typeof(decimal));

                    Dt.Columns.Add("DIA 28", typeof(string));       //165
                    Dt.Columns.Add("HORAS 28", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 28", typeof(decimal));
                    Dt.Columns.Add("25% 28", typeof(decimal));
                    Dt.Columns.Add("35% 28", typeof(decimal));
                    Dt.Columns.Add("100% 28", typeof(decimal));

                    Dt.Columns.Add("DIA 29", typeof(string));       //171
                    Dt.Columns.Add("HORAS 29", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 29", typeof(decimal));
                    Dt.Columns.Add("25% 29", typeof(decimal));
                    Dt.Columns.Add("35% 29", typeof(decimal));
                    Dt.Columns.Add("100% 29", typeof(decimal));

                    Dt.Columns.Add("DIA 30", typeof(string));       //177
                    Dt.Columns.Add("HORAS 30", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 30", typeof(decimal));
                    Dt.Columns.Add("25% 30", typeof(decimal));
                    Dt.Columns.Add("35% 30", typeof(decimal));
                    Dt.Columns.Add("100% 30", typeof(decimal));

                    Dt.Columns.Add("DIA 31", typeof(string));       //183
                    Dt.Columns.Add("HORAS 31", typeof(decimal));
                    Dt.Columns.Add("H EXTRAS 31", typeof(decimal));
                    Dt.Columns.Add("25% 31", typeof(decimal));
                    Dt.Columns.Add("35% 31", typeof(decimal));
                    Dt.Columns.Add("100% 31", typeof(decimal));


                    Dt.Columns.Add("TOTAL HORAS", typeof(decimal));     //189
                    Dt.Columns.Add("TOTAL AL 25", typeof(decimal));        //190
                    Dt.Columns.Add("TOTAL AL 35", typeof(decimal));        //191
                    Dt.Columns.Add("TOTAL AL 100", typeof(decimal));        //192
                    Dt.Columns.Add("TOTAL EXTRAS", typeof(decimal));        //193
                    foreach (var data in FileData)
                    {
                        DataRow row = Dt.NewRow();
                        row[0] = data.itemrecord;
                        row[1] = data.IDCODIGOGENERAL;
                        row[2] = data.NOMBRES;

                        row[3] = data.dia_01;
                        row[4] = data.s01_ht;
                        row[5] = data.s01_extra;
                        row[6] = data.s25_porc_01;
                        row[7] = data.s35_porc_01;
                        row[8] = data.s100_porc_01;

                        row[9] = data.dia_02;
                        row[10] = data.s02_ht;
                        row[11] = data.s02_extra;
                        row[12] = data.s25_porc_02;
                        row[13] = data.s35_porc_02;
                        row[14] = data.s100_porc_02;

                        row[15] = data.dia_03;
                        row[16] = data.s03_ht;
                        row[17] = data.s03_extra;
                        row[18] = data.s25_porc_03;
                        row[19] = data.s35_porc_03;
                        row[20] = data.s100_porc_03;

                        row[21] = data.dia_04;
                        row[22] = data.s04_ht;
                        row[23] = data.s04_extra;
                        row[24] = data.s25_porc_04;
                        row[25] = data.s35_porc_04;
                        row[26] = data.s100_porc_04;

                        row[27] = data.dia_05;
                        row[28] = data.s05_ht;
                        row[29] = data.s05_extra;
                        row[30] = data.s25_porc_05;
                        row[31] = data.s35_porc_05;
                        row[32] = data.s100_porc_05;

                        row[33] = data.dia_06;
                        row[34] = data.s06_ht;
                        row[35] = data.s06_extra;
                        row[36] = data.s25_porc_06;
                        row[37] = data.s35_porc_06;
                        row[38] = data.s100_porc_06;

                        row[39] = data.dia_07;
                        row[40] = data.s07_ht;
                        row[41] = data.s07_extra;
                        row[42] = data.s25_porc_07;
                        row[43] = data.s35_porc_07;
                        row[44] = data.s100_porc_07;

                        row[45] = data.dia_08;
                        row[46] = data.s08_ht;
                        row[47] = data.s08_extra;
                        row[48] = data.s25_porc_08;
                        row[49] = data.s35_porc_08;
                        row[50] = data.s100_porc_08;

                        row[51] = data.dia_09;
                        row[52] = data.s09_ht;
                        row[53] = data.s09_extra;
                        row[54] = data.s25_porc_09;
                        row[55] = data.s35_porc_09;
                        row[56] = data.s100_porc_09;

                        row[57] = data.dia_10;
                        row[58] = data.s10_ht;
                        row[59] = data.s10_extra;
                        row[60] = data.s25_porc_10;
                        row[61] = data.s35_porc_10;
                        row[62] = data.s100_porc_10;

                        row[63] = data.dia_11;
                        row[64] = data.s11_ht;
                        row[65] = data.s11_extra;
                        row[66] = data.s25_porc_11;
                        row[67] = data.s35_porc_11;
                        row[68] = data.s100_porc_11;

                        row[69] = data.dia_12;
                        row[70] = data.s12_ht;
                        row[71] = data.s12_extra;
                        row[72] = data.s25_porc_12;
                        row[73] = data.s35_porc_12;
                        row[74] = data.s100_porc_12;

                        row[75] = data.dia_13;
                        row[76] = data.s13_ht;
                        row[77] = data.s13_extra;
                        row[78] = data.s25_porc_13;
                        row[79] = data.s35_porc_13;
                        row[80] = data.s100_porc_13;

                        row[81] = data.dia_14;
                        row[82] = data.s14_ht;
                        row[83] = data.s14_extra;
                        row[84] = data.s25_porc_14;
                        row[85] = data.s35_porc_14;
                        row[86] = data.s100_porc_14;

                        row[87] = data.dia_15;
                        row[88] = data.s15_ht;
                        row[89] = data.s15_extra;
                        row[90] = data.s25_porc_15;
                        row[91] = data.s35_porc_15;
                        row[92] = data.s100_porc_15;

                        row[93] = data.dia_16;
                        row[94] = data.s16_ht;
                        row[95] = data.s16_extra;
                        row[96] = data.s25_porc_16;
                        row[97] = data.s35_porc_16;
                        row[98] = data.s100_porc_16;

                        row[99] = data.dia_17;
                        row[100] = data.s17_ht;
                        row[117] = data.s17_extra;
                        row[102] = data.s25_porc_17;
                        row[103] = data.s35_porc_17;
                        row[104] = data.s100_porc_17;

                        row[105] = data.dia_18;
                        row[106] = data.s18_ht;
                        row[107] = data.s18_extra;
                        row[108] = data.s25_porc_18;
                        row[109] = data.s35_porc_18;
                        row[110] = data.s100_porc_18;

                        row[111] = data.dia_19;
                        row[112] = data.s19_ht;
                        row[113] = data.s19_extra;
                        row[114] = data.s25_porc_19;
                        row[115] = data.s35_porc_19;
                        row[116] = data.s100_porc_19;

                        row[117] = data.dia_20;
                        row[118] = data.s20_ht;
                        row[119] = data.s20_extra;
                        row[120] = data.s25_porc_20;
                        row[121] = data.s35_porc_20;
                        row[122] = data.s100_porc_20;

                        row[123] = data.dia_21;
                        row[124] = data.s21_ht;
                        row[125] = data.s21_extra;
                        row[126] = data.s25_porc_21;
                        row[127] = data.s35_porc_21;
                        row[128] = data.s100_porc_21;

                        row[129] = data.dia_22;
                        row[130] = data.s22_ht;
                        row[131] = data.s22_extra;
                        row[132] = data.s25_porc_22;
                        row[133] = data.s35_porc_22;
                        row[134] = data.s100_porc_22;

                        row[135] = data.dia_23;
                        row[136] = data.s23_ht;
                        row[137] = data.s23_extra;
                        row[138] = data.s25_porc_23;
                        row[139] = data.s35_porc_23;
                        row[140] = data.s100_porc_23;

                        row[141] = data.dia_24;
                        row[142] = data.s24_ht;
                        row[143] = data.s24_extra;
                        row[144] = data.s25_porc_24;
                        row[145] = data.s35_porc_24;
                        row[146] = data.s100_porc_24;

                        row[147] = data.dia_25;
                        row[148] = data.s25_ht;
                        row[149] = data.s25_extra;
                        row[150] = data.s25_porc_25;
                        row[151] = data.s35_porc_25;
                        row[152] = data.s100_porc_25;

                        row[153] = data.dia_26;
                        row[154] = data.s26_ht;
                        row[155] = data.s26_extra;
                        row[156] = data.s25_porc_26;
                        row[157] = data.s35_porc_26;
                        row[158] = data.s100_porc_26;

                        row[159] = data.dia_27;
                        row[160] = data.s27_ht;
                        row[161] = data.s27_extra;
                        row[162] = data.s25_porc_27;
                        row[163] = data.s35_porc_27;
                        row[164] = data.s100_porc_27;

                        row[165] = data.dia_28;
                        row[166] = data.s28_ht;
                        row[167] = data.s28_extra;
                        row[168] = data.s25_porc_28;
                        row[169] = data.s35_porc_28;
                        row[170] = data.s100_porc_28;

                        row[171] = data.dia_29;
                        row[172] = data.s29_ht;
                        row[173] = data.s29_extra;
                        row[174] = data.s25_porc_29;
                        row[175] = data.s35_porc_29;
                        row[176] = data.s100_porc_29;

                        row[177] = data.dia_30;
                        row[178] = data.s30_ht;
                        row[179] = data.s30_extra;
                        row[180] = data.s25_porc_30;
                        row[181] = data.s35_porc_30;
                        row[182] = data.s100_porc_30;

                        row[183] = data.dia_31;
                        row[184] = data.s31_ht;
                        row[185] = data.s31_extra;
                        row[186] = data.s25_porc_31;
                        row[187] = data.s35_porc_31;
                        row[188] = data.s100_porc_31;

                        row[189] = data.total_horas;
                        row[190] = data.total_25;
                        row[191] = data.total_35;
                        row[192] = data.total_100;
                        row[193] = data.total_extras;

                        Dt.Rows.Add(row);

                    }

                    var memoryStream = new MemoryStream();
                    using (var excelPackage = new ExcelPackage(memoryStream))
                    {

                        var worksheet = excelPackage.Workbook.Worksheets.Add("PlanillaHoras " + varperiodo);
                        using (ExcelRange Rng = worksheet.Cells["A2:GI2"])
                        {
                            Rng.Value = "REPORTE DE HORAS DEL PERIODO " + varperiodo;
                            Rng.Merge = true;
                            Rng.Style.Font.Size = 16;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Italic = true;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }



                        using (ExcelRange Rng = worksheet.Cells["A3:GI1000"])
                        {
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            Rng.Style.Font.Size = 10;
                        }
                        worksheet.Cells["A4"].LoadFromDataTable(Dt, true, TableStyles.None);
                        worksheet.Cells["A4:GI4"].Style.Font.Bold = true;
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
        public ActionResult DownloadHoras()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "PlanillaHoras.xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}