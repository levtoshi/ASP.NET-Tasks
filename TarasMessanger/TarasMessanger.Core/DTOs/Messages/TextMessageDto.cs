using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.DTOs.Messages
{
    public class TextMessageDto : MessageDto
    {
        public string Text { get; set; }
    }
}
