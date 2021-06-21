using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalOtrosDocumentosErp
    {
        public int IDPERSONALOTROSDOC { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public int IDOTROSDOC { get; set; }
        public string DOCUMENTO_ADJUNTO { get; set; }
        public string ESTADO { get; set; }
        public string DOCUMENTO { get; set; }

        [Display(Name = "ESTADO")]
        [JsonProperty("Nestado")]
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