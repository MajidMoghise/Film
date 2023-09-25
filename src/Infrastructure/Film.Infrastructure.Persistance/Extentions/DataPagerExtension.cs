using Film.Domain.Contract.Base.Models;
using Film.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Extentions
{
    public static class DataPagerExtension
    {
        internal static async Task<PageResponseModel<TEntity>> PagingAsync<TEntity>(this IQueryable<TEntity> query, PageRequestModel listModel, CancellationToken cancellationToken = default) where TEntity : BaseModel
        {
            var startRow = (listModel.PageIndex - 1) * listModel.PageSize;
            var totalItemsCountTask = await query.CountAsync(cancellationToken);

            var objects = await query
                       .Skip(startRow)
                       .Take(listModel.PageSize)
                       .ToListAsync(cancellationToken);

            var totalCount = totalItemsCountTask;
            double pg = (double)totalCount / listModel.PageSize;

            var pageCount = Convert.ToInt32(Math.Round(pg, MidpointRounding.ToPositiveInfinity));

            return new PageResponseModel<TEntity>(objects, totalCount, listModel.PageIndex, pageCount, listModel.PageSize);
        }

        internal static async Task<PageResponseModel<TEntity>> PagingAsync<TEntity>(this IQueryable<TEntity> query, int pageSize, int pageIndex, CancellationToken cancellationToken) where TEntity : BaseModel
        {
            var startRow = (pageIndex - 1) * pageSize;
            var totalItemsCountTask = await query.CountAsync();


            var objects = await query
                       .Skip(startRow)
                       .Take(pageSize)
                       .ToListAsync(cancellationToken);

            var totalCount = totalItemsCountTask;
            double pg = (double)totalCount / pageSize;

            var pageCount = Convert.ToInt32(Math.Round(pg, MidpointRounding.ToPositiveInfinity));

            return new PageResponseModel<TEntity>(objects, totalCount, pageIndex, pageCount, pageSize);
        }

        internal static async Task<PageResponseModel<TEntity>> PagingAsync<TEntity>(this IQueryable<TEntity> query, int pageSize, int pageIndex) where TEntity : BaseModel
        {
            var startRow = (pageIndex - 1) * pageSize;
            var totalItemsCountTask = await query.CountAsync();


            var objects = await query
                       .Skip(startRow)
                       .Take(pageSize)
                       .ToListAsync();

            var totalCount = totalItemsCountTask;
            double pg = (double)totalCount / pageSize;

            var pageCount = Convert.ToInt32(Math.Round(pg, MidpointRounding.ToPositiveInfinity));

            return new PageResponseModel<TEntity>(objects, totalCount, pageSize, pageCount, pageSize);
        }

        internal static IQueryable<T> SortDynamic<T>(this IQueryable<T> query, List<string> orderByAsces, List<string> orderByDesces)
        {
            if (orderByAsces is null && orderByDesces is null)
                query = ApplyOrder(query, "CreatedOn", "OrderBy");

            var entityPropertyNames = (typeof(T).GetProperties()).Select(p => p.Name);

            if (orderByAsces?.Any() == true)
            {
                for (int i = 0; i < orderByAsces.Count; i++)
                {
                    if (!entityPropertyNames.Contains(orderByAsces[i]))
                        continue;

                    var item = orderByAsces[i];
                    if (i == 0)
                    {
                        query = ApplyOrder(query, item, "OrderBy");
                    }
                    else
                    {
                        query = ApplyOrder(query, item, "ThenBy");
                    }
                }
            }

            if (orderByDesces?.Any() == true)
            {
                for (int i = 0; i < orderByDesces.Count; i++)
                {
                    if (!entityPropertyNames.Contains(orderByDesces[i]))
                        continue;

                    var item = orderByDesces[i];
                    if (i == 0)
                    {
                        query = ApplyOrder(query, item, "OrderByDescending");
                    }
                    else
                    {
                        query = ApplyOrder(query, item, "ThenByDescending");
                    }
                }
            }


            return query;
        }
        static IOrderedQueryable<T> ApplyOrder<T>(
        IQueryable<T> source,
        string property,
        string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {

                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
        public static IOrderedQueryable<T> OrderBy<T>(
        this IQueryable<T> source,
        string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source,
            string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }

        
    }

}
