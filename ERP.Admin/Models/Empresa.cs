using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Empresa
    {
        [Display(Name = "ID")]
        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }
        [Display(Name = "Ruc / DNI")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }
        [Display(Name = "Razon Social / Nombres")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }
        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }
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
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }
        [Display(Name = "Contacto")]
        [JsonProperty("contacto")]
        public string contacto { get; set; }
        [Display(Name = "Telefono")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }

        [Display(Name = "Tipo")]
        [JsonProperty("tipo")]
        public string tipo { get; set; }

        public int fk_cliente_tipo { get; set; }
    }
}