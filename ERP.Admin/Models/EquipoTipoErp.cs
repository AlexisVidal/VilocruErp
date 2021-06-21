using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class EquipoTipoErp
    {
        public int id_tipo_equipo { get; set; }
        public int fk_equipo { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
    }
}