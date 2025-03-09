using Microsoft.AspNetCore.SignalR;
using Product.ReadEvent.Domain.Repository;
using Product.ReadEvent.Infrastructure.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadEvent.Consumer.Service
{
    public class GetListProductsHub
    {
        private readonly IHubContext<NotificationProductHubEvent> _hub;
        private readonly IProductReadEventQueryRepository _queryrepository;

        public GetListProductsHub(IHubContext<NotificationProductHubEvent> hub, IProductReadEventQueryRepository queryrepository)
        {
            _hub = hub;
            _queryrepository = queryrepository;
        }

        public async Task Execute()
        {
            IEnumerable<ProductQueryDomainEntities.Product> list = await _queryrepository.GetAllAsync();
            await _hub.Clients.All.SendAsync("GetList", list);
        }
    }
}
