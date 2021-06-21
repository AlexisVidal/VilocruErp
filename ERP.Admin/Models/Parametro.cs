using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ERP.Admin.Models
{
    public class Parametro
    {
        public int annio { get; set; }
        public string semana { get; set; }
        public string pais { get; set; }
        public string destino { get; set; }

        [Display(Name = "id_parametro")]
        [JsonProperty("id_parametro")]
        public int id_parametro { get; set; }

        [Display(Name = "nombre")]
        [JsonProperty("nombre")]
        public string nombre { get; set; }

        [Display(Name = "descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "valor_char")]
        [JsonProperty("valor_char")]
        public string valor_char { get; set; }

        [Display(Name = "valor_string")]
        [JsonProperty("valor_string")]
        public string valor_string { get; set; }

        [Display(Name = "valor_int")]
        [JsonProperty("valor_int")]
        public int valor_int { get; set; }

        [Display(Name = "valor_decimal")]
        [JsonProperty("valor_decimal")]
        public decimal valor_decimal { get; set; }

        [Display(Name = "valor_datetime")]
        [JsonProperty("valor_datetime")]
        public string valor_datetime { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}