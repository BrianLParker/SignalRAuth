namespace SignalRAuth.Server.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using SignalRAuth.Server.Models;
    using System.Threading.Tasks;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            var userId = Context.UserIdentifier;
            var applicationUser = await userManager.FindByIdAsync(userId);

            await Clients.All.SendAsync("ReceiveMessage", applicationUser.Email, message);
        }
    }
}
