using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class AsistenciaHorasErp
    {
        [Display(Name = "ITEM")]
        [JsonProperty("itemrecord")]
        public int itemrecord { get; set; }


        [Display(Name = "id_asistencia")]
        [JsonProperty("id_asistencia")]
        public int id_asistencia { get; set; }

        [Display(Name = "PERIODO")]
        [JsonProperty("PERIODO")]
        public string PERIODO { get; set; }

        [Display(Name = "DOCUMENTO")]
        [JsonProperty("IDCODIGOGENERAL")]
        public string IDCODIGOGENERAL { get; set; }

        [Display(Name = "NOMBRES")]
        [JsonProperty("NOMBRES")]
        public string NOMBRES { get; set; }

        [Display(Name = "01")]
        [JsonProperty("dia_01")]
        public string dia_01 { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s01_ht")]
        public decimal s01_ht	 { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s01_extra")]
        public decimal s01_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_01")]
        public decimal s25_porc_01  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_01")]
        public decimal s35_porc_01  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_01")]
        public decimal s100_porc_01     { get; set; }

        [Display(Name = "02")]
        [JsonProperty("dia_02")]
        public string dia_02  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s02_ht")]
        public decimal s02_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s02_extra")]
        public decimal s02_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_02")]
        public decimal s25_porc_02  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_02")]
        public decimal s35_porc_02  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_02")]
        public decimal s100_porc_02     { get; set; }

        [Display(Name = "03")]
        [JsonProperty("dia_03")]
        public string dia_03  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s03_ht")]
        public decimal s03_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s03_extra")]
        public decimal s03_extra { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_03")]
        public decimal s25_porc_03  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_03")]
        public decimal s35_porc_03  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_03")]
        public decimal s100_porc_03     { get; set; }

        [Display(Name = "04")]
        [JsonProperty("dia_04")]
        public string dia_04  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s04_ht")]
        public decimal s04_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s04_extra")]
        public decimal s04_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_04")]
        public decimal s25_porc_04  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_04")]
        public decimal s35_porc_04 { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_04")]
        public decimal s100_porc_04     { get; set; }

        [Display(Name = "05")]
        [JsonProperty("dia_05")]
        public string dia_05  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s05_ht")]
        public decimal s05_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s05_extra")]
        public decimal s05_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_05")]
        public decimal s25_porc_05  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_05")]
        public decimal s35_porc_05  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_05")]
        public decimal s100_porc_05     { get; set; }

        [Display(Name = "06")]
        [JsonProperty("dia_06")]
        public string dia_06  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s06_ht")]
        public decimal s06_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s06_extra")]
        public decimal s06_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_06")]
        public decimal s25_porc_06  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_06")]
        public decimal s35_porc_06  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_06")]
        public decimal s100_porc_06     { get; set; }

        [Display(Name = "07")]
        [JsonProperty("dia_07")]
        public string dia_07  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s07_ht")]
        public decimal s07_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s07_extra")]
        public decimal s07_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_07")]
        public decimal s25_porc_07  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_07")]
        public decimal s35_porc_07  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_07")]
        public decimal s100_porc_07     { get; set; }

        [Display(Name = "08")]
        [JsonProperty("dia_08")]
        public string dia_08  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s08_ht")]
        public decimal s08_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s08_extra")]
        public decimal s08_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_08")]
        public decimal s25_porc_08  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_08")]
        public decimal s35_porc_08  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_08")]
        public decimal s100_porc_08     { get; set; }

        [Display(Name = "09")]
        [JsonProperty("dia_09")]
        public string dia_09  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s09_ht")]
        public decimal s09_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s09_extra")]
        public decimal s09_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_09")]
        public decimal s25_porc_09  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_09")]
        public decimal s35_porc_09  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_09")]
        public decimal s100_porc_09     { get; set; }

        [Display(Name = "10")]
        [JsonProperty("dia_10")]
        public string dia_10  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s10_ht")]
        public decimal s10_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s10_extra")]
        public decimal s10_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_10")]
        public decimal s25_porc_10  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_10")]
        public decimal s35_porc_10  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_10")]
        public decimal s100_porc_10     { get; set; }

        [Display(Name = "11")]
        [JsonProperty("dia_11")]
        public string dia_11  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s11_ht")]
        public decimal s11_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s11_extra")]
        public decimal s11_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_11")]
        public decimal s25_porc_11  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_11")]
        public decimal s35_porc_11  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_11")]
        public decimal s100_porc_11     { get; set; }

        [Display(Name = "12")]
        [JsonProperty("dia_12")]
        public string dia_12  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s12_ht")]
        public decimal s12_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s12_extra")]
        public decimal s12_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_12")]
        public decimal s25_porc_12  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_12")]
        public decimal s35_porc_12  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_12")]
        public decimal s100_porc_12     { get; set; }

        [Display(Name = "13")]
        [JsonProperty("dia_13")]
        public string dia_13  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s13_ht")]
        public decimal s13_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s13_extra")]
        public decimal s13_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_13")]
        public decimal s25_porc_13  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_13")]
        public decimal s35_porc_13  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_13")]
        public decimal s100_porc_13     { get; set; }

        [Display(Name = "14")]
        [JsonProperty("dia_14")]
        public string dia_14  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s14_ht")]
        public decimal s14_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s14_extra")]
        public decimal s14_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_14")]
        public decimal s25_porc_14  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_14")]
        public decimal s35_porc_14  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_14")]
        public decimal s100_porc_14     { get; set; }

        [Display(Name = "15")]
        [JsonProperty("dia_15")]
        public string dia_15  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s15_ht")]
        public decimal s15_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s15_extra")]
        public decimal s15_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_15")]
        public decimal s25_porc_15  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_15")]
        public decimal s35_porc_15  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_15")]
        public decimal s100_porc_15     { get; set; }

        [Display(Name = "16")]
        [JsonProperty("dia_16")]
        public string dia_16  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s16_ht")]
        public decimal s16_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s16_extra")]
        public decimal s16_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_16")]
        public decimal s25_porc_16  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_16")]
        public decimal s35_porc_16  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_16")]
        public decimal s100_porc_16     { get; set; }

        [Display(Name = "17")]
        [JsonProperty("dia_17")]
        public string dia_17  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s17_ht")]
        public decimal s17_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s17_extra")]
        public decimal s17_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_17")]
        public decimal s25_porc_17  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_17")]
        public decimal s35_porc_17  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_17")]
        public decimal s100_porc_17     { get; set; }

        [Display(Name = "18")]
        [JsonProperty("dia_18")]
        public string dia_18  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s18_ht")]
        public decimal s18_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s18_extra")]
        public decimal s18_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_18")]
        public decimal s25_porc_18  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_18")]
        public decimal s35_porc_18  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_18")]
        public decimal s100_porc_18     { get; set; }

        [Display(Name = "19")]
        [JsonProperty("dia_19")]
        public string dia_19  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s19_ht")]
        public decimal s19_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s19_extra")]
        public decimal s19_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_19")]
        public decimal s25_porc_19  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_19")]
        public decimal s35_porc_19  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_19")]
        public decimal s100_porc_19     { get; set; }

        [Display(Name = "20")]
        [JsonProperty("dia_20")]
        public string dia_20  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s20_ht")]
        public decimal s20_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s20_extra")]
        public decimal s20_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_20")]
        public decimal s25_porc_20  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_20")]
        public decimal s35_porc_20  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_20")]
        public decimal s100_porc_20     { get; set; }

        [Display(Name = "21")]
        [JsonProperty("dia_21")]
        public string dia_21  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s21_ht")]
        public decimal s21_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s21_extra")]
        public decimal s21_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_21")]
        public decimal s25_porc_21  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_21")]
        public decimal s35_porc_21  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_21")]
        public decimal s100_porc_21     { get; set; }

        [Display(Name = "22")]
        [JsonProperty("dia_22")]
        public string dia_22  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s22_ht")]
        public decimal s22_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s22_extra")]
        public decimal s22_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_22")]
        public decimal s25_porc_22  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_22")]
        public decimal s35_porc_22  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_22")]
        public decimal s100_porc_22     { get; set; }

        [Display(Name = "23")]
        [JsonProperty("dia_23")]
        public string dia_23  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s23_ht")]
        public decimal s23_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s23_extra")]
        public decimal s23_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_23")]
        public decimal s25_porc_23  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_23")]
        public decimal s35_porc_23  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_23")]
        public decimal s100_porc_23     { get; set; }

        [Display(Name = "24")]
        [JsonProperty("dia_24")]
        public string dia_24  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s24_ht")]
        public decimal s24_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s24_extra")]
        public decimal s24_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_24")]
        public decimal s25_porc_24  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_24")]
        public decimal s35_porc_24  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_24")]
        public decimal s100_porc_24     { get; set; }

        [Display(Name = "25")]
        [JsonProperty("dia_25")]
        public string dia_25  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s25_ht")]
        public decimal s25_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s25_extra")]
        public decimal s25_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_25")]
        public decimal s25_porc_25  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_25")]
        public decimal s35_porc_25  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_25")]
        public decimal s100_porc_25     { get; set; }

        [Display(Name = "26")]
        [JsonProperty("dia_26")]
        public string dia_26  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s26_ht")]
        public decimal s26_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s26_extra")]
        public decimal s26_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_26")]
        public decimal s25_porc_26  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_26")]
        public decimal s35_porc_26  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_26")]
        public decimal s100_porc_26     { get; set; }

        [Display(Name = "27")]
        [JsonProperty("dia_27")]
        public string dia_27  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s27_ht")]
        public decimal s27_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s27_extra")]
        public decimal s27_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_27")]
        public decimal s25_porc_27  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_27")]
        public decimal s35_porc_27  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_27")]
        public decimal s100_porc_27     { get; set; }

        [Display(Name = "28")]
        [JsonProperty("dia_28")]
        public string dia_28  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s28_ht")]
        public decimal s28_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s28_extra")]
        public decimal s28_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_28")]
        public decimal s25_porc_28  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_28")]
        public decimal s35_porc_28  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_28")]
        public decimal s100_porc_28     { get; set; }

        [Display(Name = "29")]
        [JsonProperty("dia_29")]
        public string dia_29  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s29_ht")]
        public decimal s29_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s29_extra")]
        public decimal s29_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_29")]
        public decimal s25_porc_29  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_29")]
        public decimal s35_porc_29  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_29")]
        public decimal s100_porc_29     { get; set; }

        [Display(Name = "30")]
        [JsonProperty("dia_30")]
        public string dia_30  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s30_ht")]
        public decimal s30_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s30_extra")]
        public decimal s30_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_30")]
        public decimal s25_porc_30  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_30")]
        public decimal s35_porc_30  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_30")]
        public decimal s100_porc_30     { get; set; }

        [Display(Name = "31")]
        [JsonProperty("dia_31")]
        public string dia_31  { get; set; }

        [Display(Name = "HORAS")]
        [JsonProperty("s31_ht")]
        public decimal s31_ht   { get; set; }

        [Display(Name = "H. EXTRAS")]
        [JsonProperty("s31_extra")]
        public decimal s31_extra  { get; set; }

        [Display(Name = "25%")]
        [JsonProperty("s25_porc_31")]
        public decimal s25_porc_31  { get; set; }

        [Display(Name = "35%")]
        [JsonProperty("s35_porc_31")]
        public decimal s35_porc_31  { get; set; }

        [Display(Name = "100%")]
        [JsonProperty("s100_porc_31")]
        public decimal s100_porc_31     { get; set; }

        [Display(Name = "TOTAL HORAS")]
        [JsonProperty("total_horas")]
        public decimal total_horas  { get; set; }

        [Display(Name = "TOTAL H. EXTRAS")]
        [JsonProperty("total_extras")]
        public decimal total_extras { get; set; }

        [Display(Name = "TOTAL AL 25")]
        [JsonProperty("total_25")]
        public decimal total_25 { get; set; }

        [Display(Name = "TOTAL AL 35")]
        [JsonProperty("total_35")]
        public decimal total_35 { get; set; }

        [Display(Name = "TOTAL AL 100")]
        [JsonProperty("total_100")]
        public decimal total_100 { get; set; }

    }
}