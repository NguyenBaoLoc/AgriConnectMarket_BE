using AgriConnectMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class ControllerCreateReplyNotificationDto
    {
        public string Type { get; set; }
        public Guid ReceiverId { get; set; }
        public ControllerCreateReplyNotificationDto(string type, Guid receiverId)
        {
            Type = type;
            ReceiverId = receiverId;
        }
    }
}
