using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ControlIngresoErp
    {
        public int id_tabla { get; set; }
        public string IDCONTROLINGRESO { get; set; }
        public DateTime FECHA { get; set; }
        public string PERIODO { get; set; }
        public string IDPLANILLA { get; set; }
        public string IDUSUARIO { get; set; }
        public string NOMBRES { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string IDESTADO { get; set; }
        public string generado_zktime { get; set; }
        public string procesado { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                switch (IDESTADO)
                {
                    case "PE":
                        nestado = "PENDIENTE";
                        break;
                    case "AN":
                        nestado = "ANULADO";
                        break;
                    case "CE":
                        nestado = "CERRADA";
                        break;
                }
                return nestado;
            }
            set
            {
            }
        }

        public string Ngenerado_zktime
        {
            get
            {
                string nzktime = "";
                nzktime = generado_zktime == "0" ? "NO" : "SI";
                return nzktime;
            }
            set
            {
            }
        }
        public string Nprocesado
        {
            get
            {
                string nproce = "";
                nproce = procesado == "0" ? "NO" : "SI";
                return nproce;
            }
            set
            {
            }
        }

        public string ficha_tareo { get; set; }
    }
}