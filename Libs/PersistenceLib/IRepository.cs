﻿// ----------------------------------------------------------------------
// <copyright file="IRepository.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace PersistenceLib
{
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    ///  Generic repository interface
    /// </summary>
    /// <typeparam name="TEntity">Type of repository entity</typeparam>
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int NumberofEntries();

        TEntity GetLast();
    }
}
