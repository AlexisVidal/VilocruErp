using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Cliente
    {
        [Display(Name = "Id_cliente")]
        [JsonProperty("id_cliente")]
        public int id_cliente { get; set; }

        [Display(Name = "Fk_cliente_tipo")]
        [JsonProperty("fk_cliente_tipo")]
        public int fk_cliente_tipo { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

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

        [Display(Name = "Id_cliente_tipo")]
        [JsonProperty("id_cliente_tipo")]
        public int id_cliente_tipo { get; set; }

        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Estado_cliente_tipo")]
        [JsonProperty("estado_cliente_tipo")]
        public string estado_cliente_tipo { get; set; }

        [Display(Name = "dni_ruc")]
        [JsonProperty("dni_ruc")]
        public string dni_ruc { get; set; }

        [Display(Name = "nombre_razon_social")]
        [JsonProperty("nombre_razon_social")]
        public string nombre_razon_social { get; set; }

        public string email { get; set; }
    }
}