using TarasMessanger.Core.DTOs.Posts;

namespace TarasMessanger.Core.Abstractions;

public interface IPostRepository
{
    Task<PostDto> CreatePost(string userId, string text, IReadOnlyList<string> photoPaths);

    Task<List<PostDto>> GetFeed(int limit, int offset);

    Task<List<PostDto>> GetUserPosts(string userId, int limit, int offset);
}

