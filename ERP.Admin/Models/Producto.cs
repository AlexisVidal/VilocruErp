using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Producto
    {

        [Display(Name = "ID")]
        [JsonProperty("id_producto")]
        public int id_producto { get; set; }

        public int fk_almacen { get; set; }

        [JsonProperty("fk_producto_marca")]
        public int fk_producto_marca { get; set; }

       
        [Display(Name = "Codigo")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }
        [Display(Name = "Producto")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }
        [Display(Name = "SKU")]
        [JsonProperty("codigo_sku")]
        public string codigo_sku { get; set; }
       
        public string estado { get; set; }
        public string nombre_full { get; set; }

        public int id_producto_marca { get; set; }
        [Display(Name = "Marca")]
        [JsonProperty("descripcion_producto_marca")]
        public string descripcion_producto_marca { get; set; }
        public string estado_producto_marca { get; set; }
        
        [JsonProperty("id_precio_compra")]
        public int id_precio_compra { get; set; }
        [Display(Name = "P. Compra")]
        [JsonProperty("preciocompra")]
        public decimal preciocompra { get; set; }

        [Display(Name = "Dscto 1 %")]
        [JsonProperty("dsctounop")]
        public decimal dsctounop { get; set; }
        
        [Display(Name = "Dscto 2 %")]
        [JsonProperty("dsctodosp")]
        public decimal dsctodosp { get; set; }


        [Display(Name = "Dscto 3 %")]
        [JsonProperty("dsctotresp")]
        public decimal dsctotresp { get; set; }


        [Display(Name = "Dscto 4 %")]
        [JsonProperty("dsctocuatrop")]
        public decimal dsctocuatrop { get; set; }


        [Display(Name = "P. Compra")]
        [JsonProperty("preciocomprafinal")]
        public decimal preciocomprafinal { get; set; }


        [Display(Name = "Estado")]
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "1")
                {
                    nestado = "ACTIVO";
                }
                else
                {
                    nestado = "INACTIVO";
                }
                return nestado;
            }
            set
            {
            }
        }
        
        [JsonProperty("id_proveedor")]
        public int id_proveedor { get; set; }


        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }


        [Display(Name = "Cod Proveedor")]
        [JsonProperty("cod_proveedor")]
        public string cod_proveedor { get; set; }


        [Display(Name = "Contacto")]
        [JsonProperty("contacto")]
        public string contacto { get; set; }


        [Display(Name = "Telefono")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }


        [Display(Name = "Nro Cuenta")]
        [JsonProperty("nro_cuenta")]
        public string nro_cuenta { get; set; }

        [JsonProperty("estado_proveedor")]
        public string estado_proveedor { get; set; }

        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }

        [Display(Name = "RUC")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }

        [Display(Name = "Razon Social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        [JsonProperty("id_producto_precio_venta")]
        public int id_producto_precio_venta { get; set; }

        [JsonProperty("fk_producto_precio_venta")]
        public int fk_producto_precio_venta { get; set; }

        [JsonProperty("fk_precio_compra_precio_venta")]
        public decimal fk_precio_compra_precio_venta { get; set; }
        [Display(Name = "P. V. Unidad")]
        [JsonProperty("precio_unidad_precio_venta")]
        public decimal precio_unidad_precio_venta { get; set; }
        [Display(Name = "P. V. Mayor")]
        [JsonProperty("precio_mayor_precio_venta")]
        public decimal precio_mayor_precio_venta { get; set; }
        [Display(Name = "P. V. Especial")]
        [JsonProperty("precio_especial_precio_venta")]
        public decimal precio_especial_precio_venta { get; set; }

        [Display(Name = "Tipo Cambio")]
        [JsonProperty("tipo_cambio_precio_venta")]
        public decimal tipo_cambio_precio_venta { get; set; }

        [JsonProperty("estado_precio_venta")]
        public string estado_precio_venta { get; set; }

        //Tipo de afectacion de igv
        [JsonProperty("id_tipo_afectacion_igv")]
        public int id_tipo_afectacion_igv { get; set; }

        [JsonProperty("fk_igv")]
        public int fk_igv { get; set; }

        [JsonProperty("codigo_tipo_igv")]
        public string codigo_tipo_igv { get; set; }

        [JsonProperty("descripcion_tipo_afectacion")]
        public string descripcion_tipo_afectacion { get; set; }

        [JsonProperty("flag_afecto_igv")]
        public string flag_afecto_igv { get; set; }

        [JsonProperty("flag_tipo_afecto")]
        public string flag_tipo_afecto { get; set; }

        //IGV
        [JsonProperty("id_igv")]
        public int id_igv { get; set; }

        [JsonProperty("codigo_igv")]
        public string codigo_igv { get; set; }

        [JsonProperty("descripcion_igv")]
        public string descripcion_igv { get; set; }

        [JsonProperty("un_ece")]
        public string un_ece { get; set; }

        [JsonProperty("porcentaje")]
        public string porcentaje { get; set; }

        //TIPO MONEDA
        [JsonProperty("id_tipo_moneda")]
        public int id_tipo_moneda { get; set; }

        [Display(Name = "Moneda")]
        [JsonProperty("descripcion_tipo_moneda")]
        public string descripcion_tipo_moneda { get; set; }

        [JsonProperty("codigo_alfabetico")]
        public string codigo_alfabetico { get; set; }

        [JsonProperty("codigo_numerico")]
        public string codigo_numerico { get; set; }

        [JsonProperty("existencia")]
        public decimal existencia { get; set; }
        
        //Otros
        public decimal total_ingreso { get; set; }
        public decimal total_salida { get; set; }
        public decimal precio_costo { get; set; }

        public decimal cant_items { get; set; }
    }
}