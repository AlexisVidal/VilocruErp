using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Correlativo
    {
        [Display(Name = "id_correlativo")]
        [JsonProperty("id_correlativo")]
        public int id_correlativo { get; set; }

        [Display(Name = "fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "nro_correlativo")]
        [JsonProperty("nro_correlativo")]
        public string nro_correlativo { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        [Display(Name = "new_correlativo")]
        [JsonProperty("new_correlativo")]
        public string new_correlativo { get; set; }
        public string comprobante_modifica { get; set; }
    }
}