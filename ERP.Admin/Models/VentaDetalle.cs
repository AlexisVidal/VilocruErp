using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class VentaDetalle
    {
        [Display(Name = "Id_venta_detalle")]
        [JsonProperty("id_venta_detalle")]
        public int id_venta_detalle { get; set; }

        [Display(Name = "Fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        [Display(Name = "Fk_proyecto")]
        [JsonProperty("fk_proyecto")]
        public int fk_proyecto { get; set; }

        [Display(Name = "Fk_tipo_afectacion_igv")]
        [JsonProperty("fk_tipo_afectacion_igv")]
        public int fk_tipo_afectacion_igv { get; set; }

        [Display(Name = "Fk_tipo_isc")]
        [JsonProperty("fk_tipo_isc")]
        public int fk_tipo_isc { get; set; }

        [Display(Name = "Cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "Precio")]
        [JsonProperty("precio")]
        public decimal precio { get; set; }

        [Display(Name = "Valor Venta")]
        [JsonProperty("valor_venta")]
        public decimal valor_venta { get; set; }
        [Display(Name = "IGV")]
        [JsonProperty("igv")]
        public decimal igv { get; set; }
        [Display(Name = "Importe")]
        [JsonProperty("importe")]
        public decimal importe { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "-1")
                {
                    nestado = "TODAS";
                }
                else if (estado == "0")
                {
                    nestado = "ANULADA";
                }
                else if (estado == "1")
                {
                    nestado = "VIGENTE";
                }
                return nestado;
            }
            set
            {

            }
        }

        //Producto
        [Display(Name = "Id_producto")]
        [JsonProperty("id_producto")]
        public int id_producto { get; set; }

        [Display(Name = "Fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

        [Display(Name = "Fk_producto_marca")]
        [JsonProperty("fk_producto_marca")]
        public int fk_producto_marca { get; set; }

        [Display(Name = "Fk_producto_subfamilia")]
        [JsonProperty("fk_producto_subfamilia")]
        public int fk_producto_subfamilia { get; set; }

        [Display(Name = "Cod_producto")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }

        [Display(Name = "Nom_producto")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }

        [Display(Name = "Codigo_sku")]
        [JsonProperty("codigo_sku")]
        public string codigo_sku { get; set; }

        [Display(Name = "Embalaje")]
        [JsonProperty("embalaje")]
        public string embalaje { get; set; }

        [Display(Name = "Image_url")]
        [JsonProperty("image_url")]
        public string image_url { get; set; }

        [Display(Name = "Estado_producto")]
        [JsonProperty("estado_producto")]
        public string estado_producto { get; set; }

        //Marca
        [Display(Name = "Id_producto_marca")]
        [JsonProperty("id_producto_marca")]
        public int id_producto_marca { get; set; }

        [Display(Name = "Descripcion_marca")]
        [JsonProperty("descripcion_marca")]
        public string descripcion_marca { get; set; }

        [Display(Name = "Estado_producto_marca")]
        [JsonProperty("estado_producto_marca")]
        public string estado_producto_marca { get; set; }

        //SubFamilia
        [Display(Name = "Id_producto_subfamilia")]
        [JsonProperty("id_producto_subfamilia")]
        public int id_producto_subfamilia { get; set; }

        [Display(Name = "Fk_producto_familia")]
        [JsonProperty("fk_producto_familia")]
        public int fk_producto_familia { get; set; }

        [Display(Name = "Codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }

        [Display(Name = "Descripcion_producto_subfamilia")]
        [JsonProperty("descripcion_producto_subfamilia")]
        public string descripcion_producto_subfamilia { get; set; }

        [Display(Name = "estado_producto_subfamilia")]
        [JsonProperty("estado_producto_subfamilia")]
        public string estado_producto_subfamilia { get; set; }

        //Tipo de afectacion de igv
        [Display(Name = "Id_tipo_afectacion_igv")]
        [JsonProperty("id_tipo_afectacion_igv")]
        public int id_tipo_afectacion_igv { get; set; }

        [Display(Name = "Fk_igv")]
        [JsonProperty("fk_igv")]
        public int fk_igv { get; set; }

        [Display(Name = "Codigo_tipo_igv")]
        [JsonProperty("codigo_tipo_igv")]
        public string codigo_tipo_igv { get; set; }

        [Display(Name = "Descripcion_tipo_afectacion")]
        [JsonProperty("descripcion_tipo_afectacion")]
        public string descripcion_tipo_afectacion { get; set; }

        [Display(Name = "Flag_afecto_igv")]
        [JsonProperty("flag_afecto_igv")]
        public string flag_afecto_igv { get; set; }

        [Display(Name = "Flag_tipo_afecto")]
        [JsonProperty("flag_tipo_afecto")]
        public string flag_tipo_afecto { get; set; }

        //Igv
        [Display(Name = "Id_igv")]
        [JsonProperty("id_igv")]
        public int id_igv { get; set; }

        [Display(Name = "Codigo_igv")]
        [JsonProperty("codigo_igv")]
        public string codigo_igv { get; set; }

        [Display(Name = "Descripcion_igv")]
        [JsonProperty("descripcion_igv")]
        public string descripcion_igv { get; set; }

        [Display(Name = "Un_ece")]
        [JsonProperty("un_ece")]
        public string un_ece { get; set; }

        [Display(Name = "Porcentaje")]
        [JsonProperty("porcentaje")]
        public decimal porcentaje { get; set; }

        //Otros
        [Display(Name = "Str_distribuir")]
        [JsonProperty("str_distribuir")]
        public string str_distribuir { get; set; }

        //DESPACHO
        [Display(Name = "descripcion_comprobante")]
        [JsonProperty("descripcion_comprobante")]
        public string descripcion_comprobante { get; set; }

        [Display(Name = "nro_comprobante")]
        [JsonProperty("nro_comprobante")]
        public string nro_comprobante { get; set; }

        [Display(Name = "f_emision")]
        [JsonProperty("f_emision")]
        public DateTime f_emision { get; set; }

        public string FechaTexto
        {
            get
            {
                string fechaTexto = "";
                DateTime fechax = Convert.ToDateTime(f_emision);
                fechaTexto = fechax.ToString("d");
                return fechaTexto;
            }
            set
            {

            }

        }

        [Display(Name = "nombre_cliente")]
        [JsonProperty("nombre_cliente")]
        public string nombre_cliente { get; set; }

        [Display(Name = "cantidad_venta_detalle")]
        [JsonProperty("cantidad_venta_detalle")]
        public decimal cantidad_venta_detalle { get; set; }

        [Display(Name = "cantidad_movimiento")]
        [JsonProperty("cantidad_movimiento")]
        public decimal cantidad_movimiento { get; set; }

        [Display(Name = "nombre_almacen")]
        [JsonProperty("nombre_almacen")]
        public string nombre_almacen { get; set; }

        public string descripcion_comprobante_tipo { get; set; }
        public string str_f_emision { get; set; }
        public decimal total_bruto { get; set; }

        public string codigo_sunat { get; set; }
        public string estado_pedido_detalle { get; set; }
        public string estado_venta { get; set; }
        public string estado_comprobante_venta { get; set; }
        public string codigo_afectacion { get; set; }
        public string descripcion_afectacion { get; set; }
        public string estado_afectacion { get; set; }


        [Display(Name = "ID")]
        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }
        [Display(Name = "Ruc")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }
        [Display(Name = "Razon Social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }
        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direcion_empresa { get; set; }
       
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }
        [Display(Name = "Contacto")]
        [JsonProperty("contacto")]
        public string contacto { get; set; }
        [Display(Name = "Telefono")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }

        [Display(Name = "Tipo")]
        [JsonProperty("tipo")]
        public string tipo { get; set; }

        public string tipo_bien { get; set; }
        public string tipoina { get; set; }


    }
}