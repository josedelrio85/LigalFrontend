using LigalFrontend.Models;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LigalFrontend.Functions
{
    public static class Functions
    {
        private static LigalEntities context = new LigalEntities();

        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            //The regular expression pattern[^\w\.@-] matches any character that is not a word character, a period, 
            //an @ symbol, or a hyphen.A word character is any letter, decimal digit, or punctuation connector such as an underscore.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                // If we timeout when replacing invalid characters, we should return Empty.
                return String.Empty;
            }
        }

        public static DateTime textoToFecha(string param, bool ultimaHora = false)
        {
            string fechaTexto = param.ToString();
            DateTime dIni;
            DateTime.TryParseExact(fechaTexto, "dd/MM/yyyy HH:mm", 
                new System.Globalization.CultureInfo("es-ES"),
                DateTimeStyles.None, 
                out dIni);
            if (ultimaHora)
                dIni = dIni.AddHours(23).AddMinutes(59).AddSeconds(59);
            
            return dIni;           
        }
        
        public static string getUserType()
        {
            string tipoUsuario = null;
            
            try
            {
                int userIdSession = Int32.Parse(HttpContext.Current.Session["LogedUserID"].ToString());

                if (userIdSession > 0)
                {
                    if (HttpContext.Current.Session["Role"] == null)
                    {
                        gen_usuarios usuario = context.gen_usuarios.Find(userIdSession);
                        tipoUsuario = usuario.USERTYPE;
                    }
                    else
                    {
                        tipoUsuario = HttpContext.Current.Session["Role"].ToString();
                    }
                }

                return tipoUsuario;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static int getUserId()
        {
            int userIdSession = Int32.Parse(HttpContext.Current.Session["LogedUserID"].ToString());
            if (userIdSession > 0)
            {
                return userIdSession;
            }
            return 0;
        }

        public static string Base64Encode(string text)
        {
            if(text == null)
            {
                return null;
            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            if(base64EncodedData == null)
            {
                return null;
            }
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string infoVersion()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            //return $"{version} ({buildDate})";
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            //string ver = fvi.FileVersion;

            return "version: " +  $"{version}";
        }
    }
}