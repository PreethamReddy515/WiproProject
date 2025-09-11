namespace RealTimeChatApp.Backend.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<GroupMember> Members { get; set; } = new
        List<GroupMember>();
    }
    public class GroupMember
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; } = default!;
        public string Role { get; set; } = "member"; // member | admin
        public ApplicationUser User { get; set; }
    }
}
