using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoPrecioVenta
    {
        [Display(Name = "ID")]
        [JsonProperty("id_tipo_precio_venta")]
        public int id_tipo_precio_venta { get; set; }
        [Display(Name = "Nombre")]
        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [Display(Name = "Porcentaje")]
        [JsonProperty("porcentaje")]
        public decimal porcentaje { get; set; }
        public string estado { get; set; }
        [Display(Name = "Estado")]
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