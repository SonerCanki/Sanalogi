using System;

namespace Sanalogi.UI.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        public string OrderName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderingCompany { get; set; }
    }
}
