using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class SedeLaboralErp
    {
        [Display(Name = "ID")]
        [JsonProperty("id_sede_laboral")]
        public int id_sede_laboral { get; set; }

        [Display(Name = "ABREVIATURA")]
        [JsonProperty("abreviatura")]
        public string abreviatura { get; set; }

        [Display(Name = "SEDE")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "DIRECCION")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }
        public string estado { get; set; }

        [Display(Name = "ESTADO")]
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