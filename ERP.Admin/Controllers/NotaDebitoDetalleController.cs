using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
namespace ERP.Admin.Controllers
{
    public class NotaDebitoDetalleController : Controller
    {
        public async Task<NotaDebitoDetalle> InsertNotaDebitoDetalle(NotaDebitoDetalle nota_debito_detalle)
        {
            NotaDebitoDetalle entidad = null;
            try
            {
                List<NotaDebitoDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("NotaDebitoDetalle/agregar", nota_debito_detalle);

                var model = response.Content.ReadAsAsync<List<NotaDebitoDetalle>>();
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

        public async Task<List<NotaDebitoDetalle>> GetNotaDebitoDetalleByNotaDebito(int FkNotaCred)
        {
            List<NotaDebitoDetalle> lstEntidad = null;
            try
            {
                NotaDebitoDetalle parametro = new NotaDebitoDetalle { fk_nota_debito = FkNotaCred };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("NotaDebitoDetalle/ByNotaDebito", parametro);

                var model = response.Content.ReadAsAsync<List<NotaDebitoDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<NotaDebitoDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: NotaDebitoDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}