using BCP.Framework.Common;
using BCP.Sap.Connectors;
using BCP.Sap.Models.INFOCLIENTE;
using BCP.Sap.Models.OperacionesDB;
using BCP.Sap.Models.Seguros;
using BCP.Sap.Models.SmartLink;
using BCP.Sap.Models.Swamp;
using BCP.Sap.Models.SwampCore;
using System;
using SegurosServiceModel = BCP.Sap.Models.Seguros.Service;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Business
{
    public interface IBusinessOperacionesDB
    {
        INFOCLIENTEInsertResponse InsertarCliente(INFOCLIENTEInsertRequest request, string canal, string usuario, string operacion,string auth,string token);
        INFOCLIENTEConsultaResponse DatosCliente(INFOCLIENTEConsultaRequest request, string canal, string usuario, string operacion, string auth, string token);
        INFOCLIENTEConsultaResponse DatosClienteByCic(INFOCLIENTEConsultaByCicRequest request, string canal, string usuario, string operacion, string auth, string token);
        SegurosServiceModel.Persona DatosClienteSeguro(INFOCLIENTEConsultaRequest request, string canal, string usuario, string operacion, string auth, string token);
        SwampClienteGuidResponse IDCCliente(SwampClienteGuidRequest request, string canal, string usuario, string operacion, string auth, string token);
        RegistroSwampCuentaResponse RegistroSwampCuenta(RegistroSwampCuentaRequest request, string canal, string usuario, string operacion, string auth, string token);
        RegistroSwampCuentaProductoResponse RegistroSwampCuentaProducto(RegistroSwampCuentaProductoRequest request, string canal, string usuario, string operacion, string auth, string token);
        RegistroSwampCuentaFirmaResponse RegistroSwampCuentaFirma(RegistroSwampCuentaFirmaRequest request, string canal, string usuario, string operacion, string auth, string token);
        TarjetaActualizacionResponse TarjetaActualizacion(TarjetaActualizacionRequest request, string canal, string usuario, string operacion, string auth, string token);
        ValidacionPinResponse ValidacionPin(ValidacionPinRequest request, string canal, string usuario, string operacion, string auth, string token);
        TarjetaDesafiliacionCuentaResponse DesafiliarCuenta(TarjetaDesafiliacionCuentaRequest request, string canal, string usuario, string operacion, string auth, string token);
        TarjetaBloqueoResponse TarjetaBloqueo(TarjetaBloqueoRequest request, string canal, string usuario, string operacion, string auth, string token);
        TarjetaCambioResponse TarjetaCambio(TarjetaCambioRequest request, string canal, string usuario, string operacion, string auth, string token);
        CuentaRegistroResponse OperacionCuenta(CuentaRegistroRequest request, string canal, string usuario, string operacion, string auth, string token);
        GeneraContratoResponse GenerarContrato(GeneraContratoRequest request, string canal, string usuario, string operacion);
        string ValidarCodigo(string Codigo);
        RegistrarSeguroResponse RegistrarSeguro(SegurosServiceModel.AfiliacionRequestModel request, string canal, string usuario, string operacion);
    }
    public class BusinessOperacionesDB : IBusinessOperacionesDB
    {

        private OperacionesDBConfig _configuracion;
        private ConnectorSwampCore _swampCore;
        public BusinessOperacionesDB(OperacionesDBConfig configuracion)
        {
            this._configuracion = configuracion;
            this._swampCore = new ConnectorSwampCore(configuracion.configuracionSwampCore);
        }
        #region SECCION: INFOCLIENTE
        public INFOCLIENTEConsultaResponse DatosCliente(INFOCLIENTEConsultaRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            INFOCLIENTEConsultaResponse response = new INFOCLIENTEConsultaResponse();            
            try
            {                
                ClienteByIdcRequest request1 = new ClienteByIdcRequest
                {
                    idc = request.cliente.idcNumero + request.cliente.idcTipo + request.cliente.idcExtension,
                    complemento=request.cliente.idcComplemento
                };
                ClienteByIdcResponse response1 = this._swampCore.consultaPOSTSwampCore<ClienteByIdcResponse>(this._configuracion.configuracionSwampCore.metodoClienteByIdc, request1,auth,canal,token,usuario);
                if (response1.state == 200)
                {
                    response.data = new INFOCLIENTEConsultaData();
                    response.data.Paterno = response1.paterno;
                    response.data.Materno = response1.materno;
                    response.data.Nombres = response1.nombres;
                    response.data.CIC=response1.cic;
                    DataClienteByIdRequest request2 = new DataClienteByIdRequest
                    {
                        clienteId = response1.clienteId
                    };
                    DatoPersonalListByIdResponse response2 = this._swampCore.consultaPOSTSwampCore<DatoPersonalListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoPersonalListById, request2, auth, canal, token, usuario);
                    if (response2.state == 200)
                    {
                        response.data.FechaNacimiento = ManagerDate.ChangeFormatDate(response2.fechaNacimiento, "yyyyMMdd", "dd/MM/yyyy");
                        response.data.EstadoCivil = response2.estadoCivilAbreviacion;
                        response.data.Nacionalidad = response2.nacionalidadAbreviacion;
                        response.data.Sexo = response2.sexoAbreviacion;
                        response.data.Profesion = response2.profesionDescripcion;
                        response.data.GradoInstruccion = response2.gradoInstruccionAbreviacion;
                        response.data.Telefono = response2.telefono;
                        response.data.Residente = response2.residente ? "S" : "N";
                        response.data.celular = response2.celular;
                        response.data.mail = response2.correoElectronico;
                    }
                    DatoLaboralListByIdResponse response3 = this._swampCore.consultaPOSTSwampCore<DatoLaboralListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoLaboralListById, request2, auth, canal, token, usuario);
                    if (response3.state == 200)
                    {
                        response.data.SituacionLaboral = response3.situacionLaboralAbreviacion;
                        response.data.CIIU = response3.ciiuCodigo;
                        response.data.NegocioPropio = response3.negocioPropio ? "S" : "N";
                        response.data.TipoCuenta = response.data.NegocioPropio.Equals("S") ? "I" : "P";
                        string nit = ManagerValidation.GetDefaultValue(response3.nit, "");
                        string empresa = ManagerValidation.GetDefaultValue(response3.nombreEmpresa, "");
                        if (response.data.SituacionLaboral.Equals("DEP") || response.data.SituacionLaboral.Equals("EST") || response.data.SituacionLaboral.Equals("JUB"))
                        {
                            response.data.RUC = nit;
                            response.data.NombreEmpresa = empresa;
                        }
                        else
                        {
                            response.data.NIT = nit;
                            if (response.data.NegocioPropio.Equals("N"))
                            {
                                response.data.NombreComercial = string.Empty;
                            }
                            else
                            {
                                response.data.NombreComercial = empresa;
                            }
                        }
                        
                    }
                    DatoDireccionListByIdResponse response4= this._swampCore.consultaPOSTSwampCore<DatoDireccionListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoDireccionListById, request2, auth, canal, token, usuario);
                    if (response4.state == 200)
                    {
                       foreach(var item in response4.lstDirecciones)
                        {
                            if (item.idDireccion == 19)
                            {
                                response.data.IndicadorVivienda = ManagerValidation.formatoVivienda(item.tipoViviendaAbreviacion);
                                response.data.Localidad = item.departamento+"-"+item.provincia+"-"+item.distrito;
                                if (response.data.Localidad.Contains("NINGUNO"))
                                {
                                    response.data.Localidad = string.Empty;
                                }
                                string direccion = string.Empty;
                                string aux=string.Empty;
                                if (!string.IsNullOrEmpty(item.tipoDefDireccionDescripcion))
                                {
                                    aux = ManagerValidation.formatoTipoCalle(item.tipoDefDireccionAbreviacion.Trim());
                                    if(!string.IsNullOrEmpty(aux))
                                    {
                                        direccion = direccion + aux;
                                    }                                    
                                }
                                if (!string.IsNullOrEmpty(item.direccion))
                                {
                                    direccion = direccion + item.direccion.Trim()+" ";
                                }
                                if (!string.IsNullOrEmpty(item.numeroDireccion))
                                {
                                    direccion = direccion + "N." + item.numeroDireccion.Trim()+" ";
                                }
                                if (!string.IsNullOrEmpty(item.manzanaDireccion))
                                {
                                    direccion = direccion + "MZ." + item.manzanaDireccion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.loteDireccion))
                                {
                                    direccion = direccion + "LT." + item.loteDireccion.Trim() + " ";
                                }
                                if(!string.IsNullOrEmpty(item.numeroDepartamento) && !item.tipoDepartamentoAbreviacion.Trim().Equals("NDI"))
                                {
                                    aux = item.tipoDepartamentoAbreviacion.Trim();
                                    if (!aux[0].Equals("."))
                                    {
                                        aux = aux + ".";
                                    }
                                    direccion=direccion+aux+ item.numeroDepartamento.Trim()+" ";
                                }
                                if (!string.IsNullOrEmpty(item.nombreUrbanizacion) && !item.urbanizacionAbreviacion.Trim().Equals("NDI"))
                                {
                                    direccion = direccion + "BRR." + item.nombreUrbanizacion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.nombreSectorUrbanizacion) && !item.sectUrbanizacionAbreviacion.Trim().Equals("NDI"))
                                {
                                    direccion = direccion + item.sectUrbanizacionAbreviacion.Trim().Substring(0,1)+"." + item.nombreSectorUrbanizacion.Trim() + " ";
                                }
                                response.data.Direccion = direccion.Trim();
                            }
                        }
                    }
                    response.success = true;
                    response.state = "00";
                    response.message = "PROCESO EJECUTADO CORRECTAMENTE";
                }              
                else if(response1.state==405)
                {
                    response.state = "11";
                    response.message = "CLIENTE NO ENCONTRADO";
                }
                else
                {
                    response.state = "02";
                    response.message = "OCURRIO UN ERROR CON LA CONSULTA A SWAMPCORE.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public INFOCLIENTEConsultaResponse DatosClienteByCic(INFOCLIENTEConsultaByCicRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            INFOCLIENTEConsultaResponse response = new INFOCLIENTEConsultaResponse();
            try
            {
                ClienteByCicRequest request1 = new ClienteByCicRequest
                {
                    cic = request.cliente.CIC
                };
                ClienteByIdcResponse response1 = this._swampCore.consultaPOSTSwampCore<ClienteByIdcResponse>(this._configuracion.configuracionSwampCore.metodoClienteByCic, request1, auth, canal, token, usuario);
                if (response1.state == 200)
                {
                    response.data = new INFOCLIENTEConsultaData();
                    response.data.Paterno = response1.paterno;
                    response.data.Materno = response1.materno;
                    response.data.Nombres = response1.nombres;
                    response.data.CIC = response1.cic;
                    DataClienteByIdRequest request2 = new DataClienteByIdRequest
                    {
                        clienteId = response1.clienteId
                    };
                    DatoPersonalListByIdResponse response2 = this._swampCore.consultaPOSTSwampCore<DatoPersonalListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoPersonalListById, request2, auth, canal, token, usuario);
                    if (response2.state == 200)
                    {
                        response.data.FechaNacimiento = ManagerDate.ChangeFormatDate(response2.fechaNacimiento, "yyyyMMdd", "dd/MM/yyyy");
                        response.data.EstadoCivil = response2.estadoCivilAbreviacion;
                        response.data.Nacionalidad = response2.nacionalidadAbreviacion;
                        response.data.Sexo = response2.sexoAbreviacion;
                        response.data.Profesion = response2.profesionDescripcion;
                        response.data.GradoInstruccion = response2.gradoInstruccionAbreviacion;
                        response.data.Telefono = response2.telefono;
                        response.data.Residente = response2.residente ? "S" : "N";
                        response.data.celular = response2.celular;
                        response.data.mail = response2.correoElectronico;
                    }
                    DatoLaboralListByIdResponse response3 = this._swampCore.consultaPOSTSwampCore<DatoLaboralListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoLaboralListById, request2, auth, canal, token, usuario);
                    if (response3.state == 200)
                    {
                        response.data.SituacionLaboral = response3.situacionLaboralAbreviacion;
                        response.data.CIIU = response3.ciiuCodigo;
                        response.data.NegocioPropio = response3.negocioPropio ? "S" : "N";
                        response.data.TipoCuenta = response.data.NegocioPropio.Equals("S") ? "I" : "P";
                        string nit = ManagerValidation.GetDefaultValue(response3.nit, "");
                        string empresa = ManagerValidation.GetDefaultValue(response3.nombreEmpresa, "");
                        if (response.data.SituacionLaboral.Equals("DEP") || response.data.SituacionLaboral.Equals("EST") || response.data.SituacionLaboral.Equals("JUB"))
                        {
                            response.data.RUC = nit;
                            response.data.NombreEmpresa = empresa;
                        }
                        else
                        {
                            response.data.NIT = nit;
                            if (response.data.NegocioPropio.Equals("N"))
                            {
                                response.data.NombreComercial = string.Empty;
                            }
                            else
                            {
                                response.data.NombreComercial = empresa;
                            }
                        }

                    }
                    DatoDireccionListByIdResponse response4 = this._swampCore.consultaPOSTSwampCore<DatoDireccionListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoDireccionListById, request2, auth, canal, token, usuario);
                    if (response4.state == 200)
                    {
                        foreach (var item in response4.lstDirecciones)
                        {
                            if (item.idDireccion == 19)
                            {
                                response.data.IndicadorVivienda = ManagerValidation.formatoVivienda(item.tipoViviendaAbreviacion);
                                response.data.Localidad = item.departamento + "-" + item.provincia + "-" + item.distrito;
                                if (response.data.Localidad.Contains("NINGUNO"))
                                {
                                    response.data.Localidad = string.Empty;
                                }
                                string direccion = string.Empty;
                                string aux = string.Empty;
                                if (!string.IsNullOrEmpty(item.tipoDefDireccionDescripcion))
                                {
                                    aux = ManagerValidation.formatoTipoCalle(item.tipoDefDireccionAbreviacion.Trim());
                                    if (!string.IsNullOrEmpty(aux))
                                    {
                                        direccion = direccion + aux;
                                    }
                                }
                                if (!string.IsNullOrEmpty(item.direccion))
                                {
                                    direccion = direccion + item.direccion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.numeroDireccion))
                                {
                                    direccion = direccion + "N." + item.numeroDireccion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.manzanaDireccion))
                                {
                                    direccion = direccion + "MZ." + item.manzanaDireccion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.loteDireccion))
                                {
                                    direccion = direccion + "LT." + item.loteDireccion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.numeroDepartamento) && !item.tipoDepartamentoAbreviacion.Trim().Equals("NDI"))
                                {
                                    aux = item.tipoDepartamentoAbreviacion.Trim();
                                    if (!aux[0].Equals("."))
                                    {
                                        aux = aux + ".";
                                    }
                                    direccion = direccion + aux + item.numeroDepartamento.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.nombreUrbanizacion) && !item.urbanizacionAbreviacion.Trim().Equals("NDI"))
                                {
                                    direccion = direccion + "BRR." + item.nombreUrbanizacion.Trim() + " ";
                                }
                                if (!string.IsNullOrEmpty(item.nombreSectorUrbanizacion) && !item.sectUrbanizacionAbreviacion.Trim().Equals("NDI"))
                                {
                                    direccion = direccion + item.sectUrbanizacionAbreviacion.Trim().Substring(0, 1) + "." + item.nombreSectorUrbanizacion.Trim() + " ";
                                }
                                response.data.Direccion = direccion.Trim();
                            }
                        }
                    }
                    response.success = true;
                    response.state = "00";
                    response.message = "PROCESO EJECUTADO CORRECTAMENTE";
                }
                else if (response1.state == 405)
                {
                    response.state = "11";
                    response.message = "CLIENTE NO ENCONTRADO";
                }
                else
                {
                    response.state = "02";
                    response.message = "OCURRIO UN ERROR CON LA CONSULTA A SWAMPCORE.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #region CONSULTAS INFOCLIENTE SEGUROS
        public SegurosServiceModel.Persona DatosClienteSeguro(INFOCLIENTEConsultaRequest request, string canal, string usuario, string operacion, string auth, string token)
        {               

            try
            {
                SegurosServiceModel.Persona personaFinal = new();
                ClienteByIdcRequest request1 = new ClienteByIdcRequest
                {
                    idc = request.cliente.idcNumero + request.cliente.idcTipo + request.cliente.idcExtension,
                    complemento = request.cliente.idcComplemento
                };
                ClienteByIdcResponse response1 = this._swampCore.consultaPOSTSwampCore<ClienteByIdcResponse>(this._configuracion.configuracionSwampCore.metodoClienteByIdc, request1, auth, canal, token, usuario);
                if (response1.state == 200)
                {
                    personaFinal.CI = request.cliente.idcNumero;
                    personaFinal.EXTENSION = request.cliente.idcExtension;
                    personaFinal.PATERNO = response1.paterno;
                    personaFinal.MATERNO = response1.materno;
                    personaFinal.NOMBRE = response1.nombres;
                    personaFinal.CIC = response1.cic;
                    personaFinal.COMPLEMENTO = response1.complemento;
                    DataClienteByIdRequest request2 = new DataClienteByIdRequest
                    {
                        clienteId = response1.clienteId
                    };
                    DatoPersonalListByIdResponse response2 = this._swampCore.consultaPOSTSwampCore<DatoPersonalListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoPersonalListById, request2, auth, canal, token, usuario);
                    if (response2.state == 200)
                    {
                        personaFinal.FECHA_NACIMIENTO = ManagerDate.ChangeFormatDate(response2.fechaNacimiento, "yyyyMMdd", "dd/MM/yyyy");
                        personaFinal.ESTADO_CIVIL = response2.estadoCivilAbreviacion;
                        personaFinal.NACIONALIDAD = response2.nacionalidadAbreviacion;
                        personaFinal.SEXO = response2.sexoDescripcion;
                        personaFinal.PROFESION = response2.profesionDescripcion;
                        personaFinal.TELEFONO = response2.telefono;
                        personaFinal.CELULAR = response2.celular;
                        personaFinal.CORREO = response2.correoElectronico;
                    }
                    DatoLaboralListByIdResponse response3 = this._swampCore.consultaPOSTSwampCore<DatoLaboralListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoLaboralListById, request2, auth, canal, token, usuario);
                    if (response3.state == 200)
                    {
                        personaFinal.OCUPACION = response3.situacionLaboralAbreviacion; 
                    }
                    DatoDireccionListByIdResponse response4 = this._swampCore.consultaPOSTSwampCore<DatoDireccionListByIdResponse>(this._configuracion.configuracionSwampCore.metodoDatoDireccionListById, request2, auth, canal, token, usuario);
                    if (response4.state == 200)
                    {
                        foreach (var item in response4.lstDirecciones)
                        {
                            if (item.idDireccion == 19)
                            {

                                personaFinal.DIRECCION = item.direccion;
                                personaFinal.NUMERO_DIRECCION = item.numeroDireccion;
                                personaFinal.TIPO_DIRECCION = item.tipoDefDireccionDescripcion;
                            }
                        }
                    }
                    return personaFinal;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        public INFOCLIENTEInsertResponse InsertarCliente(INFOCLIENTEInsertRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            INFOCLIENTEInsertResponse response = new INFOCLIENTEInsertResponse();
            response.operation = operacion;
            try
            {
                ClienteInsertRequest request1 = new ClienteInsertRequest();
                request1.idc = request.cliente.idcNumero;
                request1.extension = request.cliente.idcExtension;
                request1.tipo = request.cliente.idcTipo;
                request1.paterno = request.data.Paterno;
                request1.materno = request.data.Materno;
                request1.nombres = request.data.Nombres;
                request1.fechaNacimiento = ManagerDate.ChangeFormatDate(request.data.FechaNacimiento, "dd/MM/yyyy", "ddMMyyyy");
                request1.sexo = request.data.Sexo;
                request1.estadoCivil = request.data.EstadoCivil;
                request1.nacionalidad = request.data.Nacionalidad;
                request1.localidad = request.data.Localidad;
                request1.telefono = request.data.Telefono;
                request1.instruccion = request.data.GradoInstruccion;
                request1.profesion = request.data.Profesion;
                request1.situacionLaboral = request.data.SituacionLaboral;
                request1.condVivienda = request.data.TipoVivienda;
                request1.residente = request.data.Residente;
                request1.negocioPropio = request.data.negocioPropio;
                request1.empresa = request.data.NombreEmpresa;
                request1.email = request.data.Mail;
                request1.magnitud = request.data.MagnitudEmpresa;
                request1.celular = request.data.Celular;
                request1.ciiu = request.data.CIIU;
                request1.ciiu2 = request.data.ciiu2;
                request1.cic = request.data.CIC;
                request1.usuario = request.usuario;
                request1.canal = canal;
                request1.calle = request.data.strCalle;
                request1.direccion = request.data.Domicilio;
                request1.manzana = request.data.strManzana;
                request1.lote = request.data.strLote;
                request1.departamento = request.data.strDepartamento;
                request1.departamentoPiso = request.data.strDepPiso;
                request1.urbanizacion = request.data.strUrbanizacion;
                request1.urbanizacionTipo = request.data.strUrbanizacionTipo;
                request1.sector = request.data.strSector;
                request1.sectorTipo = request.data.strSectorTipo;
                request1.nit = request.data.NIT;
                request1.numero = request.data.strNumeroDomicilio;
                ClienteInsertResponse response1 = this._swampCore.consultaPOSTSwampCore<ClienteInsertResponse>(this._configuracion.configuracionSwampCore.metodoClienteInsert, request1, auth, canal, token, usuario);
                if (response1.state==200)
                {
                    response.success = true;
                    response.state = "00";
                    response.message = "PROCESO EJECUTADO CORRECTAMENTE.";
                }
                else
                {
                    response.state = "01";
                    response.message = "NO SE PUDO REGISTRAR NINGUN DATO.";
                }
            }
            catch (Exception Ex)
            {
                response.state = "99";
                response.message = Ex.Message;
            }
            return response;
        }
        #endregion

        #region SECCION: SWAMP
        public SwampClienteGuidResponse IDCCliente(SwampClienteGuidRequest request,string canal,string usuario,string operacion, string auth, string token)
        {
            SwampClienteGuidResponse response = new SwampClienteGuidResponse();
            try
            {
                DatosBasicosClienteTicketRequest request1= new DatosBasicosClienteTicketRequest
                {
                    guid=request.guid,
                    estado=1
                };
                DatosBasicosClienteTicketResponse response1 = this._swampCore.consultaPOSTSwampCore<DatosBasicosClienteTicketResponse>(this._configuracion.configuracionSwampCore.metodoDatosBasicosClienteTicket, request1, auth, canal, token, usuario);
                if (response1.state==200)
                {
                    response.data = new SwampClienteGuidResponseData
                    {
                        guid = request1.guid,
                        idcNumero=response1.idc,
                        idcTipo=response1.tipo,
                        idcExtension=response1.extension
                        //,complemento=response1.complemento
                    };
                    response.success = true;
                    response.state = "00";
                    response.message = "Operación ejecutada correctamente.";
                }
                else if (response1.state == 200)
                {
                    response.success = false;
                    response.state = "01";
                    response.message = "Guid de sesión Invalida.";
                }
                else
                {
                    response.success = false;
                    response.state = "02";
                    response.message = "Ocurrio un error con la consulta a SwampCore.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public RegistroSwampCuentaResponse RegistroSwampCuenta(RegistroSwampCuentaRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            RegistroSwampCuentaResponse response = new RegistroSwampCuentaResponse();
            response.operation = operacion;
            try
            {
                CuentaAddRequest request1 = new CuentaAddRequest 
                { 
                    guid=request.data.ses_guid,
                    original=request.data.cta_original,
                    apertura=request.data.cta_apertura,
                    countFirmantes=request.data.cta_countfirmantes,
                    countProductos=request.data.cta_countproductos,
                    numFirmantes=request.data.cta_numfirmantes,
                    id=request.data.cta_id,
                    materno=request.data.cta_apmaterno,
                    paterno=request.data.cta_appaterno,
                    clienteDelBanco=request.data.cta_clientedelbanco,
                    codCIIU=request.data.cta_codciiu,
                    codSectorista=request.data.cta_codigosectorista,
                    codSucursalAgencia= request.data.cta_codSucursalAgencia,
                    codTipoBanca=request.data.cta_codtipobanca,
                    codTipoTarjetaCredimas=request.data.cta_codtipotarjetacredimas,
                    ctaAPlazoInfo=request.data.cta_ctaaplazoinfo,
                    ctaExcInfo=request.data.cta_ctaexcinfo,
                    direccion=request.data.cta_direccion,
                    idcN=request.cliente.idcNumero+request.cliente.idcExtension,
                    //idcS=request.cliente.idcTipo,
                    idcT=request.cliente.idcTipo,
                    localidadDescripcion=request.data.cta_localidaddescripcion,
                    monto=request.data.cta_monto,
                    nombreRazonSocial=request.data.cta_nombres_razsocial,
                    nombreComercialNombreCuenta=request.data.cta_nomcomerc_nomcuenta,
                    nroCredimas=request.data.cta_numerocredimas,
                    situacionTarjetaDescripcion=request.data.cta_situaciontarjetadescripcion,
                    tarjetaBancaExclusiva=request.data.cta_tarjetabancaexclusiva,
                    telefono=request.data.cta_telefono,
                    gremio=request.data.cta_gremio,
                    sucAge= request.usuario.sucursal + " - " + request.usuario.agencia,
                    tipoCuenta=request.data.cta_tipocuenta,
                    tipoOperacionCredimas=request.data.cta_tipooperacioncredimas,
                    usrCrea=request.usuario.usuarioExtra
                };
                CuentaAddResponse response1 = this._swampCore.consultaPOSTSwampCore<CuentaAddResponse>(this._configuracion.configuracionSwampCore.metodoCuentaAdd, request1, auth, canal, token, usuario);
                if (response1.state==200)
                {
                    response.success = true;
                    response.state = "00";
                    response.message = "PROCESO RERALIZADO CORRECTAMENTE";
                    response.data = new RegistroSwampCuentaResponseData { idCuenta = response1.id };
                }
                else
                {
                    response.state = "01";
                    response.message = "ERROR AL REALIZAR LA INSERCION";
                }
            }
            catch (Exception ex)
            {
                response.state = "99";
                response.message = ex.Message;
            }
            return response;
        }
        public RegistroSwampCuentaFirmaResponse RegistroSwampCuentaFirma(RegistroSwampCuentaFirmaRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            RegistroSwampCuentaFirmaResponse response = new RegistroSwampCuentaFirmaResponse();
            response.operation = operacion;
            try
            {
                CuentaFirmaAddRequest request1 = new CuentaFirmaAddRequest 
                {
                    guid=request.data.ses_guid,
                    ctaId=request.data.cta_id,
                    firId=request.data.fir_id,
                    actualizaDatos=request.data.fir_actualizadatos,
                    clienteNuevo=request.data.fir_clientenuevo,
                    materno=request.data.fir_apmaterno,
                    paterno=request.data.fir_appaterno,
                    nombresRazSocial=request.data.fir_nombres_razsocial,
                    estadoCivil=request.data.fir_estadocivil,
                    fechNac=request.data.fir_fechanac,
                    idcN=request.cliente.idcNumero+ request.cliente.idcExtension,
                    idcS= request.cliente.idcNumero + request.cliente.idcExtension,
                    idcT=request.cliente.idcTipo,
                    nroCredimas=request.data.fir_numerocredimas,
                    usrCreacion=request.usuario
                };
                CuentaAddResponse response1 = this._swampCore.consultaPOSTSwampCore<CuentaAddResponse>(this._configuracion.configuracionSwampCore.metodoCuentaFirmaAdd, request1, auth, canal, token, usuario);
                if (response1.state==200)
                {
                    response.success = true;
                    response.state = "00";
                    response.message = "PROCESO RERALIZADO CORRECTAMENTE";
                }
                else
                {
                    response.state = "01";
                    response.message = "ERROR AL REALIZAR LA INSERCION";
                }
            }
            catch (Exception ex)
            {
                response.state = "99";
                response.message = ex.Message;
            }
            return response;
        }
        public RegistroSwampCuentaProductoResponse RegistroSwampCuentaProducto(RegistroSwampCuentaProductoRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            RegistroSwampCuentaProductoResponse response = new RegistroSwampCuentaProductoResponse();
            response.operation = operacion;
            try
            {
                CuentaProductoAddRequest request1 = new CuentaProductoAddRequest 
                {
                    guid=request.data.ses_guid,
                    id=request.data.cta_id,
                    prodId=request.data.pro_id,
                    monto=request.data.pro_monto,
                    clave=request.data.pro_clave,
                    codMoneda=request.data.pro_codmoneda,
                    codTipoProducto=request.data.pro_codtipoproducto,
                    subCodTipoPorProducto=request.data.pro_subcodtipoproducto,
                    nueva=request.data.pro_nueva,
                    numeroCuenta=request.data.pro_numerocuenta,
                    tipoDPF=request.data.pro_tipodpf,
                    tipoPlazo=request.data.pro_tipoplazo,
                    usrCrea=request.usuario
                };
                CuentaProductoAddResponse response1 = this._swampCore.consultaPOSTSwampCore<CuentaProductoAddResponse>(this._configuracion.configuracionSwampCore.metodoCuentaProductoAdd, request1, auth, canal, token, usuario);
                if (response1.state==200)
                {
                    response.success = true;
                    response.state = "00";
                    response.message = "PROCESO RERALIZADO CORRECTAMENTE";
                }
                else
                {
                    response.state = "01";
                    response.message = "ERROR AL REALIZAR LA INSERCION";
                }
            }
            catch (Exception ex)
            {
                response.state = "99";
                response.message = ex.Message;
            }
            return response;
        }
        #endregion

        #region SECCION: SMART-LINK
        public ValidacionPinResponse ValidacionPin(ValidacionPinRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            ValidacionPinResponse response = new ValidacionPinResponse();
            try
            {                
                request.data.tarjeta = request.data.tarjeta.Replace("-","");
                SwampCoreResponse consulta = this._swampCore.consultaPOSTSwampCore<SwampCoreResponse>(this._configuracion.configuracionSwampCore.metodoValidacionPin, request.data, auth, canal, token, usuario);
                if (consulta.state == 200)
                {
                    response.success = true;
                    response.state = "00";
                    response.message = "OPERACION EJECUTADA CORRECTAMENTE";
                }
                else
                {
                    response.message = consulta.message;
                    response.state = "01";
                }
            }
            catch (Exception )
            {
                throw;
            }
            return response;
        }
        public TarjetaActualizacionResponse TarjetaActualizacion(TarjetaActualizacionRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            TarjetaActualizacionResponse response = new TarjetaActualizacionResponse();
            try
            {
                ActualizarTarjetaRequest request1 = new ActualizarTarjetaRequest
                {
                    tarjeta = request.data.tarjeta.Replace("-",""),
                    cta1=ManagerValidation.GetDefaultValue(request.data.cuenta_ch_mn.Replace("-",""),""),
                    cta2 = ManagerValidation.GetDefaultValue(request.data.cuenta_ch_me.Replace("-", ""), ""),
                    cta3 = ManagerValidation.GetDefaultValue(request.data.cuenta_cc_mn.Replace("-", ""), ""),
                    cta4 = ManagerValidation.GetDefaultValue(request.data.cuenta_cc_me.Replace("-", ""), "")
                };
                ActualizarTarjetaResponse response1 = this._swampCore.consultaPOSTSwampCore<ActualizarTarjetaResponse>(this._configuracion.configuracionSwampCore.metodoActualizarTarjeta, request1, auth, canal, token, usuario);
               if (response1.state==200)
                {
                    response.state = "00";
                    response.success = true;
                    response.message = "PROCESO EJECUTADO CORRECTAMENTE";
                }
                else
                {
                    response.state = "01";
                    response.message = response1.message;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public TarjetaDesafiliacionCuentaResponse DesafiliarCuenta(TarjetaDesafiliacionCuentaRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            TarjetaDesafiliacionCuentaResponse response = new TarjetaDesafiliacionCuentaResponse();
            try
            {
                request.data.tarjeta = request.data.tarjeta.Replace("-", "");
                request.data.cuenta = request.data.cuenta.Replace("-", "");

                SwampCoreResponse respuesta = this._swampCore.consultaPOSTSwampCore<SwampCoreResponse>(this._configuracion.configuracionSwampCore.metodoDesafiliacionCuenta, request.data, auth, canal, token, usuario);
                if (respuesta.state == 200)
                {
                    response.state = "00";
                    response.success = true;
                    response.message = "PROCESO REALIZADO CORRECTAMENTE";
                }
                else
                {
                    response.state = "01";
                    response.message = respuesta.message;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public TarjetaBloqueoResponse TarjetaBloqueo(TarjetaBloqueoRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            TarjetaBloqueoResponse response = new TarjetaBloqueoResponse();
            try
            {
                BloqueoTarjetaRequest request1 = new BloqueoTarjetaRequest
                {
                    tarjeta=request.data.tarjeta.Replace("-",""),
                    situacion="B"
                };
                BloqueoTarjetaResponse response1 = this._swampCore.consultaPOSTSwampCore<BloqueoTarjetaResponse>(this._configuracion.configuracionSwampCore.metodoBloqueoTarjeta, request1, auth, canal, token, usuario);
                if (response1.state==200)
                {
                    response.state = "00";
                    response.success = true;
                    response.message = "PROCESO REALIZADO CORRECTAMENTE";
                }
                else
                {
                    response.state = "01";
                    response.message = response1.message;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public TarjetaCambioResponse TarjetaCambio(TarjetaCambioRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            TarjetaCambioResponse response = new TarjetaCambioResponse();
            try
            {
                TarjetaActualizacionRequest dataCambio = new TarjetaActualizacionRequest
                {
                    operation = operacion,
                    data = new TarjetaActualizacionRequestData
                    {
                        tarjeta = request.data.tarjetaNueva,
                        cuenta_cc_me = request.data.cuenta_cc_me,
                        cuenta_cc_mn = request.data.cuenta_cc_mn,
                        cuenta_ch_me = request.data.cuenta_ch_me,
                        cuenta_ch_mn = request.data.cuenta_ch_mn
                    }
                };
                TarjetaActualizacionResponse cambio = TarjetaActualizacion(dataCambio, canal, usuario, operacion,auth,token);
                if (cambio.success)
                {
                    if (!string.IsNullOrEmpty(request.data.tarjetaAntigua))
                    {
                        TarjetaBloqueoRequest bloquear = new TarjetaBloqueoRequest
                        {
                            operation = operacion,
                            data = new TarjetaBloqueoRequestData
                            {
                                tarjeta = request.data.tarjetaAntigua
                            }
                        };
                        TarjetaBloqueoResponse respuesta = TarjetaBloqueo(bloquear, canal, usuario, operacion,auth,token);
                        if (respuesta.success)
                        {
                            response.state = "00";
                            response.success = true;
                            response.message = "PROCESO REALIZADO CORRECTAMENTE";
                        }
                        else
                        {
                            response.state = "02";
                            response.message = respuesta.message;
                        }
                    }
                    else
                    {
                        response.state = "00";
                        response.success = true;
                        response.message = "PROCESO REALIZADO CORRECTAMENTE";
                    }
                }
                else
                {
                    response.state = "01";
                    response.message = cambio.message;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public CuentaRegistroResponse OperacionCuenta(CuentaRegistroRequest request, string canal, string usuario, string operacion, string auth, string token)
        {
            CuentaRegistroResponse response = new CuentaRegistroResponse();
            try
            {
                CambioAperturaRequest cambio = new CambioAperturaRequest
                {
                    tarjeta = request.data.tarjeta.Replace("-", ""),
                    cuenta = request.data.cuenta.Replace("-", ""),
                    cliente = request.data.nombreCliente
                };
                CambioAperturaResponse response1 = this._swampCore.consultaPOSTSwampCore<CambioAperturaResponse>(this._configuracion.configuracionSwampCore.metodoCambioApertura, cambio, auth, canal, token, usuario);
                if (response1.state == 200)
                {
                    response.success = true;
                    response.state = "00";
                    response.message = "OPERACION EJECUTADA CORRECTAMENTE";
                }
                else
                {
                    response.state = "01";
                    response.message =response1.message;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region SECCION: SEGUROS
        public GeneraContratoResponse GenerarContrato(GeneraContratoRequest request, string canal, string usuario, string operacion)
        {
            GeneraContratoResponse response = new GeneraContratoResponse();
            try
            {
                ConnectorSeguros seguros = new ConnectorSeguros(this._configuracion.configuracionSeguros.url, this._configuracion.configuracionSeguros.usuario, this._configuracion.configuracionSeguros.contrasena,request.data.IpOrigen);
                SegurosServiceModel.GetCertificadoRequestModel req= new();
                req.IdProducto = request.data.CodigoProducto;
                req.IdPersona = request.data.IdPersona;
                req.IdAfiliacion = request.data.IdAfiliacion;
                SegurosServiceModel.GetCertificadoResponseModel res = seguros.GenerarContrato(req);
                response.message = res.MENSAJE_ERROR;
                response.state = "01";
                if (res.CODIGO_ERROR.Equals("200"))
                {
                    string image = Convert.ToBase64String(res.IMAGEN);
                    if (!string.IsNullOrEmpty(image))
                    {
                        response.success = true;
                        response.state = "00";
                        response.message = "Operacion realizada exitosamente";
                        response.data = new GeneraContratoResponseData
                        {
                            PDF = image
                        };
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public string ValidarCodigo(string Codigo)
        {
            string IdCodigo = "";
            var codigos = this._configuracion.configuracionSeguros.Codes;
            codigos.ForEach(code =>
            {
                if (code.IdProducto == Codigo)
                {
                    IdCodigo = code.Codigo;
                }
            });
            return IdCodigo;
        }
        public RegistrarSeguroResponse RegistrarSeguro(SegurosServiceModel.AfiliacionRequestModel request, string canal, string usuario, string operacion)
        {

            RegistrarSeguroResponse response = new();            
            try
            {
                SegurosServiceModel.AfiliacionResponseModel res = new();
                ConnectorSeguros seguros = new ConnectorSeguros(this._configuracion.configuracionSeguros.url, this._configuracion.configuracionSeguros.usuario, this._configuracion.configuracionSeguros.contrasena, request.UserCredenciales.Ip);
                res = seguros.RegistrarSeguro(request);
                string mensaje = res.MENSAJE_ERROR;
                if (res.CODIGO_ERROR.Equals("200"))
                {
                    response.success = true;
                }
                else
                {
                    response.success = false;
                }
                response.message = mensaje;
                response.state = response.success ?"00":"01";
                if (response.success)
                {
                    response.message = "Operacion realizada exitosamente";
                    response.data = new RegistrarSeguroResponseData 
                    {
                        IdAfiliacion = res.ID_AFILIACION,
                        IdCliente=res.ID_CLIENTE
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion
    }
}