using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class TarjetaCredito
    {
        [Display(Name = "id_tarjeta_credito")]
        [JsonProperty("id_tarjeta_credito")]
        public int id_tarjeta_credito { get; set; }

        [Display(Name = "descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}