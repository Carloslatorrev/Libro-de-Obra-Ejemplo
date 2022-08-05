using LODapp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LODapp.Services
{
    public class InternetServices
    {
        public static string BaseUrl = "";
        public static string UrlPlataforma = "";
        public static string UrlPlataformaDescargar = "";
        public static string UrlRecoverPassword = "";
    #if DEBUG
        public static  HttpClientHandler insecureHandler = GetInsecureHandler();
        public static  HttpClient client = new HttpClient(insecureHandler);
    #else
        public static HttpClient client = new HttpClient();
    #endif


        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        static InternetServices()
        {
            client = new HttpClient() {
                BaseAddress = new Uri(BaseUrl)
            };
            client.Timeout = TimeSpan.FromSeconds(60);
            
        }

    }
}
