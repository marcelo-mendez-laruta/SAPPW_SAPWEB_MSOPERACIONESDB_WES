using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BCP.Framework.Security;
using srvSeguros;
using BCP.Sap.Models.Seguros;
using System.Globalization;
using ServiceModel = BCP.Sap.Models.Seguros.Service;

namespace BCP.Sap.Connectors
{
    public class ConnectorSeguros
    {
        private ServicioAfiliacionAuthClient _seguros;
        private CREDENCIALES_AUTH _crededenciales;
        public ConnectorSeguros(string url, string usuario, string contrasena, string ipOrigen)
        {
            usuario = SegCrypt.EncryptDecrypt(false, usuario);
            contrasena = SegCrypt.EncryptDecrypt(false, contrasena);
            _crededenciales = new CREDENCIALES_AUTH
            {
                IP = ipOrigen,
                USUARIO = usuario,
                PASSWORD = contrasena
            };
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            EndpointAddress address = new EndpointAddress(url);
            _seguros = new ServicioAfiliacionAuthClient(binding, address);
        }
        public ServiceModel.GetCertificadoResponseModel GenerarContrato(ServiceModel.GetCertificadoRequestModel request)
        {
            ServiceModel.GetCertificadoResponseModel response = new();
            try
            {
                OC_IBKResponseDocumentoIBK respuesta = _seguros.GetFileCertificado(request.IdPersona, request.IdAfiliacion, request.IdProducto, _crededenciales);
                response.ERROR_TECNICO = respuesta.ERROR_TECNICO;
                response.CODIGO_ERROR = respuesta.CODIGO_ERROR;
                response.MENSAJE_ERROR = respuesta.MENSAJE_ERROR;
                response.IMAGEN=respuesta.IMAGEN;                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _seguros.Close();
            }
            return response;
        }
        public ServiceModel.AfiliacionResponseModel RegistrarSeguro(ServiceModel.AfiliacionRequestModel request)
        {
            ServiceModel.AfiliacionResponseModel response = new();
            try
            {
                OC_IBKOC_PERSONA Persona = new();
                Persona.CELULAR=request.Persona.CELULAR;
                Persona.CI = request.Persona.CI;
                Persona.CIC = request.Persona.CIC;
                Persona.COMPLEMENTO = request.Persona.COMPLEMENTO;
                Persona.CORREO = request.Persona.CORREO;
                Persona.DIRECCION = request.Persona.DIRECCION;
                Persona.ESTADO_CIVIL = request.Persona.ESTADO_CIVIL;
                Persona.EXTENSION = request.Persona.EXTENSION;
                Persona.MATERNO = request.Persona.MATERNO;
                Persona.NACIONALIDAD = request.Persona.NACIONALIDAD;
                Persona.NOMBRE = request.Persona.NOMBRE;
                Persona.NUMERO_DIRECCION = request.Persona.NUMERO_DIRECCION;
                Persona.OCUPACION = request.Persona.OCUPACION;
                Persona.PATERNO = request.Persona.PATERNO;
                Persona.PROFESION = request.Persona.PROFESION;
                Persona.SEXO = request.Persona.SEXO;
                Persona.TELEFONO = request.Persona.TELEFONO;
                Persona.TIPO_DIRECCION = request.Persona.TIPO_DIRECCION;
                DateTime nacimiento;
                DateTime.TryParseExact(request.Persona.FECHA_NACIMIENTO, "dd/MM/yyyy", null, DateTimeStyles.None, out nacimiento);
                Persona.FECHA_NACIMIENTO = nacimiento;
                if (!string.IsNullOrEmpty(request.Persona.IMAGEN))
                {
                    Persona.IMAGEN = Convert.FromBase64String(request.Persona.IMAGEN);
                }

                CREDENCIALES userCredenciales = new();
                userCredenciales.IP = request.UserCredenciales.Ip;
                userCredenciales.USUARIO = request.UserCredenciales.Usuario;

                OC_IBKResponseAfiliacionIBK serviceResponses = _seguros.SaveAfiliacionSAP(Persona, request.Sucursal, request.Agencia, request.Cuenta.Replace("-", ""), request.IdProducto, userCredenciales, _crededenciales);
                response.ID_CLIENTE = serviceResponses.ID_CLIENTE;
                response.ID_AFILIACION= serviceResponses.ID_AFILIACION;
                response.ERROR_TECNICO= serviceResponses.ERROR_TECNICO;
                response.CODIGO_ERROR = serviceResponses.CODIGO_ERROR;
                response.MENSAJE_ERROR = serviceResponses.MENSAJE_ERROR;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _seguros.Close();
            }
            return response;
        }
    }
}
