using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ERP.Admin.Models
{
    public class EmpresaCuentaErp
    {
        [Display(Name = "id_empresa_cuenta")]
        [JsonProperty("id_empresa_cuenta")]
        public int id_empresa_cuenta { get; set; }

        [Display(Name = "fk_empresa")]
        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }

        [Display(Name = "RUC")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }

        [Display(Name = "PROVEEDOR")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "fk_banco")]
        [JsonProperty("fk_banco")]
        public int fk_banco { get; set; }

        [Display(Name = "BANCO")]
        [JsonProperty("banco")]
        public string banco { get; set; }

        [Display(Name = "IDMONEDA")]
        [JsonProperty("IDMONEDA")]
        public string IDMONEDA { get; set; }

        [Display(Name = "MONEDA")]
        [JsonProperty("moneda")]
        public string moneda { get; set; }

        [Display(Name = "CUENTA")]
        [JsonProperty("nro_cuenta")]
        public string nro_cuenta { get; set; }

        [Display(Name = "CUENTA INTERBANCARIA")]
        [JsonProperty("nro_cuenta_interbancario")]
        public string nro_cuenta_interbancario { get; set; }


        public string estado { get; set; }

        [Display(Name = "ESTADO")]
        [JsonProperty("Nestado")]
        public string NEstado
        {
            get
            {
                string nestado = "";
                nestado = estado == "1" ? "ACTIVO" : "INACTIVO";
                return nestado;
            }
            set
            {
            }
        }


    }
}