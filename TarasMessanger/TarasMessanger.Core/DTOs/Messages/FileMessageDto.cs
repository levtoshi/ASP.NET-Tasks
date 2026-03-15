using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.DTOs.Messages
{
    public class FileMessageDto : MessageDto
    {
        public string? Text { get; set; }
        public required string FilePath { get; set; }
    }
}
