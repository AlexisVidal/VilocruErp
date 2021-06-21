using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class ClienteNatural
    {
        [Display(Name = "id_cliente_natural")]
        [JsonProperty("id_cliente_natural")]
        public int id_cliente_natural { get; set; }

        [Display(Name = "fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "fk_cliente")]
        [JsonProperty("fk_cliente")]
        public int fk_cliente { get; set; }

        [Display(Name = "ruc")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}