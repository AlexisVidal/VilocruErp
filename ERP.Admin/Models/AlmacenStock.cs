using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class AlmacenStock
    {
        [Display(Name = "Id_almacen_stock")]
        [JsonProperty("id_almacen_stock")]
        public int id_almacen_stock { get; set; }

        [Display(Name = "Fk_almacen")]
        [JsonProperty("fk_almacen")]
        public int fk_almacen { get; set; }

        [Display(Name = "Fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "existencia")]
        [JsonProperty("existencia")]
        public decimal existencia { get; set; }

        [Display(Name = "pto_limite")]
        [JsonProperty("pto_limite")]
        public decimal pto_limite { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        //Alamcen
        [Display(Name = "Id_almacen")]
        [JsonProperty("id_almacen")]
        public int id_almacen { get; set; }

        [Display(Name = "Cod_almacen")]
        [JsonProperty("cod_almacen")]
        public string cod_almacen { get; set; }

        [Display(Name = "Nombre")]
        [JsonProperty("nombre")]
        public string nombre { get; set; }

        [Display(Name = "Ubicacion")]
        [JsonProperty("ubicacion")]
        public string ubicacion { get; set; }

        [Display(Name = "Estado_almacen")]
        [JsonProperty("estado_almacen")]
        public string estado_almacen { get; set; }

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
        public int fk_producto_subfamilia { get; set; }

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

        //Marca
        [Display(Name = "Id_producto_marca")]
        [JsonProperty("id_producto_marca")]
        public int id_producto_marca { get; set; }

        [Display(Name = "Descripcion_producto_marca")]
        [JsonProperty("descripcion_producto_marca")]
        public string descripcion_producto_marca { get; set; }

        [Display(Name = "Estado_producto_marca")]
        [JsonProperty("estado_producto_marca")]
        public string estado_producto_marca { get; set; }

        //Sub-Familia
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

        //Sub-Familia
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



        //OTROS
        public decimal cantidad_descontar { get; set; }

        public int fk_producto_tipo { get; set; }
        public string producto_tipo { get; set; }
        public string codigo_producto_linea { get; set; }
        public int fk_producto_linea { get; set; }
        public string producto_linea { get; set; }
        public string estado_linea { get; set; }
    }
}