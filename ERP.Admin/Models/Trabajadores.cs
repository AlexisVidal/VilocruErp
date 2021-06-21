using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ERP.Admin.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    [Table("tbl_trabajador")]
    public class Trabajadores
    {
        [Key]
        [DataMember]
        [Display(Name = "ID")]
        public string id { get; set; }

        [DataMember]
        [Display(Name = "Nombres")]
        public string nombres { get; set; }

        [DataMember]
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }

        [DataMember]
        [Display(Name = "DNI")]
        public string dni { get; set; }

        [DataMember]
        [Display(Name = "Nacimiento")]
        public System.DateTime nacimiento { get; set; }

        [DataMember]
        [Display(Name = "Empresa")]
        public string empresa { get; set; }

        [DataMember]
        [Display(Name = "Direccion")]
        public string direccion { get; set; }

        [DataMember]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [DataMember]
        [Display(Name = "Celular")]
        public string celular { get; set; }

        [DataMember]
        [Display(Name = "Sexo")]
        public string sexo { get; set; }

        [DataMember]
        [Display(Name = "Foto")]
        public string foto { get; set; }
    }
}