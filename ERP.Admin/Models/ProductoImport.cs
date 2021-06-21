using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ProductoImport
    {
        public string PROVEEDOR { get; set; }
        public int IDPROVEEDOR { get; set; }
        public string MARCA { get; set; }
        public int IDMARCA { get; set; }
        public string FAMILIA { get; set; }
        public int IDFAMILIA { get; set; }
        public string SUBFAMILIA { get; set; }
        public int IDSUBFAMILIA { get; set; }
        public string SKU { get; set; }
        public string PRODUCTO { get; set; }
        public string UND { get; set; }
        public string EMPAQUE { get; set; }
        public int IDTIPOMONEDA { get; set; }

        public decimal PRECIOCOMPRA { get; set; }
        public decimal DSCTOUNO { get; set; }
        public decimal DSCTODOS { get; set; }
        public decimal DSCTOTRES { get; set; }
        public decimal DSCTOCUATRO { get; set; }
        public decimal PRECIOCOMPRAFINAL { get; set; }

        public decimal DTIPOCAMBIO { get; set; }
        public decimal DPPVUNIDAD { get; set; }
        public decimal DPVUNIDAD { get; set; }
        public decimal DPPVMAYOR { get; set; }
        public decimal DPVMAYOR { get; set; }
        public decimal DPPVESPECIAL { get; set; }
        public decimal DPVESPECIAL { get; set; }

        public decimal DSTOCKINICIAL { get; set; }
        public int DIDALMACEN { get; set; }
    }
}