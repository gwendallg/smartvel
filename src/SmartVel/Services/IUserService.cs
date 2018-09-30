using System.Threading.Tasks;

namespace SmartVel.Services
{
    /// <summary>
    /// interface de service de gestion d'utilisateur
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// authentifie l'utilisateur
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="remember"></param>
        /// <returns></returns>
        Task ConnectAsync(string login, string password, bool remember);

        /// <summary>
        /// deconnecte l'utilisateur
        /// </summary>
        /// <returns></returns>
        Task DisconnectAsync();
    }
}
