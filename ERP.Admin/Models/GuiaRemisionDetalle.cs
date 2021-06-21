using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class GuiaRemisionDetalle
    {
        [Display(Name = "Id_guia_remision_detalle")]
        [JsonProperty("id_guia_remision_detalle")]
        public int id_guia_remision_detalle { get; set; }

        [Display(Name = "Fk_guia_remision")]
        [JsonProperty("fk_guia_remision")]
        public int fk_guia_remision { get; set; }

        [Display(Name = "Fk_compra_detalle")]
        [JsonProperty("fk_compra_detalle")]
        public int fk_compra_detalle { get; set; }

        [Display(Name = "Cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        //Guia de Remision
        [Display(Name = "Id_guia_remision")]
        [JsonProperty("id_guia_remision")]
        public int id_guia_remision { get; set; }

        [Display(Name = "Fk_compra")]
        [JsonProperty("fk_compra")]
        public int fk_compra { get; set; }

        [Display(Name = "Fk_conductor")]
        [JsonProperty("fk_conductor")]
        public int fk_conductor { get; set; }

        [Display(Name = "Fk_transporte")]
        [JsonProperty("fk_transporte")]
        public int fk_transporte { get; set; }

        [Display(Name = "Nro_guia")]
        [JsonProperty("nro_guia")]
        public string nro_guia { get; set; }

        [Display(Name = "F_emision")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "F_traslado")]
        [JsonProperty("f_traslado")]
        public string f_traslado { get; set; }

        [Display(Name = "Estado_guia_remision")]
        [JsonProperty("estado_guia_remision")]
        public string estado_guia_remision { get; set; }

        //CompraDetalle
        [Display(Name = "Id_compra_detalle")]
        [JsonProperty("id_compra_detalle")]
        public int id_compra_detalle { get; set; }

        [Display(Name = "Fk_compra_compra_detalle")]
        [JsonProperty("fk_compra_compra_detalle")]
        public int fk_compra_compra_detalle { get; set; }

        [Display(Name = "Fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "Cantidad_compra")]
        [JsonProperty("cantidad_compra")]
        public decimal cantidad_compra { get; set; }

        [Display(Name = "Cantidad_recibida")]
        [JsonProperty("cantidad_recibida")]
        public decimal cantidad_recibida { get; set; }

        [Display(Name = "Precio_compra")]
        [JsonProperty("precio_compra")]
        public decimal precio_compra { get; set; }

        [Display(Name = "Estado_compra")]
        [JsonProperty("estado_compra")]
        public string estado_compra { get; set; }

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

        [Display(Name = "cod_producto")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }

        [Display(Name = "Codigo_sku")]
        [JsonProperty("codigo_sku")]
        public string codigo_sku { get; set; }

        [Display(Name = "Embalaje")]
        [JsonProperty("embalaje")]
        public string embalaje { get; set; }

        [Display(Name = "Nom_producto")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }

        [Display(Name = "Image_url")]
        [JsonProperty("image_url")]
        public string image_url { get; set; }

        [Display(Name = "Estado_producto")]
        [JsonProperty("estado_producto")]
        public string estado_producto { get; set; }

        //Producto Marca
        [Display(Name = "Id_producto_marca")]
        [JsonProperty("id_producto_marca")]
        public int id_producto_marca { get; set; }

        [Display(Name = "Descripcion_producto_marca")]
        [JsonProperty("descripcion_producto_marca")]
        public string descripcion_producto_marca { get; set; }

        [Display(Name = "Estado_producto_marca")]
        [JsonProperty("estado_producto_marca")]
        public string estado_producto_marca { get; set; }

        //Producto SubFamilia
        [Display(Name = "Id_producto_subfamilia")]
        [JsonProperty("id_producto_subfamilia")]
        public int id_producto_subfamilia { get; set; }

        [Display(Name = "Fk_producto_familia")]
        [JsonProperty("fk_producto_familia")]
        public int fk_producto_familia { get; set; }

        [Display(Name = "Codigo_producto_subfamilia")]
        [JsonProperty("codigo_producto_subfamilia")]
        public string codigo_producto_subfamilia { get; set; }

        [Display(Name = "Descripcion_producto_subfamilia")]
        [JsonProperty("descripcion_producto_subfamilia")]
        public string descripcion_producto_subfamilia { get; set; }

        [Display(Name = "Estado_subfamilia")]
        [JsonProperty("estado_subfamilia")]
        public int estado_subfamilia { get; set; }

        //IGV
        [Display(Name = "fk_tipo_afectacion_igv")]
        [JsonProperty("fk_tipo_afectacion_igv")]
        public int fk_tipo_afectacion_igv { get; set; }

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
        public decimal porcentaje { get; set; }
    }
}