using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class DetraccionTipo
    {
        public int id_detraccion_tipo { get; set; }
        public string codigo_detraccion { get; set; }
        public string detraccion_tipo { get; set; }
        public int porcentaje { get; set; }
        public string estado_detraccion { get; set; }
    }
}