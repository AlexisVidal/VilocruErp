using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    public class CompraErpController : Controller
    {
        // GET: CompraErp
        public async Task<ActionResult> Index()
        {
            List<CompraErp> lstEntidad = new List<CompraErp>();
            try
            {
                lstEntidad = await GetCompraAll();
            }
            catch (Exception e)
            {
                lstEntidad = new List<CompraErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return View(lstEntidad);
        }
        [HttpPost]
        public async Task<JsonResult> EliminaCompra(int id_compra)
        {
            List<CompraErp> _lentidad = new List<CompraErp>();
            CompraErp _entidad = null;
            CompraErp busca = new CompraErp()
            {
                id_compra = id_compra,
                estado = "0"
            };

            List<CompraDetalle> _ldentidad = new List<CompraDetalle>();
            CompraDetalle _dentidad = null;
            CompraDetalle dbusca = new CompraDetalle()
            {
                fk_compra = id_compra,
                estado = "0"
            };

            try
            {
                _ldentidad = await GetCompraDetalleFk(id_compra);

            }
            catch (Exception ex)
            {
                _ldentidad = new List<CompraDetalle>();
            }

            try
            {
                _entidad = await GetCompraById(id_compra);

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
                    metodo = "Compra/eliminar";
                    var response = await client.PostAsJsonAsync(metodo, busca);
                    var respuesta = response.Content.ReadAsAsync<string>();
                    if (respuesta != null && respuesta.Result.ToString() != "0")
                    {
                        idinserted = respuesta.Result.ToString();
                        if (_ldentidad.Any())
                        {
                            foreach (var items in _ldentidad)
                            {
                                int deletedetalle = await DeleteDetalleCompra(items.id_compra_detalle);
                            }
                        }
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

        private async Task<int> DeleteDetalleCompra(int id_compra_detalle)
        {
            int idinserted = 0;
            CompraDetalle busca = new CompraDetalle()
            {
                id_compra_detalle = id_compra_detalle,
                estado = "0"
            };
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "CompraDetalle/t_compra_detalleDelete";
                var response = await client.PostAsJsonAsync(metodo, busca);
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

        private async Task<List<CompraDetalle>> GetCompraDetalleFk(int id_compra)
        {
            CompraDetalle entidad = null;
            List<CompraDetalle> lstEntidad = new List<CompraDetalle>();
            try
            {
                

                CompraDetalle parametro = new CompraDetalle { fk_compra = id_compra };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CompraDetalle/getByFkCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<CompraDetalle>();
            }
            return lstEntidad;
        }

        public async Task<List<CompraErp>> GetCompraAll()
        {
            List<CompraErp> lstEntidad = null;
            HttpClient client = new HttpClient();
            try
            {
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Compra/all");
                var model = response.Content.ReadAsAsync<List<CompraErp>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
                else
                {
                    lstEntidad = new List<CompraErp>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstEntidad;
        }
        public async Task<List<Empresa>> GetProveedor()
        {
            List<Empresa> lempresa = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Empresa/all");
            var model = response.Content.ReadAsAsync<List<Empresa>>();
            if (model.Result.Count > 0)
            {
                lempresa = model.Result.Where(x => x.tipo.Equals("P")).ToList();
            }
            else
            {
                lempresa = new List<Empresa>();
            }

            return lempresa;
        }
        public async Task<ActionResult> Registro(string id_save)
        {
            List<CompraErp> _lentidad = new List<CompraErp>();
            List<CompraDetalle> _ldentidad = new List<CompraDetalle>();
            CompraErp _entidad = new CompraErp();
            List<Empresa> _lentidadproveedor = new List<Empresa>();
            Empresa _entidadproveedor = new Empresa()
            {
                id_empresa = 0,
                ruc = "",
                razon_social = "",
                direccion = "",
                contacto = "",
                telefono = "",
                tipo = "P",
                estado = "1"
            };

            _lentidadproveedor = await GetProveedor();
            _lentidadproveedor.Add(_entidadproveedor);
            ViewBag.Proveedor = _lentidadproveedor.OrderBy(z => z.id_empresa).ToList();

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

            _lentidadpersonal = await GetPersonalErp();
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

            _lentidadproyecto = await GetProyectoErp();
            _lentidadproyecto.Add(_entidadproyecto);
            ViewBag.Proyectos = _lentidadproyecto.OrderBy(z => z.descripcion).ToList();

            List<Almacen> _lentidadalmacen = new List<Almacen>();
            Almacen _entidadalmacen = new Almacen()
            {
                id_almacen = 0,
                nombre = "",
                ubicacion = "",
                estado = "1"
            };

            _lentidadalmacen = await GetAlmacenErp();
            _lentidadalmacen.Add(_entidadalmacen);
            ViewBag.Almacenes = _lentidadalmacen.OrderBy(z => z.nombre).ToList();

            List<MonedaErp> _lentidadmoneda = new List<MonedaErp>();
            MonedaErp _entidadmoneda = new MonedaErp()
            {
                IDMONEDA  = "",
                DESCRIPCION = "",
                ESTADO = "1"
            };

            _lentidadmoneda = await GetMonedaErp();
            _lentidadmoneda.Add(_entidadmoneda);
            ViewBag.Monedas = _lentidadmoneda.OrderBy(z => z.IDMONEDA).ToList();


            int id_savei = 0;
            if (id_save != "0")
            {
                
                try
                {
                    id_savei = Convert.ToInt32(id_save);
                    _entidad = await GetCompraById(id_savei);
                    if (_entidad != null)
                    {
                        ViewBag.Compra = _entidad;
                    }
                    else
                    {
                        ViewBag.Compra = null;
                    }
                }
                catch (Exception ex)
                {
                    id_savei = 0;
                    ViewBag.Compra = null;
                }
            }
            else
            {
                ViewBag.Compra = null;
            }

            if (id_savei > 0)
            {
                try
                {
                    _ldentidad = await GetCompraDetalleFk(id_savei);
                    if (_ldentidad != null)
                    {
                        ViewBag.CompraDetalle = _ldentidad;
                    }
                    else
                    {
                        ViewBag.CompraDetalle = null;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.CompraDetalle = null;
                }
            }
            else
            {
                ViewBag.CompraDetalle = null;
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView();
        }
        public async Task<List<MonedaErp>> GetMonedaErp()
        {
            List<MonedaErp> xlMonedaErp = new List<MonedaErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Empresa/t_monedaSelectAll");
                var model = response.Content.ReadAsAsync<List<MonedaErp>>();
                if (model.Result.Count > 0)
                {
                    xlMonedaErp = model.Result.ToList();
                }
                else
                {
                    xlMonedaErp = new List<MonedaErp>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return xlMonedaErp;
        }
        private async Task<List<Almacen>> GetAlmacenErp()
        {
            List<Almacen> lalmacen = null;
            HttpClient client = new HttpClient();
            string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
            client.BaseAddress = new Uri(connectionInfo);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Almacen/all");
            var model = response.Content.ReadAsAsync<List<Almacen>>();
            if (model.Result.Count > 0)
            {
                lalmacen = model.Result.Where(x=>x.estado.Equals("1")).ToList();
            }
            else
            {
                lalmacen = new List<Almacen>();
            }
            return lalmacen;
        }

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

        public async Task<List<PersonalErp>> GetPersonalErp()
        {
            List<PersonalErp> lempresa = null;
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Personalerp/t_personal_generalSelectAllForList");
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

        public async Task<CompraErp> GetCompraById(int IdComp)
        {
            CompraErp entidad = null;
            try
            {
                List<CompraErp> lstEntidad = null;

                CompraErp parametro = new CompraErp { id_compra = IdComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Compra/buscar", parametro);

                var model = response.Content.ReadAsAsync<List<CompraErp>>();
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
        public async Task<int> InsertCompra(CompraErp compra)
        {
            int NewIdComp = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("Compra/agregar", compra);

                var rptaReturn = response.Content.ReadAsAsync<List<CompraErp>>();
                if (rptaReturn.Result.Count > 0)
                {
                    NewIdComp = Convert.ToInt32(rptaReturn.Result[0].id_compra);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return NewIdComp;
        }

        public async Task<int> InsertDetalleCompra(CompraDetalle detcompra)
        {
            int NewIdComp = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("CompraDetalle/agregar", detcompra);

                var rptaReturn = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (rptaReturn.Result.Count > 0)
                {
                    NewIdComp = Convert.ToInt32(rptaReturn.Result[0].id_compra_detalle);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return NewIdComp;
        }
        public async Task<ActionResult> SaveNewCompra(int id_save, int fk_orden_compra, int fk_empresa, string n_compra,  
            string f_compra, string agencia_transporte, string observacion, string IDCODIGOGENERAL, string nro_guia,
            int fk_proyecto, List<List<string>> arrOCDeta, int fk_almacen, string nro_factura, string idmoneda, string credito)
        {
            string msgReturn = "";
            string NroReturn = "";
            int NewIdComp = 0;
            int NewIdDeta = 0;
            DateTime NewFechOC = DateTime.Now;
            int flgError = 0;
            msgReturn = "La Orden de Compra se registró satisfactoriamente";
            try
            {
                string FkUsua = Session["IdUsuario"].ToString().Trim();
                CompraDetalle compra_detalle = null;
                
                CompraErp compra = new CompraErp
                {
                    id_compra = id_save,
                    fk_orden_compra = fk_orden_compra,
                    fk_empresa = fk_empresa,
                    n_compra = n_compra,
                    f_compra = Convert.ToDateTime(f_compra),
                    agencia_transporte = agencia_transporte,
                    observacion = observacion,
                    IDCODIGOGENERAL = IDCODIGOGENERAL,
                    nro_guia= nro_guia,
                    fk_proyecto= fk_proyecto,
                    IDUSUARIO = FkUsua,
                    fk_almacen = fk_almacen,
                    nro_factura = nro_factura,
                    IDMONEDA = idmoneda,
                    credito = credito,
                    estado = "1"
                };
                decimal montototal = 0;
                if (id_save == 0)
                {
                    NewIdComp = await InsertCompra(compra);
                    if (NewIdComp > 0)
                    {
                        if (arrOCDeta != null)
                        {
                            montototal = 0;
                            foreach (var oneOCDeta in arrOCDeta)
                            {
                                compra_detalle = new CompraDetalle
                                {
                                    fk_compra = NewIdComp,
                                    fk_producto = Convert.ToInt32(oneOCDeta[0]),
                                    cantidad = Convert.ToDecimal(oneOCDeta[1]),
                                    precio = Convert.ToDecimal(oneOCDeta[2])
                                };
                                NewIdDeta = await InsertDetalleCompra(compra_detalle);
                                if (NewIdDeta <= 0)
                                {
                                    msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                                    flgError = 1;
                                    break;
                                }
                                else
                                {
                                    montototal = Convert.ToDecimal(montototal) + Convert.ToDecimal(Convert.ToDecimal(oneOCDeta[1]) * Convert.ToDecimal(oneOCDeta[2]));
                                }
                            }
                        }
                        NroReturn = compra.n_compra;
                        if (credito.Equals("1"))
                        {
                            ComercialController comerctrl = new ComercialController();
                            var ctainserted = await GuardaCtaPagarProveedor("0", fk_empresa, 3, nro_factura,
                                idmoneda, montototal, f_compra, "01/01/1901",
                                "0", 0, "", "0");
                            var xjson = ctainserted;
                        }
                    }
                    else
                    {
                        msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }
                }
                else
                {
                    NewIdComp = await UpdateCompra(compra);
                    if (NewIdComp > 0)
                    {
                        List<CompraDetalle> listdelete = new List<CompraDetalle>();
                        listdelete = await GetDetalles(NewIdComp);
                        if (listdelete.Any())
                        {
                            foreach (var idelete in listdelete)
                            {
                                int deleted = await DeleteDetalleCompra(idelete.id_compra_detalle);
                            }
                        }
                        if (arrOCDeta != null)
                        {
                            foreach (var oneOCDeta in arrOCDeta)
                            {
                                
                                compra_detalle = new CompraDetalle
                                {
                                    fk_compra = NewIdComp,
                                    fk_producto = Convert.ToInt32(oneOCDeta[0]),
                                    cantidad = Convert.ToDecimal(oneOCDeta[1]),
                                    precio = Convert.ToDecimal(oneOCDeta[2])
                                };
                                NewIdDeta = await InsertDetalleCompra(compra_detalle);
                                if (NewIdDeta <= 0)
                                {
                                    msgReturn = "Ocurrió un error al intentar Registrar los detalles, Por favor comuniquese con el administrador de sistemas";
                                    flgError = 1;
                                    break;
                                }
                            }
                        }
                        NroReturn = compra.n_compra;
                    }
                    else
                    {
                        msgReturn = "Ocurrió un error al intentar Registrar, Por favor comuniquese con el administrador de sistemas";
                        flgError = 1;
                    }

                    
                }
                
                
                if (flgError == 1)
                {
                    //Response.StatusCode = 500;
                    return Json(-1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msgReturn = "Ocurrió un error al intentar actualizar la OC, Por favor comuniquese con el administrador de sistemas";
                //Response.StatusCode = 500;
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            return Json(NewIdComp, JsonRequestBehavior.AllowGet);
        }
        public async Task<int> GuardaCtaPagarProveedor(string id_save, int fk_empresa, int fk_comprobantetipo,
            string documento, string IDMONEDA, decimal monto, string f_emision, string f_vencimiento,
            string afectaDetraccion, decimal montoDetraccion, string observacion, string detraccion_estados)
        {
            string FkUsua = Session["IdUsuario"].ToString().Trim();
            CuentaPagarProveedorErp _entidad = new CuentaPagarProveedorErp();
            if (id_save == "0")
            {
                _entidad.id_cuentas_por_pagar_proveedor = 0;
            }
            else
            {


                _entidad.id_cuentas_por_pagar_proveedor = Convert.ToInt32(id_save);
            }
            _entidad.fk_proveedor = Convert.ToInt32(fk_empresa);
            _entidad.fk_comprobante_tipo = Convert.ToInt32(fk_comprobantetipo);
            _entidad.documento = documento;
            _entidad.IDMONEDA = IDMONEDA;
            _entidad.monto = Convert.ToDecimal(monto);
            _entidad.f_emision = Convert.ToDateTime(f_emision);
            _entidad.f_vencimiento = Convert.ToDateTime(f_vencimiento);
            _entidad.afecta_detraccion = afectaDetraccion;
            _entidad.monto_detraccion = Convert.ToDecimal(montoDetraccion);
            _entidad.observacion = observacion;
            _entidad.IDUSUARIO = FkUsua;
            _entidad.detraccion_estado = detraccion_estados;
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
                if (_entidad.id_cuentas_por_pagar_proveedor == 0)
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_proveedorInsert";
                }
                else
                {
                    metodo = "Cuentaserp/t_cuentas_por_pagar_proveedorUpdate";
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
                return 0;
            }
            return Convert.ToInt32(idinserted);
        }
        private async Task<List<CompraDetalle>> GetDetalles(int newIdComp)
        {
            CompraDetalle entidad = null;
            List<CompraDetalle> lstEntidad = new List<CompraDetalle>();
            try
            {


                CompraDetalle parametro = new CompraDetalle { fk_compra = newIdComp };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CompraDetalle/getByFkCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
            }
            catch (Exception ex)
            {
                lstEntidad = new List<CompraDetalle>();
            }
            return lstEntidad;
        }

        private async Task<int> UpdateCompra(CompraErp _entidad)
        {
            int idinserted = 0;
            try
            {
                var client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string metodo = "";
                metodo = "Compra/modificar";
                var response = await client.PostAsJsonAsync(metodo, _entidad);
                var respuesta = response.Content.ReadAsAsync<string>();
                if (respuesta != null && respuesta.Result.ToString() != "0")
                {
                    idinserted = Convert.ToInt32(respuesta.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                idinserted = 0;
            }
            return idinserted;
        }

        [HttpPost]
        public async Task<JsonResult> GetCompraDetalleJson(int id_compra)
        {
            CompraDetalle entidad = null;
            List<CompraDetalle> lstEntidad = new List<CompraDetalle>();
            try
            {


                CompraDetalle parametro = new CompraDetalle { fk_compra = id_compra };
                HttpClient client = new HttpClient();
                string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CompraDetalle/getByFkCompra", parametro);

                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lstEntidad = model.Result.ToList();
                }
            }
            catch (Exception ex)
            {
                lstEntidad = new List<CompraDetalle>();
            }
            return Json(lstEntidad, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewComprasProyecto(int IdProyecto, string CallBy)
        {
            string msgReturn = "";
            decimal preciofinal = 0;
            List<ProyectoErp> lstComprasProyecto = new List<ProyectoErp>();
           
            try
            {
                lstComprasProyecto = await GetComprasByProyecto(IdProyecto);
                if (lstComprasProyecto.Any())
                {
                    preciofinal = lstComprasProyecto[0].precio_total;
                }
                ViewBag.CallBy = CallBy;
            }
            catch (Exception ex)
            {
                lstComprasProyecto = new List<ProyectoErp>();
            }
            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            ViewBag.PrecioFinal = preciofinal;
            return PartialView(lstComprasProyecto);
        }

        private async Task<List<ProyectoErp>> GetComprasByProyecto(int idProyecto)
        {
            ProyectoErp busca = new ProyectoErp()
            {
                id_proyecto = idProyecto
            };
            List<ProyectoErp> lreturn = new List<ProyectoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Servicioerp/t_proyectoSelectAllCompras", busca);
                var model = response.Content.ReadAsAsync<List<ProyectoErp>>();
                if (model.Result.Count > 0)
                {
                    lreturn = model.Result.ToList();
                }
                else
                {
                    lreturn = new List<ProyectoErp>();
                }
            }
            catch (Exception e)
            {
                lreturn = new List<ProyectoErp>();
            }

            return lreturn;
        }

        public async Task<ActionResult> ViewProyectoFull(int IdProyecto, string CallBy)
        {
            string msgReturn = "";
            decimal preciofinal = 0;
            string codigoproyecto = "";
            string ruccliente = "";
            string cliente = "";
            string descripcion = "";
            string observacion = "";
            string inicio = "";
            string fin = "";
            string montoservicio = "";
            string moneda = "";
            string servicios = "";
            List<string> lservicios = new List<string>();
            //string fin = "";
            //string fin = "";
            List<ProyectoErp> lstComprasProyecto = new List<ProyectoErp>();
            List<CompraDetalle> lstComprasDetalles = new List<CompraDetalle>();
            List<ServicioTomadoErp> lstServicioTomado = new List<ServicioTomadoErp>();

            try
            {
                lstComprasProyecto = await GetComprasByProyecto(IdProyecto);
                if (lstComprasProyecto.Any())
                {
                    preciofinal = lstComprasProyecto[0].precio_total;
                    codigoproyecto = lstComprasProyecto[0].codigo;
                    ruccliente = lstComprasProyecto[0].ruc_cliente;
                    cliente = lstComprasProyecto[0].razon_social;
                    descripcion = lstComprasProyecto[0].descripcion;
                    observacion = lstComprasProyecto[0].observacion;
                    inicio = lstComprasProyecto[0].inicio.ToString("dd/MM/yyyy");
                    fin = lstComprasProyecto[0].fin.ToString("dd/MM/yyyy");
                    montoservicio = lstComprasProyecto[0].monto.ToString("N");
                    moneda = lstComprasProyecto[0].MONEDA;
                    servicios = lstComprasProyecto[0].descripcion_servicio;
                    if (servicios.Length > 0)
                    {
                        lservicios = servicios.Split(',').ToList();
                    }
                }
                ViewBag.CallBy = CallBy;

                var comprasdetalles = await GetComprasDetallesByProyecto(IdProyecto);
                if (comprasdetalles.Any())
                {
                    lstComprasDetalles = comprasdetalles.ToList();
                }
                var serviciostomados = await GetServiciosTomadosAll();
                if (serviciostomados.Any())
                {
                    lstServicioTomado = serviciostomados.Where(x => x.fk_proyecto == IdProyecto).ToList();
                }

            }
            catch (Exception ex)
            {
                lstComprasProyecto = new List<ProyectoErp>();
            }

            ViewBag.PrecioFinal = preciofinal;
            ViewBag.Codigoproyecto = codigoproyecto;
            ViewBag.Ruccliente = ruccliente;
            ViewBag.Cliente = cliente;
            ViewBag.Descripcion = descripcion;
            ViewBag.Observacion = observacion;
            ViewBag.Inicio = inicio;
            ViewBag.Fin = fin;
            ViewBag.Montoservicio = montoservicio;
            ViewBag.Moneda = moneda;
            ViewBag.Servicios = servicios;
            ViewBag.Lservicios = lservicios;

            ViewBag.ServiciosTomados = lstServicioTomado;
            ViewBag.CompraDetalles = lstComprasDetalles;

            string logohorizontal = System.Configuration.ConfigurationManager.AppSettings["logohorizontal"];
            ViewBag.LogoHorizontals = logohorizontal;
            return PartialView(lstComprasProyecto);
        }

        private async Task<List<CompraDetalle>> GetComprasDetallesByProyecto(int idProyecto)
        {
            CompraDetalle busca = new CompraDetalle()
            {
                fk_proyecto = idProyecto
            };
            List<CompraDetalle> lreturn = new List<CompraDetalle>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("Servicioerp/t_compra_detalleSelectProyecto", busca);
                var model = response.Content.ReadAsAsync<List<CompraDetalle>>();
                if (model.Result.Count > 0)
                {
                    lreturn = model.Result.ToList();
                }
                else
                {
                    lreturn = new List<CompraDetalle>();
                }
            }
            catch (Exception e)
            {
                lreturn = new List<CompraDetalle>();
            }

            return lreturn;
        }

        private async Task<List<ServicioTomadoErp>> GetServiciosTomadosAll()
        {
            List<ServicioTomadoErp> lentidad = new List<ServicioTomadoErp>();
            try
            {
                HttpClient client = new HttpClient();
                string connectionInfo = ConfigurationManager.AppSettings["Uristring"];
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Servicioerp/t_servicio_tomadoSelectFullAll");
                var model = response.Content.ReadAsAsync<List<ServicioTomadoErp>>();
                if (model.Result.Count > 0)
                {
                    lentidad = model.Result.Where(x => x.estado.Equals("1")).ToList();
                }
                else
                {
                    lentidad = new List<ServicioTomadoErp>();
                }
            }
            catch (Exception ex)
            {
                
            }
            return lentidad;
        }
    }
}