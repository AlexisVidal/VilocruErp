using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class NotaDebitoDetalle
    {
        [Display(Name = "id_nota_debito_detalle")]
        [JsonProperty("id_nota_debito_detalle")]
        public int id_nota_debito_detalle { get; set; }

        [Display(Name = "fk_nota_debito")]
        [JsonProperty("fk_nota_debito")]
        public int fk_nota_debito { get; set; }

        [Display(Name = "fk_venta_detalle")]
        [JsonProperty("fk_venta_detalle")]
        public int fk_venta_detalle { get; set; }

        [Display(Name = "cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "precio")]
        [JsonProperty("precio")]
        public decimal precio { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        //NOTA CREDITO DETALLE
        [Display(Name = "Id_venta_detalle")]
        [JsonProperty("id_venta_detalle")]
        public int id_venta_detalle { get; set; }

        [Display(Name = "Fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        [Display(Name = "Fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "Fk_tipo_afectacion_igv")]
        [JsonProperty("fk_tipo_afectacion_igv")]
        public int fk_tipo_afectacion_igv { get; set; }

        [Display(Name = "Fk_tipo_isc")]
        [JsonProperty("fk_tipo_isc")]
        public int fk_tipo_isc { get; set; }

        [Display(Name = "cantidad_venta_detalle")]
        [JsonProperty("cantidad_venta_detalle")]
        public decimal cantidad_venta_detalle { get; set; }

        [Display(Name = "precio_venta_detalle")]
        [JsonProperty("precio_venta_detalle")]
        public decimal precio_venta_detalle { get; set; }

        [Display(Name = "estado_venta_detalle")]
        [JsonProperty("estado_venta_detalle")]
        public string estado_venta_detalle { get; set; }

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

        [Display(Name = "cantidad_movimiento")]
        [JsonProperty("cantidad_movimiento")]
        public decimal cantidad_movimiento { get; set; }

        [Display(Name = "nombre_almacen")]
        [JsonProperty("nombre_almacen")]
        public string nombre_almacen { get; set; }

        public string codigo_sunat { get; set; }
        public string descripcion { get; set; }
        public decimal valor_venta { get; set; }
        public decimal igv { get; set; }
        public decimal importe { get; set; }

        public string descripcion_comprobante_tipo { get; set; }

        public decimal total_igv { get; set; }
        public decimal total_neto { get; set; }
        public string nro_comprobante_comprobante_venta { get; set; }
        public int motivo_nc { get; set; }
        public string motivo { get; set; }
        public string sigla { get; set; }
        public decimal total_bruto { get; set; }
        public int id_cliente_natural { get; set; }
        public string dni_cliente_natural { get; set; }
        public string nombre_cliente_natural { get; set; }
        public string direccion_cliente_natural { get; set; }
        public string apellido_paterno_cliente_natural { get; set; }
        public string apellido_materno_cliente_natural { get; set; }
        public string ruc_empresa_cliente_juridico { get; set; }
        public string razon_social { get; set; }
        public string direccion_empresa { get; set; }
        public string cadena_para_codigo_qr { get; set; }

        public string tipo_bien { get; set; }
    }
}