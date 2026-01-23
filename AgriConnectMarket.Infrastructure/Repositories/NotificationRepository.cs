using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext _dbContext) : base(_dbContext) { }
        public async Task<IEnumerable<Notification>> GetNotificationsByProfileIdAsync(Guid profileId, bool includeProfile = false, bool includeOrder = false)
        {
            var query = _dbContext.Set<Notification>().Where(n => n.ProfileId == profileId && !n.IsDeleted);

            if (includeProfile)
            {
                query = query.Include(n => n.Profile);
            }

            if (includeOrder)
            {
                query = query.Include(n => n.Order);
            }

            return await query.ToListAsync();
        }
    }
}
