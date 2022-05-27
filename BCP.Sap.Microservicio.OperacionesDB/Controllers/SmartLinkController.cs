using BCP.Framework.Common;
using BCP.Framework.Logs;
using BCP.Sap.Business;
using BCP.Sap.Models.Comunes;
using BCP.Sap.Models.SmartLink;
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
    public class SmartLinkController : ControllerBase
    {
        private string _operacion;
        private string _error;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private IBusinessOperacionesDB _business;
        private string _aplicacion;

        public SmartLinkController(IConfiguration configuration, ILogger logger, IBusinessOperacionesDB business)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._business = business;
        }
        /// <summary>
        /// Método que permite actualizar los datos de una tarjeta en SmartLink.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("actualizacion")]
        [HttpPost]
        [Authorize]
        public IActionResult ActualizacionTarjeta([FromBody] TarjetaActualizacionRequest request)
        {
            TarjetaActualizacionResponse response = new TarjetaActualizacionResponse();
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
                response = this._business.TarjetaActualizacion(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite actualizar los datos de una tarjeta en SmartLink.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("desafiliacion")]
        [HttpPost]
        [Authorize]
        public IActionResult DesafiliarCuenta([FromBody] TarjetaDesafiliacionCuentaRequest request)
        {
            TarjetaDesafiliacionCuentaResponse response = new TarjetaDesafiliacionCuentaResponse();
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
                response = this._business.DesafiliarCuenta(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite validar el pin de una trajeta en SmartLink.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("validacion")]
        [HttpPost]
        [Authorize]
        public IActionResult ValidacionPin([FromBody] ValidacionPinRequest request)
        {
            ValidacionPinResponse response = new ValidacionPinResponse();
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
                response = this._business.ValidacionPin(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite registrar el bloqueo de una tarjeta en SmartLink.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("bloqueo")]
        [HttpPost]
        [Authorize]
        public IActionResult BloqueoTarjeta([FromBody] TarjetaBloqueoRequest request)
        {
            TarjetaBloqueoResponse response = new TarjetaBloqueoResponse();
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
                response = this._business.TarjetaBloqueo(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite registrar el cambio de una tarjeta en SmartLink.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("cambio")]
        [HttpPost]
        [Authorize]
        public IActionResult CambioTarjeta([FromBody] TarjetaCambioRequest request)
        {
            TarjetaCambioResponse response = new TarjetaCambioResponse();
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
                response = this._business.TarjetaCambio(request, canal, usuario, this._operacion,auth,token);
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
        /// Método que permite realizar operacion de cuenta en SmartLink.
        /// </summary>
        /// <param name="request">Objeto con los datos necesarios para realizar la transacción.</param>
        /// <returns></returns>
        [Route("cuenta")]
        [HttpPost]
        [Authorize]
        public IActionResult OperacionCuenta([FromBody] CuentaRegistroRequest request)
        {
            CuentaRegistroResponse response = new CuentaRegistroResponse();
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
                response = this._business.OperacionCuenta(request, canal, usuario, this._operacion,auth,token);
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
