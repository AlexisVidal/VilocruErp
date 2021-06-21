using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoContratoErp
    {
        public string IDTIPOCONTRATO { get; set; }
        public string ESTADO { get; set; }
        public string DESCRIPCION { get; set; }
        public string GLOSA { get; set; }
        public string RUTA { get; set; }
        public string FECHACREACION { get; set; }
        public string IDTPCONTRATO { get; set; }
        public string TIPOCONTRATOTRABAJADOR { get; set; }
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (ESTADO == "1")
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
    }
}