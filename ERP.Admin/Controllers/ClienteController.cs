using ERP.Admin.App_Start;
using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ERP.Admin.Controllers
{
    public class ClienteController : Controller
    {
        public async Task<int> InsertCliente(Cliente cliente)
        {
            int idinserted = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Cliente/agregar", cliente);

                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return idinserted;
        }

        public async Task<int> InsertClienteJuridico(ClienteJuridico cliente_juridico)
        {
            int idinserted = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ClienteJuridico/agregar", cliente_juridico);

                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return idinserted;
        }

        public async Task<int> InsertClienteNatural(ClienteNatural cliente_natural)
        {
            int idinserted = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("ClienteNatural/agregar", cliente_natural);

                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return idinserted;
        }

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public async Task<List<Cliente>> GetClienteAll()
        {
            List<Cliente> lentidad = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Cliente/all");
            var model = response.Content.ReadAsAsync<List<Cliente>>();
            if (model.Result.Count > 0)
            {
                lentidad = model.Result.ToList();
            }
            else
            {
                lentidad = null;
            }

            return lentidad;
        }

        public async Task<ActionResult> ListaClientes(string CallBy)
        {
            List<Cliente> lstCliente = null;
            try
            {
                lstCliente = await GetClienteAll();
                lstCliente = lstCliente.Where(i => i.estado.Equals("1")).ToList();
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                lstCliente = new List<Cliente>();
                return PartialView(lstCliente);
            }
            return PartialView(lstCliente);
        }
    }
}