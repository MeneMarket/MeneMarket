using MeneMarket.Models.Foundations.Chats;
using MeneMarket.Services.Foundations.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : RESTFulController
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService) =>
            this.chatService = chatService;

        [HttpPost]
        [Authorize(Roles = "User,Admin,Owner")]
        public async ValueTask<ActionResult<Chat>> PostChatAsync(Chat chat) =>
            await this.chatService.AddChatAsync(chat);

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult<IQueryable<Chat>> GetAllchats(int pageNumber)
        {
            var allChats = this.chatService.RetrieveAllChats(pageNumber);

            return Ok(allChats);
        }

        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async ValueTask<ActionResult<Chat>> GetChatByIdAsync(Guid id) =>
            await this.chatService.RetrieveChatByIdAsync(id);

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async ValueTask<ActionResult<Chat>> DeleteChatAsync(Guid id) =>
            await this.chatService.RemoveChatAsync(id);
    }
}