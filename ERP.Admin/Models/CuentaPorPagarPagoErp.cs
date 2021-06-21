using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class CuentaPorPagarPagoErp
    {
        public int id_cuentas_por_pagar_pago { get; set; }
        public int fk_cuentas_por_pagar { get; set; }

        public string fk_usuario { get; set; }
        public DateTime f_pago { get; set; }
        public string sfpago {
            get
            {
                string npago = f_pago.ToString("dd/MM/yyyy");
                
                return npago;
            }
            set {}
        }
        public string nro_operacion_pago { get; set; }
        public string nro_operacion { get; set; }
        public decimal monto { get; set; }
        public decimal monto_pago { get; set; }
        public string f_registro { get; set; }
        public string f_registro_pago { get; set; }
        public string estado { get; set; }
        public string IDUSUARIO { get; set; }
        public int fk_entidad { get; set; }
        public string razon_social { get; set; }
        public string IDMONEDA { get; set; }
        public string moneda { get; set; }
        public int fk_motivo_prestamo { get; set; }
        public DateTime f_operacion { get; set; }
        public DateTime f_vencimiento { get; set; }
        public string motivo_prestamo { get; set; }
        public string observacion { get; set; }
        public decimal saldo { get; set; }
        public string NOMBRE_CORTO { get; set; }
    }
}