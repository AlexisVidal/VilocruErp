using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class MovimientoCaja
    {
        [Display(Name = "id_movimiento_caja")]
        [JsonProperty("id_movimiento_caja")]
        public int id_movimiento_caja { get; set; }

        [Display(Name = "fk_caja")]
        [JsonProperty("fk_caja")]
        public int fk_caja { get; set; }

        [Display(Name = "fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "f_movimiento")]
        [JsonProperty("f_movimiento")]
        public DateTime f_movimiento { get; set; }

        [Display(Name = "nro_documento")]
        [JsonProperty("nro_documento")]
        public string nro_documento { get; set; }

        [Display(Name = "descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "monto")]
        [JsonProperty("monto")]
        public decimal monto { get; set; }

        [Display(Name = "ingreso_egreso")]
        [JsonProperty("ingreso_egreso")]
        public string ingreso_egreso { get; set; }

        public string descripcion_ingreso_egreso
        {
            get
            {
                string descIngrEgre = "";
                if(ingreso_egreso == "1")
                {
                    descIngrEgre = "INGRESO";
                }else if(ingreso_egreso == "2")
                {
                    descIngrEgre = "EGRESO";
                }
                return descIngrEgre;
            }
            set { }
        }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        public string NEstado
        {
            get {
                string nestado = "";
                if(estado == "1")
                {
                    nestado = "ABIERTA";
                }else if(estado == "2")
                {
                    nestado = "CERRADA";
                }
                return nestado;
            }
            set { }
        }

        //COMRPOBANTE TIPO
        [Display(Name = "descripcion_comprobante_tipo")]
        [JsonProperty("descripcion_comprobante_tipo")]
        public string descripcion_comprobante_tipo { get; set; }
    }
}