using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Storage.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; }
    }
}
