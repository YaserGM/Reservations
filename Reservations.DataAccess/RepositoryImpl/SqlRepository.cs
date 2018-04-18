// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlRepository.cs" company="ISUCorp">
//   ISUCorp 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Reservations.Core.Definitions;
using Reservations.DataAccess.Contracts;

namespace Reservations.DataAccess.RepositoryImpl
{
    #region NameSpaces

    using System;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    #endregion

    /// <inheritdoc />
    /// <summary>
    ///     The sql repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class SqlRepository<T> : IRepository<T>
        where T : class, IDataBaseEntity
    {
        #region Constructors and Desctructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRepository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public SqlRepository(ObjectContext context)
        {
            this.ObjectSet = context.CreateObjectSet<T>();
        }

        #endregion

        #region Private Properties

        /// <summary>
        ///     Gets the object set.
        /// </summary>
        private ObjectSet<T> ObjectSet { get; }

        #endregion

        #region IRepository<T> Members

        /// <inheritdoc />
        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        public void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The remove.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        public void Delete(T entity)
        {
            this.ObjectSet.DeleteObject(entity);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The find.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Linq.IQueryable" />.
        /// </returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.ObjectSet.Where(predicate);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The find.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Linq.IQueryable" />.
        /// </returns>
        public IQueryable<T> Find(string predicate)
        {
            return this.ObjectSet.Where(predicate);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The find all.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Linq.IQueryable" />.
        /// </returns>
        public IQueryable<T> GetAll()
        {
            return this.ObjectSet;
        }

        /// <inheritdoc />
        /// <summary>
        /// The find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="!:T" />.
        /// </returns>
        public T Get(int id)
        {
            return this.ObjectSet.FirstOrDefault(t => t.Id == id);
        }

        #endregion
    }
}