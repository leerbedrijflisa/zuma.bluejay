using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lisa.Zuma.BlueJay.Web.Data
{
    /// <summary>
    /// Provides access to the underlying data layer.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all the entries in the entity specified by <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="filter">A filter expression filter the result with.</param>
        /// <param name="orderBy">An expression to order the result with.</param>
        /// <param name="includeProperties">A comma separated string containing other values to retrieve.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Gets the entity of type <typeparamref name="TEntity"/> as identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the <typeparamref name="TEntity"/> to retrieve.</param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Adds a <typeparamref name="TEntity"/> to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Deletes an <typeparamref name="TEntity"/>, identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the entity to remove.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);
    }
}
