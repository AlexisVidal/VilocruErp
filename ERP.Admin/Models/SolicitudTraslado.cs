using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class SolicitudTraslado
    {
        [Display(Name = "Id_solicitud_traslado")]
        [JsonProperty("id_solicitud_traslado")]
        public int id_solicitud_traslado { get; set; }

        [Display(Name = "Fk_almacen_solicita")]
        [JsonProperty("fk_almacen_solicita")]
        public int fk_almacen_solicita { get; set; }

        [Display(Name = "Fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "NRO SOLICITUD")]
        [JsonProperty("n_solicitud_traslado")]
        public string n_solicitud_traslado { get; set; }

        [Display(Name = "FECHA")]
        [JsonProperty("f_solicitud_traslado")]
        public string f_solicitud_traslado { get; set; }

        [Display(Name = "tipo")]
        [JsonProperty("tipo")]
        public string tipo { get; set; }

        public string descripcion_tipo
        {
            get
            {
                string desc_tipo = "";
                if (tipo == "1")
                {
                    desc_tipo = "MANUALMENTE";
                }
                else if (tipo == "2")
                {
                    desc_tipo = "AUTOMATICAMENTE";
                }
                return desc_tipo;
            }
            set { }
        }

        [Display(Name = "motivo_cierre")]
        [JsonProperty("motivo_cierre")]
        public string motivo_cierre { get; set; }
        
        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "-1")
                {
                    nestado = "TODAS";
                }
                else if(estado == "0")
                {
                    nestado = "ANULADA";
                }
                else if(estado == "1")
                {
                    nestado = "REGISTRADA";
                }
                else if(estado == "2")
                {
                    nestado = "ATENDIDA PARCIALMENTE";
                }
                else if(estado == "3")
                {
                    nestado = "ATENDIDA TOTALMENTE";
                }
                else if(estado == "4")
                {
                    nestado = "FINALIZADA POR EL USUARIO";
                }
                return nestado;
            }
            set
            {

            }
        }

        //ALMACEN DESTINO
        [Display(Name = "Id_almacen_destino")]
        [JsonProperty("id_almacen_destino")]
        public int id_almacen_destino { get; set; }

        [Display(Name = "CÓDIGO ALM. DESTINO")]
        [JsonProperty("cod_almacen_destino")]
        public string cod_almacen_destino { get; set; }

        [Display(Name = "NOMBRE ALM. DESTINO")]
        [JsonProperty("nombre_almacen_destino")]
        public string nombre_almacen_destino { get; set; }

        [Display(Name = "UBICACIÓN ALM. DESTINO")]
        [JsonProperty("ubicacion_almacen_destino")]
        public string ubicacion_almacen_destino { get; set; }

        [Display(Name = "Estado_almacen_destino")]
        [JsonProperty("estado_almacen_destino")]
        public string estado_almacen_destino { get; set; }

        //USUARIO
        [Display(Name = "Id_usuario")]
        [JsonProperty("id_usuario")]
        public int id_usuario { get; set; }

        [Display(Name = "Fk_trabajador")]
        [JsonProperty("fk_trabajador")]
        public int fk_trabajador { get; set; }

        [Display(Name = "Id_trabajador")]
        [JsonProperty("id_trabajador")]
        public int id_trabajador { get; set; }

        [Display(Name = "Fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "Id_persona")]
        [JsonProperty("id_persona")]
        public int id_persona { get; set; }

        [Display(Name = "Nombres_persona")]
        [JsonProperty("nombres_persona")]
        public string nombres_persona { get; set; }

        [Display(Name = "Apellido_paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }

        [Display(Name = "Apellido_materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }

        [Display(Name = "REGISTRADO POR")]
        public string usuario_registro {
            get {
                return nombres_persona + " " + apellido_paterno + " " + apellido_materno;
            }
            set { }
        }
    }
}