using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class LetrasPorPagarErp
    {
        public int id_letras_por_pagar_proveedor { get; set; }
        public int fk_cuentas_por_pagar_proveedor { get; set; }
        public int fk_comprobante_tipo { get; set; }
        public string comprobante_tipo { get; set; }
        public int fk_proveedor { get; set; }
        public int dias { get; set; }
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string IDMONEDA { get; set; }
        public string moneda { get; set; }
        public string IDUSUARIO { get; set; }
        public string documento_referencia { get; set; }
        public DateTime f_emision { get; set; }
        public decimal monto { get; set; }
        public int nro_letras { get; set; }
        public decimal saldo { get; set; }
        public DateTime f_registro { get; set; }
        public string estado { get; set; }
        public string observacion { get; set; }
        public DateTime f_vencimiento { get; set; }
        public string df_vencimiento
        {
            get
            {
                if (f_vencimiento.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return f_vencimiento.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }

        //DETALLE
        public int id_letras_por_pagar_proveedor_detalle { get; set; }
        public int fk_letras_por_pagar_proveedor { get; set; }
        public int nro_letra { get; set; }
        public DateTime fecha { get; set; }
        public string dfecha
        {
            get
            {
                if (fecha.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return fecha.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public decimal monto_detalle { get; set; }
        public string estado_detalle { get; set; }
        public string NOMBRE_CORTO { get; set; }
        public string letra_numero { get; set; }

        public DateTime fecha_pago { get; set; }
        public string dfecha_pago
        {
            get
            {
                if (fecha_pago.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return fecha_pago.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }






        //DETALLE


        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "1")
                {
                    nestado = "ACTIVO";
                }
                else
                {
                    nestado = "INACTIVO";
                }
                return nestado;
            }
            set
            {
            }
        }


        public string NColor
        {
            get
            {
                string ncolor = "";
                if (estado_detalle == "1")
                {
                    ncolor = "#eeef37";
                }
                else if (estado_detalle == "2")
                {
                    ncolor = "#5ddc42";
                }
                else if (estado_detalle == "3")
                {
                    ncolor = "#0067a5";
                }
                else if (estado_detalle == "4")
                {
                    ncolor = "#ff0000";
                }
                else
                {
                    ncolor = "#5A6173";
                }
                return ncolor;
            }
            set
            {
            }
        }
        [Display(Name = "TCOLOR")]
        [JsonProperty("TCOLOR")]
        public string TCOLOR
        {
            get
            {
                string tcolor = "";
                if (estado == "1")
                {
                    tcolor = "black";
                }
                else if (estado == "2")
                {
                    tcolor = "white";
                }
                else if (estado == "3")
                {
                    tcolor = "white";
                }
                else
                {
                    tcolor = "white";
                }
                return tcolor;
            }
            set
            {
            }
        }
    }
}