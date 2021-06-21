using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ServicioTomadoDetalleErp
    {
        public int id_servicio_tomado_detalle { get; set; }
        public int fk_servicio_tomado { get; set; }
        public int fk_tipo_afectacion_igv { get; set; }
        public string nombre_servicio { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
        public int total { get; set; }
        public string estado { get; set; }
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "1")
                {
                    nestado = "ACTIVA";
                }
                else if (estado == "0")
                {
                    nestado = "ANULADA";
                }
                return nestado;
            }
            set
            {
            }
        }
    }
}