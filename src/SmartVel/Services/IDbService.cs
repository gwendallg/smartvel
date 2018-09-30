using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SmartVel.Services
{
    public interface IDbService
    {
        /// <summary>
        /// créé une nouvelle connexion
        /// </summary>
        /// <returns></returns>
        SQLiteConnection CreateConnection();

        IList<T> Query<T>(string query, SQLiteConnection connection = null, params object[] args) where T : new();
    }
}
