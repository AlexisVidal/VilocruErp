using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class CompraDiversasDetalle
    {
        [Display(Name = "id_compra_diversas_detalle")]
        [JsonProperty("id_compra_diversas_detalle")]
        public int id_compra_diversas_detalle { get; set; }

        [Display(Name = "fk_comprobante_compra")]
        [JsonProperty("fk_comprobante_compra")]
        public int fk_comprobante_compra { get; set; }

        [Display(Name = "descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "cantidad")]
        [JsonProperty("cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "precio")]
        [JsonProperty("precio")]
        public decimal precio { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}