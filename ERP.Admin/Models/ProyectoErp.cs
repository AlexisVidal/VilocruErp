using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ERP.Admin.Models
{
    public class ProyectoErp
    {
        public int id_proyecto { get; set; }
        public string codigo { get; set; }
        public int fk_empresa { get; set; }
        public int fk_servicio { get; set; }
        public string razon_social { get; set; }
        public string descripcion { get; set; }

        public string descripcionf
        {
            get
            {
                return codigo + " - " + descripcion;
            }
            set
            {
            }
        }
        public string observacion { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public decimal monto { get; set; }
        public string IDUSUARIO { get; set; }
        public DateTime f_registro { get; set; }
        public string estado { get; set; }

        [Display(Name = "ESTADO")]
        [JsonProperty("Nestado")]
        public string NEstado
        {
            get
            {
                string nestado;
                switch (estado)
                {
                    case "1":
                        nestado = "ACTIVO";
                        break;
                    case "2":
                        nestado = "FINALIZADO";
                        break;
                    case "3":
                        nestado = "SUSPENDIDO";
                        break;
                    default:
                        nestado = "INACTIVO";
                        break;
                }
                return nestado;
            }
            set
            {
            }
        }

        public string IDMONEDA { get; set; }
        public string MONEDA { get; set; }
        public string servicio { get; set; }
        public int cant_compra { get; set; }
        public decimal precio { get; set; }
        public int fk_producto { get; set; }
        public int id_compra { get; set; }
        public int fk_orden_compra { get; set; }
        public int fk_empresa_compra { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public DateTime f_compra { get; set; }
        public string df_compra
        {
            get
            {
                if (f_compra.ToString("dd/MM/yyyy") == "01/01/1901")
                {
                    return "";
                }
                else
                {
                    return f_compra.ToString("dd/MM/yyyy");
                }
            }
            set
            {
            }
        }
        public string n_compra { get; set; }
        public string IDUSUARIO_COMPRA { get; set; }
        public int id_empresa { get; set; }


        public string ruc { get; set; }
        public string razon_social_compra { get; set; }
        public string direccion { get; set; }
        public string estado_empresa { get; set; }
        public string nombres { get; set; }
        public string A_PATERNO { get; set; }
        public string A_MATERNO { get; set; }
        public int id_producto { get; set; }
        public int fk_producto_marca { get; set; }
        public int fk_producto_familia { get; set; }
        public string cod_producto { get; set; }
        public string nom_producto { get; set; }
        public string codigo_sku { get; set; }
        public string estado_producto { get; set; }
        public int id_producto_marca { get; set; }
        public string descripcion_producto_marca { get; set; }
        public string estado_producto_marca { get; set; }
        public int id_producto_familia { get; set; }
        public string codigo_producto_familia { get; set; }
        public string descripcion_producto_familia { get; set; }
        public string estado_familia { get; set; }
        public int id_compra_detalle { get; set; }
        public int fk_compra { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantidad_recibida { get; set; }
        public decimal precio_total { get; set; }
        public string descripcion_servicio { get; set; }
        public string ruc_cliente { get; set; }
    }
}