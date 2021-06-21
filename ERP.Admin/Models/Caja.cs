using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Caja
    {
        [Display(Name = "id_caja")]
        [JsonProperty("id_caja")]
        public int id_caja { get; set; }

        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "f_inicio")]
        [JsonProperty("f_inicio")]
        public DateTime f_inicio { get; set; }

        [Display(Name = "f_fin")]
        [JsonProperty("f_fin")]
        public DateTime f_fin { get; set; }

        [Display(Name = "monto_apertura")]
        [JsonProperty("monto_apertura")]
        public decimal monto_apertura { get; set; }

        [Display(Name = "monto_cierre")]
        [JsonProperty("monto_cierre")]
        public decimal monto_cierre { get; set; }

        [Display(Name = "estado")]
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
                } else if (estado == "0")
                {
                    nestado = "ANULADA";
                } else if (estado == "1")
                {
                    nestado = "ABIERTA";
                }
                else if (estado == "2") {
                    nestado = "CERRADA";
                }
                return nestado;
            }

            set { }
        }

        public string usuario_nombre_completo
        {
            get
            {
                return dni + " - " + nombres + " " + apellido_paterno + " " + apellido_materno;
            }
            set { }
        }

        //USUARIO
        [Display(Name = "fk_trabajador")]
        [JsonProperty("fk_trabajador")]
        public int fk_trabajador { get; set; }

        [Display(Name = "fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "nombres")]
        [JsonProperty("nombres")]
        public string nombres { get; set; }

        [Display(Name = "apellido_paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }

        [Display(Name = "apellido_materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }

        [Display(Name = "email")]
        [JsonProperty("email")]
        public string email { get; set; }

        [Display(Name = "dni")]
        [JsonProperty("dni")]
        public string dni { get; set; }
    }
}