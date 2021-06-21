using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class GuiaRemisionSalida
    {
        [Display(Name = "Id_guia_remision_salida")]
        [JsonProperty("id_guia_remision_salida")]
        public int id_guia_remision_salida { get; set; }

        [Display(Name = "Fk_almacen_movimiento")]
        [JsonProperty("fk_almacen_movimiento")]
        public int fk_almacen_movimiento { get; set; }

        [Display(Name = "Fk_conductor")]
        [JsonProperty("fk_conductor")]
        public int fk_conductor { get; set; }

        [Display(Name = "Fk_transporte")]
        [JsonProperty("fk_transporte")]
        public int fk_transporte { get; set; }
        [Display(Name = "Fk_empresa")]
        [JsonProperty("fk_empresa")]
        public int fk_empresa { get; set; }

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

        //AlmacenMovimiento
        [Display(Name = "id_almacen_movimiento")]
        [JsonProperty("id_almacen_movimiento")]
        public int id_almacen_movimiento { get; set; }

        [Display(Name = "id_movimiento")]
        [JsonProperty("id_movimiento")]
        public int id_movimiento { get; set; }
        [Display(Name = "Fecha")]
        [JsonProperty("fecha")]
        public DateTime fecha { get; set; }
        [Display(Name = "fk_almacen")]
        [JsonProperty("fk_almacen")]
        public int fk_almacen { get; set; }
        [Display(Name = "fk_movimiento_tipo")]
        [JsonProperty("fk_movimiento_tipo")]
        public int fk_movimiento_tipo { get; set; }
        [Display(Name = "codigo_movimiento_tipo")]
        [JsonProperty("codigo_movimiento_tipo")]
        public string codigo_movimiento_tipo { get; set; }
        [Display(Name = "Documento")]
        [JsonProperty("IDCODIGOGENERAL")]
        public string IDCODIGOGENERAL { get; set; }
        [Display(Name = "Cliente")]
        [JsonProperty("cliente")]
        public string cliente { get; set; }
        //[Display(Name = "Direccion")]
        //[JsonProperty("direccion")]
        //public string direccion { get; set; }
        [Display(Name = "OC/OS")]
        [JsonProperty("oc_os")]
        public string oc_os { get; set; }
        [Display(Name = "Unidad")]
        [JsonProperty("maquina_unidad")]
        public string maquina_unidad { get; set; }
        [Display(Name = "Observaciones")]
        [JsonProperty("observaciones")]
        public string observaciones { get; set; }
        [Display(Name = "IDRESPONSABLE")]
        [JsonProperty("IDRESPONSABLE")]
        public string IDRESPONSABLE { get; set; }
        [Display(Name = "id_almacen")]
        [JsonProperty("id_almacen")]
        public int id_almacen { get; set; }
        [Display(Name = "Cod Almacen")]
        [JsonProperty("cod_almacen")]
        public string cod_almacen { get; set; }
        [Display(Name = "Almacen")]
        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [Display(Name = "Ubicacion")]
        [JsonProperty("ubicacion")]
        public string ubicacion { get; set; }
        [Display(Name = "id_movimiento_tipo")]
        [JsonProperty("id_movimiento_tipo")]
        public int id_movimiento_tipo { get; set; }
        [Display(Name = "Descripcion")]
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        [Display(Name = "Codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }
        [Display(Name = "A Paterno")]
        [JsonProperty("A_PATERNO")]
        public string A_PATERNO { get; set; }
        [Display(Name = "A Materno")]
        [JsonProperty("A_MATERNO")]
        public string A_MATERNO { get; set; }
        [Display(Name = "Nombres")]
        [JsonProperty("NOMBRES")]
        public string NOMBRES { get; set; }

        [Display(Name = "Encargado")]
        [JsonProperty("NTrabajador")]
        public string NTrabajador
        {
            get
            {
                string ntrabajador = A_PATERNO + " " + A_MATERNO + ", " + NOMBRES;
                return ntrabajador;
            }
            set
            {

            }
        }


        [Display(Name = "Responsable")]
        [JsonProperty("RESPONSABLE")]
        public string RESPONSABLE { get; set; }
        [Display(Name = "Personal")]
        [JsonProperty("PERSONAL")]
        public string PERSONAL { get; set; }

        [Display(Name = "cod_producto")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }
        public int fk_producto { get; set; }

        [Display(Name = "nom_producto")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }

        [Display(Name = "cod_sku")]
        [JsonProperty("cod_sku")]
        public string cod_sku { get; set; }

        [Display(Name = "descripcion_marca")]
        [JsonProperty("descripcion_marca")]
        public string descripcion_marca { get; set; }

        [Display(Name = "descripcion_producto_tipo")]
        [JsonProperty("descripcion_producto_tipo")]
        public string descripcion_producto_tipo { get; set; }
        [Display(Name = "cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        //Empresa
        [Display(Name = "ID")]
        [JsonProperty("id_empresa")]
        public int id_empresa { get; set; }
        [Display(Name = "Ruc")]
        [JsonProperty("ruc")]
        public string ruc { get; set; }
        [Display(Name = "Razon Social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }
        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }
        
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string email { get; set; }
        [Display(Name = "Contacto")]
        [JsonProperty("contacto")]
        public string contacto { get; set; }
        [Display(Name = "Telefono")]
        [JsonProperty("telefono")]
        public string telefono { get; set; }

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


        [Display(Name = "Dni")]
        [JsonProperty("dni")]
        public string dni { get; set; }

        [Display(Name = "Estado_persona")]
        [JsonProperty("estado_persona")]
        public string estado_persona { get; set; }

        //Vehiculo
        [Display(Name = "Id_vehiculo")]
        [JsonProperty("id_vehiculo")]
        public int id_vehiculo { get; set; }
        [Display(Name = "Fk_equipo")]
        [JsonProperty("fk_equipo")]
        public int fk_equipo { get; set; }
        [Display(Name = "Equipo")]
        [JsonProperty("equipo")]
        public string equipo { get; set; }
        [Display(Name = "Fk_tipo_equipo")]
        [JsonProperty("fk_tipo_equipo")]
        public int fk_tipo_equipo { get; set; }
        [Display(Name = "Tipo_equipo")]
        [JsonProperty("tipo_equipo")]
        public string tipo_equipo { get; set; }
        [Display(Name = "Fk_vehiculo_marca")]
        [JsonProperty("fk_vehiculo_marca")]
        public int fk_vehiculo_marca { get; set; }
        [Display(Name = "Marca")]
        [JsonProperty("marca")]
        public string marca { get; set; }
        [Display(Name = "Fk_carroceria")]
        [JsonProperty("fk_carroceria")]
        public int fk_carroceria { get; set; }
        [Display(Name = "Carroceria")]
        [JsonProperty("carroceria")]
        public string carroceria { get; set; }
        [Display(Name = "Modelo")]
        [JsonProperty("modelo")]
        public string modelo { get; set; }
        [Display(Name = "Serie")]
        [JsonProperty("serie")]
        public string serie { get; set; }
        [Display(Name = "Placa")]
        [JsonProperty("placa")]
        public string placa { get; set; }
        [Display(Name = "Nro Motor")]
        [JsonProperty("nro_motor")]
        public string nro_motor { get; set; }
        [Display(Name = "Año Fabricacion")]
        [JsonProperty("anio_fabricacion")]
        public int anio_fabricacion { get; set; }
        
       
    }
}