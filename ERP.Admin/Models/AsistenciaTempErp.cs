using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ERP.Admin.Models
{
    public class AsistenciaTempErp
    {
        public int itemrecord { get; set; }

        [Display(Name = "ID")]
        [JsonProperty("id_asistencia")]
        public string id_asistencia { get; set; }

        [Display(Name = "PERIODO")]
        [JsonProperty("PERIODO")]
        public string PERIODO { get; set; }

        [Display(Name = "DOCUMENTO")]
        [JsonProperty("IDCODIGOGENERAL")]
        public string IDCODIGOGENERAL { get; set; }

        [Display(Name = "NOMBRES")]
        [JsonProperty("NOMBRES")]
        public string NOMBRES { get; set; }

        [Display(Name = "01 I")]
        [JsonProperty("s01_ingreso")]
        public string s01_ingreso { get; set; }

        [Display(Name = "01 S")]
        [JsonProperty("s01_salida")]
        public string s01_salida { get; set; }
        public string s01_horas { get; set; }
        public string s01_ingresor { get; set; }
        public string s01_salidar { get; set; }
        public string s01_horasr { get; set; }
        public string s02_ingreso { get; set; }
        public string s02_salida { get; set; }
        public string s02_horas { get; set; }
        public string s02_ingresor { get; set; }
        public string s02_salidar { get; set; }
        public string s02_horasr { get; set; }
        public string s03_ingreso { get; set; }
        public string s03_salida { get; set; }
        public string s03_horas { get; set; }
        public string s03_ingresor { get; set; }
        public string s03_salidar { get; set; }
        public string s03_horasr { get; set; }
        public string s04_ingreso { get; set; }
        public string s04_salida { get; set; }
        public string s04_horas { get; set; }
        public string s04_ingresor { get; set; }
        public string s04_salidar { get; set; }
        public string s04_horasr { get; set; }
        public string s05_ingreso { get; set; }
        public string s05_salida { get; set; }
        public string s05_horas { get; set; }
        public string s05_ingresor { get; set; }
        public string s05_salidar { get; set; }
        public string s05_horasr { get; set; }
        public string s06_ingreso { get; set; }
        public string s06_salida { get; set; }
        public string s06_horas { get; set; }
        public string s06_ingresor { get; set; }
        public string s06_salidar { get; set; }
        public string s06_horasr { get; set; }
        public string s07_ingreso { get; set; }
        public string s07_salida { get; set; }
        public string s07_horas { get; set; }
        public string s07_ingresor { get; set; }
        public string s07_salidar { get; set; }
        public string s07_horasr { get; set; }
        public string s08_ingreso { get; set; }
        public string s08_salida { get; set; }
        public string s08_horas { get; set; }
        public string s08_ingresor { get; set; }
        public string s08_salidar { get; set; }
        public string s08_horasr { get; set; }
        public string s09_ingreso { get; set; }
        public string s09_salida { get; set; }
        public string s09_horas { get; set; }
        public string s09_ingresor { get; set; }
        public string s09_salidar { get; set; }
        public string s09_horasr { get; set; }
        public string s10_ingreso { get; set; }
        public string s10_salida { get; set; }
        public string s10_horas { get; set; }
        public string s10_ingresor { get; set; }
        public string s10_salidar { get; set; }
        public string s10_horasr { get; set; }
        public string s11_ingreso { get; set; }
        public string s11_salida { get; set; }
        public string s11_horas { get; set; }
        public string s11_ingresor { get; set; }
        public string s11_salidar { get; set; }
        public string s11_horasr { get; set; }
        public string s12_ingreso { get; set; }
        public string s12_salida { get; set; }
        public string s12_horas { get; set; }
        public string s12_ingresor { get; set; }
        public string s12_salidar { get; set; }
        public string s12_horasr { get; set; }
        public string s13_ingreso { get; set; }
        public string s13_salida { get; set; }
        public string s13_horas { get; set; }
        public string s13_ingresor { get; set; }
        public string s13_salidar { get; set; }
        public string s13_horasr { get; set; }
        public string s14_ingreso { get; set; }
        public string s14_salida { get; set; }
        public string s14_horas { get; set; }
        public string s14_ingresor { get; set; }
        public string s14_salidar { get; set; }
        public string s14_horasr { get; set; }
        public string s15_ingreso { get; set; }
        public string s15_salida { get; set; }
        public string s15_horas { get; set; }
        public string s15_ingresor { get; set; }
        public string s15_salidar { get; set; }
        public string s15_horasr { get; set; }
        public string s16_ingreso { get; set; }
        public string s16_salida { get; set; }
        public string s16_horas { get; set; }
        public string s16_ingresor { get; set; }
        public string s16_salidar { get; set; }
        public string s16_horasr { get; set; }
        public string s17_ingreso { get; set; }
        public string s17_salida { get; set; }
        public string s17_horas { get; set; }
        public string s17_ingresor { get; set; }
        public string s17_salidar { get; set; }
        public string s17_horasr { get; set; }
        public string s18_ingreso { get; set; }
        public string s18_salida { get; set; }
        public string s18_horas { get; set; }
        public string s18_ingresor { get; set; }
        public string s18_salidar { get; set; }
        public string s18_horasr { get; set; }
        public string s19_ingreso { get; set; }
        public string s19_salida { get; set; }
        public string s19_horas { get; set; }
        public string s19_ingresor { get; set; }
        public string s19_salidar { get; set; }
        public string s19_horasr { get; set; }
        public string s20_ingreso { get; set; }
        public string s20_salida { get; set; }
        public string s20_horas { get; set; }
        public string s20_ingresor { get; set; }
        public string s20_salidar { get; set; }
        public string s20_horasr { get; set; }
        public string s21_ingreso { get; set; }
        public string s21_salida { get; set; }
        public string s21_horas { get; set; }
        public string s21_ingresor { get; set; }
        public string s21_salidar { get; set; }
        public string s21_horasr { get; set; }
        public string s22_ingreso { get; set; }
        public string s22_salida { get; set; }
        public string s22_horas { get; set; }
        public string s22_ingresor { get; set; }
        public string s22_salidar { get; set; }
        public string s22_horasr { get; set; }
        public string s23_ingreso { get; set; }
        public string s23_salida { get; set; }
        public string s23_horas { get; set; }
        public string s23_ingresor { get; set; }
        public string s23_salidar { get; set; }
        public string s23_horasr { get; set; }
        public string s24_ingreso { get; set; }
        public string s24_salida { get; set; }
        public string s24_horas { get; set; }
        public string s24_ingresor { get; set; }
        public string s24_salidar { get; set; }
        public string s24_horasr { get; set; }
        public string s25_ingreso { get; set; }
        public string s25_salida { get; set; }
        public string s25_horas { get; set; }
        public string s25_ingresor { get; set; }
        public string s25_salidar { get; set; }
        public string s25_horasr { get; set; }
        public string s26_ingreso { get; set; }
        public string s26_salida { get; set; }
        public string s26_horas { get; set; }
        public string s26_ingresor { get; set; }
        public string s26_salidar { get; set; }
        public string s26_horasr { get; set; }
        public string s27_ingreso { get; set; }
        public string s27_salida { get; set; }
        public string s27_horas { get; set; }
        public string s27_ingresor { get; set; }
        public string s27_salidar { get; set; }
        public string s27_horasr { get; set; }
        public string s28_ingreso { get; set; }
        public string s28_salida { get; set; }
        public string s28_horas { get; set; }
        public string s28_ingresor { get; set; }
        public string s28_salidar { get; set; }
        public string s28_horasr { get; set; }
        public string s29_ingreso { get; set; }
        public string s29_salida { get; set; }
        public string s29_horas { get; set; }
        public string s29_ingresor { get; set; }
        public string s29_salidar { get; set; }
        public string s29_horasr { get; set; }
        public string s30_ingreso { get; set; }
        public string s30_salida { get; set; }
        public string s30_horas { get; set; }
        public string s30_ingresor { get; set; }
        public string s30_salidar { get; set; }
        public string s30_horasr { get; set; }
        public string s31_ingreso { get; set; }
        public string s31_salida { get; set; }
        public string s31_horas { get; set; }
        public string s31_ingresor { get; set; }
        public string s31_salidar { get; set; }
        public string s31_horasr { get; set; }

        public decimal s01_ht { get; set; }
        public decimal s02_ht { get; set; }
        public decimal s03_ht { get; set; }
        public decimal s04_ht { get; set; }
        public decimal s05_ht { get; set; }
        public decimal s06_ht { get; set; }
        public decimal s07_ht { get; set; }
        public decimal s08_ht { get; set; }
        public decimal s09_ht { get; set; }
        public decimal s10_ht { get; set; }
        public decimal s11_ht { get; set; }
        public decimal s12_ht { get; set; }
        public decimal s13_ht { get; set; }
        public decimal s14_ht { get; set; }
        public decimal s15_ht { get; set; }
        public decimal s16_ht { get; set; }
        public decimal s17_ht { get; set; }
        public decimal s18_ht { get; set; }
        public decimal s19_ht { get; set; }
        public decimal s20_ht { get; set; }
        public decimal s21_ht { get; set; }
        public decimal s22_ht { get; set; }
        public decimal s23_ht { get; set; }
        public decimal s24_ht { get; set; }
        public decimal s25_ht { get; set; }
        public decimal s26_ht { get; set; }
        public decimal s27_ht { get; set; }
        public decimal s28_ht { get; set; }
        public decimal s29_ht { get; set; }
        public decimal s30_ht { get; set; }
        public decimal s31_ht { get; set; }

        public string s01_irefrigerio { get; set; }
        public string s01_frefrigerio { get; set; }
        public decimal s01_refh { get; set; }

        public string s02_irefrigerio { get; set; }
        public string s02_frefrigerio { get; set; }
        public decimal s02_refh { get; set; }

        public string s03_irefrigerio { get; set; }
        public string s03_frefrigerio { get; set; }
        public decimal s03_refh { get; set; }

        public string s04_irefrigerio { get; set; }
        public string s04_frefrigerio { get; set; }
        public decimal s04_refh { get; set; }

        public string s05_irefrigerio { get; set; }
        public string s05_frefrigerio { get; set; }
        public decimal s05_refh { get; set; }

        public string s06_irefrigerio { get; set; }
        public string s06_frefrigerio { get; set; }
        public decimal s06_refh { get; set; }

        public string s07_irefrigerio { get; set; }
        public string s07_frefrigerio { get; set; }
        public decimal s07_refh { get; set; }

        public string s08_irefrigerio { get; set; }
        public string s08_frefrigerio { get; set; }
        public decimal s08_refh { get; set; }

        public string s09_irefrigerio { get; set; }
        public string s09_frefrigerio { get; set; }
        public decimal s09_refh { get; set; }

        public string s10_irefrigerio { get; set; }
        public string s10_frefrigerio { get; set; }
        public decimal s10_refh { get; set; }

        public string s11_irefrigerio { get; set; }
        public string s11_frefrigerio { get; set; }
        public decimal s11_refh { get; set; }

        public string s12_irefrigerio { get; set; }
        public string s12_frefrigerio { get; set; }
        public decimal s12_refh { get; set; }

        public string s13_irefrigerio { get; set; }
        public string s13_frefrigerio { get; set; }
        public decimal s13_refh { get; set; }

        public string s14_irefrigerio { get; set; }
        public string s14_frefrigerio { get; set; }
        public decimal s14_refh { get; set; }

        public string s15_irefrigerio { get; set; }
        public string s15_frefrigerio { get; set; }
        public decimal s15_refh { get; set; }

        public string s16_irefrigerio { get; set; }
        public string s16_frefrigerio { get; set; }
        public decimal s16_refh { get; set; }

        public string s17_irefrigerio { get; set; }
        public string s17_frefrigerio { get; set; }
        public decimal s17_refh { get; set; }

        public string s18_irefrigerio { get; set; }
        public string s18_frefrigerio { get; set; }
        public decimal s18_refh { get; set; }

        public string s19_irefrigerio { get; set; }
        public string s19_frefrigerio { get; set; }
        public decimal s19_refh { get; set; }

        public string s20_irefrigerio { get; set; }
        public string s20_frefrigerio { get; set; }
        public decimal s20_refh { get; set; }

        public string s21_irefrigerio { get; set; }
        public string s21_frefrigerio { get; set; }
        public decimal s21_refh { get; set; }

        public string s22_irefrigerio { get; set; }
        public string s22_frefrigerio { get; set; }
        public decimal s22_refh { get; set; }

        public string s23_irefrigerio { get; set; }
        public string s23_frefrigerio { get; set; }
        public decimal s23_refh { get; set; }

        public string s24_irefrigerio { get; set; }
        public string s24_frefrigerio { get; set; }
        public decimal s24_refh { get; set; }

        public string s25_irefrigerio { get; set; }
        public string s25_frefrigerio { get; set; }
        public decimal s25_refh { get; set; }

        public string s26_irefrigerio { get; set; }
        public string s26_frefrigerio { get; set; }
        public decimal s26_refh { get; set; }

        public string s27_irefrigerio { get; set; }
        public string s27_frefrigerio { get; set; }
        public decimal s27_refh { get; set; }

        public string s28_irefrigerio { get; set; }
        public string s28_frefrigerio { get; set; }
        public decimal s28_refh { get; set; }

        public string s29_irefrigerio { get; set; }
        public string s29_frefrigerio { get; set; }
        public decimal s29_refh { get; set; }

        public string s30_irefrigerio { get; set; }
        public string s30_frefrigerio { get; set; }
        public decimal s30_refh { get; set; }

        public string s31_irefrigerio { get; set; }
        public string s31_frefrigerio { get; set; }
        public decimal s31_refh { get; set; }

        public decimal s01_dia_horas { get; set; }
        public decimal s02_dia_horas { get; set; }
        public decimal s01_jornales { get; set; }
        public decimal s02_jornales { get; set; }

    }
}