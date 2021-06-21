using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PrecioCompra
    {
        [Display(Name = "ID")]
        [JsonProperty("id_precio_compra")]
        public int id_precio_compra { get; set; }

        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "P. Compra")]
        [JsonProperty("preciocompra")]
        public decimal preciocompra { get; set; }

        [Display(Name = "Dscto 1")]
        [JsonProperty("dsctounop")]
        public decimal dsctounop { get; set; }

        [Display(Name = "Dscto 2")]
        [JsonProperty("dsctodosp")]
        public decimal dsctodosp { get; set; }

        [Display(Name = "Dscto 3")]
        [JsonProperty("dsctotresp")]
        public decimal dsctotresp { get; set; }

        [Display(Name = "Dscto 4")]
        [JsonProperty("dsctocuatrop")]
        public decimal dsctocuatrop { get; set; }

        [Display(Name = "P. Final")]
        [JsonProperty("preciocomprafinal")]
        public decimal preciocomprafinal { get; set; }

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