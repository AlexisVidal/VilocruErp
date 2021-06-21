using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class NotaCreditoCompra
    {
        [Display(Name = "id_nota_credito_compra")]
        [JsonProperty("id_nota_credito_compra")]
        public int id_nota_credito_compra { get; set; }

        [Display(Name = "fk_comprobante_compra")]
        [JsonProperty("fk_comprobante_compra")]
        public int fk_comprobante_compra { get; set; }

        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "nro_comprobante")]
        [JsonProperty("nro_comprobante")]
        public string nro_comprobante { get; set; }

        [Display(Name = "f_emision")]
        [JsonProperty("f_emision")]
        public DateTime f_emision { get; set; }

        [Display(Name = "total_bruto")]
        [JsonProperty("total_bruto")]
        public decimal total_bruto { get; set; }

        [Display(Name = "total_igv")]
        [JsonProperty("total_igv")]
        public decimal total_igv { get; set; }

        [Display(Name = "total_isc")]
        [JsonProperty("total_isc")]
        public decimal total_isc { get; set; }

        [Display(Name = "total_neto")]
        [JsonProperty("total_neto")]
        public decimal total_neto { get; set; }

        [Display(Name = "motivo")]
        [JsonProperty("motivo")]
        public string motivo { get; set; }

        [Display(Name = "flg_afecta_stock")]
        [JsonProperty("flg_afecta_stock")]
        public string flg_afecta_stock { get; set; }

        [Display(Name = "saldo_disponible")]
        [JsonProperty("saldo_disponible")]
        public decimal saldo_disponible { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        //COMPROBANTE_COMPRA
        [Display(Name = "id_comprobante_compra")]
        [JsonProperty("id_comprobante_compra")]
        public int id_comprobante_compra { get; set; }

        [Display(Name = "fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

        [Display(Name = "fk_compra")]
        [JsonProperty("fk_compra")]
        public int fk_compra { get; set; }

        [Display(Name = "fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "fk_tipo_moneda")]
        [JsonProperty("fk_tipo_moneda")]
        public int fk_tipo_moneda { get; set; }

        [Display(Name = "nro_comprobante_compra")]
        [JsonProperty("nro_comprobante_compra")]
        public string nro_comprobante_compra { get; set; }

        [Display(Name = "f_emision_comprobante_compra")]
        [JsonProperty("f_emision_comprobante_compra")]
        public string f_emision_comprobante_compra { get; set; }

        [Display(Name = "total_bruto_comprobante_compra")]
        [JsonProperty("total_bruto_comprobante_compra")]
        public decimal total_bruto_comprobante_compra { get; set; }

        [Display(Name = "total_igv_comprobante_compra")]
        [JsonProperty("total_igv_comprobante_compra")]
        public decimal total_igv_comprobante_compra { get; set; }

        [Display(Name = "total_isc_comprobante_compra")]
        [JsonProperty("total_isc_comprobante_compra")]
        public decimal total_isc_comprobante_compra { get; set; }

        [Display(Name = "total_neto_comprobante_compra")]
        [JsonProperty("total_neto_comprobante_compra")]
        public decimal total_neto_comprobante_compra { get; set; }

        [Display(Name = "saldo_bruto")]
        [JsonProperty("saldo_bruto")]
        public decimal saldo_bruto { get; set; }

        [Display(Name = "estado_comprobante_compra")]
        [JsonProperty("estado_comprobante_compra")]
        public string estado_comprobante_compra { get; set; }

        //COMPROBANTE_TIPO_COMPROBANTE_COMPRA
        [Display(Name = "id_comprobante_tipo")]
        [JsonProperty("id_comprobante_tipo")]
        public int id_comprobante_tipo { get; set; }

        [Display(Name = "codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }

        [Display(Name = "descripcion_comprobante_tipo")]
        [JsonProperty("descripcion_comprobante_tipo")]
        public string descripcion_comprobante_tipo { get; set; }

        [Display(Name = "estado_comprobante_tipo")]
        [JsonProperty("estado_comprobante_tipo")]
        public string estado_comprobante_tipo { get; set; }

        //TIPO_MONEDA
        [Display(Name = "id_tipo_moneda")]
        [JsonProperty("id_tipo_moneda")]
        public int id_tipo_moneda { get; set; }

        [Display(Name = "descripcion_tipo_moneda")]
        [JsonProperty("descripcion_tipo_moneda")]
        public string descripcion_tipo_moneda { get; set; }

        //PROVEEDOR
        [Display(Name = "id_proveedor")]
        [JsonProperty("id_proveedor")]
        public int id_proveedor { get; set; }

        [Display(Name = "fk_empresa")]
        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }

        [Display(Name = "cod_proveedor")]
        [JsonProperty("cod_proveedor")]
        public string cod_proveedor { get; set; }

        [Display(Name = "contacto")]
        [JsonProperty("contacto")]
        public string contacto { get; set; }

        [Display(Name = "telefono")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }

        [Display(Name = "nro_cuenta")]
        [JsonProperty("nro_cuenta")]
        public string nro_cuenta { get; set; }

        [Display(Name = "estado_proveedor")]
        [JsonProperty("estado_proveedor")]
        public string estado_proveedor { get; set; }

        //EMPRESA
        [Display(Name = "id_empresa")]
        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }

        [Display(Name = "ruc")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }

        [Display(Name = "razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }

        [Display(Name = "estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //COMPRA
        [Display(Name = "n_compra")]
        [JsonProperty("n_compra")]
        public string n_compra { get; set; }
        
    }
}