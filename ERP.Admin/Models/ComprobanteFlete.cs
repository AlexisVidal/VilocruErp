using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ComprobanteFlete
    {
        public int id_comprobante_flete { get; set; }
        public int fk_comprobante_compra { get; set; }
        public int fk_movimiento_caja { get; set; }
        public string estado { get; set; }
    }
}