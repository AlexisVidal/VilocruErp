using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class GuiaRemision
    {
        [Display(Name = "Id_guia_remision")]
        [JsonProperty("id_guia_remision")]
        public int id_guia_remision { get; set; }

        [Display(Name = "Fk_compra")]
        [JsonProperty("fk_compra")]
        public int fk_compra { get; set; }

        [Display(Name = "Fk_conductor")]
        [JsonProperty("fk_conductor")]
        public int fk_conductor { get; set; }

        [Display(Name = "Fk_transporte")]
        [JsonProperty("fk_transporte")]
        public int fk_transporte { get; set; }

        [Display(Name = "Nro_guia")]
        [JsonProperty("nro_guia")]
        public string nro_guia { get; set; }

        [Display(Name = "F_emision")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "F_traslado")]
        [JsonProperty("f_traslado")]
        public string f_traslado { get; set; }

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
                    nestado = "FINALIZADA";
                }
                else if (estado == "3")
                {
                    nestado = "FACTURADA";
                }
                return nestado;
            }
            set
            {
            }
        }

        //Compra
        [Display(Name = "Id_compra")]
        [JsonProperty("id_compra")]
        public int id_compra { get; set; }

        [Display(Name = "Fk_orden_compra")]
        [JsonProperty("fk_orden_compra")]
        public int fk_orden_compra { get; set; }

        [Display(Name = "Fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "Fk_proveedor_compra")]
        [JsonProperty("fk_proveedor_compra")]
        public int fk_proveedor_compra { get; set; }

        [Display(Name = "N_compra")]
        [JsonProperty("n_compra")]
        public string n_compra { get; set; }

        [Display(Name = "F_compra")]
        [JsonProperty("f_compra")]
        public string f_compra { get; set; }

        [Display(Name = "Estado_compra")]
        [JsonProperty("estado_compra")]
        public string estado_compra { get; set; }

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

        //Conductor
        [Display(Name = "Id_conductor")]
        [JsonProperty("id_conductor")]
        public int id_conductor { get; set; }

        [Display(Name = "Fk_persona")]
        [JsonProperty("fk_persona")]
        public int fk_persona { get; set; }

        [Display(Name = "N_licencia")]
        [JsonProperty("n_licencia")]
        public string n_licencia { get; set; }

        [Display(Name = "Estado_conductor")]
        [JsonProperty("estado_conductor")]
        public string estado_conductor { get; set; }

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

        //Transporte
        [Display(Name = "Id_transporte")]
        [JsonProperty("id_transporte")]
        public int id_transporte { get; set; }

        [Display(Name = "N_placa")]
        [JsonProperty("n_placa")]
        public string n_placa { get; set; }

        [Display(Name = "Estado_transporte")]
        [JsonProperty("estado_transporte")]
        public string estado_transporte { get; set; }
    }
}