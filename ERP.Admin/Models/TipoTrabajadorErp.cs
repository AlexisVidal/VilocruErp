using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class TipoTrabajadorErp
    {
        public string IDTIPOTRABAJADOR { get; set; }
        public string DESCRIPCION { get; set; }
        public string GRATIFICACION { get; set; }
        public string DGRATIFICACION
        {
            get
            {
                if (GRATIFICACION == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public string CTS { get; set; }
        public string DCTS
        {
            get
            {
                if (CTS == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public string VACACIONES { get; set; }
        public string DVACACIONES
        {
            get
            {
                if (VACACIONES == "0")
                {
                    return "NO";
                }
                else
                {
                    return "SI";
                }
            }
            set
            {

            }
        }
        public int DIAS_VAC { get; set; }
        public int MINIMO_DIAS_VAC { get; set; }
        public string ESTADO { get; set; }
    }
}