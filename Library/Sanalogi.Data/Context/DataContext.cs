using Microsoft.EntityFrameworkCore;
using Sanalogi.Data.Entity;
using Sanalogi.Data.Map;

namespace Sanalogi.Data.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }


        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderMap());
        }
    }
}
