using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ObligacionTipoErp
    {
        public int id_obligacion_tipo { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
    }
}