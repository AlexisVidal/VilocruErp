using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class MigoDniErp
    {
        public bool success { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string estado_del_contribuyente { get; set; }
        public string condicion_de_domicilio { get; set; }
        public string distrito { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }
        public string direccion { get; set; }
        public string token { get; set; }

    }
}