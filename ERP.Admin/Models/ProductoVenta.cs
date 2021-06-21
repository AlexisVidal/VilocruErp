using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoVenta
    {
        public int id_producto_venta { get; set; }
        public int fk_tipo_producto_venta { get; set; }
        public string codigo_tipo_producto { get; set; }
        public string codigo_afectacion_igv { get; set; }
        public string cod_producto { get; set; }
        public string codigo_sunat { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
    }
}
