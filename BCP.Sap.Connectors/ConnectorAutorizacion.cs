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
    public class ConnectorAutorizacion
    {

        private ConfiguracionAutorizacion _configuracion;
        private string _endPoint;
        public ConnectorAutorizacion(ConfiguracionAutorizacion configuracion)
        {
            this._configuracion = configuracion;
        }

        public AutorizacionResponse VerificaAutorizacion(string usuario, string password, AutorizacionRequest request)
        {
            AutorizacionResponse response = new AutorizacionResponse();
            try
            {
                this._endPoint = this._configuracion.url + this._configuracion.metodo;

                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
                httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(usuario + ":" + password)));
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Correlation-Id", ManagerOperation.GenerateOperation());
                    httpClient.DefaultRequestHeaders.Add("api-version", "1.0");


                    var json = ManagerJson.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = httpClient.PostAsync(new Uri(this._endPoint), content).Result;
                    var resultContent = result.Content.ReadAsStringAsync().Result;
                    response = ManagerJson.Deserialize<AutorizacionResponse>(resultContent);
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
