using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class ComprobanteVentaFullModel
    {
        [Display(Name = "Id_comprobante_venta")]
        [JsonProperty("id_comprobante_venta")]
        public int id_comprobante_venta { get; set; }

        [Display(Name = "Fk_comprobante_tipo")]
        [JsonProperty("fk_comprobante_tipo")]
        public int fk_comprobante_tipo { get; set; }

        [Display(Name = "Fk_venta")]
        [JsonProperty("fk_venta")]
        public int fk_venta { get; set; }

        [Display(Name = "Fk_caja")]
        [JsonProperty("fk_caja")]
        public int fk_caja { get; set; }

        [Display(Name = "Fk_medio_pago")]
        [JsonProperty("fk_medio_pago")]
        public int fk_medio_pago { get; set; }

        [Display(Name = "F_emision")]
        [JsonProperty("f_emision")]
        public string f_emision { get; set; }

        [Display(Name = "Nro_comprobante")]
        [JsonProperty("nro_comprobante")]
        public string nro_comprobante { get; set; }

        [Display(Name = "Total_bruto")]
        [JsonProperty("total_bruto")]
        public decimal total_bruto { get; set; }

        [Display(Name = "Total_igv")]
        [JsonProperty("total_igv")]
        public decimal total_igv { get; set; }

        [Display(Name = "Total_isc")]
        [JsonProperty("total_isc")]
        public decimal total_isc { get; set; }

        [Display(Name = "Total_neto")]
        [JsonProperty("total_neto")]
        public decimal total_neto { get; set; }

        [Display(Name = "saldo_bruto")]
        [JsonProperty("saldo_bruto")]
        public decimal saldo_bruto { get; set; }

        [Display(Name = "flg_cronograma")]
        [JsonProperty("flg_cronograma")]
        public string flg_cronograma { get; set; }

        [Display(Name = "Estado")]
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
                else if (estado == "1")
                {
                    nestado = "VIGENTE";
                }
                return nestado;
            }
            set
            {

            }
        }

        //Comprobante Tipo
        [Display(Name = "Id_comprobante_tipo")]
        [JsonProperty("id_comprobante_tipo")]
        public int id_comprobante_tipo { get; set; }

        [Display(Name = "Codigo")]
        [JsonProperty("codigo")]
        public string codigo { get; set; }

        [Display(Name = "Descripcion_comprobante_tipo")]
        [JsonProperty("descripcion_comprobante_tipo")]
        public string descripcion_comprobante_tipo { get; set; }

        [Display(Name = "Estado_comprobante_tipo")]
        [JsonProperty("estado_comprobante_tipo")]
        public string estado_comprobante_tipo { get; set; }

        //Venta
        [Display(Name = "Id_venta")]
        [JsonProperty("id_venta")]
        public int id_venta { get; set; }

        [Display(Name = "Fk_usuario_venta")]
        [JsonProperty("fk_usuario_venta")]
        public int fk_usuario_venta { get; set; }

        [Display(Name = "Fk_usuario_pago")]
        [JsonProperty("fk_usuario_pago")]
        public int fk_usuario_pago { get; set; }

        [Display(Name = "Fk_cliente")]
        [JsonProperty("fk_cliente")]
        public int fk_cliente { get; set; }

        [Display(Name = "Fk_pedido")]
        [JsonProperty("fk_pedido")]
        public int fk_pedido { get; set; }

        [Display(Name = "N_venta")]
        [JsonProperty("n_venta")]
        public string n_venta { get; set; }

        [Display(Name = "F_venta")]
        [JsonProperty("f_venta")]
        public string f_venta { get; set; }

        [Display(Name = "Estado_venta")]
        [JsonProperty("estado_venta")]
        public string estado_venta { get; set; }

        //Pedido
        [Display(Name = "Id_pedido")]
        [JsonProperty("id_pedido")]
        public int id_pedido { get; set; }

        [Display(Name = "Fk_cliente_pedido")]
        [JsonProperty("fk_cliente_pedido")]
        public int fk_cliente_pedido { get; set; }

        [Display(Name = "Fk_userventa")]
        [JsonProperty("fk_userventa")]
        public int fk_userventa { get; set; }

        [Display(Name = "N_pedido")]
        [JsonProperty("n_pedido")]
        public string n_pedido { get; set; }

        [Display(Name = "F_pedido")]
        [JsonProperty("f_pedido")]
        public string f_pedido { get; set; }

        [Display(Name = "Estado_pedido")]
        [JsonProperty("estado_pedido")]
        public string estado_pedido { get; set; }

        //Cliente
        [Display(Name = "Id_cliente")]
        [JsonProperty("id_cliente")]
        public int id_cliente { get; set; }

        [Display(Name = "Fk_cliente_tipo")]
        [JsonProperty("fk_cliente_tipo")]
        public int fk_cliente_tipo { get; set; }

        [Display(Name = "Estado_cliente")]
        [JsonProperty("estado_cliente")]
        public string estado_cliente { get; set; }

        //Cliente Tipo
        [Display(Name = "Id_cliente_tipo")]
        [JsonProperty("id_cliente_tipo")]
        public int id_cliente_tipo { get; set; }

        [Display(Name = "Descripcion_cliente_tipo")]
        [JsonProperty("descripcion_cliente_tipo")]
        public string descripcion_cliente_tipo { get; set; }

        [Display(Name = "Estado_cliente_tipo")]
        [JsonProperty("estado_cliente_tipo")]
        public string estado_cliente_tipo { get; set; }

        //Cliente Natural
        [Display(Name = "Id_cliente_natural")]
        [JsonProperty("id_cliente_natural")]
        public int id_cliente_natural { get; set; }

        [Display(Name = "Fk_persona_cliente_natural")]
        [JsonProperty("fk_persona_cliente_natural")]
        public int fk_persona_cliente_natural { get; set; }

        [Display(Name = "Fk_cliente_cliente_natural")]
        [JsonProperty("fk_cliente_cliente_natural")]
        public int fk_cliente_cliente_natural { get; set; }

        [Display(Name = "Ruc_cliente_natural")]
        [JsonProperty("ruc_cliente_natural")]
        public string ruc_cliente_natural { get; set; }

        [Display(Name = "Estado_cliente_natural")]
        [JsonProperty("estado_cliente_natural")]
        public string estado_cliente_natural { get; set; }

        //Persona Cliente NAtural
        [Display(Name = "Id_persona_cliente_natural")]
        [JsonProperty("id_persona_cliente_natural")]
        public int id_persona_cliente_natural { get; set; }

        [Display(Name = "Nombre_cliente_natural")]
        [JsonProperty("nombre_cliente_natural")]
        public string nombre_cliente_natural { get; set; }

        [Display(Name = "Apellido_paterno_cliente_natural")]
        [JsonProperty("apellido_paterno_cliente_natural")]
        public string apellido_paterno_cliente_natural { get; set; }

        [Display(Name = "Apellido_materno_cliente_natural")]
        [JsonProperty("apellido_materno_cliente_natural")]
        public string apellido_materno_cliente_natural { get; set; }

        [Display(Name = "F_nacimiento_cliente_natural")]
        [JsonProperty("f_nacimiento_cliente_natural")]
        public string f_nacimiento_cliente_natural { get; set; }

        [Display(Name = "Email_cliente_natural")]
        [JsonProperty("email_cliente_natural")]
        public string email_cliente_natural { get; set; }
        public string email_cliente_juridico { get; set; }

        [Display(Name = "Dni_cliente_natural")]
        [JsonProperty("dni_cliente_natural")]
        public string dni_cliente_natural { get; set; }

        [Display(Name = "Estado_persona_cliente_natural")]
        [JsonProperty("estado_persona_cliente_natural")]
        public string estado_persona_cliente_natural { get; set; }

        //Cliente Juridico
        [Display(Name = "Id_cliente_juridico")]
        [JsonProperty("id_cliente_juridico")]
        public int id_cliente_juridico { get; set; }

        [Display(Name = "Fk_empresa_cliente_juridico")]
        [JsonProperty("fk_empresa_cliente_juridico")]
        public int fk_empresa_cliente_juridico { get; set; }

        [Display(Name = "Fk_cliente_cliente_juridico")]
        [JsonProperty("fk_cliente_cliente_juridico")]
        public int fk_cliente_cliente_juridico { get; set; }

        [Display(Name = "Estado_cliente_juridico")]
        [JsonProperty("estado_cliente_juridico")]
        public string estado_cliente_juridico { get; set; }

        //Empresa Cliente Juridico
        [Display(Name = "Id_empresa_cliente_juridico")]
        [JsonProperty("id_empresa_cliente_juridico")]
        public int id_empresa_cliente_juridico { get; set; }

        [Display(Name = "Ruc_empresa_cliente_juridico")]
        [JsonProperty("ruc_empresa_cliente_juridico")]
        public string ruc_empresa_cliente_juridico { get; set; }

        [Display(Name = "Razon_social")]
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }

        [Display(Name = "Direccion_empresa")]
        [JsonProperty("direccion_empresa")]
        public string direccion_empresa { get; set; }

        [Display(Name = "Estado_empresa")]
        [JsonProperty("estado_empresa")]
        public string estado_empresa { get; set; }

        //Usuario Venta
        [Display(Name = "Id_usuario_venta")]
        [JsonProperty("id_usuario_venta")]
        public int id_usuario_venta { get; set; }

        [Display(Name = "Fk_trabajador_usuario_venta")]
        [JsonProperty("fk_trabajador_usuario_venta")]
        public int fk_trabajador_usuario_venta { get; set; }

        //Trabajador Usuario Venta
        [Display(Name = "Id_trabajador_usuario_venta")]
        [JsonProperty("id_trabajador_usuario_venta")]
        public int id_trabajador_usuario_venta { get; set; }

        [Display(Name = "fk_persona_usuario_venta")]
        [JsonProperty("fk_persona_usuario_venta")]
        public int fk_persona_usuario_venta { get; set; }

        [Display(Name = "Fk_tipo_trabajador_usuario_venta")]
        [JsonProperty("fk_tipo_trabajador_usuario_venta")]
        public int fk_tipo_trabajador_usuario_venta { get; set; }

        [Display(Name = "Estado_trabajador_usuario_venta")]
        [JsonProperty("estado_trabajador_usuario_venta")]
        public string estado_trabajador_usuario_venta { get; set; }

        //Persona Usuario Venta
        [Display(Name = "Id_persona_usuario_venta")]
        [JsonProperty("id_persona_usuario_venta")]
        public int id_persona_usuario_venta { get; set; }

        [Display(Name = "Nombre_usuario_venta")]
        [JsonProperty("nombre_usuario_venta")]
        public string nombre_usuario_venta { get; set; }

        [Display(Name = "Apellido_paterno_usuario_venta")]
        [JsonProperty("apellido_paterno_usuario_venta")]
        public string apellido_paterno_usuario_venta { get; set; }

        [Display(Name = "Apellido_materno_usuario_venta")]
        [JsonProperty("apellido_materno_usuario_venta")]
        public string apellido_materno_usuario_venta { get; set; }

        [Display(Name = "F_nacimiento_usuario_venta")]
        [JsonProperty("f_nacimiento_usuario_venta")]
        public string f_nacimiento_usuario_venta { get; set; }

        [Display(Name = "Email_usuario_venta")]
        [JsonProperty("email_usuario_venta")]
        public string email_usuario_venta { get; set; }

        [Display(Name = "Dni_usuario_venta")]
        [JsonProperty("dni_usuario_venta")]
        public string dni_usuario_venta { get; set; }

        [Display(Name = "Estado_persona_usuario_venta")]
        [JsonProperty("estado_persona_usuario_venta")]
        public string estado_persona_usuario_venta { get; set; }

        //Usuario Pago
        [Display(Name = "Id_usuario_pago")]
        [JsonProperty("id_usuario_pago")]
        public int id_usuario_pago { get; set; }

        [Display(Name = "Fk_trabajador_usuario_pago")]
        [JsonProperty("fk_trabajador_usuario_pago")]
        public int fk_trabajador_usuario_pago { get; set; }

        //Trabajador Usuario Pago
        [Display(Name = "Id_trabajador_usuario_pago")]
        [JsonProperty("id_trabajador_usuario_pago")]
        public int id_trabajador_usuario_pago { get; set; }

        [Display(Name = "Fk_persona_usuario_pago")]
        [JsonProperty("fk_persona_usuario_pago")]
        public int fk_persona_usuario_pago { get; set; }

        [Display(Name = "Fk_tipo_trabajador_usuario_pago")]
        [JsonProperty("fk_tipo_trabajador_usuario_pago")]
        public int fk_tipo_trabajador_usuario_pago { get; set; }

        [Display(Name = "Estado_trabajador_usuario_pago")]
        [JsonProperty("estado_trabajador_usuario_pago")]
        public string estado_trabajador_usuario_pago { get; set; }

        //Persona Usuario Pago
        [Display(Name = "Id_persona_usuario_pago")]
        [JsonProperty("id_persona_usuario_pago")]
        public int id_persona_usuario_pago { get; set; }

        [Display(Name = "Nombre_usuario_pago")]
        [JsonProperty("nombre_usuario_pago")]
        public string nombre_usuario_pago { get; set; }

        [Display(Name = "Apellido_paterno_usuario_pago")]
        [JsonProperty("apellido_paterno_usuario_pago")]
        public string apellido_paterno_usuario_pago { get; set; }

        [Display(Name = "Apellido_materno_usuario_pago")]
        [JsonProperty("apellido_materno_usuario_pago")]
        public string apellido_materno_usuario_pago { get; set; }

        [Display(Name = "F_nacimiento_usuario_pago")]
        [JsonProperty("f_nacimiento_usuario_pago")]
        public string f_nacimiento_usuario_pago { get; set; }

        [Display(Name = "Email_usuario_pago")]
        [JsonProperty("email_usuario_pago")]
        public string email_usuario_pago { get; set; }

        [Display(Name = "Dni_usuario_pago")]
        [JsonProperty("dni_usuario_pago")]
        public string dni_usuario_pago { get; set; }

        [Display(Name = "Estado_persona_usuario_pago")]
        [JsonProperty("estado_persona_usuario_pago")]
        public string estado_persona_usuario_pago { get; set; }

        //Persona Usuario Caja
        [Display(Name = "id_persona_usuario_caja")]
        [JsonProperty("id_persona_usuario_caja")]
        public int id_persona_usuario_caja { get; set; }

        [Display(Name = "nombre_usuario_caja")]
        [JsonProperty("nombre_usuario_caja")]
        public string nombre_usuario_caja { get; set; }

        [Display(Name = "apellido_paterno_usuario_caja")]
        [JsonProperty("apellido_paterno_usuario_caja")]
        public string apellido_paterno_usuario_caja { get; set; }

        [Display(Name = "apellido_materno_usuario_caja")]
        [JsonProperty("apellido_materno_usuario_caja")]
        public string apellido_materno_usuario_caja { get; set; }

        [Display(Name = "f_nacimiento_usuario_caja")]
        [JsonProperty("f_nacimiento_usuario_caja")]
        public string f_nacimiento_usuario_caja { get; set; }

        [Display(Name = "email_usuario_caja")]
        [JsonProperty("email_usuario_caja")]
        public string email_usuario_caja { get; set; }

        [Display(Name = "dni_usuario_caja")]
        [JsonProperty("dni_usuario_caja")]
        public string dni_usuario_caja { get; set; }

        [Display(Name = "estado_persona_usuario_caja")]
        [JsonProperty("estado_persona_usuario_caja")]
        public string estado_persona_usuario_caja { get; set; }

        //DEL MEDIO DE PAGO
        [Display(Name = "fk_usuario")]
        [JsonProperty("fk_usuario")]
        public int fk_usuario { get; set; }

        [Display(Name = "fk_tarjeta_credito")]
        [JsonProperty("fk_tarjeta_credito")]
        public int fk_tarjeta_credito { get; set; }

        [Display(Name = "fk_nota_credito")]
        [JsonProperty("fk_nota_credito")]
        public int fk_nota_credito { get; set; }

        [Display(Name = "fk_banco")]
        [JsonProperty("fk_banco")]
        public int fk_banco { get; set; }

        [Display(Name = "nro_documento")]
        [JsonProperty("nro_documento")]
        public string nro_documento { get; set; }

        //MEDIO DE PAGO
        [Display(Name = "descripcion_medio_pago")]
        [JsonProperty("descripcion_medio_pago")]
        public string descripcion_medio_pago { get; set; }

        //COMPROBANTE COMPRA COBRO
        [Display(Name = "monto")]
        [JsonProperty("monto")]
        public decimal monto { get; set; }
        public string sigla { get; set; }
        public string direccion_cliente_natural { get; set; }


        public string medio_pago { get; set; }
        public string tarjeta { get; set; }

        public string generico { get; set; }

        //Detalle venta
        [Display(Name = "Id_venta_detalle")]
        [JsonProperty("id_venta_detalle")]
        public int id_venta_detalle { get; set; }


        [Display(Name = "Fk_producto")]
        [JsonProperty("fk_producto")]
        public int fk_producto { get; set; }

        [Display(Name = "Fk_tipo_afectacion_igv")]
        [JsonProperty("fk_tipo_afectacion_igv")]
        public int fk_tipo_afectacion_igv { get; set; }

        [Display(Name = "Fk_tipo_isc")]
        [JsonProperty("fk_tipo_isc")]
        public int fk_tipo_isc { get; set; }

        [Display(Name = "Cantidad")]
        [JsonProperty("cantidad")]
        public decimal cantidad { get; set; }

        [Display(Name = "Precio")]
        [JsonProperty("precio")]
        public decimal precio { get; set; }
        
        [Display(Name = "Estado Venta Detalle")]
        [JsonProperty("estado_venta_detalle")]
        public string estado_venta_detalle { get; set; }

        public string NEstadoVentaDetalle
        {
            get
            {
                string nestadoventadetalle = "";
                if (estado_venta_detalle == "-1")
                {
                    nestadoventadetalle = "TODAS";
                }
                else if (estado_venta_detalle == "0")
                {
                    nestadoventadetalle = "ANULADA";
                }
                else if (estado_venta_detalle == "1")
                {
                    nestadoventadetalle = "VIGENTE";
                }
                return nestadoventadetalle;
            }
            set
            {

            }
        }

        //Producto
        [Display(Name = "Id_producto")]
        [JsonProperty("id_producto")]
        public int id_producto { get; set; }

        [Display(Name = "Fk_proveedor")]
        [JsonProperty("fk_proveedor")]
        public int fk_proveedor { get; set; }

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

        [Display(Name = "Embalaje")]
        [JsonProperty("embalaje")]
        public string embalaje { get; set; }

        [Display(Name = "Image_url")]
        [JsonProperty("image_url")]
        public string image_url { get; set; }

        [Display(Name = "Estado_producto")]
        [JsonProperty("estado_producto")]
        public string estado_producto { get; set; }

        //Marca
        [Display(Name = "Id_producto_marca")]
        [JsonProperty("id_producto_marca")]
        public int id_producto_marca { get; set; }

        [Display(Name = "Descripcion_marca")]
        [JsonProperty("descripcion_marca")]
        public string descripcion_marca { get; set; }

        [Display(Name = "Estado_producto_marca")]
        [JsonProperty("estado_producto_marca")]
        public string estado_producto_marca { get; set; }

        //SubFamilia
        [Display(Name = "Id_producto_subfamilia")]
        [JsonProperty("id_producto_subfamilia")]
        public int id_producto_subfamilia { get; set; }

        [Display(Name = "Fk_producto_familia")]
        [JsonProperty("fk_producto_familia")]
        public int fk_producto_familia { get; set; }

        [Display(Name = "Codigo Subfamilia")]
        [JsonProperty("codigo_subfamilia")]
        public string codigo_subfamilia { get; set; }

        [Display(Name = "Descripcion_producto_subfamilia")]
        [JsonProperty("descripcion_producto_subfamilia")]
        public string descripcion_producto_subfamilia { get; set; }

        [Display(Name = "estado_producto_subfamilia")]
        [JsonProperty("estado_producto_subfamilia")]
        public string estado_producto_subfamilia { get; set; }

        //Tipo de afectacion de igv
        [Display(Name = "Id_tipo_afectacion_igv")]
        [JsonProperty("id_tipo_afectacion_igv")]
        public int id_tipo_afectacion_igv { get; set; }

        [Display(Name = "Fk_igv")]
        [JsonProperty("fk_igv")]
        public int fk_igv { get; set; }

        [Display(Name = "Codigo_tipo_igv")]
        [JsonProperty("codigo_tipo_igv")]
        public string codigo_tipo_igv { get; set; }

        [Display(Name = "Descripcion_tipo_afectacion")]
        [JsonProperty("descripcion_tipo_afectacion")]
        public string descripcion_tipo_afectacion { get; set; }

        [Display(Name = "Flag_afecto_igv")]
        [JsonProperty("flag_afecto_igv")]
        public string flag_afecto_igv { get; set; }

        [Display(Name = "Flag_tipo_afecto")]
        [JsonProperty("flag_tipo_afecto")]
        public string flag_tipo_afecto { get; set; }

        //Igv
        [Display(Name = "Id_igv")]
        [JsonProperty("id_igv")]
        public int id_igv { get; set; }

        [Display(Name = "Codigo_igv")]
        [JsonProperty("codigo_igv")]
        public string codigo_igv { get; set; }

        [Display(Name = "Descripcion_igv")]
        [JsonProperty("descripcion_igv")]
        public string descripcion_igv { get; set; }

        [Display(Name = "Un_ece")]
        [JsonProperty("un_ece")]
        public string un_ece { get; set; }

        [Display(Name = "Porcentaje")]
        [JsonProperty("porcentaje")]
        public decimal porcentaje { get; set; }

        //Otros
        [Display(Name = "Str_distribuir")]
        [JsonProperty("str_distribuir")]
        public string str_distribuir { get; set; }

        //DESPACHO
        [Display(Name = "descripcion_comprobante")]
        [JsonProperty("descripcion_comprobante")]
        public string descripcion_comprobante { get; set; }



        public string FechaTexto
        {
            get
            {
                string fechaTexto = "";
                DateTime fechax = Convert.ToDateTime(f_emision);
                fechaTexto = fechax.ToString("d");
                return fechaTexto;
            }
            set
            {

            }

        }

        [Display(Name = "nombre_cliente")]
        [JsonProperty("nombre_cliente")]
        public string nombre_cliente { get; set; }

        [Display(Name = "cantidad_venta_detalle")]
        [JsonProperty("cantidad_venta_detalle")]
        public decimal cantidad_venta_detalle { get; set; }

        [Display(Name = "cantidad_movimiento")]
        [JsonProperty("cantidad_movimiento")]
        public decimal cantidad_movimiento { get; set; }

        [Display(Name = "nombre_almacen")]
        [JsonProperty("nombre_almacen")]
        public string nombre_almacen { get; set; }

        public string str_f_emision { get; set; }


        public string codigo_sunat { get; set; }

        public string cadena_para_codigo_qr { get; set; }
        public string enlace { get; set; }

        public decimal efectivo_recibido { get; set; }
        public decimal vuelto { get; set; }
        public string fk_moneda { get; set; }
        public string descripcion_moneda { get; set; }
    }
}