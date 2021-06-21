using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalReferenciaErp
    {
        public int IDPERSONALREFERENCIA { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public string NOMBRES { get; set; }
        public string CARGO { get; set; }
        public string TELEFONO { get; set; }
        public string ESTADO { get; set; }
    }
}