namespace TarasMessanger.Core.DTOs.Posts;

public class PostDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = "";
    public string AuthorNickname { get; set; } = "";
    public string Text { get; set; } = "";
    public List<string> PhotoPaths { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

