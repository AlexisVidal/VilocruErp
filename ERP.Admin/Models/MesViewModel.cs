using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class MesViewModel
    {
        public int Id { get; set; }
        public string Ids
        {
            get
            {
                string idss = Id.ToString();
                string idsx = idss.PadLeft(2, '0');
                return idsx;
            }
            set
            {

            }
        }

        public string Mes
        {
            get
            {
                string smes = "";
                string idmes = Ids.ToString();
                switch (idmes)
                {
                    case "01":
                        smes = "ENERO";
                        break;
                    case "02":
                        smes = "FEBRERO";
                        break;
                    case "03":
                        smes = "MARZO";
                        break;
                    case "04":
                        smes = "ABRIL";
                        break;
                    case "05":
                        smes = "MAYO";
                        break;
                    case "06":
                        smes = "JUNIO";
                        break;
                    case "07":
                        smes = "JULIO";
                        break;
                    case "08":
                        smes = "AGOSTO";
                        break;
                    case "09":
                        smes = "SETIEMBRE";
                        break;
                    case "10":
                        smes = "OCTUBRE";
                        break;
                    case "11":
                        smes = "NOVIEMBRE";
                        break;
                    case "12":
                        smes = "DICIEMBRE";
                        break;
                }
                return smes;
            }
            set
            {

            }
        }

    }
}