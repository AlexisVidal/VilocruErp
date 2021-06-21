using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class CuentaPorPagarErp
    {
        public int id_cuentas_por_pagar { get; set; }
        public int fk_entidad { get; set; }
        public string IDUSUARIO { get; set; }
        public string IDMONEDA { get; set; }
        public int fk_motivo_prestamo { get; set; }
        public DateTime f_operacion { get; set; }
        public DateTime f_vencimiento { get; set; }
        public decimal monto { get; set; }
        public decimal saldo { get; set; }
        public DateTime f_registro { get; set; }
        public string estado { get; set; }
        public string nro_operacion { get; set; }
        public string NOMBRE_CORTO { get; set; }
        public string moneda { get; set; }
        public string motivo_prestamo { get; set; }
        public string tipo { get; set; }
        public string razon_social { get; set; }
        public string observacion { get; set; }
        public int nro_cuotas { get; set; }
        public int dias { get; set; }
        public decimal monto_cuota { get; set; }
        public decimal interes { get; set; }

        //detalles
        public int id_cuentas_por_pagar_bancos_detalle { get; set; }
        public int fk_cuentas_por_pagar { get; set; }
        public int nro_cuota { get; set; }
        public string cuota_numero { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto_detalle { get; set; }
        public string estado_detalle { get; set; }
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