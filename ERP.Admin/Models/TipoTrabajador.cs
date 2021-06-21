using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoTrabajador
    {
        [Display(Name = "Id")]
        public int id_tipo_trabajador { get; set; }
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
        public string estado { get; set; }
        [Display(Name = "Estado")]
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