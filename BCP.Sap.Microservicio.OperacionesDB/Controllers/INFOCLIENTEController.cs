using BCP.Framework.Common;
using BCP.Framework.Logs;
using BCP.Sap.Business;
using BCP.Sap.Models.Comunes;
using BCP.Sap.Models.INFOCLIENTE;
using BCP.Sap.Models.Swamp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.Sap.Microservicio.OperacionesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class INFOCLIENTEController : ControllerBase
    {
        private string _operacion;
        private string _error;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private IBusinessOperacionesDB _business;
        private string _aplicacion;

        public INFOCLIENTEController(IConfiguration configuration, ILogger logger, IBusinessOperacionesDB business)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._business = business;
        }
        /// <summary>
        /// Método que permite insertar clientes naturales en INFOCLIENTE
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("cliente/insertar")]
        [HttpPost]
        [Authorize]
        public IActionResult InsertarCliente([FromBody] INFOCLIENTEInsertRequest request)
        {
            INFOCLIENTEInsertResponse response = new INFOCLIENTEInsertResponse();
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
                response = this._business.InsertarCliente(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite consultar clientes naturales en INFOCLIENTE.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("cliente/consultar")]
        [HttpPost]
        [Authorize]
        public IActionResult ConsultarCliente([FromBody] INFOCLIENTEConsultaRequest request)
        {
            INFOCLIENTEConsultaResponse response = new INFOCLIENTEConsultaResponse();
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
                response = this._business.DatosCliente(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite consultar clientes naturales en INFOCLIENTE por el Cic.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("cliente/consultarByCic")]
        [HttpPost]
        [Authorize]
        public IActionResult ConsultarClienteBycic([FromBody] INFOCLIENTEConsultaByCicRequest request)
        {
            INFOCLIENTEConsultaResponse response = new INFOCLIENTEConsultaResponse();
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
                response = this._business.DatosClienteByCic(request, canal, usuario, this._operacion, auth, token);
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
    }
}
