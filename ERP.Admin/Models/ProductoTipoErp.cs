using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoTipoErp
    {
        [Display(Name = "ID")]
        [JsonProperty("id_producto_tipo")]
        public int id_producto_tipo { get; set; }

        [Display(Name = "ID LINEA")]
        [JsonProperty("fk_producto_linea")]
        public int fk_producto_linea { get; set; }

        [Display(Name = "COD.")]
        [JsonProperty("codigo_tipo")]
        public string codigo_tipo { get; set; }

        [Display(Name = "TIPO")]
        [JsonProperty("producto_tipo")]
        public string producto_tipo { get; set; }

        [Display(Name = "ABREV.")]
        [JsonProperty("abreviatura_tipo")]
        public string abreviatura_tipo { get; set; }

        public string estado_tipo { get; set; }

        [Display(Name = "Estado")]
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado_tipo == "1")
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

        [Display(Name = "COD. LINEA")]
        [JsonProperty("codigo_linea")]
        public string codigo_linea { get; set; }

        [Display(Name = "LINEA")]
        [JsonProperty("linea")]
        public string linea { get; set; }
    }
}