using AgriConnectMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateNotificationDto
    {
        public string Type { get; set; }
        public Guid ProfileId { get; set; }
        public Guid? OrderId { get; set; }
        public CreateNotificationDto(string type, Guid profileId, Guid? orderId = null)
        {
            Type = type;
            ProfileId = profileId;
            OrderId = orderId;
        }
    }
}
