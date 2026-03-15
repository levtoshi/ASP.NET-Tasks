using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Messages;
using TarasMessanger.Core.Entities.Message;
using TarasMessanger.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TarasMessanger.Core.Services
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IPrivateChatRepository _privateChatRepository;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public MessageService(
            IMessageRepository messageRepository, 
            IHubContext<NotificationHub> notificationHubContext, 
            IPrivateChatRepository privateChatRepository
        )
        {
            _messageRepository = messageRepository;
            _notificationHubContext = notificationHubContext;
            _privateChatRepository = privateChatRepository;
        }

        public async Task<TextMessage> SendTextMessage(string senderId, Guid chatId, string text)
        {
            var msg = new TextMessage
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                UserId = senderId,
                Text = text,
                CreatedAt = DateTime.UtcNow,
            };
            _messageRepository.Add(msg);
            await _messageRepository.SaveChangesAsync();
            await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification");

            return msg;
        }

        public async Task<FileMessage> SendFileMessage(string senderId, Guid chatId, string filePath, string? text)
        {
            var msg = new FileMessage
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                UserId = senderId,
                FilePath = filePath,
                Text = text,
                CreatedAt = DateTime.UtcNow,
            };
            _messageRepository.Add(msg);
            await _messageRepository.SaveChangesAsync();
            await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification");

            return msg;
        }

        public async Task<GeoMessage> SendGeoMessage(string senderId, Guid chatId, double latitude, double longitude)
        {
            var msg = new GeoMessage
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                UserId = senderId,
                Latitude = latitude,
                Longitude = longitude,
                CreatedAt = DateTime.UtcNow,
            };
            _messageRepository.Add(msg);
            await _messageRepository.SaveChangesAsync();
            await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification");

            return msg;
        }

        public async Task<List<MessageDto>> GetMessages(Guid chatId, int limit, int offset)
        {
            return await _messageRepository.GetMessages(chatId, limit, offset);
        }
    }
}
