namespace CryptoProj.API.Records
{
    public record Error(int StatusCode, string Message, string? Detail = null);
}