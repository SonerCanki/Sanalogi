using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sanalogi.Data.Entity;

namespace Sanalogi.Data.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.OrderName).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.OrderingCompany).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.Quantity).IsRequired(true);
            builder.Property(x => x.OrderDate).IsRequired(true);
        }
    }
}
