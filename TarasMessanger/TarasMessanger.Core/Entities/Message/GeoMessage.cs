using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Core.Entities.Message
{
    public class GeoMessage : MessageBase
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
