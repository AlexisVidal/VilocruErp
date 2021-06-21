using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ObligacionPagarErp
    {
        public int id_obligacion_pagar { get; set; }
        public int fk_obligacion_tipo { get; set; }
        public string obligacion_tipo { get; set; }
        public int fk_comprobante_tipo { get; set; }
        public string codigo { get; set; }
        public string comprobante_tipo { get; set; }
        public string sigla { get; set; }
        public int fk_proveedor { get; set; }
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string direccion { get; set; }
        public string IDMONEDA { get; set; }
        public string NOMBRE_CORTO { get; set; }
        public string moneda { get; set; }
        public string IDUSUARIO { get; set; }
        public string documento_referencia { get; set; }
        public DateTime fecha_emision { get; set; }
        public string df_emision
        {
            get
            {
                if (fecha_emision.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return fecha_emision.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public decimal monto { get; set; }
        public int nro_letras { get; set; }
        public decimal saldo { get; set; }
        public decimal interes { get; set; }
        public decimal cuota_inicial { get; set; }
        public DateTime fecha_registro { get; set; }
        public string df_registro
        {
            get
            {
                if (fecha_registro.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return fecha_registro.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public string estado { get; set; }
        public string observacion { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public string df_vencimiento
        {
            get
            {
                if (fecha_vencimiento.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return fecha_vencimiento.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public int dias { get; set; }


        //detalles
        public int id_obligacion_pagar_detalle { get; set; }
        public int fk_obligacion_pagar { get; set; }
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

        public string mes_vence { get; set; }
        public string num_cuota { get; set; }
        public decimal monto_soles { get; set; }
        public decimal monto_dolares { get; set; }

        public int aniobusca { get; set; }
        public int mesbusca { get; set; }
        public int num_week { get; set; }
        public string detalle { get; set; }

        public string NombreEmpresa { get; set; }
        public string RucEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public decimal monto_cuota { get; set; }

    }
}