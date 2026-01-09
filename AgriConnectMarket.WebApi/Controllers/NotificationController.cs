using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController(NotificationService _notificationService, FarmService _farmService, ProfileService _profileService) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetMyNotifications(CancellationToken ct)
        {
            var result = await _notificationService.GetUserNotificationsAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }
        [HttpPost("review")]
        public async Task<IActionResult> CreateReviewNotification(ControllerCreateReviewNotificationDto dto, CancellationToken ct)
        {
            var farm = await _farmService.GetFarmById(dto.ReceiverId);

            var farmerProfile = await _profileService.GetProfileByAccountId(farm.Value.FarmerId);
            var createNotificationDto = new CreateNotificationDto(
                type: dto.Type,
                profileId: farmerProfile.Value.Id
            );

            var result = await _notificationService.CreateNotificationAsync(createNotificationDto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }
        [HttpPost("reply")]
        public async Task<IActionResult> CreateReplyNotification(ControllerCreateReplyNotificationDto dto, CancellationToken ct)
        {
            var createNotificationDto = new CreateNotificationDto(
                type: dto.Type,
                profileId: dto.ReceiverId
            );

            var result = await _notificationService.CreateNotificationAsync(createNotificationDto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }
        [HttpPatch("{notificationId}")]
        public async Task<IActionResult> ChangeReadStatus([FromRoute] Guid notificationId, CancellationToken ct)
        {
            var result = await _notificationService.ChangeReadStatus(notificationId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }
    }
}
