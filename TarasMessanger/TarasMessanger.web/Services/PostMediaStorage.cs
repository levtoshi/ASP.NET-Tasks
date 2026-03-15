using Microsoft.AspNetCore.Components.Forms;

namespace TarasMessanger.web.Services;

public class PostMediaStorage
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".png", ".jpg", ".jpeg", ".webp"
    };

    private readonly IWebHostEnvironment _environment;

    public PostMediaStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<List<string>> SavePostImages(IReadOnlyList<IBrowserFile> files, CancellationToken cancellationToken = default)
    {
        if (files.Count == 0)
            return new List<string>();

        var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        var folderPart = Guid.NewGuid().ToString("N");

        var absoluteFolder = Path.Combine(_environment.WebRootPath, "uploads", "posts", datePart, folderPart);
        Directory.CreateDirectory(absoluteFolder);

        var result = new List<string>(files.Count);

        foreach (var file in files)
        {
            var ext = Path.GetExtension(file.Name) ?? "";
            if (!AllowedExtensions.Contains(ext))
                continue;

            var safeName = Path.GetRandomFileName().Replace(".", "") + ext.ToLowerInvariant();
            var absolutePath = Path.Combine(absoluteFolder, safeName);

            await using var fs = new FileStream(absolutePath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(fs, cancellationToken);

            var relative = $"/uploads/posts/{datePart}/{folderPart}/{safeName}";
            result.Add(relative);
        }

        return result;
    }
}

