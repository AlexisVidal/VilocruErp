using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalFamiliarErp
    {
        public int IDPERSONALFAMILIAR { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public string CODIGOGENERAL { get; set; }
        public string IDVINCULO { get; set; }
        public string NOMBRES { get; set; }
        public string TELEFONO { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string vinculo { get; set; }

        public DateTime fecha_nacimiento { get; set; }
        public string IDDOCVINC { get; set; }
        public string DOCVINC { get; set; }
        public string documento_adjunto { get; set; }
        public string ESTADO { get; set; }

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
                    nestado = "BAJA";
                }

                return nestado;
            }
            set
            {

            }
        }
        public string IDBAJA { get; set; }
        public DateTime fecha_baja { get; set; }
    }
}