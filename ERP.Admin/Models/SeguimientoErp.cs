using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class SeguimientoErp
    {
        public int id_segumiento { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public DateTime fecha_registro { get; set; }
        public string ubicacion { get; set; }
        public string sensacion { get; set; }
        public string sintomas { get; set; }
        public string prueba_covid { get; set; }
        public string fecha_prueba { get; set; }
        public string contacto_covid { get; set; }
        public string trabajador { get; set; }
    }
}