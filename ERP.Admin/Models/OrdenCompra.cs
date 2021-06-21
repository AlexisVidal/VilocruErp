using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class OrdenCompra
    {
        [Display(Name = "Id_orden_compra")]
        [JsonProperty("id_orden_compra")]
        public int id_orden_compra { get; set; }

        [Display(Name = "Fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "Fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

        [Display(Name = "N_orden_compra")]
        [JsonProperty("n_orden_compra")]
        public string n_orden_compra { get; set; }

        [Display(Name = "F_emision")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "Estado")]
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
                else if (estado == "0")
                {
                    nestado = "ANULADA";
                }
                else if (estado == "1")
                {
                    nestado = "REGISTRADA";
                }
                else if (estado == "2")
                {
                    nestado = "APROBADA";
                }
                else if (estado == "3")
                {
                    nestado = "ENT. PARCIALMETE";
                }
                else if (estado == "4")
                {
                    nestado = "ENT. TOTALMENTE";
                }
                return nestado;
            }
            set
            {
            }
        }

        //Proveedor
        [Display(Name = "Id_proveedor")]
        [JsonProperty("id_proveedor")]
        public int id_proveedor { get; set; }

        [Display(Name = "Fk_empresa")]
        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }

        [Display(Name = "Cod_proveedor")]
        [JsonProperty("cod_proveedor")]
        public string cod_proveedor { get; set; }

        [Display(Name = "Estado_proveedor")]
        [JsonProperty("estado_proveedor")]
        public string estado_proveedor { get; set; }

        //Empresa
        [Display(Name = "Id_empresa")]
        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }

        [Display(Name = "Ruc")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }

        [Display(Name = "Razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }

        [Display(Name = "Estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //Usuario
        [Display(Name = "Id_usuario")]
        [JsonProperty("id_usuario")]
        public int id_usuario { get; set; }

        [Display(Name = "Fk_trabajador")]
        [JsonProperty("fk_trabajador")]
        public int fk_trabajador { get; set; }

        [Display(Name = "Estado_usuario")]
        [JsonProperty("estado_usuario")]
        public string estado_usuario { get; set; }

        //Trabajador
        [Display(Name = "Id_trabajador")]
        [JsonProperty("id_trabajador")]
        public int id_trabajador { get; set; }

        [Display(Name = "Fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "Fk_tipo_trabajador")]
        [JsonProperty("fk_tipo_trabajador")]
        public int fk_tipo_trabajador { get; set; }

        [Display(Name = "Estado_trabajador")]
        [JsonProperty("estado_trabajador")]
        public string estado_trabajador { get; set; }

        //Persona
        [Display(Name = "Id_persona")]
        [JsonProperty("id_persona")]
        public int id_persona { get; set; }

        [Display(Name = "Nombres")]
        [JsonProperty("nombres")]
        public string nombres { get; set; }

        [Display(Name = "Apellido_paterno")]
        [JsonProperty("apellido_paterno")]
        public string apellido_paterno { get; set; }

        [Display(Name = "Apellido_materno")]
        [JsonProperty("apellido_materno")]
        public string apellido_materno { get; set; }

        [Display(Name = "F_nacimiento")]
        [JsonProperty("f_nacimiento")]
        public string f_nacimiento { get; set; }

        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }

        [Display(Name = "Dni")]
        [JsonProperty("dni")]
        public string dni { get; set; }

        [Display(Name = "Estado_persona")]
        [JsonProperty("estado_persona")]
        public string estado_persona { get; set; }

        //Tipo Trabajador
        [Display(Name = "Id_tipo_trabajador")]
        [JsonProperty("id_tipo_trabajador")]
        public int id_tipo_trabajador { get; set; }

        [Display(Name = "Descripcion_tipo_trabajador")]
        [JsonProperty("descripcion_tipo_trabajador")]
        public string descripcion_tipo_trabajador { get; set; }

        [Display(Name = "Estado_tipo_trabajador")]
        [JsonProperty("estado_tipo_trabajador")]
        public string estado_tipo_trabajador { get; set; }
    }
}