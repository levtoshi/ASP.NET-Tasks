using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.DTOs.Chats
{
    public class PrivateChatDto
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string SenderNickname { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverNickname { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
