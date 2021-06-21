using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class MotivoPrestamoErp
    {
        public int id_motivo_prestamo { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
    }
}