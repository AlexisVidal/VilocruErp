using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PaletSaec
    {
        public int Id { get; set; }
        public string CamaraCongelamiento { get; set; }
        public string CamaraUbicacion
        {
            get
            {
                string camid = CamaraCongelamiento.ToString();
                int indexoi = 0;
                int? indexo = camid.IndexOf(".");
                if (indexo != null)
                {
                    indexoi = Convert.ToInt32(indexo);
                }
                string camids = "";
                switch (indexoi)
                {
                    case -1:
                        if (camid == "1")
                        {
                            camids = "PLANTA PRINCIPAL CAMARA 01";
                        }else if (camid == "2")
                        {
                            camids = "PLANTA PRINCIPAL CAMARA 02";
                        }
                        break;
                    default:
                        camids = "PLANTA DE HIELO";
                        break;
                }
                return camids;
            }
            set { }
        }
        public string Estante { get; set; }
        public decimal Altura { get; set; }
        public decimal Fondo { get; set; }
        public int Palet { get; set; }
        public decimal PBascula { get; set; }
        public decimal PBrutos { get; set; }
        public decimal PNetos { get; set; }
        public string Partida { get; set; }
    }
}