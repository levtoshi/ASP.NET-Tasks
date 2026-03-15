using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Chats;
using TarasMessanger.Core.DTOs.Messages;
using TarasMessanger.Core.Entities.Message;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Storage.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(MessageBase message)
        {
            _context.Add(message);
        }

        public async Task<List<MessageDto>> GetMessages(Guid chatId, int limit, int offset)
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.ChatId == chatId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .Join(_context.Users,
                    m => m.UserId,
                    u => u.Id,
                    (m, u) => MessageMapper.Map(m, u.Nickname)
                )
                .Reverse()
                .ToListAsync();
        }

        public async Task<MessageDto> GetMessage(Guid id)
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Join(_context.Users,
                    m => m.UserId,
                    u => u.Id,
                    (m, u) => MessageMapper.Map(m, u.Nickname)
                )
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
