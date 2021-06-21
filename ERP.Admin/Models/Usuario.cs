using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Usuario
    {
        [Display(Name = "ID")]
        [JsonProperty("id_usuario")]
        public int id_usuario { get; set; }
        [Display(Name = "ID Trabajador")]
        [JsonProperty("fk_trabajador")]
        public int fk_trabajador { get; set; }

        [JsonProperty("clave")]
        public string clave { get; set; }

        [JsonProperty("clave_modificaciones")]
        public string clave_modificaciones { get; set; }
        public string estado { get; set; }

        public int id_trabajador { get; set; }
        [Display(Name = "ID Persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }
        [Display(Name = "ID Tipo")]
        [JsonProperty("fk_tipo_trabajador")]
        public int fk_tipo_trabajador { get; set; }
        [Display(Name = "Codigo")]
        [JsonProperty("cod_trabajador")]
        public string cod_trabajador { get; set; }
        public string estado_trabajador { get; set; }

        public int id_persona { get; set; }
        [Display(Name = "Nombres")]
        [JsonProperty("nombres")]
        public string nombres { get; set; }
        [Display(Name = "A. Paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }
        [Display(Name = "A. Materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }
        [Display(Name = "Nacimiento")]
        [JsonProperty("f_nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime f_nacimiento { get; set; }
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }
        [Display(Name = "DNI")]
        [JsonProperty("dni")]
        public string dni { get; set; }
        public string estado_persona { get; set; }
        public int id_tipo_trabajador { get; set; }
        [Display(Name = "Tipo")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        public string estado_tipo_trabajador { get; set; }

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