using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class GastoPersonalLiquidacionErp
    {
        public int ITEM { get; set; }
        public int id_gasto_personal_liquidacion { get; set; }
        public int fk_gasto_personal { get; set; }
        public string codigo { get; set; }
        public DateTime f_emision { get; set; }
        public string sf_emision
        {
            get
            {
                return f_emision.ToString("dd/MM/yyyy");
            }
            set
            {
            }
        }
        public string documento { get; set; }
        public string concepto { get; set; }
        public string destino { get; set; }
        public decimal monto { get; set; }
        public string estado { get; set; }
        public DateTime f_registro { get; set; }
        public decimal ingreso { get; set; }
        public int id_gasto_personal { get; set; }
        public int fk_proyecto { get; set; }
        public string proyecto { get; set; }

        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string revisado { get; set; }

        public string brevisado
        {
            get
            {
                string brevisadox = "";
                if (revisado == "1")
                {
                    brevisadox = "checked";
                }

                return brevisadox;
            }
            set
            {

            }
        }
    }
}