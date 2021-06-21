using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ServicioTomadoErp
    {
        public int id_servicio_tomado { get; set; }
        public int fk_orden_servicio { get; set; }
        public string IDUSUARIO { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        public int fk_proyecto { get; set; }
        public int fk_empresa { get; set; }
        public int id_empresa { get; set; }


        public string ruc { get; set; }
        public string razon_social_compra { get; set; }
        public string direccion { get; set; }
        public string estado_empresa { get; set; }
        public string n_servicio { get; set; }
        public DateTime f_servicio { get; set; }
        public string df_servicio
        {
            get
            {
                if (f_servicio.ToString("dd/MM/yyyy") == "01/01/1901")
                {
                    return "";
                }
                else
                {
                    return f_servicio.ToString("dd/MM/yyyy");
                }
            }
            set
            {
            }
        }
        public string observacion { get; set; }
        public string estado { get; set; }
        public DateTime f_registro { get; set; }
        public string credito { get; set; }
        public string nro_factura { get; set; }
        public string IDMONEDA { get; set; }
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
        public string dcredito { get; set; }
        public string moneda { get; set; }



        public int id_servicio_tomado_detalle { get; set; }
        public int fk_servicio_tomado { get; set; }
        public int fk_tipo_afectacion_igv { get; set; }
        public string nombre_servicio { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
        public int total { get; set; }
        public string estado_detalle { get; set; }
        public string NEstadoDetalle
        {
            get
            {
                string nestado = "";
                if (estado_detalle == "1")
                {
                    nestado = "ACTIVA";
                }
                else if (estado_detalle == "0")
                {
                    nestado = "ANULADA";
                }
                return nestado;
            }
            set
            {
            }
        }
    }
}