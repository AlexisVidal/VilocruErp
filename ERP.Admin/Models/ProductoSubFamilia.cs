using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoSubFamilia
    {
        [Display(Name = "ID")]
        [JsonProperty("id_producto_subfamilia")]
        public int id_producto_subfamilia { get; set; }
        [Display(Name = "ID Familia")]
        [JsonProperty("fk_producto_familia")]
        public int fk_producto_familia { get; set; }
        [Display(Name = "Codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }
        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        public string estado { get; set; }

        public int id_producto_familia { get; set; }
        [Display(Name = "Codigo Familia")]
        [JsonProperty("codigo_producto_familia")]
        public string codigo_producto_familia { get; set; }
        [Display(Name = "Familia")]
        [JsonProperty("descripcion_producto_familia")]
        public string descripcion_producto_familia { get; set; }
        public string estado_producto_familia { get; set; }

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
        public string NSubFamiliaMix
        {
            get
            {
                string nsubfamiliamix = "";
                nsubfamiliamix = descripcion_producto_familia + " - " + descripcion;
                return nsubfamiliamix;
            }
            set
            {
            }
        }
    }
}