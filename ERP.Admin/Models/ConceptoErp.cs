using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ConceptoErp
    {
        public string IDCONCEPTO { get; set; }
        public string ESTADO { get; set; }
        public string DESCRIPCION { get; set; }
        public string DESCR_CORTA { get; set; }
        public string CODIGO_RTPS { get; set; }
        public string GENERAR_CTACTE { get; set; }
        public string ES_REDONDEO { get; set; }
        public string FORMULA_GLOBAL { get; set; }
        public string FORMULA { get; set; }
        public string FORMULA_ENCODIGO { get; set; }
        public string ES_PRESTAMO { get; set; }
        public string ASIGNAR_CONCEPTO { get; set; }
        public string TIPO_REMUNERACION { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string IDTIPOCONCEPTO { get; set; }
        public string DISTRIBUIR_QUINCENA { get; set; }
        public string CONCEPTO_PROVISION { get; set; }
        public string CONCEPTO_PROVISION_PROMEDIO { get; set; }
        public string CONCEPTO_AFP { get; set; }
        public int ORDEN { get; set; }
        public string CONCEPTO_DISTRI_UTILIDAD { get; set; }
        public string IDDOCUMENTO { get; set; }
        public string CONCEPTO_NOPRORRATEA_RENTA { get; set; }
        public string NOMOSTRAR_ENBOLETA { get; set; }
        public string CONCEPTO_HEXTRA { get; set; }
        public string ES_INTERESES { get; set; }
        public string CONCEPTO_RENTA_PROMEDIO { get; set; }
        public string TOMA_VALOR { get; set; }
        public string ES_AFAMILIAR { get; set; }
        public string ES_DIAS_COMPUTABLE { get; set; }
        public string ES_DIAS_VACACIONES { get; set; }
        public string AFECTA_CTS_R { get; set; }
        public string AFECTA_GRATI_R { get; set; }
        public string AFECTA_VACA_R { get; set; }
        public string AFECTA_CTS_P { get; set; }
        public string AFECTA_GRATI_P { get; set; }
        public string AFECTA_VACA_P { get; set; }
        public string IDCONCEPTO_PROVISION { get; set; }
        public string IDCONCEPTO_RENTA { get; set; }
        public string ES_ADELANTO { get; set; }
        public string ES_DIAS { get; set; }
        public string ES_UTILIDADES { get; set; }
        public string ES_GRATIEXTRAORDINARIA { get; set; }
        public string ES_FALTA_TARDANZA { get; set; }
        public string ES_COMISION { get; set; }
        public string ES_JUDICIAL { get; set; }
        public string ES_ONP_EXTORNO { get; set; }
        public string ES_INGAFECTO_EXTORNO { get; set; }
        public string ES_DESCANSOMEDICO { get; set; }
        public string DESAGREGAR_CONCEPTO { get; set; }
        public string AGRUPAR { get; set; }
        public string ES_REINTEGRORENTA { get; set; }
        public string IDCONCEPTO_IN { get; set; }
        public string CONCEPTO_PROMEDIO_DIARIO { get; set; }
        public string TIPO_APORTE_AFP { get; set; }
        public string ES_ADEL_UTILIDADES { get; set; }
        public string ES_CARGO_CONSTCIVIL { get; set; }
        public string ES_REINTEGRO { get; set; }
        public string base_sub { get; set; }
        public string ES_BONOPRODUCCION { get; set; }
        public string DES_BONOPRODUCCION
        {
            get
            {
                if (ES_BONOPRODUCCION == "N")
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
        public string TIPOCONCEPTO { get; set; }
        public string IDPLANILLA { get; set; }
        public string FORMULA_ENCODIGO_DETALLE { get; set; }
        public string FORMULA_DETALLE { get; set; }

    }
}