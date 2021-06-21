using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class CompraErp
    {
        public int id_compra { get; set; }
        public int fk_orden_compra { get; set; }
        public string IDUSUARIO { get; set; }
        public int fk_empresa { get; set; }
        public string n_compra { get; set; }
        public DateTime f_compra { get; set; }
        public string motivo_cierre { get; set; }
        public string estado { get; set; }
        public string nro_guia { get; set; }
        public int fk_proyecto { get; set; }
        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "1")
                {
                    nestado = "ACTIVA";
                }
                else if (estado == "0")
                {
                    nestado = "ANULADA";
                }
                return nestado;
            }
            set
            {
            }
        }
        public int id_empresa { get; set; }
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string direccion { get; set; }
        public string estado_empresa { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public string nombres { get; set; }
        public string A_PATERNO { get; set; }
        public string A_MATERNO { get; set; }
        public string agencia_transporte { get; set; }
        public string observacion { get; set; }
        
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

        public string proyecto { get; set; }
        public int fk_almacen { get; set; }
        public string almacen { get; set; }
        public string nro_factura  { get; set; }
        public string credito  { get; set; }
        public string dcredito  { get; set; }
        public string IDMONEDA  { get; set; }
        public string moneda { get; set; }

    }
}