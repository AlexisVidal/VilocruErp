using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class VentaPedido
    {
        [Display(Name = "id_venta_pedido")]
        [JsonProperty("id_venta_pedido")]
        public int id_venta_pedido { get; set; }

        [Display(Name = "fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        [Display(Name = "fk_pedido")]
        [JsonProperty("fk_pedido")]
        public int fk_pedido { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}