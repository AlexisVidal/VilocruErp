using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class FeriadoErp
    {
        public int id_feriado { get; set; }
        public DateTime FECHA { get; set; }
        public string DESCRIPCION { get; set; }
        public string FERIADO_REGIONAL { get; set; }
        public string anio { get; set; }
        public string repite_anualmente { get; set; }

    }
}