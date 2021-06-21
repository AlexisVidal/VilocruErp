using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class DocIdentidadErp
    {
        public string IDDOCIDENTIDAD { get; set; }
        public string ESTADO { get; set; }
        public string DESCRIPCION { get; set; }
        public string DESCR_CORTO { get; set; }
        public string FORMATO { get; set; }
        public string SINCRONIZA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string codigo_sbs { get; set; }
        public string codigo_essalud { get; set; }
        public string IDREGISTRO_SUNAT { get; set; }
        public string orden { get; set; }
    }
}