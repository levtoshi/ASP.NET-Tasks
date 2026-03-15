using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Chats;
using TarasMessanger.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Services.Hubs
{
    public class ChatHub : Hub
    {
        public async Task AssignNewPrivateChat(PrivateChatDto privateChat)
        {
            // Broadcast a newly created chat so participants can refresh their chat list.
            await Clients.All.SendAsync("ReceiveNewChat", privateChat);
        }
    }
}
