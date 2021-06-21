using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class SolicitudTrasladoDetalle
    {
        [Display(Name = "id_solicitud_traslado_detalle")]
        [JsonProperty("id_solicitud_traslado_detalle")]
        public int id_solicitud_traslado_detalle { get; set; }

        [Display(Name = "fk_solicitud_traslado")]
        [JsonProperty("fk_solicitud_traslado")]
        public int fk_solicitud_traslado { get; set; }

        [Display(Name = "fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "CANT. SOLICITADA")]
        [JsonProperty("cantidad_solicitada")]
        public decimal cantidad_solicitada { get; set; }

        [Display(Name = "CANT. ATENDIDA")]
        [JsonProperty("cantidad_atendida")]
        public decimal cantidad_atendida { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        string NEstado
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
            set { }
        }

        //PRODCUCTO
        [Display(Name = "id_producto")]
        [JsonProperty("id_producto")]
        public int id_producto { get; set; }

        [Display(Name = "fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

        [Display(Name = "fk_producto_marca")]
        [JsonProperty("fk_producto_marca")]
        public int fk_producto_marca { get; set; }

        [Display(Name = "fk_producto_subfamilia")]
        [JsonProperty("fk_producto_subfamilia")]
        public int fk_producto_subfamilia { get; set; }

        [Display(Name = "CÓDIGO")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }

        [Display(Name = "PRODUCTO")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }

        [Display(Name = "CÓDIGO SKU")]
        [JsonProperty("codigo_sku")]
        public string codigo_sku { get; set; }

        [Display(Name = "embalaje")]
        [JsonProperty("embalaje")]
        public string embalaje { get; set; }

        [Display(Name = "image_url")]
        [JsonProperty("image_url")]
        public string image_url { get; set; }

        [Display(Name = "estado_producto")]
        [JsonProperty("estado_producto")]
        public string estado_producto { get; set; }

        //MARCA
        [Display(Name = "id_producto_marca")]
        [JsonProperty("id_producto_marca")]
        public int id_producto_marca { get; set; }

        [Display(Name = "MARCA")]
        [JsonProperty("descripcion_marca")]
        public string descripcion_marca { get; set; }

        [Display(Name = "estado_producto_marca")]
        [JsonProperty("estado_producto_marca")]
        public string estado_producto_marca { get; set; }

        //SUB FAMILIA
        [Display(Name = "id_producto_subfamilia")]
        [JsonProperty("id_producto_subfamilia")]
        public int id_producto_subfamilia { get; set; }

        [Display(Name = "fk_producto_familia")]
        [JsonProperty("fk_producto_familia")]
        public int fk_producto_familia { get; set; }

        [Display(Name = "codigo_subfamilia")]
        [JsonProperty("codigo_subfamilia")]
        public string codigo_subfamilia { get; set; }

        [Display(Name = "SUB-FAMILIA")]
        [JsonProperty("descripcion_producto_subfamilia")]
        public string descripcion_producto_subfamilia { get; set; }

        [Display(Name = "estado_producto_subfamilia")]
        [JsonProperty("estado_producto_subfamilia")]
        public string estado_producto_subfamilia { get; set; }

        //AFECTACION IGV
        [Display(Name = "id_tipo_afectacion_igv")]
        [JsonProperty("id_tipo_afectacion_igv")]
        public int id_tipo_afectacion_igv { get; set; }

        [Display(Name = "fk_igv")]
        [JsonProperty("fk_igv")]
        public int fk_igv { get; set; }

        [Display(Name = "codigo_tipo_igv")]
        [JsonProperty("codigo_tipo_igv")]
        public string codigo_tipo_igv { get; set; }

        [Display(Name = "descripcion_tipo_afectacion")]
        [JsonProperty("descripcion_tipo_afectacion")]
        public string descripcion_tipo_afectacion { get; set; }

        [Display(Name = "flag_afecto_igv")]
        [JsonProperty("flag_afecto_igv")]
        public string flag_afecto_igv { get; set; }

        [Display(Name = "flag_tipo_afecto")]
        [JsonProperty("flag_tipo_afecto")]
        public string flag_tipo_afecto { get; set; }

        //IGV
        [Display(Name = "id_igv")]
        [JsonProperty("id_igv")]
        public int id_igv { get; set; }

        [Display(Name = "codigo_igv")]
        [JsonProperty("codigo_igv")]
        public string codigo_igv { get; set; }

        [Display(Name = "descripcion_igv")]
        [JsonProperty("descripcion_igv")]
        public string descripcion_igv { get; set; }

        [Display(Name = "un_ece")]
        [JsonProperty("un_ece")]
        public string un_ece { get; set; }

        [Display(Name = "porcentaje")]
        [JsonProperty("porcentaje")]
        public string porcentaje { get; set; }
    }
}