using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ServicioErp
    {
        public int id_servicio { get; set; }
        public string cod_servicio { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public string codigo_sunat { get; set; }
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
    }
}