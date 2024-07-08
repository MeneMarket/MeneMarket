using MeneMarket.Models.Foundations.Messages;
using MeneMarket.Services.Foundations.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : RESTFulController
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService) =>
            this.messageService = messageService;

        [HttpPost]
        [Authorize(Roles = "User,Admin,Owner")]
        public async ValueTask<ActionResult<Message>> PostMessageAsync(Message message) =>
            await this.messageService.AddMessageAsync(message);
    }
}