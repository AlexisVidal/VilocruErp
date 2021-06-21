using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalCuentaErp
    {
        public int id_personal_cuenta { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public int fk_banco { get; set; }
        public string nro_cuenta { get; set; }
        public string estado { get; set; }
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "1")
                {
                    nestado = "ACTIVO";
                }
                else
                {
                    nestado = "INACTIVO";
                }
                return nestado;
            }
            set
            {
            }
        }
        public string idbanco { get; set; }
        public string banco { get; set; }
    }
}