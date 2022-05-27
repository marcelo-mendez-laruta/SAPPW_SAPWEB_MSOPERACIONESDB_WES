using BCP.Framework.Common;
using BCP.Sap.Models.Autorizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Connectors
{
    public class ConnectorSwampCore
    {

        private ConfiguracionWebAPI _configuracion;
        private string _endPoint;
        public ConnectorSwampCore(ConfiguracionWebAPI configuracion)
        {
            this._configuracion = configuracion;
        }

        public T consultaPOSTSwampCore<T>(string metodo, object request,string auth, string canal,string token,string appUserId)
        {
            T response;
            try
            {
                this._endPoint  = this._configuracion.url + metodo;

                var authHeader = AuthenticationHeaderValue.Parse(auth);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] autorizados = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
                httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(autorizados[0] + ":" + autorizados[1])));
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("CorrelationId", ManagerOperation.GenerateOperation());
                    httpClient.DefaultRequestHeaders.Add("Channel", canal);
                    httpClient.DefaultRequestHeaders.Add("PublicToken", token);
                    httpClient.DefaultRequestHeaders.Add("AppUserId", appUserId);
                    httpClient.DefaultRequestHeaders.Add("api-version", "1.0");

                    var json = ManagerJson.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = httpClient.PostAsync(this._endPoint, content).Result;
                    var resultContent = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {                       
                        response = ManagerJson.Deserialize<T>(resultContent);
                    }
                    else{
                        response= ManagerJson.Deserialize<T>("{\"operation\":\"" + DateTime.Now.ToString("yyyyMMddhhmmss") + "\"  ,\"state\": \"" + (int)result.StatusCode + "\",\"message\": \"" + result.ReasonPhrase + "\"}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
        public string consultaGETSwampCore(string metodo)
        {
            string response = string.Empty;
            try
            {
                this._endPoint = this._configuracion.url + metodo;

                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
                httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(usuario + ":" + password)));
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Correlation-Id", ManagerOperation.GenerateOperation());
                    httpClient.DefaultRequestHeaders.Add("api-version", "1.0");


                    var result = httpClient.GetAsync(new Uri(this._endPoint)).Result;
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}
