using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class Trabajador
    {
        [Display(Name = "Id")]
        public int id_trabajador { get; set; }
        [Display(Name = "IdPersona")]
        public int fk_persona { get; set; }
        [Display(Name = "IdTipoTrabajador")]
        public int fk_tipo_trabajador { get; set; }
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

        public int id_persona { get; set; }
        [Display(Name = "Nombres")]
        public string nombres { get; set; }
        [Display(Name = "A. Paterno")]
        public string apellido_paterno { get; set; }
        [Display(Name = "A. Materno")]
        public string apellido_materno { get; set; }
        public string f_nacimiento { get; set; }
        [Display(Name = "F. Nacimiento")]
        public string Fnacimiento
        {
            get
            {
                string fnacimiento = "";
                DateTime fechax = Convert.ToDateTime(f_nacimiento);
                fnacimiento = fechax.ToString("dd/MM/yyyy");
                return fnacimiento;
            }
            set
            {
            }
        }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "DNI")]
        public string dni { get; set; }
        public string estado_persona { get; set; }
        public int id_tipo_trabajador { get; set; }
        [Display(Name = "Tipo Trabajador")]
        public string descripcion { get; set; }
        public string estado_tipo_trabajador { get; set; }
    }
}