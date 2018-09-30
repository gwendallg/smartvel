using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SmartVel.Models;
using SQLite;

namespace SmartVel.Services
{
    public interface IDbGenericService<TEntity> : IDbService where TEntity : Entity, new()
    {
        /// <summary>
        /// retourne en liste d'entité en fonction de critère
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        IList<TEntity> Where(Expression<Func<TEntity, bool>> predicate, SQLiteConnection connection = null);

        /// <summary>
        /// retourne l'entité en fonction de sa clé primaire
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        TEntity GetById(int id, SQLiteConnection connection = null);

        /// <summary>
        /// ajoute une nouvelle entité au dépôt
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity, SQLiteConnection connection = null);

        /// <summary>
        /// met à jour l'entité
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity, SQLiteConnection connection = null);


        /// <summary>
        /// supprime l'entité
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        TEntity Delete(TEntity entity, SQLiteConnection connection = null);

    }
}
