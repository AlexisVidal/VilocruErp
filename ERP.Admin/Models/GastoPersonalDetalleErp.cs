using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class GastoPersonalDetalleErp
    {
        public int id_gasto_personal { get; set; }
        public string codigo { get; set; }
        public string IDUSUARIO { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public string NOMBRES { get; set; }
        public DateTime f_apertura { get; set; }
        public string sf_apertura
        {
            get
            {
                return f_apertura.ToString("dd/MM/yyyy");
            }
            set
            {
            }
        }
        public DateTime f_registro { get; set; }
        public string sf_registro
        {
            get
            {
                return f_registro.ToString("dd/MM/yyyy");
            }
            set
            {
            }
        }
        public decimal MONTO { get; set; }
        public string estado { get; set; }
        public string sestado
        {
            get
            {
                if (estado == "1")
                {
                    return "ACTIVA";
                }
                else
                {
                    return "ANULADA";
                }

            }
            set
            {
            }
        }


        public int id_gasto_personal_detalle { get; set; }
        public int fk_gasto_personal { get; set; }
        public DateTime f_operacion { get; set; }
        public string sf_operacion
        {
            get
            {
                return f_operacion.ToString("dd/MM/yyyy");
            }
            set
            {
            }
        }
        public decimal monto { get; set; }
        public decimal monto_detalle { get; set; }
        public string nro_operacion { get; set; }
        public string observacion { get; set; }
        public string estado_detalle { get; set; }
        public string  codigo_full { get; set; }

    }
}