using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ReporteContable
    {
        public string cont_anio { get; set; }
        public string cont_centro { get; set; }
        public string cont_mes { get; set; }
        public string voucher { get; set; }
        public string asiento { get; set; }
        public string num_subcuenta { get; set; }
        public string nombre { get; set; }
        public string concepto { get; set; }
        public decimal debe { get; set; }
        public decimal haber { get; set; }
        public string fecha { get; set; }
        public decimal deudor { get; set; }
        public decimal acreedor { get; set; }
        public decimal bg_debe { get; set; }
        public decimal bg_haber { get; set; }
        public decimal erf_debe { get; set; }
        public decimal erf_haber { get; set; }
        public decimal ern_debe { get; set; }
        public decimal ern_haber { get; set; }
        public string item { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public string posicion { get; set; }
        public string cuenta { get; set; }
        public int numero { get; set; }
        public string ncuenta { get; set; }
    }
}