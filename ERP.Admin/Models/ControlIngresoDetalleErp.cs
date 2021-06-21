using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ControlIngresoDetalleErp
    {
        public string IDCONTROLINGRESO { get; set; }
        public string ITEM { get; set; }
        public string CORRELATIVO { get; set; }
        public string IDPERSONAL { get; set; }
        public string NOMBRES { get; set; }
        public string IDPLANILLA { get; set; }
        public string PLANILLA { get; set; }
        public string MARCACION { get; set; }
        public DateTime FECHA { get; set; }
        public string TIPO { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string idgrupo_trabajo { get; set; }
        public string IDUSUARIO { get; set; }
        public string NTIPO
        {
            get
            {
                string ntipo = "";
                switch (TIPO)
                {
                    case "I":
                        ntipo = "INGRESO";
                        break;
                    case "S":
                        ntipo = "SALIDA";
                        break;
                    case "R":
                        ntipo = "RE-INGRESO";
                        break;
                    case "Z":
                        ntipo = "RE-SALIDA";
                        break;
                    case "A":
                        ntipo = "INICIO REFRIGERIO";
                        break;
                    case "B":
                        ntipo = "FIN REFRIGERIO";
                        break;
                }
                return ntipo;
            }
            set
            {
            }
        }

        public string DESCTIPO { get; set; }
        public int id_tabla { get; set; }          
    }
}