namespace CryptoProj.Domain.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? GoogleId { get; set; }
    public decimal Balance { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}