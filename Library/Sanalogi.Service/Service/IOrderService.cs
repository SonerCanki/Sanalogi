using Sanalogi.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sanalogi.Service.Service
{
    public interface IOrderService
    {
        Task<Order> Add(Order item);
        Task<Order> Update(Order item);
        Task<bool> Remove(Order item);
        Task<Order> GetById(Guid id, params Expression<Func<Order, object>>[] includeParameters);
        Task<Order> GetByDefault(Expression<Func<Order, bool>> exp, params Expression<Func<Order, object>>[] includeParameters);
        IQueryable<Order> GetDefault(Expression<Func<Order, bool>> exp, params Expression<Func<Order, object>>[] includeParameters);
        Task<bool> Any(Expression<Func<Order, bool>> exp);
        Task<int> Save();
        IQueryable<Order> Table { get; }
        IQueryable<Order> TableNoTracking { get; }
    }
}
