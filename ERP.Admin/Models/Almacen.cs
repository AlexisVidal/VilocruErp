using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Almacen
    {
        [Display(Name = "ID")]
        [JsonProperty("id_almacen")]
        public int id_almacen { get; set; }
        [Display(Name = "Codigo")]
        [JsonProperty("cod_almacen")]
        public string cod_almacen { get; set; }
        [Display(Name = "Nombre")]
        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [Display(Name = "Ubicacion")]
        [JsonProperty("ubicacion")]
        public string ubicacion { get; set; }
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
    }
}