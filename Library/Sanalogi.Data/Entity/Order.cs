using Sanalogi.Core.Entity;
using System;

namespace Sanalogi.Data.Entity
{
    public class Order:BaseEntity
    {
        public string OrderName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderingCompany { get; set; }
    }
}
