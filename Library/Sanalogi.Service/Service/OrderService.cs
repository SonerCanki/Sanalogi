using Microsoft.EntityFrameworkCore;
using Sanalogi.Data.Context;
using Sanalogi.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sanalogi.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        private DbSet<Order> _entities;

        public DbSet<Order> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<Order>();
                return _entities;
            }
        }

        public IQueryable<Order> Table => Entities;

        public IQueryable<Order> TableNoTracking => Entities.AsNoTracking();

        public async Task<Order> Add(Order item)
        {
            try
            {
                if (item == null)
                    return null;

                await Entities.AddAsync(item);

                if (await Save() > 0)
                    return item;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> Update(Order item)
        {
            try
            {
                if (item == null)
                    return null;

                Entities.Update(item);

                if (await Save() > 0)
                    return item;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Any(Expression<Func<Order, bool>> exp) => await Entities.AnyAsync(exp);

        public async Task<Order> GetByDefault(Expression<Func<Order, bool>> exp, params Expression<Func<Order, object>>[] includeParameters)
        {
            IQueryable<Order> queryable = TableNoTracking;
            foreach (Expression<Func<Order, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return await queryable.FirstOrDefaultAsync(exp);
        }

        public async Task<Order> GetById(Guid id, params Expression<Func<Order, object>>[] includeParameters)
        {
            IQueryable<Order> queryable = TableNoTracking;
            foreach (Expression<Func<Order, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return await queryable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Order> GetDefault(Expression<Func<Order, bool>> exp, params Expression<Func<Order, object>>[] includeParameters)
        {
            IQueryable<Order> queryable = TableNoTracking;
            foreach (Expression<Func<Order, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return queryable.Where(exp).AsQueryable();
        }

        public async Task<bool> Remove(Order item)
        {
            if (Entities.Remove(item) != null)
            {
               await Save();
                return true;
            }else
                return false;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

    }
}
