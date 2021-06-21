using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Conductor
    {
        [Display(Name = "Id_conductor")]
        [JsonProperty("id_conductor")]
        public int id_conductor { get; set; }

        [Display(Name = "Fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "N_licencia")]
        [JsonProperty("n_licencia")]
        public string n_licencia { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "-1")
                {
                    nestado = "TODAS";
                }
                else if (estado == "0")
                {
                    nestado = "ANULADA";
                }
                else if (estado == "1")
                {
                    nestado = "VIGENTE";
                }
                return nestado;
            }
            set
            {
            }
        }

        //Persona
        [Display(Name = "Id_persona")]
        [JsonProperty("id_persona")]
        public int id_persona { get; set; }

        [Display(Name = "Nombres")]
        [JsonProperty("nombres")]
        public string nombres { get; set; }

        [Display(Name = "Apellido_paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }

        [Display(Name = "Apellido_materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }

        [Display(Name = "F_nacimiento")]
        [JsonProperty("f_nacimiento")]
        public string f_nacimiento { get; set; }

        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }

        [Display(Name = "Dni")]
        [JsonProperty("dni")]
        public string dni { get; set; }

        [Display(Name = "Estado_persona")]
        [JsonProperty("estado_persona")]
        public string estado_persona { get; set; }
    }
}