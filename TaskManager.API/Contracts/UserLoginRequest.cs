namespace WebApplication3.Contracts;

public record UserLoginRequest(
    string Email,
    string Password
);