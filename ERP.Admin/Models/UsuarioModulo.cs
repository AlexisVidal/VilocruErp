using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class UsuarioModulo
    {
        public int id_modulo { get; set; }
        public string sigla { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public int id_usuario_modulo { get; set; }
        public string IDUSUARIO { get; set; }
        public int fk_modulo { get; set; }
        public string estado { get; set; }
        public int id_modulo_menu { get; set; }
        public string menu { get; set; }
        public int fk_menu_modulo { get; set; }

    }
}