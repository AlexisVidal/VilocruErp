﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class UnidadMedidaErp
    {
        public int id_unidad_medida { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string abreviatura { get; set; }
    }
}