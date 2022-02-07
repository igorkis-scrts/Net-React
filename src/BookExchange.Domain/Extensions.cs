﻿using BookExchange.Domain.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BookExchange.Domain
{
     public static class Extensions
     {
          public delegate Expression<Func<T, bool>> ExpressionOperation<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2);

          public static Expression<Func<T, bool>> GetTrueExpression<T>()
          {
               return a => true;
          }
          
          public static Expression<Func<T, bool>> CombineExpresions<T>(this List<Expression<Func<T, bool>>> expressions, LogicalOperator combineOperator)
          {
               ExpressionOperation<T> applyOperator;

               switch (combineOperator)
               {
                    case LogicalOperator.Or:
                         applyOperator = OrElse<T>;
                         break;
                    default:
                         applyOperator = AndAlso<T>;
                         break;
               };

               if (expressions == null || expressions.Count == 0) return (a => true);

               var result = expressions.Aggregate((current, expression) => applyOperator(current, expression));

               return result;
          }
          public static Expression<Func<T, bool>> AndAlso<T>(
              this Expression<Func<T, bool>> expr1,
              Expression<Func<T, bool>> expr2)
          {
               var parameter = Expression.Parameter(typeof(T));

               var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
               var left = leftVisitor.Visit(expr1.Body);

               var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
               var right = rightVisitor.Visit(expr2.Body);

               return Expression.Lambda<Func<T, bool>>(
                   Expression.AndAlso(left, right), parameter);
          }

          public static Expression<Func<T, bool>> OrElse<T>(
              this Expression<Func<T, bool>> expr1,
              Expression<Func<T, bool>> expr2)
          {
               var parameter = Expression.Parameter(typeof(T));

               var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
               var left = leftVisitor.Visit(expr1.Body);

               var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
               var right = rightVisitor.Visit(expr2.Body);

               return Expression.Lambda<Func<T, bool>>(
                   Expression.OrElse(left, right), parameter);
          }



          private class ReplaceExpressionVisitor
              : ExpressionVisitor
          {
               private readonly Expression _oldValue;
               private readonly Expression _newValue;

               public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
               {
                    _oldValue = oldValue;
                    _newValue = newValue;
               }

               public override Expression Visit(Expression node)
               {
                    if (node == _oldValue)
                         return _newValue;
                    return base.Visit(node);
               }
          }
     }
}
