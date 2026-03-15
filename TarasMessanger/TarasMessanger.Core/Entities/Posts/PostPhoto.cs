namespace TarasMessanger.Core.Entities.Posts;

public class PostPhoto
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public string Path { get; set; } = "";

    public int SortOrder { get; set; }
}

