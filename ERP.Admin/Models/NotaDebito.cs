﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class NotaDebito
    {
        [Display(Name = "id_nota_debito")]
        [JsonProperty("id_nota_debito")]
        public int id_nota_debito { get; set; }

        [Display(Name = "fk_comprobante_venta")]
        [JsonProperty("fk_comprobante_venta")]
        public int fk_comprobante_venta { get; set; }

        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public string fk_usuario { get; set; }

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
        public int motivo_nc { get; set; }

        [Display(Name = "flg_afecta_stock")]
        [JsonProperty("flg_afecta_stock")]
        public string flg_afecta_stock { get; set; }

        [Display(Name = "saldo_disponible")]
        [JsonProperty("saldo_disponible")]
        public decimal saldo_disponible { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        //COMPROBANTE VENTA
        [Display(Name = "Id_comprobante_venta")]
        [JsonProperty("id_comprobante_venta")]
        public int id_comprobante_venta { get; set; }

        [Display(Name = "Fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "Fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        [Display(Name = "Fk_caja")]
        [JsonProperty("fk_caja")]
        public int fk_caja { get; set; }

        [Display(Name = "Fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "f_emision_comprobante_venta")]
        [JsonProperty("f_emision_comprobante_venta")]
        public string f_emision_comprobante_venta { get; set; }

        [Display(Name = "nro_comprobante_comprobante_venta")]
        [JsonProperty("nro_comprobante_comprobante_venta")]
        public string nro_comprobante_comprobante_venta { get; set; }

        [Display(Name = "total_bruto_comprobante_venta")]
        [JsonProperty("total_bruto_comprobante_venta")]
        public decimal total_bruto_comprobante_venta { get; set; }

        [Display(Name = "total_igv_comprobante_venta")]
        [JsonProperty("total_igv_comprobante_venta")]
        public decimal total_igv_comprobante_venta { get; set; }

        [Display(Name = "total_isc_comprobante_venta")]
        [JsonProperty("total_isc_comprobante_venta")]
        public decimal total_isc_comprobante_venta { get; set; }

        [Display(Name = "total_neto_comprobante_venta")]
        [JsonProperty("total_neto_comprobante_venta")]
        public decimal total_neto_comprobante_venta { get; set; }

        [Display(Name = "saldo_bruto")]
        [JsonProperty("saldo_bruto")]
        public decimal saldo_bruto { get; set; }

        [Display(Name = "flg_cronograma")]
        [JsonProperty("flg_cronograma")]
        public string flg_cronograma { get; set; }

        [Display(Name = "estado_comprobante_venta")]
        [JsonProperty("estado_comprobante_venta")]
        public string estado_comprobante_venta { get; set; }

        public string generico { get; set; }


        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado_comprobante_venta == "-1")
                {
                    nestado = "TODOS";
                }
                else if (estado_comprobante_venta == "0")
                {
                    nestado = "ANULADO";
                }
                else if (estado_comprobante_venta == "1")
                {
                    nestado = "VIGENTE";
                }
                return nestado;
            }
            set
            {

            }
        }

        //Comprobante Tipo
        [Display(Name = "Id_comprobante_tipo")]
        [JsonProperty("id_comprobante_tipo")]
        public int id_comprobante_tipo { get; set; }

        [Display(Name = "Codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }

        [Display(Name = "Descripcion_comprobante_tipo")]
        [JsonProperty("descripcion_comprobante_tipo")]
        public string descripcion_comprobante_tipo { get; set; }

        [Display(Name = "Estado_comprobante_tipo")]
        [JsonProperty("estado_comprobante_tipo")]
        public string estado_comprobante_tipo { get; set; }

        //Venta
        [Display(Name = "Id_venta")]
        [JsonProperty("id_venta")]
        public int id_venta { get; set; }

        [Display(Name = "Fk_usuario_venta")]
        [JsonProperty("fk_usuario_venta")]
        public int fk_usuario_venta { get; set; }

        [Display(Name = "Fk_usuario_pago")]
        [JsonProperty("fk_usuario_pago")]
        public int fk_usuario_pago { get; set; }

        [Display(Name = "Fk_cliente")]
        [JsonProperty("fk_cliente")]
        public int fk_cliente { get; set; }

        [Display(Name = "Fk_pedido")]
        [JsonProperty("fk_pedido")]
        public int fk_pedido { get; set; }

        [Display(Name = "N_venta")]
        [JsonProperty("n_venta")]
        public string n_venta { get; set; }

        [Display(Name = "F_venta")]
        [JsonProperty("f_venta")]
        public string f_venta { get; set; }

        [Display(Name = "Estado_venta")]
        [JsonProperty("estado_venta")]
        public string estado_venta { get; set; }

        //Pedido
        [Display(Name = "Id_pedido")]
        [JsonProperty("id_pedido")]
        public int id_pedido { get; set; }

        [Display(Name = "Fk_cliente_pedido")]
        [JsonProperty("fk_cliente_pedido")]
        public int fk_cliente_pedido { get; set; }

        [Display(Name = "Fk_userventa")]
        [JsonProperty("fk_userventa")]
        public int fk_userventa { get; set; }

        [Display(Name = "N_pedido")]
        [JsonProperty("n_pedido")]
        public string n_pedido { get; set; }

        [Display(Name = "F_pedido")]
        [JsonProperty("f_pedido")]
        public string f_pedido { get; set; }

        [Display(Name = "Estado_pedido")]
        [JsonProperty("estado_pedido")]
        public string estado_pedido { get; set; }

        //Cliente
        [Display(Name = "Id_cliente")]
        [JsonProperty("id_cliente")]
        public int id_cliente { get; set; }

        [Display(Name = "Fk_cliente_tipo")]
        [JsonProperty("fk_cliente_tipo")]
        public int fk_cliente_tipo { get; set; }

        [Display(Name = "Estado_cliente")]
        [JsonProperty("estado_cliente")]
        public string estado_cliente { get; set; }

        //Cliente Tipo
        [Display(Name = "Id_cliente_tipo")]
        [JsonProperty("id_cliente_tipo")]
        public int id_cliente_tipo { get; set; }

        [Display(Name = "Descripcion_cliente_tipo")]
        [JsonProperty("descripcion_cliente_tipo")]
        public string descripcion_cliente_tipo { get; set; }

        [Display(Name = "Estado_cliente_tipo")]
        [JsonProperty("estado_cliente_tipo")]
        public string estado_cliente_tipo { get; set; }

        //Cliente Natural
        [Display(Name = "Id_cliente_natural")]
        [JsonProperty("id_cliente_natural")]
        public int id_cliente_natural { get; set; }

        [Display(Name = "Fk_persona_cliente_natural")]
        [JsonProperty("fk_persona_cliente_natural")]
        public int fk_persona_cliente_natural { get; set; }

        [Display(Name = "Fk_cliente_cliente_natural")]
        [JsonProperty("fk_cliente_cliente_natural")]
        public int fk_cliente_cliente_natural { get; set; }

        [Display(Name = "Ruc_cliente_natural")]
        [JsonProperty("ruc_cliente_natural")]
        public string ruc_cliente_natural { get; set; }

        [Display(Name = "Estado_cliente_natural")]
        [JsonProperty("estado_cliente_natural")]
        public string estado_cliente_natural { get; set; }

        //Persona Cliente NAtural
        [Display(Name = "Id_persona_cliente_natural")]
        [JsonProperty("id_persona_cliente_natural")]
        public int id_persona_cliente_natural { get; set; }

        [Display(Name = "Nombre_cliente_natural")]
        [JsonProperty("nombre_cliente_natural")]
        public string nombre_cliente_natural { get; set; }

        [Display(Name = "Apellido_paterno_cliente_natural")]
        [JsonProperty("apellido_paterno_cliente_natural")]
        public string apellido_paterno_cliente_natural { get; set; }

        [Display(Name = "Apellido_materno_cliente_natural")]
        [JsonProperty("apellido_materno_cliente_natural")]
        public string apellido_materno_cliente_natural { get; set; }

        [Display(Name = "F_nacimiento_cliente_natural")]
        [JsonProperty("f_nacimiento_cliente_natural")]
        public string f_nacimiento_cliente_natural { get; set; }

        [Display(Name = "Email_cliente_natural")]
        [JsonProperty("email_cliente_natural")]
        public string email_cliente_natural { get; set; }

        [Display(Name = "Dni_cliente_natural")]
        [JsonProperty("dni_cliente_natural")]
        public string dni_cliente_natural { get; set; }

        [Display(Name = "Estado_persona_cliente_natural")]
        [JsonProperty("estado_persona_cliente_natural")]
        public string estado_persona_cliente_natural { get; set; }

        //Cliente Juridico
        [Display(Name = "Id_cliente_juridico")]
        [JsonProperty("id_cliente_juridico")]
        public int id_cliente_juridico { get; set; }

        [Display(Name = "Fk_empresa_cliente_juridico")]
        [JsonProperty("fk_empresa_cliente_juridico")]
        public int fk_empresa_cliente_juridico { get; set; }

        [Display(Name = "Fk_cliente_cliente_juridico")]
        [JsonProperty("fk_cliente_cliente_juridico")]
        public int fk_cliente_cliente_juridico { get; set; }

        [Display(Name = "Estado_cliente_juridico")]
        [JsonProperty("estado_cliente_juridico")]
        public string estado_cliente_juridico { get; set; }

        //Empresa Cliente Juridico
        [Display(Name = "Id_empresa_cliente_juridico")]
        [JsonProperty("id_empresa_cliente_juridico")]
        public int id_empresa_cliente_juridico { get; set; }

        [Display(Name = "Ruc_empresa_cliente_juridico")]
        [JsonProperty("ruc_empresa_cliente_juridico")]
        public string ruc_empresa_cliente_juridico { get; set; }

        [Display(Name = "Razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "Direccion_empresa")]
        [JsonProperty("direccion_empresa")]
        public string direccion_empresa { get; set; }

        [Display(Name = "Estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //Usuario Venta
        [Display(Name = "Id_usuario_venta")]
        [JsonProperty("id_usuario_venta")]
        public int id_usuario_venta { get; set; }

        [Display(Name = "Fk_trabajador_usuario_venta")]
        [JsonProperty("fk_trabajador_usuario_venta")]
        public int fk_trabajador_usuario_venta { get; set; }

        //Trabajador Usuario Venta
        [Display(Name = "Id_trabajador_usuario_venta")]
        [JsonProperty("id_trabajador_usuario_venta")]
        public int id_trabajador_usuario_venta { get; set; }

        [Display(Name = "fk_persona_usuario_venta")]
        [JsonProperty("fk_persona_usuario_venta")]
        public int fk_persona_usuario_venta { get; set; }

        [Display(Name = "Fk_tipo_trabajador_usuario_venta")]
        [JsonProperty("fk_tipo_trabajador_usuario_venta")]
        public int fk_tipo_trabajador_usuario_venta { get; set; }

        [Display(Name = "Estado_trabajador_usuario_venta")]
        [JsonProperty("estado_trabajador_usuario_venta")]
        public string estado_trabajador_usuario_venta { get; set; }

        //Persona Usuario Venta
        [Display(Name = "Id_persona_usuario_venta")]
        [JsonProperty("id_persona_usuario_venta")]
        public int id_persona_usuario_venta { get; set; }

        [Display(Name = "Nombre_usuario_venta")]
        [JsonProperty("nombre_usuario_venta")]
        public string nombre_usuario_venta { get; set; }

        [Display(Name = "Apellido_paterno_usuario_venta")]
        [JsonProperty("apellido_paterno_usuario_venta")]
        public string apellido_paterno_usuario_venta { get; set; }

        [Display(Name = "Apellido_materno_usuario_venta")]
        [JsonProperty("apellido_materno_usuario_venta")]
        public string apellido_materno_usuario_venta { get; set; }

        [Display(Name = "F_nacimiento_usuario_venta")]
        [JsonProperty("f_nacimiento_usuario_venta")]
        public string f_nacimiento_usuario_venta { get; set; }

        [Display(Name = "Email_usuario_venta")]
        [JsonProperty("email_usuario_venta")]
        public string email_usuario_venta { get; set; }

        [Display(Name = "Dni_usuario_venta")]
        [JsonProperty("dni_usuario_venta")]
        public string dni_usuario_venta { get; set; }

        [Display(Name = "Estado_persona_usuario_venta")]
        [JsonProperty("estado_persona_usuario_venta")]
        public string estado_persona_usuario_venta { get; set; }

        //Usuario Pago
        [Display(Name = "Id_usuario_pago")]
        [JsonProperty("id_usuario_pago")]
        public int id_usuario_pago { get; set; }

        [Display(Name = "Fk_trabajador_usuario_pago")]
        [JsonProperty("fk_trabajador_usuario_pago")]
        public int fk_trabajador_usuario_pago { get; set; }

        //Trabajador Usuario Pago
        [Display(Name = "Id_trabajador_usuario_pago")]
        [JsonProperty("id_trabajador_usuario_pago")]
        public int id_trabajador_usuario_pago { get; set; }

        [Display(Name = "Fk_persona_usuario_pago")]
        [JsonProperty("fk_persona_usuario_pago")]
        public int fk_persona_usuario_pago { get; set; }

        [Display(Name = "Fk_tipo_trabajador_usuario_pago")]
        [JsonProperty("fk_tipo_trabajador_usuario_pago")]
        public int fk_tipo_trabajador_usuario_pago { get; set; }

        [Display(Name = "Estado_trabajador_usuario_pago")]
        [JsonProperty("estado_trabajador_usuario_pago")]
        public string estado_trabajador_usuario_pago { get; set; }

        //Persona Usuario Pago
        [Display(Name = "Id_persona_usuario_pago")]
        [JsonProperty("id_persona_usuario_pago")]
        public int id_persona_usuario_pago { get; set; }

        [Display(Name = "Nombre_usuario_pago")]
        [JsonProperty("nombre_usuario_pago")]
        public string nombre_usuario_pago { get; set; }

        [Display(Name = "Apellido_paterno_usuario_pago")]
        [JsonProperty("apellido_paterno_usuario_pago")]
        public string apellido_paterno_usuario_pago { get; set; }

        [Display(Name = "Apellido_materno_usuario_pago")]
        [JsonProperty("apellido_materno_usuario_pago")]
        public string apellido_materno_usuario_pago { get; set; }

        [Display(Name = "F_nacimiento_usuario_pago")]
        [JsonProperty("f_nacimiento_usuario_pago")]
        public string f_nacimiento_usuario_pago { get; set; }

        [Display(Name = "Email_usuario_pago")]
        [JsonProperty("email_usuario_pago")]
        public string email_usuario_pago { get; set; }

        [Display(Name = "Dni_usuario_pago")]
        [JsonProperty("dni_usuario_pago")]
        public string dni_usuario_pago { get; set; }

        [Display(Name = "Estado_persona_usuario_pago")]
        [JsonProperty("estado_persona_usuario_pago")]
        public string estado_persona_usuario_pago { get; set; }

        public string sigla { get; set; }
        public string direccion_cliente_natural { get; set; }
        public string email_cliente_juridico { get; set; }
        public string cadena_para_codigo_qr { get; set; }
        public string enlace { get; set; }




        public string medio_pago { get; set; }
        public string tarjeta { get; set; }

        public decimal efectivo_recibido { get; set; }
        public decimal vuelto { get; set; }
        public bool check_detraccion { get; set; }
        public decimal detraccion { get; set; }
        public int fk_detraccion_tipo { get; set; }
        public string codigo_detraccion { get; set; }
        public string detraccion_tipo { get; set; }
        public int porcentaje { get; set; }
        public string estado_detraccion { get; set; }
        public string fk_moneda { get; set; }
        public string descripcion_moneda { get; set; }

        [Display(Name = "F_vencimiento")]
        [JsonProperty("f_vencimiento")]
        public DateTime f_vencimiento { get; set; }
        public string referencia { get; set; }
        public string codigo_hash { get; set; }
        public string observacion_full { get; set; }
        public string observacion_a { get; set; }
        public string observacion_b { get; set; }



        public decimal total_exonerado { get; set; }
        public decimal total_inafecto { get; set; }
        public decimal total_gratuito { get; set; }

        public DateTime f_vencimiento_comprobante_venta { get; set; }
        public int fk_moneda_comprobante_venta { get; set; }
        public string email { get; set; }
        public decimal tipo_cambio { get; set; }
    }
}