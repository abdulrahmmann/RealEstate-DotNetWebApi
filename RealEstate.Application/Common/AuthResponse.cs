using System.Net;

namespace RealEstate.Application.Common;

public class AuthResponse
{
    public string Token { get; set; }

    public bool Result { get; set; }

    public string Message { get; set; } = string.Empty;

    public HttpStatusCode HttpStatusCode { get; set; }

    public DateTime Timestamp { get; set; }

    public List<string> Errors { get; set; } = [];

    public string? Email { get; set; }

    public AuthResponse(string token, bool result, string message, HttpStatusCode httpStatusCode)
    {
        Token = token;
        Result = result;
        Message = message;
        HttpStatusCode = httpStatusCode;
        Timestamp = DateTime.UtcNow;
    }

    public AuthResponse(string token, string email, bool result, string message, HttpStatusCode httpStatusCode)
        : this(token, result, message, httpStatusCode)
    {
        Email = email;
    }
}