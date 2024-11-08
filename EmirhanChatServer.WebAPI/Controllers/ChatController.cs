using EmirhanChatServer.WebAPI.Context;
using EmirhanChatServer.WebAPI.Dtos;
using EmirhanChatServer.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmirhanChatServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ChatController(
        AppLicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChats(Guid userId,Guid toUserId,CancellationToken cancellationToken)
        {
            List<Chat> chats = await context.Chats.Where(p => p.UserId == userId && p.ToUserId == toUserId ||
            p.ToUserId == userId && p.UserId == toUserId).OrderBy(p => p.Date).ToListAsync(cancellationToken);

            return Ok(chats);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageDto request, CancellationToken cancellationToken)
        {
            Chat chat = new()
            {
                UserId = request.UserId,
                ToUserId = request.ToUserId,
                Message = request.message,
                Date = DateTime.Now
            };
            await context.AddAsync(chat, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
