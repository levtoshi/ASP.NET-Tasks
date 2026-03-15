using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Chats;
using TarasMessanger.Core.Entities;
using TarasMessanger.Core.Entities.Message;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Storage.Repositories
{
    public class PrivateChatRepository : IPrivateChatRepository
    {
        private readonly ApplicationDbContext _context;

        public PrivateChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(PrivateChat message)
        {
            _context.Add(message);
        }

        public async Task<List<PrivateChatDto>> GetPrivateChats(string userId, int offset = 0, int limit = 10)
        {
            var query = await _context.PrivateChats
                .AsNoTracking()
                .Where(x => x.SenderId == userId || x.ReceiverId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .Select(x => new PrivateChatDto
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    SenderNickname = _context.Users.Where(s => s.Id == x.SenderId).Select(s => s.Nickname).FirstOrDefault(),
                    ReceiverId = x.ReceiverId,
                    ReceiverNickname = _context.Users.Where(r => r.Id == x.ReceiverId).Select(r => r.Nickname).FirstOrDefault(),
                    CreatedAt = x.CreatedAt
                })
                .Reverse()
                .ToListAsync();

            return query;
        }

        public async Task<PrivateChatDto> GetPrivateChat(Guid chatId)
        {
            var query = await _context.PrivateChats
                .AsNoTracking()
                .Where(x => x.Id == chatId)
                .Select(x => new PrivateChatDto
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    SenderNickname = _context.Users.Where(s => s.Id == x.SenderId).Select(s => s.Nickname).FirstOrDefault(),
                    ReceiverId = x.ReceiverId,
                    ReceiverNickname = _context.Users.Where(r => r.Id == x.ReceiverId).Select(r => r.Nickname).FirstOrDefault(),
                    CreatedAt = x.CreatedAt
                })
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task<bool> IsPrivateChatExistsAsync(string senderId, string receiverId)
        {
            var exists = await _context.PrivateChats
                .AsNoTracking()
                .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || (x.ReceiverId == senderId && x.SenderId == receiverId) )
                .AnyAsync();

            return exists;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
