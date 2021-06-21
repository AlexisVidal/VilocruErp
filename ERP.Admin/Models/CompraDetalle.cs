using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class CompraDetalle
    {
        [Display(Name = "Id_compra_detalle")]
        [JsonProperty("id_compra_detalle")]
        public int id_compra_detalle { get; set; }
        public int fk_proyecto { get; set; }

        [Display(Name = "Fk_compra")]
        [JsonProperty("fk_compra")]
        public int fk_compra { get; set; }

        [Display(Name = "Fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "Cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "Cantidad_recibida")]
        [JsonProperty("cantidad_recibida")]
        public decimal cantidad_recibida { get; set; }

        [Display(Name = "Precio")]
        [JsonProperty("precio")]
        public decimal precio { get; set; }

        [Display(Name = "Estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

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

        [Display(Name = "Fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

        [Display(Name = "n_compra")]
        [JsonProperty("n_compra")]
        public string n_compra { get; set; }

        [Display(Name = "f_compra")]
        [JsonProperty("f_compra")]
        public string f_compra { get; set; }

        [Display(Name = "agencia_transporte")]
        [JsonProperty("agencia_transporte")]
        public string agencia_transporte { get; set; }

        [Display(Name = "observacion")]
        [JsonProperty("observacion")]
        public string observacion { get; set; }

        [Display(Name = "N_orden")]
        [JsonProperty("n_orden")]
        public string n_orden { get; set; }

        [Display(Name = "F_orden")]
        [JsonProperty("f_orden")]
        public string f_orden { get; set; }

        [Display(Name = "Estado_orden_compra")]
        [JsonProperty("estado_orden_compra")]
        public string estado_orden_compra { get; set; }

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

        //Producto
        [Display(Name = "Id_producto")]
        [JsonProperty("id_producto")]
        public int id_producto { get; set; }

        [Display(Name = "Fk_proveedor_producto")]
        [JsonProperty("fk_proveedor_producto")]
        public int fk_proveedor_producto { get; set; }

        [Display(Name = "Fk_producto_marca")]
        [JsonProperty("fk_producto_marca")]
        public int fk_producto_marca { get; set; }

        [Display(Name = "Fk_producto_subfamilia")]
        [JsonProperty("fk_producto_subfamilia")]
        public decimal fk_producto_subfamilia { get; set; }

        [Display(Name = "Cod_producto")]
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }

        [Display(Name = "Nom_producto")]
        [JsonProperty("nom_producto")]
        public string nom_producto { get; set; }

        [Display(Name = "Codigo_sku")]
        [JsonProperty("codigo_sku")]
        public string codigo_sku { get; set; }

        [Display(Name = "Estado_producto")]
        [JsonProperty("estado_producto")]
        public string estado_producto { get; set; }

        //ProductoMarca
        [Display(Name = "Id_producto_marca")]
        [JsonProperty("id_producto_marca")]
        public int id_producto_marca { get; set; }

        [Display(Name = "Descripcion_producto_marca")]
        [JsonProperty("descripcion_producto_marca")]
        public string descripcion_producto_marca { get; set; }

        [Display(Name = "estado_producto_marca")]
        [JsonProperty("estado_producto_marca")]
        public string estado_producto_marca { get; set; }

        //ProductoSubFamilia
        [Display(Name = "Id_producto_subfamilia")]
        [JsonProperty("id_producto_subfamilia")]
        public int id_producto_subfamilia { get; set; }

        [Display(Name = "Fk_producto_familia")]
        [JsonProperty("fk_producto_familia")]
        public int fk_producto_familia { get; set; }

        [Display(Name = "Codigo_producto_subfamilia")]
        [JsonProperty("codigo_producto_subfamilia")]
        public string codigo_producto_subfamilia { get; set; }

        [Display(Name = "Descripcion_producto_subfamilia")]
        [JsonProperty("descripcion_producto_subfamilia")]
        public string descripcion_producto_subfamilia { get; set; }

        [Display(Name = "Estado_subfamilia")]
        [JsonProperty("estado_subfamilia")]
        public string estado_subfamilia { get; set; }

        public string descripcion_comprobante_tipo { get; set; }
        public string nro_comprobante { get; set; }

        //ProductoSubFamilia
        [Display(Name = "Id_producto_familia")]
        [JsonProperty("id_producto_familia")]
        public int id_producto_familia { get; set; }

        [Display(Name = "Codigo_producto_familia")]
        [JsonProperty("codigo_producto_familia")]
        public string codigo_producto_familia { get; set; }

        [Display(Name = "Descripcion_producto_familia")]
        [JsonProperty("descripcion_producto_familia")]
        public string descripcion_producto_familia { get; set; }

        [Display(Name = "Estado_familia")]
        [JsonProperty("estado_familia")]
        public string estado_familia { get; set; }

        
        public string razon_social_compra { get; set; }
        public string nombres { get; set; }
        public string A_PATERNO { get; set; }
        public string A_MATERNO { get; set; }
        public string NNombres
        {
            get
            {
                string nnomb = "";
                nnomb = nombres + " " + A_PATERNO + " " + A_MATERNO;
                return nnomb;
            }
            set
            {
            }
        }
    }
}