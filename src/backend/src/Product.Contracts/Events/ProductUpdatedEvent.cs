using Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Contracts.Events
{
    public class ProductUpdatedEvent : BaseEvent
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Status { get; set; }

        public DateTime? CommercializedAt { get; set; }
        public DateTime? CommercializedCancelledAt { get; set; }

        public string StatusCommercialization { get; set; }
    }
}
