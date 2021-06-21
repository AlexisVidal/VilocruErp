using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalPrestamoDetalle
    {
        public int id_personal_prestamo { get; set; }
        public int fk_tipo_check { get; set; }
        public int id_personal_prestamo_detalle { get; set; }
        public int nrocuota { get; set; }
        public DateTime FECHADESCUENTO { get; set; }
        public string df_descuento
        {
            get
            {
                if (FECHADESCUENTO.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return FECHADESCUENTO.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public decimal montodescuento { get; set; }
        public string estado { get; set; }
        public string estado_detalle { get; set; }
        public string NDEstado
        {
            get
            {
                string nestado = "";
                switch (estado_detalle)
                {
                    case "1":
                        nestado = "ACTIVO";
                        break;
                    case "2":
                        nestado = "CANCELADO";
                        break;
                    case "0":
                        nestado = "ANULADO";
                        break;
                }
                return nestado;
            }
            set
            {
            }
        }

        public string IDCODIGOGENERAL { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public string df_INICIO
        {
            get
            {
                if (FECHAINICIO.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return FECHAINICIO.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public DateTime FECHAFIN { get; set; }
        public string df_FIN
        {
            get
            {
                if (FECHAFIN.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return FECHAFIN.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public string ESTADO { get; set; }
        public decimal MONTOTOTAL { get; set; }

        public string A_MATERNO { get; set; }
        public string A_PATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string CODIGO_CONTROL { get; set; }
        public int CUOTAS { get; set; }

        public decimal porcancelar { get; set; }
        public decimal porcancelartotal { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                switch (ESTADO)
                {
                    case "1":
                        nestado = "ACTIVO";
                        break;
                    case "2":
                        nestado = "CANCELADO";
                        break;
                    case "0":
                        nestado = "ANULADO";
                        break;
                }
                return nestado;
            }
            set
            {
            }
        }

        public string NombresFull
        {
            get
            {
                string nombrefull = A_PATERNO + " " + A_MATERNO + " " + NOMBRES;
                return nombrefull;
            }
            set
            {
            }
        }
        public string PERIODO { get; set; }

    }
}