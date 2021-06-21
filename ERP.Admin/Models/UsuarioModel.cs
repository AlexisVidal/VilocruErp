using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class UsuarioModel
    {
        public int id_usuario { get; set; }
        public int fk_trabajador { get; set; }
        public int fk_nivel_usuario { get; set; }
        public string clave { get; set; }
        public string clave_modificaciones { get; set; }
        public string estado_usuario { get; set; }
        public int id_trabajador { get; set; }
        public int fk_persona { get; set; }
        public string estado_trabajador { get; set; }
        public int id_nivel_usuario { get; set; }
        public string descripcion_nivel_usuario { get; set; }
        public string estado_nivel { get; set; }
        public int id_persona { get; set; }
        public string nombres { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public DateTime f_nacimiento { get; set; }
        public string email { get; set; }
        public string dni { get; set; }
        public string estado_persona { get; set; }
    }
}