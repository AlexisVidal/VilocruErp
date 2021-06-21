using ERP.Admin.App_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PalletsGrupales
    {
        public int Id { get; set; }
        public string Encriptado { get; set; }
        public string Ubicacion
        {
            get
            {
                string idubicacion = Encriptado.ToString();
                //string sidubicacion = QRHelper.DoInBackgroundEncode(idubicacion);
                string sidubicacion = "";
                return sidubicacion;
            }
            set
            {

            }
        }
        public string Fk_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Responsable
        {
            get
            {
                string responsable = "";
                responsable = Apellido + ", " + Nombre;
                return responsable;
            }
            set
            {

            }
        }
        public string IdProducto { get; set; }
        public string Producto { get; set; }
        public string Formato { get; set; }
        public string Empaque { get; set; }
        public int Bultos { get; set; }
        public DateTime FechaG { get; set; }
        public string Fecha
        {
            get
            {
                string fecha = "";
                if (FechaG.ToString() == "1/01/0001 00:00:00")
                {
                    fecha = "SIN DEFINIR";
                }
                else
                {
                    fecha = FechaG.ToString("g");
                }
                return fecha;
            }
            set
            {

            }
        }
        public string TipoProceso { get; set; }
        public string Proceso
        {
            get
            {
                string proceso = "";
                if (TipoProceso == "N")
                {
                    proceso = "NORMAL";
                }
                else if (TipoProceso == "R")
                {
                    proceso = "REEPROCESO";
                }
                else if (TipoProceso == "E")
                {
                    proceso = "REEMPAQUE";
                }
                else if (TipoProceso == "M")
                {
                    proceso = "MEZCLADO";
                }
                return proceso;
            }
            set
            {

            }
        }
        public string Fk_Camara { get; set; }
        public string Camara { get; set; }
        public Decimal Peso { get; set; }
        public string Statu { get; set; }
        public string Estado
        {
            get
            {
                string estado = "";
                if (Statu == "1")
                {
                    estado = "ACTIVO";
                }
                else
                {
                    estado = "INACTIVO";
                }
                return estado;
            }
            set
            {

            }
        }
        public int FkProductoFinal { get; set; }
    }
}