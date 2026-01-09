using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Domain.Entities
{
    public class Notification : BaseEntity<Guid>, IAuditableEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public Guid? ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public Notification(string title, string message, string type, bool isRead = false, bool isDeleted = false, Guid? profileId = null, Guid? orderId = null)
        {
            Guard.AgainstNullOrEmpty(title, nameof(title));
            Guard.AgainstNullOrEmpty(message, nameof(message));
            Guard.AgainstNullOrEmpty(type, nameof(type));

            this.Title = title;
            this.Message = message;
            this.Type = type;
            this.IsRead = isRead;
            this.IsDeleted = false;

            if (profileId is not null)
            {
                this.ProfileId = profileId;
            }
            if (orderId is not null)
            {
                this.OrderId = orderId;
            }
        }
    }
}
