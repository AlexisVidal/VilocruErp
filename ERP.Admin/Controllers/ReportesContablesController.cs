using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing.Printing;
using CrystalDecisions.Shared;
using System.Web.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace ERP.Admin.Controllers
{
    [SessionAuthorize]
    public class ReportesContablesController : Controller
    {
        // GET: ReportesContables
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LibroDiario()
        {
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstMes.Add(new Meses { id_mes = 1, nombre_mes = "ENERO" });
                lstMes.Add(new Meses { id_mes = 2, nombre_mes = "FEBRERO" });
                lstMes.Add(new Meses { id_mes = 3, nombre_mes = "MARZO" });
                lstMes.Add(new Meses { id_mes = 4, nombre_mes = "ABRIL" });
                lstMes.Add(new Meses { id_mes = 5, nombre_mes = "MAYO" });
                lstMes.Add(new Meses { id_mes = 6, nombre_mes = "JUNIO" });
                lstMes.Add(new Meses { id_mes = 7, nombre_mes = "JULIO" });
                lstMes.Add(new Meses { id_mes = 8, nombre_mes = "AGOSTO" });
                lstMes.Add(new Meses { id_mes = 9, nombre_mes = "SETIEMBRE" });
                lstMes.Add(new Meses { id_mes = 10, nombre_mes = "OCYUBRE" });
                lstMes.Add(new Meses { id_mes = 11, nombre_mes = "NOVIEMBRE" });
                lstMes.Add(new Meses { id_mes = 12, nombre_mes = "DICIEMBRE" });

                for (int i = 6; i >= 0; i--)
                {
                    lstAnio.Add(new Anio { id_anio = anioActual, nombre_anio = anioActual });
                    anioActual--;
                }
                lstAnio = lstAnio.OrderByDescending(p => p.id_anio).ToList();

                ViewBag.Meses = lstMes.ToList();
                ViewBag.Anios = lstAnio.ToList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Blank", "home");
            }
        }

        public async Task<ActionResult> LoadDataLibroDiario(string ContAnio, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            string ContCent = "1";
            try
            {
                lstEntidad = await GetReporteContableLibroDiario(ContAnio, ContCent, ContMes);
                if (lstEntidad == null)
                {
                    lstEntidad = new List<ReporteContable>();
                }
            }
            catch (Exception ex)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstEntidad);
        }

        public ActionResult HojaTrabajo()
        {
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstMes.Add(new Meses { id_mes = 1, nombre_mes = "ENERO" });
                lstMes.Add(new Meses { id_mes = 2, nombre_mes = "FEBRERO" });
                lstMes.Add(new Meses { id_mes = 3, nombre_mes = "MARZO" });
                lstMes.Add(new Meses { id_mes = 4, nombre_mes = "ABRIL" });
                lstMes.Add(new Meses { id_mes = 5, nombre_mes = "MAYO" });
                lstMes.Add(new Meses { id_mes = 6, nombre_mes = "JUNIO" });
                lstMes.Add(new Meses { id_mes = 7, nombre_mes = "JULIO" });
                lstMes.Add(new Meses { id_mes = 8, nombre_mes = "AGOSTO" });
                lstMes.Add(new Meses { id_mes = 9, nombre_mes = "SETIEMBRE" });
                lstMes.Add(new Meses { id_mes = 10, nombre_mes = "OCYUBRE" });
                lstMes.Add(new Meses { id_mes = 11, nombre_mes = "NOVIEMBRE" });
                lstMes.Add(new Meses { id_mes = 12, nombre_mes = "DICIEMBRE" });

                for (int i = 6; i >= 0; i--)
                {
                    lstAnio.Add(new Anio { id_anio = anioActual, nombre_anio = anioActual });
                    anioActual--;
                }
                lstAnio = lstAnio.OrderByDescending(p => p.id_anio).ToList();

                ViewBag.Meses = lstMes.ToList();
                ViewBag.Anios = lstAnio.ToList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Blank", "home");
            }
        }

        public async Task<ActionResult> LoadDataHojaTrabajo(string ContAnio, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            string ContCent = "1";
            try
            {
                lstEntidad = await GetReporteContableHojaTrabajo(ContAnio, ContCent, ContMes);
                if (lstEntidad == null)
                {
                    lstEntidad = new List<ReporteContable>();
                }
            }
            catch (Exception ex)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstEntidad);
        }
        public ActionResult BalanceGeneral()
        {
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstMes.Add(new Meses { id_mes = 1, nombre_mes = "ENERO" });
                lstMes.Add(new Meses { id_mes = 2, nombre_mes = "FEBRERO" });
                lstMes.Add(new Meses { id_mes = 3, nombre_mes = "MARZO" });
                lstMes.Add(new Meses { id_mes = 4, nombre_mes = "ABRIL" });
                lstMes.Add(new Meses { id_mes = 5, nombre_mes = "MAYO" });
                lstMes.Add(new Meses { id_mes = 6, nombre_mes = "JUNIO" });
                lstMes.Add(new Meses { id_mes = 7, nombre_mes = "JULIO" });
                lstMes.Add(new Meses { id_mes = 8, nombre_mes = "AGOSTO" });
                lstMes.Add(new Meses { id_mes = 9, nombre_mes = "SETIEMBRE" });
                lstMes.Add(new Meses { id_mes = 10, nombre_mes = "OCYUBRE" });
                lstMes.Add(new Meses { id_mes = 11, nombre_mes = "NOVIEMBRE" });
                lstMes.Add(new Meses { id_mes = 12, nombre_mes = "DICIEMBRE" });

                for (int i = 6; i >= 0; i--)
                {
                    lstAnio.Add(new Anio { id_anio = anioActual, nombre_anio = anioActual });
                    anioActual--;
                }
                lstAnio = lstAnio.OrderByDescending(p => p.id_anio).ToList();

                ViewBag.Meses = lstMes.ToList();
                ViewBag.Anios = lstAnio.ToList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Blank", "home");
            }
        }

        public async Task<ActionResult> LoadDataBalanceGeneral(string ContAnio, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            string ContCent = "1";
            try
            {
                lstEntidad = await GetReporteContableBalanceGeneral(ContAnio, ContCent, ContMes);
                if (lstEntidad == null)
                {
                    lstEntidad = new List<ReporteContable>();
                }
            }
            catch (Exception ex)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstEntidad);
        }

        public ActionResult LibroMayor()
        {
            List<Meses> lstMes = new List<Meses>();
            List<Anio> lstAnio = new List<Anio>();
            int anioActual = Convert.ToInt32(DateTime.Now.Year);
            int newAnio = anioActual;
            try
            {
                lstMes.Add(new Meses { id_mes = 1, nombre_mes = "ENERO" });
                lstMes.Add(new Meses { id_mes = 2, nombre_mes = "FEBRERO" });
                lstMes.Add(new Meses { id_mes = 3, nombre_mes = "MARZO" });
                lstMes.Add(new Meses { id_mes = 4, nombre_mes = "ABRIL" });
                lstMes.Add(new Meses { id_mes = 5, nombre_mes = "MAYO" });
                lstMes.Add(new Meses { id_mes = 6, nombre_mes = "JUNIO" });
                lstMes.Add(new Meses { id_mes = 7, nombre_mes = "JULIO" });
                lstMes.Add(new Meses { id_mes = 8, nombre_mes = "AGOSTO" });
                lstMes.Add(new Meses { id_mes = 9, nombre_mes = "SETIEMBRE" });
                lstMes.Add(new Meses { id_mes = 10, nombre_mes = "OCYUBRE" });
                lstMes.Add(new Meses { id_mes = 11, nombre_mes = "NOVIEMBRE" });
                lstMes.Add(new Meses { id_mes = 12, nombre_mes = "DICIEMBRE" });

                for (int i = 6; i >= 0; i--)
                {
                    lstAnio.Add(new Anio { id_anio = anioActual, nombre_anio = anioActual });
                    anioActual--;
                }
                lstAnio = lstAnio.OrderByDescending(p => p.id_anio).ToList();

                ViewBag.Meses = lstMes.ToList();
                ViewBag.Anios = lstAnio.ToList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Blank", "home");
            }
        }

        public async Task<ActionResult> LoadDataLibroMayor(string ContAnio, string ContMes, string NombMes)
        {
            List<ReporteContable> lstEntidad = null;
            List<ReporteContable> lstGroupEntidad = new List<ReporteContable>();
            ReporteContable reportContable = null;
            string ContCent = "1";
            try
            {
                lstEntidad = await GetReporteContableLibroMayor(ContAnio, ContCent, ContMes);
                if (lstEntidad != null)
                {
                    var newGroupCuenta = lstEntidad.GroupBy(p => p.cuenta).Select(i =>
                     new
                     {
                         NumeCuen = i.First().cuenta
                     }).ToList();
                    foreach(var oneItem in newGroupCuenta)
                    {
                        reportContable = new ReporteContable
                        {
                            cuenta = oneItem.NumeCuen
                        };
                        lstGroupEntidad.Add(reportContable);
                    }
                }
                else
                {
                    lstEntidad = new List<ReporteContable>();
                }
                ViewBag.anio = ContAnio;
                ViewBag.NombreMes = NombMes;
                ViewBag.lstGroupCuenta = lstGroupEntidad;
            }
            catch (Exception ex)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstEntidad);
        }

        public async Task<List<ReporteContable>> GetReporteContableLibroMayor(string ContAnio, string ContCent, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                ReporteContable parametro = new ReporteContable { cont_anio = ContAnio, cont_centro = ContCent, cont_mes = ContMes };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ReportesContables/LibroMayor", parametro);

                var model = response.Content.ReadAsAsync<List<ReporteContable>>();
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

        public async Task<List<ReporteContable>> GetReporteContableBalanceGeneral(string ContAnio, string ContCent, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                ReporteContable parametro = new ReporteContable { cont_anio = ContAnio, cont_centro = ContCent, cont_mes = ContMes };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ReportesContables/BalanceGeneral", parametro);

                var model = response.Content.ReadAsAsync<List<ReporteContable>>();
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

        public async Task<List<ReporteContable>> GetReporteContableHojaTrabajo(string ContAnio, string ContCent, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                ReporteContable parametro = new ReporteContable { cont_anio = ContAnio, cont_centro = ContCent, cont_mes = ContMes };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ReportesContables/HojaTrabajo", parametro);

                var model = response.Content.ReadAsAsync<List<ReporteContable>>();
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

        public async Task<List<ReporteContable>> GetReporteContableLibroDiario(string ContAnio, string ContCent, string ContMes)
        {
            List<ReporteContable> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                ReporteContable parametro = new ReporteContable { cont_anio = ContAnio, cont_centro = ContCent, cont_mes = ContMes };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ReportesContables/LibroDiario", parametro);

                var model = response.Content.ReadAsAsync<List<ReporteContable>>();
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
    }
}
