using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Messages;
using TarasMessanger.Core.Entities.Message;
using Microsoft.AspNetCore.SignalR;

namespace TarasMessanger.Services.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public MessageHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task JoinPrivateChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"private-chat-{chatId}");
        }

        public async Task LeavePrivateChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"private-chat-{chatId}");
        }

        public async Task SendTextMessage(TextMessage message)
        {
            TextMessageDto textMessageDto = (await _messageRepository.GetMessage(message.Id) as TextMessageDto);
            await Clients.Group($"private-chat-{message.ChatId}").SendAsync("ReceiveTextMessage", textMessageDto);
        }

        public async Task SendFileMessage(FileMessage message)
        {
            FileMessageDto fileMessageDto = (await _messageRepository.GetMessage(message.Id) as FileMessageDto);
            await Clients.Group($"private-chat-{message.ChatId}").SendAsync("ReceiveFileMessage", fileMessageDto);
        }

        public async Task SendGeoMessage(GeoMessage message)
        {
            GeoMessageDto geoMessageDto = (await _messageRepository.GetMessage(message.Id) as GeoMessageDto);
            await Clients.Group($"private-chat-{message.ChatId}").SendAsync("ReceiveGeoMessage", geoMessageDto);
        }
    }
}
