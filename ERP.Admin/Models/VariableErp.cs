using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class VariableErp
    {
        public string IDVARIABLE { get; set; }
        public string IDVARIABLEOLD { get; set; }

        public string ES_AFP { get; set; }
        public string DES_AFP
        {
            get
            {
                if (ES_AFP == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public string ESTADO { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string DESCRIPCION { get; set; }
        public string DESCR_CORTA { get; set; }
        public string IDTIPOVARIABLE { get; set; }
        public string CONVERTIR { get; set; }
        public string ES_ESSALUD { get; set; }
        public string DES_ESSALUD {
            get
            {
                if (ES_ESSALUD == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public string ES_SENATI { get; set; }
        public string DES_SENATI {
            get
            {
                if (ES_SENATI == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public string ES_AFAMILIAR { get; set; }
        public string DES_AFAMILIAR {
            get
            {
                if (ES_AFAMILIAR == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public string CODIGO_SBS { get; set; }
        public string es_variable { get; set; }
        public string ITEM { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FINAL { get; set; }
        public string ESTADO_DETALLE { get; set; }
        public decimal VALOR { get; set; }
        public string IDPLANILLA { get; set; }
        public string IDPLANILLAOLD { get; set; }
        public string DESC_PLANILLA { get; set; }
        public string DESC_TIPOVARIABLE { get; set; }
    }
}