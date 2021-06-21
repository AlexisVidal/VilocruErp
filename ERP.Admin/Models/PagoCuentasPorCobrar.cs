using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class PagoCuentasPorCobrar
    {
        [Display(Name = "id_pago_cuentas_por_cobrar")]
        [JsonProperty("id_pago_cuentas_por_cobrar")]
        public int id_pago_cuentas_por_cobrar { get; set; }

        [Display(Name = "fk_cuentas_por_cobrar")]
        [JsonProperty("fk_cuentas_por_cobrar")]
        public int fk_cuentas_por_cobrar { get; set; }

        [Display(Name = "fk_caja")]
        [JsonProperty("fk_caja")]
        public int fk_caja { get; set; }

        [Display(Name = "fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "fk_tarjeta_credito")]
        [JsonProperty("fk_tarjeta_credito")]
        public int fk_tarjeta_credito { get; set; }

        [Display(Name = "fk_nota_credito")]
        [JsonProperty("fk_nota_credito")]
        public int fk_nota_credito { get; set; }

        [Display(Name = "fk_banco")]
        [JsonProperty("fk_banco")]
        public int fk_banco { get; set; }

        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "f_pago")]
        [JsonProperty("f_pago")]
        public DateTime f_pago { get; set; }

        [Display(Name = "nro_documento")]
        [JsonProperty("nro_documento")]
        public string nro_documento { get; set; }

        [Display(Name = "monto")]
        [JsonProperty("monto")]
        public decimal monto { get; set; }

        [Display(Name = "f_registro")]
        [JsonProperty("f_registro")]
        public DateTime f_registro { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        //COMPROBANTE VENTA
        [Display(Name = "id_comprobante_venta")]
        [JsonProperty("id_comprobante_venta")]
        public int id_comprobante_venta { get; set; }

        [Display(Name = "fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        [Display(Name = "fk_caja_comprobante_venta")]
        [JsonProperty("fk_caja_comprobante_venta")]
        public int fk_caja_comprobante_venta { get; set; }

        [Display(Name = "f_emision")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "nro_comprobante")]
        [JsonProperty("nro_comprobante")]
        public string nro_comprobante { get; set; }

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

        [Display(Name = "saldo_bruto")]
        [JsonProperty("saldo_bruto")]
        public decimal saldo_bruto { get; set; }

        [Display(Name = "flg_cronograma")]
        [JsonProperty("flg_cronograma")]
        public string flg_cronograma { get; set; }

        [Display(Name = "estado_comprobante_venta")]
        [JsonProperty("estado_comprobante_venta")]
        public string estado_comprobante_venta { get; set; }

        //COMPROBANTE TIPO
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

        //VENTA
        [Display(Name = "id_venta")]
        [JsonProperty("id_venta")]
        public int id_venta { get; set; }

        [Display(Name = "fk_usuario_venta")]
        [JsonProperty("fk_usuario_venta")]
        public int fk_usuario_venta { get; set; }

        [Display(Name = "fk_usuario_pago")]
        [JsonProperty("fk_usuario_pago")]
        public int fk_usuario_pago { get; set; }

        [Display(Name = "fk_cliente")]
        [JsonProperty("fk_cliente")]
        public int fk_cliente { get; set; }

        [Display(Name = "fk_pedido")]
        [JsonProperty("fk_pedido")]
        public int fk_pedido { get; set; }

        [Display(Name = "n_venta")]
        [JsonProperty("n_venta")]
        public string n_venta { get; set; }

        [Display(Name = "f_venta")]
        [JsonProperty("f_venta")]
        public string f_venta { get; set; }

        [Display(Name = "estado_venta")]
        [JsonProperty("estado_venta")]
        public string estado_venta { get; set; }

        //PEDIDO
        [Display(Name = "id_pedido")]
        [JsonProperty("id_pedido")]
        public int id_pedido { get; set; }

        [Display(Name = "fk_cliente_pedido")]
        [JsonProperty("fk_cliente_pedido")]
        public int fk_cliente_pedido { get; set; }

        [Display(Name = "fk_userventa")]
        [JsonProperty("fk_userventa")]
        public int fk_userventa { get; set; }

        [Display(Name = "n_pedido")]
        [JsonProperty("n_pedido")]
        public string n_pedido { get; set; }

        [Display(Name = "f_pedido")]
        [JsonProperty("f_pedido")]
        public string f_pedido { get; set; }

        [Display(Name = "estado_pedido")]
        [JsonProperty("estado_pedido")]
        public string estado_pedido { get; set; }

        //CLIENTE
        [Display(Name = "id_cliente")]
        [JsonProperty("id_cliente")]
        public int id_cliente { get; set; }

        [Display(Name = "fk_cliente_tipo")]
        [JsonProperty("fk_cliente_tipo")]
        public int fk_cliente_tipo { get; set; }

        [Display(Name = "estado_cliente")]
        [JsonProperty("estado_cliente")]
        public string estado_cliente { get; set; }

        //CLIENTE TIPO
        [Display(Name = "id_cliente_tipo")]
        [JsonProperty("id_cliente_tipo")]
        public int id_cliente_tipo { get; set; }

        [Display(Name = "descripcion_cliente_tipo")]
        [JsonProperty("descripcion_cliente_tipo")]
        public string descripcion_cliente_tipo { get; set; }

        [Display(Name = "estado_cliente_tipo")]
        [JsonProperty("estado_cliente_tipo")]
        public string estado_cliente_tipo { get; set; }

        //CLIENTE NATURAL
        [Display(Name = "id_cliente_natural")]
        [JsonProperty("id_cliente_natural")]
        public int id_cliente_natural { get; set; }

        [Display(Name = "fk_persona_cliente_natural")]
        [JsonProperty("fk_persona_cliente_natural")]
        public int fk_persona_cliente_natural { get; set; }

        [Display(Name = "fk_cliente_cliente_natural")]
        [JsonProperty("fk_cliente_cliente_natural")]
        public int fk_cliente_cliente_natural { get; set; }

        [Display(Name = "ruc_cliente_natural")]
        [JsonProperty("ruc_cliente_natural")]
        public string ruc_cliente_natural { get; set; }

        [Display(Name = "estado_cliente_natural")]
        [JsonProperty("estado_cliente_natural")]
        public string estado_cliente_natural { get; set; }

        //PERSONA CLIENTE NATURAL
        [Display(Name = "id_persona_cliente_natural")]
        [JsonProperty("id_persona_cliente_natural")]
        public int id_persona_cliente_natural { get; set; }

        [Display(Name = "nombre_cliente_natural")]
        [JsonProperty("nombre_cliente_natural")]
        public string nombre_cliente_natural { get; set; }

        [Display(Name = "apellido_paterno_cliente_natural")]
        [JsonProperty("apellido_paterno_cliente_natural")]
        public string apellido_paterno_cliente_natural { get; set; }

        [Display(Name = "apellido_materno_cliente_natural")]
        [JsonProperty("apellido_materno_cliente_natural")]
        public string apellido_materno_cliente_natural { get; set; }

        [Display(Name = "f_nacimiento_cliente_natural")]
        [JsonProperty("f_nacimiento_cliente_natural")]
        public string f_nacimiento_cliente_natural { get; set; }

        [Display(Name = "email_cliente_natural")]
        [JsonProperty("email_cliente_natural")]
        public string email_cliente_natural { get; set; }

        [Display(Name = "dni_cliente_natural")]
        [JsonProperty("dni_cliente_natural")]
        public string dni_cliente_natural { get; set; }

        [Display(Name = "estado_persona_cliente_natural")]
        [JsonProperty("estado_persona_cliente_natural")]
        public string estado_persona_cliente_natural { get; set; }

        //CLIENTE JURIDICO
        [Display(Name = "id_cliente_juridico")]
        [JsonProperty("id_cliente_juridico")]
        public int id_cliente_juridico { get; set; }

        [Display(Name = "fk_empresa_cliente_juridico")]
        [JsonProperty("fk_empresa_cliente_juridico")]
        public int fk_empresa_cliente_juridico { get; set; }

        [Display(Name = "fk_cliente_cliente_juridico")]
        [JsonProperty("fk_cliente_cliente_juridico")]
        public int fk_cliente_cliente_juridico { get; set; }

        [Display(Name = "estado_cliente_juridico")]
        [JsonProperty("estado_cliente_juridico")]
        public string estado_cliente_juridico { get; set; }

        //EMPRESA CLIENTE JURIDICO
        [Display(Name = "id_empresa_cliente_juridico")]
        [JsonProperty("id_empresa_cliente_juridico")]
        public int id_empresa_cliente_juridico { get; set; }

        [Display(Name = "ruc_empresa_cliente_juridico")]
        [JsonProperty("ruc_empresa_cliente_juridico")]
        public string ruc_empresa_cliente_juridico { get; set; }

        [Display(Name = "razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "direccion_empresa")]
        [JsonProperty("direccion_empresa")]
        public string direccion_empresa { get; set; }

        [Display(Name = "estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //MEDIO DE PAGO
        [Display(Name = "descripcion_medio_pago")]
        [JsonProperty("descripcion_medio_pago")]
        public string descripcion_medio_pago { get; set; }
    }
}