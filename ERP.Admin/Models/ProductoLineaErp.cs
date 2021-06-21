using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoLineaErp
    {
        [Display(Name = "ID")]
        [JsonProperty("id_producto_linea")]
        public int id_producto_linea { get; set; }

        [Display(Name = "Codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }

        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
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