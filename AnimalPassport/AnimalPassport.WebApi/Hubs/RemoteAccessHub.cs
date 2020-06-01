using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPassport.Entities.Entities;
using Microsoft.AspNetCore.SignalR;

namespace AnimalPassport.WebApi.Hubs
{
    public class RemoteAccessHub : Hub
    {
        public static readonly List<string> Users = new List<string>();

        public void Login()
        {
            Users.Add(Context.ConnectionId);
        }
    }
}