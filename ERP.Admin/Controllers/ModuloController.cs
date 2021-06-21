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
    public class ModuloController : Controller
    {
        public async Task<List<Modulo>> GetModuloAll()
        {
            List<Modulo> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Modulo/All");
                var model = response.Content.ReadAsAsync<List<Modulo>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<Models.Modulo>();
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
