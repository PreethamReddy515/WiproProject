using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Backend.Data;
using RealTimeChatApp.Backend.Models;
using System.Security.Claims;

namespace RealTimeChatApp.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ChatController(AppDbContext db) { _db = db; }

        // Fetch private messages between logged-in user and another user
        [HttpGet("private/{userId}")]
        public async Task<IActionResult> GetPrivate(string userId)
        {
            var me = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
            if (me == null) return Unauthorized();

            var msgs = await _db.Messages
                .Where(m => (m.SenderId == me && m.ReceiverId == userId) ||
                            (m.SenderId == userId && m.ReceiverId == me))
                .OrderBy(m => m.SentAtUtc)
                .ToListAsync();

            return Ok(msgs);
        }

        // DTO for sending a message
        public record SendDto(string? ReceiverId, int? GroupId, string Content, string? AttachmentUrl);

        // Send a message (private or group)
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendDto dto)
        {
            var me = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
            if (me == null) return Unauthorized();

            var msg = new Message
            {
                SenderId = me,
                ReceiverId = dto.ReceiverId,
                GroupId = dto.GroupId,
                Content = dto.Content,
                AttachmentUrl = dto.AttachmentUrl, 
                SentAtUtc = DateTime.UtcNow
            };


            await _db.Messages.AddAsync(msg);
            await _db.SaveChangesAsync();

            return Ok(msg);
        }

        // Fetch messages for a group
        [HttpGet("group/{groupId:int}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var msgs = await _db.Messages
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.SentAtUtc)
                .ToListAsync();

            return Ok(msgs);
        }
    }
}
