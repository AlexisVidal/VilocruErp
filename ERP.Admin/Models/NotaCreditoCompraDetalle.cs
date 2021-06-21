using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class NotaCreditoCompraDetalle
    {
        [Display(Name = "id_nota_credito_compra_detalle")]
        [JsonProperty("id_nota_credito_compra_detalle")]
        public int id_nota_credito_compra_detalle { get; set; }

        [Display(Name = "fk_nota_credito_compra")]
        [JsonProperty("fk_nota_credito_compra")]
        public int fk_nota_credito_compra { get; set; }

        [Display(Name = "fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "precio")]
        [JsonProperty("precio")]
        public decimal precio { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

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
    }
}