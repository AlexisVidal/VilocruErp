using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class BancoErp
    {
        [Display(Name = "ID BANCO")]
        [JsonProperty("id_banco")]
        public int id_banco { get; set; }
        [Display(Name = "ID")]
        [JsonProperty("idbanco")]
        public string idbanco { get; set; }

        [Display(Name = "BANCO")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        
        public string estado { get; set; }

        [Display(Name = "COD. SUNAT")]
        [JsonProperty("codigo_sunat")]
        public string codigo_sunat { get; set; }

        

        [Display(Name = "DIRECCION")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }

        [Display(Name = "TELEFONO")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }

        

        [Display(Name = "ESTADO")]
        [JsonProperty("Nestado")]
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "1")
                {
                    nestado = "ACTIVO";
                }
                else
                {
                    nestado = "INACTIVO";
                }
                return nestado;
            }
            set
            {
            }
        }
    }
}