using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoConceptoErp
    {
        public string IDTIPOCONCEPTO { get; set; }
        public string ESTADO { get; set; }
        public string PRIORIDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public string TPCTAS { get; set; }
        public DateTime FECHACREACION { get; set; }
    }
}