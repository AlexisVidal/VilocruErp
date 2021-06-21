using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoSinProveedor
    {
        [Display(Name = "ID")]
        [JsonProperty("id_producto")]
        public int id_producto { get; set; }

        [JsonProperty("fk_producto_marca_subfamilia")]
        public int fk_producto_marca_subfamilia { get; set; }

        [Display(Name = "Codigo")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }

        [Display(Name = "Nombre")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }

        [Display(Name = "SKU")]
        [JsonProperty("codigo_sku")]
        public string codigo_sku { get; set; }

        [Display(Name = "Embalaje")]
        [JsonProperty("embalaje")]
        public string embalaje { get; set; }

        [Display(Name = "Imagen")]
        [JsonProperty("image_url")]
        public string image_url { get; set; }

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