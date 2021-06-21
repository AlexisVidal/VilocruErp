using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ERP.Admin.Models
{
    public class ComprobanteTraslado
    {
        [Display(Name = "id_comprobante_traslado")]
        [JsonProperty("id_comprobante_traslado")]
        public int id_comprobante_traslado { get; set; }

        [Display(Name = "fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "fk_solicitud_traslado")]
        [JsonProperty("fk_solicitud_traslado")]
        public int fk_solicitud_traslado { get; set; }

        [Display(Name = "fk_almacen_despacho")]
        [JsonProperty("fk_almacen_despacho")]
        public int fk_almacen_despacho { get; set; }

        [Display(Name = "NRO COMPROBANTE")]
        [JsonProperty("nro_comprobante")]
        public string nro_comprobante { get; set; }

        [Display(Name = "FEC. EMISIÓN")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if (estado == "-1")
                {
                    nestado = "TODOS";
                }
                else if (estado == "0")
                {
                    nestado = "ANULADO";
                }
                else if(estado == "1")
                {
                    nestado = "POR RECEPCIONAR";
                }
                else if(estado == "2")
                {
                    nestado = "RECEPCIONADO";
                }
                return nestado;
            }
            set { }
        }

        //COMPROBANTE TIPO
        [Display(Name = "id_comprobante_tipo")]
        [JsonProperty("id_comprobante_tipo")]
        public int id_comprobante_tipo { get; set; }

        [Display(Name = "codigo_comprobante_tipo")]
        [JsonProperty("codigo_comprobante_tipo")]
        public string codigo_comprobante_tipo { get; set; }

        [Display(Name = "COMPROBANTE")]
        [JsonProperty("descripcion_comprobante_tipo")]
        public string descripcion_comprobante_tipo { get; set; }

        [Display(Name = "estado_comprobante_tipo")]
        [JsonProperty("estado_comprobante_tipo")]
        public string estado_comprobante_tipo { get; set; }

        //SOLICITUD TRASLADO
        [Display(Name = "id_solicitud_traslado")]
        [JsonProperty("id_solicitud_traslado")]
        public int id_solicitud_traslado { get; set; }

        [Display(Name = "fk_almacen_solicita")]
        [JsonProperty("fk_almacen_solicita")]
        public int fk_almacen_solicita { get; set; }

        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "NRO SOLICITUD")]
        [JsonProperty("n_solicitud_traslado")]
        public string n_solicitud_traslado { get; set; }

        [Display(Name = "f_solicitud_traslado")]
        [JsonProperty("f_solicitud_traslado")]
        public string f_solicitud_traslado { get; set; }

        [Display(Name = "estado_solicitud_traslado")]
        [JsonProperty("estado_solicitud_traslado")]
        public string estado_solicitud_traslado { get; set; }

        //ALMACEN DESTINO
        [Display(Name = "id_almacen_destino")]
        [JsonProperty("id_almacen_destino")]
        public int id_almacen_destino { get; set; }

        [Display(Name = "CODIGO ALM. DESTINO")]
        [JsonProperty("cod_almacen_destino")]
        public string cod_almacen_destino { get; set; }

        [Display(Name = "NOMBRE ALM. DESTINO")]
        [JsonProperty("nombre_almacen_destino")]
        public string nombre_almacen_destino { get; set; }

        [Display(Name = "DIRECCIÓN ALM. DESTINO")]
        [JsonProperty("ubicacion_almacen_destino")]
        public string ubicacion_almacen_destino { get; set; }

        [Display(Name = "estado_almacen_destino")]
        [JsonProperty("estado_almacen_destino")]
        public string estado_almacen_destino { get; set; }

        //ALMACEN ORIGEN
        [Display(Name = "id_almacen_origen")]
        [JsonProperty("id_almacen_origen")]
        public int id_almacen_origen { get; set; }

        [Display(Name = "CODIGO ALM. ORIGEN")]
        [JsonProperty("cod_almacen_origen")]
        public string cod_almacen_origen { get; set; }

        [Display(Name = "NOMBRE ALM. ORIGEN")]
        [JsonProperty("nombre_almacen_origen")]
        public string nombre_almacen_origen { get; set; }

        [Display(Name = "DIRECCIÓN ALM. ORIGEN")]
        [JsonProperty("ubicacion_almacen_origen")]
        public string ubicacion_almacen_origen { get; set; }

        [Display(Name = "estado_almacen_origen")]
        [JsonProperty("estado_almacen_origen")]
        public string estado_almacen_origen { get; set; }
    }
}