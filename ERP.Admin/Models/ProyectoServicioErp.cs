using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProyectoServicioErp
    {
        public int id_proyecto_servicio { get; set; }
        public int fk_proyecto { get; set; }
        public int fk_servicio { get; set; }
        public DateTime f_registro { get; set; }
        public string IDUSUARIO { get; set; }
        public string servicio { get; set; }

    }
}