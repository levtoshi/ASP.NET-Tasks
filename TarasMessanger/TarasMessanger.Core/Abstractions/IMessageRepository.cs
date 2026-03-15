using TarasMessanger.Core.DTOs.Chats;
using TarasMessanger.Core.DTOs.Messages;
using TarasMessanger.Core.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.Abstractions
{
    public interface IMessageRepository
    {
        void Add(MessageBase message);
        Task<List<MessageDto>> GetMessages(Guid chatId, int limit, int offset);
        Task<MessageDto> GetMessage(Guid id);
        Task<int> SaveChangesAsync();
    }
}
