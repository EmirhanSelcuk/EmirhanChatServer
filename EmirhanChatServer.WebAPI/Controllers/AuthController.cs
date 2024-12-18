﻿using EmirhanChatServer.WebAPI.Context;
using EmirhanChatServer.WebAPI.Dtos;
using EmirhanChatServer.WebAPI.Models;
using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmirhanChatServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(
        AppLicationDbContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request,CancellationToken cancellationToken)
        {
            bool isNameExists = await context.Users.AnyAsync(p => p.Name == request.Name,cancellationToken);

            if (isNameExists)
            {
                return BadRequest(new { Message = "Bu kullanıcı adı daha önceden kullanılmıştır" });
            }
            string avatar = FileService.FileSaveToServer(request.File, "wwwroot/avatar/");
            User user = new()
            {
                Name = request.Name,
                Avatar = avatar
            };

            await context.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Login (string name, CancellationToken cancellationToken)
        {
            User? user = await context.Users.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
            if(user is null)
            {
                return BadRequest(new { Message = "Kullanıcı bulunamadı" });
            }
            user.Status = "Online";
            await context.SaveChangesAsync(cancellationToken);
            return Ok(user);
        }
    }
}
