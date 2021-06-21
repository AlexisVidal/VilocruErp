using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class PagoCuentasPorPagar
    {
        [Display(Name = "id_pago_cuentas_por_pagar")]
        [JsonProperty("id_pago_cuentas_por_pagar")]
        public int id_pago_cuentas_por_pagar { get; set; }

        [Display(Name = "fk_cuentas_por_pagar")]
        [JsonProperty("fk_cuentas_por_pagar")]
        public int fk_cuentas_por_pagar { get; set; }

        [Display(Name = "fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "fk_tarjeta_credito")]
        [JsonProperty("fk_tarjeta_credito")]
        public int fk_tarjeta_credito { get; set; }

        [Display(Name = "fk_nota_credito")]
        [JsonProperty("fk_nota_credito")]
        public int fk_nota_credito { get; set; }

        [Display(Name = "fk_banco")]
        [JsonProperty("fk_banco")]
        public int fk_banco { get; set; }

        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "f_pago")]
        [JsonProperty("f_pago")]
        public DateTime f_pago { get; set; }

        [Display(Name = "nro_documento")]
        [JsonProperty("nro_documento")]
        public string nro_documento { get; set; }

        [Display(Name = "monto")]
        [JsonProperty("monto")]
        public decimal monto { get; set; }

        [Display(Name = "f_registro")]
        [JsonProperty("f_registro")]
        public DateTime f_registro { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}