using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Backend.Data;
using RealTimeChatApp.Backend.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _um;

    public GroupController(AppDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }

    // DTOs
    public record CreateGroupDto(string Name, List<string> MemberUsernames);
    public record EditGroupMembersDto(int GroupId, List<string> Usernames);

    /// <summary>
    /// Create a new group by providing usernames.
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateGroupDto dto)
    {
        var g = new Group { Name = dto.Name };
        _db.Groups.Add(g);
        await _db.SaveChangesAsync();

        var addedMembers = new List<string>();

        foreach (var username in dto.MemberUsernames.Distinct())
        {
            var user = await _um.Users.FirstOrDefaultAsync(u => u.DisplayName == username || u.UserName == username);
            if (user != null)
            {
                _db.GroupMembers.Add(new GroupMember
                {
                    GroupId = g.Id,
                    UserId = user.Id,
                    Role = "member"
                });
                addedMembers.Add(user.DisplayName ?? user.UserName);
            }
        }

        if (!addedMembers.Any())
        {
            return BadRequest("No valid users found with given usernames.");
        }

        await _db.SaveChangesAsync();

        return Ok(new { Group = g, Members = addedMembers });
    }

    /// <summary>
    /// Add members to an existing group by usernames.
    /// </summary>
    [HttpPost("add-members")]
    public async Task<IActionResult> AddMembers([FromBody] EditGroupMembersDto dto)
    {
        var group = await _db.Groups.FindAsync(dto.GroupId);
        if (group == null) return NotFound("Group not found.");

        var addedMembers = new List<string>();

        foreach (var username in dto.Usernames.Distinct())
        {
            var user = await _um.Users.FirstOrDefaultAsync(u => u.DisplayName == username || u.UserName == username);
            if (user != null)
            {
                bool alreadyMember = await _db.GroupMembers
                    .AnyAsync(gm => gm.GroupId == dto.GroupId && gm.UserId == user.Id);

                if (!alreadyMember)
                {
                    _db.GroupMembers.Add(new GroupMember
                    {
                        GroupId = dto.GroupId,
                        UserId = user.Id,
                        Role = "member"
                    });
                    addedMembers.Add(user.DisplayName ?? user.UserName);
                }
            }
        }

        if (!addedMembers.Any())
            return BadRequest("No valid users found with given usernames.");

        await _db.SaveChangesAsync();
        return Ok(new { Message = "Members added.", Members = addedMembers });
    }

    /// <summary>
    /// Remove members from an existing group by usernames.
    /// </summary>
    [HttpPost("remove-members")]
    public async Task<IActionResult> RemoveMembers([FromBody] EditGroupMembersDto dto)
    {
        var group = await _db.Groups.FindAsync(dto.GroupId);
        if (group == null) return NotFound("Group not found.");

        var removedMembers = new List<string>();

        foreach (var username in dto.Usernames.Distinct())
        {
            var user = await _um.Users.FirstOrDefaultAsync(u => u.DisplayName == username || u.UserName == username);
            if (user != null)
            {
                var gm = await _db.GroupMembers
                    .FirstOrDefaultAsync(x => x.GroupId == dto.GroupId && x.UserId == user.Id);

                if (gm != null)
                {
                    _db.GroupMembers.Remove(gm);
                    removedMembers.Add(user.DisplayName ?? user.UserName);
                }
            }
        }

        if (!removedMembers.Any())
            return BadRequest("No valid users found with given usernames.");

        await _db.SaveChangesAsync();
        return Ok(new { Message = "Members removed.", Members = removedMembers });
    }

    /// <summary>
    /// Get all members of a specific group by group ID.
    /// </summary>
    [HttpGet("members/{groupId}")]
    public async Task<IActionResult> GetGroupMembers(int groupId)
    {
        var group = await _db.Groups.FindAsync(groupId);
        if (group == null)
            return NotFound("Group not found.");

        var members = await _db.GroupMembers
            .Where(gm => gm.GroupId == groupId)
            .Include(gm => gm.User)
            .Select(gm => new
            {
                gm.UserId,
                UserName = gm.User.DisplayName ?? gm.User.UserName,
                gm.Role
            })
            .ToListAsync();

        return Ok(members);
    }

}
