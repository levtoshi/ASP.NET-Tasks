using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.DTOs.Messages
{
    public abstract class MessageDto
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
