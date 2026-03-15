using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Posts;

namespace TarasMessanger.Services;

public class PostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public Task<PostDto> CreatePost(string userId, string text, IReadOnlyList<string> photoPaths)
        => _postRepository.CreatePost(userId, text, photoPaths);

    public Task<List<PostDto>> GetFeed(int limit, int offset)
        => _postRepository.GetFeed(limit, offset);

    public Task<List<PostDto>> GetUserPosts(string userId, int limit, int offset)
        => _postRepository.GetUserPosts(userId, limit, offset);
}

