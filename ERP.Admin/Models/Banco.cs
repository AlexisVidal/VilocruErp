using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Banco
    {
        [Display(Name = "id_banco")]
        [JsonProperty("id_banco")]
        public int id_banco { get; set; }

        [Display(Name = "descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}