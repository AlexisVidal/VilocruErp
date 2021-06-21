using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PrimerDiaHabilErp
    {
        public int id_primer_dia_habil { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public DateTime dia { get; set; }

        public int cuotas { get; set; }
        public DateTime diab { get; set; }
        public string df_dia
        {
            get
            {
                if (dia.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return dia.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
    }
}