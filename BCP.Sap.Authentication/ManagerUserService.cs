using BCP.Sap.Business;
using BCP.Sap.Models.Autorizacion;
using BCP.Sap.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Authentication
{
    public interface IManagerUserService
    {
        Task<BasicAutorizacionModel> Authenticate(string username, string password, string channel, string publicToken, string appUserId);
        Task<IEnumerable<BasicAutorizacionModel>> GetAll();
    }

    public class ManagerUserService : IManagerUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<BasicAutorizacionModel> _users = null;
        private ConfiguracionBase _configuracion;
        public ManagerUserService(ConfiguracionBase configuracion)
        {
            this._configuracion = configuracion;
        }

        public List<BasicAutorizacionModel> _usersA(string usuario, string password, string channel, string publicToken, string appUserId)
        {
            BusinessAutorizacion ss = new BusinessAutorizacion(this._configuracion.configuracionAutorizacion);
            return ss.getUsuarioN(usuario, password, channel, publicToken, appUserId);
        }

        public async Task<BasicAutorizacionModel> Authenticate(string username, string password, string channel, string publicToken, string appUserId)
        {
            _users = _usersA(username, password, channel, publicToken, appUserId);

            var user = await Task.Run(() => _users.SingleOrDefault(x => x.userName == username));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<BasicAutorizacionModel>> GetAll()
        {
            return await Task.Run(() => this._users.WithoutPasswords());
        }
    }

    public static class ExtensionMethods
    {
        public static IEnumerable<BasicAutorizacionModel> WithoutPasswords(this IEnumerable<BasicAutorizacionModel> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static BasicAutorizacionModel WithoutPassword(this BasicAutorizacionModel user)
        {
            user.password = null;
            return user;
        }
    }
}
