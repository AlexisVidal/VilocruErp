using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Proveedor
    {
        [Display(Name = "ID")]
        [JsonProperty("id_proveedor")]
        public int id_proveedor { get; set; }

        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }
        [Display(Name = "Codigo")]
        [JsonProperty("cod_proveedor")]
        public string cod_proveedor { get; set; }
        [Display(Name = "Contacto")]
        [JsonProperty("contacto")]
        public string contacto { get; set; }
        [Display(Name = "Telefono")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }
        [Display(Name = "Cuenta")]
        [JsonProperty("nro_cuenta")]
        public string nro_cuenta { get; set; }
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
        public int id_empresa { get; set; }
        [Display(Name = "RUC")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }
        [Display(Name = "Razon Social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }
        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }
        public string estado_empresa { get; set; }
    }
}