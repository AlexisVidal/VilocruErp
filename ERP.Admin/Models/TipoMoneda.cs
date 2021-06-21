using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoMoneda
    {
        [Display(Name = "ID")]
        [JsonProperty("id_tipo_moneda")]
        public int id_tipo_moneda { get; set; }

        [Display(Name = "Tipo Moneda")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        [JsonProperty("codigo_alfabetico")]
        public string codigo_alfabetico { get; set; }
        [JsonProperty("codigo_numerico")]
        public string codigo_numerico { get; set; }
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}