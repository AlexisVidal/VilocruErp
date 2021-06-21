using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class CorrelativoGenerico
    {
        [Display(Name = "id_correlativo_generico")]
        [JsonProperty("id_correlativo_generico")]
        public int id_correlativo_generico { get; set; }

        [Display(Name = "nro_correlativo")]
        [JsonProperty("nro_correlativo")]
        public string nro_correlativo { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}