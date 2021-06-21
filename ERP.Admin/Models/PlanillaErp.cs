using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ERP.Admin.Models
{
    public class PlanillaErp
    {
        [Display(Name = "ID")]
        [JsonProperty("IDPLANILLA")]
        public string IDPLANILLA { get; set; }

        [Display(Name = "BONIFICACION")]
        [JsonProperty("BONIFICACION")]
        public string BONIFICACION { get; set; }

        
        public string ESTADO { get; set; }
        [Display(Name = "ESTADO")]
        [JsonProperty("DESTADO")]
        public string DESTADO
        {
            get
            {
                if (ESTADO == "1")
                {
                    return "ACTIVO";
                }
                else
                {
                    return "ANULADO";
                }
            }
            set
            {

            }
        }
        [Display(Name = "DIASTRABAJADOS")]
        [JsonProperty("DIASTRABAJADOS")]
        public string DIASTRABAJADOS { get; set; }

        [Display(Name = "DIAS TRABAJADOS")]
        [JsonProperty("DESDIASTRABAJADOS")]
        public string DESDIASTRABAJADOS { get; set; }

        [Display(Name = "DIAS_DESCANSO")]
        [JsonProperty("DIAS_DESCANSO")]
        public string DIAS_DESCANSO { get; set; }

        [Display(Name = "DIAS DESCANSO")]
        [JsonProperty("DESCDIAS_DESCANSO")]
        public string DESCDIAS_DESCANSO { get; set; }

        [Display(Name = "DIAS_SUBSIDIO")]
        [JsonProperty("DIAS_SUBSIDIO")]
        public string DIAS_SUBSIDIO { get; set; }

        [Display(Name = "DIAS SUBSIDIO")]
        [JsonProperty("DESDIAS_SUBSIDIO")]
        public string DESDIAS_SUBSIDIO { get; set; }

        [Display(Name = "DIAS_MATERNIDAD")]
        [JsonProperty("DIAS_MATERNIDAD")]
        public string DIAS_MATERNIDAD { get; set; }

        [Display(Name = "DIAS MATERNIDAD")]
        [JsonProperty("DESDIAS_MATERNIDAD")]
        public string DESDIAS_MATERNIDAD { get; set; }

        [Display(Name = "FALTAS")]
        [JsonProperty("FALTAS")]
        public string FALTAS { get; set; }

        [Display(Name = "FALTAS")]
        [JsonProperty("DESFALTAS")]
        public string DESFALTAS { get; set; }

        [Display(Name = "HORAS_FALTA")]
        [JsonProperty("HORAS_FALTA")]
        public string HORAS_FALTA { get; set; }

        [Display(Name = "HORAS FALTA")]
        [JsonProperty("DESHORAS_FALTA")]
        public string DESHORAS_FALTA { get; set; }

        [Display(Name = "HORAS_DESCANSO")]
        [JsonProperty("HORAS_DESCANSO")]
        public string HORAS_DESCANSO { get; set; }

        [Display(Name = "HORAS DESCANSO")]
        [JsonProperty("DESHORAS_DESCANSO")]
        public string DESHORAS_DESCANSO { get; set; }

        [Display(Name = "HORAS_DOBLES")]
        [JsonProperty("HORAS_DOBLES")]
        public string HORAS_DOBLES { get; set; }

        [Display(Name = "HORAS DOBLES")]
        [JsonProperty("DESHORAS_DOBLES")]
        public string DESHORAS_DOBLES { get; set; }

        [Display(Name = "HORAS_EXTRAS")]
        [JsonProperty("HORAS_EXTRAS")]
        public string HORAS_EXTRAS { get; set; }

        [Display(Name = "HORAS EXTRAS")]
        [JsonProperty("DESHORAS_EXTRAS")]
        public string DESHORAS_EXTRAS { get; set; }

        [Display(Name = "HORAS_EXTRAS2")]
        [JsonProperty("HORAS_EXTRAS2")]
        public string HORAS_EXTRAS2 { get; set; }

        [Display(Name = "HORAS EXTRAS")]
        [JsonProperty("DESHORAS_EXTRAS2")]
        public string DESHORAS_EXTRAS2 { get; set; }

        [Display(Name = "HORAS_EXTDOBLES")]
        [JsonProperty("HORAS_EXTDOBLES")]
        public string HORAS_EXTDOBLES { get; set; }

        [Display(Name = "HORAS EXT DOBLES")]
        [JsonProperty("DESHORAS_EXTDOBLES")]
        public string DESHORAS_EXTDOBLES { get; set; }

        [Display(Name = "HORAS_FERIADO")]
        [JsonProperty("HORAS_FERIADO")]
        public string HORAS_FERIADO { get; set; }

        [Display(Name = "HORAS FERIADO")]
        [JsonProperty("DESHORAS_FERIADO")]
        public string DESHORAS_FERIADO { get; set; }

        [Display(Name = "HORAS_NOCTURNAS")]
        [JsonProperty("HORAS_NOCTURNAS")]
        public string HORAS_NOCTURNAS { get; set; }

        [Display(Name = "HORAS NOCTURNAS")]
        [JsonProperty("DESHORAS_NOCTURNAS")]
        public string DESHORAS_NOCTURNAS { get; set; }

        [Display(Name = "HORAS_EXTNOCTU")]
        [JsonProperty("HORAS_EXTNOCTU")]
        public string HORAS_EXTNOCTU { get; set; }

        [Display(Name = "HORAS EXT NOCTU")]
        [JsonProperty("DESHORAS_EXTNOCTU")]
        public string DESHORAS_EXTNOCTU { get; set; }

        [Display(Name = "HORAS_EXTNOCTU2")]
        [JsonProperty("HORAS_EXTNOCTU2")]
        public string HORAS_EXTNOCTU2 { get; set; }

        [Display(Name = "HORAS DOB NOCTU 2")]
        [JsonProperty("DESHORAS_EXTNOCTU2")]
        public string DESHORAS_EXTNOCTU2 { get; set; }

        [Display(Name = "HORAS_DOBNOCTU")]
        [JsonProperty("HORAS_DOBNOCTU")]
        public string HORAS_DOBNOCTU { get; set; }

        [Display(Name = "HORAS DOB NOCTU")]
        [JsonProperty("DESHORAS_DOBNOCTU")]
        public string DESHORAS_DOBNOCTU { get; set; }

        [Display(Name = "HORAS_NORMALES")]
        [JsonProperty("HORAS_NORMALES")]
        public string HORAS_NORMALES { get; set; }

        [Display(Name = "HORAS NORMALES")]
        [JsonProperty("DESHORAS_NORMALES")]
        public string DESHORAS_NORMALES { get; set; }

        [Display(Name = "HORAS DIA")]
        [JsonProperty("HORAS_DIA")]
        public int HORAS_DIA { get; set; }

        [Display(Name = "HORAS_PERIODO")]
        [JsonProperty("HORAS_PERIODO")]
        public string HORAS_PERIODO { get; set; }

        [Display(Name = "HORAS PERIODO")]
        [JsonProperty("DESHORAS_PERIODO")]
        public string DESHORAS_PERIODO { get; set; }

        [Display(Name = "NRO_CORRELATIVO")]
        [JsonProperty("NRO_CORRELATIVO")]
        public string NRO_CORRELATIVO { get; set; }

        [Display(Name = "NRO SUELDOS")]
        [JsonProperty("NRO_SUELDOS")]
        public string NRO_SUELDOS { get; set; }

        [Display(Name = "DESCRIPCION")]
        [JsonProperty("DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "PERIODICIDAD")]
        [JsonProperty("IDPERIODICIDAD")]
        public string IDPERIODICIDAD { get; set; }
        public string DIDPERIODICIDAD
        {
            get
            {
                if (IDPERIODICIDAD == "M")
                {
                    return "MENSUAL";
                }
                else
                {
                    return "QUINCENAL";
                }
            }
            set
            {

            }
        }

        public string TIPO_ENVIO { get; set; }

        [Display(Name = "TIPO_ENVIO")]
        [JsonProperty("DTIPO_ENVIO")]
        public string DTIPO_ENVIO
        {
            get
            {
                if (TIPO_ENVIO == "M")
                {
                    return "MENSUAL";
                }
                else
                {
                    return "QUINCENAL";
                }
            }
            set
            {

            }
        }
        [Display(Name = "FECHA CREACION")]
        [JsonProperty("FECHACREACION")]
        public DateTime FECHACREACION { get; set; }

        [Display(Name = "VAR_COSTOC")]
        [JsonProperty("VAR_COSTOC")]
        public string VAR_COSTOC { get; set; }

        [Display(Name = "DESVAR_COSTOC")]
        [JsonProperty("DESVAR_COSTOC")]
        public string DESVAR_COSTOC { get; set; }

        [Display(Name = "VBASICO")]
        [JsonProperty("VBASICO")]
        public string VBASICO { get; set; }

        [Display(Name = "V. BASICO")]
        [JsonProperty("DESVBASICO")]
        public string DESVBASICO { get; set; }

        [Display(Name = "IDMONEDA")]
        [JsonProperty("IDMONEDA")]
        public string IDMONEDA { get; set; }

        [Display(Name = "MONEDA")]
        [JsonProperty("MONEDA")]
        public string MONEDA { get; set; }

        [Display(Name = "BASICO_SUELDOMINIMO")]
        [JsonProperty("BASICO_SUELDOMINIMO")]
        public string BASICO_SUELDOMINIMO { get; set; }

        [Display(Name = "SUELDO BASICO")]
        [JsonProperty("DESBASICO_SUELDOMINIMO")]
        public string DESBASICO_SUELDOMINIMO { get; set; }

        [Display(Name = "MESES CALCULO RENTA")]
        [JsonProperty("MESES_CALCULO_RENTA")]
        public string MESES_CALCULO_RENTA { get; set; }
    }
}