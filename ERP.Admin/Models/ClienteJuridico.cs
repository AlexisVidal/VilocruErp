using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class ClienteJuridico
    {
        [Display(Name = "id_cliente_juridico")]
        [JsonProperty("id_cliente_juridico")]
        public int id_cliente_juridico { get; set; }

        [Display(Name = "fk_empresa")]
        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }

        [Display(Name = "fk_cliente")]
        [JsonProperty("fk_cliente")]
        public int fk_cliente { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}