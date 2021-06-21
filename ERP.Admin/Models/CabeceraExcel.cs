using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class CabeceraExcel
    {
        public string PROVEEDOR { get; set; }
        public string MARCA { get; set; }
        public string FAMILIA { get; set; }
        public string SUBFAMILIA { get; set; }
        public string SKU { get; set; }
        public string PRODUCTO { get; set; }
        public string UND { get; set; }
        public double EMPAQUE { get; set; }
        public double IDMONEDA { get; set; }
        public double PRECIOCOMPRA { get; set; }
        public double DPRIMERO { get; set; }
        public double PRIMERO { get; set; }
        public double DSEGUNDO { get; set; }
        public double SEGUNDO { get; set; }
        public double DTERCER { get; set; }
        public double TERCER { get; set; }
        public double DCUARTO { get; set; }
        public double CUARTO { get; set; }
        public double PRECIOFINAL { get; set; }
        public double TIPOCAMBIO { get; set; }
        public double PPVUNIDAD { get; set; }
        public double PVUNIDAD { get; set; }
        public double PPVMAYOR { get; set; }
        public double PVMAYOR { get; set; }
        public double PPVESPECIAL { get; set; }
        public double PVESPECIAL { get; set; }
        public double STOCKINICIAL { get; set; }
        public double IDALMACEN { get; set; }
    }
}