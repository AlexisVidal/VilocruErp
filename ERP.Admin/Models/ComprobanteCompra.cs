using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class ComprobanteCompra
    {
        [Display(Name = "Id_comprobante_compra")]
        [JsonProperty("id_comprobante_compra")]
        public int id_comprobante_compra { get; set; }

        [Display(Name = "Kk_compra")]
        [JsonProperty("fk_compra")]
        public int fk_compra { get; set; }

        [Display(Name = "Fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "fk_tipo_moneda")]
        [JsonProperty("fk_tipo_moneda")]
        public int fk_tipo_moneda { get; set; }

        [Display(Name = "Nro_comprobante")]
        [JsonProperty("nro_comprobante")]
        public string nro_comprobante { get; set; }

        [Display(Name = "F_emision")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "Total_bruto")]
        [JsonProperty("total_bruto")]
        public decimal total_bruto { get; set; }

        [Display(Name = "Total_igv")]
        [JsonProperty("total_igv")]
        public decimal total_igv { get; set; }

        [Display(Name = "Total_isc")]
        [JsonProperty("total_isc")]
        public decimal total_isc { get; set; }

        [Display(Name = "Total_neto")]
        [JsonProperty("total_neto")]
        public decimal total_neto { get; set; }

        [Display(Name = "saldo_bruto")]
        [JsonProperty("saldo_bruto")]
        public decimal saldo_bruto { get; set; }

        [Display(Name = "flg_cronograma")]
        [JsonProperty("flg_cronograma")]
        public string flg_cronograma { get; set; }

        [Display(Name = "tipo_cambio")]
        [JsonProperty("tipo_cambio")]
        public decimal tipo_cambio { get; set; }

        [Display(Name = "tipo_compra")]
        [JsonProperty("tipo_compra")]
        public string tipo_compra { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

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
                    nestado = "REGISTRADA";
                }
                else if (estado == "2")
                {
                    nestado = "FINALIZADA";
                }
                return nestado;
            }
            set
            {
            }
        }

        //COMPRA
        [Display(Name = "Id_compra")]
        [JsonProperty("id_compra")]
        public int id_compra { get; set; }

        [Display(Name = "Fk_orden_compra")]
        [JsonProperty("fk_orden_compra")]
        public int fk_orden_compra { get; set; }

        [Display(Name = "Fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "Fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

        [Display(Name = "N_compra")]
        [JsonProperty("n_compra")]
        public string n_compra { get; set; }

        [Display(Name = "F_compra")]
        [JsonProperty("f_compra")]
        public string f_compra { get; set; }

        [Display(Name = "Estado_compra")]
        [JsonProperty("estado_compra")]
        public string estado_compra { get; set; }

        //PROVEEDOR
        [Display(Name = "Id_proveedor")]
        [JsonProperty("id_proveedor")]
        public int id_proveedor { get; set; }

        [Display(Name = "Fk_empresa")]
        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }

        [Display(Name = "Cod_proveedor")]
        [JsonProperty("cod_proveedor")]
        public string cod_proveedor { get; set; }

        [Display(Name = "Estado_proveedor")]
        [JsonProperty("estado_proveedor")]
        public string estado_proveedor { get; set; }

        //EMPRESA
        [Display(Name = "Id_empresa")]
        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }

        [Display(Name = "Ruc")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }

        [Display(Name = "Razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }

        [Display(Name = "Estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //USUARIO
        [Display(Name = "Id_usuario")]
        [JsonProperty("id_usuario")]
        public int id_usuario { get; set; }

        [Display(Name = "Fk_trabajador")]
        [JsonProperty("fk_trabajador")]
        public int fk_trabajador { get; set; }

        [Display(Name = "Estado_usuario")]
        [JsonProperty("estado_usuario")]
        public string estado_usuario { get; set; }

        //TRABAJADOR
        [Display(Name = "Id_trabajador")]
        [JsonProperty("id_trabajador")]
        public int id_trabajador { get; set; }

        [Display(Name = "Fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "Fk_tipo_trabajador")]
        [JsonProperty("fk_tipo_trabajador")]
        public int fk_tipo_trabajador { get; set; }

        [Display(Name = "Estado_trabajador")]
        [JsonProperty("estado_trabajador")]
        public string estado_trabajador { get; set; }

        //PERSONA
        [Display(Name = "Id_persona")]
        [JsonProperty("id_persona")]
        public int id_persona { get; set; }

        [Display(Name = "Nombres")]
        [JsonProperty("nombres")]
        public string nombres { get; set; }

        [Display(Name = "Apellido_paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }

        [Display(Name = "Apellido_materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }

        [Display(Name = "F_nacimiento")]
        [JsonProperty("f_nacimiento")]
        public string f_nacimiento { get; set; }

        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }

        [Display(Name = "Dni")]
        [JsonProperty("dni")]
        public string dni { get; set; }

        [Display(Name = "Estado_persona")]
        [JsonProperty("estado_persona")]
        public string estado_persona { get; set; }

        //COMPROBANTE TIPO
        [Display(Name = "Id_comprobante_tipo")]
        [JsonProperty("id_comprobante_tipo")]
        public int id_comprobante_tipo { get; set; }

        [Display(Name = "codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }
        
        [Display(Name = "Descripcion_comprobante_tipo")]
        [JsonProperty("descripcion_comprobante_tipo")]
        public string descripcion_comprobante_tipo { get; set; }

        [Display(Name = "Estado_comprobante_tipo")]
        [JsonProperty("estado_comprobante_tipo")]
        public string estado_comprobante_tipo { get; set; }

        //TIPO MONEDA
        [Display(Name = "id_tipo_moneda")]
        [JsonProperty("id_tipo_moneda")]
        public int id_tipo_moneda { get; set; }

        [Display(Name = "descripcion_tipo_moneda")]
        [JsonProperty("descripcion_tipo_moneda")]
        public string descripcion_tipo_moneda { get; set; }

        [Display(Name = "codigo_alfabetico")]
        [JsonProperty("codigo_alfabetico")]
        public string codigo_alfabetico { get; set; }

        [Display(Name = "codigo_numerico")]
        [JsonProperty("codigo_numerico")]
        public string codigo_numerico { get; set; }
    }
}