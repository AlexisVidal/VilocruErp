using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class AfpErp
    {
        [Display(Name = "ID")]
        [JsonProperty("IDAFP")]
        public string IDAFP { get; set; }

        [Display(Name = "DESCRIPCION")]
        [JsonProperty("DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "IDREGIMEN")]
        [JsonProperty("IDREGIMEN")]
        public string IDREGIMEN { get; set; }

        [Display(Name = "ESTADO")]
        [JsonProperty("ESTADO")]
        public string ESTADO { get; set; }

        [Display(Name = "SINCRONIZA")]
        [JsonProperty("SINCRONIZA")]
        public string SINCRONIZA { get; set; }

        [Display(Name = "FECHACREACION")]
        [JsonProperty("FECHACREACION")]
        public DateTime FECHACREACION { get; set; }

        [Display(Name = "CODIGO_SAP")]
        [JsonProperty("CODIGO_SAP")]
        public string CODIGO_SAP { get; set; }

        [Display(Name = "REGIMEN")]
        [JsonProperty("regimen_pensionario")]
        public string regimen_pensionario { get; set; }

        [Display(Name = "SEGURO")]
        [JsonProperty("SEGURO")]
        public decimal SEGURO { get; set; }
        [Display(Name = "COMISION_FLUJO")]
        [JsonProperty("COMISION_FLUJO")]
        public decimal COMISION_FLUJO { get; set; }
        [Display(Name = "COMISION_MIXTA")]
        [JsonProperty("COMISION_MIXTA")]
        public decimal COMISION_MIXTA { get; set; }
        [Display(Name = "FONDO")]
        [JsonProperty("FONDO")]
        public decimal FONDO { get; set; }
        [Display(Name = "TOTAL")]
        [JsonProperty("TOTAL")]
        public decimal TOTAL { get; set; }
        [Display(Name = "TIPO")]
        [JsonProperty("TIPO")]
        public string TIPO { get; set; }

    }
}