using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmartVel.Models;
using SQLite;
using Xamarin.Forms;

namespace SmartVel.Services
{
    public class DbGenericService<TEntity> : DbService, IDbGenericService<TEntity> where TEntity : Entity, new()
    {
        #region Where

        public IList<TEntity> Where(Expression<Func<TEntity, bool>> predicate, SQLiteConnection connection = null)
        {
            if (connection != null)
                return OnWhere(predicate, connection);
            using (var innerConnection = CreateConnection())
            {
                return OnWhere(predicate, innerConnection);
            }
        }

        private static IList<TEntity> OnWhere(Expression<Func<TEntity, bool>> predicate, SQLiteConnection connection)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            return connection
                .Table<TEntity>()
                .Where(predicate)
                .ToList();
        }

        #endregion

        #region GetById

        public TEntity GetById(int id, SQLiteConnection connection = null)
        {
            if (connection != null)
                return OnGetById(id, connection);
            using (var innerConnection = CreateConnection())
            {
                return OnGetById(id, innerConnection);
            }
        }

        private static TEntity OnGetById(int id, SQLiteConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            return connection
                .Table<TEntity>()
                .SingleOrDefault(e => e.Id == id);
        }

        #endregion
        
        #region Insert

        public TEntity Insert(TEntity entity, SQLiteConnection connection = null)
        {
            if (connection != null)
                return OnInsert(entity, connection);
            using (var innerConnection = CreateConnection())
            {
                return OnInsert(entity, innerConnection);
            }
        }


        private static TEntity OnInsert(TEntity entity, SQLiteConnection connection)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            entity.CreatedDate = DateTime.UtcNow;
            entity.LastModifiedDate = entity.CreatedDate;
            entity.CreatedAt = Application.Current.GetCurrentUser()?.Login;
            entity.LastModifiedAt = entity.CreatedAt;
            connection
                .Insert(entity);
            return entity;
        }

        #endregion

        #region Update

        public TEntity Update(TEntity entity, SQLiteConnection connection = null)
        {
            if (connection != null)
                return OnUpdate(entity, connection);
            using (var innerConnection = CreateConnection())
            {
                return OnUpdate(entity, innerConnection);
            }
        }

        private static TEntity OnUpdate(TEntity entity, SQLiteConnection connection)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            entity.LastModifiedDate = entity.CreatedDate;
            entity.LastModifiedAt = Application.Current.GetCurrentUser()?.Login;
            connection
                .Update(entity);
            return entity;
        }

        #endregion

        #region Delete

        public TEntity Delete(TEntity entity, SQLiteConnection connection = null)
        {
            if (connection != null)
                return OnDelete(entity, connection);
            using (var innerConnection = CreateConnection())
            {
                return OnDelete(entity, innerConnection);
            }
        }

        private static TEntity OnDelete(TEntity entity, SQLiteConnection connection = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            connection.Delete(entity);
            return entity;
        }

        #endregion

    }
}
