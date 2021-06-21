using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalHorarioErp
    {
        public int id_personal_horario { get; set; }
        public int fk_proyecto { get; set; }
        public string proyecto { get; set; }
        public string lugar { get; set; }
        public string IDPERSONAL { get; set; }
        public string nombres { get; set; }
        public decimal lunes { get; set; }
        public decimal martes { get; set; }
        public decimal miercoles { get; set; }
        public decimal jueves { get; set; }
        public decimal viernes { get; set; }
        public decimal sabado { get; set; }
        public decimal horas_mes { get; set; }
    }
}