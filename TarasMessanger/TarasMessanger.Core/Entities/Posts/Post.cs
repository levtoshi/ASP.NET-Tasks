namespace TarasMessanger.Core.Entities.Posts;

public class Post
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = "";

    public string Text { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; }

    public List<PostPhoto> Photos { get; set; } = new();
}

