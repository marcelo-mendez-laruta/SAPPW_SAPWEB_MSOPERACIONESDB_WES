using BCP.Sap.Models.Configuracion;
using BCP.Sap.Models.His;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Business
{
    public class BusinessHis
    {
        private ConfiguracionSocket _configuracion;
        public BusinessHis(ConfiguracionSocket usuario)
        {
            this._configuracion = usuario;
        }

        public bool AdapterAvailable(string _HostURI, int _PortNumber, string operacion)
        {
            var client = new TcpClient();
            try
            {
                var result = client.BeginConnect(_HostURI, _PortNumber, null, null);

                result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));
                if (!client.Connected)
                {
                    client.Close();
                    return false;
                }
                else
                {
                    client.Close();
                    return true;
                }
            }
            catch (SocketException ex)
            {
                client.Close();
                return false;
            }
        }

        public HisResponse ExecuteQuery(HisRequest objRequest)
        {
            string modo = "";
            HisResponse objResponse = new HisResponse();
            if (!string.IsNullOrEmpty(objRequest.ip) && !string.IsNullOrEmpty(objRequest.puerto) && !string.IsNullOrEmpty(objRequest.puerto_respaldo))
            {
                using (objResponse = new HisResponse())
                {

                    string user = this._configuracion.usuario.PadLeft(32, ' ');
                    string header = user + "                         0.0.0.0";
                    string _ip = string.Empty;
                    string _puerto = string.Empty;


                    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.SendTimeout = 50000;
                    clientSocket.ReceiveTimeout = 50000;
                    bool sinConexion = false;
                    try
                    {
                        if (AdapterAvailable(objRequest.ip, int.Parse(objRequest.puerto), objRequest.operacion))
                        {
                            _ip = objRequest.ip;
                            _puerto = objRequest.puerto;
                        }
                        else if (AdapterAvailable(objRequest.ip, int.Parse(objRequest.puerto_respaldo), objRequest.operacion))
                        {
                            _ip = objRequest.ip;
                            _puerto = objRequest.puerto_respaldo;
                        }
                        else
                        {
                            sinConexion = true;
                        }

                        if (sinConexion)
                        {
                            objResponse.code = "500";
                            objResponse.message = "NO SE PUDO CONECTAR AL SERVIDOR SNA.";
                            objResponse.strTramaOut = string.Empty;
                            objResponse.success = false;
                            return objResponse;
                        }
                        else
                        {
                            string trama = header + objRequest.trama;
                            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse(_ip), int.Parse(_puerto));
                            clientSocket.Connect(miDireccion);
                            clientSocket.Send(Encoding.ASCII.GetBytes(trama));
                            byte[] data = new byte[10048];
                            int length = clientSocket.Receive(data);
                            string Received = Encoding.ASCII.GetString(data, 0, length);
#if DEBUG
                            string caracteres = string.Empty;
                            if (!string.IsNullOrEmpty(Received))
                            {
                                for (int i = 0; i < Received.Length - 1; i++)
                                {
                                    char character = Char.Parse(Received.Substring(i, 1));
                                    caracteres += "[" + Received.Substring(i, 1) + ":" + (int)character + "]";
                                }
                            }
                            string[] datos = Received.Split((char)63);
#endif
                            objResponse.strTramaOut = Received.Replace("\0", "");
                            objResponse.strMensajeOut = "";

                            if (objResponse.strTramaOut.Length >= 2)
                            {
                                switch (objResponse.strTramaOut.Substring(0, 2))
                                {
                                    case "00":
                                        if (objResponse.strTramaOut.Contains("F:ARCHIVO CERRADO"))
                                        {
                                            objResponse.code = objResponse.strTramaOut.Substring(0, 2);
                                            objResponse.message = objResponse.strTramaOut.Substring(2);
                                            objResponse.success = false;
                                        }
                                        else
                                        {
                                            objResponse.code = objResponse.strTramaOut.Substring(0, 2);
                                            objResponse.message = objResponse.strTramaOut;
                                            objResponse.success = true;
                                        }

                                        break;
                                    default:
                                        objResponse.code = objResponse.strTramaOut.Substring(0, 2);
                                        objResponse.message = objResponse.strTramaOut + ". " + objResponse.strMensajeOut.Trim();
                                        objResponse.success = false;
                                        break;
                                }
                            }
                            else
                            {
                                objResponse.code = "99";
                                objResponse.message = "";
                                objResponse.success = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        objResponse.code = "500";
                        objResponse.message = "NO SE PUDO CONECTAR AL ADAPTADOR SNA.";
                        objResponse.strTramaOut = string.Empty;
                        objResponse.success = false;
                    }
                    finally
                    {
                        clientSocket.Disconnect(false);
                        clientSocket.Close();
                        clientSocket.Dispose();
                    }
                    return objResponse;
                }
            }
            else
            {
                using (objResponse = new HisResponse())
                {
                    objResponse.strTramaOut = string.Empty;
                    objResponse.strMensajeOut = "No se especificó  la Ip o el puerto del servidor SNA.";
                    return objResponse;
                }
            }
            return objResponse;
        }
    }
}
