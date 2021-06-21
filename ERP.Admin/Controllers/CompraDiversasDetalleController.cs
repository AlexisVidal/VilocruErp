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
    public class CompraDiversasDetalleController : Controller
    {
        public async Task<CompraDiversasDetalle> InsertCompraDiversasDetalle(CompraDiversasDetalle comprobante_compra)
        {
            CompraDiversasDetalle entidad = null;
            try
            {
                List<CompraDiversasDetalle> lstEntidad = null;

                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CompraDiversasDetalle/agregar", comprobante_compra);

                var model = response.Content.ReadAsAsync<List<CompraDiversasDetalle>>();
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

        public async Task<CompraDiversasDetalle> DeleteCompraDiversasDetalle(int IdCompDiveDeta)
        {
            CompraDiversasDetalle entidad = null;
            try
            {
                List<CompraDiversasDetalle> lstEntidad = null;

                var client = new HttpClient();
                CompraDiversasDetalle parametro = new CompraDiversasDetalle { id_compra_diversas_detalle = IdCompDiveDeta };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CompraDiversasDetalle/eliminar", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDiversasDetalle>>();
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

        public async Task<List<CompraDiversasDetalle>> GetCompraDiversasDetalle_ByComprobanteCompra(int FkCompComp)
        {
            List<CompraDiversasDetalle> lstEntidad = null;
            try
            {
                var client = new HttpClient();
                CompraDiversasDetalle parametro = new CompraDiversasDetalle { fk_comprobante_compra = FkCompComp };
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CompraDiversasDetalle/buscarByComprbanteCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDiversasDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CompraDiversasDetalle>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }

        // GET: CompraDiversasDetalle
        public ActionResult Index()
        {
            return View();
        }
    }
}