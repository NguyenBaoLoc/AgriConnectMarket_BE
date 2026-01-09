using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class NotificationService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        // Token required
        public async Task<Result<IEnumerable<Notification>>> GetUserNotificationsAsync(CancellationToken ct)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<IEnumerable<Notification>>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = (Guid)_currentUserService.UserId;
            var notifications = await _uow.NotificationRepository.GetNotificationsByProfileIdAsync(userId, includeOrder: true);

            if (!notifications.Any())
            {
                return Result<IEnumerable<Notification>>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
            }

            return Result<IEnumerable<Notification>>.Success(notifications);
        }
        public async Task<Result<CreateNotificationResponseDto>> CreateNotificationAsync(CreateNotificationDto dto, CancellationToken ct = default)
        {
            var (title, message) = GenerateNotificationContent(dto.Type);
            Notification entity;
            if (dto.Type == nameof(NotificationTypeEnum.ORDER_UPDATED) || dto.Type == nameof(NotificationTypeEnum.ORDER_PLACED))
            {
                entity = new Notification(
                    title,
                    message,
                    dto.Type,
                    profileId: dto.ProfileId,
                    orderId: dto.OrderId
                );
            }
            else
            {
                entity = new Notification(
                    title,
                    message,
                    dto.Type,
                    profileId: dto.ProfileId
                );
            }

            await _uow.NotificationRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            var resultDto = new CreateNotificationResponseDto()
            {
                Title = title,
                Message = message,
                Type = dto.Type,
                IsRead = entity.IsRead,
                IsDeleted = entity.IsDeleted,
                ProfileId = dto.ProfileId,
                OrderId = dto.OrderId,
            };

            return Result<CreateNotificationResponseDto>.Success(resultDto);
        }
        public async Task<Result<Guid>> ChangeReadStatus(Guid notificationId, CancellationToken ct = default)
        {
            var existing = await _uow.NotificationRepository.GetByIdAsync(notificationId, ct);

            if (existing is null)
            {
                return Result<Guid>.Fail(MessageConstant.NOTIFICATION_NOT_FOUND);
            }

            existing.IsRead = !existing.IsRead;

            await _uow.NotificationRepository.UpdateAsync(existing, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<Guid>.Success(notificationId);
        }
        private static (string Title, string Message) GenerateNotificationContent(string type)
        {
            return type switch
            {
                nameof(NotificationTypeEnum.VOUCHER) => ("You have a new voucher", "Save more on your purchase with the new available voucher."),
                nameof(NotificationTypeEnum.ORDER_PLACED) => ("New order", "A new order has been placed."),
                nameof(NotificationTypeEnum.ORDER_UPDATED) => ("Order status change", "Your order has been been updated, view it's status by clicking on View Order."),
                nameof(NotificationTypeEnum.REVIEW) => ("New review", "You have a new review from a customer."),
                nameof(NotificationTypeEnum.REPLY) => ("New reply", "You review on a product has been replied by the farmer of that product."),
                _ => ("Notification", "You have a new notification.")
            };
        }
    }
}
