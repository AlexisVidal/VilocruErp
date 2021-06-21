using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class GuiaComprobante
    {
        [Display(Name = "Id_guia_comprobante")]
        [JsonProperty("id_guia_comprobante")]
        public int id_guia_comprobante { get; set; }

        [Display(Name = "Fk_guia_remision")]
        [JsonProperty("fk_guia_remision")]
        public int fk_guia_remision { get; set; }

        [Display(Name = "Fk_comprobante_compra")]
        [JsonProperty("fk_comprobante_compra")]
        public int fk_comprobante_compra { get; set; }

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
    }
}