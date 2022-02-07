﻿using AutoMapper;
using NetReact.Domain.Filter;
using NetReact.Domain.Models;
using NetReact.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetReact.Domain.Interfaces
{
     public interface IRepositoryBase<T> where T : BaseEntity
     {
          T GetById(int id);
          public T GetByIdWithInclude(int id, params Expression<Func<T, object>>[] includeProperties);
          List<T> GetAll();
          List<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties);
          List<T> GetAllByCondition(Expression<Func<T, bool>> predicate);
          List<T> GetAllByConditions(List<Expression<Func<T, bool>>> predicate, List<Expression<Func<T, Object>>> includes, LogicalOperator predicateLogicalOperators);
          List<T> GetAllByConditionWithInclude(Expression<Func<T, bool>> predicate,
               params Expression<Func<T, object>>[] includeProperties);
          PagedResponse<TDto> GetPagedData<TDto>(List<Expression<Func<T, bool>>> predicates, 
               List<Expression<Func<T, object>>> includes, PaginationFilter paginationFilter, IMapper mapper);
          bool SaveAll();
          void SaveAllWithIdentityInsert();
          void Add(T entity); // insert the item into the dbSet
          void Update(T entity);
          T Delete(int id);   // remove item from the DbSet
     }
}

