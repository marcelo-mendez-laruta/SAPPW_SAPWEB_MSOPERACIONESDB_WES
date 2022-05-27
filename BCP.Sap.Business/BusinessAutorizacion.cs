using BCP.Framework.Common;
using BCP.Sap.Connectors;
using BCP.Sap.Models.Autorizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Business
{
    public class BusinessAutorizacion
    {
        private ConnectorAutorizacion _dataAccessAutorizacion;
        public BusinessAutorizacion(ConfiguracionAutorizacion configuracion)
        {
            this._dataAccessAutorizacion = new ConnectorAutorizacion(configuracion);
        }

        public List<UsuarioApi> getUsuario(string usuario, string password, string canal, string publicToken, string appUserId)
        {
            List<UsuarioApi> response = new List<UsuarioApi>();

            AutorizacionRequest svrRequest = new AutorizacionRequest();
            svrRequest.date = ManagerOperation.GenerateOperationV2(string.Empty);
            svrRequest.channel = canal;
            svrRequest.publicToken = publicToken;
            svrRequest.appUserId = appUserId;
            AutorizacionResponse svrResponse = this._dataAccessAutorizacion.VerificaAutorizacion(usuario, password, svrRequest);
            try
            {
                if (svrResponse.state.Equals("00"))
                {
                    response.Add(new UsuarioApi { id = svrResponse.data.id, userName = svrResponse.data.username, password = "" });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public List<BasicAutorizacionModel> getUsuarioN(string usuario, string password, string canal, string publicToken, string appUserId)
        {
            List<BasicAutorizacionModel> response = new List<BasicAutorizacionModel>();

            AutorizacionRequest svrRequest = new AutorizacionRequest();
            svrRequest.date = ManagerOperation.GenerateOperationV2(string.Empty);
            svrRequest.channel = canal;
            svrRequest.publicToken = publicToken;
            svrRequest.appUserId = appUserId;
            AutorizacionResponse svrResponse = this._dataAccessAutorizacion.VerificaAutorizacion(usuario, password, svrRequest);
            try
            {
                if (svrResponse.state.Equals("00"))
                {
                    response.Add(new BasicAutorizacionModel { id = svrResponse.data.id, userName = svrResponse.data.username, password = "" });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

    }
}
