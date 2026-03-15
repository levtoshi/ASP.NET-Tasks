using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Chats;
using TarasMessanger.Core.Entities;
using TarasMessanger.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Services
{
    public class PrivateChatService
    {
        private readonly IPrivateChatRepository _privateChatRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public PrivateChatService(IPrivateChatRepository privateChatRepository, IHubContext<ChatHub> hubContext)
        {
            _privateChatRepository = privateChatRepository;
            _hubContext = hubContext;
        }

        public async Task<List<PrivateChatDto>> GetUserPrivateChats(string userId)
        {
            return await _privateChatRepository.GetPrivateChats(userId, 0, 10);
        }

        public async Task<PrivateChatDto> CreateNewPrivateChat(string senderId, string receiverId)
        {
            bool isExists = await _privateChatRepository.IsPrivateChatExistsAsync(senderId, receiverId);
            if (isExists)
            {
                throw new Exception("Chat already exists!");
            }

            var privateChat = new PrivateChat
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiverId,
                CreatedAt = DateTime.UtcNow
            };
            _privateChatRepository.Add(privateChat);
            await _privateChatRepository.SaveChangesAsync();

            return await _privateChatRepository.GetPrivateChat(privateChat.Id);
        }

        public async Task<PrivateChatDto?> GetUserPrivateChat(Guid chatId, string userId)
        {
            PrivateChatDto chat =  await _privateChatRepository.GetPrivateChat(chatId);
            if (chat == null || (chat.SenderId != userId && chat.ReceiverId != userId) )
            {
                return null;
            }

            return chat;
        }
    }
}
