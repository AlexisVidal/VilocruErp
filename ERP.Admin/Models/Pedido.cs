using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class Pedido
    {
        [Display(Name = "Id_pedido")]
        [JsonProperty("id_pedido")]
        public int id_pedido { get; set; }

        [Display(Name = "Fk_cliente")]
        [JsonProperty("fk_cliente")]
        public int fk_cliente { get; set; }

        [Display(Name = "Fk_userventa")]
        [JsonProperty("fk_userventa")]
        public int fk_userventa { get; set; }

        [Display(Name = "N_pedido")]
        [JsonProperty("n_pedido")]
        public string n_pedido { get; set; }

        [Display(Name = "tipo_pago")]
        [JsonProperty("tipo_pago")]
        public string tipo_pago { get; set; }

        [Display(Name = "F_pedido")]
        [JsonProperty("f_pedido")]
        public string f_pedido { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if(estado == "-1")
                {
                    nestado = "TODOS";
                }
                else if(estado == "0")
                {
                    nestado = "ANULADO";
                }
                else if(estado == "1")
                {
                    nestado = "REGISTRADO";
                }
                else if(estado == "2")
                {
                    nestado = "FACTURADO";
                }
                return nestado;
            }
            set
            {

            }
        }

        //Cliente
        [Display(Name = "Id_cliente")]
        [JsonProperty("id_cliente")]
        public int id_cliente { get; set; }

        [Display(Name = "Fk_cliente_tipo")]
        [JsonProperty("fk_cliente_tipo")]
        public int fk_cliente_tipo { get; set; }

        [Display(Name = "Estado_cliente")]
        [JsonProperty("estado_cliente")]
        public string estado_cliente { get; set; }

        //Cliente Tipo
        [Display(Name = "Id_cliente_tipo")]
        [JsonProperty("id_cliente_tipo")]
        public int id_cliente_tipo { get; set; }

        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Estado_cliente_tipo")]
        [JsonProperty("estado_cliente_tipo")]
        public string estado_cliente_tipo { get; set; }

        //Cliente Natural
        [Display(Name = "Id_cliente_natural")]
        [JsonProperty("id_cliente_natural")]
        public int id_cliente_natural { get; set; }

        [Display(Name = "Fk_persona_cliente_natural")]
        [JsonProperty("fk_persona_cliente_natural")]
        public int fk_persona_cliente_natural { get; set; }

        [Display(Name = "Fk_cliente_cliente_natural")]
        [JsonProperty("fk_cliente_cliente_natural")]
        public int fk_cliente_cliente_natural { get; set; }

        [Display(Name = "Ruc_cliente_natural")]
        [JsonProperty("ruc_cliente_natural")]
        public string ruc_cliente_natural { get; set; }

        [Display(Name = "Estado_cliente_natural")]
        [JsonProperty("estado_cliente_natural")]
        public string estado_cliente_natural { get; set; }

        //Persona Cliente Natural
        [Display(Name = "Id_persona_cliente_natural")]
        [JsonProperty("id_persona_cliente_natural")]
        public int id_persona_cliente_natural { get; set; }

        [Display(Name = "Nombre_cliente_natural")]
        [JsonProperty("nombre_cliente_natural")]
        public string nombre_cliente_natural { get; set; }

        [Display(Name = "Apellido_paterno_cliente_natural")]
        [JsonProperty("apellido_paterno_cliente_natural")]
        public string apellido_paterno_cliente_natural { get; set; }

        [Display(Name = "Apellido_materno_cliente_natural")]
        [JsonProperty("apellido_materno_cliente_natural")]
        public string apellido_materno_cliente_natural { get; set; }

        [Display(Name = "F_nacimiento_cliente_natural")]
        [JsonProperty("f_nacimiento_cliente_natural")]
        public string f_nacimiento_cliente_natural { get; set; }

        [Display(Name = "Email_cliente_natural")]
        [JsonProperty("email_cliente_natural")]
        public string email_cliente_natural { get; set; }
        public string email_cliente_juridico { get; set; }

        [Display(Name = "Dni_cliente_natural")]
        [JsonProperty("dni_cliente_natural")]
        public string dni_cliente_natural { get; set; }

        [Display(Name = "Estado_persona_cliente_natural")]
        [JsonProperty("estado_persona_cliente_natural")]
        public string estado_persona_cliente_natural { get; set; }

        //Cliente Juridico
        [Display(Name = "Id_cliente_juridico")]
        [JsonProperty("id_cliente_juridico")]
        public int id_cliente_juridico { get; set; }

        [Display(Name = "Fk_empresa_cliente_juridico")]
        [JsonProperty("fk_empresa_cliente_juridico")]
        public int fk_empresa_cliente_juridico { get; set; }

        [Display(Name = "Fk_cliente_cliente_juridico")]
        [JsonProperty("fk_cliente_cliente_juridico")]
        public int fk_cliente_cliente_juridico { get; set; }

        [Display(Name = "Estado_cliente_juridico")]
        [JsonProperty("estado_cliente_juridico")]
        public string estado_cliente_juridico { get; set; }

        //Empresa Cliente Juridico
        [Display(Name = "Id_empresa_cliente_juridico")]
        [JsonProperty("id_empresa_cliente_juridico")]
        public int id_empresa_cliente_juridico { get; set; }

        [Display(Name = "Ruc_empresa_cliente_juridico")]
        [JsonProperty("ruc_empresa_cliente_juridico")]
        public string ruc_empresa_cliente_juridico { get; set; }

        [Display(Name = "Razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "Direccion_empresa")]
        [JsonProperty("direccion_empresa")]
        public string direccion_empresa { get; set; }

        [Display(Name = "Estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //Usuario Venta
        [Display(Name = "Id_usuario_venta")]
        [JsonProperty("id_usuario_venta")]
        public int id_usuario_venta { get; set; }

        [Display(Name = "Fk_trabajador_usuario_venta")]
        [JsonProperty("fk_trabajador_usuario_venta")]
        public int fk_trabajador_usuario_venta { get; set; }

        //Trabajador usuario Venta
        [Display(Name = "Id_trabajador_usuario_venta")]
        [JsonProperty("id_trabajador_usuario_venta")]
        public int id_trabajador_usuario_venta { get; set; }

        [Display(Name = "Fk_persona_usuario_venta")]
        [JsonProperty("fk_persona_usuario_venta")]
        public int fk_persona_usuario_venta { get; set; }

        [Display(Name = "Fk_tipo_trabajador_usuario_venta")]
        [JsonProperty("fk_tipo_trabajador_usuario_venta")]
        public int fk_tipo_trabajador_usuario_venta { get; set; }

        [Display(Name = "Estado_trabajador_usuario_venta")]
        [JsonProperty("estado_trabajador_usuario_venta")]
        public string estado_trabajador_usuario_venta { get; set; }

        //Persona Usuario Venta
        [Display(Name = "Id_persona_usuario_venta")]
        [JsonProperty("id_persona_usuario_venta")]
        public int id_persona_usuario_venta { get; set; }

        [Display(Name = "Nombre_usuario_venta")]
        [JsonProperty("nombre_usuario_venta")]
        public string nombre_usuario_venta { get; set; }

        [Display(Name = "Apellido_paterno_usuario_venta")]
        [JsonProperty("apellido_paterno_usuario_venta")]
        public string apellido_paterno_usuario_venta { get; set; }

        [Display(Name = "Apellido_materno_usuario_venta")]
        [JsonProperty("apellido_materno_usuario_venta")]
        public string apellido_materno_usuario_venta { get; set; }

        [Display(Name = "F_nacimiento_usuario_venta")]
        [JsonProperty("f_nacimiento_usuario_venta")]
        public string f_nacimiento_usuario_venta { get; set; }

        [Display(Name = "Email_usuario_venta")]
        [JsonProperty("email_usuario_venta")]
        public string email_usuario_venta { get; set; }

        [Display(Name = "Dni_usuario_venta")]
        [JsonProperty("dni_usuario_venta")]
        public string dni_usuario_venta { get; set; }

        [Display(Name = "Estado_persona_usuario_venta")]
        [JsonProperty("estado_persona_usuario_venta")]
        public string estado_persona_usuario_venta { get; set; }
    }
}