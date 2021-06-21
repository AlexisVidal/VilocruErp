using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoAfectacion
    {
        public int id_tipo_afectacion_igv { get; set; }
        public int fk_igv { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string flag_afecto_igv { get; set; }
        public string flag_tipo_afecto { get; set; }
        public string estado { get; set; }
        public decimal porcentaje { get; set; }
        public string codespecial { get; set; }
    }
}