using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class AnioViewModel
    {
        public int Anio { get; set; }
        public string AnioS
        {
            get
            {
                string anios = Anio.ToString();
                return anios;
            }
            set
            {

            }
        }
    }
}