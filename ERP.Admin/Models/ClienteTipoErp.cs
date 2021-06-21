using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ClienteTipoErp
    {
        [Display(Name = "Id_cliente_tipo")]
        [JsonProperty("id_cliente_tipo")]
        public int id_cliente_tipo { get; set; }

        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

    }
}