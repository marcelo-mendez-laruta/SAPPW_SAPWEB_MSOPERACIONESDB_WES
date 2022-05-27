using BCP.Framework.Common;
using BCP.Framework.Logs;
using BCP.Sap.Business;
using BCP.Sap.Models.Comunes;
using BCP.Sap.Models.INFOCLIENTE;
using BCP.Sap.Models.OperacionesDB;
using BCP.Sap.Models.Seguros;
using BCP.Sap.Models.Swamp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegurosServiceModel = BCP.Sap.Models.Seguros.Service;

namespace BCP.Sap.Microservicio.OperacionesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private string _operacion;
        private string _error;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private IBusinessOperacionesDB _business;
        private string _aplicacion;

        public SegurosController(IConfiguration configuration, ILogger logger, IBusinessOperacionesDB business)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._business = business;
        }
        /// <summary>
        /// Método que permite obtener el pdf de un seguro
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("certificado/generar")]
        [HttpPost]
        [Authorize]
        public IActionResult GenerarContrato([FromBody] GeneraContratoRequest request)
        {
            GeneraContratoResponse response = new GeneraContratoResponse();

            try
            {
                #region SECCION: HEADERS
                string canal = Request.Headers["channel"];
                string usuario = Request.Headers["AppUserId"];
                #endregion
                this._operacion = ManagerOperation.GenerateOperation(request.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.Serialize(request));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.Serialize(request));
                response = this._business.GenerarContrato(request, canal, usuario, this._operacion);
            }
            catch (Exception ex)
            {
                response.state = "500";
                response.message = "Ocurrió un error para más información revise el log.";
                response.success = false;
                this._error = ex.Message;
                response.errors = new List<string>();
                response.errors.Add(this._error);
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", request.operation, this._error, ManagerJson.Serialize(ex));
            }
            finally
            {
                response.operation = this._operacion;
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.Serialize(response));
            }
            if (Response.StatusCode == 200)
                return Ok(response);
            else
                return BadRequest(response);

        }
        /// <summary>
        /// Método que permite registrar un seguro
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("seguros/afiliar")]
        [HttpPost]
        [Authorize]
        public IActionResult RegistrarSeguro([FromBody] RegistrarSeguroRequest request)
        {
            #region Validar Matricula
            var tempUsuario = request.UserCredenciales.Usuario.Split("-");
            string tempUsuario2 = tempUsuario != null ? tempUsuario[0] : request.UserCredenciales.Usuario;
            request.UserCredenciales.Usuario = tempUsuario2.TrimEnd(' ');
            #endregion
            RegistrarSeguroResponse response = new RegistrarSeguroResponse();
            #region Validar Codigo
            string Seguroscode = this._business.ValidarCodigo(request.IdProducto);
            #endregion
            if (Seguroscode != "")
            {
                try
                {
                    #region SECCION: HEADERS
                    string canal = Request.Headers["channel"];
                    string usuario = Request.Headers["AppUserId"];
                    string auth = Request.Headers["Authorization"];
                    string token = Request.Headers["PublicToken"];
                    #endregion
                    this._operacion = ManagerOperation.GenerateOperation(request.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.Serialize(request));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.Serialize(request));
                    #region Obtener Datos Persona
                    INFOCLIENTEConsultaRequest req = new();
                    IdentificadorClienteNatural client = new();
                    client.idcNumero = request.Idc;
                    client.idcTipo = request.Tipo;
                    client.idcExtension = request.Extension;
                    client.idcComplemento = request.Complemento;
                    req.cliente=client;
                    var personaFinal = this._business.DatosClienteSeguro(req, canal, usuario, _operacion, auth, token);
                    if (personaFinal!=null)
                    {
                        #region obtener codigo host profesion 
                        if (!string.IsNullOrEmpty(request.Profesion))
                        {
                            personaFinal.PROFESION = request.Profesion;
                        }
                        #endregion
                        SegurosServiceModel.AfiliacionRequestModel finalrequest = new();                       
                        finalrequest.Persona = personaFinal;
                        #endregion
                        finalrequest.UserCredenciales = request.UserCredenciales;
                        finalrequest.IdProducto = Seguroscode;
                        finalrequest.Agencia = request.Agencia;
                        finalrequest.Sucursal = request.Sucursal;
                        finalrequest.Cuenta = request.Cuenta;
                        Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.Serialize(finalrequest));
                        response = this._business.RegistrarSeguro(finalrequest, canal, usuario, this._operacion);
                        if (response.success)
                        {
                            #region GenerarCertificado
                            GeneraContratoResponse PdfResponse = new();
                            GeneraContratoRequest PdfRequest = new();
                            GeneraContratoRequestData generaContratoRequestData = new GeneraContratoRequestData();
                            generaContratoRequestData.IdPersona = response.data.IdCliente;
                            generaContratoRequestData.IdAfiliacion = response.data.IdAfiliacion;
                            generaContratoRequestData.CodigoProducto = Seguroscode;
                            generaContratoRequestData.IpOrigen = request.UserCredenciales.Ip;
                            PdfRequest.data = generaContratoRequestData;
                            PdfResponse = this._business.GenerarContrato(PdfRequest, canal, usuario, this._operacion);
                            if (PdfResponse.success)
                            {
                                response.data.PDF = PdfResponse.data.PDF;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        response.state = "11";
                        response.message = "OCURRIO UN ERROR AL RECUPERAR LA INFORMACION DE INFOCLIENTE";
                    }
                    return Ok(response);
                   
                }
                catch (Exception ex)
                {
                    response.state = "500";
                    response.message = "Ocurrió un error para más información revise el log.";
                    response.success = false;
                    this._error = ex.Message;
                    response.errors = new List<string>();
                    response.errors.Add(this._error);
                    Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", request.operation, this._error, ManagerJson.Serialize(ex));
                }
                finally
                {
                    response.operation = this._operacion;
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.Serialize(response));
                }
                if (Response.StatusCode == 200)
                {
                    return Ok(response);
                }                   
                else
                    return BadRequest(response);

            }
            else
            {
                response.message = "Codigo no encontrado";
                return Ok(response);
            }
        }
    }
}