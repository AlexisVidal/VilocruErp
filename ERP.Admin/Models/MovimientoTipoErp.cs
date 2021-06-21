using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class MovimientoTipoErp
    {
        public int id_movimiento_tipo { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string codigo { get; set; }
    }
}