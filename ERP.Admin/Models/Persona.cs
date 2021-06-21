using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Persona
    {
        [Display(Name = "ID")]
        [JsonProperty("id_persona")]
        public int id_persona { get; set; }
        [Display(Name = "Nombres")]
        [JsonProperty("nombres")]
        public string nombres { get; set; }
        [Display(Name = "Apellido Paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }
        [Display(Name = "Apellido Materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }
        [Display(Name = "f_nacimiento")]
        [JsonProperty("f_nacimiento")]
        public DateTime f_nacimiento { get; set; }
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }
        [Display(Name = "DNI")]
        [JsonProperty("dni")]
        public string dni { get; set; }
        [Display(Name = "estados")]
        [JsonProperty("estado")]
        public string estado { get; set; }
        [Display(Name = "Fecha")]
        public string FechaTexto
        {
            get
            {
                string fechaTexto = "";
                DateTime fechax = Convert.ToDateTime(f_nacimiento);
                fechaTexto = fechax.ToString("d");
                return fechaTexto;
            }
            set
            {

            }

        }
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
        [Display(Name = "NombreCompleto")]
        public string NombreCompleto
        {
            get
            {
                string nombreCompleto = "";
                nombreCompleto = apellido_paterno + " " + apellido_materno + ", " + nombres;
                return nombreCompleto;
            }
            set
            {

            }

        }
        public string direccion { get; set; }
    }
}