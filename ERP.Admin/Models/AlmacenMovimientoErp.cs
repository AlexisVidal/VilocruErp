using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class AlmacenMovimientoErp
    {
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
        [Display(Name = "Direccion")]
        [JsonProperty("direccion")]
        public string direccion { get; set; }
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

    }
}