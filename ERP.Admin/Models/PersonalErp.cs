using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Admin.Models
{
    public class PersonalErp
    {
        public string IDCODIGOGENERAL { get; set; }
        public string ESTADO { get; set; }
        public string ASIGNACION { get; set; }
        public string A_MATERNO { get; set; }
        public string A_PATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string CODIGO_CONTROL { get; set; }
        public string OBSERVACION { get; set; }
        public string PERSONAL_FOTO { get; set; }
        public string PERSONAL_FIRMA { get; set; }
        public string SEXO { get; set; }
        public string SINCRONIZA { get; set; }
        public string TIPO_CUENTABANCO { get; set; }
        public string AUTOGENERADOAFP { get; set; }
        public string CUENTA_BANCO { get; set; }
        public string CUENTA_CTS { get; set; }
        public string DIRECCION_INTERIOR { get; set; }
        public int DIRECCION_NUMERO { get; set; }
        public string DIRECCION_REFERENCIA { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public DateTime INICIO_AFP { get; set; }
        public DateTime INICIO_ONP { get; set; }
        public string AUTOGENERADOIPSS { get; set; }
        public string NRODOCUMENTO { get; set; }
        public string REGIMEN_PENSION { get; set; }
        public string TELEFONO { get; set; }
        public string TELEFONO2 { get; set; }
        public string CELULAR { get; set; }
        public string EMAIL { get; set; }
        public string DESCRIPCION_VIA { get; set; }
        public string DESCRIPCION_ZONA { get; set; }
        public string FECHACREACION { get; set; }
        public string IDAFP { get; set; }
        public string AFP { get; set; }
        public string IDNACIONALIDAD { get; set; }
        public string IDLUGARNAC { get; set; }
        public string IDDOCIDENTIDAD { get; set; }
        public string DOCIDENTIDAD { get; set; }
        public string IDZONA { get; set; }
        public string IDESTADOCIVIL { get; set; }
        public string ESTADOCIVIL { get; set; }

        public string IDEPS { get; set; }
        public string IDVIA { get; set; }
        public string IDBANCOCTS { get; set; }
        public string IDBANCO { get; set; }
        public string IDUBIGEO { get; set; }

        public string IDMONEDABANCO { get; set; }
        public string IDMONEDACTS { get; set; }
        public string IDPTOGENCODIGO { get; set; }
        public string ESSALUD_VIDA { get; set; }
        public string LISTA_NEGRA { get; set; }
        public string IDCATEGORIA { get; set; }
        public string TRABAJADORPENSIONISTA { get; set; }
        public string IDTIPOPENSION { get; set; }
        public string IDCLIEPROV { get; set; }
        public string REGIMENLABORAL { get; set; }
        public string SCTRSALUD { get; set; }
        public string SCTRPENSION { get; set; }
        public string IDSITUACIONEPS { get; set; }
        public string IDREGIMEN { get; set; }
        public string DISCAPACITADO { get; set; }
        public string TIPOREMUNERACION { get; set; }
        public string CONTROLINMEDIATO { get; set; }
        public string SINDICALIZADO { get; set; }
        public string DOMICILIADO { get; set; }
        public string RUC { get; set; }
        public string IDNIVELESTUDIO { get; set; }
        public string NIVELESTUDIO { get; set; }
        public string IDMUNICIPALIDAD { get; set; }
        public string SNOCTURNO { get; set; }

        public string IDPERIODICIDAD { get; set; }
        public string IDPAGOPLA { get; set; }
        public string IDMOTIVCESE { get; set; }
        public string RQUINTAEX { get; set; }
        public string SESPECIAL { get; set; }
        public string OIQUINTACAT { get; set; }
        public string IDTIPOPENSIONP { get; set; }
        public string IDREGIMENP { get; set; }
        public DateTime FECHA_RP { get; set; }

        public string AUTOGENERADOAFPP { get; set; }
        public string IDSITUACIONP { get; set; }
        public string IDPAGOPLAP { get; set; }
        public string IDMOTIVCESEP { get; set; }
        public string SPRACTICANTE { get; set; }
        public string IMADRERESP { get; set; }
        public string ICFPRO { get; set; }
        public string IDMFORMATIVA { get; set; }
        public string SNP { get; set; }

        public string licencia_auto { get; set; }
        public string licencia_moto { get; set; }
        public string otros { get; set; }

        public bool bSCTRSALUD
        {
            get
            {
                bool bscrt = false;
                if (SCTRSALUD =="1")
                {
                    bscrt = true;
                }

                return bscrt;
            }
            set
            {

            }
        }

        public string NEstado
        {
            get
            {
                string nestado = "";
                if (ESTADO == "1")
                {
                    nestado = "ACTIVO";
                }
                else
                {
                    nestado = "INACTIVO";
                }
                
                return nestado;
            }
            set
            {

            }
        }

        public string Nsexo
        {
            get
            {
                string nsexo = "";
                if (SEXO == "M")
                {
                    nsexo = "MASCULINO";
                }
                else
                {
                    nsexo = "FEMENINO";
                }

                return nsexo;
            }
            set
            {

            }
        }
        public string DIRECCION_DEPARTAMENTO { get; set; }
        public string DIRECCION_MANZANA { get; set; }
        public string DIRECCION_ETAPA { get; set; }
        public string DIRECCION_BLOCK { get; set; }
        public string DIRECCION_LOTE { get; set; }
        public string DIRECCION_KILOMETRO { get; set; }

        public string direccion_domicilio { get; set; }
        public string IDDEPARTAMENTO { get; set; }
        public string IDPROVINCIA { get; set; }
        public string IDDISTRITO { get; set; }
        public string ESPECIALIDAD { get; set; }
        public string CENTROESTUDIOS { get; set; }

        public int ANIO_EGRESO { get; set; }

        public string NACIONALIDAD { get; set; }
        public int ITEM { get; set; }

        public string NOMBRES_FULL { get; set; }

        public int PRESTAMO { get; set; }
        public int CONTRATO { get; set; }
        public int seguimientos { get; set; }
        public string estado_salud { get; set; }

        //                     SMEDICO IDCATFORMATIVA JORNADA_MAXIMA  REGIMEN_ATIPICO ASEGURA_PENSION CATEGORIA_OCUPACIONAL IDCONVENIO  RENUNCIA_SINDICATO PROCEDENCIA IDACTIVIDAD IDLABOR IDCCOSTO IDREGIMENLABORAL     IDTIPOREGISTRO IDREGIMENASEG_SALUD IDCLIEPROV_TERCERO idpaisemisor    idvia2 direccion_interior2 descripcion_via2 direccion_numero2   DIRECCION_DEPARTAMENTO2 DIRECCION_MANZANA2  DIRECCION_LOTE2 DIRECCION_KILOMETRO2    direccion_block2 direccion_etapa2    idzona2 DESCRIPCION_ZONA2   direccion_referencia2 idubigeo2   centroasistencial idldn   fecha_aperturabanco fecha_aperturacts   edad CUENTA_MODULO   CUENTA_SUBCUENTA CUENTA_SUCURSAL MIXTA SENATI  IDBASEDATOS IDINSTITUCION    IDCARRERA    REGIMEN_EDU EDUCACION_COMPLETA PRODUCCION_PESQUERA CARGO_PESQUERA personalexterno
        //42738236	1	NO GONZALES    VIDAL JUAN ALEXIS	42738236	NULL M   P	4					0		1962-06-01 00:00:00.000	2009-07-17 00:00:00.000	1900-01-01 00:00:00.000	6206011FNCDL005	00834142	 					28 DE JULIO		2017-02-22 09:51:34.000	PRI PER	130403	01	12	SO	    	03	    	BCP 	130403	01	  	   	NO NO	1	0	NULL P	0	0	  	24	0	 	0	0	1	           	007	      	0	3	2	  	0	0	0	 	  	1900-01-01 00:00:00.000		  	 	  	0	0	0	  	1	0	1	0	0	02	 	0		   	      	            	18	    	    	    	    	    	    	 	  	           	604	  	    		    	    	    	    	    	    	    	  			      	1	  	2016-11-24 00:00:00.000	1900-01-01 00:00:00.000	0				1	0					0	 	0	0	  	0
    }
}