using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreateNotificationResponseDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ProfileId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
