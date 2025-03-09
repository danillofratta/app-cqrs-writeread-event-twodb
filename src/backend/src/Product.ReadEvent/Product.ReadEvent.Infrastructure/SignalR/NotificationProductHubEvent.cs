using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadEvent.Infrastructure.SignalR
{
    public class NotificationProductHubEvent : Hub<IEmitProducts>
    {
    }

    public interface IEmitProducts
    {
        Task GetListPayment(List<ProductQueryDomainEntities.Product> list);
    }
}
