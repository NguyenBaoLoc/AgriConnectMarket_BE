using AgriConnectMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        public Task<IEnumerable<Notification>> GetNotificationsByProfileIdAsync(Guid profileId, bool includeProfile = false, bool includeOrder = false);
    }
}
