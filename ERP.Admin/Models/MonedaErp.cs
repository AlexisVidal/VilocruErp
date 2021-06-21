using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ERP.Admin.Models
{
    public class MonedaErp
    {
        [Display(Name = "ID MONEDA")]
        [JsonProperty("IDMONEDA")]
        public string IDMONEDA { get; set; }

        [Display(Name = "NOMBRE CORTO")]
        [JsonProperty("NOMBRE_CORTO")]
        public string NOMBRE_CORTO { get; set; }

        [Display(Name = "NOMBRE")]
        [JsonProperty("DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        
        [JsonProperty("TIPO_MONEDA")]
        public string TIPO_MONEDA { get; set; }

        [Display(Name = "FECHACREACION")]
        [JsonProperty("FECHACREACION")]
        public DateTime FECHACREACION { get; set; }

        [Display(Name = "ID SUNAT")]
        [JsonProperty("IDREGISTRO_SUNAT")]
        public string IDREGISTRO_SUNAT { get; set; }


        public string ESTADO { get; set; }

        [Display(Name = "TIPO")]
        [JsonProperty("Ntipo_moneda")]
        public string Ntipo_moneda
        {
            get
            {
                string ntipomoneda = "";
                switch (TIPO_MONEDA)
                {
                    case "N":
                        ntipomoneda = "NACIONAL";
                        break;
                    case "E":
                        ntipomoneda = "EXTRANJERA";
                        break;
                    default:
                        ntipomoneda = "OTRA";
                        break;
                }
                return ntipomoneda;
            }
            set
            {
            }
        }

        [Display(Name = "ESTADO")]
        [JsonProperty("Nestado")]
        public string NEstado
        {
            get
            {
                string nestado = "";
                nestado = ESTADO == "1" ? "ACTIVO" : "INACTIVO";
                return nestado;
            }
            set
            {
            }
        }
    }
}