using Microsoft.EntityFrameworkCore;
using TarasMessanger.Core.Abstractions;
using TarasMessanger.Core.DTOs.Posts;
using TarasMessanger.Core.Entities.Posts;

namespace TarasMessanger.Storage.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PostDto> CreatePost(string userId, string text, IReadOnlyList<string> photoPaths)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Text = (text ?? "").Trim(),
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            Photos = photoPaths
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select((p, i) => new PostPhoto
                {
                    Id = Guid.NewGuid(),
                    PostId = Guid.Empty, // set after post id is generated below
                    Path = p,
                    SortOrder = i
                })
                .ToList()
        };

        foreach (var ph in post.Photos)
        {
            ph.PostId = post.Id;
        }

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        var nickname = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => u.Nickname)
            .FirstOrDefaultAsync() ?? "";

        return new PostDto
        {
            Id = post.Id,
            UserId = post.UserId,
            AuthorNickname = nickname,
            Text = post.Text,
            PhotoPaths = post.Photos.OrderBy(x => x.SortOrder).Select(x => x.Path).ToList(),
            CreatedAt = post.CreatedAt
        };
    }

    public async Task<List<PostDto>> GetFeed(int limit, int offset)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Include(p => p.Photos)
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.CreatedAt)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return await MapPosts(posts);
    }

    public async Task<List<PostDto>> GetUserPosts(string userId, int limit, int offset)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Include(p => p.Photos)
            .Where(p => !p.IsDeleted && p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return await MapPosts(posts);
    }

    private async Task<List<PostDto>> MapPosts(List<Post> posts)
    {
        var userIds = posts.Select(p => p.UserId).Distinct().ToList();

        var nicknames = await _context.Users
            .AsNoTracking()
            .Where(u => userIds.Contains(u.Id))
            .Select(u => new { u.Id, u.Nickname })
            .ToDictionaryAsync(x => x.Id, x => x.Nickname ?? "");

        return posts
            .Select(p => new PostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                AuthorNickname = nicknames.TryGetValue(p.UserId, out var n) ? n : "",
                Text = p.Text,
                PhotoPaths = p.Photos.OrderBy(x => x.SortOrder).Select(x => x.Path).ToList(),
                CreatedAt = p.CreatedAt
            })
            .ToList();
    }
}

