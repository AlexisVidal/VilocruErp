using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalContratoErp
    {
        public int id_contrato { get; set; }
        public int fk_proyecto { get; set; }
        //public string IDCONTRATO { get; set; }
        public string IDCODIGOGENERAL { get; set; }
        //public DateTime INICIO_ADDENDUM { get; set; }
        //public string dINICIO_ADDENDUM
        //{
        //    get
        //    {
        //        if (INICIO_ADDENDUM.ToString("dd/MM/yyyy").Equals("01/01/1901"))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return INICIO_ADDENDUM.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    set { }
        //}
        //public DateTime FINAL_ADDENDUM { get; set; }
        //public string dFINAL_ADDENDUM
        //{
        //    get
        //    {
        //        if (FINAL_ADDENDUM.ToString("dd/MM/yyyy").Equals("01/01/1901"))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return FINAL_ADDENDUM.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    set { }
        //}
        public string ESTADO { get; set; }
        public string DESTADO { get; set; }
        //public string APODERADO { get; set; }
        //public string APODERADO_DIRECC { get; set; }
        //public string APODERADO_DOCIDENT { get; set; }
        public decimal BASICO { get; set; }
        public decimal BONIFICACION { get; set; }
        public decimal MOVILIDAD { get; set; }
        public decimal OTROS { get; set; }

        //public string CONTRATO_ADDEMDUM { get; set; }
        
        //public string CONTRATO_PRORROGA { get; set; }
        public int DURACION { get; set; }
        public DateTime INICIO_CONTRATO { get; set; }
        public string dINICIO_CONTRATO
        {
            get
            {
                if (INICIO_CONTRATO.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return INICIO_CONTRATO.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public DateTime FINAL_CONTRATO { get; set; }
        public string dFINAL_CONTRATO
        {
            get
            {
                if (FINAL_CONTRATO.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return FINAL_CONTRATO.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public int PERIODO_PRUEBA { get; set; }
        //public DateTime INICIO_PRORROGA { get; set; }
        //public string dINICIO_PRORROGA
        //{
        //    get
        //    {
        //        if (INICIO_PRORROGA.ToString("dd/MM/yyyy").Equals("01/01/1901"))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return INICIO_PRORROGA.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    set { }
        //}
        //public DateTime FIN_PRORROGA { get; set; }
        //public string dFIN_PRORROGA
        //{
        //    get
        //    {
        //        if (FIN_PRORROGA.ToString("dd/MM/yyyy").Equals("01/01/1901"))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return FIN_PRORROGA.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    set { }
        //}
        public DateTime FECHACREACION { get; set; }
        public string dFECHACREACION
        {
            get
            {
                if (FECHACREACION.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return FECHACREACION.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public string IDTIPOCONTRATO { get; set; }
        //public DateTime FIRMA_CONVENIO { get; set; }
        //public string dFIRMA_CONVENIO
        //{
        //    get
        //    {
        //        if (FIRMA_CONVENIO.ToString("dd/MM/yyyy").Equals("01/01/1901"))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return FIRMA_CONVENIO.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    set { }
        //}
        public DateTime FIRMA_CONTRATO { get; set; }
        public string dFIRMA_CONTRATO
        {
            get
            {
                if (FIRMA_CONTRATO.ToString("dd/MM/yyyy").Equals("01/01/1901"))
                {
                    return "";
                }
                else
                {
                    return FIRMA_CONTRATO.ToString("dd/MM/yyyy");
                }
            }
            set { }
        }
        public string FIRMA_FISICA { get; set; }

        public string DFIRMA_FISICA
        {
            get
            {
                string dfimafisica = "NO";
                if (FIRMA_FISICA =="1")
                {
                    dfimafisica = "SI";
                }

                return dfimafisica;
            }
            set { }
        }
        public string IDPLANILLA { get; set; }
        public string NOMBRES { get; set; }
        public string TIPOCONTRATO { get; set; }
        public string PLANILLA { get; set; }
        public string CODIGOGENERALREGISTRO { get; set; }
    }
}