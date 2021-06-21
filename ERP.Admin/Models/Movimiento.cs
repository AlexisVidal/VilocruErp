using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Movimiento
    {
        [Display(Name = "Id_movimiento")]
        [JsonProperty("id_movimiento")]
        public int id_movimiento { get; set; }

        [Display(Name = "Fk_movimiento_tipo")]
        [JsonProperty("fk_movimiento_tipo")]
        public int fk_movimiento_tipo { get; set; }

        [Display(Name = "Fk_guia_remision_detalle")]
        [JsonProperty("fk_guia_remision_detalle")]
        public int fk_guia_remision_detalle { get; set; }

        [Display(Name = "Fk_venta_detalle")]
        [JsonProperty("fk_venta_detalle")]
        public int fk_venta_detalle { get; set; }

        [Display(Name = "fk_comprobante_traslado_detalle")]
        [JsonProperty("fk_comprobante_traslado_detalle")]
        public int fk_comprobante_traslado_detalle { get; set; }

        [Display(Name = "fk_nota_credito_detalle")]
        [JsonProperty("fk_nota_credito_detalle")]
        public int fk_nota_credito_detalle { get; set; }

        [Display(Name = "Fk_almacen")]
        [JsonProperty("fk_almacen")]
        public int fk_almacen { get; set; }

        [Display(Name = "Fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "F_movimiento")]
        [JsonProperty("f_movimiento")]
        public string f_movimiento { get; set; }

        [Display(Name = "Cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "0")
                {
                    nestado = "ANULADO";
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

        //Otras
        [Display(Name = "Fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        //KARDEX
        [Display(Name = "ingreso_salida")]
        [JsonProperty("ingreso_salida")]
        public string ingreso_salida { get; set; }

        [Display(Name = "descripcion_movimiento_tipo")]
        [JsonProperty("descripcion_movimiento_tipo")]
        public string descripcion_movimiento_tipo { get; set; }

        [Display(Name = "codigo_comprobante_ingreso")]
        [JsonProperty("codigo_comprobante_ingreso")]
        public string codigo_comprobante_ingreso { get; set; }

        [Display(Name = "tipo_comprobante_ingreso")]
        [JsonProperty("tipo_comprobante_ingreso")]
        public string tipo_comprobante_ingreso { get; set; }

        [Display(Name = "nro_comprobante_ingreso")]
        [JsonProperty("nro_comprobante_ingreso")]
        public string nro_comprobante_ingreso { get; set; }

        [Display(Name = "f_emision_ingreso")]
        [JsonProperty("f_emision_ingreso")]
        public DateTime f_emision_ingreso { get; set; }

        [Display(Name = "precio_ingreso")]
        [JsonProperty("precio_ingreso")]
        public decimal precio_ingreso { get; set; }

        [Display(Name = "tipo_afectacion_igv_ingreso")]
        [JsonProperty("tipo_afectacion_igv_ingreso")]
        public int tipo_afectacion_igv_ingreso { get; set; }

        [Display(Name = "flag_afecto_igv_ingreso")]
        [JsonProperty("flag_afecto_igv_ingreso")]
        public string flag_afecto_igv_ingreso { get; set; }

        [Display(Name = "porcentaje_igv_ingreso")]
        [JsonProperty("porcentaje_igv_ingreso")]
        public decimal porcentaje_igv_ingreso { get; set; }

        [Display(Name = "tipo_isc_ingreso")]
        [JsonProperty("tipo_isc_ingreso")]
        public int tipo_isc_ingreso { get; set; }

        [Display(Name = "codigo_comprobante_salida")]
        [JsonProperty("codigo_comprobante_salida")]
        public string codigo_comprobante_salida { get; set; }

        [Display(Name = "tipo_comprobante_salida")]
        [JsonProperty("tipo_comprobante_salida")]
        public string tipo_comprobante_salida { get; set; }

        [Display(Name = "nro_comprobante_salida")]
        [JsonProperty("nro_comprobante_salida")]
        public string nro_comprobante_salida { get; set; }

        [Display(Name = "f_emision_salida")]
        [JsonProperty("f_emision_salida")]
        public DateTime f_emision_salida { get; set; }

        [Display(Name = "precio_salida")]
        [JsonProperty("precio_salida")]
        public decimal precio_salida { get; set; }

        [Display(Name = "tipo_afectacion_igv_salida")]
        [JsonProperty("tipo_afectacion_igv_salida")]
        public int tipo_afectacion_igv_salida { get; set; }

        [Display(Name = "flag_afecto_igv_salida")]
        [JsonProperty("flag_afecto_igv_salida")]
        public string flag_afecto_igv_salida { get; set; }

        [Display(Name = "porcentaje_igv_salida")]
        [JsonProperty("porcentaje_igv_salida")]
        public decimal porcentaje_igv_salida { get; set; }

        [Display(Name = "tipo_isc_salida")]
        [JsonProperty("tipo_isc_salida")]
        public int tipo_isc_salida { get; set; }

        [Display(Name = "fk_tipo_moneda")]
        [JsonProperty("fk_tipo_moneda")]
        public int fk_tipo_moneda { get; set; }

        [Display(Name = "tipo_cambio")]
        [JsonProperty("tipo_cambio")]
        public decimal tipo_cambio { get; set; }
        public int nro_inventario { get; set; }
        public string f_inicio { get; set; }
        public string f_fin { get; set; }
        public string cod_producto { get; set; }
        public string nom_producto { get; set; }
        public string descripcion_marca { get; set; }
        public string descripcion_producto_subfamilia { get; set; }
        public decimal precio_costo { get; set; }
    }
}