using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class CuentaPorPagarLetrasErp
    {
        public int id_cuentas_por_pagar_bancos_letras { get; set; }
        public int fk_cuentas_por_pagar { get; set; }
        public int nro_cuota { get; set; }
        public DateTime f_vencimiento { get; set; }
        public decimal monto { get; set; }
        public string estado { get; set; }
    }
}