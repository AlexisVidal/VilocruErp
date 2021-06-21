using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Modulo
    {
        public int id_modulo { get; set; }
        public string sigla { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
    }
}