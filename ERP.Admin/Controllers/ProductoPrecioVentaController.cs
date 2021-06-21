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
    public class ProductoPrecioVentaController : Controller
    {
        // GET: ProductoPrecioVenta
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Guarda(string id_save, string fk_producto, string fk_precio_compra, string precio_unidadp,
                            string precio_mayorp, string precio_especialp, string precio_unidad,
                            string precio_mayor, string precio_especial, string tipo_cambio)
        {
            ProductoPrecioVenta _entidad = new ProductoPrecioVenta();

            if (id_save == "0")
            {
                _entidad.id_producto_precio_venta = 0;
            }
            else
            {
                _entidad.id_producto_precio_venta = Convert.ToInt32(id_save);
            }
            _entidad.fk_producto = Convert.ToInt32(fk_producto);
            _entidad.fk_precio_compra = Convert.ToInt32(fk_precio_compra);
            if (precio_unidadp == "")
            {
                precio_unidadp = "0";
            }
            _entidad.precio_unidadp = Convert.ToDecimal(precio_unidadp);
            if (precio_unidad == "")
            {
                precio_unidad = "0";
            }
            _entidad.precio_unidad = Convert.ToDecimal(precio_unidad);

            if (precio_mayorp == "")
            {
                precio_mayorp = "0";
            }
            _entidad.precio_mayorp = Convert.ToDecimal(precio_mayorp);
            if (precio_mayor == "")
            {
                precio_mayor = "0";
            }
            _entidad.precio_mayor = Convert.ToDecimal(precio_mayor);

            if (precio_especialp == "")
            {
                precio_especialp = "0";
            }
            _entidad.precio_especialp = Convert.ToDecimal(precio_especialp);
            if (precio_especial == "")
            {
                precio_especial = "0";
            }
            _entidad.precio_especial = Convert.ToDecimal(precio_especial);
            if (tipo_cambio == "")
            {
                tipo_cambio = "0";
            }
            _entidad.tipo_cambio = Convert.ToDecimal(tipo_cambio);

            _entidad.estado = "1";
            string idinserted = "0";

            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                if (_entidad.id_producto_precio_venta == 0)
                {
                    metodo = "ProductoPrecioVenta/agregar";
                }
                else
                {
                    metodo = "ProductoPrecioVenta/modificar";
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
        private async Task<ProductoPrecioVenta> GetPrecioVentaAsync(int id_savei)
        {
            List<ProductoPrecioVenta> lproducto = null;
            ProductoPrecioVenta _producto = null;
            ProductoPrecioVenta id_ = new ProductoPrecioVenta { id_producto_precio_venta = id_savei };
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("ProductoPrecioVenta/buscar", id_);

            var model = response.Content.ReadAsAsync<List<ProductoPrecioVenta>>();
            if (model.Result.Count > 0)
            {
                lproducto = model.Result.ToList();
                _producto = new ProductoPrecioVenta();
                _producto = lproducto[0];
            }
            else
            {
                _producto = new ProductoPrecioVenta();
                _producto = null;
            }

            return _producto;
        }
        [HttpPost]
        public async Task<JsonResult> GuardaMasivo(string familia, string marca, string subfamilia, string tipo_cambio, string precio_unidadpm,
                            string precio_mayorpm, string precio_especialpm)
        {
            int conttotal = 0;
            int contador = 0;
            List<Producto> newproduct = new List<Producto>();
            int familiaid = 0;
            int marcaid = 0;
            int subfamiliaid = 0;
            decimal tipocambionuevo = 1;
            decimal pprecio_unidadpm = 0;
            decimal pprecio_mayorpm = 0;
            decimal pprecio_especialpm = 0;
            try
            {
                if (familia != "0")
                {
                    familiaid = Convert.ToInt32(familia);
                }
                if (marca != "" || marca != "-1")
                {
                    marcaid = Convert.ToInt32(marca);
                }
                if (subfamilia != "0")
                {
                    subfamiliaid = Convert.ToInt32(subfamilia);
                }
                if (tipo_cambio != "0")
                {
                    tipocambionuevo = Convert.ToDecimal(tipo_cambio);
                }
                if (precio_unidadpm != "0")
                {
                    pprecio_unidadpm = Convert.ToDecimal(precio_unidadpm);
                }
                if (precio_mayorpm != "0")
                {
                    pprecio_mayorpm = Convert.ToDecimal(precio_mayorpm);
                }
                if (precio_especialpm != "0")
                {
                    pprecio_especialpm = Convert.ToDecimal(precio_especialpm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<Producto> lproductos = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Producto/allforventa");
            var model = response.Content.ReadAsAsync<List<Producto>>();
            if (model.Result.Count > 0)
            {
                lproductos = model.Result.ToList();
            }
            else
            {
                lproductos = new List<Producto>();
            }

            if (lproductos.Count > 0)
            {
                conttotal = lproductos.Count;
                newproduct = lproductos.ToList();
                
                if (marcaid > 0)
                {
                    var productactual2 = newproduct.ToList();
                    newproduct = new List<Producto>();
                    newproduct = productactual2.Where(x => x.fk_producto_marca == marcaid).ToList();
                }
                

                if (newproduct.Count > 0)
                {
                    foreach (var item in newproduct)
                    {
                        ProductoPrecioVenta _entidadexiste = new ProductoPrecioVenta();
                        decimal preciocompraconvert = 0;
                        ProductoPrecioVenta _entidad = new ProductoPrecioVenta();
                        if (item.fk_producto_precio_venta > 0)
                        {
                            _entidad.id_producto_precio_venta = item.fk_producto_precio_venta;
                            _entidadexiste = await GetPrecioVentaAsync(_entidad.id_producto_precio_venta);
                        }
                        else
                        {
                            _entidad.id_producto_precio_venta = 0;
                        }

                        _entidad.fk_producto = Convert.ToInt32(item.id_producto);
                        
                        if (pprecio_unidadpm > 0)
                        {
                            _entidad.precio_unidadp = pprecio_unidadpm;
                            //_entidad.precio_unidad = Math.Ceiling(((Convert.ToDecimal(preciocompraconvert) / 100) * Convert.ToDecimal(pprecio_unidadpm)) + Convert.ToDecimal(preciocompraconvert));
                            decimal round2 = Math.Round((((Convert.ToDecimal(preciocompraconvert) / 100) * Convert.ToDecimal(pprecio_unidadpm)) + Convert.ToDecimal(preciocompraconvert)),2);
                            _entidad.precio_unidad = Math.Round(round2, 1);
                        }
                        else
                        {
                            if (_entidadexiste != null)
                            {
                                _entidad.precio_unidadp = _entidadexiste.precio_unidadp;
                                //_entidad.precio_unidad = Math.Ceiling(_entidadexiste.precio_unidad);
                                decimal round2 = Math.Round(_entidadexiste.precio_unidad, 2);
                                _entidad.precio_unidad = Math.Round(round2, 1);
                            }
                            else
                            {
                                _entidad.precio_unidadp = 0;
                                //_entidad.precio_unidad = Math.Ceiling(Convert.ToDecimal(preciocompraconvert));
                                decimal round2 = Math.Round((Convert.ToDecimal(preciocompraconvert)), 2);
                                _entidad.precio_unidad = Math.Round((Convert.ToDecimal(round2)),1);
                            }
                        }
                        if (pprecio_mayorpm > 0)
                        {
                            _entidad.precio_mayorp = pprecio_mayorpm;
                            //_entidad.precio_mayor = Math.Ceiling((((Convert.ToDecimal(preciocompraconvert) / 100) * Convert.ToDecimal(pprecio_mayorpm)) + Convert.ToDecimal(preciocompraconvert)));
                            decimal round2 = Math.Round(((((Convert.ToDecimal(preciocompraconvert) / 100) * Convert.ToDecimal(pprecio_mayorpm)) + Convert.ToDecimal(preciocompraconvert))), 2);
                            _entidad.precio_mayor = Math.Round((Convert.ToDecimal(round2)), 1);
                        }
                        else
                        {
                            if (_entidadexiste != null)
                            {
                                _entidad.precio_mayorp = _entidadexiste.precio_mayorp;
                                decimal round2 = Math.Round(_entidadexiste.precio_mayor,2);
                                _entidad.precio_mayor = Math.Round(round2, 1);
                            }
                            else
                            {
                                _entidad.precio_mayorp = 0;
                                decimal round2 = Math.Round(Convert.ToDecimal(preciocompraconvert),2);
                                _entidad.precio_mayor = Math.Round(Convert.ToDecimal(round2),1);
                            }
                        }
                        if (pprecio_especialpm > 0)
                        {
                            _entidad.precio_especialp = pprecio_especialpm;
                            //_entidad.precio_especial = Math.Ceiling((((Convert.ToDecimal(preciocompraconvert) / 100) * Convert.ToDecimal(pprecio_especialpm)) + Convert.ToDecimal(preciocompraconvert)));
                            decimal round2 = Math.Round(((((Convert.ToDecimal(preciocompraconvert) / 100) * Convert.ToDecimal(pprecio_especialpm)) + Convert.ToDecimal(preciocompraconvert))), 2);
                            _entidad.precio_especial = Math.Round(round2, 1);
                        }
                        else
                        {
                            if (_entidadexiste != null)
                            {
                                _entidad.precio_especialp = _entidadexiste.precio_especialp;
                                decimal round2 = Math.Round(_entidadexiste.precio_especial,2);
                                _entidad.precio_especial = Math.Round(round2, 1);
                            }
                            else
                            {
                                _entidad.precio_especialp = 0;
                                decimal round2 = Math.Round(Convert.ToDecimal(preciocompraconvert),2);
                                _entidad.precio_especial = Math.Round(Convert.ToDecimal(round2),1);
                            }
                        }

                        _entidad.fk_precio_compra = Convert.ToInt32(item.id_precio_compra);

                        _entidad.tipo_cambio = Convert.ToDecimal(tipo_cambio);

                        _entidad.estado = "1";
                        string idinserted = "0";
                        try
                        {
                            var client2 = new HttpClient();
                            string connectionInfo2 = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                            client2.BaseAddress = new Uri(connectionInfo2);
                            client2.DefaultRequestHeaders.Accept.Clear();
                            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            string metodo = "";
                            if (_entidad.id_producto_precio_venta == 0)
                            {
                                metodo = "ProductoPrecioVenta/agregar";
                            }
                            else
                            {
                                metodo = "ProductoPrecioVenta/modificar";
                            }
                            var response2 = await client2.PostAsJsonAsync(metodo, _entidad);
                            var respuesta2 = response2.Content.ReadAsAsync<string>();
                            if (respuesta2 != null && respuesta2.Result.ToString() != "0")
                            {
                                idinserted = respuesta2.Result.ToString();
                                if (idinserted != "0")
                                {
                                    contador++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json("0", JsonRequestBehavior.AllowGet);
                        }

                    }
                }
            }







            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}