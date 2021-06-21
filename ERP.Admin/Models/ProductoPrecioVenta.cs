using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoPrecioVenta
    {
        [Display(Name = "id_producto_precio_venta")]
        [JsonProperty("id_producto_precio_venta")]
        public int id_producto_precio_venta { get; set; }

        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [JsonProperty("fk_precio_compra")]
        public int fk_precio_compra { get; set; }
        [Display(Name = "P. V. U. %")]
        [JsonProperty("precio_unidadp")]
        public decimal precio_unidadp { get; set; }
        [Display(Name = "P. V. Unidad")]
        [JsonProperty("precio_unidad")]
        public decimal precio_unidad { get; set; }
        [Display(Name = "P. V. Mayor %")]
        [JsonProperty("precio_mayorp")]
        public decimal precio_mayorp { get; set; }

        [Display(Name = "P. V. Mayor")]
        [JsonProperty("precio_mayor")]
        public decimal precio_mayor { get; set; }
        [Display(Name = "P. V. Especial %")]
        [JsonProperty("precio_especialp")]
        public decimal precio_especialp { get; set; }
        [Display(Name = "P. V. Especial")]
        [JsonProperty("precio_especial")]
        public decimal precio_especial { get; set; }

        [Display(Name = "Tipo Cambio")]
        [JsonProperty("tipo_cambio")]
        public decimal tipo_cambio { get; set; }

        [JsonProperty("fk_tipo_moneda")]
        public int fk_tipo_moneda { get; set; }


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