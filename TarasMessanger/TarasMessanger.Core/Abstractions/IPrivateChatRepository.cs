using TarasMessanger.Core.DTOs.Chats;
using TarasMessanger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.Abstractions
{
    public interface IPrivateChatRepository
    {
        void Add(PrivateChat chat);
        Task<List<PrivateChatDto>> GetPrivateChats(string userId, int offset, int limit);
        Task<PrivateChatDto> GetPrivateChat(Guid chatId);
        Task<bool> IsPrivateChatExistsAsync(string senderId, string receiverId);
        Task<int> SaveChangesAsync();
    }
}
