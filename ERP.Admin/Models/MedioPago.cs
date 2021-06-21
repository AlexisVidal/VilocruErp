using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class MedioPago
    {
        [Display(Name = "id_medio_pago")]
        [JsonProperty("id_medio_pago")]
        public int id_medio_pago { get; set; }

        [Display(Name = "descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}