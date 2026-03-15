using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.Entities.Message
{
    public class FileMessage : MessageBase
    {
        public string Text { get; set; }
        public required string FilePath { get; set; }
    }
}
