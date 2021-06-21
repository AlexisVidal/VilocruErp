using ERP.Admin.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;

namespace ERP.Admin.App_Helper
{
    public static class QRHelper
    {
        public static IHtmlString GenerateQrCode(this HtmlHelper html, string url, string alt = "QR code", int height = 300, int width = 300, int margin = 0)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;
            qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
                }
            }
        }
        public static IHtmlString GenerateQrCode3(this HtmlHelper html, string url, string alt = "QR code", int height = 200, int width = 200, int margin = 1)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;
            qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
                }
            }
        }
        public static IHtmlString GenerateQrCode2(this HtmlHelper html, string url, string alt = "QR code", int height = 200, int width = 200, int margin = 0)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;
            qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
                }
            }
        }
        public static String GetUbicacion(String encriptado)
        {
            try
            {
                string codificado = encriptado.Trim();
                //
                int tamanio = codificado.Length;
                codificado = codificado.Replace('/', '-');//NO MODIFICAR ORDEN
                codificado = codificado.Replace('=', '?');//NO MODIFICAR ORDEN
                char z = codificado[tamanio - 1];
                if (z == '?' || z == '¡')
                {
                    //codificado = codificado.Substring(0, codificado.Length - 1) + "=";
                    codificado = codificado.Replace('?', '¿');
                    codificado = codificado.Replace('+', '¡');

                }
                else
                {
                    codificado = codificado.Replace('=', '¡');
                    codificado = codificado.Replace('=', '?');
                }
                //
                string ubicacions = "SIN UBICAR";

                if (codificado != "")
                {
                    using (DataSqlDataContext dtsql = new DataSqlDataContext())
                    {
                        var ubicx = (from s in dtsql.sp_muestra_ubicacion_saec(codificado)
                                     select s.Ubicacion).ToList();
                        if (ubicx.Count > 0)
                        {
                            ubicacions = ubicx[0].ToString();
                        }
                    }
                }

                return ubicacions;
            }
            catch (Exception ex)
            {
                return "SIN UBICAR";
            }
        }

        public static String DoInBackgroundEncode(String idgrupal)
        {
            try
            {
                string idgeneradogrupal = "G" + idgrupal.ToString().PadLeft(20, '0');

                String link = "http://192.168.10.106/proancosys/movil/qr/ajax/ajax_set_qr.php";
                String data = HttpUtility.UrlEncode("contenido", Encoding.UTF8) + "=" + HttpUtility.UrlEncode(idgeneradogrupal, Encoding.UTF8);
                //SOLICITUD
                data += "&" + HttpUtility.UrlEncode("solicitud", Encoding.UTF8) + "=" + HttpUtility.UrlEncode("encrypt_qr", Encoding.UTF8);

                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(link);
                myHttpWebRequest.Method = "POST";

                byte[] datas = Encoding.ASCII.GetBytes(data);

                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.ContentLength = data.Length;

                Stream requestStream = myHttpWebRequest.GetRequestStream();
                requestStream.Write(datas, 0, data.Length);
                requestStream.Close();

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                Stream responseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8);

                string pageContent = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();

                string codificado = pageContent.Trim();
                //
                int tamanio = codificado.Length;
                codificado = codificado.Replace('/', '-');//NO MODIFICAR ORDEN
                codificado = codificado.Replace('=', '?');//NO MODIFICAR ORDEN
                char z = codificado[tamanio - 1];
                if (z == '?' || z == '¡')
                {
                    //codificado = codificado.Substring(0, codificado.Length - 1) + "=";
                    codificado = codificado.Replace('?', '¿');
                    codificado = codificado.Replace('+', '¡');

                }
                else
                {
                    codificado = codificado.Replace('=', '¡');
                    codificado = codificado.Replace('=', '?');
                }
                //
                string ubicacions = "SIN UBICAR";

                if (codificado != "")
                {
                    using (DataSqlDataContext dtsql = new DataSqlDataContext())
                    {
                        var ubicx = (from s in dtsql.sp_muestra_ubicacion_saec(codificado)
                                     select s.Ubicacion).ToList();

                        //select s.Ubicacion.ToList();
                        if (ubicx.Count > 0)
                        {
                            ubicacions = ubicx[0].ToString();
                        }
                    }
                }

                return ubicacions;
                //return codificado;
            }
            catch (Exception e)
            {
                return "SIN UBICAR";
            }
        }

    }
}